//var chartdata = [{
//    "date": "2012-07-27 01:00:00",
//    "value": 13,
//    "value2": 19
//}, {
//    "date": "2012-07-27 07:00:00",
//    "value": 11,
//    "value2": 30
//}, {
//    "date": "2012-07-27 13:00:00",
//    "value": 15,
//    "value2": 28
//}, {
//    "date": "2012-07-27 19:00:00",
//    "value": 16,
//    "value2": 26
//}, {
//    "date": "2012-07-28 01:00:00",
//    "value": 18,
//    "value2": 30
//}, {
//    "date": "2012-07-28 07:00:00",
//    "value": 13,
//    "value2": 28
//}, {
//    "date": "2012-07-28 13:00:00",
//    "value": 22,
//    "value2": 23
//}, {
//    "date": "2012-07-28 19:00:00",
//    "value": 23,
//    "value2": 19
//}, {
//    "date": "2012-07-29 01:00:00",
//    "value": 20,
//    "value2": 20
//}

////]//,[]
//];

function ve_Chart(chartdata, data) {
    console.log('vao ham');

    AmCharts.ready(function() {
        // định dạng biểu đồ
        console.log('vao chart');
        chart = new AmCharts.AmSerialChart();
        chart.theme = "light";
        chart.dataProvider = data;
        chart.marginRight = 40;
        chart.marginLeft = 40;
        chart.mouseWheelZoomEnabled = true;
        chart.dataDateFormat = "YYYY-MM-DD HH:mm:ss";
        chart.categoryField = "thoi_gian";
        chart.synchronizeGrid = true;
        chart.dataDateFormat = "YYYY-MM-DD JJ:mm:SS";
        chart.addListener("rendered", zoomChart);

        var categoryAxis = chart.categoryAxis;
        categoryAxis.parseDates = true;
        categoryAxis.minPeriod = "hh";
        categoryAxis.minorGridEnabled = false;
        categoryAxis.axisColor = "#DADADA"; // màu của trục
        //categoryAxis.twoLineMode = true;

        var lengend = new AmCharts.AmLegend();
        lengend.valueAlign = "right";
        lengend.valueWidth = 60;
        lengend.useGraphSettings = false;
        chart.addLegend(lengend);

        var valueAxes = new AmCharts.ValueAxis();
        valueAxes.axisColor = "#B0DE09";
        valueAxes.axisThickness = 2;
        valueAxes.gridAlpha = 0;
        //valueAxes.offset = 50;
        valueAxes.dashLength = 1;
        valueAxes.position = "left";
        valueAxes.axisAlpha = 1;
        valueAxes.unit = "(m)";
        chart.addValueAxis(valueAxes);

        for (var i = 0; i < chartdata.length; i++) {
            var graph1 = new AmCharts.AmGraph();
            graph1.valueAxis = valueAxes; // we have to indicate which value axis should be used
            graph1.valueField = chartdata[i].dataName;
            graph1.bullet = "round";
            graph1.hideBulletsCount = 30;
            graph1.bulletBorderThickness = 1;
            graph1.title = chartdata[i].title;
            graph1.fillColors = chartdata[i].color;
            graph1.bulletColor = "#95cfea";
            graph1.lineColor = "#95cfea";
            //graph1.useLineColorForBulletBorder = true;
            graph1.balloonText = "<span style='font-size:10px;'> Value: [[value]]</span>";
            chart.addGraph(graph1);
            console.log(graph1);
        }
        var scollBar = new AmCharts.ChartScrollbar();

        scollBar.oppositeAxis = true;
        scollBar.offset = 30;
        scollBar.scrollbarHeight = 40;
        scollBar.backgroundAlpha = 0;
        scollBar.selectedBackgroundAlpha = 0.1;
        scollBar.selectedBackgroundColor = "#333300";
        scollBar.graphFillAlpha = 0;
        scollBar.graphLineAlpha = 0.5;
        scollBar.selectedGraphFillAlpha = 0;
        scollBar.selectedGraphLineAlpha = 1;
        scollBar.graph = graph1;
        scollBar.autoGridCount = true;
        scollBar.color = "#333300";
        scollBar.cursorPosition = "mouse";
        chart.addChartScrollbar(scollBar);

        var valueScrollbar = new AmCharts.ChartScrollbar;
        valueScrollbar.oppositeAxis = true;
        valueScrollbar.offset = 50;
        valueScrollbar.scrollbarHeight = 10;

        chart.addValueScrollbar(valueScrollbar);

        var chartCursor = new AmCharts.ChartCursor();
        chartCursor.pan = true;
        chartCursor.valueLineBalloonEnabled = true;
        chartCursor.valueBalloonsEnabled = true;
        chartCursor.cursorAlpha = 1;
        chartCursor.cursorColor = "#0066cc";

        chartCursor.valueLineAlpha = 0.05;
        chartCursor.fullWidth = false; // đây là chỗ chỉnh đường gióng
        chartCursor.valueZoomable = true;
        //chartCursor.graphBulletSize = 1.7;
        chartCursor.bulletSize = 1;
        chartCursor.cursorPosition = "mouse";
        chartCursor.categoryBalloonDateFormat = "DD-MM-YYYY HH:NN:SS";
        chart.addChartCursor(chartCursor);

        chart.write("chartdiv");
        zoomChart();
        function zoomChart() { chart.zoomToIndexes(chart.dataProvider.length - 40, chart.dataProvider.length - 1); }

    });

} 