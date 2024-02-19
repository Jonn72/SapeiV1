//FUNCION DEL BOTON VISUALIZAR TARJETA DE CONTROL DE SERVICIO SOCIAL DE SERVICIO SOCIAL
$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/TarjetaControl', { psNoControl: data[1] }, displaySection);

    });
});


function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}