$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#Controltxt").val(data[1]);
        $('#ModalAceptacion').modal('show');//abrir modal            
    });
});

//FUNCIÓN PARA ASIGNAR EL MISMO RFC
$("#txtControl").keyup(function () {
    var value = $(this).val();
    $("#Controltxt").val(value);
});

//FUNCION PARA VALIDAR CARTA DE ACEPTTACION
$("#btnGuardar").click(function () {
    var control = $("#Controltxt").val();
    $.ajax({
        url: '../../../Vinculacion/ValidarCartaAceptacionSS',
        type: 'POST',
        dataType: 'json',
        data: {
            psControl: control 
        },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {

                  MensajesToastr("error ", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Carta de Aceptación registrada");
                  $('#ModalAceptacion').modal('hide'); // cerrar
                  $('body').removeClass('modal-open');
                  $('.modal-backdrop').remove();
                  $('#BodyPrincipal').load('../../../../Vinculacion/CartaAceptacionSS');
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
});