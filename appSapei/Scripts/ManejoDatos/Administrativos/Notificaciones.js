$(document).ready(function () {
    $("#sGrupo").select2();
    $("#frmNotificaciones").submit(function (e) {
        e.preventDefault();
        var titulo = $("#txtTitulo").val();
        var msg = $("#txtMensaje").val();
        var grupo = $("#sGrupo").val();
        $.ajax({
            url: "../../../AplicacionAdministrativos/EnviarNotificacion",
            type: "POST",
            dataType: "json",
            data: {
                psTitulo: titulo,
                psMsg: msg,
                psGrupo: grupo
            }
        }).done(function (data) {
            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
            }
        });
    });
});