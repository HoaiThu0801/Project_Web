$(document).ready(function () {
    $('#detail').click(function () {
        $('#popup').css('opacity', 1);
        $('#popup').css('visibility', 'visible');
        $("html, body").animate({ scrollTop: $('.popup').offset().top - 100 }, "slow");
    });
    $('#close-popup').click(function () {
        $('#popup').css('opacity', 0);
        $('#popup').css('visibility', 'hidden');
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });
});