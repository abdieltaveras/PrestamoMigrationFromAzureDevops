
class EdtPrestamo {

    formId: string;
    formulario: string;
    constructor() {
        this.formId = '#frmEdtUser';
        formulario = $("#frmEdtUser");
        initialViewState();
    }
    // set IdElement;
    //let dateFormat = 'en-GB';
    //len-GBet yearRangeForDP = "+0:+1";
    initialViewState();
    initialViewState() {
        if (elemIdUsuario.prop("value") <= 0) {
            //elemActivo.prop({ readOnly: true });
            elemBloqueado.prop({ readOnly: true });
        }
        onContraseñaExpiraChange();
        onLimitarVigenciaDeCuentaChange();
    }
    setFecha(elem: JQuery) {
        //let d = new Date();
        //let fechaTexto = '';
        //fechaTexto = elem.attr("mmddyyyy")
        //let fecha = new Date(fechaTexto);
        ////console.log("dia " + fecha.getDate());
        //let n = fecha.toLocaleDateString(dateFormat);
        //elem.val(n);
    }

    onChangeProp(elem: Element) {
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

    turnOnOffValidationOnElem(elem: JQuery) {
    }
}