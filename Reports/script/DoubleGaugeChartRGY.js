function DoubleGaugeChartRGY(DivId, Left, Right) {

    var data = google.visualization.arrayToDataTable([
        ["Hand", "Value"],
        ["Left", Left],
        ["Right", Right]
    ]);

    var view = new google.visualization.DataView(data);

    var options = {
        greenFrom: 263, greenTo: 393,
        min: 133,
        max: 523
    };

    var chart = new google.visualization.Gauge(document.getElementById(DivId));
    chart.draw(view, options);
}