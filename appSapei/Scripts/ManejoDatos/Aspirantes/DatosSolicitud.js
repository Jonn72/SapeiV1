$(document).ready(function () {
     //Carga de Combos  
     CargaCombos();
});
function CargaCombos() {
     CargaComboFormaEnterarse();
     CargaComboCarreras();
}
function CargaComboFormaEnterarse() {
     $.ajax({
          url: '../../../../Generales/RegresaComboComoEnteraste',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
     .done(function (data) {
          if (typeof (data.Success) !== "undefined") {
               MensajesToastr("error", "Solicitud Procesada", "No hay datos registrados");
          }
          else {
               var resultado = JSON.parse(data);
               var i = 0;
               $("#cboComoEnteraste").empty();
               for (; resultado.data[i];) {
                    $("#cboComoEnteraste").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                    i++;
               }
               var val = $("#hidComoEnteraste").val();
               if (val.trim().length != 0)
                    $('#cboComoEnteraste option[value=' + val + ']').attr('selected', 'selected');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
}
function CargaComboCarreras() {
     //cboCarrerasOfertadas
     $.ajax({
          url: '../../../../Generales/RegresaComboCarrerasOfertadas',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "No hay carreras ofertadas");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;

                    for (; resultado.data[i];) {
                         $("#cboCarrerasOfertadas").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         $("#cboCarrerasOfertadas2").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
                    var val = $("#hidCarrerasOfertadas").val();
                    if (val.trim().length != 0)
                         $('#cboCarrerasOfertadas option[value=' + val + ']').attr('selected', 'selected');
                    val = $("#hidCarrerasOfertadas2").val();
                    if (val.trim().length != 0)
                         $('#cboCarrerasOfertadas2 option[value=' + val + ']').attr('selected', 'selected');
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#frmDatosSolicitud").submit(function (event) {
     var enteraste = $("#cboComoEnteraste").val();
     var carrera1 = $("#cboCarrerasOfertadas").val();
     var carrera2 = $("#cboCarrerasOfertadas2").val();
     $.ajax({
          asyn: false,
          url: '../../../Aspirante/ModificarDatosSolicitud',
          type: 'POST',
          dataType: 'json',
          data: { psEnteraste: enteraste, psCarrera1: carrera1, psCarrera2: carrera2 },
     })
     .done(function (data) {

          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
               $("#btnSiguiente").prop("disabled", false);
          }
          else {
               $('#BodyPrincipal').load('../../../../Aspirante/DatosEscuela');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     event.preventDefault();
})
$("#btnAnterior").click(function Atras(evento) {
     event.preventDefault();
     $('#BodyPrincipal').load('../../../../Aspirante/DatosPersonales');
})