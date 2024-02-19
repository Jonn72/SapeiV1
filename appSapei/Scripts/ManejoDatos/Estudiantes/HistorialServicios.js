$(document).ready(function () {
     DesactivaBotones();
    QuitaBuscar();
    genera = true;
});


$("#btnGuardar").click(function Nuevo(event) {
    var valor = $("#cboServicios").val();
    var servicio = valor.split("-")[0];
    $('#divPDF').load('../../../../Reportes/FichaPagoServicios', { psServicio: servicio }, displaySection()); 
    event.preventDefault();
})
$("#cboServicios").change(function (evento) {
    evento.preventDefault();
    var valor = $("#cboServicios").val();
    $("#txtMonto").val(valor.split("-")[1]);
    $("#txtVigencia").val(valor.split("-")[2]);
});
function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}
