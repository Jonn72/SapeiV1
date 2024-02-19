var periodo;
$(document).ready(function () {
     QuitaBuscar();
     periodo = $("#hidPeriodo").val();
     CargaComboPeriodo(2, 1, periodo);
});
$("#cboPeriodo").change(function (evento) {
     evento.preventDefault
     periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../DesarrolloAcademico/ActivarPeriodoTutorias', { psPeriodo: periodo});
});
$('#dtpFechaIni, #dtpFechaFin,#dtpFechaIniGrupos, #dtpFechaFinGrupos,#dtpFechaIniCaptura, #dtpFechaFinCaptura').datetimepicker({
     locale: 'es',
     viewMode: 'years',
     format: 'DD/MM/YYYY',
     defaultDate: new Date()
});

$("#frmPeriodos").submit(function (event) {
     var iniRegistro = $("#txtFechaIniGrupos").val()
     var finRegistro = $("#txtFechaFinGrupos").val();
     var iniSeleccion = $("#txtFechaIni").val()
     var finSeleccion = $("#txtFechaFin").val();
     var iniCaptura = $("#txtFechaIniCaptura").val()
     var finCaptura = $("#txtFechaFinCaptura").val();
     periodo = $("#cboPeriodo").val();
     $.ajax({
          url: '../../../DesarrolloAcademico/GuardaPeriodoTutorias',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo : periodo, psIniGrupos: iniRegistro, psFinGrupos: finRegistro, psInicio: iniSeleccion, psFin: finSeleccion, psInicioCaptura: iniCaptura, psFinCaptura: finCaptura },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined") {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
          }
          if(!data.Success)
               MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
               $('#BodyPrincipal').load('../../../DesarrolloAcademico/ActivarPeriodoTutorias');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     event.preventDefault();
})


