$("#btnGuardarDP").click(function Atras(evento) {

    var telefono = $("#txtTelefono").val();
    var telefonoEmer = $("#txtTelefonoEmergencia").val();
    var celular = $("#txtCelular").val();
    var civil = $("#cboEstadoCivil").val();
    var nss = $("#txtNSS").val();

    if (!ValidaCamposDP(telefono, telefonoEmer, celular, nss)) {
        return;
    }
    else {
        $.ajax({
            asyn: false,
            url: '../../../Aspirante/DatosPersonalesJson',
            type: 'POST',
            dataType: 'json',
            data: { psEstadoCivil: civil, psTelefono: telefono, psTelefonoEmergencia: telefonoEmer, psCelular: celular, psNSS: nss },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Se guardaron tus datos personales");
                    $("#btnNextDP").prop('disabled', false);
                    $("#btnGuardarDP").prop('disabled', true);
                    $("#btnGuardarDP").hide();
                    $("#btnGuardarDP").remove();

                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    }
    event.preventDefault();
})

$("#btnGuardarDO").click(function () {

    var calle = $("#txtCalle").val();
    var numero = $("#txtNumeroCalle").val();
    var id_cp = $("#cboColonia").val();
    var ciudad = $("#txtCiudad").text();

    if (!ValidaCamposDO(calle, numero, ciudad)) {
        return;
    }
    else {
        $.ajax({
            asyn: false,
            url: '../../../Aspirante/DatosDomicilioJson',
            type: 'POST',
            dataType: 'json',
            data: { psCalle: calle, psNumero: numero, psIdCP: id_cp },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Se guardaron tus datos personales");
                    $("#btnNextDO").prop('disabled', false);
                    $("#btnGuardarDO").prop('disabled', true);
                    $("#btnGuardarDO").hide();
                    $("#btnGuardarDO").remove();
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    }
    event.preventDefault();
})
$("#btnGuardarSC").click(function () {

    var enteraste = $("#cboEnteraste").val();
    var carrera1 = $("#cboCarrera1").val();
    var carrera2 = $("#cboCarrera2").val();


    $.ajax({
        asyn: false,
        url: '../../../Aspirante/DatosSeleccionCarreraJson',
        type: 'POST',
        dataType: 'json',
        data: { psEnteraste: enteraste, psCarrera1: carrera1, psCarrera2: carrera2 },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Se guardaron tus datos personales");
                $("#btnNextSC").prop('disabled', false);
                $("#btnGuardarSC").prop('disabled', true);
                $("#btnGuardarSC").hide();
                $("#btnGuardarSC").remove();
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });

    event.preventDefault();
})
$("#btnGuardarEP").click(function () {

    var escuela = $("#cboEscuelaProcedencia").val();
    var año = $("#txtAñoEgreso").val();
    var promedio = $("#txtPromedio").val();

    if (!ValidaCamposEP(año, promedio)) {
        return;
    }
    else {
        $.ajax({
            asyn: false,
            url: '../../../Aspirante/DatosEscuelaProcedenciaJson',
            type: 'POST',
            dataType: 'json',
            data: { psIdEscuela: escuela, psAño: año, psPromedio: promedio },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Se guardaron tus datos personales");
                    $("#btnNextEP").prop('disabled', false);
                    $("#btnGuardarEP").prop('disabled', true);
                    $("#btnGuardarEP").hide();
                    $("#btnGuardarEP").remove();

                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    }
    event.preventDefault();
})

function ValidaCamposDP(telefono, telefonoEmer, celular, nss) {
 
    if (nss ==! "00000000000")
        if (!ex_NSS.test(nss)) {
            MensajesToastr("info", "Mensaje de Sistema", "Ingrese un NSS valido")
            return false;
        }

    if (!ex_telefono.test(telefono)) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un telefono valido")
        return false;
    }
    if (!ex_telefono.test(telefonoEmer)) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un telefono valido")
        return false;
    }
    if (!ex_telefono.test(celular)) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un no. celular valido")
        return false;
    }

    return true;
}
function ValidaCamposDO(calle, numero, ciudad) {

    if (calle === "undefined" || calle.length == 0) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese la calle")
        return false;
    }

    if (numero === "undefined" || numero.length == 0) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un número de domicilio")
        return false;
    }

    if (ciudad === "undefined" || ciudad.length == 0) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un número de domicilio")
        return false;
    }
    return true;
}
function ValidaCamposEP(año, promedio) {


    if (!ex_año.test(año)) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un año valido")
        return false;
    }
    if (promedio.trim().length == 0) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un promedio valido")
        return false;
    }
    if (!ex_promedio.test(promedio)) {
        MensajesToastr("info", "Mensaje de Sistema", "Ingrese un promedio valido")
        return false;
    }
    return true;
}
