function openOrder(evt, orderName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.opacity = 0;
        tabcontent[i].style.visibility = "hidden";
        tabcontent[i].style.height = 0;
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(orderName).style.opacity = 1;
    document.getElementById(orderName).style.visibility = "visible";
    document.getElementById(orderName).style.height = "unset";
    evt.currentTarget.className += " active";
}
