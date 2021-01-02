$(document).ready(function () {
    $('.WarehouseDetail').click(function () {
        $('#popup').css('opacity', 1);
        $('#popup').css('visibility', 'visible');
        $("html, body").animate({ scrollTop: $('.popup').offset().top - 100 }, "slow");
    });
    $('#close-popup').click(function () {
        $('#popup').css('opacity', 0);
        $('#popup').css('visibility', 'hidden');
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });
});
$(document).ready(function () {
    $('.WarehouseDetail').click(function () {
        var idwarehouse = $(this).attr("data-IDWarehouse");
        $.ajax({
            type: "get",
            data: {
                IDWarehouse: idwarehouse
            },
            url: "/OrderManagement/WarehouseDetails",
            success: function (response) {
                $("tbody#listWarehouseDetails").html("");
                $(response).each(function (i, e) {
                    var tr = $('<tr class="d-flex text-center"/>');
                    $("<td/>").html(e.DishName).appendTo(tr);
                    $("<td/>").html(e.Quantity).appendTo(tr);
                    $("<td/>").html(e.Time).appendTo(tr);
                    tr.appendTo("tbody#listWarehouseDetails");
                });
            }
        });
    })
});