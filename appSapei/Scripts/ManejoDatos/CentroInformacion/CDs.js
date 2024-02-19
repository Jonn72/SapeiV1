
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
        $("#txtDescripcion").val(data[2]);
        $("#txtDuracion").val(data[3]);
        $("#hidIdMaterial").val(data[4]);
    });
    $("#frmCds").submit(function (event) {
        var material = $("#hidIdMaterial").val();
        var descripcion = $("#txtDescripcion").val();
        var duracion = parseFloat($("#txtDuracion").val());
        var id = $("#hidId").val();
       
        $.ajax({
            url: '../../../CentroInformacion/EditaCDJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piId: id, psDescripcion: descripcion, pfDuracion: duracion, piId_Mat: material 
            },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/CDs');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });



});
