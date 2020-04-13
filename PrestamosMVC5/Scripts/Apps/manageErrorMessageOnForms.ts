$(document).ready(function () {
    let validElem = $('#validationSummary');
    let mensajerErrorDiv = $('#mensajeError');
    var hayMensajesEnelValidationSummary = validElem.length > 0;
    if (hayMensajesEnelValidationSummary) {
        mensajerErrorDiv.addClass("alert alert-danger");
        setTimeout(function () {
            validElem.remove();
            //$('#validationSummary').remove();
            //$('#mensajeError').remove();
            mensajerErrorDiv.remove();
        }, 5000);
    }
    else {
        $('#mensajeError').remove();
    }
});

