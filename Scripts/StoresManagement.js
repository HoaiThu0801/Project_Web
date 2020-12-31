$(document).ready(function () {
    $('.DeleteStore').click(function () {
        var idstore = $(this).attr("data-IDStore")
        $.ajax({
            type: "post",
            data: {
                IDStore: idstore
            },
            url: "/OrderManagement/DeleteStore",
            success: function (response) {
                if (response.type == true) {
                    notify("Thông báo", "Xóacửa hàng: " + response.message + " thành công", true);
                    setTimeout(function () {
                        $(window).attr('location', '../OrderManagement/StoresManagement');
                    }, 2000);
                }
            }
        });
    });
});
