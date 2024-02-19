var periodo;
$(document).ready(function () {
    periodo = $('#hidPeriodo').val();
    CargaComboPeriodo(2, 1, periodo);
});
$("#cboPeriodo").change(function (evento) {
    evento.preventDefault();
    $('#BodyPrincipal').load('../../../../Vinculacion/ActivarPeriodoSS/' + $("#cboPeriodo").val());
});
$('#ssinicio').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#ssfin').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#ssCierre').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#ssb1').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#ssb2').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#ssb3').datetimepicker({
    format: 'DD/MM/YYYY'
});

$("#frmAperturaPeriodo").submit(function (evento) {
    var periodo = $("#cboPeriodo").val();
    var inicio = $("#txtInicio").val();
    var fin = $("#txtFin").val();
    var cierre = $("#txtFechaCierre").val();
    var nombre = $("#txtTitulo").val();
    var url = $("#txtUrl").val();
    var b1 = $("#txtB1").val();
    var b2 = $("#txtB2").val();
    var b3 = $("#txtB3").val();
    if (url.length > 0) {
        if (url.toUpperCase().indexOf("YOUTUBE") >= 0) {
            url = url.replace("watch?v=", "embed/");
        }
    }
    $.ajax({     
        url: '../../../Vinculacion/ActivarPeriodoJsonSS',
        type: 'POST',
        dataType: 'json',
        data: {psPeriodo:periodo, psInicio: inicio, psFin: fin, psCierre:cierre, psNombre: nombre, psUrl: url, psB1: b1, psB2: b2, psB3: b3 },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {
                 
                  MensajesToastr("error ", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Periodo registrado");
                  //ACTUALIZA BOBY PRINCIPAL
                  $('#BodyPrincipal').load('../../../../Vinculacion/ActivarPeriodoSS');//agregue
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
        });
    evento.preventDefault();
})