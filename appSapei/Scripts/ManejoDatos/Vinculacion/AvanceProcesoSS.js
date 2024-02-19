
$("#btnIniciaEliminar").click(function () {
    $("#txtNoControl").val($("#hidNoControl").val());
    $('#divEliminarProceso').modal('show');//abrir modal            
});

$("#btnEliminar").click(function () {
    event.preventDefault();
    var noControl = $("#txtNoControl").val();
    var contraseña = md5($("#txtContraseña").val());
    var justificacion = $("#txtJustificacion").val();
    if (contraseña.trim().length == 0) {
        MensajesToastr("error", "Solicitud Procesada", "Debe ingresar la contraseña");
        return;
    }
    if (justificacion.trim().length == 0) {
        MensajesToastr("error", "Solicitud Procesada", "Debe ingresar la justificacion");
        return;
    }
    $.ajax({
        url: '../../../Vinculacion/EliminarProcesoSS',
        type: 'POST',
        dataType: 'json',
        data: {
            psNoControl: noControl, psContraseña: contraseña, psJustificacion: justificacion
        },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            $("#txtNoControl").val("");
            $("#txtContraseña").val("");
            $("#txtJustificacion").val("");
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
});
