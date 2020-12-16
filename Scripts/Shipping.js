$(document).ready(function () {
    $("#ProvinceSelect").change(function () {
        var provincename = $("#ProvinceSelect").val();
        $.ajax({
            type: "get",
            url: "/OrderProducts/LoadDistrict",
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
            url: "/OrderProducts/LoadWard",
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
$(document).ready(function () {
    $("#address-form").submit(function (event) {
        event.preventDefault();
        var url = $(this).attr("action");
        var form = $(this).serialize();
        $.ajax({
            type: 'post',
            url: url,
            data: form,
            success: function (res) {
                if (res == "true") {
                    alert("Thêm địa chỉ mới thành công");
                    $(window).attr('location', '../OrderProducts/Shipping');
                }
            },
        })
    });
})

$(document).ready(function () {
    var fullname = $('#Fullname').val();
    var phone = $('#PhoneNumber').val();
    var street = $('#Street').val();
    $('#update-button').click(function () {
        $('#form-address').css('opacity', '1');
        $('#form-address').css('visibility', 'visible');
        $('#form-address').css('height', 'unset');
        $('#form-address').css('width', '100%');
        $('html, body').animate({ scrollTop: $('#form-address').offset().top }, 'slow');
        $('#Fullname').val(fullname);
        $('#PhoneNumber').val(phone);
        $('#Street').val(street);
    });
    $('#openForm').click(function () {
        $('#form-address').css('opacity', '1');
        $('#form-address').css('visibility', 'visible');
        $('#form-address').css('height', 'unset');
        $('#form-address').css('width', '100%');
        $('html, body').animate({ scrollTop: $('#form-address').offset().top }, 'slow');
        $('#Fullname').val('');
        $('#PhoneNumber').val('');
        $('#Street').val('');
    });
    $('#Cancel').click(function () {
        $('#form-address').css('opacity', '0');
        $('#form-address').css('visibility', 'hidden');
        $('#form-address').css('height', '0');
        $('#form-address').css('width', '0');
        $('#address-form').trigger("reset");
    });
});

//Validation
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
$('#Street').click(function () {
    var address = $('#Street').val();
    if (address.length <= 0) {
        $('#Street').css('background', 'yellow')
        $('#showError_Address').html('Địa chỉ không được trống!');
        $('#showError_Address').css('color', 'red');
        $('#showError_Address').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Street').css('background', 'white')
        $('#showError_Address').html('');
        return true;
    }
});
$('#Street').keyup(function () {
    var address = $('#Street').val();
    if (address.length <= 0) {
        $('#Street').css('background', 'yellow')
        $('#showError_Address').html('Địa chỉ không được trống!');
        $('#showError_Address').css('color', 'red');
        $('#showError_Address').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#Street').css('background', 'white')
        $('#showError_Address').html('');
        return true;
    }
});

//Click button đặt làm mặc định
$(document).ready(function () {
    $('.default-button').click(function (e) {
        $(this).css('display', 'none');
        var IDaddress = $(this).attr("data-IDAddress");
        var IDuser = $(this).attr("data-IDUser");
        alert(IDaddress, IDuser);
        $.ajax({
            type: 'post',
            url: "/OrderProducts/DefaultShipping",
            data: {
                IDAddress: IDaddress,
                IDUser: IDuser
            },
            success: function (res) {
                if (res == "true") {
                    $(window).attr('location', '../OrderProducts/Shipping');
                }
                else {
                    alert("Có lỗi xảy ra trong quá trình xử lý, bạn vui lòng thử lại sau");
                    $(window).attr('location', '../OrderProducts/Shipping');
                }
            }
        })
    });
});