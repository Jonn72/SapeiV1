var periodo;
$(document).ready(function () {
     CargaComboPeriodo(10, 1);
});

$("#btnBuscar").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
     $("#divVisor").hide();
     CargaActividades();
});
$("#btnIndividual").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
     $("#divVisor").hide();
     $('#divControl').show();
     $('#divActividad').hide();
     $("#divBuscar").hide();
});
$("#btnGrupo").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
     $("#divVisor").hide();
     $('#divActividad').show();
     $('#divControl').hide();
     $("#divBuscar").hide();
});
$("#btnAtras, #btnAtras1").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
     $("#divVisor").hide();
     $('#divControl').hide();
     $('#divActividad').hide();
     $("#divBuscar").show();
});
$('input[type=radio][name=rbtFormato]').change(function () {
     if ($('input[name=rbtFormato]:checked').val() == 0)
     {
          $("#divNoOficio").hide();
          $("#divNoOficio").val("0");
     }
     else
          $("#divNoOficio").show();
});

function CargaActividades() {
     var periodo = $("#cboPeriodo").val();
     var control = $("#txtNoControl").val();
     if (!ex_no_control.test(control))
     {
          MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
          return;
    }
    $("#btnGenerar").prop('disabled', false);
     $.ajax({
          url: '../../../../DesarrolloAcademico/RegresaTutoriaEstudiante',
          type: 'POST',
          dataType: 'json',
          data: {psNoControl:control},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "No se encontro información de tutoría concluida");
               }
               else {
                    var resultado = JSON.parse(data);
                    var datos;
                    $("#txtNombre").val(resultado.data[0].nombre);
                    $("#txtGrupo").val(resultado.data[0].grupo);
                    datos = resultado.data[0].entrenador + "|" + resultado.data[0].carrera + "|" + resultado.data[0].semestre + "|" + resultado.data[0].desempeño + "|" + resultado.data[0].promedio + "|" + resultado.data[0].folio;
                    $('#frmLiberacion').append('<input type="hidden" id="hid' + resultado.data[0].grupo + '" value="' + datos + '" />');
                    
                   $('#divDatos').show();
                   if (resultado.data[0].promedio < 1) {
                       MensajesToastr("info", "Solicitud Procesada", "El estudiante " + resultado.data[0].nombre + ", no ha aprobado y no puede ser liberado");
                       $("#btnGenerar").prop('disabled', true);
                   }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#btnGenerarGrupo").on('click', function (evento) {
     evento.preventDefault();
     var grupo = $("#txtGrupos").val();
     var periodo = $("#cboPeriodo").val();
     es15 = true;

     $("#divVisor").show();
     $("#divCargando").show();
     $('#divPDF').load('../../../../DocumentosOficiales/ConstanciaCumplimientoPorGrupo', { psPeriodo: periodo, psGrupo: grupo, pbEs15: es15, psCredito: "TUT" }, displaySection);
});
$("#btnGenerar").on('click', function (evento) {
     event.preventDefault();
     var control = $("#txtNoControl").val();
     var nombre = $("#txtNombre").val();
     var actividad = "TUTORIAS";
     var periodo = $("#cboPeriodo").val();
     var hid = document.getElementById("hid" + $("#txtGrupo").val());
    var datos = $(hid).val().split("|");
    if (datos[4] < 1) {
        MensajesToastr("info", "Solicitud Procesada", "El estudiante " + nombre + ", no ha aprobado y no puede ser liberado");
        $("#btnGenerar").prop('disabled', true);
        return;
    }
     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $('#divPDF').load('../../../../DocumentosOficiales/ConstanciaCumplimiento', { psPeriodo: periodo, psControl: control, psNombre: nombre, psActividad: actividad, psEntrenador: datos[0], psCarrera: datos[1], psSemestre: datos[2], psDesempeño: datos[3], psPromedio: datos[4], psIngreso : "20161", psCredito: "TUT"}, displaySection);
})
function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}