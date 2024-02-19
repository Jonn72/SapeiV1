$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#NoControltxt").val(data[1]);
        $("#NoAsesoriatxt").val(data[2]);
        $('#Valida_Asesoria').modal('show');
    });
    $("#frmValidaAsesoria").submit(function (evento) {
        var noControl = $("#NoControltxt").val();
        var noAsesoria = $("#NoAsesoriatxt").val();
        $.ajax({
            url: '../../../Academicos/ValidaAsesoriaJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psNoControl: noControl, psNoAsesoria: noAsesoria },
        })
                  .done(function (data) {
                      if (typeof (data.Success) !== "undefined" && !data.Success) {
                          MensajesToastr("error", "Solicitud Procesada", "Usted no tiene permiso sobre estudiantes de la carrera");
                      }
                      else {
                          MensajesToastr("success", "Solicitud Procesada", "Validación correcta");
                          $('#Valida_Asesoria').modal('hide');
                          $('body').removeClass('modal-open');
                          $('.modal-backdrop').remove();
                          $('#BodyPrincipal').load('../../../../Academicos/AsesoriasRP');
                      }
                  })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });
});