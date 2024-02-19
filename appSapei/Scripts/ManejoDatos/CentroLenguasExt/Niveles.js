$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();
});
$("#frmCLENiveles").submit(function (event) {
     var serie = $("#txtDescripcion").val();
     var nivel = $("#txtNivel").val();
    
     $.ajax({
          url: '../../../CentroLenguasExt/GuardaNivel',
          type: 'POST',
          dataType: 'json',
          data: { psNivel: nivel, piSerie: serie },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          $('#BodyPrincipal').load('../../../../CentroLenguasExt/RegistrarNivel');
     });
     event.preventDefault();
})
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();
     var descripcion;
     var nivel;
     $("#txtNivel").val(data[0]);
     $("#txtDescripcion").val(data[1]);
});
$("#btnNuevo").click(function Nuevo(evento) {
     LimpiaControles();
     evento.preventDefault();
})
function LimpiaControles() {
     $("#txtNivel").val("");
     $("#txtDescripcion").val("");
}
