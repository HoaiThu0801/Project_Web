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
                    $(window).attr('location', '../OrderManagement/UserManagement');
                }
            }
        });
    });
})