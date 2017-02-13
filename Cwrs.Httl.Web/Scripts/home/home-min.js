var home = {
    panel: $('body'),
    DanhDauMenu: function (menuName, level) {
        $("#sidebar").find('li').removeClass("active");
        setTimeout(function () {
            $("#" + menuName).addClass("active");
            var tmp = $("#" + menuName);
            for (var i = 0; i < level; i++) {
                tmp = tmp.parent();
            }
            tmp.addClass("active");
        }, 500);
    },
    ExportExcelTable: function (tableName, excelName) {

        window.open('data:application/vnd.ms-excel,' + $('#' + tableName).html());
        e.preventDefault();

        //$("#" + tableName).table2excel({
        //    exclude: ".noExl",
        //    name: "Excel Document Name",
        //    filename: excelName,
        //    fileext: ".xls",
        //    exclude_img: true,
        //    exclude_links: true,
        //    exclude_inputs: true
        //});
    },
    RefreshDelay: function () {
        var $this = $('#container'), csspinnerClass = 'csspinner';
        home.panel = $this, spinner = $this.data('spinner') || "load1"; //$this.parents('#container').eq(0)
        home.panel.addClass(csspinnerClass + ' ' + spinner);

        //window.setTimeout(function () {
        //    panel.removeClass(csspinnerClass);
        //}, 1000);
        // e.preventDefault();
    },
    CloseRefreshDelay: function () {
        var csspinnerClass = 'csspinner';
        home.panel.removeClass(csspinnerClass);
        home.panel.removeClass("load1");
    }
    //DanhDauChildrenMenu: function (menuName) {
    //    $("#sidebar").find('li').removeClass("active");
    //    setTimeout(function () {
    //        $("#" + menuName).addClass("active");
    //        $("#" + menuName).parent().addClass("active");
    //    },500);
    //}
}