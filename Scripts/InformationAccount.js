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

//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})

//Load Ward, District
$(document).ready(function () {
    $("#ProvinceSelect").change(function () {
        var provincename = $("#ProvinceSelect").val();
        $.ajax({
            type: "get",
            url: "/Home/LoadDistrict",
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
            url: "/Home/LoadWard",
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