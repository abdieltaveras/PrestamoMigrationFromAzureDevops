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
    //let validForm = false;
    //let d = new Date();
    $("#btnSubmit").click(function () {
        //validReferences();
        //return;
        //formulario.submit();
        //alert("submit 2");
        //return;
        formulario.validate().settings.ignore = "";
        let isValidForm = true;

        $('input[data-val="true"],textarea').each(function () {
            const elem = $(this);
            //const elemId = elem.attr("id"); 
            //console.log(elemId);
            if (typeof turnOnOffValidationOnElem === 'function') {
                turnOnOffValidationOnElem(elem);
            }
            //const validarElemento = elem.attr("data-val") === "true";
            const validarElemento = elem.data("val");
            if (validarElemento) {
                const isValid = elem.valid();
                if (!isValid) {
                    isValidForm = false;
                    // ignore esta linea si desea ver en la consola cuales inputs  su validacion es falsa
                    // es decir false que indica que tiene algun error
                    // console.log(elemId + " " + isValid);
                }
            }
            else {
                //const elemId = elem.attr("id"); 
                //console.log("no validar "+elemId+" "+elem.data("val"));
            }
        });
        if (isValidForm) {
            formulario.validate({ ignore: ":hidden, .ignore-error" });
        }

        if (typeof otherValidations === 'function') {
            if (!otherValidations()) {
                return;
            }
        }        
        if (isValidForm) {
            if (typeof beforeSubmit === 'function') {
                beforeSubmit();
            }
            formulario.submit(); // Submit the form
        }
    });
});
