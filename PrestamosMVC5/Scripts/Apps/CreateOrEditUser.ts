let formId = '#frmEdtUser';
let formulario = $("#frmEdtUser");
// set IdElement;
let idContraseñaText = "Contrase_a";
let idConfirmarContraseñaText = "ConfirmarContrase_a";
let idUsuarioDebeCambiarContraseñaAlIniciarSesionText = "Usuario_DebeCambiarContrase_aAlIniciarSesion";
let idLaContraseñaExpiraText = "LaContrase_aExpira";
let idUsuarioBloqueadoText = "Usuario_Bloqueado";
let IdUsuarioActivoText = "Usuario_Activo";
let idContraseñaExpiraCadaXMesText = "Contrase_aExpiraCadaXMes";
let idLimitarVigenciaDeCuentaText = "LimitarVigenciaDeCuenta";
let idVigenteHastaText = "Usuario_VigenteHasta";
let idVigenteDesdeText = "Usuario_VigenteDesde";

let elemIdUsuario = $("#idUsuario");
let elemContraseña = $("#" + idContraseñaText);
let elemConfirmarContraseña = $("#" + idConfirmarContraseñaText);
let elemDebeCambiarContrasenaAlIniciarSesion = $("#" + idUsuarioDebeCambiarContraseñaAlIniciarSesionText);
let elemBloqueado = $("#" + idUsuarioBloqueadoText)
let elemActivo = $("#" + IdUsuarioActivoText);
let elemContraseñaExpiraCadaXMes = $("#" + idContraseñaExpiraCadaXMesText);
let elemContraseñaExpira = $('#' + idLaContraseñaExpiraText);
let elemLimitarVigenciaDeCuenta = $("#" + idLimitarVigenciaDeCuentaText);
let elemVigenteHasta = $("#" + idVigenteHastaText);
let elemVigenteDesde = $("#" + idVigenteDesdeText);
let equalToValue = elemConfirmarContraseña.attr("data-val-equalto");
let equalToOtherValue = elemConfirmarContraseña.attr("data-val-equalto-other");
let dateFormat = 'en-GB';
let yearRangeForDP = "+0:+1";
initialViewState();
$('.collapsed').css('height', 'auto');
$('.collapsed').find('.x_content').css('display', 'none');
$('.collapsed').find('i').toggleClass('fa-chevron-up fa-chevron-down');

function initialViewState() {
    if (elemIdUsuario.prop("value")<= 0)
    {
        elemActivo.prop({ readOnly: true });
        elemBloqueado.prop({ readOnly: true });
    }
    onContraseñaExpiraChange();
    onLimitarVigenciaDeCuentaChange();
}




function setFecha(elem, name: string) {
    let d = new Date();
    let fecha = new Date();
    
    if (elem.prop("id") == idVigenteHastaText) {
        fecha = elemVigenteHasta.prop("value") as Date;
    }
    if (elem.prop("id") == idVigenteDesdeText) {
        fecha = elemVigenteDesde.prop("value") as Date;
    }
    let n = fecha.toLocaleDateString(dateFormat);
    elem.val(n);
    //elem.val(fecha);

    //function extractDate(fecha: Date): Date {
    //    let result = new Date().setFullYear(fecha.getFullYear(), fecha.getMonth());
    //    //, fecha.getDay());
    //}
}

function onChangeProp(elem) {
    let elemId = elem.id;
    switch (elemId) {
        case idUsuarioDebeCambiarContraseñaAlIniciarSesionText:
            onCambiarContraseñaAlIniciarSesion();
            break;
        case idLaContraseñaExpiraText:
            onContraseñaExpiraChange();
            break;
        case idLimitarVigenciaDeCuentaText:
            onLimitarVigenciaDeCuentaChange()
            break;
    }
}

//function onUsuarioActivoChange() {
//    let valor = elemActivo.is(':checked');
//    elemActivo.prop({ value: valor });
//    $("#ForActivo").prop({ value: valor });

//}

//function onUsuarioBloqueadoChange() {
//    let valor = elemBloqueado.is(':checked');
//    elemBloqueado.prop({ value: valor });
//    $("#ForBloqueado").prop({ value: valor });
//    console.log($("#ForBloqueado").prop("value"));
//}

function onCambiarContraseñaAlIniciarSesion() {
    var result = (elemDebeCambiarContrasenaAlIniciarSesion.is(':checked'))

    
    if (result) {
        elemContraseña.attr("data-val", "false");
        elemConfirmarContraseña.attr("data-val", "false");
        var elemContraseñaError = $("#Contrase_a-error");
        var elemConfirmarContraseñaError = $("#ConfirmarContrase_a-error")
        elemContraseñaError.text("");
        elemConfirmarContraseñaError.text("");
    }
    else {
        elemContraseña.attr("data-val", "true");
        elemConfirmarContraseña.attr("data-val", "true");
    }
    //elemDebeCambiarContrasenaAlIniciarSesion.prop({ value: result });
    //$("#ForCambiarContrase_aAlIniciarSesion").prop({ value: result });
    elemContraseña.prop('disabled', result);
    elemConfirmarContraseña.prop('disabled', result);
}

function onContraseñaExpiraChange() {
    let contraseñaExpira = elemContraseñaExpira.is(':checked')
    //elemContraseñaExpira.prop({ value: contraseñaExpira });
    contraseñaExpira ? elemContraseñaExpiraCadaXMes.show() : elemContraseñaExpiraCadaXMes.hide();
    // elemContraseñaExpiraCadaXMes.removeAttr("readonly");
    let elemTextoContraseñaExpiraCadaXMes = $("textoContraseñaExpiraCadaXMes");
    // *** elemContraseñaExpira.prop({value: contraseñaExpira });
    let elemTextoContraseñaExpira = $("#textoContraseñaExpira");
    if (contraseñaExpira) { elemTextoContraseñaExpira.hide() }
    else {
        elemTextoContraseñaExpira.text("Nunca Expira");
        elemTextoContraseñaExpira.show();
    };
}
function onLimitarVigenciaDeCuentaChange() {
    let limitarVigencia = elemLimitarVigenciaDeCuenta.is(':checked');
    //elemLimitarVigenciaDeCuenta.prop({ value: limitarVigencia });
    limitarVigencia ? elemVigenteHasta.show() : elemVigenteHasta.hide();
    limitarVigencia ? elemVigenteDesde.show() : elemVigenteDesde.hide();
    let elemTextoVigenteDesde = $("#textoVigenteDesde");
    let elemTextoVigenteHasta = $("#textoVigenteHasta");
    if (limitarVigencia) {
        elemTextoVigenteDesde.hide();
        elemTextoVigenteHasta.hide();
    }
    else {
        elemTextoVigenteDesde.text("No esta limitada");
        elemTextoVigenteHasta.text("No esta limitada");
        elemTextoVigenteDesde.show();
        elemTextoVigenteHasta.show();
    };
    // *** elemLimitarVigenciaDeCuenta.prop({ value: limitarVigencia });
}


function turnOnOffValidations(elem: JQuery) {
    let propName = elem.prop("name");
    if (propName === "Contraseña" || propName === "ConfirmarContraseña") {
        if (elemDebeCambiarContrasenaAlIniciarSesion.is(':checked')) {
            elem.rules('remove', 'required');
        }
        else {
            elem.rules('add', 'required');
            if (propName === "ConfirmarContraseña") {

            }
        }
    }
}
