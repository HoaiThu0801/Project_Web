$(document).ready(function () {
    var dateStart;
    var dateEnd;
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
    });

    $('#Cancel').click(function () {
        $('.main-background').css('height', '70px');
        $('#table').css('opacity', 0);
        $('#table').css('visibility', 'hidden');
    });
});