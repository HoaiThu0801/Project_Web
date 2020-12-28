$(document).ready(function () {

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Giảm số lượng món ăn
    $(".quantiTy-decrease").click(function (event) {
        event.preventDefault();
        var IDbilldetail = $(this).attr("data-IDBillDetail");
        var quantity = $(this).attr("data-quantity");
        quantity = Number(quantity);
        quantity = quantity - 1;
        $('.changQuantity').html(quantity);
        $.ajax({
            type: "post",
            url: "/Home/EditCart",
            data: {
                IDBillDetail: IDbilldetail,
                Quantity: quantity
            },
            success: function (res) {
                if (res == "true") {
                    notify("Thông báo", "Bạn đã giảm 1 sản phẩm", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ShoppingCart');
                    }, 2000)
                }
                else {
                    notify("Xảy ra lỗi", "Không thể sửa món ăn", false);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ShoppingCart');
                    }, 2000)
                }
            }
        })
    });

    //Tăng số lượng món ăn
    $(".quantiTy-increase").click(function (event) {
        event.preventDefault();
        var IDbilldetail = $(this).attr("data-IDBillDetail");
        var quantity = $(this).attr("data-quantity");
        quantity = Number(quantity);
        quantity = quantity + 1;
        $('.changQuantity').html(quantity);
        $.ajax({
            type: "post",
            url: "/Home/EditCart",
            data: {
                IDBillDetail: IDbilldetail,
                Quantity: quantity
            },
            success: function (res) {
                if (res == "true") {
                    notify("Thông báo", "Bạn đã tăng 1 sản phẩm", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ShoppingCart');
                    }, 2000)
                }
                else if (res == "Sold-out") {
                    notify("Xảy ra lỗi", "Số lượng món ăn hiện có không đủ ", false);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ShoppingCart');
                    }, 2000)
                }
                else {
                    notify("Xảy ra lỗi", "Không thể sửa món ăn", false);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ShoppingCart');
                    }, 2000)
                }
            }
        })
    });

    //Xóa món ăn
    $(".trash-o").click(function (event) {
        event.preventDefault();
        var index = $(this).attr("data-index");
        $.ajax({
            type: "post",
            url: "/Home/DeleteCart",
            data: {
                Index: index
            },
            success: function (res) {
                if (res == "true") {
                    notify("Thông báo", "Xóa món ăn khỏi giỏ hàng thành công", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../Home/ShoppingCart');
                    }, 2000)
                }
                if (res == "false") {
                    notify("Xảy ra lỗi", "Không thể xóa món ăn này", false);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    setTimeout(function () {
                        $(window).attr('location', '../OrderManagement/OrderManagement');
                    }, 2000)
                }
            }
        });
    });
});