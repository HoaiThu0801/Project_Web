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
//CreateStrore Validation
$('#StoreName').click(function () {
    alert("1");
    var address = $('#StoreName').val();
    if (address.length <= 0) {
        $('#StoreName').css('background', 'yellow')
        $('#showFail_SN').html('Địa chỉ không được trống!');
        $('#showFail_SN').css('color', 'red');
        $('#showFail_SN').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#StoreName').css('background', 'white')
        $('#showFail_SN').html('');
        return true;
    }
});
$('#StoreName').keyup(function () {
    var address = $('#StoreName').val();
    if (address.length <= 0) {
        $('#StoreName').css('background', 'yellow')
        $('#showFail_SN').html('Tên cửa hàng không được trống!');
        $('#showFail_SN').css('color', 'red');
        $('#showFail_SN').css('font-weight', 'bold');
        return false;
    }
    else {
        $('#StoreName').css('bacskground', 'white')
        $('#showFail_SN').html('');
        return true;
    }
});

$(document).ready(function () {
    $('#LocationSelect').change(function () {
        $("#StoreNameList").css("display", "block");
        $(".show-dish-list").css("display", "block");
    });
});
$(document).ready(function () {
    $('.fa').click(function () {
        var t = $(this).toggleClass('fa-circle');
    });
});

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
                    //$("#showSuccess").html("Đăng ký thành công");
                    alert("Đăng ký thành công");
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

$(document).ready(function () {
    $.ajax({
        type: "get",
        url: "/Administrator_Setting/LoadStaff",
        success: function (res) {
            $("tbody#listStaff").html("");
            $.each(res, function (key, item) {
                var tr = $("<tr/>");
                $("<td/>").html(item.StoreName).appendTo(tr);
                $("<td/>").html(item.Location).appendTo(tr);
                $("<td/>").html(item.FullName).appendTo(tr);
                $("<td/>").html(item.UserName).appendTo(tr);
                tr.appendTo("tbody#listStaff");
            });
        },
    });
});

//Click icon X reload page
$(document).ready(function () {
    $(".remove-icon").click(function () {
        setTimeout(function () {
            location.reload();
        }, 100)
    });
});

$(document).ready(function () {
    $("#signup-form-staff").submit(function (event) {
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
                }
                else {
                    alert("Đăng ký thất bại");
                }
            },
        });
        return false;
    });
});

$(function () {
    $('#myPager').on('click', 'a', function () {
        $.ajax({
            url: this.href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#Store').html(result);
            },
        });
        return false;
    });
});
