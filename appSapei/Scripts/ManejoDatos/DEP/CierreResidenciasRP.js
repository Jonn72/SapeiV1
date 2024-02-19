$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        var Nocontrol = data[1];
        $("#NumeroControltxt").val(data[1]);
        RegresaCalificacionResidencias(Nocontrol);
    });
});

function RegresaCalificacionResidencias(Nocontrol) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../DEP/RegresaCalificacionFinalRP',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: Nocontrol },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {

                 MensajesToastr("error", "Solicitud Procesada", "Al solicitar los datos");
             }
             else {
                 var resultado = JSON.parse(data);
                 document.getElementById("Calificaciontxt").innerHTML = resultado.data[0].evaluacion_final;
                 $('#MuestraCalificacion').modal('show');
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}