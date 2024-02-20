$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#Controltxt").val(data[1]);
        $('#ModalReporteBimestral4').modal('show');//abrir modal    

    });
});

//FUNCIÓN PARA ASIGNAR EL MISMO NUMERO DE CONTROL
$("#txtControl").keyup(function () {
    var value = $(this).val();
    $("#Controltxt").val(value);
});

//FUNCION PARA VALIDAR LOS REPORTES
$("#frmReporteBimestral4").submit(function (evento) {
    var calificacion = $("#txtCalificacion").val();
    var control = $("#Controltxt").val();
    $.ajax({
        url: '../../../Vinculacion/ValidarBimestre4SS',
        type: 'POST',
        dataType: 'json',
        data: {
            psControl: control, psCalificacion: calificacion
        },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {

                  MensajesToastr("error ", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Reportes validados");
                  $('#ModalReporteBimestral4').modal('hide'); // cerrar
                  $('body').removeClass('modal-open');
                  $('.modal-backdrop').remove();
                  $('#BodyPrincipal').load('../../../../Vinculacion/Bimestre4SS');
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
    evento.preventDefault();
});