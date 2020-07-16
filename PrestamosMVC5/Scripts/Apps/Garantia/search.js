// Buscador de garantias
let locations;
let garantiaRes;
let searchingGarantia = null;
const SEARCH_GARANTIA_DELAY = 300; // en milisegundos


$(".garantia_target").keyup(function (e) {
    if (e.which != 38 && e.which != 40 && e.which != 13) {
        clearTimeout(searchingGarantia);
        searchingGarantia = setTimeout(function () {
            let value = $('#search-garantia-input').val();
            if (value.length >= 1) {
                searchGarantiaText($('#search-garantia-input').val());
            } else {
                $('.garantias').remove();
            }
        }, SEARCH_GARANTIA_DELAY);
    }
});

$(".garantia_target").keydown(function (e) {
    if (e.which === 13) {
        onGarantiaEnter();
        removeList();
    }
    console.log($('#searchinput').val());
});

async function searchGarantiaText(location) {
    console.log('Encontre', location);
    try {
        garantiaRes = await searchGarantia(location);

        showListGarantia(JSON.parse(garantiaRes));
        console.log('Activo')
        setClickListenerOnGarantia();
        //console.log(res);
    } catch (err) {

    }
}

function searchGarantia(location) {

    let dataValue = { "searchtotext": location };
    let LocalidadABuscar;

    return $.ajax({
        type: "get",
        url: "/Garantias/BuscarGarantias",
        data: dataValue,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}

function searchLocalidadGarantia(IdLocation, IdNegocio) {

    let dataValue = {
        "IdLocalidad": IdLocation,
        "IdNegocio": IdNegocio
    };

    return $.ajax({
        type: "get",
        url: "/Garantias/BuscarLocalidadGarantias",
        data: dataValue,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}

// Funcion que asignara a cada item de la lista una funcion
function setClickListenerOnGarantia() {
    const list = document.querySelectorAll('.garantias');
    list.forEach(function (btn) {
        btn.addEventListener('click', async function (evt) {

            onGarantiaClick(evt);
            removeList();
        });
    });
}

document.onkeyup = function (e) {
    if (e.which == 38) {
        //if ($('#search-garantia-input').is(':focus')) {
            if (selectPointer != 0) {
                selectPointer--;
                $('[data-order="' + (selectPointer + 1) + '"]').removeClass('active');
                $('[data-order="' + selectPointer + '"]').addClass('active');
            }
        //}
    } else if (e.which == 40) {
        //$('#input-client_search').focus();

        if ($('.list-group-item').length != selectPointer) {
            selectPointer++;
            $('[data-order="' + (selectPointer - 1) + '"]').removeClass('active');
            $('[data-order="' + selectPointer + '"]').addClass('active');
        }
    } else if (e.ctrlKey && e.which == 66) {
        $('#search-garantia-input').focus();
    }
};

function showListGarantia(list) {
    let count = 1;
    selectPointer = 0;

    if (list.length > 0) {

        $.each(list, function (index, value) {

            var Detalles = JSON.parse(value.Detalles);

            if (value.IdClasificacion === 1) {
                $("#list-address-tab").append(` <p class="list-group-item garantias list-group-item-action" data-order="${count}" id="listaddresses" data-toggle="list"
                        data-idGarantia="${value.IdGarantia}"
                        data-idlocalidad="" href="#list-home" role="tab" aria-controls="home">
                        <span class=" glyphicon glyphicon-home "></span>
                        <strong id="placeName" > ${value.NoIdentificacion} </strong><br>
                        ${Detalles.Descripcion !== null ? Detalles.Descripcion : ''}
                        ${Detalles.Medida !== null ? 'Medida: ' + Detalles.Medida : ''}
                        <br></p>`);
            } else {
                $("#list-address-tab").append(` <p class="list-group-item garantias list-group-item-action" data-order="${count}" id="listaddresses" data-toggle="list"
                        data-idGarantia="${value.IdGarantia}"
                        role="tab" aria-controls="home">
                        <span class="fa fa-automobile"></span>
                        <strong id="placeName" > ${value.NoIdentificacion} </strong><br>
                       <i> ${Detalles.Descripcion !== null ? Detalles.Descripcion + ', ' : ''}</i><strong> Año:</strong> ${Detalles.Ano} <strong> Placa:</strong> ${Detalles.Placa} <strong> Maquina:</strong> ${Detalles.NoMaquina}
                        <br></p>`);
            }
            count++;
        });
    } else {
        $("#list-address-tab").append(` <p class="list-group-item garantias list-group-item-action" id="listaddresses" data-toggle="list"
                        role="tab" aria-controls="home">
                       <span">No se encontro información</span>
                        <br></p>`);
    }
}

function removeList() {
    let currentList = $('.garantias');
    $('#search-garantia-input').val(null);
    $("p#listaddresses").remove();
}

$("body").click(function (event) {
    if (event.target.id !== 'listaddresses' && event.target.id !== 'search-garantia-input') {
        $('#list-garantia-container').hide();
    }
});

$("#search-garantia-input").focus(function () {
    $('#list-garantia-container').show();
});
