$(document).ready(function () {
    $(".DeleteUser").click(function (e) {
        e.preventDefault();
        var iduser = $(this).attr("data-IDUser");
        $.ajax({
            url: "/OrderManagement/DeleteUser",
            data: {
                IDUser: iduser
            },
            success: function (response) {
                if (response.type == true) {
                    notify("Thông báo", "Xóa người dùng có username: " + response.message + " thành công", true);
                    setTimeout(function () {
                        $(window).attr('location', '../OrderManagement/UserManagement');
                    }, 2000);
                }
            }
        });
    });
})