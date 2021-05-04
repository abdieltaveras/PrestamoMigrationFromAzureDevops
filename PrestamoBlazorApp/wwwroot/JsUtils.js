////$(document).ready(function () {
    
////    alert("ready");
////    $(".currency").inputmask('decimal', {
////        'alias': 'numeric',
////        'groupSeparator': ',',
////        'autoGroup': true,
////        'digits': 2,
////        'radixPoint': ".",
////        'digitsOptional': false,
////        'allowMinus': false,
////        'prefix': 'R$ ',
////        'placeholder': ''
////    });
////});

window.Alert = function (message) {
    if ((typeof message) === "string") {
        alert(message);
    }
    else {
        alert(JSON.stringify(message));
    }
}

window.Notification = function (message, delay) {

    $.notify(message, { autoHideDelay: delay });
    return true;
}
window.Confirm = function (message) {
    let result = false; 
    if (confirm(message)) {
        result = true;
    }
    return result;
}
window.ShowModal = function (selector) {
    let options = { backdrop: 'static' };
    $(selector).modal(options);
}
window.Reload = function (force) {
    window.location.reload(force);
}

window.scrollToBottom= function (ref) {
    ref.scrollTop = ref.scrollHeight;
}

window.FocusElementById= function (elemId)
{
    document.getElementById(elemId).focus();
}

window.SetInputMaskByElem = function (elemId, inputmask) {
    var selector = document.getElementById(elemId);
    var im = new Inputmask(inputmask);
    im.mask(selector);
}
window.SetInputMask = function () {
    Inputmask().mask(document.querySelectorAll(".masked"));
    var result = $(".currency").inputmask('currency', { rightAlign: true });
}
window.DivisionTerritorial = (Id) => {
    $("#codigo").val("0");
    console.log(searchTerritorio(Id));
}
window.sweetAlertSuccess = function(message,redirectTo = "") {
    Swal.fire({
        icon: 'success',
        title: message,
        allowOutsideClick: false,
        showConfirmButton: false,
        timer: 1500
        //text: 'Something went wrong!',
        //footer: '<a href>Why do I have this issue?</a>'
    }).then(function () {
        if (redirectTo != "") {
            window.location = redirectTo;
        } else {
            location.reload();
        }
       
    });
    return true;
}
window.SweetMessageBox = function (message,icon, redirectTo = "") {
    Swal.fire({
        icon: icon,
        title: message,
        allowOutsideClick: false,
        showConfirmButton: false,
        timer: 1500
        //text: 'Something went wrong!',
        //footer: '<a href>Why do I have this issue?</a>'
    }).then(function () {
        if (redirectTo != "") {
            window.location = redirectTo;
        } else {
            location.reload();
        }

    });
    return true;
}
window.BlockPage = function()
{
    $.blockUI({
        //fadeIn: 1000,
        message: 'Procesando Solicitud...',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
    return true;
}
window.UnBlockPage = function () {
    $.unblockUI({ fadeOut: 200 });
    return true;
}