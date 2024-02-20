$(document).ready(function () {
     $("#btnGuardar").hide();
     DesactivaBotones();
     QuitaBuscar();
     CargaComboTipoActividades();
});
function CargaComboTipoActividades() {
     $.ajax({
          url: '../../../Generales/RegresaComboTipoActividades',
          type: 'POST',
          dataType: 'json',
          data: { pbFiltradas: true },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "No existen tipo de actividades registradas");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboTipoActividades").empty();
                    for (; resultado.data[i];) {
                         $("#cboTipoActividades").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
                    CargaComboActividades(resultado.data[0].Valor);
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function CargaComboActividades(id) {
     $.ajax({
          url: '../../../Generales/RegresaComboActividades',
          type: 'POST',
          dataType: 'json',
          data: { psId: id },
     })
          .done(function (data) {
               $("#cboActividades").empty();
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "No existen actividades registradas");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    for (; resultado.data[i];) {
                         $("#cboActividades").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#cboTipoActividades").change(function () {
     var valor = $("#cboTipoActividades option:selected").val();
     CargaComboActividades(valor);
});
$('#cbxAcepto').change(function () {
     if ($('#cbxAcepto').is(':checked')) {
          $('#btnGuardar').removeAttr("disabled");
          $('#btnGuardar').show();
     }
     else {
          $('#btnGuardar').attr("disabled", true);
          $('#btnGuardar').hide();
     }
});
$("#frmRegistraActividad").submit(function (event) {
     var tipo = $("#cboTipoActividades").val();
     var actividad = $("#cboActividades").val();
     var control = $("#txtNoControl").val();
     $.ajax({
          url: '../../../Estudiante/RegistraActividad',
          type: 'POST',
          dataType: 'json',
          data: { psTipo: tipo, piActividad: actividad, psControl:control },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               LimpiaControles();
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
               $('#BodyPrincipal').load('../../../../Estudiante/Extraescolares');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     event.preventDefault();
})
function LimpiaControles()
{
     $("#btnGuardar").hide();
     $('#btnGuardar').attr("disabled", true);
     $("#cbxAcepto").attr('checked', false);
}