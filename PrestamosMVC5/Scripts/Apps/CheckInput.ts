function setCheckValue(elemReceived: JQuery) {
    //let elem = $('#' + elemReceived.id);
    const elemName = elemReceived.prop("name");
    //console.log("attached", elemName);
    const isChecked = elemReceived.is(':checked');
    const elems = $("input[name='" + elemName + "'");
    elems.each(function () {
        $(this).prop("value", isChecked);
    });
}

function attachOnChangeEventToCheckBoxes() {
    const checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        $(this).on("change", ()=>setCheckValue($(this)));
    });
}

function initCheckValues() {
    const checkBoxes = $("input:checkbox");
    checkBoxes.each(function () {
        setCheckValue($(this));
    });
}
initCheckValues();