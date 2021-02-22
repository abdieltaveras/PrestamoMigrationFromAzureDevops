// to clean error message when user input value
// need to be used togehter with <div class="invalid-feedback">
// that is when using bootstrap 4 show hide validation message
$(".remove-error").on("click", (removeMensajesError));

$(".remove-error").keypress(function () {
    if (this.value.length === 0) {
        const jQElem = $(this);
        jQElem.removeClass('is-invalid');
    }
})

function removeMensajesError() {
    const jqEleme = $(this);
    removeErrorMessageForElem(jqEleme);
}

$(document).ready(function () {
    const validElem = $('#validationSummary');
    let shertime = $("#showerrorstime").data("value");
    const mensajerErrorDiv = $('#mensajeError');
    shertime = (shertime.lenght === 0) ? 5000 : shertime*1000;
    const hayMensajesEnelValidationSummary = validElem.length > 0;
    if (hayMensajesEnelValidationSummary) {
        mensajerErrorDiv.addClass("alert alert-danger");
        setTimeout(function () {
            validElem.remove();
            //$('#validationSummary').remove();
            //$('#mensajeError').remove();
            mensajerErrorDiv.remove();
        }, shertime);
    }
    else {
        $('#mensajeError').remove();
    }
    
});

function setSpanErrorForElem(elemName: string, errorMessage: string)
{
    let spanToSearch = 'span[data-valmsg-for="' + elemName + '"]';
    let spanForError = $(spanToSearch);
    spanForError.html("<span id='" + elemName + "-error' class=''>" + errorMessage + "</span>")
}


// esta funcion quita el elemento que maneja los mensajes de error
// al quitarlo luego ya no hay formar de mostrarlo de nuevo
function removeSpanErrorForElem(elemName: string ) {
    const spanToSearch = 'span[data-valmsg-for="' + elemName + '"]';
    const spanForError = $(spanToSearch);
    spanForError.remove();
}

// esta funcion quita el mensaje del span que tiene el mensaje unicamente pero
// deja el span que contiene la logica para mostrar el error 
// lo cual permite luego agregar el mensaje
function removeErrorMessageForElem(elem: JQuery) {
    const elemErrorId = elem.attr("id") + "-error";
    const elemError = $("#" + elemErrorId);
    elemError.remove();
}
