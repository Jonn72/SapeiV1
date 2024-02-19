
$(document).ready(function () {

});

$("#cboPeriodos").change(function (evento) {
    var periodo = $("#cboPeriodos").val();
    $('#BodyPrincipal').load('../../../Dep/AperturaPeriodoRP', { psPeriodo: periodo });
    evento.preventDefault();
});

$('#rpinicio').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#rpfin').datetimepicker({
    format: 'DD/MM/YYYY'
});

$('#primerS16').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#segundoS16').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#tercerS16').datetimepicker({
    format: 'DD/MM/YYYY'
});

$('#primerS24').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#segundoS24').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#tercerS24').datetimepicker({
    format: 'DD/MM/YYYY'
});

$("#frmAperturaPeriodo").submit(function (evento) {
    var periodo = $("#cboPeriodos").val();
    var inicio = $("#txtInicio").val();
    var fin = $("#txtFin").val();
    var primerS16 = $("#txtprimerS16").val();
    var segundoS16 = $("#txtsegundoS16").val();
    var tercerS16 = $("#txttercerS16").val();
    var primerS24 = $("#txtprimerS24").val();
    var segundoS24 = $("#txtsegundoS24").val();
    var tercerS24 = $("#txttercerS24").val();
    var titulo = $("#txtTitulo").val();
    var url = $("#txtUrl").val();
    if (url.length > 0) {
        if (url.toUpperCase().indexOf("YOUTUBE") >= 0) {
            url = url.replace("watch?v=", "embed/");
        }
    }
    $.ajax({
        url: '../../../DEP/ActivarPeriodoJsonRP',
        type: 'POST',
        dataType: 'json',
        data: { psPeriodo:periodo,
            psInicio: inicio, psFin: fin, psPrimerS16: primerS16, psSegundoS16: segundoS16, psTercerS16: tercerS16,
            psPrimerS24: primerS24, psSegundoS24: segundoS24, psTercerS24: tercerS24, psTitulo: titulo, psUrl: url
        },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {
                  MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Periodo Registrado");
                  $('#BodyPrincipal').load('../../../../DEP/AperturaPeriodoRP');
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
    evento.preventDefault();
});

