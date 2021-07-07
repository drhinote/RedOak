function DoubleGaugeChart(DivId, Left, Right) {

    var data = google.visualization.arrayToDataTable([
        ["Hand", "Value"],
        ["L Injury", Left],
        ["R Injury", Right]
    ]);

    var view = new google.visualization.DataView(data);

    var options = {
        redFrom: 2, redTo: 10,
        yellowFrom: 1, yellowTo: 2,
        greenFrom: 0, greenTo: 1,
        min: 0,
        max: 10
    };

    var chart = new google.visualization.Gauge(document.getElementById(DivId));
    chart.draw(view, options);
}