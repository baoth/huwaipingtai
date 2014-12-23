/*! qsmart-treebox v1.0.0 ~ (c) 2014 lbmax depend iscroll.js,zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartTreebox(el, options, txtel) {
        var me = this;
        me.treebox = typeof el == 'string' ? $('#' + el) : el;
        if (txtel != undefined) {
            me.text = typeof el == 'string' ? $('#' + txtel) : txtel;
        } else { me.text = undefined; }
        me.id = typeof el == 'string' ? el : el.attr('id');
        me.title = options.title || '';
        me.contentid = 's-activity-' + me.id;
        me.refreshid = me.contentid + '-ref';
        me.options = {
            title: '',
            datatype: 'text',
            action: 'click',
            mdownaction: 'mousedown',
            mupaction: 'mouseup',
            dataurl: '',
            idcolumn: undefined,
            displaycolumn: [],
            displayseperator: ',',
            columns: [],
            orders: [],
            orderdirs: []
        };

        for (var i in options) {
            me.options[i] = options[i];
        }

        if (me._ismobile()) {
            me.options.action = 'tap';
            me.options.mdownaction = 'touchstart';
            me.options.mupaction = 'touchend';
        }

        $(me.treebox).on(me.options.action, function (e) {
            e.stopPropagation();
            if ($('#' + me.contentid)[0] != undefined) return;
            me._render();
            me._init();
            me._show();
        });
    };

    QsmartTreebox.prototype._init = function () {
        var me = this;
        me.itemnextclass = "qsm-next";
        me.curid = undefined;
        me.itemindex = 0;
        me.upids = [];
        me._cleardata();
        me._loaddata();
    };

    QsmartTreebox.prototype._render = function () {
        var me = this;

        me.upbuttonid = me.contentid + "-up";
        me.backid = me.contentid + '-back';
        var div = "<div class='qsm-mui qsm-cb-cover treebox' style='z-index:999' id='" + me.contentid + "' />";
        var header = "<header style='z-index:1001'><div class='qsm-mui-header-v'><h1>" + me.title + "</h1><span id='" + me.backid + "' class='qsm-mui-menu first back'>&nbsp;</span>" +
                    "<span class='qsm-mui-menu up' id='" + me.upbuttonid + "'></span>"
        //        var up = "<div id='" + me.upbuttonid + "' class='qsm-button qsm-up-button'>返回上级</div>";

        header = header + "</div></header>";

        me.wrapperid = "scl-" + me.id;
        var wrapper = "<div class='wrapper' id='" + me.wrapperid + "'><div class='scroller'><ul class='qsm-treebox-c'>";

        wrapper = wrapper + "</ul></div></div>";

        $("body").append(div);
        $("#" + me.contentid).append(header);
        $("#" + me.contentid).append(wrapper);
        $('#' + me.backid).on(me.options.action, function (e) {
            e.stopPropagation();
            me._hide();
        });
        $('#' + me.backid).on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        $('#' + me.backid).on(me.options.mupaction, function (e) {
            $(this).removeClass('down');
        });
        $('#' + me.upbuttonid).on(me.options.action, function (e) {
            e.stopPropagation();
            me._upload();
        });
        $('#' + me.upbuttonid).on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        $('#' + me.upbuttonid).on(me.options.mupaction, function (e) {
            $(this).removeClass('down');
        });
    };

    QsmartTreebox.prototype._download = function (id, upid) {
        var me = this;
        me.curid = id;
        me._cleardata();
        me._appendrotatebar();
        setTimeout(function () {
            me.upids.push(upid);
            me._loaddata(id);
        }, 100);
    };

    QsmartTreebox.prototype._upload = function () {
        var me = this;
        var id = me.upids.pop();
        me.curid = id;
        me._cleardata();
        me._appendrotatebar();
        setTimeout(function () {
            me._loaddata(id);
        }, 100);
    };

    QsmartTreebox.prototype._cleardata = function () {
        var me = this;
        $('#' + me.contentid + ' ul .qsm-mui-dataitem').remove();
    };

    QsmartTreebox.prototype._loaddata = function (parentid) {
        var me = this;
        var options = me.options;
        if (parentid == undefined) parentid = '';
        $.ajax({
            type: 'POST',
            url: options.dataurl,
            data: { columns: options.columns.join(','),
                orders: options.orders.join(','),
                orderdirs: options.orderdirs.join(','),
                parentid: parentid
            },
            dataType: options.datatype,
            success: function (data) {

                var data = eval(data);
                me._removerotatebar();
                $.each(data, function (index, item) {
                    me._additem(item);
                });
                me._resize();
            },
            error: function (xhr, type) {
            }
        });
    };

    QsmartTreebox.prototype._resize = function () {
        var me = this;
        var options = me.options;

        var li_count = $('#' + me.contentid + ' ul').children().length;

        if (me.upids.length > 0) {
            $('#' + me.upbuttonid).show();
        }
        else {
            $('#' + me.upbuttonid).hide();
        }
        var topadd = $('#' + me.contentid + ' header').height();
        var liheight = $('#' + me.contentid + ' ul li:first').height();
        var itemlength = li_count * liheight + topadd;
        $('#' + me.contentid + ' ul').css('height', itemlength + 'px');

        //        me._transition();

        if (me.scoller == undefined) {
            setTimeout(function () {
                me.scoller = new IScroll('#' + me.wrapperid, { mouseWheel: true, click: true });
            }, 1);
        }
        else {
            me.scoller.refresh();
        }

    };

    QsmartTreebox.prototype._additem = function (data) {

        var me = this;
        if (data == undefined || data == null) return;
        var id = me.contentid + '-item-' + me.itemindex;
        me.itemindex++;
        var dataid = undefined;

        if (me.options.idcolumn != undefined) {
            dataid = data[me.options.idcolumn];
        }
        if (dataid == undefined || dataid == null) {
            dataid = '';
        }

        var sp = me.options.displayseperator;
        var cansub = data['sub'];
        var item = "";
        var text = "";
        var inntxt = "";
        if (me.options.columns != undefined && me.options.columns.length > 0) {
            var cols = [];
            $.each(me.options.columns, function (index, value) {

                if (value != me.options.idcolumn) {
                    cols.splice(0, 0, value);
                }
            });
            if (cols.length > 0) {
                $.each(cols, function (index, value) {
                    inntxt = inntxt + data[value] + sp;
                });
                inntxt = inntxt.substring(0, inntxt.length - sp.length);
            }
        }
        if (me.options.displaycolumn != undefined && me.options.displaycolumn.length > 0) {
            $.each(me.options.displaycolumn, function (index, value) {
                text = text + data[value] + sp;
            });
            text = text.substring(0, text.length - sp.length);
        }
        if (cansub == "1") {
            item = "<li class='qsm-mui-dataitem " + me.itemnextclass + "' id='" + id + "' data-id='" + dataid + "' data-txt='" + text + "'>";
        }
        else {
            item = "<li class='qsm-mui-dataitem' id='" + id + "' data-id='" + dataid + "' data-txt='" + text + "'>";
        }
        item = item + inntxt + "</li>";
        $('#' + me.contentid + ' ul').append(item);

        $('#' + id).on(me.options.action, function (e) {
            e.stopPropagation();
            var dataid = $(this).data('id');
            if ($(this).hasClass(me.itemnextclass)) {

                me._download(dataid, me.curid);
            }
            else {
                me.treebox.data('id', dataid);
                if (me.options.displaycolumn != undefined && me.text != undefined) {
                    me.text.text($(this).data('txt'));
                }
                me._hide();
            }
        });
    };

    QsmartTreebox.prototype._show = function () {
        var me = this;
        $('#' + me.contentid).addClass('show');
        //        me._resize();
    };

    QsmartTreebox.prototype._hide = function () {
        var me = this;
        $('#' + me.contentid).removeClass('show');
        setTimeout(function () { me._destory(); }, 200);
    };

    QsmartTreebox.prototype._transition = function () {
        var me = this;
        $('#' + me.contentid + " ul li").each(function () {
            var mitem = $(this);
            setTimeout(function () {
                mitem.addClass('qsm-transition');
            }, 100);
        });
    };

    QsmartTreebox.prototype._destory = function () {
        var me = this;
        if (me.scoller != undefined) me.scoller.destroy();
        me.scoller = undefined;
        $('#' + me.contentid).remove();
    };

    QsmartTreebox.prototype._ismobile = function () {

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

    QsmartTreebox.prototype._appendrotatebar = function () {
        var me = this;
        var rotatebarhtml = "<div class='spinner'><div class='bar1' /><div class='bar2' /><div class='bar3' /><div class='bar4' />" +
                    "<div class='bar5' /><div class='bar6' /><div class='bar7' /><div class='bar8' />" +
                    "<div class='bar9' /><div class='bar10' /><div class='bar11' /><div class='bar12' /></div>";
        $('#' + me.contentid + ' ul').append("<li class='qsm-mui-autorefresh' id = '" + me.refreshid + "'>" + rotatebarhtml + "</li>");
    };
    QsmartTreebox.prototype._removerotatebar = function () {
        var me = this;
        $('#' + me.refreshid).remove();
    };

    if (typeof module != 'undefined' && module.exports) {
        module.exports = QsmartTreebox;
    } else {
        window.QsmartTreebox = QsmartTreebox;
    }
})(window, document);