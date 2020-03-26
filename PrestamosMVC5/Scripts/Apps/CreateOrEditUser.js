var formId = '#frmEdtUser';
var formulario = $("#frmEdtUser");
// set IdElement;
var idContraseñaText = "Contrase_a";
var idConfirmarContraseñaText = "ConfirmarContrase_a";
var idUsuarioDebeCambiarContraseñaAlIniciarSesionText = "Usuario_DebeCambiarContrase_aAlIniciarSesion";
var idLaContraseñaExpiraText = "LaContrase_aExpira";
var idUsuarioBloqueadoText = "Usuario_Bloqueado";
//let IdUsuarioActivoText = "Usuario_Activo";
var idContraseñaExpiraCadaXMesText = "Contrase_aExpiraCadaXMes";
var idLimitarVigenciaDeCuentaText = "LimitarVigenciaDeCuenta";
var idVigenteHastaText = "Usuario_VigenteHasta";
var idVigenteDesdeText = "Usuario_VigenteDesde";
var elemIdUsuario = $("#idUsuario");
var elemContraseña = $("#" + idContraseñaText);
var elemConfirmarContraseña = $("#" + idConfirmarContraseñaText);
var elemDebeCambiarContrasenaAlIniciarSesion = $("#" + idUsuarioDebeCambiarContraseñaAlIniciarSesionText);
var elemBloqueado = $("#" + idUsuarioBloqueadoText);
//let elemActivo = $("#" + IdUsuarioActivoText);
var elemContraseñaExpiraCadaXMes = $("#" + idContraseñaExpiraCadaXMesText);
var elemContraseñaExpira = $('#' + idLaContraseñaExpiraText);
var elemLimitarVigenciaDeCuenta = $("#" + idLimitarVigenciaDeCuentaText);
var elemVigenteHasta = $("#" + idVigenteHastaText);
var elemVigenteDesde = $("#" + idVigenteDesdeText);
var equalToValue = elemConfirmarContraseña.attr("data-val-equalto");
var equalToOtherValue = elemConfirmarContraseña.attr("data-val-equalto-other");
//let dateFormat = 'en-GB';
//let yearRangeForDP = "+0:+1";
initialViewState();
$('.collapsed').css('height', 'auto');
$('.collapsed').find('.x_content').css('display', 'none');
$('.collapsed').find('i').toggleClass('fa-chevron-up fa-chevron-down');
function initialViewState() {
    if (elemIdUsuario.prop("value") <= 0) {
        //elemActivo.prop({ readOnly: true });
        elemBloqueado.prop({ readOnly: true });
    }
    onContraseñaExpiraChange();
    onLimitarVigenciaDeCuentaChange();
}
var dateFormat = 'en-GB';
var yearRangeForDP = "+0:+1";
var _dateFormat = 'dd/mm/yy';
function setFecha(elem) {
    var d = new Date();
    var fechaTexto = '';
    //if (elem.prop("id") == idVigenteHastaText) {
    //    fechaTexto = elemVigenteHasta.attr("mmddyyyy")
    //}
    //if (elem.prop("id") == idVigenteDesdeText) {
    //    fechaTexto = elemVigenteDesde.attr("mmddyyyy")
    //}
    console.log(elem.attr("mmddyyyy"));
    fechaTexto = elem.attr("mmddyyyy");
    var fecha = new Date(fechaTexto);
    var n = fecha.toLocaleDateString(dateFormat);
    elem.val(n);
}
function onChangeProp(elem) {
    var elemId = elem.id;
    switch (elemId) {
        case idUsuarioDebeCambiarContraseñaAlIniciarSesionText:
            onCambiarContraseñaAlIniciarSesion();
            break;
        case idLaContraseñaExpiraText:
            onContraseñaExpiraChange();
            break;
        case idLimitarVigenciaDeCuentaText:
            onLimitarVigenciaDeCuentaChange();
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
    var result = (elemDebeCambiarContrasenaAlIniciarSesion.is(':checked'));
    if (result) {
        elemContraseña.attr("data-val", "false");
        elemConfirmarContraseña.attr("data-val", "false");
        var elemContraseñaError = $("#Contrase_a-error");
        var elemConfirmarContraseñaError = $("#ConfirmarContrase_a-error");
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
    var contraseñaExpira = elemContraseñaExpira.is(':checked');
    //elemContraseñaExpira.prop({ value: contraseñaExpira });
    contraseñaExpira ? elemContraseñaExpiraCadaXMes.show() : elemContraseñaExpiraCadaXMes.hide();
    // elemContraseñaExpiraCadaXMes.removeAttr("readonly");
    var elemTextoContraseñaExpiraCadaXMes = $("textoContraseñaExpiraCadaXMes");
    // *** elemContraseñaExpira.prop({value: contraseñaExpira });
    var elemTextoContraseñaExpira = $("#textoContraseñaExpira");
    if (contraseñaExpira) {
        elemTextoContraseñaExpira.hide();
    }
    else {
        elemTextoContraseñaExpira.text("Nunca Expira");
        elemTextoContraseñaExpira.show();
    }
    ;
}
function onLimitarVigenciaDeCuentaChange() {
    var limitarVigencia = elemLimitarVigenciaDeCuenta.is(':checked');
    //elemLimitarVigenciaDeCuenta.prop({ value: limitarVigencia });
    limitarVigencia ? elemVigenteHasta.show() : elemVigenteHasta.hide();
    limitarVigencia ? elemVigenteDesde.show() : elemVigenteDesde.hide();
    var elemTextoVigenteDesde = $("#textoVigenteDesde");
    var elemTextoVigenteHasta = $("#textoVigenteHasta");
    if (limitarVigencia) {
        elemTextoVigenteDesde.hide();
        elemTextoVigenteHasta.hide();
    }
    else {
        elemTextoVigenteDesde.text("No esta limitada");
        elemTextoVigenteHasta.text("No esta limitada");
        elemTextoVigenteDesde.show();
        elemTextoVigenteHasta.show();
    }
    ;
    // *** elemLimitarVigenciaDeCuenta.prop({ value: limitarVigencia });
}
function turnOnOffValidations(elem) {
    var propName = elem.prop("name");
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
//# sourceMappingURL=CreateOrEditUser.js.map