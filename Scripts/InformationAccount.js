$(document).ready(function () {
    $('#changePass').click(function () {
        $.ajax({
            type: 'get',
            url: '/Home/ChangePass',
            success: function () {
            }
        });
    });
});