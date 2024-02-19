
$(document).ready(function () {
     LimpiaControles();
});

$("#btnContinuar").click(function Continuar(evento) {
     RegresaDatosSolicitante();
})
$("#btnCalcelar").click(function Nuevo(evento) {
     LimpiaControles();
     evento.preventDefault();
})   
$("#frmEntregaTarjetonVehicular").submit(function (event) {
     event.preventDefault();
     var usuario = $("#txtSolicitante").val();
     var tipo = $("#txtTipo").val();
     $.ajax({
          url: '../../../../Materiales/RegistraEntregaTarjetonVehicular',
          type: 'POST',
          dataType: 'json',
          data: { psUsuario: usuario, psTipo: tipo},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
               }
               else {
                    MensajesToastr("success", "Solicitud Procesada", "Actualización Correcta");
                    EnviaMensajeGnosis(usuario, "DRF", "Registro Vehícular", "Estimado Guardian, gracias por recoger tu tarjetón vehicular en el Departamento de Recursos Materiales");
                    LimpiaControles();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });

})
function RegresaDatosSolicitante() {
     event.preventDefault();
     var tarjeton = $("#txtTarjeton").val();
     $.ajax({
          url: '../../../../Materiales/RegresaDatosTarjeton',
          type: 'POST',
          dataType: 'json',
          data: { psTarjeton: tarjeton },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "No se encontro el tarjetón: " + tarjeton);
               }
               else if (typeof (data.Success) !== "undefined" && data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "El tarjetón esta asignado a dos ó mas usuarios");
               }
               else {
                    var resultado = JSON.parse(data);
                    $("#txtSolicitante").val(resultado.data[0].usuario);
                    $("#txtPlacas").val(resultado.data[0].placas);
                    $("#txtTipo").val(resultado.data[0].tipo_vehiculo);
                    $("#divUsuario").hide();
                    $("#divRegistro").show();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });

}

function LimpiaControles() {
     $("#divUsuario").show();
     $("#divRegistro").hide();
     $("#txtTarjeton").val("");
}


