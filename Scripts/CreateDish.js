function readURL(event) {
    if (event.target.files.length > 0) {
        let src = URL.createObjectURL(event.target.files[0]);
        let show = document.getElementById("showImage");
        let file = document.getElementById("file");
        file.style.display = "block";
        show.src = src;
        show.style.display = "block";
    }
}

window.reset = function (e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
    let file = document.getElementById("file");
    file.style.display = "none";
}