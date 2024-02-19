var lbAgregaArroba = false;
$("#frmEntrenadores").submit(function (event) {
    var id = $("#hidId").val();
    var nombre = $("#txtNombre").val();
    var paterno = $("#txtPaterno").val();
    var materno = $("#txtMaterno").val();
    var usuario = $("#txtUsuario").val();
    var contrasenia = $("#txtContrasenia").val();
    var estatus = "";
    if ($("#chkActivo").is(':checked')) {
        estatus = true
    }
    else {
        estatus = false;
    }

    $.ajax({
        url: '../../../ExtraEscolares/GuardaEntrenador',
        type: 'POST',
        dataType: 'json',
        data: { piId: id, psNombre: nombre, psPaterno: paterno, psMaterno: materno, pbEstatus: estatus, psUsuario:usuario, psContrasenia:contrasenia },
    })
    .done(function (data) {
        if (typeof (data.Success) === "undefined" || !data.Success) {
             MensajesToastr("info", "Solicitud Procesada", "Error al Guardar");
            $("#btnSiguiente").prop("disabled", false);
        }
        else {
             MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
        }
    })
    .fail(function (data) {
         MensajesToastrErrorConexion();
    })
    .always(function () {
        $('#BodyPrincipal').load('../../../../ExtraEscolares/Entrenadores');
    });
    event.preventDefault();
});
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();

    var nombre;
    var apaterno;
    var amaterno
    var estatus
    var usuario;
    var contrasenia;
    nombre = data[1];
    apaterno = data[2];
    amaterno = data[3];
    estatus = data[4];
    usuario = data[5];
    contrasenia = data[6];
    $("#hidId").val(data[0]);
    $("#txtNombre").val(nombre);
    $("#txtPaterno").val(apaterno);
    $("#txtMaterno").val(amaterno);
    $("#txtUsuario").val(usuario);
    $("#txtContrasenia").val(contrasenia);
    if (usuario.indexOf("@") == 0)
    {
         lbAgregaArroba = true;
    }
   if (estatus === "ACTIVO") {
    $("#chkActivo").attr('checked', true);
   }
   else {
       $("#chkActivo").attr('checked', false);
   }
});

$("#txtUsuario").keyup(function () {
     var valor = $(this).val();
     $(this).val(valor.toUpperCase());
     if (valor.trim().length == 0)
          lbAgregaArroba = false;
     if (!lbAgregaArroba) {
          $(this).val("@" + $(this).val());
          lbAgregaArroba = true;
     }
});