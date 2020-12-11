﻿$(document).ready(function () {
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