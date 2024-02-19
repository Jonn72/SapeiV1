var periodo;
var liberacion;
$(document).ready(function () {
});

$("#btnBuscar").on('click', function (evento) {
    evento.preventDefault();
    var control = $("#txtNoControl").val().trim();
    if (!ex_no_control.test(control)) {
        MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
        return;
    }
    CargaDatos();
});
$("#txtNoControl").change(function () {
    LimpiaDatos();
});
function LimpiaDatos() {
    $('#divMuestra').hide();
    $("#txtNombre").val("");
    $("#txtCreditos").val("");
    $("#txtFechaFinSeleccion").val("");
    $("#txtFechaSeleccion").val("");
}
function CargaDatos() {
    var control = $("#txtNoControl").val().trim();
    $('#divDatos').load('../../../DEP/DatosReinscripcion', { psControl: control });
    $('#divMuestra').show();
}
function RealizaValidacion() {
    var fecha_fin = $("#txtFechaFinSeleccion").val().trim();
    $('#divMuestra').hide();
    //if (fecha_fin.length == 0) {
    //    MensajesToastr("info", "Solicitud Procesada", "no tiene registro de fin se sesión, no es necesario el reset");
    //    return;
    //}
    $('#divMuestra').show();
}
$("#btnReset").click(function Nuevo(event) {
    event.preventDefault();
    var control = $("#txtNoControl").val().trim();
    var fecha_fin = $("#txtFechaFinSeleccion").val().trim();
    //if (fecha_fin.length == 0) {
    //    MensajesToastr("info", "Solicitud Procesada", "no tiene registro de fin se sesión, no es necesario el reset");
    //    return;
    //}
    $('#BodyPrincipal').load('../../../../DEP/JsonResetHoraSeleccion', { psControl: control });
    MensajesToastr("info", "Solicitud Procesada", "Reset realizado");
})
$("#btnValidar").click(function Nuevo(event) {
    event.preventDefault();
    var control = $("#txtNoControl").val().trim();
    var autoriza = false;
    if ($('#cbxExtemporaneo').prop('checked')) {
        autoriza = true;
    }
    alert(autoriza);
    $('#BodyPrincipal').load('../../../../DEP/AutorizaReinscripcionExtJson', { psControl: control, pbAutoriza: autoriza });
    MensajesToastr("info", "Solicitud Procesada", "Autorización Activa");
})