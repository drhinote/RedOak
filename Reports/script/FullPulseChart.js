function FullPulseChart(Title, DivId, Pulse) {
    var data = new google.visualization.arrayToDataTable(Pulse);

    var options = {
        title: Title,
        titleTextStyle: {
            fontSize: 18,
            bold: true
        },
        hAxis: {
            title: 'Elapsed Time (sec)',
            viewWindow: {
                min: 0,
                max: 60
            }
        },
        vAxis: {
            title: 'Applied Force (kg-f)',
            viewWindow: {
                min: 0
            }
        },
        legend: 'right',
        interpolateNulls: true,
        series: {
            0: { lineWidth: 1, pointSize: 0 },
            1: { lineWidth: 1, pointSize: 0 }
        }
    };

    var left_pulse_chart = new google.visualization.ScatterChart(document.getElementById(DivId));
    left_pulse_chart.draw(data, options);
}