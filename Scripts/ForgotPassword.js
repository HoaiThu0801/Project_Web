$(document).ready(function () {

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Validation
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

    //Submit form using ajax
    $('#form-FP').submit(function (e) {
        e.preventDefault(); //Ngăn điều hướng trang
        var password = $('#Password').val();
        if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$/).test(password)) {
            password = "error";
        }
        var url = $(this).attr("action");
        $.ajax({
            url: url,
            type: 'post',
            data: {
                Username: $('#Username').val(),
                Password: password,
                RePassword: $('#re_password').val()
            },
            success: function (res) {
                if (res.type == true) {
                    notify(res.title, res.message, true);
                    setTimeout(function () {
                        location.reload();
                        $(window).attr('location', '../SignIn/SignIn');
                    }, 2000)
                }
                else {
                    notify(res.title, res.message, false);
                }
            }
        });
    });

});