
$(document).ready(function () {
     //Select cboEstados
     $.ajax({
          sync:false,
          url: '../../../Generales/RegresaComboEstadosMexico',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "No hay registro de entidades federativas en la base");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    var seleccion = "selected";
                    $("#cboEstados").empty();
                    for (; resultado.data[i];) {
                         $("#cboEstados").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
                    var vali = $("#hidEntidad").val();
                   if ($.trim(vali).length != 0)
                         $('#cboEstados option[value=' + vali + ']').attr('selected', 'selected');
                    else
                         $('#cboEstados option[value=9]').attr('selected', 'selected');
               }
          })
          .fail(function (data) {
               MensajesToastr("error", "Solicitud Procesada", "Error al cargar lista de Estados de México");
          });

    $("input").keyup(function () {      
       if(!$(this).hasClass("correo"))
          $(this).val($(this).val().toUpperCase());
     });
});
//Telefonos
$("input[type=tel]").keyup(function () {

     if ($("#txtTelefono").val().length >= 8) {
          $("#spTelefono").removeClass("glyphicon-remove");
          $("#spTelefono").addClass("glyphicon-ok");
          $("#spTelefono").css("color", "#00A41E");
     } else {
          $("#spTelefono").removeClass("glyphicon-ok");
          $("#spTelefono").addClass("glyphicon-remove");
          $("#spTelefono").css("color", "#FF0004");
     }

     if ($("#txtCelular").val().length >= 8) {
          $("#spCelular").removeClass("glyphicon-remove");
          $("#spCelular").addClass("glyphicon-ok");
          $("#spCelular").css("color", "#00A41E");
     } else {
          $("#spCelular").removeClass("glyphicon-ok");
          $("#spCelular").addClass("glyphicon-remove");
          $("#spCelular").css("color", "#FF0004");
     }
});
//Genera CURP
$("#txtNombre, #txtPaterno, #txtMaterno, #txtFechaNacimiento").blur(function () {
     procesaCURP();
});
$("#cboEstados").change(function () {
     procesaCURP();
});
$("#dtpFecha").change(function () {
     procesaCURP();
});
$('input[type=radio][name=rbtSexo]').change(function () {
     procesaCURP();
});
$("#txtNSS").keyup(function () {
     var valor = $("#txtNSS").val();
     var ultimo = valor.substring(valor.length - 1, valor.length);
     if (isNaN(ultimo)) {
          valor = valor.substring(0, valor.length - 1);
          $("#txtNSS").val(valor);
          return;
     }
});
function InvalidMsg(textbox) {
     if (textbox.value == '') {
          textbox.setCustomValidity('Campo Requerido');
     } else {
          if (ValidaCurp(textbox.value))
               textbox.setCustomValidity('');
          else
               textbox.setCustomValidity('Ingrese un CURP valido');
     }
     return true;
}
function procesaCURP() {
     var curp = $("#txtCURP").val();
     var nombre = $("#txtNombre").val();
     var paterno = $("#txtPaterno").val();
     var materno = $("#txtMaterno").val();
     var fecha = $("#txtFechaNacimiento").val();
     var sexo = $('input[name=rbtSexo]:checked').val();
     var estado = $("#cboEstados").val();

     if (fecha.trim() == "")
          fecha = "0000/00/00";

     curp = fnCalculaCURP(nombre, paterno, materno, fecha, sexo, estado);
     $("#txtCURP").val(curp);

}