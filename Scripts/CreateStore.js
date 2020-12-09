
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
    var address = $('#StoreName').val();
    if (address.length <= 0) {
        $('#StoreName').css('background', 'yellow')
        $('#showFail_SN').html('Tên cửa hàng không được trống!');
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

//Show store name and choose dish when change select, load store accroding to location
$(document).ready(function () {
    $('#LocationSelect').change(function () {
        var location = $('#LocationSelect').val();
        $.ajax({
            type: "get",
            url: "/Administrator_Setting/LoadStoreName",
            data: {
                Location: location
            },
            success: function (response) {          
                $('#StoreNameSelect').html("");            
                $.each(response, function (key, item) {
                    $("#StoreNameSelect").append(("<option disabled selected>" + "StoreName" + "</option>"));
                    $("#StoreNameSelect").append("<option>" + item.StoreName + "</option>");
                });
            }
        })
    });
});
//Click dish, add dish into menu_store
$(document).ready(function () {
    var storename;
    var location;
    $('#LocationSelect').change(function () {
        location = $('#LocationSelect').val();
        $('#StoreNameList').css('opacity', '1');
        $('#StoreNameList').css('visibility', 'visible');
        $('#StoreNameList').css('height', 'unset');
    });
    $('#StoreNameSelect').change(function () {
        storename = $('#StoreNameSelect').val();
        $('#DishNameList').css('opacity', '1');
        $('#DishNameList').css('visibility', 'visible');
        $('#DishNameList').css('height', 'unset');
        $.ajax({
            type: "get",
            url: "/Administrator_Setting/LoadDish_no_StoreName",
            data: {
                StoreName: storename
            },
            success: function (res) {
                $('#listdish').html("");
                var html = '';
                $.each(res, function (key, item) {
                    html += '<a href="#" class="dishname-choose" id="DishName_a"><i class="fa fa-circle-o"><input hidden value="@s.DishName" id="DishName" name="DishName" />' + item + '</i></a>';
                    $('#listdish').html(html);
                    $('.fa').click(function () {
                        var t = $(this).addClass('fa-circle');
                        let dishname = t.text();
                        $.ajax({
                            type: "post",
                            url: "/Administrator_Setting/AddDish",
                            data: {
                                DishName: dishname,
                                StoreName: storename,
                                Location: location
                            },
                            success: function (res) {
                                if (res == "False") {
                                    $('.fa').removeClass('fa-circle');
                                    alert("Món ăn đã có sẵn");
                                }
                            }
                        });
                    });
                })
            }
        })
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
                else {
                    $("#showFail_SN").html("");
                    $(".form-input").val('');
                }
                if (res == "PhoneNumber") {
                    $("#showFail_PN").html("Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx");
                    $(".form-input").val('');
                }
                else {
                    $("#showFail_PN").html("");
                    $(".form-input").val('');
                }
                if (res == "Email") {
                    $("#showFail_Email").html("Hãy nhập email hợp lệ, VD: huutuong@gmail.com");
                    $(".form-input").val('');
                }
                else {
                    $("#showFail_Email").html("");
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
//$(document).ready(function () {
//    $.ajax({
//        type: "get",
//        url: "/Administrator_Setting/LoadStore",
//        success: function (response) {
//            $("tbody#ListData").html("");
//            $.each(response, function (key, item) {
//                $("#ListData").append("<tr><td>" + item.StoreName + "</td><td>" + item.Location + "</td><td>" + item.PhoneNumber + "</td><td>" + item.Email + "</td></tr>");
//            });
//        },
//    });
//});

//$(document).ready(function () {
//    $.ajax({
//        type: "get",
//        url: "/Administrator_Setting/LoadStaff",
//        success: function (res) {
//            $("tbody#listStaff").html("");
//            $.each(res, function (key, item) {
//                var tr = $("<tr/>");
//                $("<td/>").html(item.StoreName).appendTo(tr);
//                $("<td/>").html(item.Location).appendTo(tr);
//                $("<td/>").html(item.FullName).appendTo(tr);
//                $("<td/>").html(item.UserName).appendTo(tr);
//                tr.appendTo("tbody#listStaff");
//            });
//        },
//    });
//});

//Click icon X reload page
$(document).ready(function () {
    $(".remove-icon").click(function () {
        setTimeout(function () {
            location.reload();
        }, 1000)
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
$(function () {
    $('#myPagerStaff').on('click', 'a', function () {
        $.ajax({
            url: this.href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#Staff').html(result);
            },
        });
        return false;
    });
});
$(function () {
    $('#myPagerDish').on('click', 'a', function () {
        $.ajax({
            url: this.href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#Dish').html(result);
            },
        });
        return false;
    });
});