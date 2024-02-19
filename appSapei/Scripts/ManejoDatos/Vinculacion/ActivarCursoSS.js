$("#frmAperturaCurso").submit(function (evento) {
    var titulo = $("#txtTitulo").val();
    var url = $("#txtUrl").val();
    $.ajax({
        url: '../../../Vinculacion/ActivarCursoJsonSS',
        type: 'POST',
        dataType: 'json',
        data: { psTitulo:titulo, psUrl:url },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {

                  MensajesToastr("error ", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Curso registrado");
                  //ACTUALIZA BOBY PRINCIPAL
                  $('#BodyPrincipal').load('../../../../Vinculacion/ActivarCursoSS');//agregue
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
    evento.preventDefault();
})