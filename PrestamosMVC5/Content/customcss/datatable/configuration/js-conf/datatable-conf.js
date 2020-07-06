let dataTableConfiguration = {
    dom:
        "<'#r1.row'<'col-sm-6'f><'#r1s2.col-sm-6'B>>" +
        "<'#r2.row'<'#r2s1.col-sm-2'> <'#r2s2.col-sm-4'l> <'#r2s3.col-sm-6'>>" +
        "<'#r3.row'<'col-sm-12'tr>>" +
        "<'#r4.row'<'col-sm-6'i><'col-sm-6'p>>",
    lengthMenu: [
        [10, 25, 50, -1],
        ['10 Filas', '25 Filas', '50 Filas', 'Mostrar todas']
    ],    
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
    
    buttons: [
        'copy', 'pdfHtml5', 'excel', 'print',
        {
            text: '<i class="fa fa-edit"></i>',
                attr: {
                title: 'Editar',
                    id: 'btn-edit',
                        class: ''
            },
            action: function () {
                onEditButton();
            }
        },
        {
            text: '<i class="fa fa-ban"></i>',
                attr: {
                title: 'Desactivar',
                    id: 'btn-deactivate',
                        class: ''
            },
            action: async function () {
                onDeactivateButton();
            }
            },
        {
            text: '<i class="fa fa-trash"></i>',
                attr: {
                title: 'Anular',
                    id: 'btn-cancel',
                        class: ''
            },
            action: async function () {
                onCancelButton();
            }
        },
        {
            text: 'Agregar <i class="fa fa-plus"></i>',
                attr: {
                title: 'Guardar',
                    id: 'btn-save'
            },
            action: function () {
                onNewButton();
            }
        }
    ]
        
}

function removeTemplateConfiguration() {
    $('.dt-buttons button').removeClass();
    $('.dt-buttons button').addClass('btn btn-primary');
    $('#datatable_paginate').removeClass('dataTables_paginate');

    $('.dt-button-collection div button').removeClass('dt-button button-page-length');
    $('.dt-button-collection div button').addClass('btn btn-primary');

    $('#btn-edit').appendTo('#r2s1');
    $('#btn-deactivate').appendTo('#r2s1');
    $('#btn-cancel').appendTo('#r2s1');
    $('#btn-save').appendTo('#r2s3');
}