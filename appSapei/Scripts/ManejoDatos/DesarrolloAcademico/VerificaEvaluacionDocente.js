$("#btnBuscar").click(function Nuevo(evento) {
    RegresaEstudianteDatos($("#txtNoControl").val());
    evento.preventDefault();
})

function RegresaEstudianteDatos(noControl) {
    if (!esFormatoValidoAlert())
        return
    Cargar(noControl);
}

function esFormatoValidoAlert() {
    var noControl = $("#txtNoControl").val();
    if (!ex_no_control.test(noControl)) {
        MensajesToastr("warning", "Solicitud Procesada", "Ingrese un no. de control valido");
        return false;
    }
    return true;
}

function Cargar(noControl) {
    $('#BodyPrincipal').load('../../../../DesarrolloAcademico/VerificaEvaluacionDocente?psNoControl='+noControl);
}
