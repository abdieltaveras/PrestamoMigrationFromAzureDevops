initCheckValues();
//removeHiddenCheckBoxes();

function removeHiddenCheckBoxes() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        elemName = $(this).prop("name");
        alert(elemName);
        let hiddenCheck = $("input[name='" + elemName + "'][type='hidden']");
        $("input[name='" + elemName + "']").remove("[type='hidden']");
    });
}
function initCheckValues() {
    console.log("inicialiando");
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        let elem = $(this);
        let elemFirst = $('#' + elem.prop("name"));
        let value = elemFirst.is(":checked");
        console.log(value);
        elem.prop("value", value);
        if (value) {
            elem.prop("checked", true);
        }
        else {
            elem.removeAttr("checked");
        }
    });
}

function setCheckValue(elemReceived) {
    let elem = $('#' + elemReceived.id);
    let elemName = elemReceived.name;
    let value = elem.is(':checked');
    //alert(elemName);
    let elems = $("input[name='" + elemName + "'");
    elems.each(function () {
        $(this).attr("value", value);
    });
}