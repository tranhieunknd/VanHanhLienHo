var QLProjectFn = {
    LuuMaLoaiCongTrinh: function () {

        _ma_loai_cong_trinh = $('#cbxChonLoaiCongTrinh').val();
        _ten_loai_cong_trinh = $("#cbxChonLoaiCongTrinh").data("kendoDropDownList").text();

    },
    AddTabThongTinChung: function () {
        switch (_ma_loai_cong_trinh) {
            case '1':
                {

                    break;
                }
            case '2':
                {

                    break;
                }
            case '3':
                {
                    $('.thongtinchung_kenh').attr('style', 'display:block;');
                    $('.thongtinkythuat_kenh').attr('style', 'display:none;');
                    break;
                }
            case '4':
                {
                    $('.thongtinchung_trambom').attr('style', 'display:block;');
                    $('.thongtinkythuat_trambom').attr('style', 'display:none;');
                    break;
                }
            case '5':
                {
                    $('.thongtinchung_cong').attr('style', 'display:block;');
                    $('.thongtinkythuat_cong').attr('style', 'display:none;');
                    break;
                }
            case '6':
                {
                    $('.thongtinchung_xiphong').attr('style', 'display:block;');
                    $('.thongtinkythuat_xiphong').attr('style', 'display:none;');
                    break;
                }
            case '7':
                {
                    $('.thongtinchung_cau').attr('style', 'display:block;');
                    $('.thongtinkythuat_cau').attr('style', 'display:none;');
                    break;
                }
            case '8':
                {
                    $('.thongtinchung_caumang').attr('style', 'display:block;');
                    $('.thongtinkythuat_caumang').attr('style', 'display:none;')
                    break;
                }
            default: {
            }
        }
    },
    AddTabThongTinKyThuat: function () {
        switch (_ma_loai_cong_trinh) {
            case '1':
                {

                    break;
                }
            case '2':
                {

                    break;
                }
            case '3':
                {
                    $('.thongtinkythuat_kenh').attr('style', 'display:block;');
                    $('.thongtinchung_kenh').attr('style', 'display:none;');
                    break;
                }
            case '4':
                {
                    $('.thongtinkythuat_trambom').attr('style', 'display:block;');
                    $('.thongtinchung_trambom').attr('style', 'display:none;');
                    break;
                }
            case '5':
                {
                    $('.thongtinkythuat_cong').attr('style', 'display:block;');
                    $('.thongtinchung_cong').attr('style', 'display:none;');
                    break;
                }
            case '6':
                {
                    $('.thongtinkythuat_xiphong').attr('style', 'display:block;');
                    $('.thongtinchung_xiphong').attr('style', 'display:none;');
                    break;
                }
            case '7':
                {
                    $('.thongtinkythuat_cau').attr('style', 'display:block;');
                    $('.thongtinchung_cau').attr('style', 'display:none;');
                    break;
                }
            case '8':
                {
                    $('.thongtinkythuat_caumang').attr('style', 'display:block;');
                    $('.thongtinchung_caumang').attr('style', 'display:none;');
                    break;
                }
            default: {
            }
        }

    },
}