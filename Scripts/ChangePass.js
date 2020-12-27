
$(document).ready(function () {

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Validation
    $('#New_Password').keyup(function () {
        var password = $('#New_Password').val();
        if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$/).test(password)) {
            $('#New_Password').css('background', 'yellow')
            $('#showError_NPW').html('Mật khẩu phải chứa chữ in hoa, số hoặc ký tự đặc biệt, chứa ít nhất 8 kí tự');
            $('#showError_NPW').css('color', 'red');
            $('#showError_NPW').css('font-weight', 'bold');
            return false;
        }
        else {
            $('#New_Password').css('background', 'white')
            $('#showError_NPW').html('');
            return true;
        }
    });

    $('#Re_Password').keyup(function () {
        var password = $('#New_Password').val();
        var re_password = $('#Re_Password').val();
        if (re_password != password) {
            $('#showError_RPW').html('Mật khẩu không trùng khớp');
            $('#showError_RPW').css('color', 'red');
            $('#showError_RPW').css('font-weight', 'bold');
            return false;
        } else {
            $('#showError_RPW').html('');
            return true
        }
    });

    //Focus to password when load page
    $('#Password').focus();

    //Event submit using ajax when submite element <form> has id InformationForm
    $('#InformationForm').submit(function (event) {
        event.preventDefault();

        //Declare variables
        var old_Password = $('#Password').val();
        var new_Password = $('#New_Password').val();
        var username = $('#Username').val();
        var re_password = $('#Re_Password').val();

        //Validation password
        if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$/).test(old_Password)) {
            old_Password = "error";
        }
        if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$/).test(new_Password)) {
            new_Password = "error";
        }

        $.ajax({
            type: 'post',
            url: '/Home/ChangePass',
            data:
            {
                Username: username,
                OldPassword: old_Password,
                NewPassword: new_Password,
                Re_Password: re_password,
            },
            success: function (res) {
                if (res == "false") {
                    $('#AlertBoxforJS').css('top', $('#Password').offset().top - 100);
                    notify("Xảy ra lỗi", 'Mật khẩu cũ không chính xác', false);
                    $("html, body").animate({ scrollTop: $('#Password').offset().top - 100 }, "slow");
                    $('#Password').focus();
                }
                if (res == "true") {
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    notify("Thông báo", "Đổi mật khẩu thành công", true);
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ChangePass');
                    }, 1000)
                }
                if (res == "IsEquals") {
                    $('#AlertBoxforJS').css('top', $('#New_Password').offset().top - 100);
                    notify("Xảy ra lỗi", "Mật khẩu mới không được trùng mật khẩu cũ", false);
                    $("html, body").animate({ scrollTop: $('#New_Password').offset().top - 100 }, "slow");
                    $("#New_Password").focus();
                }
                if (res == "NotLike") {
                    $('#AlertBoxforJS').css('top', $('#Re_Password').offset().top - 100);
                    notify("Xảy ra lỗi", "Mật khẩu nhập lại sai", false);
                    $("html, body").animate({ scrollTop: $('#Re_Password').offset().top - 100 }, "slow");
                    $("#Re_Password").focus();
                }
                if (res == "NotNewPassword") {
                    $('#AlertBoxforJS').css('top', $('#New_Password').offset().top - 100);
                    notify("Xảy ra lỗi", "Mật khẩu mới phải chứa chữ in hoa, số hoặc ký tự đặc biệt", false);
                    $("html, body").animate({ scrollTop: $('#New_Password').offset().top - 100 }, "slow");
                }
                if (res == "NotOldPassword") {
                    $('#AlertBoxforJS').css('top', $('#Password').offset().top - 100);
                    notify("Xảy ra lỗi", "Mật khẩu cũ phải chứa chữ in hoa, số hoặc ký tự đặc biệt", false);
                    $("html, body").animate({ scrollTop: $('#Password').offset().top - 100 }, "slow");
                }
            },
        });
    });
});