$(document).ready(function () {
    //DesactivaBotones();
});

$('#dtpFechaInicio, #dtpFechaFin').datetimepicker({
    viewMode: 'years',
    format: 'DD/MM/YYYY hh:mm',
    locale: 'es',
    defaultDate: new Date()
});

    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#cboCarreras").val(data[2]).change();
        $("#txtFechaInicio").val(data[3]);
        $("#txtIntervalo").val(data[4]);
        $("#txtPersonas").val(data[5]);
        event.preventDefault();
    });

    $('#datatable-buttons tbody').on('click', 'a.btn-info', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        CargaDatosListaCarrera(data[2]);
        $("#divModal").modal('show');
        event.preventDefault();
    });
$('#datatable-buttons tbody').on('click', 'a.btn-warning', function (event) {
    var $table = $('#datatable-buttons').DataTable();
    var $row = $(this).parents("tr");
    var data = $table.row($row).data();
    PublicaListas(data[2]);
});
$('#datatable-buttons tbody').on('click', 'a.btn-danger', function (event) {
    var $table = $('#datatable-buttons').DataTable();
    var $row = $(this).parents("tr");
    var data = $table.row($row).data();
    PublicaListas(data[2]);
});
function PublicaListas(carrera) {
    $.ajax({
        url: '../../../DEP/PublicaListasJson',
        type: 'POST',
        dataType: 'json',
        data: { psCarrera: carrera },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al Guardar Periodo");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                $('#BodyPrincipal').load('../../../../DEP/ReinscripcionFechasCarreras');

            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
        });
    event.preventDefault();
}
function CargaDatosListaCarrera(carrera) {
    $('#BodyPrincipal').load('../../../../DEP/ListasSeleccionMateriasCarreras', { psCarrera: carrera, piGenera:0 });
}
$("#frmPeriodos").submit(function (event) {
    event.preventDefault();
    var carrera = $("#cboCarreras").val()
    var iniRegistro = $("#txtFechaInicio").val()
    var intervalo = $("#txtIntervalo").val()
    var bloque = $("#txtPersonas").val();
    $.ajax({
        url: '../../../DEP/ReinscripcionFechasCarrerasJson',
        type: 'POST',
        dataType: 'json',
        data: { psCarrera: carrera, poInicio: iniRegistro, piIntervalo: intervalo, piBloque: bloque },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
                $('#BodyPrincipal').load('../../../../DEP/ReinscripcionFechasCarreras');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
        });
});
$("#btnGuardar").click(function Nuevo(event) {

})