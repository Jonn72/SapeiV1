$("#frmEstatus").submit(function (event) {
          $.ajax({
               asyn: false,
               url: '../../../Reportes/FichaAspirante',
               type: 'POST',
               dataType: 'json',
               data: {id:"PDF"},
          })
          .done(function (data) {

               if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                    $("#btnSiguiente").prop("disabled", false);
               }
               else {
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
    
     event.preventDefault();
})