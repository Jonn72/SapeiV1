var periodo;
$(document).ready(function () {
     CargaComboPeriodo(4, 1);
});
$("#btnBuscar").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
     CargaActividades();
});

function CargaActividades() {
     var periodo = $("#cboPeriodo").val();
     var control = $("#txtNoControl").val();
     if (!ex_no_control.test(control))
     {
          MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
          return;
     }
     $.ajax({
          url: '../../../../DesarrolloAcademico/RegresaTutoriaEstudiante',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo : periodo, psNoControl:control},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "No se encontraron datos");
               }
               else {
                    var resultado = JSON.parse(data);
                    var datos;
                    $("#txtNombre").val(resultado.data[0].nombre);
                    $("#txtGrupo").val(resultado.data[0].grupo);
                    $("#txtCalificacion").val(resultado.data[0].promedio);
                    $('#divDatos').show();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}

$("#btnGuardar").on('click', function (event) {
     event.preventDefault();
     var periodo = $("#cboPeriodo").val();
     var control = $("#txtNoControl").val();
     var grupo = $("#txtGrupo").val();
     var calif = $("#txtCalificacionN").val();
     if (!ValidaCalif(calif))
          return;
     $.ajax({
          url: '../../../DesarrolloAcademico/CorregirCalificacionJson',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo:periodo, psControl: control, psGrupo:grupo, pfCalif: calif },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Actualizacion exitosa");
               LimpiaControles();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     event.preventDefault();
});

function ValidaCalif(calif)
{
     if (calif == $("#txtCalificacion").val()) {
          MensajesToastr("info", "Solicitud Procesada", "La calificación es la misma que la actual");
          return false;
     }
     if (calif < 0 || calif > 4) {
          MensajesToastr("info", "Solicitud Procesada", "La calificación debe ser un valor entre 0 y 4");
          return false;
     }
     return true;
}
function LimpiaControles()
{
     $("#txtNoControl").val("");
     $("#txtCalificacion").val("");
     $("#txtCalificacionN").val("");
     $("#txtNombre").val("");
     $("#txtGrupo").empty();
     $('#divDatos').hide();
}