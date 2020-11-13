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