var doble_click = true;

$(".vista_previa").click(function () {
    var noControl = $("#hidControl").val();
    var docClave = this.name;
    var text = "";

    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../ExpedienteDigitalDocumentos/RegresaDocumentoAlumno', { piClaveDoc: docClave, psNoControl: noControl }, displaySection());

    text = $("#" + docClave + ".vista_previa").text();
    if (text == "Ver Documento Autorizado")
        OcultaControles(docClave, true);
    else
        OcultaControles(docClave, false);
    event.preventDefault();
})

function OcultaControles(docClave, valor) {
    $("#btn_validar_" + docClave).prop('disabled', valor);
    $("#btn_rechazar_" + docClave).prop('disabled', valor);
    $("#" + docClave + ".rechazar_enviar").prop('disabled', true);
    $("#" + docClave + ".rechazar_enviar").hide();

    $("#div_Obs_" + docClave).hide();

    if (valor) {
        $("#btn_validar_" + docClave).hide();
        $("#btn_rechazar_" + docClave).hide();
    }
    else {
        $("#btn_validar_" + docClave).show();
        $("#btn_rechazar_" + docClave).show();
    }
}

$(".rechazar").click(function () {
    var docClave = this.name;
    $("#div_Obs_" + docClave).show();
    $("#btn_validar_" + docClave + ".validar").prop('disabled', true);

    event.preventDefault();
})

$(".rechazar_enviar").click(function () {
    var docClave = this.name;
    var observaciones = "";
    $("#div_Obs_" + docClave).show();

    if (!$("input[type='checkbox'][name='" + docClave + "']").is(':checked')) {
        MensajesToastr("info", "Aviso de Sistema", "Debe seleccionar al menos un motivo por el cual se rechaza el documento")
        return;
    }
    $("input[type='checkbox'][name='" + docClave + "']").each(function () {
        if ($(this).is(':checked')) {
            var texto = $('label[for="' + this.id + '"]').html();
            observaciones = observaciones + texto + ";"
        }
    })
    $("#" + docClave + ".rechazar_enviar").hide();
    $("#" + docClave + ".rechazar_enviar").prop('disabled', true);
    $("input[type='checkbox'][name='" + docClave + "']").attr("disabled", true);
    GuardaRevision(docClave, false, observaciones);
    event.preventDefault();
})

$(".validar").click(function () {
    var docClave = this.name;
    doble_click = true;
    AsignaValorTxtAlertaConfirmacion("Autorizar");
    AsignaValorHidAlertaConfirmacion(docClave);
    $('#ModalAlertaConfirmacion').modal('show');
    event.preventDefault();
})

$("#btnAlertaConfirmar").click(function () {
    var docClave = RegresaValorHidAlertaConfirmacion();
    $("#btn_vp_" + docClave).text("Ver Documento Autorizado");
    $("#btn_validar_" + docClave).remove();
    $("#btn_rechazar_" + docClave).remove();
    $('#ModalAlertaConfirmacion').modal('hide');

    if (!doble_click)
        return;
    doble_click = false;
    GuardaRevision(docClave, true, "");
    OcultaControles(docClave, true);
    event.preventDefault();
})



function GuardaRevision(docClave, valida, observaciones) {
    var control = $("#hidNoControl").val();
    $.ajax({
        async: false,
        url: '../../../ExpedienteDigitalDocumentos/GuardaValidacionDocumentoAlumno',
        type: 'POST',
        dataType: 'json',
        data: { piClaveDoc: docClave, psControl: control, pbValida: valida, psObservaciones: observaciones },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Realizado");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function (data) {

        })
}

$('.input_ckeck').click(function () {
    var docClave = this.name;

    if ($("input[type='checkbox'][name='" + docClave + "']").is(':checked')) {
        $("#" + docClave + ".rechazar_enviar").prop('disabled', false);
        $("#" + docClave + ".rechazar_enviar").show();
    }
    else {
        $("#" + docClave + ".rechazar_enviar").prop('disabled', true);
        $("#" + docClave + ".rechazar_enviar").hide();
    }
});

function displaySection(id) {
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}


function MuestraDocumentosEscolares(id) {
    var noControl = $("#hidControl").val();

    $('#divPDF').load('../../../../ExpedienteDigital/MuestraDocumentosEscolaresEstudiante', { psControl: noControl, penmDoc: id }, displaySection());
    $('#divModalVisorPDF').modal('show');
    $("#divPDF").show();
    event.preventDefault();
}

function MuestraDocEsc() {
    $("#divMuestraDocEsc").show;
    $("#DivDocPersonales").hide();
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

function MuestraDocEsc(id) {
    switch (id) {
        case 1: $("#MuestraDocEsc").show();
            $("#divMuestraDocEsc").show();
            $("#DivDocPersonales").hide();
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
            break;
        case 2: $("#DivDocPersonales").show();
            $("#MuestraDocEsc").hide();
            $("#divMuestraDocEsc").hide();
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
            break;
    }
}