$(document).ready(function () {
});

$("#frmConsultas").submit(function (event) {
     var tipo = $("#cboConsultas").val();
     var periodo = $("#cboPeriodos").val();
     $("#divCargando").show();
     $("#iLoad").show("slow");
     $("#divTabla").hide();
     $(':input[type="submit"]').prop('disabled', true);
     $('#divTabla').load('../../../Consultas/EstudiantesJson/',
            { psPeriodo: periodo, poTipo: tipo },function() {
                 $(':input[type="submit"]').prop('disabled', false);
                 $("#iLoad").hide("slow");
                 $("#divTabla").show("slow");
     });
   
     event.preventDefault();
})
