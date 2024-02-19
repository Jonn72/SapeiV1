
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
$("#frmRegistroTarjetonVehicular").submit(function (event) {
     event.preventDefault();
     var usuario = $("#txtUsuario").val();
     var tipo = $("#cboTipoVehiculo").val();
     var tarjeton = $("#txtTarjeton").val();
     $.ajax({
          url: '../../../../Materiales/ActualizaRegistroTarjetonVehicular',
          type: 'POST',
          dataType: 'json',
          data: { psUsuario: usuario, psTipo: tipo, psTarjeton: tarjeton },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
               }
               else {
                    MensajesToastr("info", "Solicitud Procesada", "Registro exitoso");
                    EnviaMensajeGnosis(usuario, "DRF", "Registro Vehícular", "Estimado Guardian, se te informa que ya puedes recoger tu tarjetón vehícular con clave: " + tarjeton + " en el Departamento de Recursos Materiales");
                    LimpiaControles();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });

})
function RegresaDatosSolicitante() {
     event.preventDefault();
     var usuario = $("#txtUsuario").val();
     var tipo = validaRFCoNoControl(usuario);
     var tipo_vehiculo = $("#cboTipoVehiculo").val();
     if (tipo != "") {
          $.ajax({
               url: '../../../../Materiales/RegresaDatosSolicitanteVehicular',
               type: 'POST',
               dataType: 'json',
               data: { psUsuario: usuario, psTipoUsuario: tipo, psTipoVehiculo: tipo_vehiculo, pbConDescripcion:true},
          })
               .done(function (data) {
                    if (typeof (data.Success) !== "undefined" && !data.Success) {
                         MensajesToastr("info", "Solicitud Procesada", "No se encontro el usuario: " + usuario);
                    }
                    else if (typeof (data.Success) !== "undefined" && data.Success) {
                         MensajesToastr("info", "Solicitud Procesada", "No tiene vehículo registrado");
                    }
                    else {
                         var resultado = JSON.parse(data);
                         $("#txtNombre").val(resultado.data[0].nombre);
                         $("#txtPlacas").val(resultado.data[0].placas);
                         $("#txtMarca").val(resultado.data[0].marca);
                         $("#txtSubMarca").val(resultado.data[0].submarca);
                         $("#txtTarjeton").val(resultado.data[0].tarjeton);
                         //resultado.data[i].estado_registro
                         $("#divUsuario").hide();
                         $("#divRegistro").show();
                    }
               })
               .fail(function (data) {
                    MensajesToastrErrorConexion();
               });
     } else {
          MensajesToastr("info", "Solicitud Procesada", "Debe ingresar un no. de control o RFC valido");
     }
}

function LimpiaControles() {
     $("#divUsuario").show();
     $("#divRegistro").hide();
     $("#txtUsuario").val("");
}


