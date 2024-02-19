
function BuscaNoControl(control) {
    $('#BodyPrincipal').load('../../../../CentroLenguasExt/ActivaDesactivaPago/',
        { psControl: control });
}

$("#btnEliminarPago").on('click', function (evento) {
    ProcesaSolicitud(false);
    evento.preventDefault();
})

$("#btnActivarPago").on('click', function (evento) {
    ProcesaSolicitud(true);
    evento.preventDefault();
})

function ProcesaSolicitud(tipo) {
    var control = $("#hidNoControl").val();
    $.ajax({
        url: '../../../CentroLenguasExt/EliminaActivaPagoJson',
        type: 'POST',
        dataType: 'json',
        data: { psControl: control, pbTipo : tipo },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Realizado");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
        });
}