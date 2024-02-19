$(document).ready(function () {
    var periodo;
    periodo = $("#hidPeriodo").val()
    CargaComboPeriodo(2, 0, periodo);
});

$("#cboPeriodo").change(function (evento) {
    var periodo = $("#cboPeriodo").val();
    $('#BodyPrincipal').load('../../../../CentroLenguasExt/ActivaEstudiante/', {psPeriodo:periodo});
    evento.preventDefault();
});

$("#btnGuardar").on('click', function (evento) {
     var opcion = $("#hidOpcion").val();
     var control = $("#txtNoControl").val();
     var nivel;
     var fecha = $("#txtFecha").val();
     evento.preventDefault();

     if (opcion == "4")
          nivel = $("#cboNivelesIngles").val();
     else
          nivel = $("#txtNivel").val()
     if (opcion == "0" || opcion == "1")
          return;
     $.ajax({
          url: '../../../CentroLenguasExt/ActivaEstudianteJson',
          type: 'POST',
          dataType: 'json',
          data: {piOpcion:opcion, psControl: control, psNivel: nivel, psFecha: fecha},
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo exitosamente");
               Limpia();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {

     });
})
$("#btnBuscar").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
     CargaDatos();
});

$('#dtpFecha').datetimepicker({
     viewMode: 'years',
     format: 'YYYY-MM-DD HH:mm',
     locale: 'es',
     defaultDate: new Date()
});

function CargaDatos() {
     var control = $("#txtNoControl").val();
     if (!ex_no_control.test(control)) {
          MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
          return;
     }
     $.ajax({
          url: '../../../../CentroLenguasExt/RegresaDatosEstudiante',
          type: 'POST',
          dataType: 'json',
          data: { psControl: control },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               }
               else {
                    
                    var opcion = data.Mensaje.trim().split("|")[0];
                    var nombre = data.Mensaje.trim().split("|")[1];
                    var activaBoton = false;
                    $("#divDatos").hide();
                    $("#divHorario").hide();
                    $("#divReactivar").hide();
                    $("#divNuevo").hide();
                    switch(opcion)
                    {
                         case "0":
                              MensajesToastr("info", "Solicitud Procesada", "No existe el no. de control");
                              break;
                         case "1":
                              MensajesToastr("info", "Solicitud Procesada", "El estudiante "+nombre+", ya tiene curso de inglés registrado");
                              break;
                         case "2":
                              MensajesToastr("info", "Solicitud Procesada", "El estudiante " + nombre + ", no atendio la selección en horario asignado");
                              $("#divDatos").show();
                              $("#divHorario").show();
                              activaBoton = true;
                              break;
                         case "3":
                              var nivel = data.Mensaje.trim().split("|")[2];
                              $("#txtNivel").val(nivel);
                              $("#divDatos").show();
                              $("#divHorario").show();
                              $("#divReactivar").show();
                              activaBoton = true;
                              break;
                         case "4":
                              $("#divDatos").show();
                              $("#divHorario").show();
                              $("#divNuevo").show();
                              activaBoton = true;
                              break;
                    }
                    $("#txtNombre").val(nombre);
                    $("#hidOpcion").val(opcion);
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function Limpia()
{
     $("#divDatos").hide();
     $("#divHorario").hide();
     $("#divReactivar").hide();
     $("#divNuevo").hide();
     $("#hidOpcion").val("0");
}