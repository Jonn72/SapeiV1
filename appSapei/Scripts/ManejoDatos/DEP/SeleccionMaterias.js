$("#btnBuscar").on('click', function (evento) {
    evento.preventDefault();
    var control = $("#txtNoControl").val();
    $('#BodyPrincipal').load('../../../../Reticula/Seleccion', { psControl: control });
});