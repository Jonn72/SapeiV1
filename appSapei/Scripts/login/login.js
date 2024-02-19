(function () {


     var ip;
     $(".alert").hide();
     $("#form_log").submit(function (event) {
          var usr = $("#usr").val().trim();
          var psw = $("#psw").val().trim();
          var tipo_usuario = validaUsuario(usr);

          $(".alert").show();
          if (usr != "" && psw != "" && tipo_usuario != "") {
               psw = RegresaMd5(psw, tipo_usuario);
               $.ajax({
                    url: '../ManejoSesion/UserLogin',
                    type: 'POST',
                    dataType: 'json',
                    data: { nombre: usr, contrasenia: psw, tipousuario: tipo_usuario },
               })
			.done(function (data) {
			     $(".alert").addClass(data.Clase);
			     $(".alert").html(data.Mensaje);
			     if (data.Success) {
			          setTimeout(function () {
			               window.location.href = data.Index;
			          }, 1000);
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
			     }, 2000);
			});

          } else {
               $(".alert").addClass('alert-warning');
               $(".alert").html("<strong><span class='lnr lnr-warning'></span>&nbsp; Verifica los campos...</strong>");
               setTimeout(function () {
                    $(".alert").removeClass('alert-warning');
                    $(".alert strong").remove();
                    $(".alert").hide();
               }, 2000);
          }
          event.preventDefault();
     });
}());

     function RegresaMd5(psContrasenia, psUsuario) {
          if (psUsuario === "PERSONAL" || psUsuario === "DOCENTE")
               return md5(psContrasenia);
          return psContrasenia;
     }


