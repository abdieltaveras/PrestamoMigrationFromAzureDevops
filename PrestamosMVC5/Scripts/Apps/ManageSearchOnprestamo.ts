// esta funcion se hizo inicialmente para hacer ciertas pruebas directa, luego bryan incorporo los metodos
// para realizar busqueda mas ampliados, se dejo aqui para fines de consulta o algun posible uso
function getClientesByNumeroIdentificacion(numeroIdentificacion: string, fsetDataTo: Function) {
    const url = "/Clientes/BuscarPorNoIdentificacion"

    if (numeroIdentificacion.match(/\d/g).length > 0) {
        return $.get(url, { noIdentificacion: numeroIdentificacion },
            function (data) {
                fsetDataTo(data);
            })
            .fail(function () {
                //console.log("el documento de identidad digitado no esta registrado a ningun cliente"
            })
    }
}

function getGarantiaByNumeracion(numeracionGarantia: string, fsetDataTo: Function) {
    const url = "/Garantias/BuscarGarantias"
    if (numeracionGarantia.length > 0) {
        return $.get(url, { searchToText: numeracionGarantia },
            function (data) {
                fsetDataTo(data);
            })
            .fail(function () {
                console.error("no existen datos para la garantia");
            })
    }
}

function setDatosGarantiaIntoHtml(data: string, garantiaElem: JQuery)
{
    if (data!=='[]') {
        const garantia = JSON.parse(data)[0];        
        const detalles = JSON.parse(garantia.Detalles);
        //console.log(garantia, garantia.Detalles);
        garantiaElem.html(detalles.NoMaquina + " " + detalles.Ano + " " + detalles.Placa + " " + detalles.Matricula + " " +
            detalles.Descripcion);
        const elem = "<input type='hidden' id='Prestamo_IdGarantias_0' name='Prestamo.IdGarantias[0]' value='" + garantia.IdGarantia + "'>"
        $("#garantias").append(elem);
    }
    else {
        garantiaElem.html("garantia digitada no existe")
        $("#Prestamo_IdGarantias_0").remove();
    }
}


