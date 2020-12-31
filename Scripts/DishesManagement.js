$(document).ready(function () {
    $('.DeleteDish').click(function () {
        var iddish = $(this).attr("data-IDDish")
        $.ajax({
            type: "post",
            data: {
                IDDish: iddish
            },
            url: "/OrderManagement/DeleteDish",
            success: function (response) {
                if (response.type == true) {
                    notify("Thông báo", "Xóa món: " + response.message + " thành công", true);
                    setTimeout(function () {
                        $(window).attr('location', '../OrderManagement/DishesManagement');
                    }, 2000);
                }
            }
        });
    });
});
