const DB_NAME = 'PostalDB'
const STORE_NAME = 'postalData'
const STATS_STORE = 'postalStats'
const URL_SR = './_content/PosIndonesia/modified-kodepos.json'

const openDB = () => new Promise((resolve, reject) => {
  const request = indexedDB.open(DB_NAME, 1)

  request.onupgradeneeded = event => {
    const db = event.target.result
    if (!db.objectStoreNames.contains(STORE_NAME)) {
      db.createObjectStore(STORE_NAME)
    }
    if (!db.objectStoreNames.contains(STATS_STORE)) {
      db.createObjectStore(STATS_STORE)
    }
  }

  request.onsuccess = event => resolve(event.target.result)
  request.onerror = event => reject(event.error)
})

const getFromDB = async (storeName, key) => {
  const db = await openDB()
  return new Promise((resolve, reject) => {
    const tx = db.transaction(storeName, 'readonly')
    const store = tx.objectStore(storeName)
    const request = store.get(key)

    request.onsuccess = () => resolve(request.result || null)
    request.onerror = () => reject(null)
  })
}

const saveToDB = async (storeName, key, data) => {
  const db = await openDB()
  return new Promise((resolve, reject) => {
    const tx = db.transaction(storeName, 'readwrite')
    const store = tx.objectStore(storeName)
    store.put(data, key)

    tx.oncomplete = () => resolve()
    tx.onerror = () => reject()
  })
}

export const get = async (version) => {
  const cacheKey = `postalData_v${version}`
  let cachedData = await getFromDB(STORE_NAME, cacheKey)
  if (cachedData) return cachedData

  const response = await fetch(URL_SR)
  if (!response.ok) throw new Error('Failed to fetch JSON')

  const data = await response.json()
  await saveToDB(STORE_NAME, cacheKey, data)
  return data
}

export const getStats = async (version) => {
  console.log(version)
  const cacheKey = `postalStats_v${version}`
  let cachedStats = await getFromDB(STATS_STORE, cacheKey)

  if (cachedStats) return cachedStats  // Return the cached stats

  const data = await get(version)  // Fetch postal data
  const stats = {
    provincesCount: new Set(data.map(item => item.Province)).size,
    regenciesCount: new Set(data.map(item => item.Regency)).size,
    districtsCount: new Set(data.map(item => item.District)).size,
    villagesCount: new Set(data.map(item => item.Village)).size
  }

  // Cache the newly calculated stats in IndexedDB
  await saveToDB(STATS_STORE, cacheKey, stats)
  console.log(stats)
  return stats  // Return the newly computed stats
}

export const filter = async (version, keyword, sortOptions = [], page = 1, pageSize = 10) => {
  console.log(sortOptions)
  const data = await get(version) // Fetch from IndexedDB or JSON
  let filteredData = data
  if (keyword) {
    // Step 1: Filter data
    filteredData = data.filter(item =>
      Object.values(item).some(value =>
        String(value).toLowerCase().includes(keyword.toLowerCase())
      )
    )
  }
  if (sortOptions) {
    // Step 2: Sort data
    if (sortOptions.length > 0) {
      filteredData.sort((a, b) => {
        for (const { name, dir } of sortOptions) {
          if (!a.hasOwnProperty(name) || !b.hasOwnProperty(name)) continue

          const valA = a[name]
          const valB = b[name]

          if (valA < valB) return dir === 'asc' ? -1 : 1
          if (valA > valB) return dir === 'asc' ? 1 : -1
        }
        return 0
      })
    }
  }

  // Step 3: Pagination
  const totalItems = filteredData.length
  const totalPages = Math.ceil(totalItems / pageSize)
  const hasNextPage = page < totalPages
  const hasPreviousPage = page > 1

  const paginatedData = pageSize == -1 ? filteredData : filteredData.slice((page - 1) * pageSize, page * pageSize)
  const result = {
    page,
    pageSize,
    hasNextPage,
    hasPreviousPage,
    totalItems,
    totalPages,
    items: paginatedData
  }
  return result
}
