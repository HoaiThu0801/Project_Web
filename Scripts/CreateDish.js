var srcImage;
function readURL(event) {
    if (event.target.files.length > 0) {
        let src = URL.createObjectURL(event.target.files[0]);
        let show = document.getElementById("showImage");
        let file = document.getElementById("file");
        file.style.display = "block";
        show.src = src;
        srcImage = event.target.files[0].name;
        show.style.display = "block";

    }
}
$(document).ready(function () {
    $('#signup-dish-form').submit(function (event) {
        event.preventDefault();
        var url = $(this).attr("action");
        $.ajax({
            url: url,
            type: 'post',
            data: {
                DishName: $('#DishName').val(),
                Ingredient: $('#Ingredient').val(),
                ImportPrice: $('#ImportPrice').val(),
                SalePrice: $('#SalePrice').val(),
                Image: "/images/ImageProducts/" + srcImage,
                Category: $('#SelectCategory').val(),
            },
            success: function (res) {
                if (res.type == true) {
                    notify(res.title,res.message,true);
                    setTimeout(function () {
                        location.reload();
                    }, 2000)
                }
                else{
                    notify(res.title, res.message, false);
                    setTimeout(function () {
                        location.reload();
                    }, 2000)
                }
            }
        });
    });
});

window.reset = function (e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
    let file = document.getElementById("file");
    file.style.display = "none";
}

$(document).ready(function () {
    $('#formExcel').submit(function (event) {
        event.preventDefault();
        var formdata = new FormData();
        var fileUpload = $('#FileUpload').get(0);
        var files = fileUpload.files;
        formdata.append("FileUpload", files[0]);
        $.ajax({
            url: "/Administrator_Setting/UpLoadMenu",
            type: "post",
            enctype: 'multipart/form-data',
            processData: false,
            dataType: "json",
            data: formdata,
            contentType: false,
            success: function (res) {
                alert(res);
            },
        });
    });
});
//Scroll to top page
$(document).ready(function () {
    $("html, body").animate({ scrollTop: 0 }, "slow");
})