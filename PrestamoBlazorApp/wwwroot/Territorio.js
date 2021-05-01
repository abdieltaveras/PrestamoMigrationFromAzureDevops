
    let listaLocalidadesMostradas = [];
    let margenCount = 0;
        let basemargenes = '{"margenes": [] }';
        let margenes = JSON.parse(basemargenes);
        let MARGIN_SPACING = 30;


        $(document).ready(function () {
        let params = new URLSearchParams(document.location.search.substring(1));
            let id = params.get("division_territorial_id");


            console.log(id);
            if (id !== 0 && id !== null) {
        searchTerritorio(id);
                $('#inputIdDivisionTerritorialPadre').val(id);
            }
        });

        $("#idTipoDivision").change(function () {
            var divisionSeleccionada = $(this).children("option:selected").val();
            $('#inputIdDivisionTerritorialPadre').val(divisionSeleccionada);
            margenCount = 0;

            if (divisionSeleccionada !== '0') {
        searchTerritorio(divisionSeleccionada);
            } else {
        //TODO: Mostrar mensaje de informacion faltante
    }
        });

        async function searchTerritorio(division) {
            try {
                const res = await BuscarComponentesDeDivisionTerritorial(division);
                console.log(JSON.parse(res));
                showListDivisionesTerritorialesHijas(JSON.parse(res), division);
                showListComponentes(division, JSON.parse(res));
            } catch (err) {

    }
        }

function BuscarComponentesDeDivisionTerritorial(divisionTerritorial) {
    console.log("Estoy en la funcion")
        let dataValue = {"IdDivision": divisionTerritorial };

            return $.ajax({
        type: "get",
                url: "https://localhost:44350/api/territorio/BuscarComponenteDeDivision2",
                data: dataValue,
                //data: {},
                headers: {
                    RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
        console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                complete: function (jqXHR, status) {
    }
            });
        }

        function showListDivisionesTerritorialesHijas(lista, idDivision) {
        $('.divisionoption').remove();

            // Si la division territorial seleccionada no tiene ningun componente (Division terriritorial por debajo de ella) le asignara
            // como HijoDe el IdDivisionTerritorial de la division al que se cree (EJM: Division -> pais)

            if (lista.length > 0) {
        $.each(lista, function (index, value) {
            $('#divisionesterritorialeshijas').append(`<option class="divisionoption" value="${value.IdDivisionTerritorial}">${value.Nombre}</option>`);
        });
            } else {
                if (idDivision > 0) {
                    $('#divisionesterritorialeshijas').append(`<option selected class="divisionoption" value="${idDivision}">Primer componente</option>`);
                }
            }
        }

        function showListComponentes(division, lista) {
        let text = null;
            let componentCount = lista.length;
            $('#component-list').empty();
            $('#info-component-container').removeClass('d-none');

            scan(lista.filter(item => item.IdLocalidadPadre == division), lista, false);

            $('#component-count').text(componentCount);
        }


        async function scan(listaParcial, listaCompleta) {

            for (let i in listaParcial) {

        let saveMargin = undefined;
                let parentMargin = undefined;

                // Buscar si hay un margen guardado previamente
                margenes["margenes"].forEach(function (margen) {
                    if (margen[listaParcial[i].IdLocalidadPadre] !== undefined) {
        saveMargin = margen[listaParcial[i].IdLocalidadPadre];
                    }

                    // Buscar si tiene un margen padre, para asi aplicar el mismo mas la diferencia al hijo
                    let kiss = listaCompleta.filter(item => item.IdDivisionTerritorial == listaParcial[i].IdLocalidadPadre);
                    if (kiss[0] !== undefined) {
                        if (margen[kiss[0].IdLocalidadPadre] !== undefined) {
        parentMargin = margen[kiss[0].IdLocalidadPadre];
                        }
                    }
                });

                // Determinar cual de los margenes se aplicara al elemento
                let margenAAplicar = 0;
                if (saveMargin !== undefined) {
        margenAAplicar = saveMargin;
                } else if (parentMargin !== undefined) {
        margenAAplicar = parentMargin + MARGIN_SPACING;
                } else {
        margenAAplicar = margenCount;
                    margenCount += MARGIN_SPACING;
                }

                // Guardar en el listado el margen puesto al elemento
                let tempMerge = JSON.parse('{"' + listaParcial[i].IdLocalidadPadre + '" : ' + margenAAplicar + '}');
                margenes["margenes"].push(tempMerge);
                JSON.stringify(margenes);

                // Agregar elemento a la lista
                $('#component-list').append('<p style="margin-bottom: 4px; margin-left:' + margenAAplicar + 'px" > <i class="fa fa-chevron-right"></i> <span style="color: white; padding: 5px;" class="badge badge-pill badge-primary"> ' + listaParcial[i].Nombre + '</span> </p>');

                // Llamar la funcion nuevamente, con el filtro de la lista hija (recursividad)
                scan(listaCompleta.filter(item => item.IdLocalidadPadre == listaParcial[i].IdDivisionTerritorial), listaCompleta)
            }
        }

        function validarDatos() {
            if ($('#idTipoDivision').val() == 0) {
        showMessage('error', 'Debe seleccionar una division territorial');
                event.preventDefault();
                return;
            }

            if ($('#divisionesterritorialeshijas').val() == 0) {
        showMessage('error', 'Debe seleccionar un tipo de componente');
                event.preventDefault();
                return;
            }

            if ($('#nombre').val().length <= 2) {
        showMessage('error', 'El nombre debe ser mayor a 2 caracteres');
                event.preventDefault();
                return;
            }
            showMessage('success', 'Componente de division guardado correctamente.');
        }

        function showMessage(typeOfNotification, messageOfNotification) {
            var notification = new PNotify({
        title: typeOfNotification === 'error' ? 'Algo ocurrio mal' : 'Todo correcto',
                text: messageOfNotification,
                type: typeOfNotification,
                styling: 'bootstrap3',
            });
        }

