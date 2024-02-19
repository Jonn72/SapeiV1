$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaSolicitudResidenciasRP', { psNoControl: data[1] }, displaySection);
    });

    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtNoControl").val(data[1]);
        $('#ValidaSolicitud').modal('show'); // abrir
    });

    $("#frmValidaSolicitud").submit(function (evento) {
        var noControl = $("#txtNoControl").val();
        $.ajax({
            url: '../../../DEP/ValidaSolicitudJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psNoControl: noControl
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("error", "Solicitud Procesada", "Error al Validar");
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Validación correcta");
                      $('#ValidaSolicitud').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../DEP/SolicitudResidenciasRP');
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
