var palyer ={};
$.extend(palyer, {
    init: function (c) {

        this.threadTime = 500;
        this.sliderTouchXY = {};
        this.sliderTime = 0;
        this.sliderImgIndex = 1;
        this.isShowOldData = false;
        this.oldkeyword = "";
        this.sliderImgList = [{
            url: 'javascript:void(0)', img: 'http://localhost:12156/Product/Images/n1/test/9b839728-d085-468e-acb8-88eb9eb008b8.jpg', title: '无乐不作嗨翻周末9.9元起'
        }, {
            url: 'javascript:void(0)', img: 'http://localhost:12156/Product/Images/n1/test/13586d56-2df9-45db-89a4-577a3e009c32.jpg', title: 'test1'
        }, {
            url: 'javascript:void(0)', img: 'http://localhost:12156/Product/Images/n1/test/66946b59-c0f8-4414-b777-f0c02c302c20.jpg', title: 'test2'
        }];

    },
    sliderSetUlWidth: function () {
        var c = this;
        if (c.sliderImgList.length > 1) {
            c.sliderWidth = $("#slider").width();
            $("#slider_touch").css("width", (c.sliderImgList.length + "00%"));
            $("#slider_touch").find("li").css("width", c.sliderWidth)
        }
    },
    sliderSetLiWidth: function () {
        var c = this;
        if (c.sliderImgList.length > 1) {
            $("#slider_touch").find("li").css("width", c.sliderWidth)
        }
    },
    sliderHTML: function () {
        var e = [];
        var p = [];
        var f = this;
        for (var d = 0; d < f.sliderImgList.length; d++) {
            var c = f.sliderImgList[d]; //' + c.url + '
            e.push('<li style="float:left;"><a href="' + c.url + '" ><img alt="' + c.title + '"  style="z-index:1;height:auto; max-width:640px; min-width:320px; width: 100%" src="' + c.img + '"  /></a></li>')
            p.push(d == 0 ? '<span class="point selected"></span>' : ' <span class="point"></span>');
        }
        e = e.join("");
        $("#slider_touch").html("");
        $("#slider_touch").html(e);
        $("#serial-number").html("");
        $("#serial-number").html(p.join(""));
    },
    sliderImg: function () {
        var c = this;
        if (c.sliderImgIndex < 0) {
            c.sliderImgIndex = c.sliderImgList.length - 1
        } else {
            if (c.sliderImgIndex >= c.sliderImgList.length) {
                c.sliderImgIndex = 0
            }
        }
        $("#slider_touch").animate({ left: ("-" + (c.sliderImgIndex * c.sliderWidth) + "px") }, 200, "ease-in");
        $("#serial-number").find("span").removeClass("selected");
        $("#serial-number").find("span").eq(c.sliderImgIndex).addClass("selected");
        c.sliderImgIndex++
    },
    sliderRun: function (d) {
        var c = this;
        if (c.sliderTime >= 4000) {
            c.sliderImg();
            c.sliderTime = 0
        } else {
            c.sliderTime = c.sliderTime + d
        }
    },
    sliderRender: function () {
        var c = this;
        c.sliderSetUlWidth();
        c.sliderHTML();
        c.sliderSetLiWidth();
        setInterval(function () {
            c.sliderRun(1000);
        }, 1000);
        $("#slider").on("touchstart", function (d) {
            c.sliderTouchXY.startX = d.touches[0].clientX;
            c.sliderTouchXY.startY = d.touches[0].clientY;
            c.sliderTouchXY.initX = c.sliderTouchXY.startX;
            c.sliderTouchXY.finishX = 0
        });
        $("#slider").on("touchmove", function (f) {
            var i = f.touches;
            var e = f.touches[0].clientX;
            var d = f.touches[0].clientY;
            if (Math.abs(d - c.sliderTouchXY.startY) > Math.abs(e - c.sliderTouchXY.startX)) {
                return
            }
            f.preventDefault();
            c.sliderTime = 0;
            c.sliderTouchXY.finishX = e;
            var h = Math.abs(e - c.sliderTouchXY.startX);
            var g = $("#slider_touch").css("left").replace("px", "");
            if (c.sliderTouchXY.startX > e) {
                $("#slider_touch").css("left", (parseInt(g) - h) + "px")
            } else {
                $("#slider_touch").css("left", (parseInt(g) + h) + "px")
            }
            c.sliderTouchXY.startX = e
        });
        $("#slider").on("touchend", function (d) {
            if (c.sliderTouchXY.finishX != 0) {
                if (c.sliderTouchXY.initX > c.sliderTouchXY.finishX) {
                    c.sliderImg()
                } else {
                    if (c.sliderTouchXY.initX < c.sliderTouchXY.finishX) {
                        c.sliderImgIndex = c.sliderImgIndex - 2;
                        c.sliderImg()
                    }
                }
                c.sliderTouchXY.initX = 0;
                c.sliderTouchXY.finishX = 0;
            }
        })
    },
    /*{sliderImgList:[{url:'http://sale.jd.com/m/act/Y32sO7XWQVuTc.html?resourceType=listActivity&resourceValue=0_0_1_57026&resourceType=index_activity&resourceValue=&client=m&sid=16773a290096db3a3c0ab31db92cf4fc',title:'无乐不作嗨翻周末9.9元起',img:'http://img30.360buyimg.com/mobilecms/s480x180_jfs/t475/137/1125264323/286929/922afd45/54afac73N523a28e7.jpg'}*/
    resizeFunction: function () {
        var c = this;
        $(window).on("resize", function () {
            c.sliderSetUlWidth();
            c.sliderImg();
        })
    },
    run: function () {

        var c = this;
        if (c.sliderImgList && c.sliderImgList.length > 1) {
            c.sliderRender()
        }
        c.resizeFunction();
    }
});

