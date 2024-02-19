var periodo;
var liberacion;
$(document).ready(function () {
     CargaComboPeriodo(12, 1);
});
$("#cboPeriodo").change(function (evento) {
     evento.preventDefault();
     CargaComboActividades($("#cboPeriodo").val());
});
$("#btnBuscar").on('click', function (evento) {
     evento.preventDefault();
     $('#divDatos').hide();
    $("#divVisor").hide();
    var control = $("#txtNoControl").val().trim();

    if (!ex_no_control.test(control)) {
        MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
        return;
    }
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
     CargaComboActividades($("#cboPeriodo").val());
});
$("#btnAtras, #btnAtras1").on('click', function (evento) {
    evento.preventDefault(); 
    $('#divDatos').hide();
     $("#divVisor").hide();
     $('#divControl').hide();
     $('#divActividad').hide();
    $("#divBuscar").show();
    $("#cbxLiberacionDirecta").prop("checked", false);
});
function CargaComboActividades(periodo) {
     $.ajax({
          url: '../../../Generales/RegresaComboActividades',
          type: 'POST',
          dataType: 'json',
          data: { psId: null, psPeriodo: periodo},
     })
          .done(function (data) {
               $("#cboActividades").empty();
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("error", "Solicitud Procesada", "No existen actividades registradas");
                    $("#btnGenerarGrupo").hide();
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    for (; resultado.data[i];) {
                         $("#cboActividades").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
                    $("#btnGenerarGrupo").show();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
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
    var control = $("#txtNoControl").val().trim();

     $.ajax({
          url: '../../../../ExtraEscolares/RegresaActividadesInscritasEstudiante',
          type: 'POST',
          dataType: 'json',
          data: {psPeriodo : periodo, psNoControl:control},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               }
               else {
                    var resultado = JSON.parse(data);
                    var datos;
                    $("#txtNombre").val(resultado.data[0].nombre);
                    $("#cboActividad").empty();                    
                    $.each(resultado.data, function (index, value) {
                         $("#cboActividad").append('<option value=' + value.actividad + '>' + value.descripcion + '</option>');
                         datos = value.entrenador + "|" + value.carrera + "|" + value.semestre + "|" + value.desempeño + "|" + value.promedio + "|" + value.ingreso;
                         $('#frmLiberacion').append('<input type="hidden" id="hid' + value.actividad + '" value="'+datos+'" />');
                    });
                    $('#divDatos').show();
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
$("#btnGenerarGrupo").on('click', function (evento) {
     evento.preventDefault();
     var actividad = $("#cboActividades").val();
     var periodo = $("#cboPeriodo").val();
     var es15 = $('input[name=rbtFormato]:checked').val();
     if (es15 == "1")
          es15 = true;
     else
          es15 = false;
     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $('#divPDF').load('../../../../DocumentosOficiales/ConstanciaCumplimientoPorGrupo', { psPeriodo: periodo, psGrupo: actividad, pbEs15: es15, psCredito: "DEP" }, displaySection);
});
$("#btnGenerar").on('click', function (evento) {
     event.preventDefault();
     var control = $("#txtNoControl").val();
     var nombre = $("#txtNombre").val();
     var actividad = $("#cboActividad option:selected").text();
     var periodo = $("#cboPeriodo").val();
     var hid = document.getElementById("hid" + $("#cboActividad").val());
     var datos = $(hid).val().split("|");
     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $('#divPDF').load('../../../../DocumentosOficiales/ConstanciaCumplimiento', { psPeriodo: periodo, psControl: control, psNombre: nombre, psActividad: actividad, psEntrenador: datos[0], psCarrera: datos[1], psSemestre: datos[2], psDesempeño: datos[3], psPromedio: datos[4], psIngreso: datos[5], psCredito: "DEP" }, displaySection);
})
function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}
