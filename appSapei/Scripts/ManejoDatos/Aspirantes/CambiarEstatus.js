$("#txtNombreCompleto").change(function () {
     CargaComboEstatus();
});
function CargaComboEstatus() {
     $.ajax({
          url: '../../../../Generales/RegresaComboEstatusAspirante',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existen estatus registrados");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboEstatusAspirante").empty();
                    for (; resultado.data[i];) {
                         $("#cboEstatusAspirante").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#frmModificaEstatusAspirante").submit(function (event) {

     var folio = $("#txtFolio").val();
     var estatus = $("#cboEstatusAspirante").val();
     $.ajax({
          asyn: false,
          url: '../../../Aspirante/ModificarEstatusAspirante',
          type: 'POST',
          dataType: 'json',
          data: { psfolio: folio, psIdEstatus: estatus },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
               $('#txtFolio').val("");
               $('#txtNombreCompleto').val("");
               $('#txtEstatus').val("");
               $("#cboEstatusAspirante").empty();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     event.preventDefault();
})