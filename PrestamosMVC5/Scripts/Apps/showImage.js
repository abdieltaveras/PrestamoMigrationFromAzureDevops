function ShowImagePreview(imageUploader, previewImageElemIdText, elemToggleIdText, hiddenInputForImageElemIdText) {
    console.log(elemToggleIdText);
    $("#" + elemToggleIdText + "Agregar").toggle();
    $("#" + elemToggleIdText + "Quitar").toggle();
    if (imageUploader.files && imageUploader.files[0]) {
        let imageElement = $("#" + previewImageElemIdText);
        console.log(hiddenInputForImageElemIdText);
        let inputTextElemForImage = $("#"+ hiddenInputForImageElemIdText);
        ConsoleLogWidthAndHeigthOfImage(imageElement);
        let orientation = -1;
        let num = 0;
        uploadPhotos(imageElement, 1800, num, inputTextElemForImage);
        //compress(event, imageElement);

        ////codigo que funcionaba antes pero sin hacer resize
        ////inicio
        //var reader = new FileReader();
        //reader.onload = function (e) {
        //    imageElement.attr('src', e.target.result);
        //    let img = document.getElementById("imagePreview");
        //    //changeOrientation(img);
        //};
        //reader.readAsDataURL(imgFile);
        ////fin

        //console.log(fsize, orientation);

        // https://stackoverflow.com/questions/24658365/img-tag-displays-wrong-orientation 
    }
}


function ConsoleLogWidthAndHeigthOfImage(img) {
    console.log(img.css("width"), img.css("height"));
}
// ernesto img.js
var dataURLToBlob = function (dataURL) {
    var BASE64_MARKER = ';base64,';
    if (dataURL.indexOf(BASE64_MARKER) === -1) {
        let parts = dataURL.split(',');
        let contentType = parts[0].split(':')[1];
        let raw = parts[1];

        return new Blob([raw], { type: contentType });
    }

    let parts = dataURL.split(BASE64_MARKER);
    let contentType = parts[0].split(':')[1];
    let raw = window.atob(parts[1]);
    let rawLength = raw.length;

    var uInt8Array = new Uint8Array(rawLength);

    for (var i = 0; i < rawLength; ++i) {
        uInt8Array[i] = raw.charCodeAt(i);
    }

    return new Blob([uInt8Array], { type: contentType });
};
/* End Utility function to convert a canvas to a BLOB      */



window.uploadPhotos = function (imgElem, MAX_SIZE, imgNum, inputElemToSetResizedImage) {
    // Read in file

    var file = event.target.files[0];

    // Ensure it's an image
    if (file.type.match(/image.*/)) {
        console.log('An image has been loaded');

        // Load the image
        var reader = new FileReader();
        reader.onload = function (readerEvent) {
            var image = new Image();
            image.onload = function (imageEvent) {
                // Resize the image
                var canvas = document.createElement('canvas'),
                    max_size = MAX_SIZE,
                    width = image.width,
                    height = image.height;
                if (width > height) {
                    if (width > max_size) {
                        height *= max_size / width;
                        width = max_size;
                    }
                } else {
                    if (height > max_size) {
                        width *= max_size / height;
                        height = max_size;
                    }
                }
                canvas.width = width;
                canvas.height = height;
                var ctx = canvas.getContext('2d').drawImage(image, 0, 0, width, height);
                var dataUrl = canvas.toDataURL('image/jpeg');
                //console.log(dataUrl);
                imgElem.attr("src", dataUrl);
                inputElemToSetResizedImage.val(dataUrl);
                //$("#image1PreviewValue").val(dataUrl);
                //imgFile.setAttribute('value', dataUrl);
                var resizedImage = dataURLToBlob(dataUrl);
                console.log(resizedImage);
                $.event.trigger({
                    type: "imageResized",
                    blob: resizedImage,
                    num: imgNum,
                    url: _url
                });

            };
            image.src = readerEvent.target.result;

            //imgElem.attr("src", image.src);
            //ConsoleLogWidthAndHeigthOfImage(imgElem);
        };
        //reader.readAsDataURL(file);
        reader.readAsDataURL(file);
    }
};

/* Handle image resized events */
/* Handle image resized events */
$(document).on("imageResized", function (event) {
    console.log("imageResized event fired");
    if (event.blob && event.url) {
        //var data = new FormData();
        //data.append("Imagen", event.blob);
        //$.ajax({
        //    url: event.url,
        //    data:data,
        //    cache: false,
        //    contentType: false,
        //    processData: false,
        //    type: 'POST',
        //    success: function (response) {
        //        console.log("success");
        //        console.log(response);
        //    }
        //});
    }
});



function compress(e, imgElem) {
    const width = 50;
    const height = 30;
    const fileName = e.target.files[0].name;
    const reader = new FileReader();
    reader.readAsDataURL(e.target.files[0]);

    reader.onload = event => {
        const img = new Image();
        img.src = event.target.result;

        img.onload = () => {
            const elem = document.createElement('canvas');
            elem.width = width;
            elem.height = height;

            const ctx = elem.getContext('2d');
            // img.width and img.height will contain the original dimensions
            ctx.drawImage(img, 0, 0, width, height);
            ctx.canvas.toBlob((blob) => {

                const file = new File([blob], fileName, {
                    type: 'image/jpeg',
                    lastModified: Date.now()
                });
            }, 'image/jpeg', 1);


        },
            //imgElem.attr('src', img.attributes("src"));
            //image.src = readerEvent.target.result;readerEvent.target.result;
            imgElem.attr("src", reader.result);
        imageUploader[0].setAttribute('value', reader.result);
        console.log(reader);
        ConsoleLogWidthAndHeigthOfImage(imgElem);
        reader.onerror = error => console.log(error);
    };

}