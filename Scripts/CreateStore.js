//Mở đóng popupform
var flaqStore = true;
var flaqStaff = true;
var flaqDish = true;
var signUpStore = document.getElementById("signup-content-store");
var signUpStaff = document.getElementById("signup-content-staff");
var signUpDish = document.getElementById("signup-content-dish");
function openPopupStoreForm() {

    if (flaqStore == false) {
        signUpStore.style.opacity = 0;
        signUpStore.style.visibility = "hidden";
        flaqStore = true;
    }
    else {
        signUpStore.style.opacity = 1;
        signUpStore.style.visibility = "visible";
        flaqStore = false;
    }
}
function openPopupStaffForm() {

    if (flaqStaff == false) {
        signUpStaff.style.opacity = 0;
        signUpStaff.style.visibility = "hidden";
        flaqStaff = true;
    }
    else {
        signUpStaff.style.opacity = 1;
        signUpStaff.style.visibility = "visible";
        flaqStaff = false;
    }
}
function openPopupDishForm() {

    if (flaqDish == false) {
        signUpDish.style.opacity = 0;
        signUpDish.style.visibility = "hidden";
        flaqDish = true;
    }
    else {
        signUpDish.style.opacity = 1;
        signUpDish.style.visibility = "visible";
        flaqDish = false;
    }
}
function closePopupForm() {
    if (flaqStore == false) {
        signUpStore.style.opacity = 0;
        signUpStore.style.visibility = "hidden";
        flaqStore = true;
    }
    if (flaqStaff == false) {
        signUpStaff.style.opacity = 0;
        signUpStaff.style.visibility = "hidden";
        flaqStaff = true;
    }
    if (flaqDish == false) {
        signUpDish.style.opacity = 0;
        signUpDish.style.visibility = "hidden";
        flaqDish = true;
    }
}

//function openForm when click tab
function openForm(evt, formName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(formName).style.display = "block";
    evt.currentTarget.className += " active";
}
//Click icon X on popup page reload page
$(".remove-icon").click(function () {
    setTimeout(function () {
        location.reload();
    }, 1000);
});

$(document).ready(function () {
    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Event using Ajax for id myPager in _TabStore
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

    //Event using Ajax for id myPagerStaff in _TabStaff
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

    //Event using Ajax for id myPagerDish in _TabDish
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

    //Validation
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
            $('#StoreName').css('background', 'white')
            $('#showFail_SN').html('');
            return true;
        }
    });

    //Submit form store using ajax
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
                    notify("Thông báo", "Đăng ký thành công", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    $(".form-input").val('');
                    $(".form-ipnut").css('background', 'white');
                    $("#LocationSelect").load(" #Location");
                }
                if (res == "PhoneNumber") {
                    $('#AlertBoxforJS').css('top', $('#PhoneNumber').offset().top - 100);
                    notify("Xảy ra lỗi", "Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx", false);
                    $("html, body").animate({ scrollTop: $('#PhoneNumber').offset().top - 100 }, "slow");
                    $('#PhoneNumber').focus();
                }
                if (res == "IsEmail") {
                    $('#AlertBoxforJS').css('top', $('#Email').offset().top - 100);
                    notify("Xảy ra lỗi", "Hãy nhập email hợp lệ, VD: cuahang@gmail.com", false);
                    $("html, body").animate({ scrollTop: $('#Email').offset().top - 100 }, "slow");
                    $('#Email').focus();
                }
                if (res == "email") {
                    $('#AlertBoxforJS').css('top', $('#Email').offset().top - 100);
                    notify("Xảy ra lỗi", "Email đã được sử dụng cho cửa hàng khác khác, xin vui lòng chọn email khác", false);
                    $("html, body").animate({ scrollTop: $('#Email').offset().top - 100 }, "slow");
                    $('#Email').focus();
                }
                if (res == "nolocation") {
                    $('#AlertBoxforJS').css('top', $('#Location').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn địa chỉ cho cửa hàng", false);
                    $("html, body").animate({ scrollTop: $('#Location').offset().top - 100 }, "slow");
                    $('#Location').focus();
                }
            },
        });
    });

    //Submit form staff using ajax
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
                    notify("Thông báo", "Đăng ký thành công", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    $("#StaffSelect").load(" #staff-dropdown");
                    $("#StoreSelect").load(" #store-dropdown");
                }
                else {
                    notify("Xảy ra lỗi", "Đăng ký thất bại", false);
                }
                if (res == "nouser") {
                    $('#AlertBoxforJS').css('top', $('#staff-dropdown').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn người bán hàng", false);
                    $("html, body").animate({ scrollTop: $('#staff-dropdown').offset().top - 100 }, "slow");
                    $('#staff-dropdown').focus();
                }
                if (res == "nostore") {
                    $('#AlertBoxforJS').css('top', $('#store-dropdown').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn cửa hàng", false);
                    $("html, body").animate({ scrollTop: $('#store-dropdown').offset().top - 100 }, "slow");
                    $('#store-dropdown').focus();
                }
            },
        });
    });

    //Show store name and choose dish when change select, load store accroding to location
    $('#LocationSelectDish').change(function () {
        var location = $('#LocationSelectDish').val();
        $('#StoreNameList').css('opacity', '1');
        $('#StoreNameList').css('visibility', 'visible');
        $('#StoreNameList').css('height', 'unset');
        $.ajax({
            type: "get",
            url: "/Administrator_Setting/LoadStoreName",
            data: {
                Location: location
            },
            success: function (response) {
                $('#StoreNameSelect').html("");
                $("#StoreNameSelect").append(("<option disabled selected>" + "StoreName" + "</option>"));
                $.each(response, function (key, item) {
                    $("#StoreNameSelect").append("<option>" + item.StoreName + "</option>");
                });
            }
        })
    });

    //Event using Ajax when click dish, add dish into menu_store
    var storename;
    var location;
    $('#LocationSelectDish').change(function () {
        location = $('#LocationSelectDish').val();
        $('#StoreNameList').css('opacity', '1');
        $('#StoreNameList').css('visibility', 'visible');
        $('#StoreNameList').css('height', 'unset');
    });
    $('#StoreNameSelect').change(function () {
        storename = $('#StoreNameSelect').val();
        $('#DishNameList').css('opacity', '1');
        $('#DishNameList').css('visibility', 'visible');
        $('#signup-background-dish').css('height', '100%');
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
                        $(this).hide();
                        $.ajax({
                            type: "post",
                            url: "/Administrator_Setting/AddDish",
                            data: {
                                DishName: dishname,
                                StoreName: storename,
                                Location: location
                            },
                            success: function (res) {
                                if (res == "false") {
                                    $('.fa').removeClass('fa-circle');
                                    notify("Xảy ra lỗi","Món ăn đã có sẵn", false);
                                }
                                if (res.type == true) {
                                    notify("Thông báo", res.message + " được thêm vào thành công", true);
                                    $("html, body").animate({ scrollTop: 0 }, "slow");
                                    $('#DishNameList').load(' '+ html);
                                }
                            }
                        });
                    });
                })
            }
        })
    });
});