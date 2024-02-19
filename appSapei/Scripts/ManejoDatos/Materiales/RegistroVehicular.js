var datosComboMarcas;
var datosComboSubMarcas;

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
$("#frmRegistroVehicular").submit(function (event) {
     event.preventDefault();
     var usuario = $("#txtUsuario").val();
     var tipo = $("#cboTipoVehiculo").val();
     marca = $("#cboMarcasVehiculos").val();
     var marca = (marca.trim()).split("-")[0];
     var submarca = $("#cboSubMarcasVehiculos").val();
     submarca = (submarca.trim()).split("-")[0];
     var placas = $("#txtPlacas").val();
     var color = $("#txtColor").val();
     var modelo = $("#txtModelo").val();
     $.ajax({
          url: '../../../../Materiales/ActualizaRegistroVehicular',
          type: 'POST',
          dataType: 'json',
          data: {psUsuario:usuario, psTipo:tipo, psMarca:marca, psSubmarca:submarca, psPlacas:placas,psColor:color,psModelo:modelo},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               }
               else {
                    MensajesToastr("info", "Solicitud Procesada", "Registro Correcto");
                    LimpiaControles();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });

})
function RegresaDatosSolicitante() {
     event.preventDefault();
     var tipo;
     var usuario;
     if ($("#divUsuario").length) {
          usuario = $("#txtUsuario").val();
          tipo = validaRFCoNoControl(usuario);
     }
     else {
          tipo = " ";
          usuario = " ";
     }
     var tipo_vehiculo = $("#cboTipoVehiculo").val();
     if (tipo != "") {
          $.ajax({
               url: '../../../../Materiales/RegresaDatosSolicitanteVehicular',
               type: 'POST',
               dataType: 'json',
               data: { psUsuario: usuario, psTipoUsuario: tipo, psTipoVehiculo: tipo_vehiculo },
          })
               .done(function (data) {
                    if (typeof (data.Success) !== "undefined" && !data.Success) {                        
                         MensajesToastr("info", "Solicitud Procesada", "No se encontro el usuario o " + data.Mensaje);
                    }
                    else {
                         $("#divTipoVehiculo").hide();
                         $("#divRegistro").show();
                         if (typeof (data.Success) !== "undefined" && data.Success) {
                              $("#txtNombre").val(data.Mensaje);
                              $("#cboMarcasVehiculos").trigger('change');
                         }
                         else {
                              var resultado = JSON.parse(data);
                              var i = 0;
                              $("#cboTipoVehiculo").trigger('change');
                              $("#txtNombre").val(resultado.data[i].nombre); 
                              $("#txtPlacas").val(resultado.data[i].placas);
                              $("#cboMarcasVehiculos").val(resultado.data[i].marca.trim() + "-" + resultado.data[i].tipo_vehiculo.trim());
                              $("#cboMarcasVehiculos").trigger('change');
                              $("#cboSubMarcasVehiculos").val(resultado.data[i].submarca.trim() + "-" + resultado.data[i].marca.trim());
                              $("#txtColor").val(resultado.data[i].color);
                              $("#txtModelo").val(resultado.data[i].modelo);
                         }
                    }
               })
               .fail(function (data) {
                    MensajesToastrErrorConexion();
               });
     } else {
          MensajesToastr("info", "Solicitud Procesada", "Debe ingresar un no. de control o RFC valido");
     }
}
$(function () {
     $("#cboMarcasVehiculos").ComboFiltraComboValor($("#cboTipoVehiculo"));
});

function LimpiaControles() {
     $("#divTipoVehiculo").show();
     if ($("#divUsuario").length) {
          $("#divUsuario").show();
     }
     $("#divRegistro").hide();
     $("#txtUsuario").val("");
}
$(function () {
     $("#cboSubMarcasVehiculos").ComboFiltraComboValor($("#cboMarcasVehiculos"));
});

