function SinglePulseChart(Title, DivId, Pulse) {
    var data = new google.visualization.DataTable();
    data.addColumn('number', 'X');
    data.addColumn('number', 'Thumb');
    data.addColumn('number', 'Index');
    data.addColumn('number', 'Small');

    data.addRows(Pulse);

    var options = {
        title: Title,
        titleTextStyle: {
            fontSize: 18,
            bold: true
        },
        hAxis: {
            title: 'Elapsed Time (sec)',
            viewWindow: {
                min: 0
            }
        },
        vAxis: {
            title: 'Applied Force (kg-f)',
            viewWindow: {
                min: 0
            }
        },
        series: {
            1: { curveType: 'function' }
        },
        legend: 'bottom'
    };

    var chart = new google.visualization.LineChart(document.getElementById(DivId));
    chart.draw(data, options);
}