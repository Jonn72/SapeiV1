$(document).ready(function () {


    $("#btnSeguimiento1").click(function () {
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/ReporteEvaluacionSeguimientoRP', {}, displaySection);
    });

    $("#btnSeguimiento2").click(function () {
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/ReporteEvaluacionSeguimientoRP', {}, displaySection);
    });

    $("#btnSeguimiento3").click(function () {
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/ReporteResidenciasProfesionalesRP', {}, displaySection);
    });

});

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}