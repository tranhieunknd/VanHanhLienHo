var htm = {
    data: {},
    panel: $('body'),
    /**
     * Trả về đối tượng chứa giá trị các ô nhập có htm-data trùng với đầu vào
     * @param {attr cần lấy giá trị} attrvalue 
     * @returns {Trả về đối tượng chứa giá trị các ô nhập có htm-data trùng với đầu vào} 
     */
    GetDataByAttr: function (attrvalue) {
        var data = $("[htm-data=" + attrvalue + "]");
        var result = {};

        function CheckUpdateData(data, element, index, size) {
            if (data == undefined) {
                if (index !== size - 1) {
                    data = {};
                } else {

                    data = element.value;
                }
            }
            return data;
        }

        for (var item in data) {
            if (data.hasOwnProperty(item)) {
                var id = data[item].id;
                // kiem tra ton tai element
                if (id !== undefined) {
                    // cat cac doi tuong con
                    var split = id.split('.');
                    if (split.length > 1) {
                        var tmp = result;
                        var tmpstr = "result.";
                        var i = 0;
                        for (; i < split.length; i++) {
                            // Kiem tra mang
                            var indexBe = split[i].indexOf('[');
                            var indexAf = -1;
                            if (indexBe !== -1) {
                                indexAf = split[i].indexOf(']', indexBe);
                            }
                            // Kiem tra ton tai khoang giua []
                            if (indexBe !== -1 && indexAf !== -1) {
                                var xname = split[i].substring(0, indexBe);
                                if (tmp[xname] == undefined || !Array.isArray(tmp[xname])) {
                                    tmp[xname] = [];
                                }
                                tmp = tmp[xname];
                                // lay ten phan tu con cua mang
                                var xindexname = split[i].substring(indexBe + 1, indexAf);

                                // Kiem tra phan tu con ton tai chua
                                tmp[xindexname] = CheckUpdateData(tmp[xindexname], data[item], i, split.length);
                                //if (tmp[xindexname] == undefined) {
                                //    if (i !== split.length - 1) {
                                //        tmp[xindexname] = {};
                                //    } else {

                                //        tmp[xindexname] = data[item].value;
                                //    }
                                //}
                                tmp = tmp[xindexname];
                            } else {
                                // Neu khong lien quan den mang
                                tmp[split[i]] = CheckUpdateData(tmp[split[i]], data[item], i, split.length);
                                //if (tmp[split[i]] == undefined) {
                                //    if (i !== split.length - 1) {
                                //        tmp[split[i]] = {};
                                //    } else {
                                //        tmp[split[i]] = data[item].value;
                                //    }
                                //}
                                tmp = tmp[split[i]];
                            }

                            //if (i == split.length - 1) {
                            //    tmp = data[item].value;
                            //}
                            //tmp = tmp[split[i]];
                        }
                        // tmp[split[i]] = data[item].value;
                    } else {
                        result[id] = data[item].value;
                    }
                }
            }
            // break;
        }
        return result;
    },
    /**
     * 
     * @param {} method 
     * @param {} url 
     * @param {} obj 
     * @param {} success 
     * @param {} error 
     * @returns {} 
     */
    CallServerPassObj: function (method, url, obj, success, error) {

        var dataSend = JSON.stringify(obj);
        $.ajax({
            url: url,
            type: method,
            dataType: "json",
            data: dataSend,
            contentType: "application/json; charset=utf-8",
            success: success,
            error: error
        });
    },
    /**
     * Chuyen trang bang ajax tu dong chuyen trang
     * @param {} link 
     * @param {} method 
     * @param {} params 
     * @param {} divrelease 
     * @param {} onsuccess 
     * @returns {} 
     */
    SetUrlDynamic: function (link, method, params, divrelease, onsuccess) {
        // console.log(window.location.hostname);
        //console.log(window.location.host);

        var dataSend = JSON.stringify(params);
        htm.RefreshDelay(divrelease);
        $.ajax({
            url: link,
            type: method,
            // dataType: "json",
            data: params,
            // contentType: "application/json; charset=utf-8",
            success: function (dt) {

                var tmp = document.location.href.split('#')[0];
                if (tmp) {

                    var strresult = "";
                    //var splitLink = link.split('#');
                    //if (splitLink.length > 1) {
                    //    if (splitLink[splitLink.length - 1] != "") {
                    if (link != null && link != "") {
                            strresult += method + '#' + JSON.stringify(params);
                            strresult = window.btoa(strresult);
                            strresult = link + '/' + strresult;

                            document.location.href = tmp + '#' + strresult;
                    } else {
                        document.location.href = tmp;
                    }
                    //    }
                    //}

                    if (divrelease !== undefined) {
                        $('#' + divrelease).html(dt);
                    }
                    if (onsuccess !== undefined) {
                        onsuccess();
                    }
                    htm.CloseRefreshDelay();
                }
            },
            error: function (error) {
                console.log("-----------ERROR-------------");
                console.log(error);
                htm.CloseRefreshDelay();
            }
        });
    },
    /**
     * Lan dau load trung dua vao sau dau #
     * @param {} divmain 
     * @returns {} 
     */
    LoadFirst: function (divmain) {
        var arr = document.location.href.split('#');

        var linkCurrent = '';
        if (arr.length == 2 && arr[1] != '') {
            var paramMethod = arr[1];
            paramMethod = paramMethod.split('/');
            paramMethod = paramMethod[paramMethod.length - 1];
            var link = arr[1].substring(0, arr[1].indexOf(paramMethod) - 1); // arr[1].indexOf(paramMethod);

            paramMethod = window.atob(paramMethod);
            paramMethod = paramMethod.split('#');
            var method = paramMethod[0];
            var param = JSON.parse(paramMethod[1]);

            //console.log(window.location.origin);
            //var urlFull = 'http://' + window.location.host + "/" + link;
            var urlFull = window.location.origin + "/" + link;
            console.log(urlFull);
            $.ajax({
                type: method,
                url: urlFull,
                data: param,
                success: function (data, text) {
                    console.log('them sau ajax');
                    $("#" + divmain).html(data);
                },
                error: function (request, status, error) {
                    //console.log(request);
                    //alert(request.responseText);
                }
            });
        } else if (arr.length > 2) {
            linkCurrent = arr[0];
            document.location.href = linkCurrent;
        }
    },
    /**
     * Xét địa chỉ trực tiếp không cần thông qua dấu #
     * @param {} datacache 
     * @param {} title 
     * @param {} url 
     * @param {} divrelease 
     * @returns {} 
     */
    SetUrlDynamicFull: function (datacache, title, url, divrelease) {

        function success(dt) {
            history.pushState(datacache, title, url);
            $('#' + divrelease).html(dt);
        }
        function error(err) {
            console.log(err);
        }

        $.ajax({
            url: ('/' + url),
            type: "GET",
            success: success,
            error: error
        });
    },
    /**
     * Tao hieu ung load trang
     * @returns {} 
     */
    RefreshDelay: function (divname) {
        var $this = $('#' + divname), csspinnerClass = 'csspinner'; // divname = #container
        htm.panel = $this, spinner = $this.data('spinner') || "load1"; //$this.parents('#container').eq(0)
        htm.panel.addClass(csspinnerClass + ' ' + spinner);
    },
    /**
     * Tat hieu ung load trang
     * @returns {} 
     */
    CloseRefreshDelay: function () {
        var csspinnerClass = 'csspinner';
        htm.panel.removeClass(csspinnerClass);
        htm.panel.removeClass("load1");
    }
}