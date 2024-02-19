$(document).ready(function () {
});
$("#frmFinancierosReferencia").submit(function (event) {
     $.ajax({
          url: '../../../Financieros/EjecutaGeneraReferencias',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Actualizar");
               $("#btnSiguiente").prop("disabled", false);
          }
          else {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });

     event.preventDefault();
})

