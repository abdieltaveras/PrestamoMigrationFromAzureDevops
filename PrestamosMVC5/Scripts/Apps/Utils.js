var utils = /** @class */ (function () {
    function utils() {
    }
    utils.JqElemExist = function (elem) {
        return (elem.length > 0);
    };
    utils.getSiteCulture = function () {
        return "es-DO";
    };
    return utils;
}());
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
//# sourceMappingURL=Utils.js.map