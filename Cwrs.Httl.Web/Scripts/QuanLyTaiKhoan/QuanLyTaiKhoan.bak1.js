function ChangeInForUser() {
    var user_code = $('#user_code').val();
    var username = $('#username').val();
    var email = $('#email').val();
    var password = $('#password').val();
    var dia_chi = $('#dia_chi').val();
    var dien_thoai = $('#dien_thoai').val();
    var ghi_chu = $('#ghi_chu').val();
    var don_vi_ref = $('#don_vi_ref').val();


    var arr = new Array();
    var myJSON = '';
    var item = {
        "user_code": user_code,
        "username": username,
        "email": email,
        "password": password,
        "dia_chi": dia_chi,
        "dien_thoai": dien_thoai,
        "ghi_chu": ghi_chu,
        "don_vi_ref": don_vi_ref
    };
    arr.push(item);
    myJSON = JSON.stringify(item);
    var url = '/User/CapNhatThongTinUser';
    $.post(url,
        {
            PostData: myJSON
        },
        function (dt) {
            if (dt == -2) {
                toastr.success('Cập nhật thành công', 'Thành công')
            }
            if (dt == -1) {
                toastr.success('Cập nhật thất bại', 'Thành công')
            }
        });
};

function DeleteUser(userId) {
    $.confirm({
        title: 'Xóa',
        icon: 'glyphicon glyphicon-remove-circle',
        content: 'Bạn có muốn xóa bản ghi này không?',
        buttons: {
            Có: function () {
                var url = '/User/DeleteUser';
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { userId: userId },
                    success: function (dt) {
                        alter("2");
                        if (dt == 1) {
                            toastr.success('Xóa bản ghi thành công', 'Thành công')
                        } else {
                            toastr.error('Không xóa được bản ghi', 'Lỗi');
                        }
                    },
                    async: false
                });
            },
            Không: function () {
               
            },
        }
    });
};
