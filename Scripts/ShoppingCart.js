$(document).ready(function () {
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
                    $(window).attr('location', '../Home/ShoppingCart');
                }
                else if (res == "Sold-out") {
                    alert("Số lượng món ăn hiện có không đủ ");
                    $(window).attr('location', '../Home/ShoppingCart');
                }
                else {
                    alert("Không thể sửa món ăn");
                    $(window).attr('location', '../Home/ShoppingCart');
                }
            }
        })
    })
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
                    $(window).attr('location', '../Home/ShoppingCart');
                }
                else if (res == "Sold-out")
                {
                    alert("Số lượng món ăn hiện có không đủ ");
                    $(window).attr('location', '../Home/ShoppingCart');
                }
                else
                {
                    alert("Không thể sửa món ăn");
                    $(window).attr('location', '../Home/ShoppingCart');
                }
            }            
        })
    })
})
$(document).ready(function () {
    $(".trash-o").click(function () {
        var index = $(this).attr("data-index");
        alert(index);
        $.ajax({
            type: "post",
            url: "/Home/DeleteCart",
            data: {
                Index: index
            },
            success: function (res) {
                if (res == "true") {
                    $(window).attr('location', '../Home/ShoppingCart');
                }
                else {
                    alert("Không thể xóa món ăn này");
                    $(window).attr('location', '../Home/ShoppingCart');
                }
            }
        })
    })
})

//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})