var EOFn = {
    LoginSystem: function () {
        var UserName = $("#usName").val();
        var PassWord = $("#paWord").val();

        if (UserName == null || UserName == "") {
            alert('Qúy khách vui lòng nhập tên đăng nhập', 'usName');
            return false;
        }
        if (PassWord == null || PassWord == "") {
            alert('Qúy khách vui lòng nhập mật khẩu', 'paWord');
            return false;
        }

        var url = '/DangNhap/CheckLogin';
        $.post(url,
          {
              UserName: UserName,
              PassWord: PassWord
          },
          function (dt) {
              if (dt == "0") {
                 // alertInfor('warning', 'usName', 'Qúy khách vui lòng kiểm tra lại thông tin tài khoản');
                  //$("#textloading").html('Qúy khách vui lòng kiểm tra lại thông tin tài khoản');
                  alert('Qúy khách vui lòng kiểm tra lại thông tin tài khoản', 'usName');
                  return false;
              }
              else {
                  window.location.href = "";
              }
          });

    },
    LoginSuccess: function (dt) {
        if (dt == "0") {
            // alertInfor('warning', 'usName', 'Qúy khách vui lòng kiểm tra lại thông tin tài khoản');
            //$("#textloading").html('Qúy khách vui lòng kiểm tra lại thông tin tài khoản');
            alert('Qúy khách vui lòng kiểm tra lại thông tin tài khoản', 'usName');
            return false;
        }
        else {
            window.location.href = "";
        }
    }
}