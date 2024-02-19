

$("#txtNoControl").on('input', function () {
     var val = this.value;
     var valor;
     var carrera;
     var nombre;

});

$("#frmCambioNoControl").submit(function (event) {
     var control = $("#txtNoControl").val();
     var nuevo_control = $("#txtNoControlNuevo").val();
     $.ajax({
          url: '../../../ServiciosEscolares/CambiaNoControlJson',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: control, psNoControlNuevo: nuevo_control },
     })
     .done(function (data) {
          
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
          }
          else {
               MensajesToastr("info", "Solicitud Procesada", "Cambio realizado correctamente");
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });

     event.preventDefault();
})
