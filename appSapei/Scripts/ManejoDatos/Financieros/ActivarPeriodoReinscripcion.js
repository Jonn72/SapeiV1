$(document).ready(function () {
    var periodo;
    periodo = $("#hidPeriodo").val()
    CargaComboPeriodo(2, 1, periodo);
    DesactivaBotones();
    QuitaBuscar();
});
$("#cboPeriodo").change(function (evento) {
    evento.preventDefault();
    $('#BodyPrincipal').load('../../../../Financieros/ActivarPeriodoReinscripcion/' + $("#cboPeriodo").val());
});
$('#dtpFechaIni, #dtpFechaFin, #dtpFechaIniRegistro, #dtpFechaFinRegistro').datetimepicker({
    viewMode: 'years',
    format: 'DD/MM/YYYY',
    locale: 'es',
    defaultDate: new Date()
});

$("#frmPeriodosActividades").submit(function (event) {
    var periodo = $("#cboPeriodo").val();
    var iniImprime = $("#txtIniImprime").val()
    var finImprime = $("#txtFinImprime").val();
    var finPago = $("#txtFinPago").val()
    var finRegistro = $("#txtFinPagoOrdinario").val();

    $.ajax({
        url: '../../../Financieros/GuardaPeriodoReinscripcion',
        type: 'POST',
        dataType: 'json',
        data: { psPeriodo: periodo, psIniImprime: iniImprime, psFinImprime: finImprime, psFinPago: finPago, psFinRegistro: finRegistro },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
            $('#BodyPrincipal').load('../../../../Financieros/ActivarPeriodoReinscripcion/' + $("#cboPeriodo").val());
        });
    event.preventDefault();
})