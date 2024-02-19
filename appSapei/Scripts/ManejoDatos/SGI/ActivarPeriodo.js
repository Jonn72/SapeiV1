$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();
});

$('#dtpFechaIniRegistro, #dtpFechaFinRegistro').datetimepicker({
     viewMode: 'years',
     format: 'DD/MM/YYYY',
     locale: 'es',
     defaultDate: new Date()
});

$("#frmPeriodosActividades").submit(function (event) {
     var iniRegistro = $("#txtFechaIniRegistro").val()
     var finRegistro = $("#txtFechaFinRegistro").val();

     $.ajax({
          url: '../../../SGI/GuardaPeriodo',
          type: 'POST',
          dataType: 'json',
          data: {psIniRegistro: iniRegistro, psFinRegistro:finRegistro },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          $('#BodyPrincipal').load('../../../../SGI/ActivarPeriodo');
     });
     event.preventDefault();
})