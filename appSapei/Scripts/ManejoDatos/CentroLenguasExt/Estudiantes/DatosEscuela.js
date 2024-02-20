
$.ajax({
     url: '../../../Generales/RegresaComboTipoEscuelaProcedencia',
     type: 'POST',
     dataType: 'json',
     data: {},
})
     .done(function (data) {
          if (typeof (data.Success) !== "undefined") {
               MensajesToastr("info", "Solicitud Procesada", "No hay carreras registradas");
          }
          else {
               var resultado = JSON.parse(data);
               var i = 0;
               $("#cboTipoEscuelaProcedencia").empty();
               for (; resultado.data[i];) {
                    $("#cboTipoEscuelaProcedencia").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                    i++;
               }
               var val = $("#idEscuela").val();
               if(val.trim().length!=0)
                    $('#cboTipoEscuelaProcedencia option[value=' + val + ']').attr('selected', 'selected');
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });

$("#frmDatosEscuela").submit(function (event) {
     var noControl = $("#txtNoControl").val();
     var egreso = $("#txtAnioEgreso").val();
     var promedio = $("#txtPromedio").val();
     var idescuela = $("#cboTipoEscuelaProcedencia").val();
     if (!ValidaCampos(egreso, promedio)) {
          MensajesToastr("info", "Solicitud Procesada", "Complete todos los campos");
     }
     else {
          $.ajax({
               asyn: false,
               url: '../../../Estudiante/ModificarDatosEscuela',
               type: 'POST',
               dataType: 'json',
               data: { psControl: noControl, psIdEscuela: idescuela, psEgreso: egreso, psPromedio: promedio },
          })
          .done(function (data) {

               if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                    $("#btnSiguiente").prop("disabled", false);
               }
               else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    if ($("#tipo_usuario").val() !== 'ALU')
                         LimpiaControles();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     }
     event.preventDefault();
})

$("#btnBuscar").click(function Nuevo(evento) {
     RegresaEstudianteDatos($("#txtNoControl").val());
     evento.preventDefault();
})
$("#btnAtras").click(function Nuevo(evento) {
     LimpiaControles();
     evento.preventDefault();
})
function RegresaEstudianteDatos(noControl) {
     if (!esFormatoValidoAlert())
          return
     Cargar(noControl);
}
function Cargar(noControl) {
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
                    $("#divNoControl").hide();
                    $("#divDatos").show();
                    $("#txtNombreCompleto").val(resultado.data[0].paterno + " " + resultado.data[0].materno + " " +resultado.data[0].nombre);
                    $("#txtAnioEgreso").val(resultado.data[0].anio_egreso);
                    $("#txtPromedio").val(resultado.data[0].promedio);
                    $("#cboTipoEscuelaProcedencia").val(resultado.data[0].id_escuela);                 
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function ValidaCampos(egreso, promedio) {

     if (egreso.length == 0) {
          $("#txtAnioEgreso").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (promedio.length == 0) {
          $("#txtPromedio").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     return true;
}
function LimpiaControles()
{
     $("#divNoControl").show();
     $("#divDatos").hide();
     $("#txtAnioEgreso").val("");
     $("#txtPromedio").val("");
     $("#cboTipoEscuelaProcedencia").val(1);
     $("#txtNombreCompleto").val("");
     $("#txtNoControl").val("");
}