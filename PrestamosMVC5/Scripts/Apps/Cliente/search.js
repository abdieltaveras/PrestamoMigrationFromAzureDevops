// Buscador de Clientes

let clienteRes;
let searchingCliente = null;
const SEARCH_CLIENTE_DELAY = 300; // en milisegundos

$(".cliente_target").keyup(function (e) {
    if (e.which != 38 && e.which != 40 && e.which != 13) {
        clearTimeout(searchingCliente);
        searchingCliente = setTimeout(function () {
            let value = $('#search-cliente-input').val();
            if (value.length >= 1) {
                searchClienteText($('#search-cliente-input').val());
            } else {
                $('.clientes').remove();
            }
        }, SEARCH_CLIENTE_DELAY);
    }
});

$(".cliente_target").keydown(function (e) {
     if (e.which === 13) {
         onClienteEnter();
         removeListCliente();
    }
});

async function searchClienteText(cliente) {
    //console.log('Encontre', cliente);
    try {
        clienteRes = await searchCliente(cliente);
        // analizar esto con bryan
        $("#list-clientes-tab").empty();
        showListCliente(JSON.parse(clienteRes));
        //console.log('Activo', clienteRes)
        setClickListenerOnCliente();
        //console.log(res);
    } catch (err) {

    }
}

function searchCliente(location) {

    let dataValue = { "searchtotext": location, "CargarImagenesClientes": IMAGEN_CLIENTE_EN_BUSCADOR_CLIENTE  };

    return $.ajax({
        type: "get",
        url: "/Clientes/BuscarClientes",
        data: dataValue,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
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
            removeListCliente();
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
    //console.log('puntero', selectPointer)
};

function showListCliente(list) {
    let count = 1;
    selectPointer = 0;
    $('.clientes').remove();
    infoClientes = [];
    //console.log('lista', list);
    if (list.length > 0) {
        $.each(list, function (index, value) {
            let infoCliente = {
                IdCliente: value.IdCliente, NombreCompleto: value.Nombres + " " + value.Apellidos, Telefonos: value.TelefonoMovil, NoIdentificacion: value.NoIdentificacion, Imagen1Filename: value.Imagen1FileName
            };
            infoClientes.push(infoCliente);
            $("#list-clientes-tab").append(` <p class="list-group-item clientes list-group-item-action pb-2 pt-2 pl-2 pr-2 " data-order="${count}" data-toggle="list"
                                            data-index=${index}
                                            data-idCliente = "${value.IdCliente}"
                                            role="tab" aria-controls="home">
                                            ${ IMAGEN_CLIENTE_EN_BUSCADOR_CLIENTE ? `<img src="${value.Imagen1FileName}" height="60px" width="auto" class="float-left mr-2" style="border: 1px solid #666; border-radius: 10px;"/>` : ``}
                                            <strong id="placeName" >Cliente: <i>${value.Nombres} ${value.Apellidos} | <small style="font-weight: 600;"> Sexo: ${value.Sexo}</i> </span><br>
                                            <span style="font-weight: 600;">Telefono: </span> ${value.TelefonoMovil} ${value.NoIdentificacion === undefined ? `` : ` | No. de identificacion: ${value.NoIdentificacion}`}</i><br>
                                            <br> </p>`);
            count++;
        });
    } else {
        $("#list-clientes-tab").append(` <p class="list-group-item clientes list-group-item-action" id="listclientes" data-toggle="list"
                        role="tab" aria-controls="home">
                        <span">No se encontro información</span>
                        <br></p>`);
    }
}

function removeListCliente() {
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
