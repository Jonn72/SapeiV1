var idBusqueda;
$(document).ready(function () {
     $(".alert").hide();
     $("#divBotones").hide();
});
$("#frmEstudianteGenerales").submit(function (event) {
     var valor = $("#txtBuscar").val();
     var tipoBusqueda = $('input[name=rbtBusqueda]:checked').val();
     $('#BodyPrincipal').load('../../../../Estudiante/CargaTablaGenerales',
            { psValor: valor, psTipo: tipoBusqueda });
     event.preventDefault();
})
$("#btnRegresar").click(function Atras(evento) {
     event.preventDefault();
     $('#BodyPrincipal').load('../../../../Estudiante/EstudianteGenerales');
})

var table = $('#datatable-buttons').DataTable();

$('#datatable-buttons tbody').on('click', 'tr', function () {
     var esVisible = $("#divBotones").is(":visible");
     var nuevoValor = table.row(this).data()[0];
     if (nuevoValor !== idBusqueda) {
          idBusqueda = nuevoValor;
          $("#divBotones").show("slow");
     }
     else {
          $("#divBotones").hide("slow");
     }
});

$("#btnDatosGenerales").click(function Atras(evento) {
     RegresaEstudianteDatos(idBusqueda)
})

function RegresaEstudianteDatos(noControl) {
     event.preventDefault();
     $('#divDatosGenerales').load('../../../../Estudiante/CargaDatosGenerales',
            { psNoControl: noControl });
}
