$("#btnRegresar").click(function () {
    $("#Mensajes").css("display", "none");
    $("#tabla").css("display", "block");
    $("#frmQuejasySugerencias").css("display", "none");
})

$("#frmQuejasySugerencias").submit(function (event) {
    event.preventDefault();
    $(".alert").show();

    var asunto = $("#txtAsunto").val();
    var mensaje = $("#txtQS").val();

    $.ajax({
        url: '../../../Generales/QuejasySugerenciasJSON',
        type: 'POST',
        dataType: 'json',
        data: { psAsunto: asunto, psMensaje: mensaje },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined" && !data.Success) {
                 MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
             }
             else {
                 MensajesToastr("info", "Solicitud Procesada", "Mensaje Enviado");
                 LimpiaControles();
                 $('#BodyPrincipal').load('../../../../Generales/GeneraQS');
             }
         })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
})


function LimpiaControles() {
    $("#txtAsunto").val("");
    $("#txtQS").val("");
}

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