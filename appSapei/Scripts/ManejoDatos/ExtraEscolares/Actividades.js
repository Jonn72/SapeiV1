var table = $('#datatable-buttons').DataTable();
$(document).ready(function () {
     CargaComboTipoActividades();
     CargaComboEntrenadores();
});
function CargaComboTipoActividades() {
     $.ajax({
          url: '../../../Generales/RegresaComboTipoActividades',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existen tipo de actividades registradas");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboTipoActividades").empty();
                    for (; resultado.data[i];) {
                         $("#cboTipoActividades").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function CargaComboEntrenadores() {
     $.ajax({
          url: '../../../Generales/RegresaComboEntrenadores',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existen tipo de actividades registradas");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboEntrenadores").empty();
                    for (; resultado.data[i];) {
                         $("#cboEntrenadores").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#frmActividades").submit(function (event) {
     var id = $("#hidId").val();
     var tipo = $("#cboTipoActividades").val();
     var descripcion = $("#txtDescripcion").val();
     var entrenador = $("#cboEntrenadores").val();
     var capacidad = $("#txtCapacidad").val();
     if (!ValidaTodosDias()) {
          $(window).scrollTop(0);          
          MensajesToastr("info", "Solicitud Procesada", "Verifique los horarios");
     }
     else {
          var horario = RegresaHorario();
          $.ajax({
               url: '../../../ExtraEscolares/GuardaActividad',
               type: 'POST',
               dataType: 'json',
               data: { piId: id, psTipo: tipo, psDescripcion: descripcion, piEntrenador: entrenador, psHorario: horario, piCapacidad:capacidad },
          })
          .done(function (data) {

               if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
               }
               else {
                    MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
                    $('#divTabla').load('../../../../ExtraEscolares/RecargaActividades',{pbSoloTabla: true });
                    LimnpiaControles();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     }
     event.preventDefault();
})
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = table.row(this).data();
     var tipo;
     var descripcion;
     var capacidad;
     var horario
     tipo = data[1];
     descripcion = data[2];
     entrenador = data[3];
     capacidad = data[4];
     $("#hidId").val(data[0]);
     $("#cboTipoActividades option:contains(" + tipo + ")").attr('selected', 'selected');
     $("#txtDescripcion").val(descripcion);
     $("#cboEntrenadores option:contains(" + entrenador + ")").attr('selected', 'selected');
     $("#txtCapacidad").val(capacidad);
     CargaHorarios(data[6]);
});
$("#btnNuevo").click(function Nuevo(evento) {
     LimnpiaControles();
     evento.preventDefault();
})
function LimnpiaControles()
{
     $("#hidId").val("0");
     $("#txtDescripcion").val("");
     $("#txtCapacidad").val(1);
     LimpiaHorarios();
}