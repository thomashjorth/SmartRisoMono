google.load("visualization", "1", { packages: ["gauge"] });
var chart, data, options;
function drawGauge(min, max, label) {

    data = google.visualization.arrayToDataTable([
      ['Label', 'Value'],
      [label, 0]
    ]);

    var diff = max - min;

    options = {
        //width: 800, height: 300,
        min: min, max: max,
        redFrom: max - (diff / 10), redTo: max,
        yellowFrom: max - (diff / 4), yellowTo: max - (diff / 10),
        minorTicks: 5
    };

    chart = new google.visualization.Gauge(document.getElementsByClassName('gauge')[0]);

    chart.draw(data, options);

}

function updateValue(newValue) {
    data.setValue(0, 1, newValue);
    chart.draw(data, options);
}