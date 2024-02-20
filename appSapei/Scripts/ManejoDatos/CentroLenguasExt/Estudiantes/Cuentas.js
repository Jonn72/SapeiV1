
$(".btnReset").click(function () {
    
    var servicio = this.name.split("-")[0];
    var contraseña = this.name.split("-")[1];
    $.ajax({
        url: '../../../Correos/ResetContraseñaCuentas',
        type: 'POST',
        dataType: 'json',
        data: { psServicio: servicio, psContraseña: contraseña },
    })
        .done(function (data) {
            if (typeof (data.Success) == "undefined") {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else if (typeof (data.Success) == false) {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "La solicitud se envío a la mesa de ayuda");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
        });
    event.preventDefault();
});