function OneSeriesBarChart(DivId, Left, LeftColor, Right, RightColor) {

    var data = google.visualization.arrayToDataTable([
        ["Hand", "", { role: 'style' }],
        ["Left", Left, LeftColor],
        ["Right", Right, RightColor]
    ]);

    var view = new google.visualization.DataView(data);

    var options = {
        bar: { groupWidth: '90%' },
        legend: { position: 'none' },
        hAxis: {
            minValue: 0,
            maxValue: 10
        }
    };

    var chart = new google.visualization.BarChart(document.getElementById(DivId));
    chart.draw(view, options);
}