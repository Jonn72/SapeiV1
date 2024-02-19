$(document).ready(function () {
    var periodo;
    periodo = $("#hidPeriodo").val()
    DesactivaBotones();
    CargaComboPeriodo(2, 0, periodo);
});

$("#cboPeriodo").change(function (evento) {
    var periodo = $("#cboPeriodo").val();
    $('#BodyPrincipal').load('../../../../CentroLenguasExt/ImprimirListaInscritos/' + periodo);
    evento.preventDefault();
});
