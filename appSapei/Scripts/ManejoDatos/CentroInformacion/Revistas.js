var $rowTbl;


$(document).ready(function () {
    $('#datatable-buttons').DataTable().column(4).visible(false);
    $('#btnGuardar').attr('disabled', true);
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        $('#btnGuardar').attr('disabled', false);
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtSecciones").val(data[2]);
        $("#txtPublicacion").val(data[3]);
        $("#hidId").val(data[0]);
        $("#hidIdRevistas").val(data[4]);
        $("#txtEdicion").val(data[5]);
    });
    
    $("#frmRevistas").submit(function (event) {
        
        var secciones = $("#txtSecciones").val();
        var fechap = $("#txtPublicacion").val();
        var id = $("#hidId").val();
        var id_mat = $("#hidIdRevistas").val();
        var edicion = $("#txtEdicion").val();
        $.ajax({
            url: '../../../CentroInformacion/EditaRevistaJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piId: id, piSecciones: secciones,
                psFechaP: fechap, piId_Mat: id_mat,psEdicion:edicion
            }
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Revistas');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });
});
