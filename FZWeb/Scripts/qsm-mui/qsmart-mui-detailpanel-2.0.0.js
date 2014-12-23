/*! qsmart-detailpanel v1.0.0 ~ (c) 2014 lbmax depend iscroll.js,zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartDetailpanel(el, options, detaildata) {
        var me = this;
        me.detailpanel = typeof el == 'string' ? $('#' + el) : el;
        me.id = typeof el == 'string' ? el : el.attr('id');
        me.title = options.title || '';
        me.contentid = 's-activity-' + me.id;
        me.refreshid = me.contentid + '-refs';
        me.data = detaildata || {};
        me.options = {
            title: '',
            bounce: true,
            datatype: 'text',
            action: 'click',
            mdownaction: 'mousedown',
            mupaction: 'mouseup',
            panel: undefined,
            panelurl: '',
            moresetting: false
        };
        me._enabled = true;
        for (var i in options) {
            me.options[i] = options[i];
        }

        if (me._ismobile()) {
            me.options.action = 'tap';
            me.options.mdownaction = 'touchstart';
            me.options.mupaction = 'touchend';
        }

        me.divcontent = typeof options.panel == 'string' ? $('#' + options.panel) : options.panel;
        $(me.detailpanel).on(me.options.action, function (e) {
            if (me._enabled == false) return;
            e.stopPropagation();
            if ($('#' + me.contentid)[0] != undefined) return;
            me._init();
            me._render();

        });
    };

    QsmartDetailpanel.prototype.enable = function () {
        var me = this;
        me._enabled = true;
    };

    QsmartDetailpanel.prototype.disable = function () {
        var me = this;
        me._enabled = false;
    };

    QsmartDetailpanel.prototype._init = function () {
    };

    QsmartDetailpanel.prototype._render = function () {
        var me = this;
        var options = me.options;
        me.backid = me.contentid + '-back';
        me.moreid = me.contentid + '-more';
        me.okid = me.contentid + '-ok';
        me.footerid = me.contentid + '-footer';
        var div = "<div class='qsm-mui qsm-cb-cover panel' style='z-index:990' id='" + me.contentid + "' />";
        var header = "<header style='z-index:991'><div class='qsm-mui-header-v'><h1>" + me.title + "</h1>";

        if (me.options.moresetting) {
            header = header + "<span  style='display:none;' class='qsm-mui-menu more' id='" + me.moreid + "'></span>";
        }
        header = header + "</div></header>";

        me.wrapperid = "scl-" + me.id;
        var wrapper = "<div class='wrapper' id='" + me.wrapperid + "'><div class='scroller'><ul class='qsm-treebox-c'>";

        wrapper = wrapper + "</ul></div></div>";
        var footer = "<div class='qsm-mui-status' id='" + me.footerid + "' style='z-index:992;'>" +
                     "<ul class='lbs-list'><li id='" + me.footerid + "-0' style='width:50%;'>" +
                     "<div class='ct' id='" + me.backid + "'><div class='icon'><span>" + String.fromCharCode(parseInt('e60c', 16)) +
                     "</span></div><p>取消</p></div></li>" +
                     "<li id='" + me.footerid + "-1' style='width:50%;'>" +
                     "<div class='ct' id='" + me.okid + "'><div class='icon'><span>" + String.fromCharCode(parseInt('e60b', 16)) +
                     "</span></div><p>确定</p></div></li>" +
                     "</ul></div>";
        $("body").append(div);
        $("#" + me.contentid).append(header);
        $("#" + me.contentid).append(wrapper);
        $("#" + me.contentid).append(footer);
        me._bind();

        if (me.divcontent != undefined && me.divcontent[0] != undefined) {
            $("#" + me.contentid + " ul").append(me.divcontent);
            me.divcontent.addClass('qsm-panel-content');
            me._show();
        }
        else {
            me._appendrotatebar();
            setTimeout(function () {
                $.ajax({
                    type: 'POST',
                    url: options.panelurl,
                    dataType: options.datatype,
                    success: function (data) {

                        me._removerotatebar();
                        $("#" + me.contentid + " .scroller ul").append(data);
                        me.divcontent = $("#" + me.contentid + " .scroller ul div:first");
                        $(me.divcontent).addClass('qsm-panel-content');
                        me._resize();
                        me._show();
                    },
                    error: function (xhr, type) {
                    }
                });
            }, 100);
        }

    };

    QsmartDetailpanel.prototype._resize = function () {
        var me = this;
        var options = me.options;
        var topadd = $('#' + me.contentid + ' header').height();
        var liheight = $('#' + me.contentid + ' .scroller ul .qsm-panel-content').height();
        var footerheight = $('#' + me.footerid).height();
        var itemlength = liheight + topadd + footerheight;
        $('#' + me.contentid + ' .scroller ul').css('height', itemlength + 'px');
        if (me.scoller == undefined) {
            setTimeout(function () {
                me.scoller = new IScroll('#' + me.wrapperid, { mouseWheel: true, click: true, bounce: me.options.bounce });
            }, 1);
        }
        else {
            me.scoller.refresh();
        }
    };

    QsmartDetailpanel.prototype._show = function () {
        var me = this;
        $('#' + me.contentid + ' .qsm-panel-htm').trigger('beforeshow', [me]);
        $('#' + me.contentid).addClass('show');
        me._resize();
    };

    QsmartDetailpanel.prototype._hide = function () {
        var me = this;
        $('#' + me.contentid).removeClass('show');
        setTimeout(function () { me._destory(); }, 200);
    };

    QsmartDetailpanel.prototype._transition = function () {
        var me = this;

        setTimeout(function () {
            me.divcontent.addClass('qsm-transition');
        }, 100);
    };

    QsmartDetailpanel.prototype._destory = function () {
        var me = this;
        if (me.scoller != undefined) me.scoller.destroy();
        me.scoller = undefined;
        $('#' + me.contentid).remove();
        me.divcontent = undefined;
    };

    QsmartDetailpanel.prototype._buttonstriggle = function (bt) {
        var me = this;
        var footer = $('#' + me.footerid);
        if (footer.css('display') == 'none') {
            footer.show();
            $(bt).addClass('down');
        }
        else {
            footer.hide();
            $(bt).removeClass('down');
        }
    };

    QsmartDetailpanel.prototype._ismobile = function () {

        var ua = navigator.userAgent;
        var ipad = ua.match(/(iPad).*OS\s([\d_]+)/),
        isIphone = !ipad && ua.match(/(iPhone\sOS)\s([\d_]+)/),
        isAndroid = ua.match(/(Android)\s+([\d.]+)/),
        isMobile = isIphone || isAndroid;
        if (isMobile) {
            return true;
        } else {
            return false;
        }
    };

    QsmartDetailpanel.prototype._bind = function () {
        var me = this;
        var bk = $('#' + me.backid);
        bk.on(me.options.action, function (e) {
            e.stopPropagation();
            me._hide();
        });
        bk.on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        bk.on(me.options.mupaction, function (e) {

            $(this).removeClass('down');

        });
        var ok = $('#' + me.okid);
        ok.on(me.options.action, function (e) {
            e.stopPropagation();
            $('#' + me.contentid + " .qsm-panel-htm").trigger('beforesure', [me]);
            me._hide();
        });
        ok.on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        ok.on(me.options.mupaction, function (e) {

            $(this).removeClass('down');

        });
        $('#' + me.moreid).on(me.options.action, function (e) {
            e.stopPropagation();
            me._buttonstriggle(this);
        });
        $('#' + me.contentid + " .sure").on(me.options.action, function (e) {
            e.stopPropagation();
            me._hide();
        });
    };

    QsmartDetailpanel.prototype._appendrotatebar = function () {
        var me = this;
        var rotatebarhtml = "<div class='spinner'><div class='bar1' /><div class='bar2' /><div class='bar3' /><div class='bar4' />" +
                    "<div class='bar5' /><div class='bar6' /><div class='bar7' /><div class='bar8' />" +
                    "<div class='bar9' /><div class='bar10' /><div class='bar11' /><div class='bar12' /></div>";
        $('#' + me.contentid + ' .scroller ul').append("<li class='qsm-mui-autorefresh' id = '" + me.refreshid + "'>" + rotatebarhtml + "</li>");
    };
    QsmartDetailpanel.prototype._removerotatebar = function () {
        var me = this;
        $('#' + me.refreshid).remove();
    };

    if (typeof module != 'undefined' && module.exports) {
        module.exports = QsmartDetailpanel;
    } else {
        window.QsmartDetailpanel = QsmartDetailpanel;
    }
})(window, document);