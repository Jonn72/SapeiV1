$(document).ready(function () {
});

$("#btnRegresar").click(function Nuevo(event) {
    $('#BodyPrincipal').load('../../../../DEP/ReinscripcionFechasCarreras');
    event.preventDefault();
})
$("#btnGenerar").click(function Nuevo(event) {
    var carrera = $('#hidCarrera').val();
    $('#BodyPrincipal').load('../../../../DEP/ListasSeleccionMateriasCarreras', { psCarrera: carrera, piGenera: 1 });
    event.preventDefault();
})
$("#btnDescargar").click(function Nuevo(event) {
    var carrera = $('#hidCarrera').val();
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../Reportes/RegresaListasReinscripcion', { psCarrera: carrera }, displaySection);
    event.preventDefault();
});
function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}
$('#datatable-buttons tbody').on('click', 'a.btn-info', function (event) {
    var $table = $('#datatable-buttons').DataTable();
    var $row = $(this).parents("tr");
    var data = $table.row($row).data();
    CargaDatosListaCarrera(data[2]);
    $("#divModal").modal('show');
    event.preventDefault();
});
