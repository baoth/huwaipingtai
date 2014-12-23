/*! qsmart-detailpanel v1.0.0 ~ (c) 2014 lbmax depend zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartStatusBar(el, options) {
        var me = this;

        me.statusbar = typeof el == 'string' ? $('#' + el) : el;
        me.id = typeof el == 'string' ? el : el.attr('id');


        me.options = {
            action: 'click',
            mdownaction: 'mousedown',
            mupaction: 'mouseup',
            linenumber: 4,
            buttons: []
        };


        for (var i in options) {
            me.options[i] = options[i];
        }

        if (me._ismobile()) {
            me.options.action = 'tap';
            me.options.mdownaction = 'touchstart';
            me.options.mupaction = 'touchend';
        }

        me._init();
        me._render();
    };

    QsmartStatusBar.prototype._init = function () {
        var me = this;
        me.itemindex = 0;
    };

    QsmartStatusBar.prototype._render = function () {
        var me = this;
        var options = me.options;
        me.contentid = me.id + "-c";
        me.statusbar.children().remove();
        me.statusbar.addClass('qsm-mui-status');
        me.statusbar.append("<ul id='" + me.contentid + "' class='lbs-list'></ul>");
        me._addbuttons(options.buttons);
    };

    QsmartStatusBar.prototype._addbuttons = function (buttons) {
        var me = this;


        if (buttons == undefined || buttons.length <= 0) return;
        $.each(buttons, function (index, button) {
            if (typeof (button) != 'object') return;
            me._addbuttonitem(button);
        }
        );
    };

    QsmartStatusBar.prototype._addbuttonitem = function (button) {
        /*
        //button example
        var bt = {
        fonticon:'save', //16进制
        title: '保存',
        action:undefined,
        sub:[]
        }
        */

        var me = this;

        var lwidth = 100 / me.options.linenumber + '%';
        var iid = me.contentid + '-bt-' + me.itemindex;
        var item = "<li id='" + iid + "' style='width:" + lwidth + "'><div class='ct'><div class='icon'><span>" +
                   String.fromCharCode(parseInt(button.fonticon, 16)) + "</span></div><p>" + button.title + "</p></div></li>";
        $('#' + me.contentid).append(item);
        $('#' + iid + ' .ct').on(me.options.mdownaction, function () {
            $(this).addClass('down');
        });
        $('#' + iid + ' .ct').on(me.options.mupaction, function () {
            $(this).removeClass('down');
        });
        if (button.action != undefined && typeof (button.action) == 'function') {

            $('#' + iid).on(me.options.action, button.action);
        }
        me.itemindex++;
    };

    QsmartStatusBar.prototype._destory = function () {
        var me = this;
        me._init();
        me.statusbar.children().remove();
    };

    QsmartStatusBar.prototype._ismobile = function () {

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

    if (typeof module != 'undefined' && module.exports) {
        module.exports = QsmartStatusBar;
    } else {
        window.QsmartStatusBar = QsmartStatusBar;
    }
})(window, document);