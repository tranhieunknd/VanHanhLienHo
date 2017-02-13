function LoadDonVi() {
    alert("dau tien");
    $.ajax({
        url: '/PhuongAn/SelectAllDonVi',
        type: "GET",
        dataType: "json",
        
        success: function (data)
        {
            if (data != "") {
                var appenddata = [];
                var i = 0;
                $.each(data, function (key, value) {
                    appenddata[i] = value.Name;
                    //$("#TenDonVi").html(appenddata[i]);
                    //i++;
                });
                
                alert("hai");
            }
        },
        error: function () {
            alert("Da co loi xay ra");
        }
    });
}

//function LoadDonVi() {
//    alert("cuoicung");
//    $("#TenDonVi").load("/PhuongAn/SelectAllDonVi");

//}
//function QuanLyLop() {
//    $("#QuanLyLop").load("/QLLop");
//}