$(document).ready(function () {
     $("#divTabla").hide();
     $("#divBotones").hide();
     $("#divProcesar").hide();
     init_dataTables();
     CargaComboPeriodo(5, 1);
});
$("#btnBuscar").click(function cargar(evento) {
     CargaTabla();
     event.preventDefault();
})
$("#btnProcesar").click(function procesar(evento) {
     $("#divProcesar").show();
     $("#divTabla").hide();
     $("#divBotones").hide();
     event.preventDefault();
})
$("#btnIniciar").click(function procesar(evento) {
     Procesar();
     event.preventDefault();
})
$("#btnAtras").click(function Atras(evento) {
     if ($('#divTabla').is(':hidden') && $('#divCombo').is(':hidden')) {
          $('#divTabla').show();
          $("#divBotones").show();
          $('#divProcesar').hide();
     }
     else
     {
          $('#divTabla').hide();
          $("#divBotones").hide();
          $('#divCombo').show();
     }
     event.preventDefault();
})
function CargaTabla()
{
     var valor = $("#cboPeriodo").val();
     $('#divTabla').load('../../../../ServiciosEscolares/CargaTablaAspirantesEstudiantes',
            { psPeriodo: valor });
     $("#divCombo").hide();
     $("#divTabla").show();
     $("#divBotones").show();
}
function Procesar() {
     var periodo = $("#cboPeriodo").val();
     $.ajax({
          url: '../../../ServiciosEscolares/ProcesaAspirantes2Estudiantes',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo: periodo },
     })
     .done(function (data) {
          if (typeof (data.Success) !== "undefined") {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          $("#divCombo").show();
          $("#divProcesar").hide();
     });
}