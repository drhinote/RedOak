function OneSeriesColumnChart(Title, DivId, Left, Right, Color) {
    var data = google.visualization.arrayToDataTable([
        ["Hand", "Test 1", { role: 'style' }],
        ["Left", Left, Color],
        ["Right", Right, Color]
    ]);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        }
    ]);

    var options = {
        title: Title,
        bar: { groupWidth: '90%' },
        legend: 'none',
        colors: [Color],
        vAxis: {
            minValue: 0,
            maxValue: 400
        }
    };

    var rg_reaction_chart = new google.visualization.ColumnChart(document.getElementById(DivId));
    rg_reaction_chart.draw(view, options);
}