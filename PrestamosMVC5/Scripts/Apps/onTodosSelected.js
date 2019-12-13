$(document).ready(function () {
    if (!todoSelected) { console.log("debe inicializar en el script de la pagina que llama este funcion la variable todosValue"); }
    $('#SelectedOpcion').on('change', function () {
        if (todoSelected(this))
            $('#searchForm').submit();
    });
});
