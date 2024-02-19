
$(document).ready(function () {

    $('#datatable-buttons').DataTable().column(4).visible(false);
    $('#btnGuardar').attr('disabled', true);
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        $('#btnGuardar').attr('disabled', false);
        var $table = $('#datatable-buttons').DataTable();
        var $rowParent = $(this).parents("tr");//row child
        var $row = $rowParent;
        var data = $table.row($row).data();
        $("#hidId").val(data[0]);
        data[2] = data[2].split(" ")[0];
        data[2] = data[2].replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");
        $("#txtFecha").val(data[2]);
        $("#txtPaginas").val(data[3]);
        $("#hidIdMaterial").val(data[4]);
    });
    $("#frmTesis").submit(function (event) {
        var material = $("#hidIdMaterial").val();
        var fechap = $("#txtFecha").val();
        var paginas = $("#txtPaginas").val();
        var id = $("#hidId").val();

        $.ajax({
            url: '../../../CentroInformacion/EditaTesisJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piId:id, psFechaP:fechap,
                piPaginas: paginas, piMaterial: material
            },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Tesis');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });



});
