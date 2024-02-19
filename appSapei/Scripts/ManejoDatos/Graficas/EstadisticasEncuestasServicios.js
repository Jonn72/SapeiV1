$(document).ready(function () {
     $("#divFiltro1").show();
     $("#divFiltro2").show();
     $("#divFiltro3").hide();
});
$("#cboFiltro1").change(function () {
     Seleccionar();
});

$("#cboFiltro2").change(function () {
     Seleccionar();
});

$("#cboFiltro3").change(function () {
     Seleccionar();
});

function Seleccionar()
{
     var carrera = $("#cboFiltro1 option:selected").val();
     var encuesta = $("#cboFiltro2 option:selected").val();
     var pregunta = $("#cboFiltro3 option:selected").val();

     $("#divFiltro3").hide();

     if (encuesta == "0" && carrera == "1" && pregunta  == "0") {
          CargaDatos(carrera);        
          return;
     }
     if (encuesta == "0" && carrera != "1" && pregunta == "0") {
          CargaDatos("2-" + carrera);
          return;
     }
     $("#divFiltro3").show();

     if (encuesta != "0" && carrera == "1" && pregunta == "0") {
          CargaDatos("3-" +  encuesta);
          return;
     }    
     
     if (encuesta != "0" && carrera != "1" && pregunta == "0") {
          CargaDatos("4-" + carrera + "-" + encuesta);
          return;
     }

     if (encuesta != "0" && carrera == "1" && pregunta != "0") {
          CargaDatos("5-" + encuesta + "-" + pregunta);
          return;
     }

     CargaDatos("6-" + carrera + "-" + encuesta + "-" + pregunta);
}