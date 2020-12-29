$(document).ready(function () {
    $(".DeleteUser").click(function (e) {
        var iduser = $(this).attr("data-IDUser");
        $.ajax({
            url: "/OrderManagement/DeleteUser",
            data: {
                IDUser: iduser
            },
            success: function (response) {
                if (response == "True") {
                    notify("Thông báo", "Xóa người dùng này thành công", true);
                    setTimeout(function () {
                        $(window).attr('location', '../OrderManagement/UserManagement');
                    }, 2000);
                }
            }
        });
    });
})