var utils = /** @class */ (function () {
    function utils() {
    }
    // para determinar si un elemento existe o no
    utils.JqElemExist = function (elem) {
        return (elem.length > 0);
    };
    // esta funcion retorna el valor de la cultura del site
    utils.getSiteCulture = function () {
        return "es-DO";
    };
    return utils;
}());
// convierte una fecha la cual segun la cultura establecida determina como descomponerla
function toDate(dateStr) {
    var _a;
    var culture = utils.getSiteCulture();
    var _b = ['0', '0', '0'], day = _b[0], month = _b[1], year = _b[2];
    switch (culture) {
        case "es-DO":
            _a = dateStr.split("/"), day = _a[0], month = _a[1], year = _a[2];
            break;
        default:
            break;
    }
    return new Date(Number(year), Number(month) - 1, Number(day));
}
// permite iterar un formulario serializado  y ver sus valores parecido al json
// de esta forma se puede entender mejor
function displaySerializedForm(formSerialized) {
    console.log(typeof formSerialized);
    var formData = formSerialized.split("&");
    var obj = {};
    for (var key in formData) {
        console.log(formData[key], key);
        obj[formData[key].split("=")[0]] = formData[key].split("=")[1];
    }
}
// busca en el XMLHttpRequest que devuelve como un html la cadena
// dentro de title que es donde se encuenta mensaje del error
function getErrorMessageFromResponseText(XMLHttpRequest) {
    var iniPos = XMLHttpRequest.responseText.search('<title>');
    var endPos = XMLHttpRequest.responseText.search('</title>');
    return XMLHttpRequest.responseText.substring(iniPos + 7, endPos);
}
// una funcion que simula un delay y ejecuta la funcion indicada despues de 
// terminado el delay
function delay(func, time) {
    if (time === void 0) { time = 3000; }
    var date = Date.now();
    var currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < time);
    func();
}
// una funcion que simula un delay y ejecuta la funcion indicada despues de 
// terminado el delay
function sleep(func, time) {
    if (time === void 0) { time = 3000; }
    delay(func, time);
}
// esta funcion invoca  un mensaje en pnotify
function showMessage(typeOfNotification, titleMessage, messageOfNotification, duration) {
    if (duration === void 0) { duration = 5000; }
    new PNotify({
        title: titleMessage,
        text: messageOfNotification,
        type: typeOfNotification,
        styling: 'jqueryui',
        delay: duration,
    });
}
//# sourceMappingURL=Utils.js.map
function avoidmessageFunctionIsDefinedButNeverUsed() {
    displaySerializedForm('');
    toDate('');
    delay(null, 0);
    sleep(null, 0);
    utils.getSiteCulture();
    utils.JqElemExist(null);
    getErrorMessageFromResponseText(null);
    showMessage('info', 'test', 'test');
}
//# sourceMappingURL=Utils.js.map