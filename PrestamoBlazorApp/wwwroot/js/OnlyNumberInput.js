export const OnlyNumberInput = (inputName) => {
    try {
        const numberInput = document.getElementById(inputName);
        numberInput.addEventListener('keypress', (event) => {
            const keyCode = event.keyCode || event.which;
            const keyValue = String.fromCharCode(keyCode);
            if (!/^\d+$/.test(keyValue)) {
                event.preventDefault();
            }
        })
        return true;
    } catch (e) {
        console.log({ e });
    }

    return false;
}
