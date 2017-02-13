function FillTongHop() {
    $.ajax(
    {
        url: '/PhuongAn/FillTongHop',
        type: "GET",
        dataType: 'json',
        data: {},
        success: function (data) {
            if (data != null) {
                $('#PhanTich').val(data.TongHop_phanTich);
                $('#TongHop').val(data.TongHop_TongHop);
                $('#KienNghi').val(data.TongHop_KienNghi);
            }
            else {
                $('#PhanTich').val("");
                $('#TongHop').val("");
                $('#KienNghi').val("");
            }
        },
        error: function () {
            alert("Da co loi xay ra");
        }
    });
};
function SaveTongHop() {

    var phanTich = $('#PhanTich').val();
    var TongHop = $('#TongHop').val();
    var KienNghi = $('#KienNghi').val();

    var arr = new Array();
    var myJSON = '';
    var item = {
        "TongHop_phanTich": phanTich,
        "TongHop_TongHop": TongHop,
        "TongHop_KienNghi": KienNghi
    };

    arr.push(item);
    myJSON = JSON.stringify(item);
    var url = '/PhuongAn/SaveTongHop';
    $.post(url,
        {
            PostData: myJSON,

        },
        function (dt) {
            if (dt == -2) {
                toastr.success('Cập nhật thành công', 'Thành công');
                FillTongHop();
            }
            if (dt == -3) {
                toastr.Error('Cập nhật thất bại', 'Thất bại');
            }
        });
};
