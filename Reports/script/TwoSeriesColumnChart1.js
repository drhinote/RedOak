function TwoSeriesColumnChart1(Title, DivId, History) {

    if (typeof History !== 'undefined') {

        var data = google.visualization.arrayToDataTable(History);

        var view = new google.visualization.DataView(data);

        var options = {
            title: Title,
            legend: { position: 'none' },
            colors: ['silver', 'blue'],
            vAxis: {
                minValue: 0,
                maxValue: 10
            }
        };

        var chart = new google.visualization.ColumnChart(document.getElementById(DivId));
        chart.draw(view, options);
    }
}