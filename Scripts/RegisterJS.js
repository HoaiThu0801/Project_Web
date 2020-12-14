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
        $('#showerror').html('Mật khẩu không trùng khớp');
        $('#showerror').css('color', 'red');
        $('#showerror').css('font-weight', 'bold');
        return false;
    } else {
        $('#showerror').html('');
        return true
    }
});

//Validation
$('#Username').keyup(function () {
    var username = $('#Username').val();
    if (!(/^(([A-za-z0-9]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+)){8,20}$/).test(username))
    {
        $('#Username').focus();
        $('#Username').css('background', 'yellow')
        $('#showError_Username').html('Tên của bạn phải chứa chữ cái viết thường hoặc hoa hoặc có chữ số, VD: Huutuong123');
        $('#showError_Username').css('color', 'red');
        $('#showError_Username').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Username').css('background', 'white')
        $('#showError_Username').html('');
        return true;
    }
});

$('#Password').keyup(function () {
    var password = $('#Password').val();
    if (!(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,30}$/).test(password)) {
        $('#Password').css('background', 'yellow')
        $('#showError_Password').html('Mật khẩu phải chứa chữ in hoa, số hoặc ký tự đặc biệt, chứa ít nhất 8 kí tự');
        $('#showError_Password').css('color', 'red');
        $('#showError_Password').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Password').css('background', 'white')
        $('#showError_Password').html('');
        return true;
    }

});

$('#Email').keyup(function () {
    var email = $('#Email').val();
    if (!(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/).test(email)) {
        $('#Email').css('background', 'yellow')
        $('#showError_Email').html('Hãy nhập email hợp lệ, VD: huutuong@gmail.com');
        $('#showError_Email').css('color', 'red');
        $('#showError_Email').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Email').css('background', 'white')
        $('#showError_Email').html('');
        return true;
    }

});

$('#Fullname').click(function () {
    var fullname = $('#Fullname').val();
    if (fullname.length <= 0) {
        $('#Fullname').css('background', 'yellow')
        $('#showError_Fullname').html('Họ tên không được trống!');
        $('#showError_Fullname').css('color', 'red');
        $('#showError_Fullname').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Fullname').css('background', 'white')
        $('#showError_Fullname').html('');
        return true;
    }
});

$('#Fullname').keyup(function () {
    var fullname = $('#Fullname').val();
    if (fullname.length <= 0) {
        $('#Fullname').css('background', 'yellow')
        $('#showError_Fullname').html('Họ tên không được trống!');
        $('#showError_Fullname').css('color', 'red');
        $('#showError_Fullname').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Fullname').css('background', 'white')
        $('#showError_Fullname').html('');
        return true;
    }

});

$('#Address').click(function () {
    var address = $('#Address').val();
    if (address.length <= 0) {
        $('#Address').css('background', 'yellow')
        $('#showError_Address').html('Địa chỉ không được trống!');
        $('#showError_Address').css('color', 'red');
        $('#showError_Address').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Address').css('background', 'white')
        $('#showError_Address').html('');
        return true;
    }
});

$('#Address').keyup(function () {
    var address = $('#Address').val();
    if (address.length <= 0) {
        $('#Address').css('background', 'yellow')
        $('#showError_Address').html('Địa chỉ không được trống!');
        $('#showError_Address').css('color', 'red');
        $('#showError_Address').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Address').css('background', 'white')
        $('#showError_Address').html('');
        return true;
    }
});
$('#PhoneNumber').keyup(function () {
    var phonenumber = $('#PhoneNumber').val();
    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    if (!(vnf_regex.test(phonenumber))) {
        $('#PhoneNumber').css('background', 'yellow')
        $('#showError_PhoneNumber').html('Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx!');
        $('#showError_PhoneNumber').css('color', 'red');
        $('#showError_PhoneNumber').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#PhoneNumber').css('background', 'white')
        $('#showError_PhoneNumber').html('');
        return true;
    }
});

$(document).ready(function () {
    $("#signup-form").submit(function (event) {
        event.preventDefault();
        var url = $(this).attr("action");
        var formdata = $(this).serialize();
        $.ajax({
            type: 'post',
            url: url,
            data: formdata,
            success: function (res) {
                if (res == "true") {
                    alert("Đăng ký thành công");
                    $(window).attr('location', '../SignIn/SignIn');
                }
                if (res == "false") {
                    alert("Đăng ký thất bại, xin bạn vui lòng thử lại lần nữa");
                    (window).attr('location', '../Register/Register');
                }
                if (res == "user") {
                    alert("Tên username bị trùng, xin vui lòng sử dụng tên khác");
                    $(window).attr('location', '../Register/Register');
                }
                if (res == "email") {
                    alert("Email đã được sử dụng cho tài khoản khác, xin vui lòng chọn email khác");
                    window.onbeforeunload;
                    $('#showError_Email').html('Email đã được đăng kí vui lòng sử dụng email khác');
                    $('#showError_Email').css('color', 'red');
                    $('#showError_Email').css('font-weight', 'bold');
                    $('#Email').css('background', 'yellow')
                }
            },

        })
    });
});

//
$(document).ready(function () {
    $("#ProvinceSelect").change(function () {
        var provincename = $("#ProvinceSelect").val();
        $.ajax({
            type: "get",
            url: "/Register/LoadDistrict",
            data: {
                ProvinceName: provincename
            },
            success: function (res) {
                $('#DistrictSelect').html("");
                $("#DistrictSelect").append(("<option disabled selected>" + "Chọn Quận/Huyện" + "</option>"));
                $.each(res, function (key, item) {
                    $("#DistrictSelect").append("<option>" + item + "</option>");
                });
            }
        })
    })
})
$(document).ready(function () {
    $("#DistrictSelect").change(function () {
        var temp = $("#DistrictSelect").val();
        var spaceIndex = temp.search(" ");
        var districtname = temp.slice(spaceIndex + 1, temp.length);
        $.ajax({
            type: "get",
            url: "/Register/LoadWard",
            data: {
                DistrictName: districtname
            },
            success: function (res) {
                $('#WardSelect').html("");
                $("#WardSelect").append(("<option disabled selected>" + "Chọn Phường/Xã" + "</option>"));
                $.each(res, function (key, item) {
                    $("#WardSelect").append("<option>" + item + "</option>");
                });
            }
        })
    })
})
