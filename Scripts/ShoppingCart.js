
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
            }            
        })
    })
})
