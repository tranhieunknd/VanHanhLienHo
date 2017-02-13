var ListAlbum = [];
var ListAnh = [];
var HinhAnh = {

    // Load cây album bên trái
    LoadAlbum: function (congtrinh, album) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();
        ListAlbum = [];
        ListAnh = [];
        ListAlbum = HinhAnh.GetListAlbum();
        ListAnh = HinhAnh.GetListAnh();
        //console.log(ListAlbum);
        //console.log(ListAnh);
        var htm = '';

        if (ListAlbum == '') {
            htm += '<ul><li class="has-sub open"><a href="#" style="font-size:18px">Không có công trình</a>';
            htm += '<ul style="display:block;">';
            htm += '<li><a href="#" style="color:red;font-size:18px">Không có album</a></li>';
            htm += '</ul></li></ul>';
            $("#hinhanh-content-left").html(htm);
            $('#hinhanh-content-view').empty();

            $('#hinhanh-btnAdd').show();
            $("#hinhanh-btnAdd").attr('onclick', 'HinhAnh.AddAlbum();');
            $('.hinhanh-btnAdd').html("Thêm album");
        }
        else {
            var cong_trinh = 0;
            var demul = 0;
            var demli = 0;
            htm += '<ul>';
            for (var i = 0; i < ListAlbum.length; i++) {
                if (cong_trinh == ListAlbum[i].structure_code_ref) {
                    htm += '<li id="ab' + ListAlbum[i].album_code + '"><img src="/../Content/Images/folder-horizontal.png" class="imagesfolder" ><a href="#" onclick="HinhAnh.LoadImagesab(' + ListAlbum[i].album_code + ',\'' + ListAlbum[i].album_name + '\',' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].album_name + ' (' + HinhAnh.CountImgOfAlbum(ListAlbum[i].album_code) + ' ảnh)</a></li>';
                    demul = 1;
                    demli = 0;
                }
                else {
                    if (demul == 0) {
                        if (demli == 0) {
                            htm += '<li class="has-sub" id="ct' + ListAlbum[i].structure_code_ref + '"><img src="/../Content/Images/closefolder.png" class="closefolder" ><img src="/../Content/Images/openfolder.png" class="openfolder" ><a href="#" onclick="HinhAnh.LoadImagesct(' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].structure_name + ' (' + HinhAnh.CountImgOfCongTrinh(ListAlbum[i].structure_code_ref) + ' ảnh)</a>';
                            htm += '<ul id="ul' + ListAlbum[i].structure_code_ref + '">';
                            htm += '<li id="ab' + ListAlbum[i].album_code + '"><img src="/../Content/Images/folder-horizontal.png" class="imagesfolder" ><a href="#" onclick="HinhAnh.LoadImagesab(' + ListAlbum[i].album_code + ',\'' + ListAlbum[i].album_name + '\',' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].album_name + ' (' + HinhAnh.CountImgOfAlbum(ListAlbum[i].album_code) + ' ảnh)</a></li>';
                            demul = 1;
                            demli = 1;
                        }
                        else {
                            htm += '</ul></li>';
                            htm += '<li class="has-sub" id="ct' + ListAlbum[i].structure_code_ref + '"><img src="/../Content/Images/closefolder.png" class="closefolder" ><img src="/../Content/Images/openfolder.png" class="openfolder" ><a href="#" onclick="HinhAnh.LoadImagesct(' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].structure_name + ' (' + HinhAnh.CountImgOfCongTrinh(ListAlbum[i].structure_code_ref) + ' ảnh)</a>';
                            htm += '<ul id="ul' + ListAlbum[i].structure_code_ref + '">';
                            htm += '<li id="ab' + ListAlbum[i].album_code + '"><img src="/../Content/Images/folder-horizontal.png" class="imagesfolder" ><a href="#" onclick="HinhAnh.LoadImagesab(' + ListAlbum[i].album_code + ',\'' + ListAlbum[i].album_name + '\',' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].album_name + ' (' + HinhAnh.CountImgOfAlbum(ListAlbum[i].album_code) + ' ảnh)</a></li>';
                            demul = 1;
                            demli = 1;
                        }
                    }
                    else {
                        htm += '</ul></li>';
                        htm += '<li class="has-sub" id="ct' + ListAlbum[i].structure_code_ref + '"><img src="/../Content/Images/closefolder.png" class="closefolder" ><img src="/../Content/Images/openfolder.png" class="openfolder" ><a href="#" onclick="HinhAnh.LoadImagesct(' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].structure_name + ' (' + HinhAnh.CountImgOfCongTrinh(ListAlbum[i].structure_code_ref) + ' ảnh)</a>';
                        htm += '<ul id="ul' + ListAlbum[i].structure_code_ref + '">';
                        htm += '<li id="ab' + ListAlbum[i].album_code + '"><img src="/../Content/Images/folder-horizontal.png" class="imagesfolder" ><a href="#" onclick="HinhAnh.LoadImagesab(' + ListAlbum[i].album_code + ',\'' + ListAlbum[i].album_name + '\',' + ListAlbum[i].structure_code_ref + ',\'' + ListAlbum[i].structure_name + '\')">' + ListAlbum[i].album_name + ' (' + HinhAnh.CountImgOfAlbum(ListAlbum[i].album_code) + ' ảnh)</a></li>';
                        demul = 0;
                        demli = 1;
                    }
                }
                cong_trinh = ListAlbum[i].structure_code_ref;
            }
            htm += '</ul></li></ul>';
            $("#hinhanh-content-left").html(htm);

            if (album != undefined && congtrinh != undefined) {
                HinhAnh.LoadImagesab(album);
                $('#ct' + congtrinh).addClass('open');
                $('#ul' + congtrinh).attr("style", "display:block;");
                $('#ab' + album).addClass('open');
            }
            else {
                if (congtrinh != undefined) {
                    HinhAnh.LoadImagesct(congtrinh);
                    $('#ct' + congtrinh).addClass('open');
                    $('#ul' + congtrinh).attr("style", "display:block;");
                }
                else {
                    HinhAnh.LoadImagesct(ListAlbum[0].structure_code_ref);
                    $('#ct' + ListAlbum[0].structure_code_ref).addClass('open');
                    $('#ul' + ListAlbum[0].structure_code_ref).attr("style", "display:block;");
                }
            }
        }

        $('#hinhanh-content-left li.active').addClass('open').children('ul').show();
        $('#hinhanh-content-left li.has-sub>a').on('click', function () {
            $(this).removeAttr('href');
            var element = $(this).parent('li');
            if (element.hasClass('open')) {
                element.removeClass('open');
                element.find('li').removeClass('open');
                element.find('ul').slideUp();
            }
            else {
                element.addClass('open');
                element.children('ul').slideDown();
                element.siblings('li').children('ul').slideUp();
                element.siblings('li').removeClass('open');
                element.siblings('li').find('li').removeClass('open');
                element.siblings('li').find('ul').slideUp();
            }
        });
        $('#hinhanh-content-left li.has-sub ul li a').on('click', function () {
            $(this).removeAttr('href');
            var element = $(this).parent('li');
            if (element.hasClass('open')) {
                element.removeClass('open');
                element.find('li').removeClass('open');
            }
            else {
                element.addClass('open');
                element.siblings('li').removeClass('open');
                element.siblings('li').find('li').removeClass('open');
            }
        });
        $('#hinhanh-content-left li.has-sub>img').on('click', function () {
            $(this).removeAttr('href');
            var element = $(this).parent('li');
            if (element.hasClass('open')) {
                element.removeClass('open');
                element.find('li').removeClass('open');
                element.find('ul').slideUp();
            }
            else {
                element.addClass('open');
                element.children('ul').slideDown();
                element.siblings('li').children('ul').slideUp();
                element.siblings('li').removeClass('open');
                element.siblings('li').find('li').removeClass('open');
                element.siblings('li').find('ul').slideUp();
            }
        });
    },

    // Load tất cả list album
    GetListAlbum: function () {
        var temp = [];
        var url = '/Images/GetListAlbums';
        $.ajax({
            type: 'POST',
            url: url,
            data: {
            },
            success: function (dt) {
                temp = dt;
            },
            async: false
        });
        return temp;
    },

    // Load tất cả list ảnh
    GetListAnh: function () {
        var temp = [];
        var url = '/Images/GetListImages';
        $.ajax({
            type: 'POST',
            url: url,
            data: {
            },
            success: function (dt) {
                temp = dt;
            },
            async: false
        });
        return temp;
    },

    //Hàm load ảnh theo album
    LoadImagesab: function (album, albumname, congtrinh, congtrinhname) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();

        var ngaylayanh = "";
        var htm = '';
        var htmtong = '';
        var count = 0;
        for (var i = 0; i < ListAnh.length; i++) {
            if (ListAnh[i].album_code == album) {
                ngaylayanh = "";
                if (ListAnh[i].getimages_day != null) {
                    ngaylayanh = HinhAnh.formatJsonDate(ListAnh[i].getimages_day);
                }
                htm += '<div class="viewimagedt"><span class="edit" onclick="HinhAnh.EditAnh(' + ListAnh[i].structure_file_code + ',\'' + ListAnh[i].files_name + '\',' + ListAnh[i].album_code + ',\'' + ListAnh[i].album_name + '\',' + ListAnh[i].structure_code_ref + ',\'' + ListAnh[i].structure_name + '\');">&curren;</span><span class="close" onclick="HinhAnh.DeleteAnh(' + ListAnh[i].structure_file_code + ',\'' + ListAnh[i].files_name + '\',' + ListAnh[i].album_code + ',\'' + ListAnh[i].album_name + '\',' + ListAnh[i].structure_code_ref + ',\'' + ListAnh[i].structure_name + '\');">&times;</span><a href="/../Images_Data/' + ListAnh[i].link + '" title="' + ListAnh[i].epitome + '" target="' + ListAnh[i].files_name + '" data-gallery=""><img src="/../Images_Data/' + ListAnh[i].link + '" onerror="this.src=\'/../Content/Images/imagenotfound.jpg\'" /></a><span class="imagesnames" onclick="HinhAnh.EditAnh(' + ListAnh[i].structure_file_code + ',\'' + ListAnh[i].files_name + '\',' + ListAnh[i].album_code + ',\'' + ListAnh[i].album_name + '\',' + ListAnh[i].structure_code_ref + ',\'' + ListAnh[i].structure_name + '\');">' + ListAnh[i].files_name + '</span><span class="ngaylayanh">' + ngaylayanh + '</span></div>'
                count++;
            }
        }
        if (albumname == undefined || congtrinh == undefined || congtrinhname == undefined) {
            for (var i = 0; i < ListAlbum.length; i++) {
                if (ListAlbum[i].album_code == album) {
                    albumname = ListAlbum[i].album_name;
                    congtrinh = ListAlbum[i].structure_code_ref;
                    congtrinhname = ListAlbum[i].structure_name;
                }
            }
        }
        if (count == 0) {
            htmtong = '<div class="viewimage"><div class="viewimagehd"><ul class="page-breadcrumb"><li><a href="#" onclick="HinhAnh.LoadImagesab(' + album + ',\'' + albumname + '\')">' + albumname + ' (0 ảnh)</a></li></ul></div><div class="viewimagebd"><label style="color:red; font-size:18px; padding:10px;">Album không có hình ảnh</label></div></div>';
        }
        else {
            htmtong += '<div class="viewimage"><div class="viewimagehd"><ul class="page-breadcrumb"><li><a href="#" onclick="HinhAnh.LoadImagesab(' + album + ',\'' + albumname + '\')">' + albumname + ' (' + HinhAnh.CountImgOfAlbum(album) + ' ảnh)</a></li></ul></div><div class="viewimagebd">';
            htmtong += htm;
            htmtong += '</div></div>';
        }

        $('#hinhanh-content-view').show();
        $('#hinhanh-content-view').html(htmtong);

        $("#hinhanh-content-left").show();
        $('#hinhanh-content-right').removeAttr("style");

        $('#hinhanh-btnAdd').show();
        $('.hinhanh-btnAdd').html("Thêm ảnh");
        $("#hinhanh-btnAdd").attr('onclick', 'HinhAnh.AddAnh(' + album + ',\'' + albumname + '\',' + congtrinh + ',\'' + congtrinhname + '\');');
        $('#hinhanh-btnEdit').show();
        $('.hinhanh-btnEdit').html("Sửa album");
        $("#hinhanh-btnEdit").attr('onclick', 'HinhAnh.EditAlbum(' + album + ',\'' + albumname + '\',' + congtrinh + ',\'' + congtrinhname + '\');');
        $('#hinhanh-btnDel').show();
        $('.hinhanh-btnDel').html("Xóa");
        $("#hinhanh-btnDel").attr('onclick', 'HinhAnh.DeleteAlbum(' + album + ',\'' + albumname + '\',' + congtrinh + ',\'' + congtrinhname + '\');');

    },

    //Hàm load ảnh theo công trình
    LoadImagesct: function (congtrinh, congtrinhname) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();

        var htm = '';
        var count = 0;
        for (var i = 0; i < ListAlbum.length; i++) {
            if (ListAlbum[i].structure_code_ref == congtrinh) {
                htm += HinhAnh.LoadImagesDetail(ListAlbum[i].album_code, ListAlbum[i].album_name);
                count++;
            }
        }
        if (count == 0) {
            HinhAnh.LoadAlbum();
        }
        if (congtrinhname == undefined) {
            for (var i = 0; i < ListAlbum.length; i++) {
                if (ListAlbum[i].structure_code_ref == congtrinh) {
                    congtrinhname = ListAlbum[i].structure_name;
                }
            }
        }
        $('#hinhanh-content-view').show();
        $('#hinhanh-content-view').html(htm);

        $("#hinhanh-content-left").show();
        $('#hinhanh-content-right').removeAttr("style");

        $('#hinhanh-btnAdd').show();
        $('.hinhanh-btnAdd').html("Thêm album");
        $("#hinhanh-btnAdd").attr('onclick', 'HinhAnh.AddAlbum(' + congtrinh + ',\'' + congtrinhname + '\');');
    },

    //Hàm load ảnh theo album
    LoadImagesDetail: function (album, albumname) {
        var ngaylayanh = "";
        var congtrinh = '';
        var congtrinhname = '';
        var htm = '';
        var htmtong = '';
        var count = 0;
        for (var i = 0; i < ListAnh.length; i++) {
            if (ListAnh[i].album_code == album) {
                ngaylayanh = "";
                if (ListAnh[i].getimages_day != null) {
                    ngaylayanh = HinhAnh.formatJsonDate(ListAnh[i].getimages_day);
                }
                htm += '<div class="viewimagedt"><span class="edit" onclick="HinhAnh.EditAnh(' + ListAnh[i].structure_file_code + ',\'' + ListAnh[i].files_name + '\',' + ListAnh[i].album_code + ',\'' + ListAnh[i].album_name + '\',' + ListAnh[i].structure_code_ref + ',\'' + ListAnh[i].structure_name + '\');">&curren;</span><span class="close" onclick="HinhAnh.DeleteAnh(' + ListAnh[i].structure_file_code + ',\'' + ListAnh[i].files_name + '\',' + ListAnh[i].album_code + ',\'' + ListAnh[i].album_name + '\',' + ListAnh[i].structure_code_ref + ',\'' + ListAnh[i].structure_name + '\');">&times;</span><a href="/../Images_Data/' + ListAnh[i].link + '" title="' + ListAnh[i].epitome + '" target="' + ListAnh[i].files_name + '" data-gallery=""><img src="/../Images_Data/' + ListAnh[i].link + '" onerror="this.src=\'/../Content/Images/imagenotfound.jpg\'" /></a><span class="imagesnames" onclick="HinhAnh.EditAnh(' + ListAnh[i].structure_file_code + ',\'' + ListAnh[i].files_name + '\',' + ListAnh[i].album_code + ',\'' + ListAnh[i].album_name + '\',' + ListAnh[i].structure_code_ref + ',\'' + ListAnh[i].structure_name + '\');">' + ListAnh[i].files_name + '</span><span class="ngaylayanh">' + ngaylayanh + '</span></div>'
                count++;
            }
        }

        if (count == 0) {
            htmtong = '<div class="viewimage"><div class="viewimagehd"><ul class="page-breadcrumb"><li><a href="#" onclick="HinhAnh.LoadImagesab(' + album + ',\'' + albumname + '\')">' + albumname + ' (0 ảnh)</a></li></ul></div><div class="viewimagebd"><label style="color:red; font-size:18px; padding:10px;">Album không có hình ảnh</label></div></div>';
        }
        else {
            htmtong += '<div class="viewimage"><div class="viewimagehd"><ul class="page-breadcrumb"><li><a href="#" onclick="HinhAnh.LoadImagesab(' + album + ',\'' + albumname + '\')">' + albumname + ' (' + HinhAnh.CountImgOfAlbum(album) + ' ảnh)</a></li></ul></div><div class="viewimagebd">';
            htmtong += htm;
            htmtong += '</div></div>';
        }
        return htmtong;
    },

    //Button thêm mới album
    AddAlbum: function (congtrinh, congtrinhname) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();

        if (congtrinh != undefined) {
            $("#TenHoAddAlbum").data("kendoDropDownList").value(congtrinh);
        }
        $("#hinhanh-content-left").hide();
        $("#hinhanh-content-right").removeClass("harightsmall");
        $("#hinhanh-content-add").show();
        $("#album-title").html('Thêm mới album ảnh');

        if (parseInt($("#bodydegsin").css("width"), 10) < 650) {
            $(".ntk-smalltk").removeClass("ntk-smalltk").addClass("ntk-largetk");
        }
        else {
            $('.ntk-largetk').removeClass("ntk-largetk").addClass("ntk-smalltk");
        }

        $("#hinhanh-btnSave").show();
        $(".hinhanh-btnSave").html("Lưu album");
        $("#hinhanh-btnSave").attr('onclick', 'HinhAnh.SaveNewAlbum();');
        $("#hinhanh-btnCan").show();
        $(".hinhanh-btnCan").html("Hủy");
        $("#hinhanh-btnCan").attr('onclick', 'HinhAnh.CancelAlbum();');
    },

    //Button thêm mới ảnh
    AddAnh: function (album, albumname, congtrinh, congtrinhname) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();

        $("#TenHoAddImage").data("kendoDropDownList").value(congtrinh);
        $("#TenAlbumAddImage").data("kendoComboBox").value(album);
        $("#TenAlbumAddImage").data("kendoComboBox").dataSource.read();

        $("#hinhanh-content-left").hide();
        $("#hinhanh-content-right").removeClass("harightsmall");
        $("#hinhanh-content-image").show();
        $('#image-title').html('Thêm mới hình ảnh');

        if (parseInt($("#bodydegsin").css("width"), 10) < 650) {
            $(".ntk-smalltk").removeClass("ntk-smalltk").addClass("ntk-largetk");
        }
        else {
            $('.ntk-largetk').removeClass("ntk-largetk").addClass("ntk-smalltk");
        }

        $("#hinhanh-btnSave").show();
        $(".hinhanh-btnSave").html("Lưu album");
        $("#hinhanh-btnSave").attr('onclick', 'HinhAnh.SaveAddImage();');
        $("#hinhanh-btnCan").show();
        $(".hinhanh-btnCan").html("Hủy");
        $("#hinhanh-btnCan").attr('onclick', 'HinhAnh.CancelAnh();');
    },

    //Button sửa album
    EditAlbum: function (album, albumname, congtrinh, congtrinhname) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();
        for (var i = 0; i < ListAlbum.length; i++) {
            if (ListAlbum[i].album_code == album) {
                $("#TenHoAddAlbum").data("kendoDropDownList").value(ListAlbum[i].structure_code_ref);
                //$("#TenHoAddAlbum").data("kendoDropDownList").readonly();
                $("#TenAlbumAddAlbum").val(ListAlbum[i].album_name);
                $("#MoTaAddAlbum").val(ListAlbum[i].description);
            }
        }

        $("#hinhanh-content-left").hide();
        $("#hinhanh-content-right").removeClass("harightsmall");
        $("#hinhanh-content-add").show();
        $("#album-title").html('Sửa thông tin album ảnh');

        if (parseInt($("#bodydegsin").css("width"), 10) < 650) {
            $(".ntk-smalltk").removeClass("ntk-smalltk").addClass("ntk-largetk");
        }
        else {
            $('.ntk-largetk').removeClass("ntk-largetk").addClass("ntk-smalltk");
        }

        $("#hinhanh-btnSave").show();
        $(".hinhanh-btnSave").html("Lưu album");
        $("#hinhanh-btnSave").attr('onclick', 'HinhAnh.SaveEditAlbum(' + album + ',' + congtrinh + ');');
        $("#hinhanh-btnCan").show();
        $(".hinhanh-btnCan").html("Hủy");
        $("#hinhanh-btnCan").attr('onclick', 'HinhAnh.CancelAnh();');
    },

    //Button sửa ảnh
    EditAnh: function (image, imagename, album, albumname, congtrinh, congtrinhname) {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();
        for (var i = 0; i < ListAnh.length; i++) {
            if (ListAnh[i].structure_file_code == image) {
                $("#TenHoEditImage").data("kendoDropDownList").value(ListAnh[i].structure_code_ref);
                $("#TenAlbumEditImage").data("kendoComboBox").dataSource.read();
                $("#TenAlbumEditImage").data("kendoComboBox").value(ListAnh[i].album_code);
                $("#TenAnhEditImage").val(ListAnh[i].files_name);
                $("#MoTaEditImage").val(ListAnh[i].epitome);
                $("#NgayLayAnhEditImage").data("kendoDatePicker").value(ListAnh[i].getimages_day);
                $("#view-image-detail").html('<img src="/../Images_Data/' + ListAnh[i].link + '" onerror="this.src=\'/../Content/Images/imagenotfoundBIG.jpg\'" style="width:500px;"/>');
            }
        }

        $("#edit-title").html('Cập nhật thông tin hình ảnh');

        $("#hinhanh-content-left").hide();
        $("#hinhanh-content-right").removeClass("harightsmall");
        $('#hinhanh-content-edit').show();

        if (parseInt($("#bodydegsin").css("width"), 10) < 650) {
            $(".ntk-smalltk").removeClass("ntk-smalltk").addClass("ntk-largetk");
        }
        else {
            $('.ntk-largetk').removeClass("ntk-largetk").addClass("ntk-smalltk");
        }

        $("#hinhanh-btnSave").show();
        $(".hinhanh-btnSave").html("Lưu ảnh");
        $("#hinhanh-btnSave").attr('onclick', 'HinhAnh.SaveEditAnh(' + image + ',\'' + imagename + '\');');
        $("#hinhanh-btnCan").show();
        $(".hinhanh-btnCan").html("Hủy");
        $("#hinhanh-btnCan").attr('onclick', 'HinhAnh.CancelAnh();');
    },

    //Button lưu album mới
    SaveNewAlbum: function () {
        var congtrinh = $("#TenHoAddAlbum").data("kendoDropDownList").value();
        var congtrinhname = $("#TenHoAddAlbum").data("kendoDropDownList").text();
        var albumname = $("#TenAlbumAddAlbum").val();
        var mota = $("#MoTaAddAlbum").val();

        if (albumname == '') {
            alertInfor('warning', 'Thông báo', 'Bạn vui lòng nhập tên album');
        }
        else {
            var url = '/Images/CheckStructures';
            $.ajax({
                type: 'POST',
                url: url,
                data: {
                    structure_code_ref: congtrinh
                },
                success: function (dt) {
                    if (dt == 0) {
                        alertInfor('warning', 'Thông báo', 'Bạn thêm vui lòng lưu thông tin hồ chứa trước khi thêm hình ảnh công trình.');
                    }
                    else {
                        var url = '/Images/AddAlbum';
                        $.ajax({
                            type: 'POST',
                            url: url,
                            data: {
                                album_name: albumname,
                                structure_code_ref: congtrinh,
                                description: mota,
                            },
                            success: function (dt) {
                                if (dt != 0) {
                                    $("#ladyloading1").show();
                                    $("#textloading1").html('Bạn thêm thành công album <b><font color="red">' + albumname + '</b></font>');
                                    setTimeout(function () {
                                        $("#ladyloading1").hide();
                                    }, 3000);
                                    HinhAnh.LoadAlbum(congtrinh, dt);
                                }
                                else {
                                    alertInfor('error', 'Thông báo', 'Đã có lỗi xảy ra, không thể thêm album<b><font color="red"> ' + albumname + '</b></font>');
                                }
                            },
                            async: false
                        });
                    }

                },
                async: false
            });
        }
    },

    //Button lưu album sửa
    SaveEditAlbum: function (album, congtrinh) {
        var congtrinh = $("#TenHoAddAlbum").data("kendoDropDownList").value();
        var congtrinhname = $("#TenHoAddAlbum").data("kendoDropDownList").text();
        var albumname = $("#TenAlbumAddAlbum").val();
        var mota = $("#MoTaAddAlbum").val();

        if (albumname == '') {
            alertInfor('warning', 'Thông báo', 'Bạn vui lòng nhập tên album');
        }
        else {
            var url = '/Images/EditAlbum';
            $.ajax({
                type: 'POST',
                url: url,
                data: {
                    album_code: album,
                    album_name: albumname,
                    description: mota,
                    structure_code_ref: congtrinh
                },
                success: function (dt) {
                    if (dt == 1) {
                        $("#ladyloading1").show();
                        $("#textloading1").html('Bạn sửa thành công album <b><font color="red">' + albumname + '</b></font>');
                        setTimeout(function () {
                            $("#ladyloading1").hide();
                        }, 3000);
                        HinhAnh.LoadAlbum(congtrinh, album);
                    }
                    else {
                        alertInfor('error', 'Thông báo', 'Đã có lỗi xảy ra, không thể sửa album<b><font color="red"> ' + albumname + '</b></font>');
                    }
                },
                async: false
            });
        }
    },

    //Button lưu ảnh
    SaveEditAnh: function (image, imagename) {
        var imagenameold = imagename;
        var congtrinh = $("#TenHoEditImage").data("kendoDropDownList").value();
        var congtrinhname = $("#TenHoEditImage").data("kendoDropDownList").text();
        var album = $("#TenAlbumEditImage").data("kendoComboBox").value();
        var albumname = $("#TenAlbumEditImage").data("kendoComboBox").text();
        var imagename = $("#TenAnhEditImage").val();
        var mota = $("#MoTaEditImage").val();
        var ngaylayanh = $("#NgayLayAnhEditImage").data("kendoDatePicker").value();
        if (albumname == '') {
            alertInfor('warning', 'Thông báo', 'Bạn vui lòng nhập tên album');
        }
        else {
            var arr = new Array();
            var myJSON = '';
            var item = {
                "files_name": imagename, "album_code": album, "structure_code_ref": congtrinh, "structure_name": congtrinhname,
                "epitome": mota, "getimages_day": ngaylayanh, "structure_file_code": image
            };
            arr.push(item);
            myJSON = JSON.stringify(arr);
            var url = '/Images/EditImages';
            $.ajax({
                type: 'POST',
                url: url,
                data: {
                    PostData: myJSON
                },
                success: function (dt) {

                    if (dt == 1) {
                        $("#ladyloading1").show();
                        $("#textloading1").html('Bạn cập nhật thành công thông tin <b><font color="red">' + imagenameold + '</b></font>');
                        setTimeout(function () {
                            $("#ladyloading1").hide();
                        }, 3000);
                        HinhAnh.LoadAlbum(congtrinh, album);
                    }
                    else {
                        alertInfor('error', 'Thông báo', 'Đã có lỗi xảy ra, không thể cập nhật thông tin <b><font color="red">' + imagenameold + '</b></font>');
                    }
                },
                async: false
            });
        }
    },

    //Button lưu album sửa
    SaveAddImage: function () {
        var congtrinh = $("#TenHoAddImage").data("kendoDropDownList").value();
        var congtrinhname = $("#TenHoAddImage").data("kendoDropDownList").text();
        var album = $("#TenAlbumAddImage").data("kendoComboBox").value();
        var albumname = $("#TenAlbumAddImage").data("kendoComboBox").text();
        var imagename = $("#TenAnhAddImage").val();
        var mota = $("#MoTaAddImage").val();
        var ngaylayanh = $("#NgayLayAnhAddImage").data("kendoDatePicker").value();

        if (albumname == '') {
            alertInfor('warning', 'Thông báo', 'Bạn vui lòng nhập tên album');
        }
        else {
            HinhAnh.ResetKendoUpload();
            var arr = new Array();
            var myJSON = '';
            var item = {
                "files_name": imagename, "album_code": album, "album_name": albumname, "structure_code_ref": congtrinh, "structure_name": congtrinhname,
                "description": mota, "getimages_day": ngaylayanh
            };
            arr.push(item);
            myJSON = JSON.stringify(arr);
            var url = '/Images/AddImage';
            $.ajax({
                type: 'POST',
                url: url,
                data: {
                    PostData: myJSON
                },
                success: function (dt) {
                    if (dt == 1) {
                        $("#ladyloading1").show();
                        $("#textloading1").html('Bạn thêm ảnh thành công vào album <b><font color="red">' + albumname + '</b></font>');
                        setTimeout(function () {
                            $("#ladyloading1").hide();
                        }, 3000);
                        HinhAnh.LoadAlbum(congtrinh, album);
                    }
                    else {
                        alertInfor('error', 'Thông báo', 'Đã có lỗi xảy ra, không thể thêm ảnh vào album<b><font color="red"> ' + albumname + '</b></font>');
                    }
                },
                async: false
            });
        }
    },

    //Button xóa album
    DeleteAlbum: function (album, albumname, congtrinh, congtrinhname) {
        $.confirm({
            title: 'Xóa Album',
            icon: 'glyphicon glyphicon-remove-circle',
            content: 'Bạn có muốn xóa album <b><font color="red">' + albumname + '</b></font> không?',
            //autoClose: 'cancel|10000',
            confirm: function () {
                var url = '/Images/DeleteAlbum';
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: {
                        album_code: album,
                        structure_code_ref: congtrinh
                    },
                    success: function (dt) {
                        if (dt == 1) {
                            $("#ladyloading1").show();
                            $("#textloading1").html('Bạn đã xóa thành công album <b><font color="red">' + albumname + '</b></font>');
                            setTimeout(function () {
                                $("#ladyloading1").hide();
                            }, 3000);
                            for (var i = 0; i < ListAlbum.length; i++) {
                                if (ListAlbum[i] == congtrinh) {
                                    HinhAnh.LoadAlbum(congtrinh);
                                    break;
                                }
                            }

                            HinhAnh.LoadAlbum();
                            $("#ul" + congtrinh).attr('style', 'display: block;');
                            $("#ct" + congtrinh).addClass("open");
                        }
                        else {
                            $.confirm({
                                icon: 'glyphicon glyphicon-exclamation-sign',
                                title: 'glyphicon',
                                content: 'Đã có lỗi xảy ra, không thể xóa album <b><font color="red">' + albumname + '</b></font>',
                            });
                        }
                    },
                    async: false
                });
            },
            cancel: function () {
            }
        });
    },

    //Button xóa ảnh
    DeleteAnh: function (image, imagename, album, albumname, congtrinh, congtrinhname) {
        $.confirm({
            title: 'Delete user?',
            content: 'This dialog will automatically trigger \'cancel\' in 6 seconds if you don\'t respond.',
            autoClose: 'cancel|6000',
            confirm: function () {
                alert('confirmed');
            },
            cancel: function () {
                alert('canceled');
            }
        });
    },

    //Button hủy thêm mới album
    CancelAlbum: function () {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();

        $('#hinhanh-btnAdd').show();

        $('#hinhanh-content-view').show();
        $("#hinhanh-content-left").show();
        $('#hinhanh-content-right').removeAttr("style");
    },

    //Button hủy thêm mới ảnh
    CancelAnh: function () {
        HinhAnh.ResetButton();
        HinhAnh.ResetData();

        $('#hinhanh-btnAdd').show();
        $('#hinhanh-btnEdit').show();
        $('#hinhanh-btnDel').show();

        $('#hinhanh-content-view').show();
        $("#hinhanh-content-left").show();
        $('#hinhanh-content-right').removeAttr("style");
    },

    //Tính tổng số ảnh của album
    CountImgOfAlbum: function (album) {
        var count = 0;
        for (var i = 0; i < ListAnh.length; i++) {
            if (ListAnh[i].album_code == album) {
                count++;
            }
        }
        return count;
    },

    //Tính tổng số ảnh của congtrinh
    CountImgOfCongTrinh: function (congtrinh) {
        var count = 0;
        for (var i = 0; i < ListAnh.length; i++) {
            if (ListAnh[i].structure_code_ref == congtrinh) {
                count++;
            }
        }
        return count;
    },

    // Định dạng ngày tháng
    formatJsonDate: function (date) {
        var dateString = date.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        if (month < 10) {
            month = '0' + month
        }
        if (day < 10) {
            day = '0' + day
        }
        return day + " thg " + month + ", " + year;
    },

    // Reset Button
    ResetButton: function () {
        $('#hinhanh-btnAdd').hide();
        $('#hinhanh-btnEdit').hide();
        $("#hinhanh-btnSave").hide();
        $('#hinhanh-btnDel').hide();
        $("#hinhanh-btnCan").hide();
        $('#hinhanh-content-view').hide();
        $("#hinhanh-content-add").hide();
        $('#hinhanh-content-edit').hide();
        $("#hinhanh-content-image").hide();
        $("#hinhanh-content-right").addClass("harightsmall");
    },

    // Reset Data
    ResetData: function () {
    },


    // Kendo echange

    ChangeAddImage: function () {
        $("#TenAlbumAddImage").data("kendoComboBox").value([]);
        $("#TenAlbumAddImage").data("kendoComboBox").dataSource.read();
    },
    AddImageFilter: function () {
        var IdCongTrinh = $("#TenHoAddImage").val();

        if (IdCongTrinh != null && IdCongTrinh != '') {
            return {
                structure_code_ref: IdCongTrinh
            };
        }
        else {
            return {
                structure_code_ref: 0
            };
        }
    },
    ChangeEditImage: function () {
        $("#TenAlbumEditImage").data("kendoComboBox").value([]);
        $("#TenAlbumEditImage").data("kendoComboBox").dataSource.read();
    },
    EditImageFilter: function () {
        var IdCongTrinh = $("#TenHoEditImage").val();

        if (IdCongTrinh != null && IdCongTrinh != '') {
            return {
                structure_code_ref: IdCongTrinh
            };
        }
        else {
            return {
                structure_code_ref: 0
            };
        }
    },

    ResetKendoUpload: function () {
        $(".k-upload-files").remove();
        $(".k-upload-status").remove();
        $(".k-upload.k-header").addClass("k-upload-empty");
        $(".k-upload-button").removeClass("k-state-focused");
    },
}