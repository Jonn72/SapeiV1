$(document).ready(function () {
     $(".alert").hide();
     CargaComboServicios();
});
function CargaComboServicios()
{
     $.ajax({
          url: '../../../Generales/RegresaComboServicios',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "No existen servicios registrados");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboServicios").empty();
                    $("#cboServicios").append('<option value= 0>-- Seleccione el Servicio --</option>');
                    for (; resultado.data[i];) {
                         $("#cboServicios").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$('#cboServicios').on('change', function () {
     var valor = $('#cboServicios').val();
     $.ajax({
          url: '../../../Financieros/CargaYGuardaServicio',
          type: 'POST',
          dataType: 'json',
          data: { psID: valor },
     })
     .done(function (data) {

          if (typeof (data.Success) !== "undefined") {
               MensajesToastr("error", "Solicitud Procesada", "Error al Cargar Servicio");
          }
          else {
               $("#divCombo").hide();
               $("#divDatos").show();
               $("#txtConcepto").val(data.concepto);
               $('#txtMonto').val(data.monto);
               $('#cboSemestres option[value=' + data.semestre + ']').attr('selected', 'selected');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
})
$("#frmServicios").submit(function (event) {
     var monto = $("#txtMonto").val();
     var id = $('#cboServicios').val();
     var semestre = $('#cboSemestres').val();
     $.ajax({
          url: '../../../Financieros/CargaYGuardaServicio',
          type: 'POST',
          dataType: 'json',
          data: { psId: id, pdMonto: monto, piSemestre: semestre, pbGuarda: true },
     })
     .done(function (data) {
          if (!data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Cargar Servicio");
               $("#btnSiguiente").prop("disabled", false);
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
               LimpiaControles();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     event.preventDefault();
})
function LimpiaControles() {
     $("#divCombo").show();
     $("#divDatos").hide();
     $("#txtMonto").val('');
     $("#txtConcepto").val('');
     $('#cboSemestres option[value=0]').attr('selected', 'selected');
}

