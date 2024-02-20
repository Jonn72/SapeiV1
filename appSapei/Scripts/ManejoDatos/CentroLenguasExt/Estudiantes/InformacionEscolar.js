var periodo;
$(document).ready(function () {
});

$("#btnCargaAcademica").click(function () {
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../DocumentosOficiales/RegresaCargaAcademicaFirmaElectronica', displaySection);
    event.preventDefault();
})
$("#btnFirmarCargaAcademica").click(function () {
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})


function displaySection() {
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
    $('#divModalFirmaElectronica').modal('hide');
    $("#btnFirmarCargaAcademica").show();

}
$("#btnEnviarFirmar").click(function () {
    var firma = $("#txtFirma").val();
    $.ajax({
        url: '../../../Estudiante/FirmaCargaAcademica',
        type: 'POST',
        dataType: 'json',
        data: { psFirma: firma },
    })
        .done(function (data) {
            if (typeof (data.Success) == "undefined") {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else if (typeof (data.Success) == false) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                $("#btnEnviarFirmar").prop('disabled', true);
                $("#btnFirmarCargaAcademica").prop('disabled', true);
                $("#btnEnviarFirmar").hide();
                $("#btnFirmarCargaAcademica").hide();
                $('#divModalFirmaElectronica').modal('hide');

            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
        });
    event.preventDefault();
})

function OcultaModals() {
    $('#divModalVisorPDF').modal('hide');
    $("#divCargando").hide();
    $("#divPDF").hide();
    $('#divModalFirmaElectronica').modal('hide');
}

function MuestraOcultar() {
    $("#btnOcultarReticula").show();
    $("#btnMostrarReticula").hide();
}
$("#btnOcultarReticula").click(function () {
    $("#btnOcultarReticula").hide();
    $("#divReticula").hide();
    $("#btnMostrarReticula").show();
    event.preventDefault();
})