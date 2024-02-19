$(function () {
     var periodo;
     //DesactivaBotones();
     //QuitaBuscar();
     periodo = $("#hidPeriodo").val()
     CargaComboPeriodo(2, 0, periodo);
     $("#cboGruposIngles").ComboFiltraCombo($("#cboNivelesIngles"));
     $("#cboNivelesIngles").trigger('change');
});
$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../../CentroLenguasExt/RegistrarNuevoEstudiante/', { psPeriodo: periodo });
     evento.preventDefault();
});
;
$("#frmCLEColocacion").submit(function (event) {
     var periodo = $("#cboPeriodo").val();
     var control = $("#txtNoControl").val();
     var nivel = $("#cboNivelesIngles").val();
     var grupo = $("#cboGruposIngles option:selected").text();    
     var actualizar = false;
     if ($("#cbxActualizar").is(':checked'))
          actualizar = true;
     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $.ajax({
          url: '../../../CentroLenguasExt/RegistrarNuevoEstudianteJson',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psControl: control, psNivel: nivel, psGrupo: grupo, pbActualiza: actualizar },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
               $('#divTabla').load('../../../../CentroLenguasExt/RegistrarNuevoEstudiante/', { psPeriodo: periodo, pbSoloTabla: true });
               $('#divPDF').load('../../../../Reportes/HorarioIngles', { psNoControl: control }, displaySection);
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {

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
function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}