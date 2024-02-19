$(document).ready(function () {
     var datosObtenidos = null;
     var encabezados;
     var valores;
     var datos;
     var totalConsultas;
     var vectorIP;
     var vectorUbicacion;
     var vectorContadorUbicacion;
     var vectorContadorIP;
     var vectorDatosIP;
     var vectorDatosUbicacion;
     var vectorFecha;
     var vectorContadorFecha;
     var vectorDatosFecha;
     var vectorTipo;
     var vectorContadorTipo;
     var vectorDatosTipo;

     var vectorCategoria;
     var vectorContadorCategoria;
     var vectorDatosCategoria;

     ObtieneDatos();

});

$("#cboEstadistica").change(function () {
     var texto = $("#cboEstadistica option:selected").text();
     var valor = $("#cboEstadistica option:selected").val();
     if (datosObtenidos == null) {
          return;
     }
     CargaGrafica(valor);
});
function ObtieneDatos() {
     $.ajax({
          url: 'http://movil.anfei.mx/externos/consultaVisitas.php',
          type: 'GET',
          crossDomain: true,
          dataType: 'jsonp',
          success: function (data) {
               datosObtenidos = data;
               CargaDatos();
          },
          error: function () {
               console.log('Error con servidor Gnosis!');
          }
     });
}
function CargaDatos() {
     var i = 0;
     encabezados = [];
     valores = [];
     vectorIP = [];
     vectorUbicacion = [];
     vectorFecha = [];
     vectorTipo = [];
     vectorContadorIP = [];
     vectorContadorFecha = [];
     vectorContadorTipo = [];
     vectorDatosIP = [];
     vectorDatosUbicacion = [];
     vectorDatosFecha = [];
     vectorDatosTipo = [];
     GuardaDatosIP(datosObtenidos.anfei[1].ip);
     GuardaDatosUbicacion(datosObtenidos.anfei[0].ubicacion);
     GuardaDatosFechaConsulta(datosObtenidos.anfei[2].fecha);
     GuardaDatosTipoConsulta(datosObtenidos.anfei[3].tipo);
     GuardaDatosCategoriaPublicacion(datosObtenidos.anfei[4].categoria);
     CargaGrafica("1");
}
function GuardaDatosIP(datos) {
     var i = 0;
     vectorIP = [];
     vectorContadorIP = [];
     vectorDatosIP = [];
     var valor;
     var ip;
     var totalConsultas = 0;
     for (; datos[i];) {
          valor = datos[i].valor;
          ip = datos[i].descripcion + " (" + valor + ")";
          vectorIP.push(ip);
          vectorContadorIP.push(valor);
          vectorDatosIP.push({ value: valor, name: ip });
          totalConsultas = totalConsultas + parseInt(valor);
          i = i + 1;
     }
     $('#lblTotalConsultas').text(totalConsultas);
}
function GuardaDatosUbicacion(datos) {
     var i = 0;
     vectorUbicacion = [];
     vectorContadorUbicacion = [];
     vectorDatosUbicacion = [];
     var valor;
     var ubicacion;
     for (; datos[i];) {
          valor = datos[i].valor;
          ubicacion = datos[i].descripcion + " (" + valor + ")";
          vectorContadorUbicacion.push(valor);
          vectorDatosUbicacion.push({ value: valor, name: ubicacion });
          i = i + 1;
     }
}
function GuardaDatosFechaConsulta(datos) {
     var i = 0;
     vectorFecha = [];
     vectorContadorFecha = [];
     vectorDatosFecha = [];
     var valor;
     var nombre;
     for (; datos[i];) {
          valor = datos[i].valor;
          nombre = datos[i].descripcion + " (" + valor + ")";
          vectorFecha.push(nombre);
          vectorContadorFecha.push(valor);
          vectorDatosFecha.push({ value: valor, name: nombre });
          i = i + 1;
     }
}
function GuardaDatosTipoConsulta(datos) {
     var i = 0;
     vectorTipo = [];
     vectorContadorTipo = [];
     vectorDatosTipo = [];
     var valor;
     var nombre;
     for (; datos[i];) {
          valor = datos[i].valor;
          nombre = datos[i].descripcion + " (" + valor + ")";
          vectorTipo.push(nombre);
          vectorContadorTipo.push(valor);
          vectorDatosTipo.push({ value: valor, name: nombre });
          i = i + 1;
     }
}
function GuardaDatosCategoriaPublicacion(datos) {
     var i = 0;
     vectorCategoria = [];
     vectorContadorCategoria = [];
     vectorDatosCategoria = [];
     var valor;
     var nombre;
     var totalPublicaciones = 0;
     for (; datos[i];) {
          valor = datos[i].valor;
          nombre = datos[i].descripcion + " (" + valor + ")";
          vectorCategoria.push(nombre);
          vectorContadorCategoria.push(valor);
          vectorDatosCategoria.push({ value: valor, name: nombre });
          i = i + 1;
          totalPublicaciones = totalPublicaciones + parseInt(valor);
     }
     $('#lblTotalPublicaciones').text(totalPublicaciones);
}
function CargaGrafica(id) {
     switch (id) {
          case "1":
               encabezados = vectorIP;
               datos = vectorDatosIP;
               valores = vectorContadorIP;
               break;
          case "2":
               encabezados = vectorUbicacion;
               datos = vectorDatosUbicacion;
               valores = vectorContadorIP;
               break;
          case "3":
               encabezados = vectorTipo;
               datos = vectorDatosTipo;
               valores = vectorContadorTipo;
               break;
          case "4":
               encabezados = vectorFecha;
               datos = vectorDatosFecha;
               valores = vectorContadorFecha;
               break;
          case "5":
               encabezados = vectorCategoria;
               datos = vectorDatosCategoria;
               valores = vectorContadorCategoria;
               break;
     }
     CargaDatosEnGraficas();
}

Array.prototype.max = function () {
     return Math.max.apply(null, this);
};