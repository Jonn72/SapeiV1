var ex_no_control = /^((E|R|C){1})*([0,1,2]{1})([0-9]{1})([0-9]{3})([0-9]{3,5})$/;
var ex_rfc = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;
var tipo_solicitud;
$(document).ready(function () {
    tipo_solicitud = "";
});

$("input").keyup(function () {
    $(this).val($(this).val().toUpperCase());
});
function ValidaUsuario(control) {
    tipo_solicitud = "A";
    if (!ex_no_control.test(control)) {
        tipo_solicitud = "E";
        if (!ex_rfc.test(control)) {
            MensajesToastr("info", "Solicitud Procesada", "No. de control o RFC incorrecto");
            tipo_solicitud = "";
            return false;
        }
    }
    return true;
}
$("#btnValidar").click(function Nuevo(evento) {
    var valor = $("#txtValor").val().trim();
    if (!ValidaUsuario(valor)) {
        return;
    }
    $.ajax({
        url: '../../../Home/IniciaSolicitudContraseña',
        type: 'POST',
        dataType: 'json',
        data: { psValor: valor, psTipo: tipo_solicitud },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
                LimpiaControles();
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    evento.preventDefault();
})
$("#btnSolicitar").click(function Nuevo(evento) {
    var codigo = $("#txtCodigo").val().trim();

    $.ajax({
        url: '../../../Home/ValidaSolicitudContraseña',
        type: 'POST',
        dataType: 'json',
        data: { psCodigo: codigo},
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                setTimeout(function () {
                    window.location.href = '../../Home/Index';
                }, 3000);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    evento.preventDefault();
})

