initCheckValues();
function attachOnChangeEventToCheckBoxes() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        $(this).on("change", ()=>setCheckValue($(this)));
    });
}

function initCheckValues() {
    var checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        setCheckValue($(this));
    });
}

function setCheckValue(elemReceived: JQuery)
{
    //let elem = $('#' + elemReceived.id);
    let elemName = elemReceived.prop("name");
    //console.log("attached", elemName);
    let isChecked = elemReceived.is(':checked');
    let elems = $("input[name='" + elemName + "'");
    elems.each(function () {
        $(this).prop("value", isChecked);
    });
}
