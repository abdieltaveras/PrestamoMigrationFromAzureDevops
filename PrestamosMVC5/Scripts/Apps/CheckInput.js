initCheckValues();
function attachOnChangeEvent() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        var _this = this;
        $(this).on("change", function () { return setCheckValue(_this); });
    });
}
function initCheckValues() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        var elem = document.getElementById($(this).prop("id"));
        setCheckValue(elem);
    });
}
function setCheckValue(elemReceived) {
    var elem = $('#' + elemReceived.id);
    var elemName = elemReceived.getAttribute("name");
    console.log("attached", elemName);
    var isChecked = elem.is(':checked');
    var elems = $("input[name='" + elemName + "'");
    elems.each(function () {
        $(this).prop("value", isChecked);
    });
}
//function removeHiddenCheckBoxes() {
//    var checkBoxes = $("input:checkbox");
//    checkBoxes.each(function () {
//        let elemName = $(this).prop("name");
//        alert(elemName);
//        let hiddenCheck = $("input[name='" + elemName + "'][type='hidden']");
//        $("input[name='" + elemName + "']").remove("[type='hidden']");
//    });
//}
//# sourceMappingURL=CheckInput.js.map