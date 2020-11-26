$(document).ready(function () {
    var count = 0;
    $('#cart-plus').click(function () {
        count++;
        $('#number-shopping').html(count);
    });
});
