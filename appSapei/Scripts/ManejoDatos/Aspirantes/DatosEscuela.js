$("#frmDatosEscuela").submit(function (event) {
     var egreso = $("#txtAnioEgreso").val();
     var promedio = $("#txtPromedio").val();
     var idescuela = $("#cboTipoEscuelaProcedencia").val();
     if (!ValidaCampos(egreso, promedio)) {
          MensajesToastr("info", "Solicitud Procesada", "Complete todos los campos");
     }
     else {
          $.ajax({
               asyn: false,
               url: '../../../Aspirante/ModificarDatosEscuela',
               type: 'POST',
               dataType: 'json',
               data: { psIdEscuela: idescuela, psEgreso: egreso, psPromedio: promedio },
          })
          .done(function (data) {

               if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                    $("#btnSiguiente").prop("disabled", false);
               }
               else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../Aspirante/Ficha');
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     }
     event.preventDefault();
})

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

$('#dtpFechaElaboracion').datetimepicker({
     viewMode: 'years',
     format: 'YYYY/MM/DD',
     locale: 'es',
     defaultDate: new Date()
});

$("#btnAnterior").click(function Atras(evento) {
     event.preventDefault();
     $('#BodyPrincipal').load('../../../../Aspirante/DatosSolicitud');
})
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