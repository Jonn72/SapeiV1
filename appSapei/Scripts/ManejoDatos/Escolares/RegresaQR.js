$(document).ready(function () {
     $("#txtNoControl").focus();
});
$("#btnBuscar").on('click', function (evento) {
    evento.preventDefault();
    var control = $("#txtNoControl").val();
    $('#BodyPrincipal').load('../../../../ServiciosEscolares/RegresaQR', { psControl: control });
});
