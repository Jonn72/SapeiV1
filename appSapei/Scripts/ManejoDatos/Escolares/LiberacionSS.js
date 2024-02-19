$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();

     var table = $('#datatable-buttons').DataTable();

     if (!table.data().count()) {
         $("#divTabla").hide();
     }

});

function LiberaSS(control) {
     $.ajax({
         url: '../../../../ServiciosEscolares/LiberacionServicioSocialJson',
          type: 'POST',
          dataType: 'json',
          data: { psControl: control },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               }
               else {
                   MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                   BuscarInformacion();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function GeneraConstanciaAC(control) {
     event.preventDefault();
     $('#divPDF').load('../../../../DocumentosOficiales/ConstanciaLiberacionSS', { psControl: control }, displaySection);
     $("#divVisor").show();
}
$("#btnBuscar").on('click', function (evento) {
    BuscarInformacion();
});

function BuscarInformacion()
{
    event.preventDefault();
    var control = $('#txtNoControl').val();
    if (!ex_no_control.test(control)) {
        MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
        return;
    }
    $('#divTabla').load('../../../../ServiciosEscolares/LiberacionServicioSocial', { psControl: control });
    $("#divTabla").show();
}

function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}
