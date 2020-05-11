// to clean error message when user input value
// need to be used togehter with <div class="invalid-feedback">
// that is when using bootstrap 4 show hide validation message
$(".remove-error").keypress(function () {
    if (this.value.length === 0) {
        var jQElem = $(this);
        jQElem.removeClass('is-invalid');
    }
});
$(document).ready(function () {
    var validElem = $('#validationSummary');
    var shertime = $("#showerrorstime").data("value");
    var mensajerErrorDiv = $('#mensajeError');
    shertime = (shertime.lenght === 0) ? 5000 : shertime * 1000;
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
function setSpanErrorForElem(elemName, errorMessage) {
    var spanToSearch = 'span[data-valmsg-for="' + elemName + '"]';
    var spanForError = $(spanToSearch);
    spanForError.html("<span id='" + elemName + "-error' class=''>" + errorMessage + "</span>");
}
function removeSpanErrorForElem(elemName) {
    var spanToSearch = 'span[data-valmsg-for="' + elemName + '"]';
    var spanForError = $(spanToSearch);
    spanForError.remove();
}
function removeErrorMessageForElem(elem) {
    var elemErrorId = elem.attr("id") + "-error";
    var elemError = $("#" + elemErrorId);
    elemError.remove();
}
//# sourceMappingURL=manageErrorMessageOnForms.js.map