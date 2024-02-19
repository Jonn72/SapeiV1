var $rowTbl;
$(document).ready(function () {
    $('#datatable-buttons').DataTable().rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        var selected = data[1];

        if (selected === 'True')
            
            data[1] = '<div class="custom-control custom-checkbox mr-sm-2">' +
                '<input type = "checkbox" class="custom-control-input"  checked="true" >' +
                '</div>';//'<input type="checkbox" class="editor-active" checked="true">';
        else
            data[1] = '<input type="checkbox" class="editor-active" >';
        this.data(data);
    });
    //evento de checkbox de tabla
    $('#datatable-buttons').on('change', 'input.editor-active', function () {
        var $row=$(this).parents("tr");//get parent
        var $table = $('#datatable-buttons').DataTable();
        var $data = $table.row($row).data();
        $.post('../../../CentroInformacion/ReservaEjemplarJson', { psId: $data[0], pbReserva: $(this).is(":checked")});
    });
    $('#btnEliminar').off();//necesario

    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function () {
        $rowTbl = $(this).parents("tr");//get parent
        $("#myModal").modal();
    });
    $('#btnEliminar').click(function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var data = $table.row($rowTbl).data();
        $.ajax({
            url: '../../../CentroInformacion/EliminaEjemplarJson',
            type: 'POST',
            dataType: 'json',
            data: { psId: data[0] }
        }).done(function (data) {
            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $('div.modal-backdrop').removeClass('modal-backdrop');
                $('#BodyPrincipal').load('../../../../CentroInformacion/Ejemplar');

            }
        })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });

    });
});