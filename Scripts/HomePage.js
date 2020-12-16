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