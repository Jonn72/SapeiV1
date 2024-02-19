$(document).ready(function () {
     $("#txtNoControl").focus();
});
$("#btnBuscar").on('click', function (evento) {
    evento.preventDefault();
    var control = $("#txtNoControl").val();
    $('#BodyPrincipal').load('../../../../Financieros/Condonacion', { psControl: control });
});

$("#btnAnterior").click(function Atras(evento) {
     evento.preventDefault();
    $('#BodyPrincipal').load('../../../../Financieros/Condonacion');
})
$("#btnGuardar").click(function Atras(evento) {
     evento.preventDefault();
    var noControl = $("#hidControl").val();
    var clave_monto = "";
    $('input[name ="monto_condonar"]').each(function (index, value) {
            clave_monto = clave_monto + $(this).attr('id') + ':' + $(this).val() + "|" ;
    });
    if (clave_monto.length > 0)
        clave_monto = clave_monto.substring(0, clave_monto.length - 1);
    
     $.ajax({
          url: '../../../Financieros/GuardaPagoCondonacion',
          type: 'POST',
          dataType: 'json',
         data: { psNoControl: noControl, psMonto: clave_monto },
     })
     .done(function (data) {

          if (!data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Actualizar");
          }
          else {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);            
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          LimpiaControles();
     });
})

function LimpiaControles() {
     $("#divCombo").show();
     $("#divDatos").hide();
     $("#divEspecial").hide();
     $("#divIngles").hide();
     $("#txtNoControl").val('');
     $("#txtNoControl1").text('');
     $("#txtNombre").text('');
     $("#txtMontoRegistrado").text('');
     $('#txtMontoCondonacion').val('');
     $('#txtMontoReinscripcion').text('');
     $('#txtMontoEspecial').text('');
     $('#txtMontoIngles').text('');
     $("#btnGuardar").prop("disabled", false);
     $("#txtNoControl").focus();
}
