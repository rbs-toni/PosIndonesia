export function animate(id, number, duration = 3000) {
  const counterElement = document.getElementById(id);
  const startValue = 0;
  let startTime = null;

  function updateNumber(currentTime) {
    if (!startTime) startTime = currentTime;
    const elapsedTime = currentTime - startTime;

    // Calculate the progress as a fraction of total duration
    const progress = Math.min(elapsedTime / duration, 1);

    // Calculate the number increment based on progress
    const currentValue = Math.floor(startValue + (number - startValue) * progress * progress); // Quadratic easing for progressive increment

    counterElement.textContent = currentValue;

    // Continue animation until target value is reached
    if (progress < 1) {
      requestAnimationFrame(updateNumber);
    } else {
      counterElement.textContent = number; // Ensure target is reached
    }
  }

  requestAnimationFrame(updateNumber);
}
