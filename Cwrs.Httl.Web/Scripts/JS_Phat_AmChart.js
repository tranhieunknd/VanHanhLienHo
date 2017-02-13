
var amChartClick = function () {
    console.log('vao roi nhe');
    var url = '/BieuDo/BieuDoLuuLuong_Data';
    var codeHo = $("#tenHo").val();
    var khoangTG = $("#khoangTG").val();

    var thoiGianBatDau = $("#thoiGianBatDau").val();

    var data = {
        tenHo: codeHo,
        thoiGianBatDau: thoiGianBatDau,
        khoangTG: khoangTG
    }

    CallDataBieuDo("chartdiv", 2, 1, thoiGianBatDau, khoangTG);
}

var colorLine = ['#FF0000', '#FFFF00', '#00CC00', '#0033FF', '#FF6600', '#00FFFF'];

var CallDataBieuDo = function (chartName, dataType, viTriCode, dataTime, khoangTG) {
    var url = '/BieuDo/GetDataTuVanByHo';

    console.log('vao roi nhe');

    function AfterAjax(result) {
        // console.log(result);
        lstData = result.Data;
        lstDonVi = result.DonVi;
        lstCode = result.Code;

        // Format du lieu ve dinh dang cua Chart
        var chartData = [];
        var countLine = lstDonVi.length;
        console.log(countLine);

        for (var i = 0; i < lstData.length; i += countLine) {
            var tmp = { date: new Date(parseInt(lstData[i].thoiGianTao.substr(6))) }
            for (var j = 0; j < countLine; j++) {
                console.log((i + j) + " | " + lstData[i + j].giaTri);
                tmp["line" + lstCode[j]] = lstData[i + j].giaTri;
            }
            console.log(tmp);
            chartData.push(tmp);
        }

        var valueAxesData = [];
        var graphsData = [];

        // Thiet lap mot so thong so cho Chart
        for (var k = 0; k < countLine; k++) {
            var tmpAxes = {
                "id": "v" + lstCode[k],
                //"axisColor": lstDonVi[k],
                //"axisThickness": 2,
                // "axisAlpha": 1,
                "position": "left",
                "axisAlpha": 0,
                "ignoreAxisWidth": true
            }
            valueAxesData.push(tmpAxes);
            // console.log(valueAxesData);

            var tmpGraph = {
                "id": "v" + lstCode[k],
                "lineColor": colorLine[k],
                "bullet": "round",
                "bulletBorderThickness": 1,
                "bulletSize": 5,
                "hideBulletsCount": 50,
                // "hideBulletsCount": 30,
                "lineThickness": 2,
                "title": lstDonVi[k],
                "valueField": "line" + lstCode[k],
                "useLineColorForBulletBorder": true,
                "fillAlphas": 0
            }
            graphsData.push(tmpGraph);
            // console.log(graphsData);
        }

        // Khai bao cau hinh cho Chart
        var configChart = {
            "type": "serial",
            "theme": "light",
            "legend": {
                "useGraphSettings": false
            },
            "dataProvider": chartData,
            "synchronizeGrid": true,
            "valueAxes": valueAxesData,
            "graphs": graphsData,
            "chartScrollbar": {},
            "chartCursor": {
                "cursorPosition": "mouse",
                "dateFormat": "YYYY-MM-DD HH:MM:SS"

            },
            "categoryField": "date",
            "categoryAxis": {
                "minPeriod": "hh",
                "parseDates": true,
                "axisColor": "#DADADA",
                "minorGridEnabled": true,
            },
            "export": {
                "enabled": true,
                "position": "bottom-right",
                "dateFormat": "YYYY-MM-DD HH:MM:SS"
            },
            "listeners": [{
                "event": "rendered",
                "method": function (e) {
                    // e.chart.valueAxes[0].zoomToValues(30, 70);
                }
            }]
        }
        console.log(configChart);
        // Khoi tao Chart
        var chart = AmCharts.makeChart(chartName, configChart);

        function zoomChart() {
            chart.zoomToIndexes(chart.dataProvider.length - 20, chart.dataProvider.length - 1);
        }
        chart.addListener("dataUpdated", zoomChart);
        zoomChart();
    }

    var data = {
        dataType: dataType,
        viTriCode: viTriCode,
        thoiGianBatDau: dataTime,
        khoangTG: khoangTG
    }

    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: AfterAjax
    });
}
