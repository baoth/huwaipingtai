/*! qsmart-slide v1.0.0 ~ (c) 2014 lbmax depend zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartSlide(el) {
        var me = this;
        me.checked = false;
        me.slide = typeof el == 'string' ? $('#' + el) : el;
        me.id = typeof el == 'string' ? el : el.attr('id');
        me.options = { action: 'click' };
        if (me._ismobile()) {
            me.options.action = 'tap';
        }
        me._init();
        $(me.slide).on(me.options.action, function (e) {
            e.stopPropagation();
            if (me.slide.hasClass('on')) {
                me.setunchecked();
            }
            else {
                me.setchecked();
            }
        });
    };

    QsmartSlide.prototype.setunchecked = function () {
        var me = this;
        me.slide.removeClass('on');
        me.checked = false;
    };
    QsmartSlide.prototype.setchecked = function () {
        var me = this;
        me.slide.addClass('on');
        me.checked = true;
    };
    QsmartSlide.prototype._init = function () {
        var me = this;
        if (me.slide.hasClass('qsm-slide') == false) {
            me.slide.addClass('qsm_slide');
        }
        me.slide.removeClass('on');
    };

    QsmartSlide.prototype._ismobile = function () {

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
        module.exports = QsmartSlide;
    } else {
        window.QsmartSlide = QsmartSlide;
    }
})(window, document);