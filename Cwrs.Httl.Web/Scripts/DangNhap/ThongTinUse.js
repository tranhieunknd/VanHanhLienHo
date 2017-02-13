function ThongTinUser() {
    $.ajax(
    {
        url: '/DangNhap/ThongTinUse',
        type: "GET",
        dataType: 'json',
        data: {},
        success: function(data) {
            $('#user_code').val(data.user_code);
            $('#username').val(data.username);
            $('#ghi_chu').val(data.ghi_chu);
            $('#email').val(data.email);
            $('#dia_chi').val(data.dia_chi);
            $('#dien_thoai').val(data.dien_thoai);
            $('#ghi_chu').val(data.ghi_chu);
            console.log("vao roi nhes");
        },
        error: function() {
            alert("Da co loi xay ra");
        }
    });
};

function ChangeInFor() {
    var user_code = $('#user_code').val();
    var username = $('#username').val();
    var email = $('#email').val();
    var password = $('#password').val();
    var passwordNew = $('#passwordNew').val();
    var confirmPassword = $('#confirmPassword').val();
    var dia_chi = $('#dia_chi').val();
    var dien_thoai = $('#dien_thoai').val();
    var ghi_chu = $('#ghi_chu').val();


    var arr = new Array();
    var myJSON = '';
    var item = {
        "user_code": user_code,
        "username": username,
        "email": email,
        "password": password,
        "passwordNew": passwordNew,
        "confirmPassword": confirmPassword,
        "dia_chi": dia_chi,
        "dien_thoai": dien_thoai,
        "ghi_chu": ghi_chu,
    };
    arr.push(item);
    myJSON = JSON.stringify(item);
    var url = '/DangNhap/ChangeInForUser';
    $.post(url,
        {
            PostData: myJSON,
            passwordNew: passwordNew,
            confirmPassword: confirmPassword
        },
        function(dt) {
            if (dt == -2) {
                alert("thanh cong");
                $('#password').val("");
                $('#passwordNew').val("");
                $('#confirmPassword').val("");
            }
            if (dt == -3) {
                alert("that bai");
                $('#password').val("");
                $('#passwordNew').val("");
                $('#confirmPassword').val("");
            }
            if (dt == -1) {
                alert("mật khẩu cũ không đúng");
                $('#password').val("");
                $('#passwordNew').val("");
                $('#confirmPassword').val("");
            }
        });
};

