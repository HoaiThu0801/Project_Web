//Mở đóng popupform
var flaqStore = true;
var flaqStaff = true;
var flaqDish = true;
function openPopupStoreForm() {

    if (flaqStore == false) {
        document.getElementById("signup-content-store").style.display = "none";
        flaqStore = true;
    }
    else {
        document.getElementById("signup-content-store").style.display = "block";
        flaqStore = false;
    }
}
function openPopupStaffForm() {

    if (flaqStaff == false) {
        document.getElementById("signup-content-staff").style.display = "none";
        flaqStaff = true;
    }
    else {
        document.getElementById("signup-content-staff").style.display = "block";
        flaqStaff = false;
    }
}
function openPopupDishForm() {

    if (flaqDish == false) {
        document.getElementById("signup-content-dish").style.display = "none";
        flaqDish = true;
    }
    else {
        document.getElementById("signup-content-dish").style.display = "block";
        flaqDish = false;
    }
}
function closePopupForm() {
    if (flaqStore == false) {
        document.getElementById("signup-content-store").style.display = "none";
        flaqStore = true;
    }
    if (flaqStaff == false) {
        document.getElementById("signup-content-staff").style.display = "none";
        flaqStaff = true;
    }
    if (flaqDish == false) {
        document.getElementById("signup-content-dish").style.display = "none";
        flaqDish = true;
    }
}

//Save data without load page
$(document).ready(function () {
    $("#signup-form-store").submit(function (event) {
        event.preventDefault();
        var url = $(this).attr("action");
        var formdata = $(this).serialize();
        $.ajax({
            type: 'post',
            url: url,
            data: formdata,
            success: function (res) {
                if (res == "true") {
                    $("#showSuccess").html("Đăng ký thành công");
                    $(".form-input").val('');
                }
                if (res == "StoreName") {
                    $("#showFail_SN").html("Tên cửa hàng không được để trống!");
                    $(".form-input").val('');
                }
                if (res == "PhoneNumber") {
                    $("#showFail_PN").html("Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx");
                    $(".form-input").val('');
                }
                if (res == "Email") {
                    $("#showFail_Email").html("Hãy nhập email hợp lệ, VD: huutuong@gmail.com");
                    $(".form-input").val('');
                }
            },
        });
        return false;
    });
});

function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}

//Load Data using ajax
$(document).ready(function () {
    $.ajax({
        type: "get",
        url: "/Administrator_Setting/LoadStore",
        success: function (response) {
            $.each(response, function (key, item) {
                $("#ListData").append("<tr><td>" + item.StoreName + "</td><td>" + item.Location + "</td><td>" + item.PhoneNumber + "</td><td>" + item.Email + "</td></tr>");
            });
        },
    });
});
