$(document).ready(function () {
    QuitarBotones();
     $("#txtNoControl").focus();
});
$("#btnBuscar").on('click', function (evento) {
    evento.preventDefault();
    var control = $("#txtNoControl").val();
    $('#BodyPrincipal').load('../../../../Financieros/Prorrogas', { psControl: control });
});

$("#btnAnterior").click(function Atras(evento) {
     evento.preventDefault();
    $('#BodyPrincipal').load('../../../../Financieros/Prorrogas');
})
$('#dtpFechaLimite').datetimepicker({
    viewMode: 'years',
    format: 'DD/MM/YYYY',
    locale: 'es',
    defaultDate: new Date()
});

$("#btnGuardar").click(function Atras(evento) {
     evento.preventDefault();
    var noControl = $("#hidControl").val();

     $.ajax({
         url: '../../../Financieros/GuardaPagoProrrogas',
          type: 'POST',
         dataType: 'json',
         data: { psNoControl: noControl },
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
     $("#txtNoControl").val('');
     $("#btnGuardar").prop("disabled", false);
     $("#txtNoControl").focus();
}
