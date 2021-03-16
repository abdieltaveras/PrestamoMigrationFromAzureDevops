window.Alert = function (message) {
    if ((typeof message) === "string") {
        alert(message);
    }
    else {
        alert(JSON.stringify(message));
    }

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

