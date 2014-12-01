/*! qsmart-detailpanel v1.0.0 ~ (c) 2014 lbmax depend zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartPop() {
        var me = this;

        me.options = {
            action: 'click',
            mdownaction: 'mousedown',
            mupaction: 'mouseup',
            buttontext: []
        };
        if (me._ismobile()) {
            me.options.action = 'tap';
            me.options.mdownaction = 'touchstart';
            me.options.mupaction = 'touchend';
        }
    };

    QsmartPop.prototype.pop = function (title, text, callback, bttexts, single) {
        var me = this;
        me._destory();
        me.title = title || '';
        me.text = text || '';
        me.options.callback = callback;
        me.options.buttontext = bttexts || [];
        me.single = single || false;
        me._render();
    };

    QsmartPop.prototype._render = function () {
        var me = this;

        var options = me.options;

        var div = "<div class='qsm-mui-pop' id='qsm-pop-now'><div class='dw-persp'>" +
                "<div class='dwo'></div></div></div>";
        var con = "";
        if (me.single == false) {
            var bt1 = options.buttontext[0] || '取消', bt2 = options.buttontext[1] || '确定';
            con = "<div role='dialog' tableindex='-1' class='dw'>" +
                 "<div class='dwtitle'>" + me.title + "</div>" +
                 "<div class='dwtext'><span class='dwtext-c'>" + me.text + "</span></div>" +
                 "<div class='dwbt'>" +
                 "<div class='dwbt-item cancel' id='dwbt-cancel'>" + bt1 + "</div>" +
                 "<div class='dwbt-item ok' id='dwbt-ok'>" + bt2 + "</div>" +
                 "</div></div>";
        }
        else {
            var bt1 = options.buttontext[0] || '关闭';
            con = "<div role='dialog' tableindex='-1' class='dw'>" +
                 "<div class='dwtitle'>" + me.title + "</div>" +
                 "<div class='dwtext'><span class='dwtext-c'>" + me.text + "</span></div>" +
                 "<div class='dwbt'>" +
                 "<div class='dwbt-item single' id='dwbt-single'>" + bt1 + "</div>" +
                 "</div></div>";
        }
        $("body").append(div);
        $("#qsm-pop-now .dw-persp").append(con);

        var pheight = $("#qsm-pop-now .dwo").height();
        var cheight = $("#qsm-pop-now .dw").height();
        $("#qsm-pop-now .dw").css('top', (pheight - cheight) / 2);
        me._bind();
    };

    QsmartPop.prototype._destory = function () {
        var me = this;
        $('.qsm-mui-pop').remove();
    };

    QsmartPop.prototype._ismobile = function () {

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

    QsmartPop.prototype._bind = function () {
        var me = this;
        var cancel = $('#dwbt-cancel');
        var ok = $('#dwbt-ok');
        cancel.on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        cancel.on(me.options.mupaction, function (e) {
            $(this).removeClass('down');
        });
        cancel.on(me.options.action, function (e) {
            e.stopPropagation();
            me._destory();
            if (me.options.callback != undefined && typeof (me.options.callback) == 'function') {
                me.options.callback(0);
            }
        });
        ok.on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        ok.on(me.options.mupaction, function (e) {
            $(this).removeClass('down');
        });
        ok.on(me.options.action, function (e) {
            e.stopPropagation();
            me._destory();
            if (me.options.callback != undefined && typeof (me.options.callback) == 'function') {
                me.options.callback(1);
            }
        });
        var single = $('#dwbt-single');
        single.on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        single.on(me.options.mupaction, function (e) {
            $(this).removeClass('down');
        });
        single.on(me.options.action, function (e) {
            e.stopPropagation();
            me._destory();
            if (me.options.callback != undefined && typeof (me.options.callback) == 'function') {
                me.options.callback(0);
            }
        });
    };

    if (typeof module != 'undefined' && module.exports) {
        module.exports = QsmartPop;
    } else {
        window.QsmartPop = QsmartPop;
    }
})(window, document);