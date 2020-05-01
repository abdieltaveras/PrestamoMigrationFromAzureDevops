///// <reference path="only for purpose write test.ts" />

$(document).ready(function () {
    // Get Dom Elements
    //let navItems = $('.nav-item');
    //navItems.on('click', setCurrentTab);
    //let fechaNacElem = $("#Cliente_FechaNacimiento");
    //let allDateElems = $('input[type="datetime"]');

    // la clave para que valide todos los inputs

    // como validar todos los inputs
    // validat los inputs dentro de un tab pane no enfocado

    // declare variables
    let validForm = false;
    let d = new Date();
    $("#btnSubmit").click(function () {
        //alert("submit 2");
        formulario.validate().settings.ignore = "";
        let isValidForm = true;
        $('input[data-val="true"]').each(function (index, value) {
            let elem = $(this);
            turnOnOffValidationOnElem(elem);
            if (elem.attr("data-val") == "true") {
                let isValid = elem.valid();
                let elemName = elem.attr("id");
                if (!isValid) {
                    isValidForm = false;
                    console.log(elemName + " " + isValid);
                }
            }
        });
        var validform = formulario.validate();
        //console.log(isValidForm);
        formulario.validate({ ignore: ":hidden" });
        if (isValidForm) {
            formulario.submit(); // Submit the form
        }
    });
});
