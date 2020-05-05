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
    const FAMILIA = 2;

    // declare variables
    let validForm = false;
    let d = new Date();
    $("#btnSubmit").click(function () {
        //validReferences();
        //return;
        //formulario.submit();
        //alert("submit 2");
        //return;
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

        let dataOrder = $(this).data('order');
        //console.log(dataOrder);
        let selectedOption = $(this).children("option:selected").val();

        if (selectedOption != FAMILIA) {
            $('#Referencias_' + dataOrder + '__Vinculo').prop("disabled", true);
            $('#Referencias_' + dataOrder + '__Vinculo').val(0);
        } else {
            $('#Referencias_' + dataOrder + '__Vinculo').prop("disabled", false);

        }
    });

    function validReferences() {

        let selectsTipo = $('.select-tipo');
        let isValid = true;
        let count = 0;
        let validReferences = 0;

        selectsTipo.each(function (index, value) {
            let elem = $(this);
            let selectedOption = $(this).find(":selected");

            // Evaluar si selecciono un vinculo si el tipo es familiar
            if (selectedOption.val() == FAMILIA) {
                let vinculo = $('#Referencias_' + count + '__Vinculo');
               
                if (vinculo.children("option:selected").val() == 0) {
                    vinculo.addClass('is-invalid');
                    isValid = false;
                } else {
                    vinculo.removeClass('is-invalid');
                }
            }

            let nombre = $('#Referencias_' + count + '__NombreCompleto');
            let telefono = $('#Referencias_' + count + '__Telefono');
            let direccion = $('#Referencias_' + count + '__Direccion');

            if (selectedOption.val() != 0) {
                
                // Validar nombre
                if (nombre.val().length < 1) {
                    nombre.addClass('is-invalid');
                    isValid = false;
                } else {
                    nombre.removeClass('is-invalid');
                }

                // Validar Telefono
                if (telefono.val().length < 1) {
                    telefono.addClass('is-invalid');
                    isValid = false;
                } else {
                    telefono.removeClass('is-invalid');
                }

                // Validar Direccion
                if (direccion.val().length < 1) {
                    direccion.addClass('is-invalid');
                    isValid = false;
                } else {
                    direccion.removeClass('is-invalid');
                }

                validReferences++;
            }
            count++;
        });

        // Evaluar si hay menos de tres referencias
        if (validReferences < 3) {
            isValid = false;
            $('#alert').show();
        } else {
            $('#alert').hide();
        }

        return isValid;
    }

    

});
