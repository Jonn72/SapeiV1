var proceso_iniciado;
var proceso_cancelado;
$(document).ready(function () {
    proceso_iniciado = $("#hidIniciado").val();
    if (proceso_iniciado)
        CargaTabla();
});

$("#btnProcesar").click(function procesar(evento) {
    $("#divAvance").show();
    $("#btnProcesar").attr("disabled", true);
    Procesar()
     event.preventDefault();
})

$("#btnMonitorizar").click(function procesar(evento) {
    proceso_iniciado = $("#hidIniciado").val();
    alert(proceso_iniciado);
    CargaTabla();
    event.preventDefault();
})

function CargaTabla()
{
    proceso_cancelado = $("#hidCancelado").val();
    if (!proceso_cancelado)
        return;
    if (!proceso_iniciado)
        return;
    $('#divAvance').load('../../../../ServiciosEscolares/CargaDatosCierreSemestre');

}
function Procesar() {
     $.ajax({
          url: '../../../ServiciosEscolares/IniciaCierreSemestre',
          type: 'POST',
          dataType: 'json',
     })
     .always(function () {
         $('#modCargando').modal('hide');
         MensajesToastr("info", "Solicitud Procesada", "Se inicio el proceso de cierre de semestre");
         proceso_iniciado = true;
         CargaTabla();
     });
}