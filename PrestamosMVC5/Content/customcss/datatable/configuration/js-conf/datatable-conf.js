let dataTableConfiguration = {
    dom:
        "<'row'<'col-sm-6'f><'col-sm-6'B>>" +
        "<'row'<'#loco.col-sm-6 text-center'><'col-sm-6'l>>" +
        "<'row'<'col-sm-12'tr>>" +
        "<'row'<'col-sm-6'i><'col-sm-6'p>>",
    lengthMenu: [
        [10, 25, 50, -1],
        ['10 Filas', '25 Filas', '50 Filas', 'Mostrar todas']
    ],
    responsive:true,
    
    "language": {
        "sProcessing": "Procesando...",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningún dato disponible en esta tabla =(",
        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Último",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        },
        "buttons": {
            "copy": "Copiar",
            "print": "Imprimir",
            "colvis": "Visibilidad",
            "pageLength": "Mostrar %d filas",
            "copyTitle": "Copiado en portapapeles",
            "copySuccess": "%d linea/s copiada/s",
        }
    },
    'columnDefs': [{
        orderable: false,
        className: 'select-checkbox',
        targets: 0
    }],
    'select': {
        style: 'os',
        selector: 'td:first-child'
    },
    'order': [[1, 'asc']],
}

function removeTemplateConfiguration() {
    $('.dt-buttons button').removeClass();
    $('.dt-buttons button').addClass('btn btn-primary');
    $('#datatable_paginate').removeClass('dataTables_paginate');

    $('.dt-button-collection div button').removeClass('dt-button button-page-length');
    $('.dt-button-collection div button').addClass('btn btn-primary');
}