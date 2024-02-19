$("#btnCambiar").click(function Nuevo(evento) {
    console.log("hola");

    var Actual = $("#password_actual").val();
    var Nueva = $("#nuevo_password").val();
    var RNueva = $("#confirma_password").val();

    if (Nueva === RNueva) {

        $.ajax({
            asyn: false,
            url: '../../../Generales/CambiaContraseña',
            type: 'POST',
            dataType: 'json',
            data: { psActual: Actual, psNueva: Nueva, psRNueva: RNueva },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Error al Guardar", data.Mensaje);
                }
                else {
                    MensajesToastr("success", "Registro Correcto", data.Mensaje);

                    $('#BodyPrincipal').load('../../../../Generales/CambiarContraseña');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {
            });
        event.preventDefault();
    }
    else {
        MensajesToastr("error", "Error", "Las contraseñas deben de ser iguales");
    }

});

    