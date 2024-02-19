$("input").keyup(function () {
     $(this).get(0).setCustomValidity('');
});

$("#txtNombre,#txtPaterno, #txtMaterno, #txtCURP").keyup(function () {
     $(this).val(EliminaAcentos($(this).val()));
     $(this).val($(this).val().toUpperCase());
});

$("input[type=password]").keyup(function () {

     if ($("#txtContrase1").val().length == 8) {
          $("#8char").removeClass("glyphicon-remove");
          $("#8char").addClass("glyphicon-ok");
          $("#8char").css("color", "#00A41E");
     } else {
          $("#8char").removeClass("glyphicon-ok");
          $("#8char").addClass("glyphicon-remove");
          $("#8char").css("color", "#FF0004");
     }

     if ($("#txtContrase2").val().length == 8) {
          if ($("#txtContrase1").val() == $("#txtContrase2").val()) {
               $("#pwmatch").removeClass("glyphicon-remove");
               $("#pwmatch").addClass("glyphicon-ok");
               $("#pwmatch").css("color", "#00A41E");
          } else {
               $("#pwmatch").removeClass("glyphicon-ok");
               $("#pwmatch").addClass("glyphicon-remove");
               $("#pwmatch").css("color", "#FF0004");
          }
     } else {
          $("#pwmatch").removeClass("glyphicon-ok");
          $("#pwmatch").addClass("glyphicon-remove");
          $("#pwmatch").css("color", "#FF0004");
     }

    
});

$("#mc-form").submit(function (event) {
     var curp = $("#txtCURP").val();
     var nombre = $("#txtNombre").val();
     var paterno = $("#txtPaterno").val();
     var materno = $("#txtMaterno").val();
     var correo = $("#txtCorreo").val();
     var contra1 = $("#txtContrase1").val();
     var contra2 = $("#txtContrase2").val();

     if (ValidaCampos(nombre,paterno, materno, correo, contra1, contra2, curp))
     {
          $("#divFinal").show();
          $("#divContraseña").hide();
          $("#divCargando").show();
          $("#divAviso").hide();
          $.ajax({
               url: '../../../Aspirante/RegistraNuevo',
               type: 'POST',
               dataType: 'json',
               data: { psNombre: nombre, psPaterno: paterno, psMaterno: materno, psCorreo: correo, psContrasena: contra1, psContra2: contra2, psCurp:curp },
          })
          .done(function (data) {
               
               $("#divRegistro").hide();
               $("#divCargando").hide();
               $("#divAviso").show();
               if (!data.Success) {
                    var mensaje = "Error al guardar";
                    if (typeof (data.Mensaje) !== "undefined") {
                         mensaje = data.Mensaje;
                    }
                    MensajesToastr("info", "Solicitud Procesada", mensaje);
                    setTimeout(function () {
                         window.location.href = '../../Aspirante/Registro';
                    }, 3000);
               }
               else {
                    mensaje = data.Mensaje;
                    MensajesToastr("success", "Solicitud Procesada", "Pre-registro exitoso");
                    /*setTimeout(function () {
                         window.location.href = '../../Aspirante/Registro';
                    }, 15000);*/
               }
               $('#mensaje').text(mensaje);

          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     }    
     event.preventDefault();
})

function ValidaCampos(nombre, paterno, materno, correo, contra1, contra2, curp)
{
     if (curpValida(curp))
          $("#txtCURP").get(0).setCustomValidity('');
     else {
          $("#txtCURP").get(0).setCustomValidity('Ingrese un CURP valido');
          return false;
     }
     if (ex_nombres.test(nombre))
          $("#txtNombre").get(0).setCustomValidity('');
     else {
          $("#txtNombre").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(paterno))
          $("#txtPaterno").get(0).setCustomValidity('');
     else {
          $("#txtPaterno").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(materno))
          $("#txtMaterno").get(0).setCustomValidity('');
     else {
          $("#txtMaterno").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     //if (!ComparaCurpNombreApellidos(curp, nombre, paterno, materno)){
     //     MensajesToastr("info", "Solicitud Procesada", "El CURP no corresponde al nombre y apellidos ingresados");
     //     return false;
     //}
     if (ex_correo.test(correo))
          $("#txtCorreo").get(0).setCustomValidity('');
     else {
          $("#txtCorreo").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }     
     if (contra1.length != 8)
          return false;
     
     if(contra1!==contra2)
          return false;
     
     return true;
}
function ValidaCamposDatos(nombre, paterno, materno, curp) {
     if (!curpValida(curp)){
          MensajesToastr("info", "Solicitud Procesada", "Ingrese un CURP valido");
          return false;
     }
     if (!ex_nombres.test(nombre)) {
          MensajesToastr("info", "Solicitud Procesada", "Ingrese un nombre valido");
          return false;
     }
     if (!ex_nombres.test(paterno)){
          MensajesToastr("info", "Solicitud Procesada", "Ingrese un apellido paterno valido");
          return false;
     }
     if (!ex_nombres.test(materno)){
         MensajesToastr("info", "Solicitud Procesada", "Ingrese un apellido materno valido");

          return false;
     }
     //if (!ComparaCurpNombreApellidos(curp, nombre, paterno, materno)) {
     //     MensajesToastr("info", "Solicitud Procesada", "El CURP no corresponde al nombre y apellidos ingresados");
     //     return false;
     //}
     return true;
}

function InvalidMsg(textbox) {
     if (textbox.value == '') {
          textbox.setCustomValidity('Campo Requerido');
     } else {
          if (curpValida(textbox.value))
               textbox.setCustomValidity('');
          else
               textbox.setCustomValidity('Ingrese un CURP valido');
     }
     return true;
}
$("#btnPaso1").click(function Atras(evento) {
     event.preventDefault();
     var valida = $("#hidValida").val();

     if (ValidaCamposDatos($("#txtNombre").val(), $("#txtPaterno").val(), $("#txtMaterno").val(), $("#txtCURP").val())) {
          $('#divRegistro').hide();
          $('#divCorreo').show();
     }
})
$("#btnPaso2").click(function Atras(evento) {
     event.preventDefault();
     var a = $('#hidCodigo').val();
     var b = $('#txtCodigo').val();
     if (!ex_correo.test($('#txtCorreo').val())) {
          MensajesToastr("info", "Solicitud Procesada", "Ingrese su correo eléctronico");
          return false;
     }
     if (a===b) {
          $('#divCorreo').hide()
          $('#divContraseña').show()
     }
     else
          MensajesToastr("info", "Solicitud Procesada", "El código es incorrecto");

})
$("#btnValidar").click(function Atras(evento) {

     var correo = $("#txtCorreo").val();
     var nombre = $("#txtNombre").val();
     var paterno = $("#txtPaterno").val();
     var materno = $("#txtMaterno").val();
     nombre = nombre + " " + paterno + " " + materno;
     $("#hidCorreo").val(correo);
     if (ex_correo.test(correo)) {
          $("#iLoad").show("slow");
          $(':input[type="submit"]').prop('disabled', true);
          $.ajax({
               url: '../../../Generales/EnviaCodigoValidacionCorreo',
               type: 'POST',
               dataType: 'json',
               data: { psNombre:nombre, psCorreo: correo},
          })
          .done(function (data) {
               if (!data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al eviar correo de validción");
               }
               else {
                    $("#hidCodigo").val(data.Mensaje);
                    MensajesToastr("success", "Solicitud Procesada", "Se ha enviado un correo con el código de validación");
               }
               $('#mensaje').text(mensaje);

          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          })
     .always(function () {
          $("#iLoad").hide("slow");
          $(':input[type="submit"]').prop('disabled', false);
     });
     }
     else
          MensajesToastr("info", "Solicitud Procesada", "El correo tiene un formato incorrecto");

     event.preventDefault();

})