function DoubleGaugeChartRG(DivId, Left, Right) {

    var data = google.visualization.arrayToDataTable([
        ["Hand", "Value"],
        ["Left", Left],
        ["Right", Right]
    ]);

    var view = new google.visualization.DataView(data);

    var options = {
        greenFrom: 220, greenTo: 316,
        min: 124,
        max: 412
    };

    var chart = new google.visualization.Gauge(document.getElementById(DivId));
    chart.draw(view, options);
}