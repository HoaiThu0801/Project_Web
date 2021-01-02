var dateStart;
var dateEnd;
$(document).ready(function () {

    //$('#datepicker').daterangepicker({
    //    open: 'center'
    //}, function (start, end) {
    //    dateStart = start.format('YYYY-MM-DD');
    //    dateEnd = end.format('YYYY-MM-DD');
    //});

    var start = moment().subtract(29, 'days');
    var end = moment();
    var dateStart;
    var dateEnd;
    function cb(start, end) {
        $('#reportrange span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
        dateStart = start.format('YYYY-MM-DD');
        dateEnd = end.format('YYYY-MM-DD');
    }

    $('#reportrange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Hôm nay': [moment(), moment()],
            'Hôm qua': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            '7 Ngày trước': [moment().subtract(6, 'days'), moment()],
            '30 Ngày trước': [moment().subtract(29, 'days'), moment()],
            'Tháng này': [moment().startOf('month'), moment().endOf('month')],
            'Tháng trước': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);


    $('#Confirm').click(function () {
        $('.main-background').css('height', 'unset');
        $('#table').css('opacity', 1);
        $('#table').css('visibility', 'visible');
        $.ajax({
            type: "post",
            url: "/OrderManagement/TrackRevenue",
            data: {
                StartTime: dateStart,
                EndTime: dateEnd
            },
            success: function (response) {
                $("tbody.listWarehouseDetails").html("");
                $(response).each(function (i, e) {
                    var tr = $('<tr class="text-center"/>');
                    $("<td/>").html(e.StoreName).appendTo(tr);
                    $("<td/>").html(e.Address).appendTo(tr);
                    $("<td/>").html(e.FullName).appendTo(tr);
                    $("<td/>").html(e.Revenue).appendTo(tr);
                    $("<td/>").html(e.Time).appendTo(tr);
                    tr.appendTo("tbody#listRevenue");
                });
            }
        });
    });

    $('#Cancel').click(function () {
        $('.main-background').css('height', '70px');
        $('#table').css('opacity', 0);
        $('#table').css('visibility', 'hidden');
    });
    $('#ExportExcel').click(function () {
        $.ajax({
            url: "/OrderManagement/ExportTrack",
            data: {
                StartTime: dateStart,
                EndTime: dateEnd
            },
            success: function (response) {
            }
        });
    });
});
