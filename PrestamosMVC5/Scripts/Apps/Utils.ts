class utils {

    // para determinar si un elemento existe o no
    static JqElemExist(elem: JQuery): boolean {
        return (elem.length>0)
    }

    // esta funcion retorna el valor de la cultura del site
    static getSiteCulture(): string {
        return "es-DO";
    }
}

// convierte una fecha la cual segun la cultura establecida determina como descomponerla
function toDate(dateStr: string): Date {
    const culture = utils.getSiteCulture();
    let [day, month, year] = ['0', '0', '0'];
    switch (culture) {
        case "es-DO":
            [day, month, year] = dateStr.split("/")
            break;
        default:
            break;
    }
    return new Date(Number(year), Number(month) - 1, Number(day))
}

// permite iterar un formulario serializado  y ver sus valores parecido al json
// de esta forma se puede entender mejor
function displaySerializedForm(formSerialized) {
    console.log(typeof formSerialized);
    const formData = formSerialized.split("&");
    const obj = {};
    for (const key in formData) {
        console.log(formData[key], key);
        obj[formData[key].split("=")[0]] = formData[key].split("=")[1];
    }
}

// busca en el XMLHttpRequest que devuelve como un html la cadena
// dentro de title que es donde se encuenta mensaje del error
function getErrorMessageFromResponseText(XMLHttpRequest: JQueryXHR): string {
    const iniPos = XMLHttpRequest.responseText.search('<title>');
    const endPos = XMLHttpRequest.responseText.search('</title>');
    return XMLHttpRequest.responseText.substring(iniPos + 7, endPos);
} 

// una funcion que simula un delay y ejecuta la funcion indicada despues de 
// terminado el delay
function delay(func: Function, time = 3000) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < time);
    func();
}


// una funcion que simula un delay y ejecuta la funcion indicada despues de 
// terminado el delay
function sleep(func: Function, time = 3000) {
    delay(func, time);

}

// esta funcion invoca  un mensaje en pnotify

function showMessage(typeOfNotification, titleMessage, messageOfNotification, duration = 5000) {
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
    toDate('')
    delay(null,0)
    sleep(null,0)
    utils.getSiteCulture();
    utils.JqElemExist(null);
    getErrorMessageFromResponseText(null);
    showMessage('info', 'test', 'test')
    
}