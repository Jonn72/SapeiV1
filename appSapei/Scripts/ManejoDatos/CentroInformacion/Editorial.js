var $rowTbl; 
$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        if (data[0] == 1) {
             MensajesToastr("info", "Solicitud Procesada", "Este registro no puede ser eliminado o modificado");
             return;
        }
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
        if (data[0] == 1) {
             MensajesToastr("info", "Solicitud Procesada", "Este registro no puede ser eliminado o modificado");
             return;
        }
        $.ajax({
            url: '../../../CentroInformacion/EliminaEditorialJson',
            type: 'POST',
            dataType: 'json',
            data: { piId: data[0] }
        }).done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $("#modCargando").modal('toggle');
                $('#BodyPrincipal').load('../../../../CentroInformacion/Editorial');

            }
        })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });

    });
    $("input").blur(function () {
         $(this).val($(this).val().toUpperCase());
    });
});

$("#frmEditorial").submit(function (event) {
    var nombre = $("#txtDescripcion").val();
    var id = $("#hidId").val();
    $.ajax({
        url: '../../../CentroInformacion/GuardaEditorialJson',
        type: 'POST',
        dataType: 'json',
        data: { piId: id, psNombre: nombre},
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $('#BodyPrincipal').load('../../../../CentroInformacion/Editorial');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    event.preventDefault();
});
