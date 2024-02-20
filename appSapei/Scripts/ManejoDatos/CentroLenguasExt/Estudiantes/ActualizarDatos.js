$(document).ready(function () {
     $('#dtpFecha').datetimepicker({
          viewMode: 'years',
          format: 'YYYY/MM/DD',
          locale: 'es',
          defaultDate: new Date()
     });

});

$("#btnBuscar").click(function Nuevo(evento) {
     RegresaEstudianteDatos($("#txtNoControl").val());
     evento.preventDefault();
})
$("#btnRegresar").click(function Nuevo(evento) {    
     LimpiaControles();
     evento.preventDefault();
})
$("#frmDatosEstudiante").submit(function (event) {
     event.preventDefault();
     var noControl = $("#txtNoControl").val();
     var nombre = $("#txtNombre").val();
     var paterno = $("#txtPaterno").val();
     var materno = $("#txtMaterno").val();
     var fecha_nacimiento = $("#txtFechaNacimiento").val();
     var entidad_nacimiento = $("#cboEstados").val();
     var curp = $("#txtCURP").val();
     var estado_civil = $("#cboEstadoCivil").val();
     var nss = $("#txtNSS").val();
     var telefono = $("#txtTelefono").val();
     var celular = $("#txtCelular").val();
     var correo = $("#txtCorreo").val();
     var sexo = $('input[name=rbtSexo]:checked').val();
     //Domicilio
     var calle = $("#txtCalle").val();
     var numero = $("#txtNoDomicilio").val();
     var idCP = $("#cboColonia").val();
     var cp = $("#txtCodPostal").val();
     $.ajax({
          url: '../../../../Estudiante/ActualizaDatos',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: noControl, psNombre: nombre, psPaterno: paterno, psMaterno: materno, psFechaNacimiento: fecha_nacimiento, piEntidadNacimiento: entidad_nacimiento, psSexo:sexo, psCURP: curp, psEstadoCivil: estado_civil, psNSS: nss, psTelefono: telefono, psCelular: celular, psCorreo: correo, psCalle: calle, psNumero: numero, piIdCP: idCP },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Actualizar");
               }
               else {
                    MensajesToastr("success", "Solicitud Procesada", "Actualización Correcta");
                    if ($("#divNoControl").length) {
                         LimpiaControles();
                    }                   
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     
})
function RegresaEstudianteDatos(noControl) {
     if (!esFormatoValidoAlert())
          return
     Cargar(noControl);
}
function Cargar(noControl)
{
     $.ajax({
          url: '../../../../Estudiante/RegresaEstudianteDatosCompletos',
          type: 'POST',
          dataType: 'json',
          data: { psNoControl: noControl },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "No se encontro el estudiante con no. de control: " + noControl + "");
               }
               else {
                    var resultado = JSON.parse(data);
                    //$("#divNoControl").hide();
                    $("#divDatos").show();
                    $("#divPersonales").show();
                    $("#txtNombre").val(resultado.data[0].nombre);
                    $("#txtPaterno").val(resultado.data[0].paterno);
                    $("#txtMaterno").val(resultado.data[0].materno);
                    $("#txtFechaNacimiento").val(resultado.data[0].fecha_nacimiento);
                    $("#cboEstados").val(resultado.data[0].lugar_nacimiento);
                    if (resultado.data[0].sexo === 'F')
                         $("#rdbMujer").attr('checked', 'checked');
                    else
                         $("#rdbHombre").attr('checked', 'checked');
                    $("#txtCURP").val(resultado.data[0].curp);
                    $("#cboEstadoCivil").val(resultado.data[0].estado_civil);
                    $("#txtNSS").val(resultado.data[0].nss);

                    $("#txtTelefono").val(resultado.data[0].telefono);
                    $("#txtCelular").val(resultado.data[0].celular);
                    $("#txtCorreo").val(resultado.data[0].correo);

                    //Domicilio
                    $("#txtCalle").val(resultado.data[0].calle);
                    $("#txtNoDomicilio").val(resultado.data[0].numero);
                    $("#txtCodPostal").val(resultado.data[0].cp);
                    autocompleta("Estudiante", resultado.data[0].cp);
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function LimpiaControles()
{
     $("#divNoControl").show();
     $("#divDatos").hide();
     $("#divPersonales").hide();
     $("#txtNoControl").val("");

     $("#txtNombre").val("");
     $("#txtPaterno").val("");
     $("#txtMaterno").val("");
     $("#txtFechaNacimiento").val("");
     $("#cboEstados").val("");
     $("#txtNSS").val("");
     $("#txtCURP").val("");
     $("#txtTelefono").val("");
     $("#txtCelular").val("");
     $("#txtCorreo").val("");

     //Domicilio
     $("#txtCalle").val("");
     $("#txtNoDomicilio").val("");
     $("#txtCodPostal").val("");
     
}
