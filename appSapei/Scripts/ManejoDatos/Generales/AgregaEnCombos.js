$(document).ready(function () {
     filtraTabla($("#cboPadre").val(),2);
});
$("#frmAgregaEnCombos").submit(function (event) {    
     var descripcion = $("#txtDescripcion").val();
     var valor = $("#hidValor").val();
     var combo = $("#hidCombo").val();
     var ruta = $("#hidRuta").val();
     var combo_padre = $("#hidComboPadre").val();
     var valor_padre;
     var descripcion_padre;
     valor_padre = null;
     if (combo_padre.trim().length > 0) {
          valor_padre = $("#cboPadre").val().trim();
     }

     $(".alert").show();
     $.ajax({
          url: '../../../Generales/GuardaSisCombos',
          type: 'POST',
          dataType: 'json',
          data: {psValor: valor, psDescripcion: descripcion, psCombo: combo, psValorPadre: valor_padre},
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al Guardar</strong>");
               $("#btnSiguiente").prop("disabled", false);
          }
          else {
               $(".alert").addClass('alert-info');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp;Registro Correcto</strong>");
               $('#BodyPrincipal').load('../../' + ruta);
          }
     })
     .fail(function (data) {
          $(".alert").addClass('alert-danger');
          $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No hay conexion con el servidor...</strong>");
     })
     .always(function () {
          TemporizadorAlert(2000);
     });
     event.preventDefault();
})
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();
     var tipo;
     var descripcion;
     var capacidad;
     var horario
     descripcion = data[1];
     $("#hidValor").val(data[0]);
     $("#txtDescripcion").val(descripcion);
     if ($("#hidComboPadre").val().length > 0)
     {
          $("#cboPadre").val(data[2]);
          $("#hidValorPadre").val(data[2]);
     }
});
$("#btnNuevo").click(function Nuevo(evento) {
     LimpiaControles();
     evento.preventDefault();
})
function LimpiaControles() {
     $("#hidValor").val("");
     $("#hidValorPadre").val("");
     $("#txtDescripcion").val("");
}
$("#cboPadre").change(function () {
     filtraTabla($("#cboPadre").val(),2);
});