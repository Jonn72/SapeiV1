$(document).ready(function () {
     $(".alert").hide();
});

$('#dtpFechaIni, #dtpFechaFin,#dtpFechaIniEntrega, #dtpFechaFinEntrega').datetimepicker({
     viewMode: 'years',
     format: 'DD/MM/YYYY HH:mm',
     locale: 'es',
     defaultDate: new Date()
});

$("#frmPeriodosActividades").submit(function (event) {
     var periodo = $("#hidPeriodo").val();
     var inicio = $("#txtFechaIni").val()
     var fin = $("#txtFechaFin").val();
     var autos = $("#txtMaxAutos").val();
     var motos = $("#txtMaxMotos").val();
     var inicioEntrega = $("#txtFechaIniEntrega").val()
     var finEntrega = $("#txtFechaFinEntrega").val();
     $.ajax({
          url: '../../../Materiales/GuardaPeriodo',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psInicio: inicio, psFin: fin, psInicioEntrega: inicioEntrega, psFinEntrega: finEntrega, piMaxAutos: autos, piMaxMotos: motos },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
               $('#BodyPrincipal').load('../../../../Materiales/ActivarPeriodo');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     event.preventDefault();
})