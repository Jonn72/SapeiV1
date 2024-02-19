$(document).ready(function () {
     $("#txtNoControl").focus();
});
$('#txtNoControl,#txtQR').on('keypress', function (e) {
     if (e.which === 13) {
          CargaDatos();
     }
});
$("#btnAnterior").click(function Atras(evento) {
     event.preventDefault();
     LimpiaControles();
})
function CargaDatos() {
    var qr = $("#txtQR").val();
    var control = $("#txtNoControl").val();
    var tipo = $("#cboTipo").val();
     $.ajax({
          url: '../../../Financieros/CargaDatosRegistroVehicular',
          type: 'POST',
         dataType: 'json',
         data: { psQR: qr, psControl: control,psTipo:tipo },
     })
     .done(function (data) {
          if (typeof (data.Success) !== "undefined" && data.Success == false) {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
          }
          else {
               var resultado = JSON.parse(data);
               var valor = 0;
               $("#divCombo").hide();
               $("#divDatos").show();
               $("#txtNoControl1").val(resultado.data[0].usuario);
               $("#txtNombre").val(resultado.data[0].nombre);
               $("#txtMontoRegistrado").val(resultado.data[0].monto_pagar);
               $("#txtTipoVehiculo").val(resultado.data[0].tipo_vehiculo);
               valor = resultado.data[0].monto;
               if (valor !== null) {
                    $('#txtMontoRegistrar').val(valor);
                    MensajesToastr("info", "Solicitud Procesada", "El estudiante ya tiene pago registrado");
                    $('#txtMontoRegistrar').prop('readonly', true);
                    $("#btnGuardar").prop("disabled", true);
               }
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
}
$("#btnGuardar").click(function Atras(evento) {
     event.preventDefault();
     var noControl = $("#txtNoControl1").val();
     var tipo = $("#txtTipoVehiculo").val();
    var monto = $('#txtMontoRegistrar').val();
    alert(noControl)
     if (!ValidaDatos(monto))
     {
          return;
     }

     $.ajax({
          url: '../../../Financieros/GuardaPagoVehicular',
          type: 'POST',
          dataType: 'json',
          data: { psControl: noControl, psTipoVehiculo: tipo, piMonto: monto },
     })
     .done(function (data) {

          if (!data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Actualizar");
          }
          else {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
                    EnviaMensajeGnosis(noControl, "DRF", "Pago de Registro Vehícular", data.Mensaje);
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          LimpiaControles();
     });
})
function ValidaDatos(monto) {
     if (monto == $('#txtMontoRegistrado').val()) {
          return true;
     }
     MensajesToastr("info", "Validación de campos", "El monto a registrar debe ser igual al monto a pagar.");
     $("#txtMontoRegistrar").focus();
     return false;
}
function LimpiaControles() {
     $("#divCombo").show();
     $("#divDatos").hide();
     $("#txtNoControl").val('');
     $("#txtNombre").val('');
     $("#txtMontoRegistrar").val('');
     $("#txtRecibo").val('');
     $('#txtRecibo, #txtMontoRegistrar').prop('readonly', false);
     $("#btnGuardar").prop("disabled", false);
     $("#txtNoControl").focus();
}

