$(document).ready(function () {
    $('#btnEliminar').off();
    $("#frmCategorias").submit(function (event) {
        var nombre = $("#txtDescripcion").val();
        var id = $("#hidId").val();
        $.ajax({
            url: '../../../CentroInformacion/GuardaCategoriaJson',
            type: 'POST',
            dataType: 'json',
            data: { piId: id, psNombre: nombre },
        }).done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Categorias');
                }
            }).fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    })
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtDescripcion").val(data[1]);
        $("#hidId").val(data[0]);
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function () {
        $rowTbl = $(this).parents("tr");

        $("#myModal").modal();
    });
    $('#btnEliminar').click(function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var data = $table.row($rowTbl).data();

        $.ajax({
            url: '../../../CentroInformacion/EliminaCategoriaJson',
            type: 'POST',
            dataType: 'json',
            data: { piId: data[0] }
        }).done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $('div.modal-backdrop').removeClass('modal-backdrop');
                $('#BodyPrincipal').load('../../../../CentroInformacion/Categorias');

            }
        }).fail(function (data) {
                MensajesToastrErrorConexion();
        });
    });
    $("input").blur(function () {
         $(this).val($(this).val().toUpperCase());
    });
});


