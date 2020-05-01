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
        validReferences();
        //return;
        //formulario.submit();
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
    $(".select-tipo").change(function () {
        var dataOrder = $(this).data('order');
        console.log(dataOrder);
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
        var count = 0;
        selectsTipo.each(function (index, value) {
            var elem = $(this);
            var selectedOption = elem.children("option:selected").val();
            if (selectedOption == FAMILIA) {
                var vinculo = $('#Referencias_' + count + '__Vinculo').children("option:selected").val();
                console.log('vinculo', vinculo);
                if (vinculo == 0) {
                    console.log('Referencias_' + count + '__Vinculo viola la regla');
                }
            }
            console.log(selectedOption);
            count++;
        });
    }
});
//# sourceMappingURL=validate-form-Inputs.js.map