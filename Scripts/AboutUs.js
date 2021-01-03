$(document).ready(function () {
    window.onscroll = function () {
        scrollMenu();
        $('#History').css("margin-top", "50px");
    };
    var navbar = document.getElementById('scrollMenu');
    var sticky = navbar.offsetTop;
    function scrollMenu() {
        if (window.pageYOffset >= sticky) {
            navbar.classList.add('sticky');
        }
        else {
            navbar.classList.remove('sticky');
        }
    }

    $('#history-menu').click(function () {
        $("html, body").animate({ scrollTop: $('#History').offset().top - 140}, "slow");
    });
    $('#update-menu').click(function () {
        $("html, body").animate({ scrollTop: $('#Update').offset().top - 180}, "slow");
    });
    $('#thanks-menu').click(function () {
        $("html, body").animate({ scrollTop: $('#Thanks').offset().top - 180 }, "slow");
    });
    $('#aboutTeam-menu').click(function () {
        $("html, body").animate({ scrollTop: $('#AboutTeam').offset().top - 70 }, "slow");
    });
    $('#goToTop').click(function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });
});