$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtNoControl").val(data[1]);
        $('#validaAceptacion').modal('show'); // abrir
    });
    $("#frmValidaCarta").submit(function (evento) {
        var noControl = $("#txtNoControl").val();
        $.ajax({
            url: '../../../Vinculacion/ValidaAceptacionJsonRP',
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
                      $('#cancelaFolio').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Vinculacion/CartaAceptacionRP');
                      //CambiaValorTabla(fin, 3, 0);
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();        
    });
});