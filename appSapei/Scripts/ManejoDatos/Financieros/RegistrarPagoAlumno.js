
function BuscaNoControl(control) {
    $('#BodyPrincipal').load('../../../../Financieros/AutorizaReinscripcionAlumno/',
        { psControl: control });
}

$("#btnEliminarPago").on('click', function (evento) {
    ProcesaSolicitud(false);
    evento.preventDefault();
})

$("#btnAutorizarPago").on('click', function (evento) {
    ProcesaSolicitud(true);
    evento.preventDefault();
})

function ProcesaSolicitud(tipo) {
    var control = $("#hidNoControl").val();
    $.ajax({
        url: '../../../Financieros/AutorizaReinscripcionAlumnoJson',
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
                $("#divDatos").html("");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
        });
}