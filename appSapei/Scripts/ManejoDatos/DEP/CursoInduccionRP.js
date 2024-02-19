$("#frmCursoInduccion").submit(function (evento) {
    var titulo = $("#txtTitulo").val();
    var url = $("#txtUrl").val();
    $.ajax({
        url: '../../../DEP/ActivarCursoJsonRP',
        type: 'POST',
        dataType: 'json',
        data: { psTitulo: titulo, psUrl: url },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {
                  MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Curso Registrado");
                  $('body').removeClass('modal-open');
                  $('.modal-backdrop').remove();
                  $('#BodyPrincipal').load('../../../../DEP/CursoInduccionRP');
                  //CambiaValorTabla(fin, 3, 0);
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
    evento.preventDefault();
});