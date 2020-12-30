$(document).ready(function () {

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    //Show button submit when change data in element has id DishName-Select
    $('#DishName-Select').change(function () {
        $('#button-submit').css('opacity', '1');
        $('#button-submit').css('visibility', 'visible');
        $('#DisplayImportsProduct').css('height', 'unset');
    });

    //Show dish name when change select, load dishname accroding to warehouse
    $('#Warehouse-Select').change(function () {
        var warehouse = $('#Warehouse-Select').val();
        $('#DisplayImportsProduct').css('opacity', '1');
        $('#DisplayImportsProduct').css('visibility', 'visible');
        $('#DisplayImportsProduct').css('height', '270px');
        $.ajax({
            type: "get",
            url: "/ImportProducts/LoadDishName",
            data: {
                WarehouseName: warehouse
            },
            success: function (response) {
                $('#DishName-Select').html("");
                $("#DishName-Select").append(("<option>" + "DishName" + "</option>"));
                $.each(response, function (key, item) {
                    $("#DishName-Select").append("<option>" + item.DishName + "</option>");
                });
            }
        })
    });

    //Show success when submit form
    $('#import-products-form').submit(function (e) {
        e.preventDefault();
        var url = $(this).attr('action');
        var formdata = $(this).serialize();
        $.ajax({
            type: "post",
            url: url,
            data: formdata,
            success: function (res) {
                if (res == "true") {
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    notify("Thông báo", "Đã nhập hàng vào kho cửa hàng thành công", true);
                }
            }
        })
    });
});