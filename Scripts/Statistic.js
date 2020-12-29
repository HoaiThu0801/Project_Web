function openChartStatistic(evt, statisticName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(statisticName).style.display = "block";
    evt.currentTarget.className += " active";
}
$(document).ready(function () {
    var flaq = true;
    $('#statistic').click(function () {
        if (flaq == true) {
            $('#charttypes').css('opacity', 1);
            $('#charttypes').css('visibility', 'visible');
            flaq = false;
        }
        else {
            $('#charttypes').css('opacity', 0);
            $('#charttypes').css('visibility', 'hidden');
            $('.tablinks').removeClass('active');
            $('.tabcontent').css('display', 'none');
            flaq = true;
        }
    });
});