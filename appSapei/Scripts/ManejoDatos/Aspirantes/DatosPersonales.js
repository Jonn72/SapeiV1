$(document).ready(function () {
     // Toolbar extra buttons
     CargaControles()
     DesactivaControles();
});

function CargaControles()
{
     val = $("#hidSexo").val();
     if (val === 'M')
          $("#rdbMujer").attr('checked', 'checked');
     else
          $("#rdbHombre").attr('checked', 'checked');

     val = $("#hidEstadoCivil").val();
     if (val.trim().length != 0)
          $('#cboEstadoCivil option[value=' + val + ']').attr('selected', 'selected');

     autocompleta("Aspirante", val);

}
$("#frmDatosAspirante").submit(function (event) {
     var nombre = $("#txtNombre").val();
     var paterno = $("#txtPaterno").val();
     var materno = $("#txtMaterno").val();
     var curp = $("#txtCURP").val();
     var sexo = $('input[name=rbtSexo]:checked').val();
     var correo = $("#txtCorreo").val();
     var telefono = $("#txtTelefono").val();
     var celular = $("#txtCelular").val();
     var fecha = $("#txtFechaNacimiento").val();
     var domicilio = $("#txtCalle").val();
     var numero = $("#txtNoDomicilio").val();
     var cp = $("#txtCodPostal").val();
     var id_cp = $("#cboColonia").val();
     var calle = $("#txtCalle").val();
     var numero = $("#txtNoDomicilio").val();
     var entidad = $("#cboEstados").val();
     var civil = $("#cboEstadoCivil").val();
     var nss = $("#txtNSS").val();

     if (!ValidaCampos(nombre, paterno, materno, curp, correo, telefono, celular, fecha, domicilio, numero, cp)) {
          MensajesToastr("info", "Solicitud Procesada", "Complete todos los campos");
     }
     else {
          $.ajax({
               asyn: false,
               url: '../../../Aspirante/ModificarDatosPersonales',
               type: 'POST',
               dataType: 'json',
               data: { psNombre: nombre, psPaterno: paterno, psMaterno: materno, psCurp: curp, psSexo: sexo, psEntidad:entidad, psEstadoCivil:civil, psCorreo: correo, psTelefono: telefono, psCelular: celular, psFecha: fecha, psDomicilio: domicilio, psNumero: numero, psId_cp: id_cp, psCalle: calle, psNumero: numero, psNSS:nss },
          })
          .done(function (data) {

               if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                    $("#btnSiguiente").prop("disabled", false);
               }
               else {
                    $('#BodyPrincipal').load('../../../../Aspirante/DatosSolicitud');
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     }
     event.preventDefault();
})

function ValidaCampos(nombre, paterno, materno, curp, correo, telefono, celular, fecha, domicilio, numero, cp) {

     if (ex_nombres.test(nombre))
          $("#txtNombre").get(0).setCustomValidity('');
     else {
          $("#txtNombre").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(paterno))
          $("#txtPaterno").get(0).setCustomValidity('');
     else {
          $("#txtPaterno").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(materno))
          $("#txtMaterno").get(0).setCustomValidity('');
     else {
          $("#txtMaterno").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_correo.test(correo))
          $("#txtCorreo").get(0).setCustomValidity('');
     else {
          $("#txtCorreo").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_telefono.test(telefono))
          $("#txtTelefono").get(0).setCustomValidity('');
     else {
          $("#txtTelefono").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_telefono.test(celular))
          $("#txtCelular").get(0).setCustomValidity('');
     else {
          $("#txtCelular").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (fecha.length == 0) {
          $("#txtFechaNacimiento").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (domicilio.length == 0) {
          $("#txtCalle").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (numero.length == 0) {
          $("#txtNoDomicilio").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (cp.length == 0) {
          $("#txtCodPostal").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     return true;
}

$('#dtpFecha, #dtpFechaElaboracion').datetimepicker({
     viewMode: 'years',
     format: 'YYYY/MM/DD',
     locale: 'es',
     defaultDate: '0000-01-01'
});
function DesactivaControles()
{
     $('#txtNombre').prop('readonly', true);
     $('#txtPaterno').prop('readonly', true);
     $('#txtMaterno').prop('readonly', true);
     $('#txtCorreo').prop('readonly', true);
}