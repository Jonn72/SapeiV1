var periodo;
$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     CargaComboPeriodo(4, 1, periodo);
});
$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../Materiales/ConsultarRegistros', { psPeriodo: periodo });
     evento.preventDefault();
});