
$(function () {
    waitToShow = new TimelineMax({onComplete: goto});
    waitToShow.from($('.load'), 0.75, { scale: 2, opacity: 0 })
              .to($('.load'), 0.25, { scale: 2, opacity: 1 })
              .to($('.load'), 0.5, { scale: 2, opacity: 0 });

    function goto() {
        animateImage.play();
        animateImage.paused();
    }
    animateImage = new TimelineMax({ paused: true });
    animateImage.from($('.header'), 0.4, {x: -300, opacity: 0 })
        .from($('.wrapper'), 0.4, {x: 300, opacity: 0 })
        .from($('.body-menu'), 0.4, { x: 300, opacity: 0 })
        .staggerFrom($('.contentProducts'), 0.7, { width: 0, height: 0, opacity: 0 }, 0.3)
        .from($('.paging'), 0.4, {y: -400, opacity: 0 })
});

$(document).ready(function () {
    $('.cart-plus').click(function () {
        var IDDish = $(this).attr("data-IDDish");
        $.ajax({
            type: "post",
            url: "/Home/AddCart",
            data: {
                IDDish: IDDish
            },
            success: function (res) {
                if (res == "true") {
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    $(window).attr('location', '../Home/Index');        
                }
            }
        })
    });
});
//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})