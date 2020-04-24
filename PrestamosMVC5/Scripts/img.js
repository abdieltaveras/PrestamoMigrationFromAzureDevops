        function ObjToFormData(obj)
        {
            var form_data = new FormData();
            for (var key in obj)
            {
                form_data.append(key, obj[key]);
            }
            return form_data;
        }
        /* Utility function to convert a canvas to a BLOB */
        var dataURLToBlob = function (dataURL)
        {
            var BASE64_MARKER = ';base64,';
            if (dataURL.indexOf(BASE64_MARKER) == -1)
            {
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

            for (var i = 0; i < rawLength; ++i)
            {
                uInt8Array[i] = raw.charCodeAt(i);
            }

            return new Blob([uInt8Array], { type: contentType });
        }
        /* End Utility function to convert a canvas to a BLOB      */
        $(function ()
        {
            $("[data-img]").change(function ()
            {
                var img=$(this).attr("data-img");
                var num=parseInt(img[img.length-1]);
                uploadPhotos('@Url.Action("../home/UploadImage")', 1024*1.5, num);
            });

            window.uploadPhotos = function (Url, MAX_SIZE, imgNum)
            {
                // Read in file
                var file = event.target.files[0];

                // Ensure it's an image
                if (file.type.match(/image.*/))
                {
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
                            canvas.getContext('2d').drawImage(image, 0, 0, width, height);
                            var dataUrl = canvas.toDataURL('image/jpeg');
                            var resizedImage = dataURLToBlob(dataUrl);
                            console.log("event.trigger");
                            $.event.trigger({
                                type: "imageResized",
                                blob: resizedImage,
                                num: imgNum,
                                url: Url
                            });
                        };
                        image.src = readerEvent.target.result;
                    };
                    reader.readAsDataURL(file);
                }
            };

            /* Handle image resized events */
            $(document).on("imageResized", function (event)
            {
                console.log("imageResized");
                $("[data-validate=1]").attr("disabled", "true");
                if (event.blob && event.url)
                {
                    var data = ObjToFormData({
                        InspectionGKey: $('[name="InspectionGKey"]').val(),
                        UnitNbr: $('[name="UnitNbr"]').val(),
                        fileExtension: "jpg",
                        ImgType: "seal",
                        number:event.num,
                        descInfo:""
                    }); //new FormData($("#frm")[0]);
                    data.append("ImgDat", event.blob);
                    $.ajax({
                        url: event.url,
                        data: data,
                        cache: false,
                        contentType: false,
                        processData: false,
                        type: 'POST',
                        success: function (response)
                        {
                            console.log("success");
                            console.log(response);
                            $('[name="ImageName'+event.num+'"]').val(response);
                            $("[data-validate=1]").removeAttr("disabled");
                        }
                    });
                }
            });
        });