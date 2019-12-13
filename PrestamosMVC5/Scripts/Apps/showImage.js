function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        let imageElement = $(previewImage);
        let imgFile = imageUploader.files[0];
        let fsize = imageUploader.files[0].size;
        let orientation = -1;
        var reader = new FileReader();
        reader.onload = function (e) {
            imageElement.attr('src', e.target.result);
            let img = document.getElementById("imagePreview");
            alert("change");
            changeOrientation(img);
        };
        reader.readAsDataURL(imgFile);

        console.log(fsize, orientation);
        // https://stackoverflow.com/questions/24658365/img-tag-displays-wrong-orientation 
    }
}


function changeOrientation(imgElem) {
    EXIF.getData(imgElem, function () {
        var orientation = EXIF.getTag(this, "Orientation");
        alert(orientation);
        if (orientation === 6)
            rotateImg();
    });
    function rotateImg() {
        alert("a rotar");
        let elemImg = $("#imagePreview");
        degrees = 90;
        elemImg.css({
            'transform': 'rotate(' + degrees + 'deg)',
            '-ms-transform': 'rotate(' + degrees + 'deg)',
            '-moz-transform': 'rotate(' + degrees + 'deg)',
            '-webkit-transform': 'rotate(' + degrees + 'deg)',
            '-o-transform': 'rotate(' + degrees + 'deg)'
        });

    }
}
