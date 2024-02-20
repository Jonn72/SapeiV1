$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();
});

$('#dtpFechaIni, #dtpFechaFin, #dtpFechaIniRegistro, #dtpFechaFinRegistro, #dtpFechaIniCaptura, #dtpFechaFinCaptura').datetimepicker({
     viewMode: 'years',
     format: 'DD/MM/YYYY',
     locale: 'es',
     defaultDate: new Date()
});

$("#frmPeriodosActividades").submit(function (event) {
     var periodo = $("#hidPeriodo").val();
     var iniSeleccion = $("#txtFechaIni").val()
     var finSeleccion = $("#txtFechaFin").val();
     var iniRegistro = $("#txtFechaIniRegistro").val()
     var finRegistro = $("#txtFechaFinRegistro").val();
     var iniCaptura = $("#txtFechaIniCaptura").val()
     var finCaptura = $("#txtFechaFinCaptura").val();
     $.ajax({
          url: '../../../DEP/GuardaPeriodo',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psIniRegistro: iniRegistro, psFinRegistro:finRegistro, psIniSeleccion: iniSeleccion, psFinSeleccion: finSeleccion, psIniCaptura:iniCaptura, psFinCaptura:finCaptura },
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
          $('#BodyPrincipal').load('../../../../DEP/ActivarPeriodo');
     });
     event.preventDefault();
})