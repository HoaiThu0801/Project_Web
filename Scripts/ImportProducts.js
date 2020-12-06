﻿//Show dish name when change select, load dishname accroding to warehouse
$(document).ready(function () {
    alert("Mời bạn chọn kho cửa hàng và món ăn để nhập hàng");
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
});

//Show button-submit and show success when submit
$(document).ready(function () {
    //Show button submit when change data in DishName-Select
    $('#DishName-Select').change(function () {
        $('#button-submit').css('opacity', '1');
        $('#button-submit').css('visibility', 'visible');
        $('#DisplayImportsProduct').css('height', 'unset');
    });

    //Show success when submit form
    $('#import-products-form').submit(function () {
        alert("Đã nhập hàng vào kho cửa hàng thành công");
    });
});