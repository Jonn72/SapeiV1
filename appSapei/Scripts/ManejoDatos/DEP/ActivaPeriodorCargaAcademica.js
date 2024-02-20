$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();
});

$('#dtpFechaCargaIni, #dtpFechaCargaFin').datetimepicker({
     viewMode: 'years',
     format: 'DD/MM/YYYY',
     locale: 'es',
     defaultDate: new Date()
});

$("#btnGuardar").click(function () {
    var inicio = $("#txtIniCarga").val()
    var fin = $("#txtFinCarga").val();

     $.ajax({
         url: '../../../DEP/ActivaPeriodorCargaAcademicaJson',
          type: 'POST',
          dataType: 'json',
          data: {poInicio: inicio, poFin:fin},
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
     });
     event.preventDefault();
})
