
function OcultaDiv(valor) {
    $("#DocumentosInscripcion").hide();
    $("#divDocumentosEscolares").hide();
    $("#DocumentoAcademicos").hide();
    $("#Constancias").hide();
    $("#Otros").hide();
    $("#divDocumentosPersonales").hide();

    switch (valor) {
        case 1: $("#QueesExpDig").show();
            break;
        case 3: $("#divDocumentosPersonales").hide();
                $("#DocumentosEscolares").show();
            break;
        case 4: $("#QueesExpDig").hide();
                $("#DocumentoAcademicos").show();
            break;
        case 5: $("#QueesExpDig").hide();
                $("#Constancias").show();
            break;
        case 6: $("#QueesExpDig").hide();
                $("#Otros").show();
            break;
        case 7: $("#DocumentosInscripcion").hide();
                $("#divDocumentosPersonales").show();
            break;
        case 8: $("#divDocumentosPersonales").hide();
                $("#divDocumentosEscolares").show();
                    document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
            console.log(valor);
            break;
        case 9: $("#divDocumentosEscolares").hide();
               $("#divDocumentosPersonales").show();
            console.log(valor);
                document.body.scrollTop = 0;
                document.documentElement.scrollTop = 0;
            break;
        case 10: $("#divModalActualizacionDocumentos").hide();
            break;

        case 12: $("#divDocumentosEscolares").hide();
                 $("#divDocumentosPersonales").show();
            break;

    }

}

function MuestraDivDocPer() {
  $("#divDocumentosEscolares").hide();
    $("#divDocumentosPersonales").show();
}

function OcultaModal(id) {
    $("#divModalSolicitudInscripcion").hide();
    $("#divModalCartaResponsiva").hide();
    $("#divModalCartaCompromiso").hide();
    $("#divModalAvisoPrivacidad").hide();
    $("#divModalAvisoPrivacidad").hide();
    $("#divModalAutorizacionExpediente").hide();
    $("#divModalNoPertenenciaAOtroTecnologico").hide();
    $("#Valor").val(id);
    console.log(id);
}

function MuestraDocumentosEscolares(id) {
    var NoControl = $("#NoControl").val();

    $('#divPDF').load('../../../../EstudianteExpDig/RegresaDocumentosEscolares', { psControl: NoControl, penmDoc: id});
    $('#divModalVisorPDF').modal('show');
    $("#divPDF").show();
}

$("#btnFirmarDoc").click(function () {
    var NombreDoc = $("#Valor").val();
    $("#btnVistaPrevia" + NombreDoc).hide();
    $("#btnDescarga_" + NombreDoc).show();
})

$("#btnEnviarFirmar").click(function () { 
    var firma = $("#txtFirma").val();
    var NombreDoc = $("#Valor").val();
    console.log(NombreDoc);
    
    $.ajax({
        url: '../../../EstudianteExpDig/FirmaDocumento', 
        type: 'POST',
        dataType: 'json',
        data: { psFirma: firma, penmDoc: NombreDoc},
    })
        .done(function (data) {
            if (typeof (data.Success) == "undefined" || typeof (data.Success) == false) {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                $('#btnVistaPrevia_' + NombreDoc).html('<p class="card-text textoCardBody">Una vez firmado el documento, solo lo podrás descargar.</p><a id="btnDescarga_@lsNombreDocumento" class="btn btn-success" onclick="MuestraDocumentosEscolares(\'' + NombreDoc + '\');">Descargar</a>');
            }

        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
            $('.modal-backdrop').remove();
        });
    event.preventDefault();
})


$("#btnNadie").click(function () {
    $("#divPersonasAutorizo").hide();
})

var Parentesco;
var NombrePersona;
var contador = 0;

function MuestrabtnFirmar() {
    $("#Agregar").hide();
    $("#Nadie").hide();
    $("#btnGuardar").show();
    $("#Observacion").show();
    $("#btnRegresaTodo").show();
    Parentesco = " Nadie ";
    NombrePersona = "No autorizo la consulta de mi expediente";

}

$("#btnRegresaTodo").click(function () {
    $("#Agregar").show();
    $("#Nadie").show();
    $("#btnGuardar").hide();
    $("#Observacion").hide();
    $("#btnRegresaTodo").hide();

})


$("#btnRegresaOrigen").click(function () {

    $("#divModalPersonasAutorizo").modal('hide');
    $("#divModalAutorizacionExpediente").modal('hide');
    $("#btnRegresaOrigen").hide();
    $("#divModalAutorizacionExpediente").modal('show');
    $("#Nadie").show();

    if (Parentesco != null) {
        $('input[type=radio]').prop('checked', false);
        $("#Padre").show();
        $("#Madre").show();
        $("#Tutor").show();
    }
})

function RegresabtnFirmar() {

    if (contador == 0 ) {
        MensajesToastr("error", "Error de solicitud", "Debes de agregar a una persona a tu expediente");

        $("#divModalAutorizacionExpediente").modal('show');
        $("#Agregar").show();
        $("#Nadie").show();
        $("#btnActivarFirmar").hide();
    }
    else if (Parentesco != null  && NombrePersona != null) {
        $("#divModalAutorizacionExpediente").modal('show');
        $("#Agregar").hide();
        $("#Nadie").hide();
        $("#btnActivarFirmar").show();
    }
    else if (Parentesco != null && NombrePersona == null) {
        MensajesToastr("warning", "Error de solicitud", "Falta agregar el nombre de la persona a quién autorizas.");

        $("#divModalAutorizacionExpediente").modal('show');
        $("#Agregar").show();
        $("#Nadie").show();
        $("#btnActivarFirmar").hide();
    }

}


$("#btnAgregar").click(function () {
   
    $("#Nadie").hide();
    $("#btnRegresaOrigen").show();
    $("#divModalAutorizacionExpediente").modal('hide');
    $("#divModalPersonasAutorizo").find("input[type=radio]").on('change', function () {
        Parentesco = $('input[name=inlineRadioOptions]:checked').val();
      
        switch (Parentesco) {
            case "Padre": $("#SolicitaNombre1").show();
                $("#AgregarPersonas").show();
                $("#Padre").hide();

                break;
            case "Madre": $("#SolicitaNombre1").show();
                $("#AgregarPersonas").show();
                $("#Madre").hide();
                

                break;
            case "Tutor(a)": $("#SolicitaNombre1").show();
                $("#AgregarPersonas").show();
                $("#Tutor").hide();

                break;
        }
    });
})


$("#txtOtros").click(function () {

    if (contador <= 4) {
        $("#SolicitaNombre1").show();
    }
})

function obtener() {
    Parentesco = $("#txtOtros").val();
}

function obtenerNombre() {
    NombrePersona = $("#txtNombre1").val();
}


$("#btnGuardar").click(function () {
    var NoControl = $("#NoControl").val(); 

    $.ajax({
        url: '../../../EstudianteExpDig/FormatoAutorizacionExp',
        type: 'POST',
        dataType: 'json',
        data:
            { psNoControl: NoControl, psParentesco: Parentesco, psNomPersona: NombrePersona},
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                $("#btnGuardar").hide();
                $("#btnRegresaTodo").hide();
                $("#Observacion").hide();
                $("#btnActivarFirmar").show();
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
})

$("#btnGuardar1").click(function () {
    var NoControl = $("#NoControl").val();

    if (Parentesco == null || Parentesco.length == 0) {
       MensajesToastr("warning","Error de solicitud", "Debes de agregar a una persona a tu expedeinte");
        return false;
    }

    if (NombrePersona == null || NombrePersona.length == 0) {
        MensajesToastr("warning", "Error de solicitud", "Falta agregar el nombre de la persona a quién autorizas.");
        return false;
    }

    $.ajax({
        url: '../../../EstudianteExpDig/FormatoAutorizacionExp',
        type: 'POST',
        dataType: 'json',
        data:
            { psNoControl: NoControl, psParentesco: Parentesco, psNomPersona: NombrePersona },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                $("#Agregar").hide();
                $("#divModalAutorizacionExpediente").hide();
                $("#btnActivarFirmar").show();
                $("#txtOtros").val(" ");
                $("#txtNombre1").val(" ");
                Parentesco = null;
                NombrePersona = null;

                contador++;

                if (contador == 4) {
                    $("#divModalPersonasAutorizo").modal('hide');
                    $("#divModalAutorizacionExpediente").modal('show');
                    $("#Agregar").hide();
                    $("#btnActivarFirmar").show();
                }
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
})


$(function () {
    $("#divModalActualizacionDocumentos").find("input[type=radio]").on('change', function () {
        Clave_doc = $('input[name=inlineRadioOptions]:checked').val()
    });
})

$("#btnRealizarCambio").click(function () {
    $.ajax({
        url: '../../../EstudianteExpDig/ActualizacionDocumentos',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: NoControl, psClaveDoc: Clave_doc },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                if (Clave_doc == "AutorizacionExp") {
                    $("#divModalAutorizacionExpediente").show();
                }
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
})


 
