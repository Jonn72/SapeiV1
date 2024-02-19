var periodo;
var url;
var url_estadistica;
$(document).ready(function () {
     var encabezados;
     var valores;
     var datos;
     periodo = $("#hidPeriodo").val();
     CargaDatos("1");
     CargaComboPeriodo(4, 0, periodo);
});
$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     var lsUrl = '../../../../' + $("#hidURL").val() + '/';
     $('#BodyPrincipal').load(lsUrl, { psPeriodo: periodo });
     evento.preventDefault();
});
$("#cboEstadistica").change(function (evento) {
     var texto = $("#cboEstadistica option:selected").text();
    var valor = $("#cboEstadistica option:selected").val();
    CargaDatos(valor);
    evento.preventDefault();
});
function CargaDatos(psID) {
     var lsurl = '../../../../Estadisticas/' + $("#hidURL_estadistica").val();
     $.ajax({
          url: lsurl,
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psID: psID },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("success", "Solicitud Procesada", "No hay datos por cargar");
                    OcultaGraficas();
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    encabezados = [];
                    valores = [];
                    datos = resultado.data;
                    for (; resultado.data[i];) {
                         encabezados[i] = resultado.data[i].name;
                         valores[i] = resultado.data[i].value;
                         i++;
                    }
                    CargaDatosEnGraficas();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
