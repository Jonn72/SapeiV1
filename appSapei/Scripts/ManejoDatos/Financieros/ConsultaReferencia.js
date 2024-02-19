$(document).ready(function () {
     $("#txtNoControl").focus();
});
$('#txtNoControl').on('keypress', function (e) {
     if (e.which === 13) {
          CargaDatos();
          e.preventDefault();
     }
});
$("#btnAnterior").click(function Atras(evento) {
     event.preventDefault();
     LimpiaControles();
})
function CargaDatos() {
     var control = $("#txtNoControl").val();
     if (!ValidaCampo($("#txtNoControl"), 'NOCONTROL')) {
          MensajesToastr("info", "Solicitud Procesada", "Ingrese un no. de control valido");
          return;
     }
     $.ajax({
          url: '../../../Financieros/CargaDatosReferencia',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: control },
     })
     .done(function (data) {
          if (typeof (data.Success) !== "undefined" && data.Success == false) {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
          }
          else {
               var resultado = JSON.parse(data);
               $("#divCombo").hide();
               $("#divDatos").show();
               $("#txtReferencia").text(resultado.data[0].referencia);
               $("#txtNombre").text(resultado.data[0].nombre);
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
}

function LimpiaControles() {
     $("#divCombo").show();
     $("#divDatos").hide();
     $("#divEspecial").hide();
     $("#divIngles").hide();
     $("#txtNoControl").val('');
     $("#txtReferencia").text('');
     $("#txtNombre").text('');
     $("#txtNoControl").focus();
}
