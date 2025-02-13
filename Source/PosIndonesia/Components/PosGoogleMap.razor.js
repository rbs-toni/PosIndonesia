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

export const googleMap = {
  init: async (id, latlng, zoom = 10) => {
    if (id) {
      const el = document.getElementById(id)
      if (el) {
        const { Map } = await google.maps.importLibrary("maps")
        const { AdvancedMarkerElement } = await google.maps.importLibrary("marker")
        const map = new Map(el, {
          zoom: zoom,
          center: latlng,
        })

        const marker = new AdvancedMarkerElement({
          map: map,
          position: latlng,
          title: "Pos Indonesia"
        })

        data.set(id, { el, map, marker })
      }
    }
  },
  setCenter: (id, latlng, zoom = 13) => {
    const { map, marker } = data.get(id)
    if (!map) {
      console.warn(`Map with id "${id}" is not initialized`)
      return
    }
    map.setCenter(latlng)
    marker.setPosition(latlng)
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

