initCheckValues();
function attachOnChangeEvent() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        $(this).on("change", ()=>setCheckValue(this));
    });
}

function initCheckValues() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        let elem = document.getElementById($(this).prop("id"));
        setCheckValue(elem);
    });
}

function setCheckValue(elemReceived: HTMLElement)
{
    let elem = $('#' + elemReceived.id);
    let elemName = elemReceived.getAttribute("name");
    console.log("attached", elemName);
    let isChecked = elem.is(':checked');
    let elems = $("input[name='" + elemName + "'");
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