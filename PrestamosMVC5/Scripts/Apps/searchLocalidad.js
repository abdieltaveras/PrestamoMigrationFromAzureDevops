﻿
if (typeof (searchLocalidadElem) === 'undefined') { console.log("Es obligatorio pasar la variable searchLocalidadElem"); }

let searching = null;

$(".target").keyup(function () {
    clearTimeout(searching);
    searching = setTimeout(function () {
        let value = searchLocalidadElem.val();
        if (value.length >= 1) {
            searchText(value);
        } else {
            $('.direcciones').remove();
        }
    }, 300);
});

async function searchText(location) {
    try {
        const res = await search(location);
        showList(JSON.parse(res));
        setClickListener();
    }
    catch (err) { }
}

function search(location) {
    
    // let dataValue = { "searchtotext": location };
    // console.log((datavalue.searchtotext)
    let dataValue = { "texttosearch": location };
    //let LocalidadABuscar;
    return $.ajax({
        type: "get",
        url: "/Localidades/BuscarLocalidad",
        data: dataValue,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}

function setClickListener() {
    const list = document.querySelectorAll('#listaddresses');
    list.forEach(function (btn) {
        btn.addEventListener('click', function (evt) {
            // Aqui consigues el ID de la localidad seleccionada
            LocalidadABuscar = evt.target.getAttribute('data-idlocalidad');
            $('#iddelocalidad').text(` El ID de la localidad es:  ${evt.target.getAttribute('data-idlocalidad')}`);
            /// attention here you check if it is a function defined
            if (typeof setIdLocalidadToElem === 'function') {
                setIdLocalidadToElem(evt.target.getAttribute('data-idlocalidad'));
            }
            buscarRutaDeLocalidad();
            searchLocalidadElem.val($(evt.target.querySelector('strong')).text().trim());
            $('#list-address-container').hide();
        });
    });
}

$("body").click(function (event) {
    if (event.target.id !== 'listaddresses' && event.target.id !== searchLocalidadElem.attr('id')) {
        $('#list-address-container').hide();
    }
});

searchLocalidadElem.focus(function () {
    $('#list-address-container').show();
});

function showList(list) {
    $('.direcciones').remove();

    if (list.length > 0) {
        $.each(list, function (index, value) {
            console.log('La ruta a buscar es', list);

            $("#list-tab").append(`  <p class="list-group-item direcciones ${!value.PermiteCalle ? 'disabled-list-item' : ''} list-group-item-action" id="listaddresses" data-toggle="list"
                data-idlocalidadpadre="${value.IdTipoLocalidad}"
                data-idlocalidad="${value.IdLocalidad}" href="#list-home" role="tab" aria-controls="home">
                <span class="glyphicon glyphicon-map-marker"></span>
                <strong id="placeName" > ${value.Nombre} </strong>
                ${value.PermiteCalle ? '<span class="badge badge-success">Admite calle</span>' : ''}
                <br>
                <span id="placeName" style='color: #AAA;' > ${value.TipoNombrePadre} - ${value.NombrePadre} </span>
                <span style="float: right;">${value.Descripcion}  </span> </p>`);
            });

    } else {
        console.log("asd")
            $("#list-tab").append(`  <p class="list-group-item direcciones list-group-item-action" id="listaddresses" data-toggle="list"
                href="#list-home" role="tab" aria-controls="home">
                <span>No se encontro información</span> </p>`);
    }

}

// Buscar ruta de la localidad seleccionada
async function buscarRutaDeLocalidad() {
    try {
        const res = await BuscarRutaDeLocalidad(LocalidadABuscar);
        console.log('La ruta es', JSON.parse(res));
        AddItem(JSON.parse(res), 'normal');
        //setClickListener();
    }
    catch (err) { }
}

function BuscarRutaDeLocalidad(IdLocalidad) {

    let dataValue = { "IDLocalidad": IdLocalidad };

    return $.ajax({
        type: "get",
        url: "/Localidades/Buscar",
        data: dataValue,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        complete: function (jqXHR, status) {
        }
    });
}

function AddItem(list, typeOfList) {
    $('rutadelocalidad').text('');
    $('InputRutaLocalidad').text('');
    $('ReverseInputRutaLocalidad').text('');

    switch (typeOfList) {
        case 'inverso':
            for (var i = list.length - 1; i >= 0; i--) {
                if (i === list.length - 1) {
                    $('#rutadelocalidad').text(list[i].Nombre);
                    $('#ReverseInputRutaLocalidad').val(list[i].Nombre); // InputRutaLocalidad // ReverseInputRutaLocalidad
                }
                else {
                    $('#rutadelocalidad').text(`${$('#rutadelocalidad').text()},  ${list[i].Nombre}`);
                    $('#ReverseInputRutaLocalidad').val(`${$('#ReverseInputRutaLocalidad').val()},  ${list[i].Nombre}`);
                }
            }
            break;
        case 'normal':
            $.each(list, function (i, localidad) {
                if (i === 0) {
                    $('#InputRutaLocalidad').val(localidad.Nombre);
                    $('#rutadelocalidad').text(list[i].Nombre);
                }
                else {
                    $('#rutadelocalidad').text(`${$('#rutadelocalidad').text()},  ${list[i].Nombre}`);
                    $('#InputRutaLocalidad').val(`${$('#InputRutaLocalidad').val()},  ${localidad.Nombre}`);
                }
            });
            break;
        case 'TipoDeLocalidad':
            $.each(list, function (i, localidad) {
                if (i === 0) {
                    $('#InputRutaLocalidad').val(localidad.Descripcion + ' ' + localidad.Nombre);
                }
                else {
                    $('#InputRutaLocalidad').val(`${$('#InputRutaLocalidad').val()}, ${localidad.Descripcion} ${localidad.Nombre}`);
                }
            });
            break;
        default:
    }
}