// para el manejo de las teclas enter y los cursores
// requisito debe tener esta linea en el script de la pagina
// let inputElems = $(".text-box, select");

$(function () {
    // var to hold all inputelems
    let inputElems = $("[data-input='true']");
    if (inputElems.length === 0) {
        alert("keyBehavior.js informa que debe agregar el atributo [data-input=true] a los elementos input del form, para que esta rutina pueda funcionar");
        return;
    }
    // keys to manage 
    let keyEnter = 13;
    let keyUpArrow = 38;
    let keyDownArrow = 40;
    // collection to relata an index with the id of the input
    let inputElemsIndexById: JQuery;
    // fill var of input elements 
    inputElems.each(function () {
        inputElemsIndexById.push(this.id);
    });

    // todo 20191026 eliminar si no es necesario let inputCounter = inputElems.length;
    let currentField = 0;
    // to set currentField index when execute this function
    inputElems.focusin(function () {
        currentField = inputElemsIndexById.findIndex(elem => elem === this.id);
    });
    // attach function to events
    inputElems.on('keydown', keyDown).on('keyup', keyUp);

    // when a key to go ahead is pressed
    function keyDown(e) {
        if (e.which === keyEnter || e.which === keyDownArrow) {
            // if it is not the last input will advance (incrementing currentfield value)
            // and position next field else will keep on last index
            if (currentField < inputElems.length) {  currentField++; }
            else { currentField = inputElems.length;}
            moveToField(currentField);
        }
    }
    // when a key to go back is pressed
    function keyUp(key) {
        if (key.which === keyUpArrow) {
            // if it is not the firt input will go back (decrementing currentfield value)
            // and position before field else will keep on first index
            if (currentField > 0) { currentField--; }
            else { currentField = 0; }
        }
        moveToField(currentField);
    }
    // to focus the field indicated by fieldNumber
    function moveToField(fieldNumber: number) {
        let fieldToFocus = $("#" + inputElemsIndexById[fieldNumber]);
        fieldToFocus.focus();
        event.preventDefault();
    }
});