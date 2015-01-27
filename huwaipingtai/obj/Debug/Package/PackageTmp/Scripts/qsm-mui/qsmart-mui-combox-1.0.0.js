/*! qsmart-combox v1.0.0 ~ (c) 2014 lbmax depend iscroll.js,zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartCombox(el, options, txtel) {

        var me = this;

        me.itemindex = 0;
        me.combox = typeof el == 'string' ? $('#' + el) : el;
        if (txtel != undefined) {
            me.text = typeof el == 'string' ? $('#' + txtel) : txtel;
        } else { me.text = undefined; }
        me.id = typeof el == 'string' ? el : el.attr('id');
        me.title = options.title || '';
        me.total = 0;
        me._loaded = false;
        me.contentid = 's-activity-' + me.id;
        me.options = {
            shadowtxt: '请输入搜索内容',
            datatype: 'text',
            title: '',
            action: 'click',
            mdownaction: 'mousedown',
            mupaction: 'mouseup',
            dataurl: '',
            idcolumn: undefined,
            displaycolumn: [],
            displayseperator: ',',
            columns: [],
            orders: [],
            orderdirs: [],
            filters: [],
            filtertext: '',
            pagation: true,
            pagesize: 20,
            pageindex: 1
        };

        for (var i in options) {
            me.options[i] = options[i];
        }

        if (me._ismobile()) {
            me.options.action = 'tap';
            me.options.mdownaction = 'touchstart';
            me.options.mupaction = 'touchend';
        }

        $(me.combox).on(me.options.action, function (e) {
            e.stopPropagation();

            if ($('#' + me.contentid)[0] != undefined) return;
            me._render();
            me._show();
            me._init();
        });
    };

    QsmartCombox.prototype._init = function () {
        var me = this;
        me.options.pageindex = 1;
        me.total = -1;
        me._appendbatch();
    };

    QsmartCombox.prototype._render = function () {
        var me = this;

        me.searchboxid = me.contentid + "-box";
        me.searchlabelid = me.contentid + "-lbl";
        me.backid = me.contentid + '-back';
        var div = "<div class='qsm-mui qsm-cb-cover combox' style='z-index:999' id='" + me.contentid + "' />";


        var search = "<input id='" + me.searchboxid + "' type='text' name='searchtext' class='qsm-mui-scl-t' autocomplete='off' placeholder='" + me.options.shadowtxt + "'>" +
        "<span id='" + me.searchlabelid + "' class='qsm-mui-scl-l'></span>";
        var header = "<header style='z-index:1001'><div class='qsm-mui-header-v'><h1>" + me.title + "</h1><span id='" + me.backid + "' class='qsm-mui-menu first back'>&nbsp;</span>";
        header = header + search + "</div></header>";


        me.wrapperid = "scl-" + me.id;
        me.morebuttonid = me.contentid + '-more';
        me.morebuttoncontent = me.contentid + '-ct'
        var wrapper = "<div class='wrapper' id='" + me.wrapperid + "'><div class='scroller'><ul class='qsm-combox-c'>";

        if (me.options.pagation) {
            wrapper = wrapper + "<li class='qsm-mui-more-button' id = '" + me.morebuttoncontent + "'><div id='" + me.morebuttonid + "' class='qsm-button qsm-more-button'>加载更多</div></li>";
        }

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
        if (me.options.pagation) {
            $('#' + me.morebuttonid).on(me.options.action, function (e) {
                e.stopPropagation();
                me._appendbatch();
            });
        }
        $('#' + me.searchlabelid).on(me.options.action, function (e) {
            e.stopPropagation();
            me._reload();
        });
        $('#' + me.searchlabelid).on(me.options.mdownaction, function (e) {
            $(this).addClass('down');
        });
        $('#' + me.searchlabelid).on(me.options.mupaction, function (e) {
            $(this).removeClass('down');
        });
        $('#' + me.searchboxid).on('input', function (e) {

            var vl = $(this).val();
            if (vl == '') {
                me._reload();
            }
        });
        $('#' + me.searchboxid).on('keypress', function (e) {

            if (e.keyCode == "13") {
                me._reload();
            }
        });

    };

    QsmartCombox.prototype._reload = function () {
        var me = this;
        if (me.options.pagation) {
            me.options.pageindex = 1;
            me.total = -1;
            me._cleardata();
            me._appendbatch();
        }
        else {
            me._localfilte();
        }
    };

    QsmartCombox.prototype._cleardata = function () {
        var me = this;
        $('#' + me.contentid + ' ul .qsm-mui-dataitem').remove();
    };

    QsmartCombox.prototype._appendbatch = function () {
        var me = this;
        var options = me.options;
        var curcount = options.pagesize * (options.pageindex - 1);

        $.ajax({
            type: 'POST',
            url: options.dataurl,
            data: { columns: options.columns.join(','),
                orders: options.orders.join(','),
                orderdirs: options.orderdirs.join(','),
                filters: options.filters.join(','),
                filtertext: $('#' + me.searchboxid).val(),
                pagesize: options.pagesize,
                pageindex: options.pageindex,
                pagation: options.pagation ? 1 : 0
            },
            dataType: options.datatype,
            success: function (data) {

                var data = eval(data)[0];
                $.each(data.rows, function (index, item) { me._additem(item); });
                me.total = data.total;
                me.options.pageindex++;
                me._resize();
            },
            error: function (xhr, type) {

            }
        });

    };

    QsmartCombox.prototype._resize = function () {
        var me = this;
        var options = me.options;
        var curcount = options.pagesize * options.pageindex - 1;
        var needmorebutton = curcount < me.total ? true : false;
        var morebutton = $('#' + me.morebuttoncontent);

        var li_count = $('#' + me.contentid + ' ul').children().length;

        if (li_count == 1) return;

        if (needmorebutton) {
            morebutton.insertAfter($('#' + me.contentid + ' li:last'));
            if (morebutton.hasClass('show') == false) morebutton.addClass('show');
        }
        else {
            if (morebutton.hasClass('show') == true) morebutton.removeClass('show');
        }
        var topadd = $('#' + me.contentid + ' header').height();
        var liheight = $('#' + me.contentid + ' ul li:first').height();
        var itemlength = needmorebutton ? li_count * liheight + topadd : (li_count - 1) * liheight + topadd;
        $('#' + me.contentid + ' ul').css('height', itemlength + 'px');
        if (me.scoller == undefined) {
            setTimeout(function () {
                me.scoller = new IScroll('#' + me.wrapperid, { mouseWheel: true, click: true });
                me.scoller._scorllend = function (x, y) {
                    if (y < 0) {
                        console.log(y);
                    }
                }
            }, 1);
        }
        else {
            me.scoller.refresh();
        }
    };

    QsmartCombox.prototype._additem = function (data) {
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
        var text = '';
        var aa = [];
        var sp = me.options.displayseperator;
        var inntxt = '';
        if (me.options.columns != undefined && me.options.columns.length > 0) {
            var cols = [];
            $.each(me.options.columns, function (index, value) {
                if (value.toLowerCase() != me.options.idcolumn.toLowerCase()) {
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
        var item = "<li class='qsm-mui-dataitem' id='" + id + "' data-id='" + dataid + "' data-txt='" + text + "'>";
        item = item + inntxt + "</li>";
        $('#' + me.contentid + ' ul').append(item);
        $('#' + id).on(me.options.action, function (e) {
            e.stopPropagation();
            me.combox.data('id', $(this).data('id'));
            if (me.options.displaycolumn != undefined && me.text != undefined) {
                me.text.text($(this).data('txt'));
            }
            me._hide();
        });
    };

    QsmartCombox.prototype._show = function () {
        var me = this;
        $('#' + me.contentid).addClass('show');
        me._resize();
    };

    QsmartCombox.prototype._hide = function () {
        var me = this;
        $('#' + me.contentid).removeClass('show');
        setTimeout(function () { me._destory(); }, 200);
    };

    QsmartCombox.prototype._localfilte = function () {
        var me = this;
        var filter = $('#' + me.searchboxid);
        if (filter == undefined) return;
        var filtertext = filter.val();
        if (filtertext == '') {
            $('#' + me.contentid + ' ul .qsm-mui-dataitem').show();
            return;
        }
        $('#' + me.contentid + ' ul .qsm-mui-dataitem').each(function () {
            var txt = $(this).text();
            if (txt.indexOf(filtertext) >= 0) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    };

    QsmartCombox.prototype._transition = function () {
        var me = this;
        $('#' + me.contentid + " ul li").each(function () {
            var mitem = $(this);
            setTimeout(function () {
                mitem.addClass('qsm-transition');
            }, 100);
        });
    };

    QsmartCombox.prototype._destory = function () {
        var me = this;
        if (me.scoller != undefined) me.scoller.destroy();
        me.scoller = undefined;
        $('#' + me.contentid).remove();
    };

    QsmartCombox.prototype._ismobile = function () {

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
        module.exports = QsmartCombox;
    } else {
        window.QsmartCombox = QsmartCombox;
    }
})(window, document);


