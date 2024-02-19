$("#frmContactanos").submit(function (event) {
     $(".alert").show();
     var nombre = $("#txtNombre").val();
     var correo = $("#txtCorreo").val();
     var asunto = $("#txtAsunto").val();
     var comentarios = $("#txtComentarios").val();

          $.ajax({
               url: '../../../Generales/EnviaContactanos',
               type: 'POST',
               dataType: 'json',
               data: {psNombre:nombre, psCorreo:correo, psAsunto:asunto, psCOmentarios:comentarios },
          })
          .done(function (data) {

               if (typeof (data.Success) === "undefined" || !data.Success) {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al Guardar</strong>");
                    $("#btnSiguiente").prop("disabled", false);
               }
               else {
                    $(".alert").addClass('alert-info');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Mensaje Enviado</strong>");
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No hay conexion con el servidor...</strong>");
          })
          .always(function () {
               setTimeout(function () {
                    if ($(".alert").hasClass('alert-danger')) {
                         $(".alert").removeClass('alert-danger');
                    } else if ($(".alert").hasClass('alert-warning')) {
                         $(".alert").removeClass('alert-warning');
                    } else if ($(".alert").hasClass('alert-info')) {
                         $(".alert").removeClass('alert-info');
                    } else {
                         $(".alert").removeClass('alert-success');
                    }
                    $(".alert strong").remove();
                    $(".alert").hide();
               }, 3000);
          });
     
          event.preventDefault();
     })

