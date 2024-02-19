//FUNCION DEL BOTON VISUALIZAR CARTA DE PRESENTACIÓN DE SERVICIO SOCIAL
$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaCartaPresentacionSS', { psNoControl: data[2], psFolio: data[1] }, displaySection);
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-warning', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $('#txtNoControl').val(data[2]);
        $('#txtNombre').val(data[3]);
        $('#GenerarExterno').modal('show');
    });

    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaCartaPresentacionSS', { psNoControl: data[2], psFolio: data[1] }, displaySection());

    });

    $("#btnGenerar").click(function () {
        var control = $("#txtNoControl").val();
        var dependencia = $("#cboRfc").val();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaCartaPresentacionExternoSS', { psNoControl: control, psDependencia: dependencia }, displaySection);
        $('#GenerarExterno').modal('hide');
    });

});

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}



