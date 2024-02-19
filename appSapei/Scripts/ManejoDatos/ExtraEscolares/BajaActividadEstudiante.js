
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();
    var periodo;
    var nocontrol;
    var actividad;
    var id
    periodo = data[0];
    nocontrol = data[1];
    actividad = data[3];
    id = data[6];
    var r = confirm("¿esta seguro que desea eliminiar a " + nocontrol + " de la actividad " + actividad + "?");
    if (r == true) {
        $.ajax({
            url: '../../../ExtraEscolares/BajaActividadEstudianteJSON',
            type: 'POST',
            dataType: 'json',
            data: { psPeriodo: periodo, psNodeControl: nocontrol, psId: id },
        })
        .done(function (data) {
            if (typeof (data.Success) === "undefined" || !data.Success) {
                 MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
                $("#btnSiguiente").prop("disabled", false);
            }
            else {
                 MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
            }
        })
        .fail(function (data) {
             MensajesToastrErrorConexion();
        })
        .always(function () {
            $('#BodyPrincipal').load('../../../../ExtraEscolares/BajaActividadEstudiante');
        });
        event.preventDefault();
    }


});
