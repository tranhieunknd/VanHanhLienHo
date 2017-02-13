var colorLine = ['#FF0000', '#00CC00', '#FFFF00', '#0033FF', '#FF6600', '#00FFFF', '#473C8B', '#54FF9F', '#FFF68F', '#9370DB'];

var BieuDo = {
    SoSanh: function () {

        var toDate = $("#toDate").val();
        var fromDate = $("#fromDate").val();
        var viTriCode = $("#viTriCode").val();
        var data = {
            viTriCode: viTriCode,
            fromDate: fromDate,
            toDate: toDate
        };
        console.log(data);

        // BieuDo.CallDataBieuDo("chartdiv", 1, 1, fromDate, "24");

        // Lấy dữ liệu cho biểu đồ
        $.ajax({
            type: "Post",
            data: data,
            url: "BieuDo/GetDataSoSanh",
            success: function (dt) {
                if (dt != null) {
                    var config = {
                        time: "thoi_gian",
                        legend: "legenddiv",
                        data: [
                            {
                                Code: 1,
                                Name: "Trung Tâm Khí Tượng Thủy Văn Trung Ương",
                                KeyValue: "mucnuoc_KTTVTW"
                            }, {
                                Code: 2,
                                Name: "Viện Quy Hoạch Thủy Lợi",
                                KeyValue: "mucnuoc_VQHTL"
                            }, {
                                Code: 3,
                                Name: "Viện Khoa Học Thủy Lợi",
                                KeyValue: "mucnuoc_KHTL"
                            }, {
                                Code: 4,
                                Name: "Đại Học Thủy Lợi",
                                KeyValue: "mucnuoc_DHTL"
                            }, {
                                Code: 5,
                                Name: "Viện Khí Tượng Thủy Văn",
                                KeyValue: "mucnuoc_KTTV"
                            }, {
                                Code: 6,
                                Name: "Viện Cơ Học",
                                KeyValue: "mucnuoc_VCH"
                            }, {
                                Code: 7,
                                Name: "Thực Đo",
                                KeyValue: "MucNuocThucDo"
                            },
                        ]
                    }
                    $("#chartdiv").show();
                    BieuDo.RenderChart("chartdiv", dt, config);
                } else {
                    $("#chartdiv").hide();
                    toastr.error("Không có dữ liệu!", "THÔNG BÁO");
                }
            }
        });

        // Lấy lại bảng dữ liệu
        $.ajax({
            type: "Post",
            data: data,
            url: "BieuDo/BieuDoSoSanhTable",
            success: function (dt) {
                $('#tableSoSanh').html(dt);
            }
        });
    },
    LuuLuong: function () {
        var codeHo = $("#tenHo").val();
        var khoangTG = $("#khoangTG").val();
        var thoiGianBatDau = $("#thoiGianBatDau").val();

        BieuDo.CallDataBieuDo("chartdiv", 1, codeHo, thoiGianBatDau, khoangTG);
        var data = {
            hoCode: codeHo,
            fromDate: thoiGianBatDau,
            hours: khoangTG
        }
        //console.log(data);
        // Lấy lại bảng dữ liệu
        $.ajax({
            type: "Post",
            data: data,
            url: "BieuDo/BieuDoLuuLuongTable",
            success: function (dt) {
                $("#tableLuuLuongDenHo").html(dt);
            }
        });
    },
    MucNuoc: function () {

        var codeHo = $("#tenHo_mn").val();
        var khoangTG = $("#khoangTG_mn").val();
        var thoiGianBatDau = $("#thoiGianBatDau_mn").val();

        BieuDo.CallDataBieuDo("chartdiv_mn", 2, codeHo, thoiGianBatDau, khoangTG);
        var data = {
            tenHo: codeHo,
            thoiGianBatDau: thoiGianBatDau,
            khoangTG: khoangTG
        }
        //console.log(data);
        // Lấy lại bảng dữ liệu
        $.ajax({
            type: "Post",
            data: data,
            url: "BieuDo/BieuDoMucNuocTable",
            success: function (dt) {
                $("#tableMucNuoc").html(dt);
            }
        });

    },
    MucNuocHaLuu: function () {
        var codeDonVi = $("#MNHL_donViCode").val();
        var thoiGianBatDau = $("#MNHL_fromDate").val();
        var khoangTG = $("#MNHL_hours").val();
        var data = {
            donViCode: codeDonVi,
            fromDate: thoiGianBatDau,
            hours: khoangTG
        }
        // Lấy lại bảng dữ liệu

        $.ajax({
            type: "Post",
            data: data,
            url: "BieuDo/BieuDoMucNuocHaLuuTable",
            success: function (dt) {
                $("#tableMucNuocHaLuu").html(dt);
            }
        });

        // Lấy dữ liệu biểu đồ mnhl
        var codeDonVi = $("#MNHL_donViCode").val();
        var thoiGianBatDau = $("#MNHL_fromDate").val();
        var khoangTG = $("#MNHL_hours").val();
        var pa_number = $("#MNHL_Cbphuongan").val();
        var data = {
            ten_Dv: codeDonVi,
            thoiGianBD: thoiGianBatDau,
            khoangTG: khoangTG,
            pa_Number: pa_number
        }
        $.ajax({
            type: "Post",
            data: data,
            url: "BieuDo/BieuDoMucNuocHaLuu_test",
            success: function (dt) {


                var config = {
                    time: "thoi_gian",
                    data: [
                        {
                            Code: 1,
                            Name: " Mực nước Hà Nội",
                            KeyValue: "mucnuoc_HaNoi"
                        }, {
                            Code: 2,
                            Name: "Mực nước Phả Lại",
                            KeyValue: "mucnuoc_PhaLai"
                        }, {
                            Code: 3,
                            Name: "Mực nước Tuyên Quang",
                            KeyValue: "mucnuoc_TuyenQuang"
                        },
                    ]
                }
                console.log(dt);
                BieuDo.RenderChart("chartdiv_mnhl", dt, config);
            }
        });

    },
    /**
     * HieuTM: 
     * @param {} chartName 
     * @param {} dataType 
     * @param {} viTriCode 
     * @param {} dataTime 
     * @param {} khoangTG 
     * @returns {} 
     */
    CallDataBieuDo: function (chartName, dataType, viTriCode, dataTime, khoangTG) {
        var url = '/BieuDo/GetDataTuVanByHo';

        console.log('vao roi nhe');

        function AfterAjax(result) {

            // console.log(result);
            lstData = result.Data;
            lstDonVi = result.DonVi;
            lstCode = result.Code;
            // {"DonVi":[],"Data":[],"Code":[]}
            if (lstData != [] & lstCode != [] & lstCode != []) {

                // Format du lieu ve dinh dang cua Chart
                var chartData = [];
                var countLine = lstDonVi.length;
                console.log(countLine);

                for (var i = 0; i < lstData.length; i += countLine) {
                    var tmp = { date: new Date(parseInt(lstData[i].thoiGianTao.substr(6))) }
                    for (var j = 0; j < countLine; j++) {
                        tmp["line" + lstCode[j]] = lstData[i + j].giaTri;
                    }
                    chartData.push(tmp);
                }

                var valueAxesData = [];
                var graphsData = [];

                // Thiet lap mot so thong so cho Chart
                for (var k = 0; k < countLine; k++) {

                    // console.log(valueAxesData);

                    var tmpGraph = {
                        "id": "v" + lstCode[k],
                        "lineColor": colorLine[k],
                        "bullet": "round",
                        "bulletBorderThickness": 1,
                        "bulletSize": 5,
                        "hideBulletsCount": 50,
                        "valueAxis": "ax1",
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
                        "divId": "legenddiv"
                        // "useGraphSettings": false
                    },
                    "dataProvider": chartData,
                    "synchronizeGrid": true,
                    //Định nghĩa trục y cho biểu đồ
                    "valueAxes": [{
                        "id": "ax1",
                        "axisColor": "#FF6600",
                        "axisThickness": 2,
                        "axisAlpha": 1,
                        "position": "left"
                    }],
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

                $('#message').hide();
                $('#' + chartName).show();

                // Khoi tao Chart
                var chart = AmCharts.makeChart(chartName, configChart);

                function zoomChart() {
                    chart.zoomToIndexes(chart.dataProvider.length - 20, chart.dataProvider.length - 1);
                }
                chart.addListener("dataUpdated", zoomChart);
                zoomChart();
            } else {
                $('#message').show();
                $('#' + chartName).hide();
            }
        }

        var data = {
            dataType: dataType,
            viTriCode: viTriCode,
            thoiGianBatDau: dataTime,
            khoangTG: khoangTG
        }
        console.log(data);
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: AfterAjax
        });
    },
    TableSoSanh: function myfunction() {
    },
    /**
     * HieuTM: Hàm sinh biểu đồ tự động khi truyền dữ liệu
     * @param {Tên tag div chứa biểu đồ} chartName 
     * @param {} chartData 
     * @param {là mảng obj có thuộc tính Code, Name} configHeader 
     * @returns {} 
     */
    RenderChart: function (chartName, chartData, config) {
        var configTime = config.time;
        var configHeader = config.data;
        var configLegend = config.legend;
        var graphsData = [];
        console.log('đã vào hạ lưu');
        for (var i = 0; i < chartData.length; i++) {
            chartData[i][configTime] = new Date(parseInt(chartData[i][configTime].substr(6)));
        }

        // Thiet lap mot so thong so cho Chart
        for (var k = 0; k < configHeader.length; k++) {
            var tmpGraph = {
                "id": "v" + configHeader[k].Code,
                "lineColor": colorLine[k],
                "bullet": "round",
                "bulletBorderThickness": 1,
                "bulletSize": 5,
                "hideBulletsCount": 50,
                "valueAxis": "ax1",
                "lineThickness": 2,
                "title": configHeader[k].Name,
                "valueField": configHeader[k].KeyValue,
                "useLineColorForBulletBorder": true,
                "type": "smoothedLine",
                "fillAlphas": 0

                //"id": "v" + configHeader[k].Code,
                //"lineColor": colorLine[k],
                //"bullet": "round",
                //"bulletBorderThickness": 1,
                //"bulletSize": 5,
                //"hideBulletsCount": 50,
                //"valueAxis": "ax1",
                //"lineThickness": 2,
                //"title": configHeader[k].Name,
                //"valueField": configHeader[k].KeyValue,
                //"useLineColorForBulletBorder": true,
                //"fillAlphas": 0
            }
            graphsData.push(tmpGraph);
        }

        // Khai bao cau hinh cho Chart
        var configChart = {
            "type": "serial",
            "theme": "light",
            "legend": {
                "divId": configLegend// "legenddiv"
                // "useGraphSettings": false
            },
            "dataProvider": chartData,
            "synchronizeGrid": true,
            //Định nghĩa trục y cho biểu đồ
            "valueAxes": [{
                "id": "ax1",
                "axisColor": "#FF6600",
                "axisThickness": 2,
                "axisAlpha": 1,
                "position": "left"
            }],
            "graphs": graphsData,
            "chartScrollbar": {},
            "chartCursor": {
                "cursorPosition": "mouse",
                "dateFormat": "YYYY-MM-DD HH:MM:SS"
            },
            "categoryField": configTime,
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
}