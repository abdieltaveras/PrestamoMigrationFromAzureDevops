$(document).ready(function () {
    // Get Dom Elements
    //let navItems = $('.nav-item');
    //navItems.on('click', setCurrentTab);
    let fechaNacElem = $("#Cliente_FechaNacimiento");
    //let allDateElems = $('input[type="datetime"]');
    let formulario = $("#frmCreateCliente");
    // la clave para que valide todos los inputs
    formulario.validate().settings.ignore = "";
    // como validar todos los inputs
    // validat los inputs dentro de un tab pane no enfocado
    $('#mensajeError').css("background-color", "yellow");
    setTimeout(function () {
        $('#mensajeError').remove();
    }, 8000);
    // declare variables
    let validForm = false;
    let d = new Date();
    $("#btnSubmit").click(function () {
        validForm = true;
        $('input[data-val="true"]').each(function (index, value) {
            let elem = $(this);
            let isValid = elem.valid();
            let elemName = elem.attr("id");
            if (!isValid) {
                validForm = false;
                console.log(elemName + " " + isValid);
            }
        });
        var valid = formulario.validate();
        console.log(valid);
        if (validForm) {
            formulario.submit(); // Submit the form
        }
    });
});
