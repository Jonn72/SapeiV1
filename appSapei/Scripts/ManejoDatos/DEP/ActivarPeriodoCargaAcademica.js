$(document).ready(function () {
    CargaComboPeriodo(2, 1);
    CargaComboCarreras();

  
});

$('#dtpFechaCargaIni, #dtpFechaCargaFin').datetimepicker({
     viewMode: 'years',
     format: 'DD/MM/YYYY',
     locale: 'es',
     defaultDate: new Date()
});

$("#btnGuardar").click(function () {
    var inicio = $("#txtIniCarga").val()
    var fin = $("#txtFinCarga").val();

     $.ajax({
         url: '../../../DEP/ActivaPeriodorCargaAcademicaJson',
          type: 'POST',
          dataType: 'json',
          data: {poInicio: inicio, poFin:fin},
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
     });
     event.preventDefault();
})
$("#btnFirmarCargasAcademicas").click(function () {
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})
$("#btnEnviarFirmar").click(function () {
    var firma = md5($("#txtFirma").val());
    $.ajax({
        url: '../../../DEP/FirmaCargasAcademicas',
        type: 'POST',
        dataType: 'json',
        data: { psFirma: firma },
    })
        .done(function (data) {
            if (typeof (data.Success) == "undefined") {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else if (typeof (data.Success) == false) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
        });
    event.preventDefault();
})

$("#btnArchivarCargas").click(function () {
    $.ajax({
        url: '../../../ExpedienteDigital/ArchivarCargasAcademicas',
        type: 'POST',
        dataType: 'json',
        data: {},
    })
        .done(function (data) {
            if (typeof (data.Success) == "undefined") {
                MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
            }
            else if (typeof (data.Success) == false) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
        });
    event.preventDefault();
})



$("#btnDescargar").click(function () {
    var periodo = $("#cboPeriodo").val()
    var carrera = $("#cboCarreras").val()
    var semestre = $("#cboSemestre").val()
    var url = '../../../../DocumentosOficiales/RegresaCargasAcademicasFirmaElectronica?psPeriodo=' + periodo + '&psCarrera=' + carrera + '&psSemestre=' + semestre;
    DescargarArchivo(url);
})
function displaySection() {
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
 
}