////$(document).ready(function () {
////     DesactivaBotones();
////     QuitaBuscar();

////     var table = $('#datatable-buttons').DataTable();

////     if (!table.data().count()) {
////         $("#divTabla").hide();
////     }

////});

function BuscaNoControl(control) {
    $('#BodyPrincipal').load('../../../../ServiciosEscolares/LiberacionCreditosComplementarios', { psControl: control });
}


function GeneraConstanciaAC(control) {
     event.preventDefault();
     $('#divPDF').load('../../../../DocumentosOficiales/ConstanciaLiberacionAC', { psControl: control }, displaySection);
     $("#divVisor").show();
}
$("#btnLiberacion").on('click', function (evento) {
    var control = $("#hidNoControl").val();
    $.ajax({
        url: '../../../../ServiciosEscolares/LiberacionCreditosComplementariosJson',
        type: 'POST',
        dataType: 'json',
        data: { psControl: control },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                GeneraConstanciaAC(control);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    evento.preventDefault();
});
$("#btnDescargar").on('click', function (evento) {
    var control = $("#hidNoControl").val();
    GeneraConstanciaAC(control);
    evento.preventDefault();
});

function displaySection(control) {
     $("#divCargando").hide();
     $("#divPDF").show();
}
