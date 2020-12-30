//Order the item
$(document).ready(function () {

    //Event redirect when click butotn
    $('#update-button-information').click(function () {
        $(window).attr('location', '../OrderProducts/Shipping');
    });
    $('#update-button-products').click(function () {
        $(window).attr('location', '../Home/ShoppingCart');
    });

    //CheckBox event
    var flaqCheckBox = true;
    $('#CheckBox').click(function () {
        if (flaqCheckBox == true) {
            $(this).addClass('fa-check-square');
            $('.fa-check-square').css('color', 'darkblue');
            flaqCheckBox = false;
        }
        else {
            $(this).removeClass('fa-check-square');
            $('.fa-square-o').css('color', 'black');
            flaqCheckBox = true
        }
    });

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    $('#Order-item').click(function (e) {
        e.preventDefault();
        var address_user = $('#address_user').text();
        if (address_user == "") {
            notify("Xảy ra lỗi", "Bạn chưa cài địa chỉ mặc định, hãy cài địa chỉ mặc định", false);
            setTimeout(function () {
                $(window).attr('location', '../OrderProducts/Shipping');
            });
        }
        else {
            $.ajax({
                type: "post",
                data: {
                    AddressOrder: address_user
                },
                url: "/OrderProducts/OrderProducts",
                success: function (res) {
                    if (res == "true") {
                        $(window).attr('location', '../Home/Index');
                    }
                    else {
                        notify("Xảy ra lỗi", "Đặt hàng không thành công. Chúng tôi xin lỗi vì sự bất tiện này", false);
                        setTimeout(function () {
                            $(window).attr('location', '../OrderProducts/OrderProducts');
                        }, 2000);
                    }
                }
            })
        }
    });
})

