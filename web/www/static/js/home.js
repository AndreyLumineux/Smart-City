(function () {
  /** @typedef {{coord_x: number, coord_y: number, current: number, total_capacity: number}} ParkingLot */
  const font_size = 20;
  const half_marker_width = 10;
  const marker_width = half_marker_width * 2;
  const half_marker_height = 20;
  const marker_height = half_marker_height * 2;
  /** @type ParkingLot[]} */
  let parkingLots = [];
  /** @type HTMLCanvasElement */
  let canvas = document.querySelector('#root');
  /** @type HTMLImageElement */
  let map = document.querySelector('img#map');
  /** @type HTMLImageElement */
  let map_pin = document.querySelector('img#map_pin');
  let ctx = canvas.getContext('2d');

  function resize() {
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    redraw();
  }

  function redraw() {
    ctx.drawImage(map, 0, 0, canvas.width, canvas.height);
    $.each(parkingLots,
      /**
       * @param {number} _
       * @param {ParkingLot} parkingLot
       */
      function (_, parkingLot) {
      ctx.save();

      const {current, total_capacity, coord_y, coord_x} = parkingLot;
      let x = (canvas.width * coord_x / 100) - half_marker_width;
      let y = (canvas.height * coord_y / 100) - marker_height;
      let text = current + '/' + total_capacity + '';
      let measure = ctx.measureText(text);

      ctx.drawImage(map_pin, x, y, marker_width, marker_height);

      ctx.font = font_size + 'px serif';
      ctx.strokeText(text, x + half_marker_width - measure.width / 2, y + font_size * 3);

      ctx.restore();
    });
  }

  function loop() {
    $.getJSON("/api/parkingLots/get/all").done((data) => {
      parkingLots = data;
    }).always(redraw);
    setTimeout(loop, 1000);
  }

  window.addEventListener('resize', () => {
    resize();
  });

  resize();
  loop();
})();