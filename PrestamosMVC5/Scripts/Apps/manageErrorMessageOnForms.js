$(document).ready(function () {
    var validElem = $('#validationSummary');
    var shertime = $("#showerrorstime").data("value");
    var mensajerErrorDiv = $('#mensajeError');
    shertime = (shertime.lenght = 0) ? 5000 : shertime * 1000;
    var hayMensajesEnelValidationSummary = validElem.length > 0;
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
//# sourceMappingURL=manageErrorMessageOnForms.js.map