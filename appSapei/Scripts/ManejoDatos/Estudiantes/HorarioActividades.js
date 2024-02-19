var semana;
$(document).ready(function () {
     CargaHorario();
});

function CargaHorario() {
     $("#divHorario").hide("slow");
     $('#loadingmessage').show("slow");
     semana = [];
     $.ajax({
          url: '../../../Estudiante/RegresaHorario',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existen aulas registradas");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    var valor = "";
                    var evento = "";
                    var j = -1;
                    semana = "";
                    for (; resultado.data[i];) {

                         semana = semana + "|" + resultado.data[i].evento;
                         i++;
                    }
                    CargaEventos();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          }).always(function () {
               $("#divHorario").show("slow");
               $("#loadingmessage").hide("slow");
          });
}
function CargaEventos() {
     var i = 0;
     var horario = semana.split("|");
     var evento;

     while (horario[i] == "") {
          i++;
     }

     for (; horario[i];) {
          evento = horario[i].split(",");
          AgregaEvento(evento[1], evento[2], evento[0], evento[3]);
          i++;
     }

}
