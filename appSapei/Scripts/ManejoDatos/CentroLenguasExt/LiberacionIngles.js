

$("#btnSolicitar").click(function Nuevo(event) {
    var NoControl = $("#spNoControl").text();
    var TipoLiberacion = $("#cboTipoLiberacion").val();
    var Promedio = $("#txtPromedio").val();
    if (!ValidaCampos(Promedio)) {
        MensajesToastr("info", "Solicitud Procesada", "Complete todos los campos");
    }
    else {
        RegistraSolicitud(NoControl, TipoLiberacion, Promedio)
    }
    event.preventDefault();
})
$("#btnAutorizar").click(function Nuevo(event) {
    var NoControl = $("#spNoControl").text();
    var TipoLiberacion = 0;
    var Promedio = 0;
    RegistraSolicitud(NoControl, TipoLiberacion, Promedio)
    event.preventDefault();
})
$("#btnGenerar").click(function Nuevo(event) {
    var NoControl = $("#spNoControl").text();
    var tipoDoc = 1;
    $('#divPDF').load('../../../../ReportesCLE/GeneraConstancia', { psNoControl: NoControl.trim(), penmTipo : tipoDoc }, displaySection);
    event.preventDefault();
});
function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}
function RegistraSolicitud(NoControl, TipoLiberacion, Promedio)
{
    $.ajax({
        asyn: false,
        url: '../../../CentroLenguasExt/LiberacionInglesJson',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: NoControl.trim(), piTipoLiberacion: TipoLiberacion, piPromedio: Promedio },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                LimpiaControles();
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}

function ValidaCampos(Promedio) {
    if (Promedio.length == 0) {
        MensajesToastr("info", "Solicitud Procesada", "Ingrese el promedio obtenido");
        return false;
    }
    return true;
}

function LimpiaControles() {
    $("#txtBuscarNoControl").val("");
    $("#divDatos").hide();
}


