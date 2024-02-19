$(document).ready(function () {
});

$("#frmConsultas").submit(function (event) {
    var tipo = $("#cboConsultas").val();
    var periodo = $("#cboPeriodos").val();
    $("#divCargando").show();
    $("#iLoad").show("slow");
    $("#divTabla").hide();
    $(':input[type="submit"]').prop('disabled', true);
    $('#divTabla').load('../../../Consultas/EstudiantesJson/',
        { psPeriodo: periodo, poTipo: tipo }, function () {
            $(':input[type="submit"]').prop('disabled', false);
            $("#iLoad").hide("slow");
            $("#divTabla").show("slow");
        });
    /*$.ajax({
         url: '../../../Consultas/EstudiantesJson',
         type: 'POST',
         dataType: 'json',
         data: { psPeriodo: periodo, piTipo: tipo},
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
    });*/

    event.preventDefault();
})
