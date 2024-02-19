
$("#btnAutorizarRegistro").on('click', function (evento) {
    ProcesaSolicitud();
    evento.preventDefault();
})


function ProcesaSolicitud() {
    var control = $("#hidNoControl").val();
    $.ajax({
        url: '../../../Vinculacion/AutorizaRegistroExtenporaneoJson',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: control},
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Realizado");
                $("#divDatos").html("");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
        });
}