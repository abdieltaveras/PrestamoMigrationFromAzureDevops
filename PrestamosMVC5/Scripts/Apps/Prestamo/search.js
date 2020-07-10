    let searching = null;
    let res = null;
    const SEARCH_DELAY = 300; // en milisegundos

    //Parametros de configuracion
    //IMAGEN_CLIENTE_EN_BUSCADOR = false;
    //IMAGEN_CLIENTE_EN_DETALLES = false;
    //DETALLES = true;


    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
    });

    //$(document).ready(function () {
    //});
    $('input').on('ifChecked', function (event) {
        if (event.target.id === 'search-by-prestamo') {
            console.log(event.target.id);
            $('#search-by-prestamo').iCheck('check');
            $('#search-by-user').iCheck('uncheck');
            $('#search-by-garantia').iCheck('uncheck');
        } else if (event.target.id === 'search-by-user') {
            console.log(event.target.id);
            $('#search-by-user').iCheck('check');
            $('#search-by-prestamo').iCheck('uncheck');
            $('#search-by-garantia').iCheck('uncheck');
        } else if (event.target.id === 'search-by-garantia') {
            $('#search-by-garantia').iCheck('check');
            $('#search-by-user').iCheck('uncheck');
            $('#search-by-prestamo').iCheck('uncheck');
        }
        removeList();
    });

    $(".prestamo_target").keyup(function (e) {
        if (e.which != 38 && e.which != 40 && e.which != 13) {
            clearTimeout(searching);
            searching = setTimeout(function () {
                let value = $('#input-prestamo_search').val();
                if (value.length >= 1) {
                    searchPrestamoText(value);
                } else {
                    $('.prestamos').remove();
                }
            }, SEARCH_DELAY);
        }
    });

    async function searchPrestamoText(text) {
        console.log('Encontre', text);
        try {
            res = await searchPrestamo(text);

            showListPrestamos(JSON.parse(res));
            setClickListenerOnPrestamo();
            console.log(res);
        } catch (err) {
            console.log('Encontre error', err);
        }
    }

    // Funcion que asignara a cada item de la lista una funcion
    function setClickListenerOnPrestamo() {
        const list = document.querySelectorAll('.prestamos');
        list.forEach(function (btn) {
            btn.addEventListener('click', async function (evt) {

                onClick(evt);
                removeList();
            });
        });
    }

    document.onkeyup = async function (e) {
        if (e.which == 38) {
            if ($('#input-prestamo_search').is(':focus')) {
                if (selectPointer != 0) {
                    selectPointer--;
                    $('[data-order="' + (selectPointer + 1) + '"]').removeClass('active');
                    $('[data-order="' + selectPointer + '"]').addClass('active');
                }
            }
        } else if (e.which == 40) {
            //$('#input-client_search').focus();

            if ($('.list-group-item').length != selectPointer) {
                selectPointer++;
                $('[data-order="' + (selectPointer - 1) + '"]').removeClass('active');
                $('[data-order="' + selectPointer + '"]').addClass('active');
            }
        } else if (e.which == 13) {
            onEnter();
            removeList();
        } else if (e.ctrlKey && e.which == 66) {
            $('#input-prestamo_search').focus();
        }
        if (e.ctrlKey && e.which == 77) {
            $('#input-amount-pay').focus(); // Focus a campo de monto
        }
    };

    function removeList() {
        let currentList = $('.garantias');
        $('#input-prestamo_search').val(null);
        $(".list-group-item").remove();
    }

    function loadPrestamoData(id) {
        console.log(id);

        let dataValue = { "idprestamo": id };

        return $.ajax({
            type: "get",
            url: "/Ingresos/GetPrestamo",
            data: dataValue,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
            complete: function (jqXHR, status) {
            }
        });
    }

    $("body").click(function (event) {
        if (event.target.id !== 'input-prestamo_search') {
            $('#list-prestamo-container').hide();
        }
    });

    $("#input-prestamo_search").focus(function () {
        $('#list-prestamo-container').show();
    });

    function searchPrestamo(text) {

        let searchType = $(".searchtype:checked").val();
        let dataValue = { "TextToSearch": text, "SearchType": searchType, "CargarImagenesClientes": IMAGEN_CLIENTE_EN_BUSCADOR };

        console.log('type', searchType);
        console.log('data', dataValue);

        return $.ajax({
            type: "get",
            url: "/Ingresos/BuscarPrestamos",
            data: dataValue,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Request: " + XMLHttpRequest.toString(27) + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
            complete: function (jqXHR, status) {
            }
        });
    }

    function showListPrestamos(list) {
        let count = 1;
        $('.prestamos').remove();

        selectPointer = 0;
        if (list.length > 0) {

            $.each(list, function (index, value) {

                $("#list-prestamo-tab").append(` <p class="list-group-item prestamos list-group-item-action pb-2 pt-2 pl-2 pr-2 " data-order="${count}" data-toggle="list"
                                            data-idPrestamo="${value.IdPrestamo}"
                                            data-client-photo="${value.FotoCliente}"
                                            role="tab" aria-controls="home">
                                            ${ IMAGEN_CLIENTE_EN_BUSCADOR ? `<img src="${value.FotoCliente}" height="60px" width="auto" class="float-left mr-2" style="border: 1px solid #666; border-radius: 10px;"/>` : ``}
                                            <strong id="placeName" >Prestamo - ${value.PrestamoNumero} </strong>  | Monto de prestamo: <span style="color: #00c853;"> ${formatter.format(value.MontoPrestado)} </span><br>
                                            <small style="font-weight: 600;">Cliente: </small> <i>${value.Nombres} ${value.Apellidos} | <small style="font-weight: 600;"> Sexo: </small> ${value.Sexo}</i></small><br>
                                            <small>Clasificacion: ${value.Clasificacion}
                                            ${value.NoIdentificacion === undefined ? `` : `No. de identificacion: ${value.NoIdentificacion}`}</small>
                                            <br></p>`);

                count++;
            });
        } else {
            $("#list-prestamo-tab").append(` <p class="list-group-item prestamos list-group-item-action pb-2 pt-2 pl-2 pr-2 "  data-toggle="list"
                                            role="tab" aria-controls="home">
                                            No se encontro información.
                                            <br></p>`);
        }
    }