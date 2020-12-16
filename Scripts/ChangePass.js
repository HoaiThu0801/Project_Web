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

$(document).ready(function () {
    $('#Password').focus();
    $('#InformationForm').submit(function (event) {
        var old_Password = $('#Password').val();
        var new_Password = $('#New_Password').val();
        var username = $('#Username').val();
        event.preventDefault();
        $.ajax({
            type: 'post',
            url: '/Home/ChangePass',
            data:
            {
                Username: username,
                OldPassword: old_Password,
                NewPassword: new_Password,
            },
            success: function (res) {
                if (res == "false") {
                    $('#showError_OPW').html('Mật khẩu cũ không chính xác');
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ChangePass');
                    }, 100)
                }
                if (res == "true") {
                    $('#showError_RPW').html("Đổi mật khẩu thành công");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ChangePass');
                    }, 1000)
                }
                if (res == "IsEquals") {
                    setTimeout(function () {
                        $('#showError_NPW').html("Mật khẩu mới không được trùng mật khẩu cũ");
                    }, 1000);
                }
            },
        });
    });
});

//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})
