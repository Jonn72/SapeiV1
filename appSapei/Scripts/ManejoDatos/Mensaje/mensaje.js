$(document).ready(function () {
});

function LimpiaControles() {
    $("#txtAsunto").val("");
    $("#txtQS").val("");
}

function cambia_contenido(contenido0, contenido1, contenido2, contenido3, contenido4, contenido5) {
    $("#Mensajes").css("display", "block");
    $("#fecha").html("Fecha de Apertura: "+contenido3);
    $("#titulo").html("<p><h5>Folio: "+contenido0+"</h5></p><p><h3>Asunto</h3></p><h4>" + contenido4 + "</h4>");
    $("#nombre_completo").html(contenido1);
    $("#no_de_control").html(contenido2);

    var campo = $('#hidticket').val();
    if (campo === '') {
        $("#" + contenido0).css("display", "block")
        $("#hidticket").val(contenido0);
        if (contenido5 === "False") {
            $("#compose").css("visibility", "hidden")
            $("#cerrar").css("visibility", "visible")
            $("#ticket").html("Folio: " + contenido0)
            
        }
        else {
            $("#compose").css("visibility", "visible")
            $("#cerrar").css("visibility", "hidden")
        }
    }
    else {
        $("#" + campo).css("display", "none");
        $("#"+contenido0).css("display", "block")
        $("#hidticket").val(contenido0);

        if (contenido5 === "False") {
            $("#compose").css("visibility", "hidden")
            $("#cerrar").css("visibility", "visible")
        }
        else {
            $("#compose").css("visibility", "visible")
            $("#cerrar").css("visibility", "hidden")
        }
    }
}

    $("#send").click(function (event) {
        event.preventDefault();
        $(".alert").show();

        var ticket = $("#hidticket").val();
        var mensaje = $("#txtQS").val();

        $.ajax({
            url: '../../../Generales/ContestaMensajeJSON',
            type: 'POST',
            dataType: 'json',
            data: { psTicket: ticket, psMensaje: mensaje },
        })
             .done(function (data) {
                 if (typeof (data.Success) !== "undefined" && !data.Success) {
                     MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
                 }
                 else {
                     MensajesToastr("info", "Solicitud Procesada", "Mensaje Enviado");
                     LimpiaControles();
                     $('#BodyPrincipal').load('../../../../Generales/QuejaySugerencia');
                 }
             })
              .fail(function (data) {
                  MensajesToastrErrorConexion();
              });
    })

    init_contadorTa("txtQS", "contadorTaComentario", 250);

    function init_contadorTa(idtextarea, idcontador, max) {
        $("#" + idtextarea).keyup(function () {
            updateContadorTa(idtextarea, idcontador, max);
        });

        $("#" + idtextarea).change(function () {
            updateContadorTa(idtextarea, idcontador, max);
        });

    }

    function updateContadorTa(idtextarea, idcontador, max) {
        var contador = $("#" + idcontador);
        var ta = $("#" + idtextarea);
        contador.html("0/" + max);

        contador.html(ta.val().length + "/" + max);
        if (parseInt(ta.val().length) > max) {
            ta.val(ta.val().substring(0, max - 1));
            contador.html(max + "/" + max);
        }

    }
