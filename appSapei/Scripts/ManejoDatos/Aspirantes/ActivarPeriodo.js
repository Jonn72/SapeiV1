var periodo;
$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     $("#cboAnio").append($("<option />").val((new Date).getFullYear()).text((new Date).getFullYear()));
     if (periodo.charAt(4) == "3")
          $("#cboAnio").append($("<option />").val((new Date).getFullYear()+1).text((new Date).getFullYear() + 1));
     QuitaBuscar();
});

$('#dtpFechaIni, #dtpFechaFin, #dtpFechaExamen').datetimepicker({
     locale: 'es',
     defaultDate: new Date()
});

$("#frmPeriodos").submit(function (event) {
     var periodo = $("#cboAnio").val() + $("#cboPeriodo").val() ;
     var vuelta = $("#cboVuelta").val();
     var iniRegistro = $("#txtFechaIni").val()
     var finRegistro = $("#txtFechaFin").val();
     var examen = $("#txtFechaExamen").val();

     $.ajax({
          url: '../../../DesarrolloAcademico/GuardaPeriodo',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, piVuelta:vuelta, psFechaExamen:examen, psIniRegistro: iniRegistro, psFinRegistro:finRegistro,pbActivo:true},
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined") {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
          }
          if(!data.Success)
               MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
               $('#BodyPrincipal').load('../../../DesarrolloAcademico/ActivarPeriodo');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     event.preventDefault();
})


