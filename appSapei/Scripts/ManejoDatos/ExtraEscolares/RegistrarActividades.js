var semana;
var activaEventos;
var actualizar;
$(document).ready(function () {
     CargaComboAulas();
     activaEventos = false;
    actualizar = false;
    PermiteTraslape(true);
});

$("#cboAulas").change(function (evento) {
     $('#loadingmessage').show();
     $('#divHorario').hide();
     setTimeout(function () {
          LimpiaEventos();
          CargaEventos($("#cboAulas").val());
          $("#loadingmessage").hide("slow");
          $("#divHorario").show("slow");
     }, 100);

     evento.preventDefault();
});
$("#txtGrupo").blur(function () {
     $("#txtGrupo").val($("#txtGrupo").val().toUpperCase())
     ValidatxtGrupo();
});
function CargaComboAulas() {
     $("#divHorario").hide("slow");
     $('#loadingmessage').show("slow");
     semana = [];
     $.ajax({
          url: '../../../Generales/RegresaAulasConHorarios',
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
                    $("#cboAulas").empty();
                    LimpiaEventos();
                    var valor = "";
                    var evento = "";
                    var j = -1;
                    semana = [];
                    for (; resultado.data[i];) {
                         if (valor != resultado.data[i].aula.trim()) {
                              j++;
                              valor = resultado.data[i].aula.trim();
                              $("#cboAulas").append('<option value=' + j + '>' + valor + '</option>');
                              semana[j] = "";
                         }
                         semana[j] = semana[j] + "|" + resultado.data[i].evento;
                         i++;
                    }
                    CargaEventos(0);
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          }).always(function () {
               $("#divHorario").show("slow");
               $("#loadingmessage").hide("slow");
          });
}
function CargaEventos(valor) {
     var i = 0;
     var horario = semana[valor].split("|");
     var evento;

     while (horario[i] == "") {
          i++;
     }

     for (; horario[i];) {
          evento = horario[i].split(",");
          AgregaEvento(evento[1], evento[2], evento[0], evento[3],"X");
          i++;
     }
     if (activaEventos)
          ActivaEdicionEventos($("#txtGrupo").val());
}
$("#frmExtraAct").submit(function (event) {
     event.preventDefault();
     var id = $("#hidId").val();
     var tipo = $("#cboTipoActividad").val();
     var aula = $("#cboAulas option:selected").text();
     var capacidad = $("#txtCapacidad").val();
     var descripcion = $("#txtGrupo").val();
     var entrenador = $("#cboEntrenador").val();
     var horario = RegresaCadenaHorarios(descripcion);

     $.ajax({
          url: '../../../ExtraEscolares/GuardaActividad1',
          type: 'POST',
          dataType: 'json',
          data: { piId: id, psTipo: tipo, psDescripcion: descripcion, piCapacidad: capacidad, piEntrenador: entrenador, psAula: aula, psHorario: horario },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
               RegistraEvento(horario,data.Mensaje);
               LimpiaControles();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
})
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
    var data = $('#datatable-buttons').DataTable().row(this).data();
    var entrenador;
    var tipo;
    var aula;
    tipo = data[1].trim();
    entrenador = data[3].trim();
    aula = data[6].split(" ")[0].trim();
     $("#hidId").val(data[0]);
     $("#txtGrupo").val(data[2]);
     $('#txtGrupo').prop('readonly', true);


     $("#cboEntrenador option").each(function () {
          var opcion = $(this).html().trim();
          if (opcion === entrenador) {
               $(this).attr("selected", "selected");
               return;
          }
     });

     $("#cboTipoActividad option").each(function () {
          if ($(this).html().replace(/\s+/, "") == tipo.replace(/\s+/, "")) {
               $(this).attr("selected", "selected");
               return;
          }
     });

     $("#cboAulas option").each(function () {
          if ($(this).html() == aula) {
               $(this).attr("selected", "selected");
               return;
          }
     });
     $("#txtCapacidad").val(data[4]);
     $("#divEliminar").show("slow");
     $("#btnEliminar").prop('disabled', false);
     activaEventos = true;
     actualizar = true;
     $("#cboAulas").trigger('change');
});
$("#btnNuevo").click(function Nuevo(evento) {
     LimpiaControles();
     evento.preventDefault();
})
$("#btnEliminar").click(function Nuevo(evento) {
     event.preventDefault();
     var id = $("#hidId").val();
     var grupo = $("#txtGrupo").val();
     var aula = $("#cboAulas").val();
     bootbox.confirm("Realmente desea eliminar el grupo " + grupo, function (result) {
          if (result == false)
               return;
          $.ajax({
               url: '../../../ExtraEscolares/EliminaActividad',
               type: 'POST',
               dataType: 'json',
               data: { piId: id },
          })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al eliminar grupo");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
               ActualizaTabla();
               LimpiaEventosAgregados(grupo);
               EliminaEvento(grupo, aula);
               LimpiaControles();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
     });
})
function LimpiaControles() {
     var grupo = $("#txtGrupo").val();
     if (actualizar == false)
          LimpiaEventosAgregados(grupo);
     else
          RegistraEventosAgregados(grupo)
     $("#txtGrupo").val("");
     $("#txtCapacidad").val(0);
     $('#txtGrupo').prop('readonly', false);
     $("#divEliminar").hide("slow");
     $("#btnEliminar").prop('disabled', true);
     $("#hidId").val("0");
     $("#cboEntrenador").val(0)
     $("#cboAulas").val(0)
     activaEventos = false;
     actualizar = false;
}
function ValidatxtGrupo() {
     if (BuscaValor($("#txtGrupo").val(), 2)) {
          MensajesToastr("info", "Validación", "Este grupo ya fue creado, puede verificar en la tabla inferior");
          $("#txtGrupo").val("");
          return;
     }
}
function RegistraEvento(horario, id) {
     var valores = [];
     RegistraEventosAgregados($("#txtGrupo").val());
     valores[0] = id;
     valores[1] = $("#cboTipoActividad :selected").text();
     valores[2] = $("#txtGrupo").val();
     valores[3] = $("#cboEntrenador :selected").text();
     valores[4] = $("#txtCapacidad").val();
     valores[5] = 0;
     //valores[6] = $("#cboAulas").val();
     valores[6] = RegresaCadenaHorariosCorta($("#txtGrupo").val());
     ActualizaEvento();
     ActualizaTabla();
     AgregaFila(valores);
     $("#txtGrupo").val("");
     $("#txtCapacidad").val(0);
     $('#txtGrupo').prop('readonly', false);
}
function ActualizaEvento() {
     var descripcion = $("#txtGrupo").val();
     var aula = $("#cboAulas").val();

     if (actualizar == false) {
          semana[aula] = semana[aula] + RegresaCadenaCompletaHorarios($("#txtGrupo").val(),"X");
          return;
     }
     EliminaEvento(descripcion, aula);
     semana[aula] = semana[aula] + RegresaCadenaCompletaHorarios($("#txtGrupo").val(),"X");
}
function EliminaEvento(descripcion, aula)
{
     var i = 1;
     var horario = semana[aula].split("|");
     var evento = "";
     var a;
     for (; horario[i];) {
          a = horario[i].split(",")[0];
          if (a.trim() != descripcion.trim())
               evento += "|" + horario[i];
          i++;
     }
     semana[aula] = evento;
}
function ActualizaTabla() {
     if (actualizar == false) {
          return;
     }
     var descripcion = $("#txtGrupo").val();
     EliminarFila(descripcion, 2);
}