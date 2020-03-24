initCheckValues();
function attachOnChangeEvent() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        var _this = this;
        $(this).on("change", function () { return setCheckValue($(_this)); });
    });
}
function initCheckValues() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        setCheckValue($(this));
    });
}
function setCheckValue(elemReceived) {
    //let elem = $('#' + elemReceived.id);
    var elemName = elemReceived.prop("name");
    //console.log("attached", elemName);
    var isChecked = elemReceived.is(':checked');
    var elems = $("input[name='" + elemName + "'");
    elems.each(function () {
        $(this).prop("value", isChecked);
    });
}
//# sourceMappingURL=CheckInput.js.map