var periodo;
$(document).ready(function () {
     var encabezados;
     var valores;
     var datos;
     CargaDatos("1");
     periodo = $("#hidPeriodo").val();
     CargaComboPeriodo(4, 0, periodo);
});
$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../../ExtraEscolares/Inscritos/', { psPeriodo: periodo });
     evento.preventDefault();
});
$("#cboEstadistica").change(function () {
     var texto = $("#cboEstadistica option:selected").text();
     var valor = $("#cboEstadistica option:selected").val();
     CargaDatos(valor);
});
function CargaDatos(psID) {
     $.ajax({
          url: '../../../../Estadisticas/RegresaEstadisticasActividadesExtraescolares',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psID: psID },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No Existen Valores Registrados</strong>");
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
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Carreras</strong>");
          })
          .always(function () {
               setTimeout(function () {
                    if ($(".alert").hasClass('alert-danger')) {
                         $(".alert").removeClass('alert-danger');
                    } else if ($(".alert").hasClass('alert-warning')) {
                         $(".alert").removeClass('alert-warning');
                    } else if ($(".alert").hasClass('alert-info')) {
                         $(".alert").removeClass('alert-info');
                    } else {
                         $(".alert").removeClass('alert-success');
                    }
                    $(".alert strong").remove();
                    $(".alert").hide();
               }, 2000);
          });
}
