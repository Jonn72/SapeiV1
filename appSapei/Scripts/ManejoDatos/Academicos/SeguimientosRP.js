$('#rpevaluacion').datetimepicker({
    format: 'DD/MM/YYYY'
});
var seguimiento = 0;
var no_de_control = 0;
function RegresaDatos(psseguimiento,no_de_control, CalifExterno, CalifInterno, txtFecha) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Academicos/RegresaDatosCalificacionRP',
        type: 'POST',
        dataType: 'json',
        data: { psSeguimiento: psseguimiento, psNoControl : no_de_control },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {
                 $(CalifExterno).val('');
                 $(CalifInterno).val('');
                 $(txtFecha).val('');
                 MensajesToastr("success", "Solicitud Procesada", "Este reporte aun no ha sido registrado");
                 document.getElementById('btnGuardar').disabled = false;
             }
             else {
                 var resultado = JSON.parse(data);
                 var i = 0;
                 $(CalifExterno).val(resultado.data[0].evaluacion_externo);
                 $(CalifInterno).val(resultado.data[0].evaluacion_interno);
                 $(txtFecha).val(resultado.data[0].fecha_evaluacion);
                 document.getElementById('btnGuardar').disabled = true;
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}

$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#Nombretxt").val(data[1]);        
        no_de_control = data[2];
        $('#Valida_Seguimiento').modal('show');
    });
    
    $("#btn1Seguimiento").click(function () {
        seguimiento = 1;
        $("#txtNoReporte").val(seguimiento);
        $("#NoControl").val(no_de_control);
        RegresaDatos(seguimiento,no_de_control, $("#CalifExterno"), $("#CalifInterno"), $('#txtFecha'));
        $('#CapturaCalififcacion').modal('show');
    });
    $("#btn2Seguimiento").click(function () {
        seguimiento = 2;
        $("#txtNoReporte").val(seguimiento);
        $("#NoControl").val(no_de_control);
        RegresaDatos(seguimiento, no_de_control, $("#CalifExterno"), $("#CalifInterno"), $('#txtFecha'));
        $('#CapturaCalififcacion').modal('show');
    });
    $("#btn3Seguimiento").click(function () {
        seguimiento = 3;
        $("#txtNoReporte").val(seguimiento);
        $("#NoControl").val(no_de_control);
        RegresaDatos(seguimiento, no_de_control, $("#CalifExterno"), $("#CalifInterno"), $('#txtFecha'));
        $('#CapturaCalififcacion').modal('show');
    });

    $("#frmGuardaCalificacion").submit(function (evento) {
        var calExterno = $("#CalifExterno").val();
        var calInterno = $("#CalifInterno").val();
        var noControl = $("#NoControl").val();
        var fecha = $("#txtFecha").val();
        var noReporte = $("#txtNoReporte").val();
        $.ajax({
            url: '../../../Academicos/AltaCalificacionJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                piCalExterno: calExterno, piCalInterno: calInterno, psNoControl: noControl, psFecha: fecha, piSeguimiento: seguimiento
            },
        })
                  .done(function (data) {
                      if (typeof (data.Success) !== "undefined" && !data.Success) {
                          MensajesToastr("info", "Solicitud Procesada <br />"+ data.Mensaje);
                      }
                      else {
                          MensajesToastr("success", "Solicitud Procesada", "Calificación Guardada");
                          $('#CapturaCalififcacion').modal('hide');
                          $('body').removeClass('modal-open');
                          $('.modal-backdrop').remove();
                          $('#BodyPrincipal').load('../../../../Academicos/SeguimientosRP');
                      }
                  })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });
});