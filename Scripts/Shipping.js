$(document).ready(function () {
    $("#ProvinceSelect").change(function () {
        var provincename = $("#ProvinceSelect").val();
        $.ajax({
            type: "get",
            url: "/OrderProducts/LoadDistrict",
            data: {
                ProvinceName: provincename
            },
            success: function (res) {
                $('#DistrictSelect').html("");
                $("#DistrictSelect").append(("<option disabled selected>" + "Chọn Quận/Huyện" + "</option>"));
                $.each(response, function (key, item) {
                    $("#DistrictSelect").append("<option>" + item + "</option>");
                });
            }
        })
    })
})