var semana;
var activaEventos;
var actualizar;
var periodo;
$(document).ready(function () {
     CargaComboAulas();
     activaEventos = false;
     actualizar = false;
     $("#txtCapacidad").val(0);
     periodo = $("#hidPeriodo").val();
     CargaComboPeriodo(2, 1, periodo);
});
$("#cboPeriodo").change(function (evento) {
     evento.preventDefault
     periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../DesarrolloAcademico/RegistrarGrupos', { psPeriodo: periodo, pbSoloTabla : false });
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
function CargaComboAulas() {
     $("#divHorario").hide("slow");
     $('#loadingmessage').show("slow");
     semana = [];
     periodo = $("#hidPeriodo").val();
     $.ajax({
          url: '../../../Generales/RegresaAulasConHorarios',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo : periodo},
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
          AgregaEvento(evento[1], evento[2], evento[0], evento[3]);
          i++;
     }
     if (activaEventos)
          ActivaEdicionEventos($("#txtGrupo").val().trim());
}
$("#btnGuardar").click(function Nuevo(evento) {
    event.preventDefault();
     var aula = $("#cboAulas option:selected").text();
     var capacidad = $("#txtCapacidad").val();
     var grupo = $("#txtGrupo").val();
     var tutor = $("#cboTutores").val();
    var horario = "";
    var usuario = $("#hidUser").val();
    periodo = $("#cboPeriodo").val();
    if (usuario == 0) {
        GuardaTutor(grupo, tutor, capacidad);
        return;
    }
     horario = RegresaCadenaHorarios(grupo);
     if (horario.trim().length == 0)
     {
          MensajesToastr("info", "Solicitud Procesada", "Debe seleccionar el horario");
          return;
     }
     if (!ValidaHoarario(horario)) {
          MensajesToastr("info", "Validación", "Debe seleccionar un horario de 1 hora");
          return;
     }
     $.ajax({
          url: '../../../DesarrolloAcademico/GuardaGrupoTutoria',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo : periodo, psGrupo: grupo, piCapacidad: capacidad, psTutor: tutor, psAula: aula, psHorario: horario },
     })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
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
function GuardaTutor(grupo, tutor, capacidad, horario) {
    var periodo = $("#hidPeriodo").val();

    $.ajax({
        url: '../../../DesarrolloAcademico/ActualizaGrupoTutoriaTutor',
        type: 'POST',
        dataType: 'json',
        data: { psPeriodo:periodo, psGrupo: grupo, psTutor: tutor, piCapacidad: capacidad },
    })
        .done(function (data) {
            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
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
}
function ValidaHoarario(horario) {
    console.log(horario);
     return ValidaTotalHoras($("#txtGrupo").val(), 1)
}
function ValidaGrupo() {
     if (BuscaValor($("#txtGrupo").val(), 0)) {
          MensajesToastr("info", "Validación", "Este grupo ya fue creado, puede verificar en la tabla inferior");
          $("#txtGrupo").val("");
          return;
     }
}
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();
     var tutor;
     var grupo;
    var aula;

     tutor = data[1].trim();
     aula = data[4].toUpperCase().split("AULA")[1].trim();
     $("#txtGrupo").val(data[0].trim());
     $('#txtGrupo').prop('readonly', true);


     $("#cboTutores option").each(function () {
          if ($(this).html().trim() === tutor.trim()) {
               $(this).attr("selected", "selected");
               return;
          }
     });

     $("#cboAulas option").each(function () {
          if ($(this).html().trim() === aula) {
               $(this).attr("selected", "selected");
               return;
          }
     });
    $("#txtCapacidad").val(data[2]);
    $("#hidInscritos").val(data[3]);
    $("#cboAulas").trigger('change');
    actualizar = true;
     if ($("#hidUser").val() == "0")
        return;
    activaEventos = true;
     $("#divEliminar").show("slow");
     $("#btnEliminar").prop('disabled', false);

});
$("#btnNuevo").click(function Nuevo(evento) {
     LimpiaControles();
     evento.preventDefault();
})
$("#btnEliminar").click(function Nuevo(evento) {
     event.preventDefault();
     periodo = $("#cboPeriodo").val();
     if ($("#hidUser").val() == "0")
          return;
     var grupo = $("#txtGrupo").val();
     var aula = $("#cboAulas").val();
     bootbox.confirm("Realmente desea eliminar el grupo " + grupo, function (result) {
          if (result == false)
               return;
          $.ajax({
               url: '../../../DesarrolloAcademico/EliminaTutoria',
               type: 'POST',
               dataType: 'json',
               data: {psPeriodo:periodo, psGrupo: grupo },
          })
     .done(function (data) {
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("info", "Solicitud Procesada", "Error al eliminar grupo");
          }
          else {
               MensajesToastr("success", "Solicitud Procesada", "Grupo Eliminado");
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
     $("#cboTutores").val(0)
    $("#cboAulas").val(0)
    $("#hidInscritos").val(0);
     activaEventos = false;
     actualizar = false;
}
function RegistraEvento(horario) {
     var valores = [];
     RegistraEventosAgregados($("#txtGrupo").val());
     valores[0] = $("#txtGrupo").val();
     valores[1] = $("#cboTutores :selected").text();
     valores[2] = $("#txtCapacidad").val();
    valores[3] = $("#hidInscritos").val();
     valores[4] = RegresaCadenaHorariosCorta($("#txtGrupo").val()) + " AULA " + $("#cboAulas :selected").text();
     //valores[5] = $("#cboAula :selected").text();
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
          semana[aula] = semana[aula] + RegresaCadenaCompletaHorarios($("#txtGrupo").val(),"T");
          return;
     }
     EliminaEvento(descripcion, aula);
     semana[aula] = semana[aula] + RegresaCadenaCompletaHorarios($("#txtGrupo").val(),"T");
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
     EliminarFila(descripcion, 0);
}