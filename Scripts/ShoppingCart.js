$(document).ready(function () {
    $(".changQuantity").click(function (event) {
        event.preventDefault();
        var IDbilldetail = $(this).attr("data-IDBillDetail");
        var quantity = $(this).val();
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
