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
          url: '../../../../ExtraEscolares/RegresaActividadesInscritasEstudiante',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo : periodo, psNoControl:control},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               }
               else {
                    var resultado = JSON.parse(data);
                    var datos;
                    $("#txtNombre").val(resultado.data[0].nombre);
                    $("#cboActividad").empty();                    
                    $.each(resultado.data, function (index, value) {
                         $("#cboActividad").append('<option value=' + value.actividad + "|" + value.promedio + '>' + value.descripcion + '</option>');
                    });
                    CargaDatosActividad();
                    $('#divDatos').show();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#cboActividad").change(function (evento) {
     evento.preventDefault();
     CargaDatosActividad()
});
function CargaDatosActividad()
{
     var calificacion = $("#cboActividad").val().split("|")[1];
     $("#txtCalificacion").val(calificacion);
}
$("#frmCorregirCalif").submit(function (event) {
     event.preventDefault();
     var periodo = $("#cboPeriodo").val();
     var control = $("#txtNoControl").val();
     var actividad = $("#cboActividad").val().split("|")[0];
     var calif = $("#txtCalificacionN").val();
     if (!ValidaCalif(calif))
          return;
     $.ajax({
          url: '../../../ExtraEscolares/CorregirCalificacionJson',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psControl: control, piActividad: actividad, pfCalif: calif },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
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
     $("#cboActividad").empty();
     $('#divDatos').hide();
}