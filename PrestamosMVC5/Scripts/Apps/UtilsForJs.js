$.fn.changeSelectVal = function (v) {
    if (v == null) { return; }
    return this.val(v).change();
}
$.fn.changeCheckVal = function (v) {
    if (v == null) { return; }
    const sonDiferentes = (this.val() !== v.toString());
    if (sonDiferentes) {
        this.val(v).click();
    }
    return this
}
