
$("#btnOtorgarPermisos").click(function () {
    var psUsuarioSeleccionado;
    psUsuarioSeleccionado = $("#seleccionaUsuario").val();

    $("#btnEliminar").hide();
    $('#BodyPrincipal').load('../../../../ControlUsuario/PermisosUsuario', { psUsuarioSeleccionado: psUsuarioSeleccionado});
})

$("#btnEliminar").click(function () {
    var psNuevoUsuario = $("#seleccionaUsuario").val();
    var psNombreUsuario = $("#seleccionaNombre").val();
    console.log(psNuevoUsuario);
    console.log(psNombreUsuario);
    
    var psEstado = 2;

    $.ajax({
        url: '../../../ControlUsuario/EliminaUsuario',
        type: 'POST',
        dataType: 'json',
        data:
            { psNuevoUsuario: psNuevoUsuario, psNombreUsuario: psNombreUsuario, psEstado: psEstado },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("warning", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                psEstado = 1;
                MensajesToastr("success", "Solicitud Procesada", "Usuario Elimnado");
                $('#BodyPrincipal').load('../../../../ControlUsuario/Usuarios', { psNuevoUsuario: psNuevoUsuario, psNombreUsuario: psNombreUsuario, psEstado: psEstado});
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
})


var psContraseña1;
function ValidaContraseña() {
    psContraseña1 = $("#txtContraseña1").val();
    var psContraseña2 = $("#txtContraseña2").val();

    if (psContraseña1 == null || psContraseña1.length == 0 && psContraseña2 == null || psContraseña2.length == 0) {
        MensajesToastr("warning", "Error de solicitud", "Debes de agregar una contraseña");
        return false;

    } else if ((psContraseña1.length > 0) === (psContraseña2.length > 0)) {

        MensajesToastr("success", "Solicitud Procesada", "Las contraseñas coinciden");
        $("#btnGuardar").hide();
        $("#btnContiniar").show();
        
    } else{
        MensajesToastr("warning", "Las contraseñas no coinciden", "Error de solicitud");
    }
}


$("#btnContinuar").click(function () {
    var psUsuario = $("#txtUsuario").val();
    var psNombreUsuario = $("#txtNombreUsuario").val();

    psEstado = 1;

       $.ajax({
           url: '../../../ControlUsuario/CrearUsuarios',
        type: 'POST',
        dataType: 'json',
        data:
               { psUsuario: psUsuario, psNombreUsuario: psNombreUsuario, psContraseña: psContraseña1 },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("warning", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
                $("#divCrearUsuario").hide();
                $("#divSeleccionaUsuario").show();
                $('#BodyPrincipal').load('../../../../ControlUsuario/Usuarios', { psEstado: psEstado});
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
})

var psUsuarioSeleccionado;
var clave;

$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
    var data = $('#datatable-buttons').DataTable().row(this).data();
    
    var NombreUsuario;

    psUsuarioSeleccionado = data[0].trim();
    NombreUsuario = data[1].trim();

    $("#seleccionaUsuario option").each(function () {
        if ($(this).html().trim() === psUsuarioSeleccionado.trim()) {
            $(this).attr("selected", "selected");
            return;
        }
    });

    $("#seleccionaNombre option").each(function () {
        if ($(this).html().trim() === NombreUsuario.trim()) {
            $(this).attr("selected", "selected");
            return;
        }
    });
});




$("#btnAsignaPermiso").click(function () {
    
    //console.log(psUsuarioSeleccionado);
    var psClav1;
    psClav1 = $('#clave1').val();
    var psClav2;
    psClav2 = $('#clave2').val();
    var psClave = clave;

    console.log(psUsuarioSeleccionado);
    console.log(psClave);

    if (psClav1.length > 0) {
        psClave = psClav1;
    }

    if (psClav2.length > 0) {
        psClave = psClav2;
    }

    $.ajax({
        url: '../../../ControlUsuario/OtotgarPermisosUsuario',
        type: 'POST',
        dataType: 'json',
        data:
            { psUsuarioSeleccionado: psUsuarioSeleccionado, psClave: psClave },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("warning", "Solicitud Procesada", "Error de solicitud");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Permiso Asignado");
                $('#BodyPrincipal').load('../../../../ControlUsuario/OtotgarPermisosUsuario', { psUsuarioSeleccionado: psUsuarioSeleccionado, psClave: psClave });
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
})


