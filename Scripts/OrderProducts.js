$('#update-button-information').click(function () {
    $(window).attr('location', '../OrderProducts/Shipping');
});
$('#update-button-products').click(function () {
    $(window).attr('location', '../Home/ShoppingCart');
});

$(document).ready(function () {
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
})

//Order the item
$(document).ready(function () {
    $('#Order-item').click(function () {
        var address_user = $('#address_user').text();
        if (address_user == "") {
            alert("Bạn chưa cài địa chỉ mặc định, hãy cài địa chỉ mặc định");
            $(window).attr('location', '../OrderProducts/Shipping');
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
                        alert("Bạn đã đặt hàng thành công. Cảm ơn bạn đã đồng hàng cùng cửa hàng MST");
                        $(window).attr('location', '../Home/Index');
                    }
                    else {
                        alert("Đặt hàng không thành công. Chúng tôi xin lỗi vì sự bất tiện này");
                        $(window).attr('location', '../OrderProducts/OrderProducts');
                    }
                }
            })
        }
    });
})

//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})