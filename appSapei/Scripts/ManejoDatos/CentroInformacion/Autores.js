var $rowTbl; 
$(document).ready(function () {
    $('#btnEliminar').off();
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {  
                    var $table = $('#datatable-buttons').DataTable();
                    var $row = $(this).parents("tr");
                    var data = $table.row($row).data();

                    if (data[0] == 1) {
                         MensajesToastr("info", "Solicitud Procesada", "Este registro no puede ser eliminado o modificado");
                         return;
                    }
                    $("#txtNombre").val(data[1]);
                    $("#txtPaterno").val(data[2]);
                    $("#txtMaterno").val(data[3]);
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
            url: '../../../CentroInformacion/EliminaAutorJson',
            type: 'POST',
            dataType: 'json',
            data: { piId: data[0]}
        }).done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Autores');
                   
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });    
            
    });

    $("#frmAutores").submit(function (event) {
        var nombre = $("#txtNombre").val();
        var paterno = $("#txtPaterno").val();
        var materno = $("#txtMaterno").val();
        var id = $("#hidId").val();
        $.ajax({
            url: '../../../CentroInformacion/GuardaAutorJson',
            type: 'POST',
            dataType: 'json',
            data: { piId: id, psNombre: nombre, psPaterno: paterno, psMaterno: materno },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Autores');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });
    $("input").blur(function () {
         $(this).val($(this).val().toUpperCase());
    });
});



