
$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     DesactivaBotones();
     CargaComboPeriodo(2, 1, periodo);
});

$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../../ExtraEscolares/InscritosPorActividad/', { psPeriodo: periodo });
     evento.preventDefault();
});

