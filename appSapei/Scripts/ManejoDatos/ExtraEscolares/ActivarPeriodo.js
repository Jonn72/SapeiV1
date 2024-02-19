$(document).ready(function () {
});

$('#dtpFechaIni, #dtpFechaFin').datetimepicker({
     viewMode: 'years',
     format: 'DD/MM/YYYY HH:mm',
     locale: 'es',
     defaultDate: new Date()
});

$("#frmPeriodosActividades").submit(function (event) {
     var periodo = $("#hidPeriodo").val();
     var inicio = $("#txtFechaIni").val()
     var fin = $("#txtFechaFin").val();
     $(".alert").show();
     $.ajax({
          url: '../../../ExtraEscolares/GuardaPeriodo',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psInicio: inicio, psFin: fin },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          $('#BodyPrincipal').load('../../../../ExtraEscolares/ActivarPeriodo');
     });
     event.preventDefault();
})