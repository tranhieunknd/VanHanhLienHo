function XemKichBanThietLap() {

}

var mucNuocDangBinhThuong = [0, 215, 117, 120];
var TinhToanXa = {
    XemLichSu: false,
    ResetSTT: false,
    TaoDienBienMucNuoc: function () {

        var viTri = $('#viTriCode').val();
        var mucNuoc = $('#mucNuoc').val();
        var luuLuong = $('#luuLuong').val();

        function afterTaoKichBan(result) {
            var tmpViTri = $('#viTriCode').val();

            for (var i = 0; i < result.length; i++) {
                var x = moment(result[i].thoi_gian);
                result[i].tmp_thoi_gian = result[i].thoi_gian;
                result[i].thoi_gian = x.format('YYYY-MM-DD HH:mm:ss');
            }

            var dataSource = new kendo.data.DataSource({
                data: result
            });
            var grid = $("#GridXa").data("kendoGrid");
            KichBanXa_Stt = 0;
            grid.setDataSource(dataSource);

            //var dataChart = result;
            //var config = {
            //    time: "dateChart1",
            //    legend: "legendMucNuoc",
            //    data: [
            //        {
            //            Code: 1,
            //            Name: "Mực nước hồ (m)",
            //            KeyValue: "muc_nuoc_ho"
            //        }, {
            //            Code: 2,
            //            Name: "Mực nước dâng bình thường (m)",
            //            KeyValue: "nuoc_dang_binh_thuong"
            //        }
            //    ]
            //};
            //BieuDo.RenderChart("chartMucNuoc", dataChart, config);
            //dataChart = result;
            //config = {
            //    time: "dateChart2",
            //    legend: "legendDungTich",
            //    data: [
            //        {
            //            Code: 1,
            //            Name: "Dung tích hồ",
            //            KeyValue: "dung_tich_ho"
            //        }
            //    ]
            //}
            //BieuDo.RenderChart("chartDungTich", dataChart, config);

            TinhToanXa.SetDataChart(result, tmpViTri);
        }

        var dataFull = TinhToanXa.GetParamFull();

        var dataJson = {
            vitricode: viTri,
            time: dataFull.DateSelect,
            mucnuoc: mucNuoc,
            luuluong: luuLuong,
            jsondata: dataFull.jsonData//JSON.stringify(dataGrid)
        }
        console.log(dataJson);
        $.ajax({
            type: "Post",
            data: dataJson,
            url: "ThietLapXa/TaoKichBanXa",
            success: afterTaoKichBan
        });

    },
    GetParamDataTable: function () {
        var grid = $("#GridXa").data("kendoGrid");
        var dataGrid = grid.dataSource.view();
        var displayedDataAsJson = JSON.stringify(dataGrid);

        var datetimepicker = $("#DateSelect").data("kendoDateTimePicker");
        var date = $('#DateSelect').val();
        date = moment(datetimepicker.value());
        console.log();
        var numberRow = $('#NumRow').val();

        var dataJson = {
            rowNumber: numberRow,
            dateSelect: date.format('YYYY-MM-DD HH:mm:ss'),
            jsonData: displayedDataAsJson
        }
        console.log(dataJson);
        return dataJson;
    },
    GetParamFull: function () {
        var viTriCode = $('#viTriCode').val();
        var kichBanXa = $('#KichBanXa').val();
        var mucNuoc = $('#mucNuoc').val();
        var luuLuong = $('#luuLuong').val();

        var grid = $("#GridXa").data("kendoGrid");
        var dataGrid = grid.dataSource.view();
        //for (var i = 0; i < dataGrid.length; i++) {
        //    var x = moment(dataGrid[i].thoi_gian);
        //    dataGrid[i].thoi_gian = x.format('YYYY-MM-DD HH:mm:ss');
        //}
        var tmpDataGrig = TinhToanXa.FormatTableServer(dataGrid);
        var displayedDataAsJson = JSON.stringify(tmpDataGrig);

        var datetimepicker = $("#DateSelect").data("kendoDateTimePicker");
        var date = datetimepicker.value();
        var numberRow = $('#NumRow').val();

        // console.log();

        var dataJson = {
            mucNuoc: mucNuoc,
            luuLuong: luuLuong,
            KichBanXa: kichBanXa,
            viTriCode: viTriCode,
            NumRow: numberRow,
            DateSelect: moment(date).format('YYYY-MM-DD HH:mm:ss'),
            jsonData: displayedDataAsJson,
            GridXa: dataGrid
        }
        return dataJson;
    },
    ChangeGrid: function () {
        TinhToanXa.ResetSTT = true;
        $('#GridXa').data('kendoGrid').dataSource.read();
        $('#GridXa').data('kendoGrid').refresh();
    },
    ChangeRowGrid: function () {

        var gridKendo = $("#GridXa").data("kendoGrid");
        var dataGridKendo = gridKendo.dataSource;

        var grid = $("#GridXa").data("kendoGrid");
        var dataGrid = grid.dataSource.data();// $("#GridXa").data().kendoGrid.dataSource.view();
        var displayedDataAsJson = JSON.stringify(dataGrid);

        var date = $('#DateSelect').val();
        var numberRow = $('#NumRow').val();

        var dataJson = {
            rowNumber: numberRow,
            dateSelect: date,
            jsonData: JSON.stringify(dataGridKendo.view())
        }

        // Bắt đầu code thu nghiêm
        //var countGrid = dataGrid.length;

        //if (countGrid < numberRow) {
        //    var tmpItem = dataGrid[dataGrid.length - 1];

        //    var time = moment(tmpItem.thoi_gian);
        //    tmpx = numberRow - countGrid;
        //    for (var i = 0; i < tmpx; i++) {
        //        grid.dataSource.add();
        //        console.log('xong 1 vong');
        //    }
        //    dataGrid = grid.dataSource.data();

        //    for (var k = 0; k < tmpx; k++) {
        //        var x = time.add(1, 'h');
        //        console.log(x);
        //        console.log(k);
        //        dataGrid[k].thoi_gian = x;
        //    }

        //} else {
        //    var count = countGrid - numberRow;
        //    for (var j = 0; j < count; j++) {
        //        dataGrid.pop();
        //    }
        //}
        //var dataSource = new kendo.data.DataSource({
        //    data: dataGrid
        //});

        //var grid = $("#GridXa").data("kendoGrid");
        //KichBanXa_Stt = 0
        //grid.setDataSource(dataSource);
        // Kết thúc code thu nghiêm

        function afterChangeRowNumber(result) {
            console.log(JSON.stringify(result));
            //var dataSource = new kendo.data.DataSource(result);
            var dataSource = new kendo.data.DataSource({
                data: result.Data,
                total: result.ToTal
            });


            var grid = $("#GridXa").data("kendoGrid");
            KichBanXa_Stt = 0;
            grid.setDataSource(dataSource);
        }

        $.ajax({
            type: "Post",
            data: dataJson,
            url: "ThietLapXa/ChangeRowGrid",
            success: afterChangeRowNumber
        });

    },
    ChangeMucNuoc: function () {
        var mucNuoc = $('#mucNuoc').val();
        var viTri = $('#viTriCode').val();

        function after(result) {
            $('#luuLuong').val(result);
        }

        TinhToanXa.LayGiaTriHienTai(viTri, 1, mucNuoc, after);
    },
    ChangeLuuLuong: function () {
        var luuLuong = $('#luuLuong').val();
        var viTri = $('#viTriCode').val();
        function after(result) {
            $('#mucNuoc').val(result);
        }
        TinhToanXa.LayGiaTriHienTai(viTri, 2, luuLuong, after);
    },
    ChangeXemLichSu: function () {
        console.log(TinhToanXa.XemLichSu);
        TinhToanXa.XemLichSu = !TinhToanXa.XemLichSu;
        console.log(TinhToanXa.XemLichSu);

        if ($("#XemKichBanThietLap").css("display") == 'none') {
            $("#XemKichBan-icon").removeClass("glyphicon-chevron-down");
            $("#XemKichBan-icon").addClass("glyphicon-chevron-up");
            $("#XemKichBanThietLap").show();
        }
        else {
            $("#XemKichBan-icon").addClass("glyphicon-chevron-down");
            $("#XemKichBan-icon").removeClass("glyphicon-chevron-up");
            $("#XemKichBanThietLap").hide();
        }
    },
    ChangeKichBanXa: function () {
        var data = TinhToanXa.GetParamFull();
        if (data.viTriCode != -1) {
            function afterCallServe(result) {

                /**
                 * Gan gia tri cho cac phan tu
                 */
                $('#mucNuoc').val(result.KichBan.muc_nuoc_ho);
                $('#luuLuong').val(result.KichBan.luu_luong_ve_ho);
                $("#DateSelect").data("kendoDateTimePicker").value(result.KichBan.thoi_gian);
                $('#viTriCode').val(result.KichBan.vi_tri_ref);

                /*Do du lieu len bang*/
                for (var i = 0; i < result.KichBanChiTiet.length; i++) {
                    var x = moment(result.KichBanChiTiet[i].thoi_gian);
                    result.KichBanChiTiet[i].tmp_thoi_gian = result.KichBanChiTiet[i].thoi_gian;
                    result.KichBanChiTiet[i].thoi_gian = x.format('YYYY-MM-DD HH:mm:ss');
                }

                TinhToanXa.SetDataChart(result.KichBanChiTiet, result.KichBan.vi_tri_ref);

                var dataSource = new kendo.data.DataSource({
                    data: result.KichBanChiTiet
                });

                var grid = $("#GridXa").data("kendoGrid"); //.value();
                KichBanXa_Stt = 0;
                grid.setDataSource(dataSource);

                var numerictextbox = $("#NumRow").data("kendoNumericTextBox");
                numerictextbox.value(result.KichBanChiTiet.length);
                //var dataChart1 = result.KichBanChiTiet;
                //var config = {
                //    time: "dateChart1",
                //    legend: "legendDungTich",
                //    data: [
                //        {
                //            Code: 1,
                //            Name: "Dung tích hồ",
                //            KeyValue: "dung_tich_ho"
                //        }
                //    ]
                //}
                //BieuDo.RenderChart("chartDungTich", dataChart1, config);
                //config = {
                //    time: "dateChart2",
                //    legend: "legendMucNuoc",
                //    data: [
                //        {
                //            Code: 1,
                //            Name: "Mực nước hồ (m)",
                //            KeyValue: "muc_nuoc_ho"
                //        }, {
                //            Code: 2,
                //            Name: "Mực nước dâng bình thường (m)",
                //            KeyValue: "nuoc_dang_binh_thuong"
                //        }
                //    ]
                //}
                //dataChart2 = result.KichBanChiTiet;
                //BieuDo.RenderChart("chartMucNuoc", dataChart2, config);

            }
            $.ajax({
                type: "Post",
                data: { kichbancode: data.KichBanXa },
                url: "ThietLapXa/GetInfoKichBan",
                success: afterCallServe
            });
        }
    },
    ChangeViTri: function () {
        var data = TinhToanXa.GetParamFull();
        $.ajax({
            type: "Post",
            data: { vitri: data.viTriCode },
            url: "ThietLapXa/GetInfoReadlTimeLake",
            success: function (result) {
                function RoundNumber(data, number) {
                    var x = data * Math.pow(10, number);
                    console.log(x);
                    Math.round(x);
                    console.log(x);
                    x = x / Math.pow(10, number);
                    return x;
                }

                var mucNuoc = $('#mucNuoc').val(result.MucNuoc);
                var luuLuong = $('#luuLuong').val(result.LuuLuong);
                // $('#DateSelect').val(moment().format('DD/MM/YYYY HH:mm'));
            }
        });
    },
    LuuKichBan: function () {
        var data = TinhToanXa.GetParamFull();
        console.log(data);
        var dataJson = {
            kichbancode: TinhToanXa.XemLichSu ? data.KichBanXa : -1,
            vitricode: data.viTriCode,
            mucnuoc: data.mucNuoc,
            luuluong: data.luuLuong,
            time: data.DateSelect,
            jsondata: data.jsonData
        }
        console.log(dataJson);
        $.ajax({
            type: "Post",
            data: dataJson,
            url: "ThietLapXa/LuuKichBanXa",
            success: function (result) {
                if (result != "false") {
                    toastr.success('Cập nhật dữ liệu thành công.',
                        'Thông báo',
                        { timeOut: 2000, closeHtml: '<button><i class="icon-off"></i></button>' });
                    var strHTML = "";
                    for (var i = 0; i < result.listKichBan.length; i++) {
                        if (result.listKichBan[i].thiet_lap_kich_ban_code == result.codeKichBan) {
                            strHTML += '<option selected value="' +
                                result.listKichBan[i].thiet_lap_kich_ban_code +
                                '">' +
                                result.listKichBan[i].thiet_lap_kich_ban_name +
                                '</option>';
                        } else {
                            strHTML += '<option value="' +
                                result.listKichBan[i].thiet_lap_kich_ban_code +
                                '">' +
                                result.listKichBan[i].thiet_lap_kich_ban_name +
                                '</option>';
                        }
                    }
                    $('#KichBanXa').html(strHTML);

                } else {
                    toastr.error('Cập nhật dữ liệu thất bại.',
                        'Thông báo',
                        { timeOut: 2000, closeHtml: '<button><i class="icon-off"></i></button>' });
                }
            }
        });
    },
    LamMoi: function () {

    },
    LayGiaTriHienTai: function (vitricode, datatype, data, fun) {
        var dataJson = {
            vitricode: viTriCode,
            datatype: datatype,
            data: data
        }

        $.ajax({
            type: "Post",
            data: dataJson,
            url: "ThietLapXa/TaoKichBanXa",
            success: fun
        });
    },
    FormatTableServer: function (data) {
        var arr = [];
        for (var i = 0; i < data.length; i++) {
            var date = moment(data[i].thoi_gian);
            var tmp = {
                muc_nuoc_ho: data[i].muc_nuoc_ho,
                dung_tich_ho: data[i].dung_tich_ho,
                luu_luong_du_bao: data[i].luu_luong_du_bao,
                luu_luong_phat_dien: data[i].luu_luong_phat_dien,
                luu_luong_xa: data[i].luu_luong_xa,
                tong_tran: data[i].luu_luong_xa,
                thoi_gian: date.format('YYYY-MM-DD HH:mm:ss')
            }
            arr.push(tmp);
        }
        console.log(arr);
        return arr;
    },
    SetDataChart: function (result, tmpViTri) {

        for (var i = 0; i < result.length; i++) {

            result[i].dateChart1 = result[i].tmp_thoi_gian;
            result[i].dateChart2 = result[i].tmp_thoi_gian;

            result[i].nuoc_dang_binh_thuong = mucNuocDangBinhThuong[tmpViTri];
            result[i].tong_luu_luong_xa = result[i].luu_luong_phat_dien + result[i].luu_luong_xa + result[i].tong_tran;
        }

        var dataChart = result;
        var config = {
            time: "dateChart1",
            legend: "legendMucNuoc",
            data: [
                {
                    Code: 1,
                    Name: "Mực nước hồ (m)",
                    KeyValue: "muc_nuoc_ho"
                }, {
                    Code: 2,
                    Name: "Mực nước dâng bình thường (m)",
                    KeyValue: "nuoc_dang_binh_thuong"
                }
            ]
        };
        BieuDo.RenderChart("chartMucNuoc", dataChart, config);

        dataChart = result;
        config = {
            time: "dateChart2",
            legend: "legendDungTich",
            data: [
                {
                    Code: 1,
                    Name: "Lưu lượng đến (m3/s)",
                    KeyValue: "luu_luong_du_bao"
                }, {
                    Code: 2,
                    Name: "Lưu lượng xả (m3/s)",
                    KeyValue: "tong_luu_luong_xa"
                }
            ]
        }
        BieuDo.RenderChart("chartDungTich", dataChart, config);
    }
}