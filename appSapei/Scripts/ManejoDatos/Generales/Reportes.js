

function RegresaReporte(nombre)
{
     $(".alert").show();

     $.ajax({
          asyn: false,
          url: '../../../Reportes/FichaAspirante',
          type: 'POST',
          dataType: 'json',
          data: { id: "PDF" },
     })
     .done(function (data) {

          return;
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

}