class utils {

    static JqElemExist(elem: JQuery): boolean {
        return (elem.length>0)
    }

    static getSiteCulture(): string {
        return "es-DO";
    }
}
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