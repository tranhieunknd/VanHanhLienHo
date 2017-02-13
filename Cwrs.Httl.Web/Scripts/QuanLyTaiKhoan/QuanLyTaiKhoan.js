function ThongBaoThanhCong(result) {
    if (result == 0) {
        toastr.success('Thêm tài khoản thành công', 'Thành công');
    }
    else {
        toastr.error('Thêm tài khoản thất bại', 'Thất bại');
    }
};
function ThongBaoSua(result) {
    if (result == 0) {
        toastr.success('Sửa tài khoản thành công', 'Thành công');
    }
    else {
        toastr.error('Sửa tài khoản thất bại', 'Thất bại');
    }
};
function ThongBaoThatBai() {
    toastr.error('Đã xảy ra lỗi', 'Thất bại');
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
                        if (dt == 1) {
                            
                            toastr.success('Xóa bản ghi thành công', 'Thành công');
                            $("#container").load("User/SelectAllUser");
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
