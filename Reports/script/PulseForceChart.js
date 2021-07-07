function PulseForceChart(Title, DivId, Pulse) {
    var data = new google.visualization.arrayToDataTable(Pulse);

    var options = {
        title: Title,
        titleTextStyle: {
            fontSize: 18,
            bold: true
        },
        hAxis: {
            title: 'Applied Force (kg-f)',
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
        legend: 'bottom',
        interpolateNulls: true,
        series: {
            0: { lineWidth: 1, pointSize: 0 },
            1: { lineWidth: 1, pointSize: 0 },
            2: { lineWidth: 1, pointSize: 0 }
        }
    };

    var chart = new google.visualization.ScatterChart(document.getElementById(DivId));
    chart.draw(data, options);
}