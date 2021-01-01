var srcImage;
function readURL(event) {
    if (event.target.files.length > 0) {
        let src = URL.createObjectURL(event.target.files[0]);
        let show = document.getElementById("showImage");
        show.src = src;
        srcImage = event.target.files[0].name;
    }
}

$(document).ready(function () {
    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

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

    //Load District when select Province
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

    //Load Ward when select District
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
    //Show success when change information account
    $('#InformationForm').submit(function (event) {
        event.preventDefault();
        //Declare variables
        var Fullname = $('#Fullname').val();
        var gender = $('#Gender').val();
        var DateofBirth = $('#DateofBirth').val();
        var IdentityCard = $('#IdentityCard').val();
        var Province = $('#ProvinceSelect').val();
        var District = $('#DistrictSelect').val();
        var Ward = $('#WardSelect').val();
        var Street = $('#Address').val();
        var PhoneNumber = $('#PhoneNumber').val();
        var fileUpload = $('#Image').get(0);
        var files = fileUpload.files;
        var url = $(this).attr("action");

        var formdata = new FormData();
        formdata.append("FullName", Fullname);
        formdata.append("Gender", gender);
        formdata.append("DateofBirth", DateofBirth);
        formdata.append("IdentityCard", IdentityCard);
        formdata.append("Province", Province);
        formdata.append("District", District);
        formdata.append("Ward", Ward);
        formdata.append("Street", Street);
        formdata.append("PhoneNumber", PhoneNumber);
        formdata.append("ImageAvatarUser", files[0])
        
        $.ajax({
            type: 'post',
            url: url,
            dataType: "json",
            processData: false,
            enctype: 'multipart/form-data',
            data: formdata,
            contentType: false,
            success: function (res) {
                if (res == "true") {
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    notify("Thông báo", "Cập nhật thông tin thành công", true);
                    setTimeout(function () {
                        $(window).attr('location', '../Home/InformationAccount');
                    }, 2000);
                }
            },
        });
    });
});