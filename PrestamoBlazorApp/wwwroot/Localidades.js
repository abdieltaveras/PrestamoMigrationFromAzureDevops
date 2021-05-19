
    let locations;

//$(".target").keyup(function () {
//    searchText($('#searchinput').val());
//});


async function searchText(location) {
    try {
        const res = await search(location);
         showList(res);
         setClickListener();
        
    } catch (err) {
        console.log(err);
        }
}

            function search(location) {

                let dataValue = {"search": location };
                let TipoDeLocalidadABuscar;
                let idLocalidadABuscar;

                return $.ajax({
        type: "get",
                    url: "https://localhost:44350/api/localidades/BuscarLocalidad",
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
            console.log(evt.target.getAttribute('data-idlocalidadpadre'))
            console.log(evt.target.getAttribute('data-idtipolocalidadhijo'));

            console.log('Datos de evento', (evt.target));

            TipoDeLocalidadABuscar = evt.target.getAttribute('data-idlocalidadpadre');
            idLocalidadABuscar = evt.target.getAttribute('data-idlocalidad');

            buscarTipoDeLocalidadesHijas();
            $("#searchinput").val($(evt.target.querySelector('strong')).text().trim());
            $('#inputLocalidadPadre').val(evt.target.getAttribute('data-idlocalidad'));
            $('#list-address-container').hide();
            var event = new Event('change');
            $('#inputLocalidadPadre').dispatchEvent(event);
            alert('seleccionado');
        });
                });
            }

            $("body").click(function (event) {
                if (event.target.id !== 'listaddresses' && event.target.id !== 'searchinput') {
        $('#list-address-container').hide();
                }
            });

            $("#searchinput").focus(function () {
        $('#list-address-container').show();
            });

function showList(list) {
        $('.direcciones').remove();
    if (list.length > 0) {
        $("#list-address-container").css({'display':'block'});
        $.each(list, function (index, value) {
            $("#list-tab").append(`<p class="list-group-item direcciones list-group-item-action" id="listaddresses" data-toggle="list"
                            data-idlocalidadpadre="${value.IdTipoLocalidad}"
                            data-idlocalidad="${value.IdLocalidad}" href="#list-home" role="tab" aria-controls="home">
                            <strong id="placeName" > ${value.Nombre} </strong>
                            <span style="float: right;">${value.Descripcion}
                            <button type="button" class="btn btn-outline-info"> <i class="fa fa-edit"></i> </button> </span> </p>`);

            //console.table(value);
            
        });
                } else {
        $("#list-tab").append(`<p class="list-group-item direcciones list-group-item-action" id="listaddresses" data-toggle="list"
                        href="#list-home" role="tab" aria-controls="home">
                        <span">No se encontro información</span> </p>`);
                }
            }


            // Buscar divisiones geograficas hijas de la localidad seleccionada
            async function buscarTipoDeLocalidadesHijas() {

                try {

                    const res = await BuscarTipoDeLocalidad(TipoDeLocalidadABuscar);

                    const json_list = JSON.parse(res);

                    console.log('Cantidad', json_list);

                    if (json_list.length === 0) {
                        alert('warning' + 'Falta informacion' + 'Esta ubicacion no tiene divisiones territoriales');
                        $('#Nombre').prop("disabled", true);
                        $('#btnguardar').prop("disabled", true);
                        return;
                    }

                    $('#Nombre').prop("disabled", false);
                    $('#btnguardar').prop("disabled", false);

                    AddListToSelect(json_list);


                    const localidadesHija = await buscarLocalidadesHijas(idLocalidadABuscar);
                  
                    showLocalidadesHijasList(localidadesHija);

                } catch (err) {
                    alert(err);
    }
            }

            function BuscarTipoDeLocalidad(IdTipoLocalidad) {

        let dataValue = {"localidadPadre": IdTipoLocalidad };

                return $.ajax({
        type: "get",
                    url: "https://localhost:44350/api/territorio/BuscarTerritorios",
                    data: dataValue,
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    },
                    complete: function (jqXHR, status) {
    }
                });
            }

            function AddListToSelect(list) {

                 $('.tipolocalidades').remove();
                console.log('aaay', list.length);
                if (list.length > 1) {
                    $('#selecttipolocalidad').show();
                    $.each(list, function (i, item) {
                        $('#tipolocalidadeshijas').append('<option class="tipolocalidades" value="' + item.IdTipoLocalidad + '">' + item.Nombre + '</option>');
                    });
                    $('#titulodelocalidad').text(' Seleccione el tipo de direccion');
                } else if (list.length === 1) {
                     $('#selecttipolocalidad').hide();
                    $('#titulodelocalidad').text(' La localidad a ingresar es: ' + list[0].Nombre);

                    $('#tipolocalidadeshijas').append('<option class="tipolocalidades" selected value="' + list[0].IdTipoLocalidad + '">' + list[0].Nombre + '</option>');
                } else if (list.length === 0) {
                    $('#titulodelocalidad').text('Agregar localidad');
                   
                    $('.alert').alert();
                }
            }

            //function validarDatos() {

        //    if ($('#nombre').val().length <= 2) {
        //        showMessage('error', 'El nombre debe ser mayor a 2 caracteres');
        //        event.preventDefault();
        //        return;
        //    }

        //    showMessage('success', 'Componente de division guardado correctamente.');
        //}

        //function showMessage(typeOfNotification, titleMessage, messageOfNotification) {
        //    var notification = new PNotify({
        //        title: titleMessage,
        //        text: messageOfNotification,
        //        type: typeOfNotification,
        //        styling: 'bootstrap3',
        //    });
        //}

            function buscarLocalidadesHijas(idLocalidad) {
        console.log('idLoc', idLocalidad);
                let dataValue = {"idLocalidad": idLocalidad };

                return $.ajax({
        type: "get",
                    url: "https://localhost:44350/api/Localidades/GetHijasLocalidades",
                    data: dataValue,
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    },
                    complete: function (jqXHR, status) {
    }
                });
            }

function showLocalidadesHijasList(localidades) {
  
        $('#localidadesHijas').empty();
                console.log('misterio',localidades);
                $.each(localidades, function (index, value) {
        $("#localidadesHijas").append(`<p> Tipo: ${value.DivisionTerritorial} - <span class="badge badge-primary">${value.Nombre}</span> </p>`);

                    console.table(value);
                });
            }

