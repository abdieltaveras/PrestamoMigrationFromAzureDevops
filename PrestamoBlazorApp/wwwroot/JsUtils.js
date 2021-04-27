
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
}
window.DivisionTerritorial = (Id)=> {
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