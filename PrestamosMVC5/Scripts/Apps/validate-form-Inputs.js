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
    //Declare const
    var FAMILIA = 2;
    // declare variables
    var validForm = false;
    var d = new Date();
    $("#btnSubmit").click(function () {
        //validReferences();
        //return;
        //formulario.submit();
        //alert("submit 2");
        //return;
        formulario.validate().settings.ignore = "";
        var isValidForm = true;
        $('input[data-val="true"]').each(function (index, value) {
            var elem = $(this);
            turnOnOffValidationOnElem(elem);
            if (elem.attr("data-val") == "true") {
                var isValid = elem.valid();
                var elemName = elem.attr("id");
                if (!isValid) {
                    isValidForm = false;
                    console.log(elemName + " " + isValid);
                }
            }
        });
        var validform = formulario.validate();
        //console.log(isValidForm);
        //Validar referencias
        if (!validReferences()) {
            return;
        }
        formulario.validate({ ignore: ":hidden" });
        if (isValidForm) {
            beforeSubmit();
            formulario.submit(); // Submit the form
        }
    });
    $(".select-tipo").change(function () {
        var dataOrder = $(this).data('order');
        //console.log(dataOrder);
        var selectedOption = $(this).children("option:selected").val();
        if (selectedOption != FAMILIA) {
            $('#Referencias_' + dataOrder + '__Vinculo').prop("disabled", true);
            $('#Referencias_' + dataOrder + '__Vinculo').val(0);
        }
        else {
            $('#Referencias_' + dataOrder + '__Vinculo').prop("disabled", false);
        }
    });
    function validReferences() {
        var selectsTipo = $('.select-tipo');
        var isValid = true;
        var count = 0;
        var validReferences = 0;
        selectsTipo.each(function (index, value) {
            var elem = $(this);
            var selectedOption = $(this).find(":selected");
            // Evaluar si selecciono un vinculo si el tipo es familiar
            if (selectedOption.val() == FAMILIA) {
                var vinculo = $('#Referencias_' + count + '__Vinculo');
                if (vinculo.children("option:selected").val() == 0) {
                    vinculo.addClass('is-invalid');
                    isValid = false;
                }
                else {
                    vinculo.removeClass('is-invalid');
                }
            }
            var nombre = $('#Referencias_' + count + '__NombreCompleto');
            var telefono = $('#Referencias_' + count + '__Telefono');
            var direccion = $('#Referencias_' + count + '__Direccion');
            if (selectedOption.val() != 0) {
                // Validar nombre
                if (nombre.val().length < 1) {
                    nombre.addClass('is-invalid');
                    isValid = false;
                }
                else {
                    nombre.removeClass('is-invalid');
                }
                // Validar Telefono
                if (telefono.val().length < 1) {
                    telefono.addClass('is-invalid');
                    isValid = false;
                }
                else {
                    telefono.removeClass('is-invalid');
                }
                // Validar Direccion
                if (direccion.val().length < 1) {
                    direccion.addClass('is-invalid');
                    isValid = false;
                }
                else {
                    direccion.removeClass('is-invalid');
                }
                validReferences++;
            }
            count++;
        });
        // Evaluar si hay menos de tres referencias
        if (validReferences < cantidadDeReferenciasMinimasPorCliente) {
            isValid = false;
            $('#alert').show();
        }
        else {
            $('#alert').hide();
        }
        return isValid;
    }
});
//# sourceMappingURL=validate-form-Inputs.js.map