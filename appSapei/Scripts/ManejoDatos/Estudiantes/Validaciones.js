function esFormatoValido() {
     var noControl = $("#txtNoControl").val();
     if (ex_no_control.test(noControl)) {
          if (!ExisteNoControl(noControl)) {
               $("#txtNoControl").get(0).setCustomValidity('');
               return true;
          }
          $("#txtNoControl").get(0).setCustomValidity('El no. de control ya existe');
          return false;
     }
     $('#txtNoControl').get(0).setCustomValidity('Ingrese un no. de control valido.');
     return false;
}
function esFormatoValidoAlert() {
     var noControl = $("#txtNoControl").val();
     if (!ex_no_control.test(noControl)) {
          MensajesToastr("warning", "Solicitud Procesada", "Ingrese un no. de control valido");
          return false;
     }
     return true;
}
function ValidaExisteNoControl() {
     var noControl = $("#txtNoControl").val();
     if (ex_no_control.test(noControl)) {
          if (RegresaEstudianteDatos(noControl)) {
               $("#txtNoControl").get(0).setCustomValidity('');
               return true;
          }
          $("#txtNoControl").get(0).setCustomValidity('El no. de control no existe');
          return false;
     }
     $('#txtNoControl').get(0).setCustomValidity('Ingrese un no. de control valido.');
     return false;
}
function ExisteNoControl(noControl) {
     var esValido = false;
     $.ajax({
          async: false,
          url: '../../../../Estudiante/ExisteNoControl',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: noControl },
     })
          .done(function (data) {
               esValido = data.Success;
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
               }, 2000);
          });
     return esValido;
}
function RegresaEstudianteDatos(noControl) {
     var esValido = false;
     var nombreCompleto;
     $.ajax({
          async: false,
          url: '../../../../Estudiante/RegresaEstudianteDatos',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: noControl },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "Error de conexión");
               }
               else {
                    esValido = true;
                    nombreCompleto = data.nombre_alumno + " " + data.apellido_paterno + " " + data.apellido_materno;
                    $("#txtNombreCompleto").val(nombreCompleto);
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     return esValido;
}