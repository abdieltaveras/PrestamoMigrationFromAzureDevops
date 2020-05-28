var EdtPrestamo = /** @class */ (function () {
    function EdtPrestamo() {
        this.formId = '#frmEdtUser';
        formulario = $("#frmEdtUser");
        initialViewState();
    }
    EdtPrestamo.prototype.initialViewState = function () {
        if (elemIdUsuario.prop("value") <= 0) {
            //elemActivo.prop({ readOnly: true });
            elemBloqueado.prop({ readOnly: true });
        }
        onContraseñaExpiraChange();
        onLimitarVigenciaDeCuentaChange();
    };
    EdtPrestamo.prototype.setFecha = function (elem) {
        //let d = new Date();
        //let fechaTexto = '';
        //fechaTexto = elem.attr("mmddyyyy")
        //let fecha = new Date(fechaTexto);
        ////console.log("dia " + fecha.getDate());
        //let n = fecha.toLocaleDateString(dateFormat);
        //elem.val(n);
    };
    EdtPrestamo.prototype.onChangeProp = function (elem) {
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
    };
    EdtPrestamo.prototype.turnOnOffValidationOnElem = function (elem) {
    };
    return EdtPrestamo;
}());
//# sourceMappingURL=CreateOrEditPrestamo.js.map