function ApplyInputMask() {
    var elementsFound = $("input[data-tel='true']");
    elementsFound.each(function () {
        $(this).attr("data-inputmask", "'mask' : '(999) 999-9999'");
    });
}
ApplyInputMask();
//# sourceMappingURL=setMask.js.map