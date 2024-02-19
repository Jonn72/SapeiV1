var id;
var nombre_archivo;
$(document).ready(function () {
});
$("#cboDocOficiales").change(function (evento) {
     evento.preventDefault();
     var valor = $("#cboDocOficiales").val();
     if (valor == 0)
          return;
     var datos = valor.split(":");
     id = datos[0];
     if (datos[2] == "1")
        $("#divCombo").show();
     if (datos[3] == "1")
        $("#divSemestre").show();
     if (datos[8] == "0")
        $("#divControl").show();
    if (datos[4] == "1")
        $("#divSeguro").show();
    if (datos[5] == "1")
        $("#divImss").show();
    if (datos[6] == "1") 
        $("#divTitulo").show();
        
    
    if (datos[7] == "1")
        $("#divTraslado").show();

    $("#hidNombreReporte").val(datos[9]);
    $("#hidTipoDocumento").val(datos[1]);
});
$("#btnGenerar").click(function Nuevo(evento) {
     if ($("#cboDocOficiales").val() == "0")
     {
          MensajesToastr("error", "Solicitud Procesada", "Debe seleccionar algún documento");
          return;
     }
     $("#divCargando").show();
     $("#divPDF").hide();
    $('#divPDF').load('../../../../DocumentosOficiales/RegresaDocumentoOficial', { psId: $("#cboDocOficiales").val().split(":")[0], psPeriodo: $("#cboPeriodo").val(), psSemestre: $("#cboSemestre").val(), psControl: $("#txtNoControl").val(), psNombreReporte: $("#hidNombreReporte").val(), psTipoDoc: $("#hidTipoDocumento").val(), psFechaEmision: $("#txtFechaEmision").val(), psOficio: $("#txtNoOficio").val(), psNomSeguro: $("#txtNomSeguro").val(), psNoSeguro: $("#txtNoSeguro").val(), psIniSeguro: $("#txtFechaInicioSeguro").val(), psFinSeguro: $("#txtFechaFinSeguro").val(), psImss: $("#txtImss").val(), psFechaTitulo: $("#txtFechaTitulo").val(), psNombreDirector: $("#txtNombreDirector").val(), psNombreInstituto: $("#txtNombreInstituto").val() }, displaySection);
     event.preventDefault();
})
function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}
$('#dtpFecha').datetimepicker({
    viewMode: 'years',
    format: 'YYYY/MM/DD',
    locale: 'es',
    defaultDate: new Date()
});

$('#dtpFechaInicioSeguro').datetimepicker({
    viewMode: 'years',
    format: 'YYYY/MM/DD',
    locale: 'es',
    defaultDate: new Date()
});

$('#dtpFechaFinSeguro').datetimepicker({
    viewMode: 'years',
    format: 'YYYY/MM/DD',
    locale: 'es',
    defaultDate: new Date()
});

$('#dtpFechaTitulo').datetimepicker({
    viewMode: 'years',
    format: 'YYYY/MM/DD',
    locale: 'es',
    defaultDate: new Date()
});