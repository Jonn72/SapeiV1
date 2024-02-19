$(document).ready(function () {

});


$("#btnBuscarNoControlRegresar").on('click', function (evento) {
    $('#MuestraDocumentos').hide();
    $('#MuestraDetalles').show();
    $('#MuestraTabla').show();
    $("#txtBuscarNoControl").val('');
    
    evento.preventDefault();
})

$("#btnBuscarNoControlTabla").on('click', function (evento) {
    $('#divModal').modal("show");
    evento.preventDefault();
})

function BuscaNoControl(control) {
    $('#MuestraDocumentos').load('../../../../ExpedienteDigital/MuestraDocumentosEstudiante/', 
        { psControl: control }, MuestraDatos());
}

function MuestraDatos() {
    $('#MuestraDocumentos').show();
    $('#MuestraDetalles').hide();
    $('#MuestraTabla').hide();
}



