$(document).ready(function () {
    $(".alert").hide();
    $("#eliminar").click(function (event) {
        event.preventDefault();
        $("#eliminar").text("Procesando...");
        $.ajax({
            url: '../../Gnosis/EliminarDispositivo',
            type: 'POST',
            dataType: 'json',
            data: {},
        })
        .done(function (data) {
            
            if (data.Success) {

                $(".alert").show();
                $(".alert").addClass('alert-success');
                $(".alert").html("<center><strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; " + data.Mensaje + "</strong></center>");
            } else {

                $(".alert").show();
                $(".alert").addClass('alert-danger');
                $(".alert").html("<center><strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; " + data.Mensaje + "</strong></center>");
            }
        })
        .fail(function () {
            $(".alert").show();
            $(".alert").addClass('alert-danger');
            $(".alert").html("<center><strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error critico!. No se pudo realizar la publicación</strong></center>");
        })
        .always(function () {
            $("#eliminar").text("Espere un momento..");
            limpiarMensaje();
        });
    });

    function limpiarMensaje() {
        setTimeout(function () {
            $("#eliminar").text("Eliminar Dispositivo");
            if ($(".alert").hasClass('alert-danger')) {
                $(".alert").removeClass('alert-danger');
            } else if ($(".alert").hasClass('alert-warning')) {
                $(".alert").removeClass('alert-warning');
            } else if ($(".alert").hasClass('alert-info')) {
                $(".alert").removeClass('alert-info');
            } else {
                $(".alert").removeClass('alert-success');
            }
            $(".alert strong").remove();
            $(".alert").hide();
            
            $('#BodyPrincipal').load('../../../../Gnosis/DesvinculaDispositivo');
        }, 2000);
    }
});