$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtFolio").val(data[2]);
        $('#valida').modal('show');
    });

    $("#frmBajaFolio").submit(function (evento) {
        var folio = $("#txtFolio").val();

        $.ajax({
            url: '../../../DEP/BajaFoliosJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                piFolio: folio
            },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud no procesada");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada","Folio inactivo");
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    $('#BodyPrincipal').load('../../../../DEP/BajaFoliosRP');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        evento.preventDefault();
    });

});