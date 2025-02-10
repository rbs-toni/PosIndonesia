const observer = (el) => new IntersectionObserver((entries, observer) => {
  entries.forEach(entry => {
    if (entry.isIntersecting) {
      if (entry.target === el) {
        el.classList.add('top')
      }
    }
  })
})

export function init(id) {
  if (id) {
    const el = document.getElementById(id)
    const target = document.getElementById('PosNav')
    if (el && target) {
      const obs = observer(el)
      obs.observe(target)
    }
  }
}