$(document).ready(function () {
    $("#btninterno").click(function () {
        $('#BodyPrincipal').load('../../../../Vinculacion/CartaPresentacionInternoSS');
    });
    $("#btnexterno").click(function () {
        $('#BodyPrincipal').load('../../../../Vinculacion/CartaPresentacionExternoSS');
    });
});