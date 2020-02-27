var formId = '#frmEdtUser';
var formulario = $("#frmEdtUser");
// set IdElement;
var idContraseñaText = "Contrase_a";
var idConfirmarContraseñaText = "ConfirmarContrase_a";
var idUsuarioDebeCambiarContraseñaAlIniciarSesionText = "Usuario_DebeCambiarContrase_aAlIniciarSesion";
var idLaContraseñaExpiraText = "LaContrase_aExpira";
var idUsuarioBloqueadoText = "Usuario_Bloqueado";
var IdUsuarioActivoText = "Usuario_Activo";
var idContraseñaExpiraCadaXMesText = "Contrase_aExpiraCadaXMes";
var idLimitarVigenciaDeCuentaText = "LimitarVigenciaDeCuenta";
var idVigenteHastaText = "Usuario_VigenteHasta";
var idVigenteDesdeText = "Usuario_VigenteDesde";
var elemIdUsuario = $("#idUsuario");
var elemContraseña = $("#" + idContraseñaText);
var elemConfirmarContraseña = $("#" + idConfirmarContraseñaText);
var elemDebeCambiarContrasenaAlIniciarSesion = $("#" + idUsuarioDebeCambiarContraseñaAlIniciarSesionText);
var elemBloqueado = $("#" + idUsuarioBloqueadoText);
var elemActivo = $("#" + IdUsuarioActivoText);
var elemContraseñaExpiraCadaXMes = $("#" + idContraseñaExpiraCadaXMesText);
var elemContraseñaExpira = $('#' + idLaContraseñaExpiraText);
var elemLimitarVigenciaDeCuenta = $("#" + idLimitarVigenciaDeCuentaText);
var elemVigenteHasta = $("#" + idVigenteHastaText);
var elemVigenteDesde = $("#" + idVigenteDesdeText);
var equalToValue = elemConfirmarContraseña.attr("data-val-equalto");
var equalToOtherValue = elemConfirmarContraseña.attr("data-val-equalto-other");
var dateFormat = 'en-GB';
var yearRangeForDP = "+0:+1";
initialViewState();
$('.collapsed').css('height', 'auto');
$('.collapsed').find('.x_content').css('display', 'none');
$('.collapsed').find('i').toggleClass('fa-chevron-up fa-chevron-down');
function removeDuplicateInputsCreateByRazor() {
    $("input[type=hidden][value=false]").each(function () {
        var name = $(this).prop("name");
        var count = $("input[name='" + name + "']");
        if (count.length > 1) {
            $(this).remove();
        }
    });
}
function initialViewState() {
    //removeDuplicateInputsCreateByRazor();
    if (elemIdUsuario.prop("value") <= 0) {
        elemActivo.prop({ readOnly: true });
        elemBloqueado.prop({ readOnly: true });
    }
    initInputs();
}
function initInputs() {
    setInitialCheckedValues();
    //todo: revisar esta linea viene del razor
    //if (@Model.ShowAdvancedOptions.ToString().ToLower()) {
    onUsuarioActivoChange();
    onUsuarioBloqueadoChange();
    onCambiarContraseñaAlIniciarSesion();
    onContraseñaExpiraChange();
    onLimitarVigenciaDeCuentaChange();
    //}
}
function setInitialCheckedValues() {
    //elemLimitarVigenciaDeCuenta.prop({ value: valor });
    setInitValueForCheckAndValueProp(elemDebeCambiarContrasenaAlIniciarSesion, "@Model.Usuario.DebeCambiarContraseñaAlIniciarSesion");
    setInitValueForCheckAndValueProp(elemActivo, "@Model.Usuario.Activo");
    setInitValueForCheckAndValueProp(elemBloqueado, "@Model.Usuario.Bloqueado");
    setInitValueForCheckAndValueProp(elemContraseñaExpira, "@Model.LaContraseñaExpira");
    setInitValueForCheckAndValueProp(elemLimitarVigenciaDeCuenta, "@Model.LimitarVigenciaDeCuenta");
}
function setInitValueForCheckAndValueProp(elem, _value) {
    _value = _value.toLowerCase();
    elem.prop(":checked", _value);
    elem.prop({ value: _value });
}
function setFecha(elem, name) {
    alert("setfecha2");
    var d = new Date();
    var fecha = new Date();
    if (elem.prop("id") == idVigenteHastaText) {
        fecha = elemVigenteHasta.prop("value");
    }
    if (elem.prop("id") == idVigenteDesdeText) {
        fecha = elemVigenteDesde.prop("value");
    }
    var n = fecha.toLocaleDateString(dateFormat);
    elem.val(n);
    //elem.val(fecha);
    //function extractDate(fecha: Date): Date {
    //    let result = new Date().setFullYear(fecha.getFullYear(), fecha.getMonth());
    //    //, fecha.getDay());
    //}
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
        case IdUsuarioActivoText:
            onUsuarioActivoChange();
            break;
        case idUsuarioBloqueadoText:
            onUsuarioBloqueadoChange();
        case "verMasOpciones":
            break;
    }
}
function onUsuarioActivoChange() {
    var valor = elemActivo.is(':checked');
    elemActivo.prop({ value: valor });
    $("#ForActivo").prop({ value: valor });
}
function onUsuarioBloqueadoChange() {
    var valor = elemBloqueado.is(':checked');
    elemBloqueado.prop({ value: valor });
    $("#ForBloqueado").prop({ value: valor });
    console.log($("#ForBloqueado").prop("value"));
}
function onCambiarContraseñaAlIniciarSesion() {
    var result = (elemDebeCambiarContrasenaAlIniciarSesion.is(':checked'));
    //alert(result);
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
    elemDebeCambiarContrasenaAlIniciarSesion.prop({ value: result });
    $("#ForCambiarContrase_aAlIniciarSesion").prop({ value: result });
    elemContraseña.prop('disabled', result);
    elemConfirmarContraseña.prop('disabled', result);
}
function onContraseñaExpiraChange() {
    var contraseñaExpira = elemContraseñaExpira.is(':checked');
    elemContraseñaExpira.prop({ value: contraseñaExpira });
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
    elemLimitarVigenciaDeCuenta.prop({ value: limitarVigencia });
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