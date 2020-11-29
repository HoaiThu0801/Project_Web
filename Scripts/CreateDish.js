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
        
        const reader = new FileReader();
        reader.onload();
        
    }
}
$(document).ready(function () {
    $('#signup-dish-form').submit(function (event) {
        event.preventDefault();
        var url = $(this).attr("action");
        var formdata = $(this).serialize();
        $.ajax({
            url: url,
            type: 'post',
            data: {
                DishName: $('#DishName').val(),
                Ingredient: $('#Ingredient').val(),
                ImportPrice: $('#ImportPrice').val(),
                SalePrice: $('#SalePrice').val(),
                Image: "/images/ImageProducts/" + srcImage,
            },
            success: function (res) {
                if (res == "true") {
                    alert("Tạo món ăn thành công");
                    setTimeout(function () {
                        location.reload();
                    },1000)
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