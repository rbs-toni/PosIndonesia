const data = new Map()

const fetchJson = async (url) => {
  const response = await fetch(url)
  return response.ok ? response.json() : null
}

export const getGeo = async () => {
  try {
    const { ip } = await fetchJson('https://get.geojs.io/v1/ip.json')
    return ip ? fetchJson(`https://get.geojs.io/v1/ip/geo/${ip}.json`) : null
  } catch (e) {
    console.error("Failed to fetch geo data", e)
  }
}

export const leaflet = {
  init: (id, latlng, zoom) => {
    if (typeof L === 'undefined') {
      console.warn("Leaflet is not loaded")
      return
    }
    if (data.has(id)) {
      return data.get(id)
    }

    const map = L.map(id, {
      center: latlng,
      zoom: zoom
    })

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map)

    const marker = L.marker(latlng).addTo(map)

    data.set(id, { map, marker })
    return map
  },
  setView: (id, latlng, zoom = 13) => {
    const { map, marker } = data.get(id)
    if (!map) {
      console.warn(`Map with id "${id}" is not initialized`)
      return
    }
    map.setView(latlng, zoom)
    marker.setLatLng(latlng)
  },
  flyTo: (id, latlng, zoom = 13) => {
    const { map, marker } = data.get(id)
    if (!map) {
      console.warn(`Map with id "${id}" is not initialized`)
      return
    }
    map.flyTo(latlng, zoom)
    marker.setLatLng(latlng)
  },
  dispose: (id) => {
    if (!data.has(id)) {
      console.warn(`Map with id "${id}" is not initialized`)
      return
    }

    const instance = data.get(id)
    instance.map.remove()
    data.delete(id)
  }
}

