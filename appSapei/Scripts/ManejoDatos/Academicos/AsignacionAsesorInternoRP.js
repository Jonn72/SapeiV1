$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtIdPrograma").val(data[1]);
        $("#txtProyecto").val(data[3]);
        $('#AsignacionDocente').modal('show');
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaAsignacionInterno', { piProyecto: data[1] }, displaySection);
    });
    $("#frmAsignacionInterno").submit(function (evento) {
        var rfc = $("#cboRfc").val();
        var idProyecto = $("#txtIdPrograma").val();
        $.ajax({
            url: '../../../Academicos/AsignaInternoJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psRfc: rfc, piProyecto: idProyecto },
        })
               .done(function (data) {
                   if (typeof (data.Success) !== "undefined" && !data.Success) {
                       MensajesToastr("info", "Solicitud Procesada", "Ya existe docente asignado");
                       $('#AsignacionDocente').modal('hide');
                   }
                   else {
                       MensajesToastr("success", "Solicitud Procesada", "Docente Registrado");
                       $('body').removeClass('modal-open');
                       $('.modal-backdrop').remove();
                       $('#BodyPrincipal').load('../../../../Academicos/AsignacionAsesorInternoRP');
                       $('#AsignacionDocente').modal('hide');
                   }
               })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });
});

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}