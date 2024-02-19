$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();
    $('.spinner').css("display", "block");
    periodo = $("#hidPeriodo").val()
    CargaComboPeriodo(4, 1, periodo);
});

$("#cboPeriodo").change(function (evento) {
    var periodo = $("#cboPeriodo").val();
    $('#BodyPrincipal').load('../../../ExtraEscolares/CargaCalificacionEstudiantesActividad', { psPeriodo: periodo });
    evento.preventDefault();
});