
$("#btnGenerar").on('click', function (evento) {
    evento.preventDefault();
    var oficio = $("#txtOficio").val();
    var fecha = $("#txtFecha").val();
    var control = $("#txtControl").val();
    var id = $("#cboDocumento").val();
    $("#divDocumento").show();
    $("#divVisor").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../ReportesCLE/GeneraDocumentos', { psOficio: oficio, psFecha: fecha, psControl: control, penmDoc:id} , displaySection);
});

function displaySection() {
    $("#divCargando").hide();
    $("#divPDF").show();
}
