$(document).ready(function () {
    var validElem = $('#validationSummary');
    var mensajerErrorDiv = $('#mensajeError');
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
//# sourceMappingURL=manageErrorMessageOnForms.js.map