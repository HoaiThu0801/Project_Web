//Animate when reload Home Page
$(function () {
    //waitToShow = new TimelineMax({onComplete: goto});
    //waitToShow.from($('.load'), 0.75, { scale: 2, opacity: 0 })
    //          .to($('.load'), 0.25, { scale: 2, opacity: 1 })
    //          .to($('.load'), 0.5, { scale: 2, opacity: 0 });

    //function goto() {
    //    animateImage.play();
        $('.khoiload').css("opaity", 0);
        $('.khoiload').css("visibility", "hidden");
    //}
    //animateImage = new TimelineMax({ paused: true });
    //animateImage.from($('.header'), 0.4, { opacity: 0 })
    //    .from($('.wrapper'), 0.4, { opacity: 0 })
    //    .from($('.body-menu'), 0.4, { opacity: 0 })
    //    .staggerFrom($('.contentProducts'), 0.7, { width: 0, height: 0, opacity: 0 }, 0.3)
    //    .from($('.paging'), 0.4, { y: -400, opacity: 0 })
    //    .from($('#footer-animate'), 0.4, { opacity: 0 });
});

$(document).ready(function () {

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Event using ajax when click class cart-plus
    $('.cart-plus').click(function (e) {
        e.preventDefault();
        var IDDish = $(this).attr("data-IDDish");
        $.ajax({
            type: "post",
            url: "/Home/AddCart",
            dataType: "json",
            data: {
                IDDish: IDDish
            },
            success: function (res) {
                if (res.type == true) {
                    notify("Thông báo", "Bạn đã thêm món " + res.message + " vào giỏ hàng thành công", res.type);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/Index');
                    }, 2000);
                }
                if (res == "user") {
                    notify("Thông báo", "Bạn phải đăng nhập thì mới thêm được món ăn vào giỏ hàng", false);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../SignIn/SignIn');
                    }, 2000)
                }
            }
        })
    });
});
