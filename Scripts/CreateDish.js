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
                if (res == "true") {
                    alert("Tạo món ăn thành công");
                    setTimeout(function () {
                        location.reload();
                    }, 1000)
                }
                if (res == "false") {
                    alert("Tên món ăn bị trùng");
                }
                if (res == "NotPrice") {
                    alert("Giá nhập hoặc giá bán chưa nhập chính xác");
                }
                if (res == "CategoryNull") {
                    alert("Chưa chọn giá trị của trường Phân loại");
                }
                if (res == "ImageNull") {
                    alert("Chưa chọn hình ảnh");
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