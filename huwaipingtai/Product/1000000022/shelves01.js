$(document).ready(function () {
    $('.i-a-back').on($.clickAction, function () { window.history.back(-1); });
    $('.i-a-bh').on('click', function () {
        var tg = $('.i-bh-tab');
        if (tg.hasClass('on')) {
            tg.removeClass('on');
        }
        else {
            tg.addClass('on');
        }
    });

    /*数量加减*/
    $("#number").numeral();
    $("#number").on('blur', function () {
        var value = $(this).val();
        if (value) {
            value = parseInt(value);
            if (value > 0) {
                $(this).val(q);
            }
            else {
                $(this).val("1");
            }
        }
        else {
            $(this).val("1");
        }
    });
    $('#plus').on($.clickAction, function () {
        var $number = $('#number');
        var value = $number.val();
        if (value) {
            value = parseInt(value);
            if (value >= 0) {
                value++;
                $number.val(value);
            }
        }
        else {
            $number.val("1");
        }
    });
    $('#minus').on($.clickAction, function () {
        var $number = $('#number');
        var value = $number.val();
        if (value) {
            value = parseInt(value);
            if (value > 1) {
                value--;
                $number.val(value);
            }
        }
        else {
            $number.val("1");
        }
    });
});
/*图片浏览器*/
function createbanner(imgs) {
    imgs = imgs || [];
    if (imgs.length == 0) return;
    var $banner = $('#banner-content');
    $banner.children().remove();
    var $navcontent = $('#nav-content');
    $navcontent.children().remove();
    var navwidth = (100 / imgs.length);
    $.each(imgs, function (index, item) {
        $banner.append("<div><img src='" + item + "' /></div>");
        var leftpercent = index * navwidth;
        $navcontent.append("<li id='nav-" + index + "' style='left:" + leftpercent + "%;width:" + navwidth + "%;'></li>");
    });
    document.tab = new QsmartTab('tabpages', { 'callback': tabcallback });
    tabcallback(0);
};
function tabcallback(index) {
    $('#nav-content li').removeClass('on');
    $('#nav-' + index).addClass('on');
};

/*颜色 尺码*/
function colorcreator(colors) {
    colors = colors || [];
    if (colors.length == 0) return;
    var $color = $('#color');
    $color.children().remove();
    $.each(colors, function (index, item) {
        item.on = item.on || "";
        if (item.on == 'true' || item.on == true) item.on = ' on'
        $color.append("<a href='" + item.href + "' class='btn-color-op mb-10" + item.on + "'>" + item.name + "</a>&nbsp;");
    });
};
function sizecreator(sizes) {
    sizes = sizes || [];
    if (sizes.length == 0) return;
    var $size = $('#size');
    $size.children().remove();
    $.each(sizes, function (index, item) {
        item.on = item.on || "";
        if (item.on == 'true' || item.on == true) item.on = ' on'
        $size.append("<a href='" + item.href + "' class='btn-color-op mb-10" + item.on + "'>" + item.name + "</a>&nbsp;");
    });
};