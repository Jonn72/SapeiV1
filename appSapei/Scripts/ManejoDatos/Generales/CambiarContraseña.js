var esCifrada;
$(document).ready(function () {
     esCifrada = $("#hidCifrada").val();
});
$("#frmCambiarContraseña").submit(function (event) {
     var contraActual = $("#txtContraseña").val();
     var contraNueva = $("#txtContraseñaNueva").val();
     var asunto = $("#txtAsunto").val();
     var comentarios = $("#txtComentarios").val();
     
          $.ajax({
               url: '../../../Generales/CambiaContraseña',
               type: 'POST',
               dataType: 'json',
               data: { psNombre: nombre, psCorreo: correo },
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
               TemporizadorAlert(3000);
          });
     
     event.preventDefault();
})
$("txtContrase1").onblur(function () {
     var contra;
     if (esCifrada === 1)
          contra = md5($("#hidContra").val());
     else
          contra = $("#hidContra").val();

     if ($("#txtContrase1").val() != contra) {
          $("#txtContrase1").get(0).setCustomValidity('');
     }
     else {
          $("#txtContrase1").get(0).setCustomValidity('La contraseña nueva no puede ser igual a la anterior');
          return false;
     }
});
$("txtContrase2").onblur(function () {
     if ($("#txtContrase1").val() == $("#txtContrase2").val()) {
          $("#txtContrase2").get(0).setCustomValidity('');
     }
     else {
          $("#txtContrase2").get(0).setCustomValidity('La contraseña nueva no puede ser igual a la anterior');
          return false;
     }
});
