var genera;
$(document).ready(function () {
     $("#btnGuardar").hide();
     DesactivaBotones();
    QuitaBuscar();
    genera = true;
});
$('#cbxAcepto').change(function () {
     if ($('#cbxAcepto').is(':checked')) {
          $('#btnGuardar').removeAttr("disabled");
          $('#btnGuardar').show();
     }
     else {
          $('#btnGuardar').attr("disabled", true);
          $('#btnGuardar').hide();
     }
});
function LimpiaControles()
{
     $("#btnGuardar").hide();
     $('#btnGuardar').attr("disabled", true);
     $("#cbxAcepto").attr('checked', false);
}
$("#btnGuardar").click(function Nuevo(event) {
    if(genera)
        $('#divPDF').load('../../../../Reportes/FichaPagoReinscripcion', displaySection()); 
    event.preventDefault();
})

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}