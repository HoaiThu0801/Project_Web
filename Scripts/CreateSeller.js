(function ($) {
    $(".toggle-password").click(function () {

        $(this).toggleClass("zmdi-eye zmdi-eye-off");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
})(jQuery);

$('#re_password').keyup(function () {
    var password = $('#Password').val();
    var re_password = $('#re_password').val();
    if (re_password != password) {
        $('#showError').html('Mật khẩu không trùng khớp');
        $('#showError').css('color', 'red');
        $('#showError').css('font-weight', 'bold');
        return false;
    } else {
        $('#showError').html('');
        return true
    }
});

$(document).ready(function () {
    $("#signup-seller-form").submit(function (event) {
        var url = $(this).attr("action");
        var formdata = $(this).serialize();
        $.ajax({
            type: "post",
            url: url,
            data: formdata,
            success: function (res) {
                if (res == "true") {
                    alert("Đăng ký thành công");
                    $(window).attr('location', '../Home/Index');
                }       
            }
        })
    });
});
