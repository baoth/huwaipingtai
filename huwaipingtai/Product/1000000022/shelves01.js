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
    $('#allinfos').on($.clickAction, function () {
        var me = $(this);
        if (me.hasClass('on')) {
            me.removeClass('on');
            $('#infos-detail').hide();
        }
        else {
            me.addClass('on');
            $('#infos-detail').show();
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

/*页面加载*/
function pageload(options) {
    if (options == undefined) return;
    //商品描述信息
    options.title = options.title || "";
    if (options.title == "") {
        $('#title').hide();
    }
    else {
        $('#title span').text(options.title);
    }
    //促销信息
    options.promotion = options.promotion || "";
    if (options.promotion == "") {
        $('#promotion').hide();
    }
    else {
        $('#promotion span').text(options.promotion);
    }
    //服务信息
    options.vender = options.vender || "";
    if (options.vender == "") {
        $('#vender').hide();
    }
    else {
        $('#vender span').text(options.vender);
    }
    //赠品信息
    giftscreator(options.gifts);
    //添加颜色选项
    colorcreator(options.colors);
    //添加尺寸选项
    sizecreator(options.sizes);
    //添加图片浏览
    createbanner(options.imgs);
    $('.btn-hf').on($.clickAction, function () {
        var sku = $(this).data('sku');
        if (sku != undefined && sku != "") {
            var url = '/' + sku + '.html';
            $(this).attr('href', url);
        }
    });
};

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
        item.on = item.on || ""; item.sku = item.sku || ''; item.name = item.name || '';
        if (item.on == 'true' || item.on == true) {
            item.on = ' on';
            $('#sh-color').text(item.name);
        }
        $color.append("<a href='' data-sku='" + item.sku + "' class='btn-hf btn-color-op mb-10" + item.on + "'>" + item.name + "</a>&nbsp;");
    });
};
function sizecreator(sizes) {
    sizes = sizes || [];
    if (sizes.length == 0) return;
    var $size = $('#size');
    $size.children().remove();
    $.each(sizes, function (index, item) {
        item.on = item.on || ""; item.sku = item.sku || ''; item.name = item.name || '';
        if (item.on == 'true' || item.on == true) {
            item.on = ' on';
            $('#sh-size').text(item.name);
        }
        $size.append("<a href='' data-sku='" + item.sku + "' class='btn-hf btn-color-op mb-10" + item.on + "'>" + item.name + "</a>&nbsp;");
    });
};

/*赠品*/
function giftscreator(gifts) {
    gifts = gifts || [];
    if (gifts.length == 0) {
        $('.info.gifts').hide();
        return;
    }
    var $giftscontent = $('#giftslist .content');
    $giftscontent.children().remove();
    $.each(gifts, function (index, item) {
        item.sku = item.sku || ''; item.name = item.name || '';
        $giftscontent.append("<a href='' data-sku='" + item.sku + "' class='link-event btn-hf'>" + item.name + "</a>");
    });
}