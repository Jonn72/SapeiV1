
$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     DesactivaBotones();
     CargaComboPeriodo(2, 0, periodo);
});

$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../../CentroLenguasExt/ImprimirListaReinscripcion/' + periodo);
     evento.preventDefault();
});

$("#frmGeneraListas").submit(function (event) {
     var periodo = $("#cboPeriodo").val();
     //$("#divBoton").hide("slow");
     //$('#divCarga').show("slow");
     $("#iLoad").show("slow");
     $(':input[type="submit"]').prop('disabled', true);

     $.ajax({
          url: '../../../CentroLenguasExt/GeneraListas',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo },
     })
     .done(function (data) {
          
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Actualizar");
          }
          else {
              MensajesToastr("info", "Solicitud Procesada", data.Mensaje);

          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          $("#iLoad").hide("slow");
          $(':input[type="submit"]').prop('disabled', false);
     });

     event.preventDefault();
})
