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

    //Scroll to top page
    $("html, body").animate({ scrollTop: 0 }, "slow");

    $('#signup-dish-form').submit(function (event) {
        event.preventDefault();
        //Khai báo biến
        var dishname = $('#DishName').val();
        var ingredient = $('#Ingredient').val();
        var importPrice = $('#ImportPrice').val();
        var category = $('#SelectCategory').val();
        var salePrice = $('#SalePrice').val();

        var formdata = new FormData();
        var fileUpload = $('#Image').get(0);
        var files = fileUpload.files;
        //Validate importPrice and salePrice
        var regex = /[0-9]/;
        if (!regex.test(importPrice)) {
            importPrice = "";
        }
        if (!regex.test(salePrice)) {
            salePrice = "";
        }
        var url = $(this).attr("action");
        //Add to formdata
        formdata.append("DishName", dishname);
        formdata.append("Ingredient", ingredient);
        formdata.append("ImportPrice", importPrice);
        formdata.append("SalePrice", salePrice);
        formdata.append("Category", category);
        formdata.append("FileUpload", files[0]);

        $.ajax({
            url: url,
            type: 'post',
            dataType: "json",
            processData: false,
            enctype: 'multipart/form-data',
            data: formdata,
            contentType: false,
            success: function (res) {
                if (res.type == true) {
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    notify(res.title,res.message,true);
                    setTimeout(function () {
                        location.reload();
                    }, 2000)
                }
                if (res == "menu") {
                    $('#AlertBoxforJS').css('top', $('#DishName').offset().top - 100);
                    notify("Xảy ra lỗi", "Tên món ăn đã có trong dữ liệu", false);
                    $("html, body").animate({ scrollTop: $('#DishName').offset().top - 100 }, "slow");
                }
                if (res == "importPrice") {
                    $('#AlertBoxforJS').css('top', $('#ImportPrice').offset().top - 100);
                    notify("Xảy ra lỗi", "Giá trị của nhập giá nhập phải là số", false);
                    $("html, body").animate({ scrollTop: $('#ImportPrice').offset().top - 100 }, "slow");
                }
                if (res == "salePrice") {
                    $('#AlertBoxforJS').css('top', $('#SalePrice').offset().top - 100);
                    notify("Xảy ra lỗi", "Giá trị của nhập giá bán phải là số", false);
                    $("html, body").animate({ scrollTop: $('#SalePrice').offset().top - 100 }, "slow");
                }
                if (res == "image") {
                    $('#AlertBoxforJS').css('top', $('#ImageG').offset().top - 100);
                    notify("Xảy ra lỗi", "Chưa chọn hình ảnh cho món ăn", false);
                    $("html, body").animate({ scrollTop: $('#ImageG').offset().top - 100 }, "slow");
                }
                if (res == "category") {
                    $('#AlertBoxforJS').css('top', $('#SelectCategory').offset().top - 100);
                    notify("Xảy ra lỗi", "Giá trị của trường Phân loại chưa có", false);
                    $("html, body").animate({ scrollTop: $('#SelectCategory').offset().top - 100 }, "slow");
                }
            }
        });
    });

    //Event submit excel file
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
                if (res == "success") {
                    notify("Thông báo", "Thêm dữ liệu thành công", true);
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                if (res == "warning") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Vui lòng chọn tệp excel", false);
                }
                if (res == "false") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Chọn tệp để thêm dữ liệu", false);
                }
                if (res == "dishname") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Cột Dishname trong excel chưa có dữ liệu", false);
                }
                if (res == "ingredient") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Cột Ingredient trong excel chưa có dữ liệu", false);
                }
                if (res == "importPrice") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Cột ImportPrice trong excel chưa có dữ liệu", false);
                }
                if (res == "salePrice") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Cột SalePrice trong excel chưa có dữ liệu", false);
                }
                if (res == "image") {
                    $('#AlertBoxforJS').css('top', $('#formExcel').offset().top - 100);
                    $("html, body").animate({ scrollTop: $('#formExcel').offset().top - 100 }, "slow");
                    notify("Xảy ra lỗi", "Cột Image trong excel chưa có dữ liệu", false);
                }
            },
        });
    });
});

window.reset = function (e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
    let file = document.getElementById("file");
    file.style.display = "none";
}
