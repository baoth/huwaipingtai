/*! qsmart-detailpanel v1.0.0 ~ (c) 2014 lbmax depend zepto-1.1.4.js,zepto.touch.js,zepto.selector.js */
(function (window, document) {

    function QsmartTab(el, options) {
        var me = this;
        options = options || {};
        me.tab = typeof el == 'string' ? document.querySelector('#' + el) : el;
        me.$tab = $(me.tab);
        me.scroller = me.tab.children[0];
        me.$scroller = $(me.scroller);
        me.$tabs = me.$scroller.children();
        me.tabWidth = me.tab.clientWidth;
        me.tabHeight = me.tab.clientHeight;
        me.activeindex = 0;

        me.scrollerleft = 0;
        me.scrollertop = 0;

        me.options = {
            action: 'click',
            mdownaction: 'mousedown',
            mupaction: 'mouseup',
            moveaction: 'mousemove',
            cancelaction: 'mousecancel',
            animate: true,
            duration: 'fast',
            easeing: 'linear',
            delay: 0,
            callback: undefined,
            snapdistance: 0
        };

        for (var i in options) {
            me.options[i] = options[i];
        }

        if (me._ismobile()) {
            me.options.action = 'tap';
            me.options.mdownaction = 'touchstart';
            me.options.mupaction = 'touchend';
            me.options.moveaction = 'touchmove';
            me.options.cancelaction = 'touchcancel';
        }

        if (me.options.snapdistance < 0) me.options.snapdistance = 0;

        if (me.$tabs.length <= 0) return;

        me._render();

        me._bind();

    };

    QsmartTab.prototype._render = function () {
        var me = this;

        me.$tab.css('overflow', 'hidden'); me.$tab.addClass('qsm-mui-tab');



        var scrollerWidth = me.tabWidth * me.$tabs.length + 'px';
        me.maxLeft = me.tabWidth * (me.$tabs.length - 1);

        me.$scroller.css({ 'height': '100%', 'position': 'relative', 'width': scrollerWidth, 'left': '0px', 'top': '0px' });
        me.$scroller.addClass('qsm-mui-tab-scroller');

        $.each(me.$tabs, function (index, item) {

            var mleft = (index * me.tabWidth) + 'px';

            $(this).css({ 'width': me.tabWidth + 'px', 'height': me.tabHeight + 'px', 'position': 'absolute',
                'left': mleft, 'top': '0px', 'float': 'left','padding':0,'margin':0
            });
            $(this).addClass('qsm-mui-tab-item item-' + index);
            $(this).data('index', index);
        });

        me.tabscount = me.$tabs.length;
    };

    QsmartTab.prototype.next = function () {
        var me = this;

        me.activeindex++;
        if (me.activeindex > me.tabscount - 1 || me.activeindex < 0) {
            me.activeindex--;
            if (me.options.snapdistance == 0) return;
        }
        me._excute();
    };

    QsmartTab.prototype.prev = function () {
        var me = this;
        me.activeindex--;
        if (me.activeindex > me.tabscount - 1 || me.activeindex < 0) {
            me.activeindex++;
            if (me.options.snapdistance == 0) return;
        }
        me._excute();
    };

    QsmartTab.prototype.jump = function (index) {
        var me = this;
        if (index > me.tabscount - 1 || index < 0) return;
        if (me.activeindex == index) return;
        me.activeindex = index;

        me._excute(true);
    };

    QsmartTab.prototype._excute = function (isjump) {

        var me = this;
        $('.qsm-mui-tab-item').removeClass('active');
        $('.qsm-mui-tab-item.item-' + me.activeindex).addClass('active');
        me.scrollerleft = 0 - me.tabWidth * me.activeindex;
        var value = me.scrollerleft;
        if (me.options.animate && isjump != true) {
            me.$scroller.animate({ 'left': value + 'px' }, me.options.duration, me.options.easeing,
                                  me.options.callback(me.activeindex), me.options.delay);
        }
        else {
            me.$scroller.css({ 'left': value + 'px', 'opacity': '0' });
            me.$scroller.animate({ 'opacity': '1' }, 200, 'linear', me.options.callback(me.activeindex));
            //            me.$scroller.css('left', value + 'px');
            //            if (me.options.callback != undefined && typeof (me.options.callback) == 'function') {
            //                me.options.callback(me.activeindex);
            //            }
        }
    }

    QsmartTab.prototype._bind = function () {
        var me = this;

        me._addEvent(me.$tab, me.options.mdownaction, this);
        me._addEvent($(window), me.options.mupaction, this);

        me.$tab.on('swipeLeft', function () { me.next(); });
        me.$tab.on('swipeRight', function () { me.prev(); });
    }

    QsmartTab.prototype._start = function (e) {
        // React to left mouse button only
        if (e.type != 'touchstart') {
            if (e.button !== 0) {
                return;
            }
        }
        var point = e.touches ? e.touches[0] : e;

        this.startX = point.pageX;
        this.startY = point.pageY;

        this._addEvent(window, this.options.moveaction, this);
        this.started = true;
    };

    QsmartTab.prototype._move = function (e) {

        if (this.started != true) return;

        var point = e.touches ? e.touches[0] : e;
        var distX = point.pageX - this.startX, distY = point.pageY - this.startY;

        if (distX == 0) return;

        var value = this.scrollerleft + distX / 2;
        if (value > (0 + this.options.snapdistance) || value < (0 - (this.maxLeft + this.options.snapdistance))) return;

        this.$scroller.css('left', value + 'px');
    };

    QsmartTab.prototype._end = function (e) {
        console.log('end');
        if (this.started != true) return;
        this._removeEvent(window, this.options.moveaction, this);
        var point = e.touches ? e.touches[0] : e;
        if (point != undefined) {

            var distX = point.pageX - this.startX, distY = point.pageY - this.startY;
            if (distX > 0) this.prev();
            else if (distX < 0) this.next();
        }

        this.started = false;
    };

    QsmartTab.prototype.handleEvent = function (e) {

        switch (e.type) {
            case 'touchstart':
            case 'pointerdown':
            case 'MSPointerDown':
            case 'mousedown':
                this._start(e);
                break;
            case 'touchmove':
            case 'pointermove':
            case 'MSPointerMove':
            case 'mousemove':
                this._move(e);
                break;
            case 'touchend':
            case 'pointerup':
            case 'MSPointerUp':
            case 'mouseup':
            case 'touchcancel':
            case 'pointercancel':
            case 'MSPointerCancel':
            case 'mousecancel':

                this._end(e);
                break;
            case 'orientationchange':
            case 'resize':
                this._resize();
                break;
            case 'transitionend':
            case 'webkitTransitionEnd':
            case 'oTransitionEnd':
            case 'MSTransitionEnd':
                this._transitionEnd(e);
                break;
            case 'wheel':
            case 'DOMMouseScroll':
            case 'mousewheel':
                this._wheel(e);
                break;
            case 'keydown':
                this._key(e);
                break;
            case 'click':
                if (!e._constructed) {
                    e.preventDefault();
                    e.stopPropagation();
                }
                break;
        }
    }

    QsmartTab.prototype._addEvent = function (el, type, fn, capture) {

        if (el.addEventListener == undefined) {
            el[0].addEventListener(type, fn, !!capture);
            return;
        }
        el.addEventListener(type, fn, !!capture);
    };

    QsmartTab.prototype._removeEvent = function (el, type, fn, capture) {
        if (el.removeEventListener == undefined) {
            el[0].removeEventListener(type, fn, !!capture);
            return;
        }
        el.removeEventListener(type, fn, !!capture);
    };

    QsmartTab.prototype._ismobile = function () {

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
        module.exports = QsmartTab;
    } else {
        window.QsmartTab = QsmartTab;
    }
})(window, document);