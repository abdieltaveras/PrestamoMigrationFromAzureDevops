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
    var validForm = false;
    var d = new Date();
    $("#btnSubmit").click(function () {
        //alert("submit 2");
        formulario.validate().settings.ignore = "";
        var isValidForm = true;
        $('input[data-val="true"]').each(function (index, value) {
            var elem = $(this);
            turnOnOffValidations(elem);
            var isValid = elem.valid();
            var elemName = elem.attr("id");
            if (!isValid) {
                isValidForm = false;
                console.log(elemName + " " + isValid);
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
//# sourceMappingURL=validate-form-Inputs.js.map