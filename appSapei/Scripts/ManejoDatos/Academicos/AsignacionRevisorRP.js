function RegresaNombreAsesor(piProyecto, txtNombre) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Academicos/ConsultaRevisorRP',
        type: 'POST',
        dataType: 'json',
        data: { piId: piProyecto },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {
                 $(txtNombre).val('');
                 MensajesToastr("info", "Solicitud Procesada");
             }
             else {
                 var resultado = JSON.parse(data);
                 var i = 0;
                 $(txtNombre).val(resultado.data[0].rfc_revisor);
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}
$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtId").val(data[1]);
        $("#txtProyecto").val(data[2]);
        $('#AsignacionRevisor').modal('show');
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        var piProyecto = data[1];
        RegresaNombreAsesor(piProyecto, $("#txtNombre"));
        $('#verDocente').modal('show');
    });
    $("#frmAsignacionRevisor").submit(function (evento) {
        var rfc = $("#cboRfc").val();
        var proyecto = $("#txtId").val();
        $.ajax({
            url: '../../../Academicos/AsignaRevisorJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psRfc: rfc, piProyecto: proyecto },
        })
               .done(function (data) {
                   if (typeof (data.Success) !== "undefined" && !data.Success) {
                       MensajesToastr("error", "Solicitud Procesada", "Ya existe docente asignado");
                       $('#AsignacionDocente').modal('hide');
                   }
                   else {
                       MensajesToastr("success", "Solicitud Procesada", "Docente Registrado");
                       $('body').removeClass('modal-open');
                       $('.modal-backdrop').remove();
                       $('#BodyPrincipal').load('../../../../Academicos/AsignacionRevisorRP');
                       $('#AsignacionRevisor').modal('hide');
                   }
               })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });
});