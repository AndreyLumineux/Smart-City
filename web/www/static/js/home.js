(function () {
  /** @type HTMLCanvasElement */
  let canvas = document.querySelector('#root');
  /** @type HTMLImageElement */
  let map = document.querySelector('img#map');
  let ctx = canvas.getContext('2d');

  function resize() {
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    redraw();
  }

  function redraw() {
    ctx.drawImage(map, 0, 0, canvas.width, canvas.height);
  }

  window.addEventListener('resize', () => {
    resize();
  });

  resize();
})();