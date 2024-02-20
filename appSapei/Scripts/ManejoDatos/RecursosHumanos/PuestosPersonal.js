DesactivaBotones();
ocultar_buscar();
QuitarMostrar();


function guardapuesto() {
    var RFC = $("#txtRFC").val();
    var Puesto = $("#cboPuesto").val();

        $.ajax({
            asyn: false,
            url: '../../../RecursosHumanos/GuardaPuesto',
            type: 'POST',
            dataType: 'json',
            data: { psRFC: RFC, psPuesto: Puesto, psBaja: 0 },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Guardado");
                    $('#BodyPrincipal').load('../../../RecursosHumanos/PuestosPersonal?psRFC='+RFC);
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
}

function eliminapuesto(RFC, Puesto) {

    $.ajax({
        asyn: false,
        url: '../../../RecursosHumanos/GuardaPuesto',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: RFC, psPuesto: Puesto, psBaja: 1 },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al Eliminar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Puesto Eliminado");
                $('#BodyPrincipal').load('../../../RecursosHumanos/PuestosPersonal?psRFC=' + RFC);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}
