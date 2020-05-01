var _urlNoImage = urlNoImage;
var _constantNoImagen = constantNoImagen;
function QuitarImagen(elemId, imageElemIdText, hiddeInputImgElemText) {
    //console.log(elemId, imageElemIdText);
    console.log(_urlNoImage, _constantNoImagen);
    $("#" + imageElemIdText).attr("src", _urlNoImage);
    $("#" + elemId + "Agregar").toggle();
    $("#" + elemId + "Quitar").toggle();
    $("#" + hiddeInputImgElemText).val(_constantNoImagen);
    event.preventDefault();
}
function ShowImagePreview(imageUploader, previewImageElemIdText, elemToggleIdText, hiddenInputForImageElemIdText) {
    console.log(elemToggleIdText);
    $("#" + elemToggleIdText + "Agregar").toggle();
    $("#" + elemToggleIdText + "Quitar").toggle();
    if (imageUploader.files && imageUploader.files[0]) {
        var imageElement = $("#" + previewImageElemIdText);
        console.log(hiddenInputForImageElemIdText);
        var inputTextElemForImage = $("#" + hiddenInputForImageElemIdText);
        ConsoleLogWidthAndHeigthOfImage(imageElement);
        var orientation_1 = -1;
        var num = 0;
        uploadPhotos(imageElement, 1800, num, inputTextElemForImage, imageUploader);
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
        var parts_1 = dataURL.split(',');
        var contentType_1 = parts_1[0].split(':')[1];
        var raw_1 = parts_1[1];
        return new Blob([raw_1], { type: contentType_1 });
    }
    var parts = dataURL.split(BASE64_MARKER);
    var contentType = parts[0].split(':')[1];
    var raw = window.atob(parts[1]);
    var rawLength = raw.length;
    var uInt8Array = new Uint8Array(rawLength);
    for (var i = 0; i < rawLength; ++i) {
        uInt8Array[i] = raw.charCodeAt(i);
    }
    return new Blob([uInt8Array], { type: contentType });
};
/* End Utility function to convert a canvas to a BLOB      */
var uploadPhotos = function (imgElem, MAX_SIZE, imgNum, inputElemToSetResizedImage, inputFileElem) {
    // Read in file
    var file = inputFileElem.target.files[0];
    // Ensure it's an image
    if (file.type.match(/image.*/)) {
        console.log('An image has been loaded');
        // Load the image
        var reader = new FileReader();
        reader.onload = function (readerEvent) {
            var image = new Image();
            image.onload = function (imageEvent) {
                // Resize the image
                var canvas = document.createElement('canvas'), max_size = MAX_SIZE, width = image.width, height = image.height;
                if (width > height) {
                    if (width > max_size) {
                        height *= max_size / width;
                        width = max_size;
                    }
                }
                else {
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
                //$.event.trigger({
                //    type: "imageResized",
                //    blob: resizedImage,
                //    num: imgNum,
                //    url: _url
                //});
                image.src = readerEvent.target.result.toString();
            };
            //imgElem.attr("src", image.src);
            //ConsoleLogWidthAndHeigthOfImage(imgElem);
        };
        //reader.readAsDataURL(file);
        reader.readAsDataURL(file);
    }
};
//# sourceMappingURL=showImage2.js.map