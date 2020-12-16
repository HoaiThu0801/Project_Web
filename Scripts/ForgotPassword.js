
$('#re_password').keyup(function () {
    var password = $('#Password').val();
    var re_password = $('#re_password').val();
    if (re_password != password) {
        $('#showError').html('Mật khẩu không trùng khớp');
        $('#showError').css('color', 'black');
        return false;
    } else {
        $('#showError').html('');
        return true
    }
});
$('#Username').keyup(function () {
    $('#IsNotUsername').html('');
    var username = $('#Username').val();
    if (username == '') {
        $('#IsNotUsername').html('Username không được trống');
        $('#IsNotUserName').css('color', 'black');
        return false;
    }
    else {
        $('#IsNotUsername').html('');
        return true;
    }
});
$('#Password').keyup(function () {
    $('#IsNotPassword').html('');
    var username = $('#Password').val();
    if (username == '') {
        $('#IsNotPassword').html('Mật khẩu không được trống');
        $('#IsNotPassword').css('color', 'black');
        return false;
    }
    else {
        $('#IsNotPassword').html('');
        return true;
    }
});

//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})