$(document).ready(function () {
    var allDateElems = $('.datetype,input[type="datetime"],input[type="date"]');
    allDateElems.data("data-val", false);
    allDateElems.each(function () {
        setFecha($(this));
    });
    function setFecha(jQelem) {
        var fecha = toDate(jQelem.val());
        var formattedDate = moment(fecha).format("DD/MM/YYYY");
        jQelem.val(formattedDate);
    }
    // esto es para que no valide la fecha con las reglas que le envia el razor, 
    // asi evitamos que este saliendo el dialogo del datepicker 
    // al ejecutar un submit del formulario
    //allDateElems.rules('remove');
    // to validate methods with a rule that works right
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
    };
    // evitamos que el usuario ponga la fecha y cometa errores
    allDateElems.prop("readonly", true);
    allDateElems.datepicker({
        dateFormat: _dateFormat,
        timepicker: false,
        changeMonth: true,
        changeYear: true,
        changeDay: true,
        showButtonPanel: true,
        yearRange: yearRangeForDP,
        //altField: "#ValidFrom",
        //altFormat: "yy-mm-dd",
        onClose: function (dateText, inst) {
            //$(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectDay));
        }
    });
});
function convertUtcDateToFormat(date, format) {
    var fecha = moment.utc(date).format(format);
    return fecha;
}
function avoidFunctionsError() {
    convertUtcDateToFormat(null, null);
}
//# sourceMappingURL=DatePickerInit.js.map