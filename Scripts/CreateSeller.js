
$(document).ready(function () {

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Add eye in password
    $(".toggle-password").click(function () {

        $(this).toggleClass("zmdi-eye zmdi-eye-off");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });

    //Validation
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
    $('#Username').keyup(function () {
        var username = $('#Username').val();
        if (!(/^(([A-za-z0-9]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+)){8,20}$/).test(username)) {
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

    //Event using submit form with ajax
    $("#signup-seller-form").submit(function (event) {
        event.preventDefault();

        //validate PhoneNumber
        var phonenumber = $('#PhoneNumber').val();
        var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        if (!(vnf_regex.test(phonenumber))) {
            $('#AlertBoxforJS').css('top', $('#PhoneNumber').offset().top - 100);
            notify("Xảy ra lỗi", "Hãy nhập số điện thoại hợp lệ, VD: 03xxxxxxxx", false);
            $("html, body").animate({ scrollTop: $('#PhoneNumber').offset().top - 100 }, "slow");
            $('#PhoneNumber').focus();
        }
        //end

        var url = $(this).attr("action");
        var formdata = $(this).serialize();
        $.ajax({
            type: "post",
            url: url,
            data: formdata,
            success: function (res) {
                if (res == "true") {
                    notify("Thông báo", "Đăng ký thành công", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/Index');
                    }, 2000)
                }
                if (res == "false") {
                    notify("Xảy ra lỗi", "Đăng ký thất bại, xin bạn vui lòng thử lại lần nữa", false);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                if (res == "user") {
                    $('#AlertBoxforJS').css('top', $('#Username').offset().top - 100);
                    notify("Xảy ra lỗi", "Tên username bị trùng, xin vui lòng sử dụng tên khác", false);
                    $("html, body").animate({ scrollTop: $('#Username').offset().top - 100 }, "slow");
                    $('#Username').focus();
                }
                if (res == "password") {
                    $('#AlertBoxforJS').css('top', $('#re_password').offset().top - 100);
                    notify("Xảy ra lỗi", "Mật khẩu nhập lại sai", false);
                    $("html, body").animate({ scrollTop: $('#re_password').offset().top - 100 }, "slow");
                    $('#re_password').focus();
                }
                if (res == "email") {
                    $('#AlertBoxforJS').css('top', $('#Email').offset().top - 100);
                    notify("Xảy ra lỗi", "Email đã được sử dụng cho tài khoản khác, xin vui lòng chọn email khác", false);
                    $("html, body").animate({ scrollTop: $('#Email').offset().top - 100 }, "slow");
                    $('#Email').focus();
                }
                if (res == "province") {
                    $('#AlertBoxforJS').css('top', $('#ProvinceSelect').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn thành phố", false);
                    $("html, body").animate({ scrollTop: $('#ProvinceSelect').offset().top - 100 }, "slow");
                    $('#ProvinceSelect').focus();
                }
                if (res == "district") {
                    $('#AlertBoxforJS').css('top', $('#DistrictSelect').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn quận huyện", false);
                    $("html, body").animate({ scrollTop: $('#DistrictSelect').offset().top - 100 }, "slow");
                    $('#DistrictSelect').focus();
                }
                if (res == "role") {
                    $('#AlertBoxforJS').css('top', $('#Role').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn chức vụ", false);
                    $("html, body").animate({ scrollTop: $('#Role').offset().top - 100 }, "slow");
                    $('#Role').focus();
                }
            }
        })
    });

    //Event when select Province
    var district = $('#District');
    $("#ProvinceSelect").change(function () {
        district.css("height", "unset");
        district.css("opacity", 1);
        district.css("visibility", "visible");
        district.css("margin-bottom", "20px");
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
    });

    //Event when select District
    var ward = $("#Ward");
    $("#DistrictSelect").change(function () {
        ward.css("height", "unset");
        ward.css("opacity", 1);
        ward.css("visibility", "visible");
        ward.css("margin-bottom", "20px");
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
    });
});