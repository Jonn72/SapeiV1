$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     CargaComboPeriodo(4, 1, periodo);
});

$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../ServiciosEscolares/ReporteAspirantes2AlumnosInscritos', { psPeriodo: periodo });
     evento.preventDefault();
});