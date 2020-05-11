let _urlNoImage = urlNoImage;
let _constantNoImagen = constantNoImagen;

function QuitarImagen(elemId, imageElemIdText, hiddeInputImgElemText) {
    //console.log(elemId, imageElemIdText);
    $("#" + imageElemIdText).attr("src", _urlNoImage);
    $("#" + elemId + "Agregar").toggle();
    $("#" + elemId + "Quitar").toggle();
    $("#" + hiddeInputImgElemText).val(_constantNoImagen);
    event.preventDefault();
}
function ShowImagePreview(imageUploader, previewImageElemIdText, elemToggleIdText, hiddenInputForImageElemIdText) {
    
    $("#" + elemToggleIdText + "Agregar").toggle();
    $("#" + elemToggleIdText + "Quitar").toggle();
    if (imageUploader.files && imageUploader.files[0]) {
        let imageElement = $("#" + previewImageElemIdText);
        
        let inputTextElemForImage = $("#"+ hiddenInputForImageElemIdText);
        
        let orientation = -1;
        let num = 0;
        uploadPhotos(imageElement, 1800, num, inputTextElemForImage);
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
                
                //$.event.trigger({
                //    type: "imageResized",
                //    blob: resizedImage,
                //    num: imgNum,
                //    url: _url
                //});

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



