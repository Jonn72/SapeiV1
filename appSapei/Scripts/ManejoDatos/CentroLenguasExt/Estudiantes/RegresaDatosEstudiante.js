$("#btnBuscarEstudiante").click(function Nuevo(evento) {
     ValidaEstudianteDatos($("#txtNoControl").val());
     evento.preventDefault();
})

function ValidaEstudianteDatos(noControl) {
     if (!esFormatoValidoAlert())
          return
     CargarResultado(noControl);
}
function CargarResultado(noControl) {
     $.ajax({
          url: '../../../../Estudiante/RegresaEstudianteDatos',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: noControl },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "No se encontro el estudiante con no. de control: " + noControl + "");
               }
               else {
                    $("#divNoControl").hide();
                    $("#divDatos").show();
                    $("#txtNombreCompleto").val(data.nombre_alumno + " " + data.apellido_paterno + " " + data.apellido_materno);
                   
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
