$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtNoControl").val(data[1]);
        $('#ModalAutorizar').modal('show'); // abrir
    });

    $("#frmAutorizaResidencia").submit(function (evento) {
        var noControl = $("#txtNoControl").val();
        $.ajax({
            url: '../../../DEP/ValidaResidenciaJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psNoControl: noControl
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("error", "Solicitud Procesada", "Error al Autorizar");
                  }
                  else {
                      $('#ModalAutorizar').modal('hide'); // cerrar
                      MensajesToastr("success", "Solicitud Procesada", "Residencia Autorizada");
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../DEP/AutorizacionResidenciasRP');
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
    });
});