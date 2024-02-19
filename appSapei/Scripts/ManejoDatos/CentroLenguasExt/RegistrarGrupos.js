var semana;
var activaEventos;
var actualizar;
$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     CargaComboPeriodo(5, 1, periodo);
     CargaComboAulas(periodo);
     activaEventos = false;
    actualizar = false;
    $('#cboAulas').select2({ theme: "classic" });
    $('#cboNivelesIngles').select2({ theme: "classic" });
});
$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../../CentroLenguasExt/RegistrarGrupos/' + periodo);
     evento.preventDefault();
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
     ValidaGrupo();
});
function CargaComboAulas(periodo) {
     $("#divHorario").hide("slow");
     $('#loadingmessage').show("slow");
     semana = [];
     $.ajax({
          url: '../../../Generales/RegresaAulasConHorarios',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo },
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
                        if (resultado.data[i].evento.trim().length > 0) {
                            semana[j] = semana[j] + "|" + resultado.data[i].evento;
                        }
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
     var i = 1;
    var horario = semana[valor].split("|");
    var evento;
    for (; horario[i];) {          
        evento = horario[i].split(",");
        AgregaEvento(evento[1], evento[2], evento[0], evento[3]);
        i++;
     }
     if (activaEventos)
          ActivaEdicionEventos($("#txtGrupo").val());
}
$("#frmCLEGrupos").submit(function (event) {
     event.preventDefault();
     var periodo = $("#hidPeriodo").val();
     var grupo = $("#txtGrupo").val();
     var nivel = $("#cboNivelesIngles").val();
     var aula = $("#cboAulas option:selected").text();
     var capacidad = $("#txtCapacidad").val();
     var horario = RegresaCadenaHorarios(grupo);
     if (horario.trim().length == 0) {
          MensajesToastr("info", "Validación", "Debe seleccionar un horario de 6 horas");
          return;
     }
     if (!ValidaHoarario(horario)) {
          MensajesToastr("info", "Validación", "Debe seleccionar un horario de 6 horas");
          return;
     }
     $.ajax({
          url: '../../../CentroLenguasExt/GuardaGrupo',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psNivel: nivel, psGrupo: grupo, piCapacidad: capacidad, psAula: aula, psHorario: horario },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al Guardar Periodo");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se guardo correctamente");
               RegistraEvento(horario);
               LimpiaControles();
          }
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     });
})
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();
     var nivel;
     var aula;
    nivel = data[1].trim();
    aula = data[5].split(" ")[1].trim();
    $("#cboNivelesIngles").val(nivel).change();
     $("#txtGrupo").val(data[2]);
     $('#txtGrupo').prop('readonly', true);
     $("#cboAulas option").each(function () {
          if ($(this).html() == aula) {
               $(this).attr("selected", "selected");
               return;
          }
     });
     $("#txtCapacidad").val(data[3]);
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
     var periodo = $("#hidPeriodo").val();
     var grupo = $("#txtGrupo").val();
     var nivel = $("#cboNivelesIngles").val();
     var aula = $("#cboAulas").val();
     bootbox.confirm("Realmente desea eliminar el grupo " + grupo, function (result) {
          if (result == false)
               return;
          $.ajax({
               url: '../../../CentroLenguasExt/EliminaGrupo',
               type: 'POST',
               dataType: 'json',
               data: { psPeriodo: periodo, psNivel: nivel, psGrupo: grupo },
          })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al eliminar grupo");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Se elimino correctamente");
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
     activaEventos = false;
     actualizar = false;
}
function ValidaHoarario(horario) {
     return ValidaTotalHoras($("#txtGrupo").val(),6)
}
function ValidaGrupo() {
     if (BuscaValor($("#txtGrupo").val(), 2)) {
          MensajesToastr("info", "Validación", "Este grupo ya fue creado, puede verificar en la tabla inferior");
          $("#txtGrupo").val("");
          return;
     }
}
function RegistraEvento(horario) {
     var valores = [];
     RegistraEventosAgregados($("#txtGrupo").val());
     valores[0] = $("#hidPeriodo").val();
     valores[1] = $("#cboNivelesIngles").val();
     valores[2] = $("#txtGrupo").val();
     valores[3] = $("#cboAulas :selected").text();
     valores[4] = $("#txtCapacidad").val();
     valores[5] = 0;
     valores[6] = RegresaCadenaHorariosCorta($("#txtGrupo").val());
     ActualizaEvento();
     ActualizaTabla();
     AgregaFila(valores);
     $("#txtGrupo").val("");
     $("#txtCapacidad").val(0);
     $('#txtGrupo').prop('readonly', false);
}
function ActualizaEvento() {
     var grupo = $("#txtGrupo").val();
     var aula = $("#cboAulas").val();

     if (actualizar == false) {
          semana[aula] = semana[aula] + RegresaCadenaCompletaHorarios($("#txtGrupo").val(),"I");
          return;
     }
     EliminaEvento(grupo, aula);
     semana[aula] = semana[aula] + RegresaCadenaCompletaHorarios($("#txtGrupo").val(),"I");
}
function EliminaEvento(grupo, aula)
{
     var i = 1;
     var horario = semana[aula].split("|");
     var evento = "";
     var a;
     for (; horario[i];) {
          a = horario[i].split(",")[0];
          if (a.trim() != grupo.trim())
               evento += "|" + horario[i];
          i++;
     }
     semana[aula] = evento;
}
function ActualizaTabla() {
     if (actualizar == false) {
          return;
     }
     var grupo = $("#txtGrupo").val();
     EliminarFila(grupo, 2);
}