
var flaq = true;
function openPopupForm() {

    if (flaq == false) {
        document.getElementById("signup-content").style.display = "none";
        flaq = true;
    }
    else {
        document.getElementById("signup-content").style.display = "block";
        flaq = false;
    }
}
function closePopupForm() {
    if (flaq == false) {
        document.getElementById("signup-content").style.display = "none";
        flaq = true;
    }
    else {
        document.getElementById("signup-content").style.display = "none";
        flaq = false;
    }
}

$(document).ready(function () {
    $("#signup-form").submit(function (event) {
        event.preventDefault();
        var url = $(this).attr("action");
        var formdata = $(this).serialize();
        $.ajax({
            type: 'post',
            url: url,
            data: formdata,
            success: function () {
                $("#showSuccess").html("Đăng ký thành công");
                $(".form-input").val('');
            },
        });
        return false;
    });
});