var QLDuLieuPhiCongTrinh =
{
    AddinfoKTXH: function () {
        var obj = new Object();
        var laymahuyen = $("#DistrictKTXH").val();
        if (laymahuyen == null) laymahuyen = "0";
        obj.laymahuyen = laymahuyen;

        var laymaxa = $("#CommuneKTXH").val();
        if (laymaxa == null) laymaxa = "0";
        obj.laymaxa = laymaxa;

        var laydvql = $("#DonViQuanLyKTXH").val();
        if (laydvql == null) laydvql = "0";
        obj.laydvql = laydvql;

        var laytg = $('#ThoiGianKTXH').val();
        obj.laytg = laytg;

        return JSON.stringify(obj);
    },

    AddinfoTPNN: function () {
        var obj = new Object();
        var laymahuyen = $("#District_TPNN").val();
        if (laymahuyen == null) laymahuyen = "0";
        obj.laymahuyen = laymahuyen;

        var laymaxa = $("#Commune_TPNN").val();
        if (laymaxa == null) laymaxa = "0";
        obj.laymaxa = laymaxa;

        var laydvql = $("#DonViQuanLy_TPNN").val();
        if (laydvql == null) laydvql = "0";
        obj.laydvql = laydvql;

        var laytg = $('#ThoiGian_TPNN').val();
        obj.laytg = laytg;

        return JSON.stringify(obj);
    },

    AddinfoRDS: function () {
        var obj = new Object();
        var laymahuyen = $("#District_RDS").val();
        if (laymahuyen == null) laymahuyen = "0";
        obj.laymahuyen = laymahuyen;

        var laymaxa = $("#Commune_RDS").val();
        if (laymaxa == null) laymaxa = "0";
        obj.laymaxa = laymaxa;

        var laydvql = $("#DonViQuanLy_RDS").val();
        if (laydvql == null) laydvql = "0";
        obj.laydvql = laydvql;

        var laytg = $('#ThoiGian_RDS').val();
        obj.laytg = laytg;

        return JSON.stringify(obj);
    }

}