var periodo;
$(document).ready(function () {
     var periodo;
     periodo = $("#hidPeriodo").val()
     QuitaBuscar();
     CargaComboPeriodo(4, 1, periodo);
});
$("#cboPeriodo").change(function (evento) {
     var periodo = $("#cboPeriodo").val();
     $('#BodyPrincipal').load('../../../ServiciosEscolares/MateriasPorBloque', { psPeriodo: periodo });
     evento.preventDefault();
});
$("#txtNoControl").on('input', function () {
     var val = this.value;
     var valor;
     var carrera;
     var nombre;
     $('#divDatos').hide();
     $("#divVisor").hide();

     if ($('#txtEstudiantes option').filter(function () {
         return this.value === val;
     }).length) {
          valor = $('#txtEstudiantes [value="' + val + '"]').data('value');
          carrera = valor.split('-')[1];
          $('option').each(function () {
               if ($(this).val() == carrera) {
                    $(this).show();
               } else {
                    $(this).hide();
               }
               $('#cboGrupos').val(carrera);
          })
         $('#txtNombre').val(valor.split('-')[0]);
         $('#txtNip').val(valor.split('-')[2]);
          $('#divDatos').show();
          $("#divVisor").hide();
     }
});

$("#frmMateriasBloque").submit(function (event) {
     var control = $("#txtNoControl").val();
     var grupo = $("#cboGrupos option:selected").text();
     var periodo = $("#cboPeriodo").val();
     $("#iLoad").show("slow");
     $(':input[type="submit"]').prop('disabled', true);
     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $.ajax({
          url: '../../../ServiciosEscolares/MateriasPorBloqueJson',
          type: 'POST',
          dataType: 'json',
          data: { psPeriodo: periodo, psControl:control, psGrupo:grupo },
     })
     .done(function (data) {
          
          if (typeof (data.Success) === "undefined" || !data.Success) {
               MensajesToastr("error", "Solicitud Procesada", "Error al Actualizar");
          }
          else {
               MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
               $('#divTabla').load('../../../ServiciosEscolares/MateriasPorBloque/',
            { pbSoloTabla: true });
              
               $('#divPDF').load('../../../../DocumentosOficiales/RegresaCargaAcademica', { psPeriodo: periodo, psControl: control }, displaySection);
          }
         // $("#txtNoControl").val("");
          // $("#txtNombre").val("");
          //$('#divDatos').hide();
     })
     .fail(function (data) {
          MensajesToastrErrorConexion();
     })
     .always(function () {
          $("#iLoad").hide("slow");
          $(':input[type="submit"]').prop('disabled', false);
     });

     event.preventDefault();
})
function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}