var control
$(document).ready(function () {
     //LimpiaControles();
});

$("#frmValidaControl").submit(function (event) {
     RegresaDatosSolicitante();
     event.preventDefault();
})

$("#frmRegistroEstudianteIngles").submit(function (event) {
     event.preventDefault();
     var grupo = $('input[name=rbtCurso]:checked').val();
     var control = $("#hidControl").val();
     if (grupo.trim() == "")
     {
          MensajesToastr("info", "Solicitud Procesada", "Debe seleccionar algún grupo");
          return
     }
     $.ajax({
          url: '../../../../CentroLenguasExt/GuardaGrupoEstudiante',
          type: 'POST',
          dataType: 'json',
          data: { psControl:control, psGrupo:grupo },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               }
               else {
                    MensajesToastr("info", "Solicitud Procesada", "Grupo Registrado");
                    $('#BodyPrincipal').load('../../../../' + data.Mensaje);
                    LimpiaControles();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });

})
function RegresaDatosSolicitante() {
     var control = $("#txtNoControl").val();
     $('#BodyPrincipal').load('../../../../Estudiante/DatosCursosIngles/',
            { psNoControl: control });
}
function LimpiaControles() {
     if ($("#divUsuario").length) {
          $("#divUsuario").show();
     }
     $("#divRegistro").hide();
}
$("#btnDescargarHorario").on('click', function (evento) {
     evento.preventDefault();

     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $('#divPDF').load('../../../../Reportes/HorarioIngles',  displaySection);

});

function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}
