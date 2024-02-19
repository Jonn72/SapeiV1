var genera;

$(function () {
    genera = true;
});


$("#btnCargaAcademica").click(function () {
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../DocumentosOficiales/RegresaCargaAcademicaFirmaElectronica', displaySection);
    $('#btnFirmarCargaAcademica').show();
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
    $("#btnFirmarCargaAcademica").prop('disabled', false);

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
                $('.modal-backdrop').remove();
                $('#BodyPrincipal').load('../../../../Estudiante/SeleccionMaterias');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
        });
    event.preventDefault();
})

$('#cbxAcepto').change(function () {
    if ($('#cbxAcepto').is(':checked')) {
        $('#btnDescargarFicha').removeAttr("disabled");
        $('#btnDescargarFicha').show();
        $('#btnIniciar').removeAttr("disabled");
        $('#btniniciar').show();
        $('#divModalTerminos').modal('show');
        $('.PagoElectronico').removeAttr("disabled");
        $('.PagoElectronico').show();
    }
    else {
        $('#btnDescargarFicha').attr("disabled", true);
        $('#btnDescargarFicha').hide();
        $('#btnIniciar').attr("disabled", true);
        $('#btnIniciar').hide();
        $('#divModalTerminos').modal('hide');
        $('.PagoElectronico').attr("disabled", true);
        $('.PagoElectronico').hide();
    }
});

$("#btnDescargarFicha").click(function Nuevo(event) {

    if (genera)
        $('#divPDF').load('../../../../Reportes/FichaPagoReinscripcion', function (responseText, statusText, xhr) {
            if (statusText == "success") {
                genera = false;
                $("#divCargando").hide();
                $('#divModalVisorPDF').modal('show');
                $("#divPDF").show();
            }
            if (statusText == "error")
                MensajesToastr("info", "Solicitud Procesada", "Error al generar la ficha de pago");
        });
    else
        displaySection2();
    event.preventDefault();
})

$("#btnIniciar").click(function Nuevo(event) {
    $('#BodyPrincipal').load('../../../../Estudiante/SeleccionMateriasJson');
    event.preventDefault();
})

function displaySection2() {
    genera = false;
    $("#divCargando").hide();
    $('#divModalVisorPDF').modal('show');
    $("#divPDF").show();
}