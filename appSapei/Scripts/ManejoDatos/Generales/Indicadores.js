$(document).ready(function () {
});

$("#btnConsultar").on('click', function (evento) {
    evento.preventDefault();
    var periodo = $("#cboPeriodos").val();
    var indicador = $("#cboIndicadores").val();

    $('#divTabla').load('../../../../Generales/RegresaIndicadores', { piIndicador: indicador, psPeriodo: periodo }, displaySection);
});
function displaySection() {
    $("#divTabla").show();
}