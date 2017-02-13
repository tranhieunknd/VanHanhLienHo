var mainsys = {
    LogOut: function () {
        var url = "/DangNhap/LogOut";
        $.post(url,
          {
          },
          function (dt) {
              window.location.href = dt;
          });
    }
}