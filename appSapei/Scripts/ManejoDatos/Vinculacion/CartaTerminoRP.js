$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtNoControl").val(data[1]);
        $('#validaTermino').modal('show'); // abrir
    });
    $("#frmValidaCartaTermino").submit(function (evento) {
        var noControl = $("#txtNoControl").val();
        $.ajax({
            url: '../../../Vinculacion/ValidaCartaTerminoJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psNoControl: noControl
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Validación correcta");
                      $('#validaTermino').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Vinculacion/CartaTerminoRP');
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
    });

});