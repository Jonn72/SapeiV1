
function ValidaExisteFolio() {
     var Folio = $("#txtFolio").val();
     if (ex_folio_aspirante.test(Folio)) {
          if (RegresaAspiranteDatos(Folio)) {
               $("#txtFolio").get(0).setCustomValidity('');
               return true;
          }
          $("#txtFolio").get(0).setCustomValidity('El folio no existe');
          return false;
     }
     $('#txtFolio').get(0).setCustomValidity('Ingrese un folio valido.');
     return false;
}
function ExisteFolio(Folio) {
     var esValido = false;
     $.ajax({
          async: false,
          url: '../../../../Aspirante/ExisteFolio',
          type: 'POST',
          dataType: 'json',
          data: { psFolio: Folio },
     })
          .done(function (data) {
               esValido = data.Success;
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     return esValido;
}
function RegresaAspiranteDatos(Folio) {
     var esValido = false;
     var nombreCompleto;
     $(".alert").show();
     $.ajax({
          async: false,
          url: '../../../Aspirante/RegresaAspiranteDatos',
          type: 'POST',
          dataType: 'json',
          data: { psFolio: Folio },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existe folio");
                    esValido = false;
               }
               else {
                    esValido = true;
                    var resultado = JSON.parse(data);
                    nombreCompleto = resultado.data[0].nombre + " " + resultado.data[0].paterno + " " + resultado.data[0].materno;
                    $("#txtNombreCompleto").val(nombreCompleto);
                    $("#txtEstatus").val(resultado.data[0].estatus);
                    $("#txtNombreCompleto").trigger("change");
                    $("#cboEstatusAspirante").empty();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     return esValido;
}