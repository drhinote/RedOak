function TwoSeriesBarChart(DivId, History) {

    if (History !== null) {

        var data = google.visualization.arrayToDataTable(History);

        var view = new google.visualization.DataView(data);

        var options = {
            bar: { groupWidth: '90%' },
            legend: { position: 'none' },
            colors: ['silver', 'blue'],
            hAxis: {
                minValue: 0,
                maxValue: 10
            }
        };

        var chart = new google.visualization.BarChart(document.getElementById(DivId));
        chart.draw(view, options);
    }
}