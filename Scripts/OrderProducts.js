$('#update-button-information').click(function () {
    $(window).attr('location', '../OrderProducts/Shipping');
});
$('#update-button-products').click(function () {
    $(window).attr('location', '../Home/ShoppingCart');
});

$(document).ready(function () {
    var flaqCheckBox = true;
    $('#CheckBox').click(function () {
        if (flaqCheckBox == true) {
            $(this).addClass('fa-check-square');
            $('.fa-check-square').css('color', 'darkblue');
            flaqCheckBox = false;
        }
        else {
            $(this).removeClass('fa-check-square');
            $('.fa-square-o').css('color', 'black');
            flaqCheckBox = true
        }
    });
})
