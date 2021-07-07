function TwoSeriesColumnChart(Title, DivId, Left2s, Left3s, Right2s, Right3s) {

    google.charts.load("current", { packages: ['corechart', 'bar', 'line'] });

    var data = google.visualization.arrayToDataTable([
        ["Hand", "Test 1", "Test 2"],
        ["Left", Left2s, Left3s],
        ["Right", Right2s, Right3s]
    ]);

    var formatter = new google.visualization.NumberFormat({ pattern: '#.##%' });
    formatter.format(data, 1);
    formatter.format(data, 2);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        },
        2,
        {
            calc: "stringify",
            sourceColumn: 2,
            type: "string",
            role: "annotation"
        }
    ]);

    var options = {
        title: Title,
        bar: { groupWidth: '90%' },
        legend: { position: 'bottom' },
        vAxis: {
            minValue: 0,
            maxValue: 1,
            format: 'percent'
        },
        series: {
            0: { color: 'blue' },
            1: { color: 'silver' }
        }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById(DivId));
    chart.draw(view, options);
}