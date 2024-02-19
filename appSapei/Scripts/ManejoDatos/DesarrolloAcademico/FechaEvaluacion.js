$(document).ready(function () {
});
$('#txtFechaIniCaptura, #txtFechaFinCaptura').datetimepicker({
    viewMode: 'years',
    format: 'DD/MM/YYYY',
    locale: 'es',
    defaultDate: new Date()
});


$("#frmPeriodoEvalDoc").submit(function (event) {
    event.preventDefault();

    var periodo = $("#hidPeriodo").val();
    var fechaini = $("#txtFechaIniCaptura").val();
    var fechafin = $("#txtFechaFinCaptura").val();

    $.ajax({
        url: '../../../DesarrolloAcademico/GuardaFechaJSON',
        type: 'POST',
        dataType: 'json',
        data: { psPeriodo: periodo, psFechaInicio: fechaini, psFechaFin: fechafin },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined" && !data.Success) {
                 MensajesToastr("error", "Error Al Guardar", data.Mensaje);
             }
             else {
                 MensajesToastr("info", "Solicitud Procesada", "Periodo Activado");
                 $('#BodyPrincipal').load('../../../DesarrolloAcademico/FechaEvaluacionDocente');
             }
         })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
})