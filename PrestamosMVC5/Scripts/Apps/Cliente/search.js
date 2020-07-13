// Buscador de Clientes

let clienteRes;

$(".cliente_target").keyup(function (e) {
    console.log('no lo podia creer', e.which)
    if (e.which != 38 && e.which != 40 && e.which != 13) {
        $('.clientes').remove();

        searchClienteText($('#search-cliente-input').val());
    }
});
$(".cliente_target").keydown(function (e) {
    console.log('no lo podia creer', e.which)
     if (e.which === 13) {
        onClienteEnter();
    }
    console.log($('#searchinput').val());
});

async function searchClienteText(cliente) {
    console.log('Encontre', cliente);
    try {
        clienteRes = await searchCliente(cliente);

        showListCliente(JSON.parse(clienteRes));
        console.log('Activo', clienteRes)
        setClickListenerOnCliente();
        //console.log(res);
    } catch (err) {

    }
}

function searchCliente(location) {

    let dataValue = { "searchtotext": location };
    let LocalidadABuscar;

    return $.ajax({
        type: "get",
        url: "/Clientes/BuscarClientes",
        data: dataValue,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}


// Funcion que asignara a cada item de la lista una funcion
function setClickListenerOnCliente() {
    const list = document.querySelectorAll('.clientes');
    list.forEach(function (btn) {
        btn.addEventListener('click', async function (evt) {

            onClienteClick(evt);
            removeList();
        });
    });
}

document.onkeyup = function (e) {
    if (e.which == 38) {
        //if ($('#search-cliente-input').is(':focus')) {
            if (selectPointer != 0) {
                selectPointer--;
                $('[data-order="' + (selectPointer + 1) + '"]').removeClass('active');
                $('[data-order="' + selectPointer + '"]').addClass('active');
            }
        //}
    } else if (e.which == 40) {
        //$('#input-client_search').focus();
        //if ($('#search-cliente-input').is(':focus')) {
            if ($('.list-group-item').length != selectPointer) {
                selectPointer++;
                $('[data-order="' + (selectPointer - 1) + '"]').removeClass('active');
                $('[data-order="' + selectPointer + '"]').addClass('active');
            }
        //}

    } else if (e.ctrlKey && e.which == 66) {
        $('#search-cliente-input').focus();
    }
    console.log('puntero', selectPointer)
};

function showListCliente(list) {
    let count = 1;
    selectPointer = 0;
    console.log('lista', list);
    if (list.length > 0) {

        $.each(list, function (index, value) {

            $("#list-clientes-tab").append(` <p class="list-group-item clientes list-group-item-action" data-order="${count}" id="listclientes" data-toggle="list"
                        data-idCliente="${value.IdCliente}"
                        href="#list-home" role="tab" aria-controls="home">
                        <strong id="placeName" > ${value.Nombres} ${value.Apellidos} </strong><br>
                        <strong id="placeName" > ${value.NoIdentificacion} </strong><br>
                        <br></p>`);

            //if (value.IdClasificacion === 1) {
            //    $("#list-address-tab").append(` <p class="list-group-item clientes list-group-item-action" data-order="${count}" id="listaddresses" data-toggle="list"
            //            data-idCliente="${value.IdCliente}"
            //            data-idlocalidad="" href="#list-home" role="tab" aria-controls="home">
            //            <span class=" glyphicon glyphicon-home "></span>
            //            <strong id="placeName" > ${value.NoIdentificacion} </strong><br>
            //            ${Detalles.Descripcion !== null ? Detalles.Descripcion : ''}
            //            ${Detalles.Medida !== null ? 'Medida: ' + Detalles.Medida : ''}
            //            <br></p>`);
            //} else {
            //    $("#list-address-tab").append(` <p class="list-group-item clientes list-group-item-action" data-order="${count}" id="listaddresses" data-toggle="list"
            //            data-idCliente="${value.IdCliente}"
            //            role="tab" aria-controls="home">
            //            <span class="fa fa-automobile"></span>
            //            <strong id="placeName" > ${value.NoIdentificacion} </strong><br>
            //           <i> ${Detalles.Descripcion !== null ? Detalles.Descripcion + ', ' : ''}</i><strong> Año:</strong> ${Detalles.Ano} <strong> Placa:</strong> ${Detalles.Placa} <strong> Maquina:</strong> ${Detalles.NoMaquina}
            //            <br></p>`);
            //}
            count++;
        });
    } else {
        $("#list-clientes-tab").append(` <p class="list-group-item clientes list-group-item-action" id="listclientes" data-toggle="list"
                        role="tab" aria-controls="home">
                       <span">No se encontro información</span>
                        <br></p>`);
    }
}

function removeList() {
    //let currentList = $('.clientes');
    $('#search-cliente-input').val(null);
    $(".clientes").remove();
}

$("body").click(function (event) {
    if (event.target.id !== 'listclientes' && event.target.id !== 'search-cliente-input') {
        $('#list-cliente-container').hide();
    }
});

$("#search-cliente-input").focus(function () {
    $('#list-cliente-container').show();
});
