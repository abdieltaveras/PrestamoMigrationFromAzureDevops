function validarFormulario() {
    // el monto no puede estar en 0
    var validaciones = [];
    let value = $("#@Html.IdFor(model => model.MontoAPrestar)").val()
    let valorMontoPrestado = toFloat(value);
    //if (isNaN(valorMontoPrestado)) valorMontoPrestado = 0;
    //console.log(valorMontoPrestado);
    $("#@Html.IdFor(model => model.Prestamo.MontoPrestado)").val(valorMontoPrestado);
    const montoAPrestar = parseFloat($("#@Html.IdFor(m => m.Prestamo.MontoPrestado)").val());
    const deudaRenovacion = parseFloat($("#@Html.IdFor(m => m.Prestamo.DeudaRenovacion)").val());
    const totalAPrestar = montoAPrestar + deudaRenovacion
    console.log(montoAPrestar, typeof (deudaRenovacion), totalAPrestar);
    if (totalAPrestar <= 0) {
        validaciones.push("no ha especificado monto a prestar")
    }
    if ($("#@Html.IdFor(model => model.Prestamo.IdCliente)").val() <= 0) {
        validaciones.push("no ha especificado cliente")
    }

    if (($("#@Html.IdFor(model => model.LlevaGarantia)").val()) === true) && (idGarantias.length = 0))
    {
        validaciones.push("no ha especificado Garantias y este tipo de prestamo lleva garantias")
    }

    if (($("#@Html.IdFor(model => model.IncluirRenovacion)").val()) === true) {
        alert("si");
        if ($("#@Html.IdFor(model => model.Prestamo.IdPrestamoARenovar)").val() <= 0) {
            validaciones.push("no ha especificado Prestamo a renovar")
        }
    }

    const fecha = $("#@Html.IdFor(model => model.Prestamo.FechaEmisionReal)").val()
    console.log(`la fecha del prestamo es ${fecha}`);
    if (validaciones.length > 0) {
        console.log(validaciones);
        alert("tiene varios errores revise");
    }
}