var periodo;
var eliminar_no_control;
$(document).ready(function () {
    periodo = $('#hidPeriodo').val();
    CargaComboPeriodo(6, 1, periodo);
});
$("#cboPeriodo").change(function (evento) {
    evento.preventDefault();
    var link = '/Vinculacion/ConsultaEstadoSS/' + $("#cboPeriodo").val();
    $('#btnEstado').attr('href', link);
});

