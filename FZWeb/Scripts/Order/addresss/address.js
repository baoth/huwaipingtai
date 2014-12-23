var citytemp;
var areatemp;
var towntemp;
function provinceData(idprovince) {
    if (typeof (idprovince) == "undefined") {
        idprovince = 1
    }
    jQuery.get("http://p.m.jd.com/order/area.action", { idProvince: 0 }, function (data) {
        var data = eval("(" + data + ")");
        if (data != null) {
            $("#address_province").empty();
            for (var i = 0; i < data.length; i++) {
                if (data[i].id == idprovince) {
                    $("#province_label").text(data[i].name)
                }
                $("#address_province").append("<option " + (idprovince == data[i].id ? "selected" : "") + " id='option_add_" + data[i].id + "' value=" + data[i].id + ">" + data[i].name + "</option>")
            }
        }
        $("#address_province").change()
    })
}
var citychange = function cityData() {
    $("#province_label").text($("#address_province").find("option:selected").text());
    var idprovince = $("#address_province").val();
    var idcity = citytemp;
    jQuery.get("http://p.m.jd.com/order/area.action", { idProvince: idprovince }, function (data) {
        var data = eval("(" + data + ")");
        if (data != null) {
            $("#address_city").empty();
            for (var i = 0; i < data.length; i++) {
                if (data[i].id == idcity) {
                    $("#city_label").text(data[i].name)
                }
                $("#address_city").append("<option " + (idcity == data[i].id ? "selected" : "") + " id='option_add_" + data[i].id + "' value=" + data[i].id + ">" + data[i].name + "</option>")
            }
        }
        $("#address_city").change()
    })
};
var areachange = function areaData() {
    $("#city_label").text($("#address_city").find("option:selected").text());
    var idcity = $("#address_city").val();
    var idarea = areatemp;
    if (idcity == null) {
        $("#address_area").empty();
        $("#address_area").change();
        return
    }
    jQuery.get("http://p.m.jd.com/order/area.action", { idCity: idcity }, function (data) {
        var data = eval("(" + data + ")");
        if (data != null) {
            $("#address_area").empty();
            for (var i = 0; i < data.length; i++) {
                if (data[i].id == idarea) {
                    $("#area_label").text(data[i].name)
                }
                $("#address_area").append("<option " + (idarea == data[i].id ? "selected" : "") + " id='option_add_" + data[i].id + "' value=" + data[i].id + ">" + data[i].name + "</option>")
            }
        }
        $("#address_area").change()
    })
};
var townchange = function townData() {
    $("#area_label").text($("#address_area").find("option:selected").text());
    var idarea = $("#address_area").val();
    var idtown = towntemp;
    if (idarea == null) {
        $("#address_town").empty();
        $("#address_town").change();
        return
    }
    jQuery.get("http://p.m.jd.com/order/area.action", { idArea: idarea }, function (data) {
        var data = eval("(" + data + ")");
        if (!!data && data.length > 0) {
            $("#townaddress").show();
            $("#address_town").empty();
            for (var i = 0; i < data.length; i++) {
                if (data[i].id == idtown) {
                    $("#town_label").text(data[i].name)
                }
                $("#address_town").append("<option " + (idtown == data[i].id ? "selected" : "") + " id='option_add_" + data[i].id + "' value=" + data[i].id + ">" + data[i].name + "</option>")
            }
        } else {
            $("#address_town").empty();
            $("#townaddress").hide()
        }
        $("#address_town").change()
    })
};
var labelchange = function setLabel() {
    $("#town_label").text($("#address_town").find("option:selected").text());
    var e = $("#address_province").find("option:selected").text();
    var c = $("#address_city").find("option:selected").text();
    var d = $("#address_area").find("option:selected").text();
    var b = $("#address_town").find("option:selected").text();
    var a = $("#address_where").val().replace(e, "");
    a = a.replace(c, "");
    a = a.replace(d, "");
    a = a.replace(b, "");
    $("#address_where").val(a)
};
function getData(c, a, f, b, i, d, g, j, h, e) {
    $("#address_name").val(a);
    $("#address_where").val(f);
    $("#address_mobile").val(b);
    $("#address_email").val(i);
    $("#address_zip").val(d);
    citytemp = j;
    areatemp = h;
    towntemp = e;
    provinceData(g)
}
var validateName = function () {
    var a = $("#address_name").val();
    if (a == null || a.trim() == "") {
        $("#nameErrorMsg").show();
        return false
    } else {
        $("#nameErrorMsg").hide();
        return true
    }
};
var validateWhere = function () {
    var a = $("#address_where").val();
    if (a == null || a.trim() == "") {
        $("#addressErrorMsg").show();
        return false
    } else {
        $("#addressErrorMsg").hide();
        return true
    }
};
var validateAddressMobile = function () {
    var a = $("#address_mobile").val();
    if (a == null || a.trim() == "") {
        $("#mobileErrorMsg").text("\u7535\u8bdd\u53f7\u7801\u4e0d\u80fd\u4e3a\u7a7a");
        $("#mobileErrorMsgDiv").show();
        return false
    } else {
        if (!/^1(\d){10}$/.exec(a)) {
            $("#mobileErrorMsg").text("\u7535\u8bdd\u53f7\u7801\u683c\u5f0f\u9519\u8bef");
            $("#mobileErrorMsgDiv").show();
            return false
        } else {
            $("#mobileErrorMsgDiv").hide();
            return true
        }
    }
};
var validateSubmit = function () {
    var a = true;
    if (!validateWhere()) {
        a = false
    }
    if (!validateName()) {
        a = false
    }
    if (!validateAddressMobile()) {
        a = false
    }
    if (a) {
        return true
    } else {
        return false
    }
};

/*ѡ���ַ*/
function selectAddress(addressId) {
    $("#addressId").val(addressId);
    spinerShow();
    $("#addressForm").submit();
    spinerHide();
}
function confirmDel(id) {
    if (confirm("ȷ��ɾ����")) {
        spinerShow();
        $("#delHref" + id).click();
        spinerHide();
    }
}
function keyDown(id) {
    $("a[name='selSpan']").removeClass("on");
    $("#selSpan" + id).addClass("on");
}
$(document).ready(function () {
    $("#background").css("height", $("#body").css("height")); ;
    if ($("#parent1").text() != "" && $("#parent1").text() != null) {
        $("#backUrl").attr("href", "javascript:backListener('parent1')");
    }
});