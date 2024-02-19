function EnviaMensajeGnosis(no_de_control, usuario, titulo, mensaje) {
     $.ajax({
          url: '../../../Gnosis/EnviarNotificacionPerosonzalida',
          type: 'POST',
          dataType: 'json',
          data: { psControl: no_de_control, psTitulo: titulo, psDescripcion: mensaje, psUsuario: usuario },
     })
     .done(function (data) {
          if (data.Success) {
               Command: toastr["success"](data.Mensaje, "")
          } else {
               Command: toastr["info"](data.Mensaje, "")
          }
     })
     .fail(function (data) {
          Command: toastr["error"]("No hay conexion con el servidor", "Verifica tu conexion")
     })
     .always(function () {

     });
}

