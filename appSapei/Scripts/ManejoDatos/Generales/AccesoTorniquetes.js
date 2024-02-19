$(document).ready(function () {
     $(".alert").hide();
     init_dataTables();
});
$("#frmAccesoTorniquetes").submit(function (event) {
     var valor = $("#txtBuscar").val();
     $('#divTabla').load('../../../../ControlAcceso/CargaTablaAccesoTorniquetes',
            { psValor: valor });
     event.preventDefault();
})

