$.validator.unobtrusive.adapters.add(
    'max',
    ['max'],
    function (options) {
        options.rules['max'] = parseInt(options.params['max'], 10);
        options.messages['max'] = options.message;
    });

$.validator.unobtrusive.adapters.add(
    'min',
    ['min'],
    function (options) {
        options.rules['min'] = parseInt(options.params['min'], 10);
        options.messages['min'] = options.message;
    });