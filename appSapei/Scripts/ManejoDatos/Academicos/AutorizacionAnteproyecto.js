$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtNoControl").val(data[1]);
        $('#Validacion').modal('show'); // abrir
    });
    $("#btnValida").click(function () {
        var noControl = $("#txtNoControl").val();
        $.ajax({
            url: '../../../Academicos/AutorizaAnteproyectoJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psNoControl: noControl },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Validación Correcta");
                    $('#Validacion').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    $('#BodyPrincipal').load('../../../../Academicos/AutorizacionAnteproyectosRP');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    });
});