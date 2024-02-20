(function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $("#actualizar_noticias").on('click', function () {
        generarNotificacion('../../Gnosis/ActualizarNoticias');

    });

    $("#actualizar_informacion").on('click', function () {
        generarNotificacion('../../Gnosis/ActualizacionGeneral');
    });

    $("#eliminar_dispositivos").on('click', function () {
        generarNotificacion('../../Gnosis/EliminarDispositivos');
    });

    $("#actualizar_tablas").on('click', function () {
        var tokensArray = $("#tokens").find('p');
        var listaTablas = $("#select_data").val();
        var tokens = [];
        $.each(tokensArray, function () {
            tokens.push('\'' + $(this).text() + '\'');
        });
        if (tokens.length > 0) {
            $.ajax({
                url: '../../Gnosis/ActualizarTablas',
                type: 'POST',
                dataType: 'json',
                data: { psList: tokens.toString(), psTablas: listaTablas.toString() },
            })
            .done(function (data) {
                if (data.Success) {
                    Command: toastr["success"]("Se a generado la notificacion a los dispositivos indicados", "Solicitud Procesada")

                }
                else {
                    Command: toastr["warning"]("No se pudo realizar la solicitud", "Intentelo de nuevo")
                }
            })
            .fail(function () {
                Command: toastr["error"]("No hay conexion con el servidor", "Intentelo de nuevo")
            })
            .always(function () {
                $("#buscar_txt").val("");
            });
        } else {
            Command: toastr["warning"]("Ingrese un numero de control valido para poder realizar esta operacion", "¡Verifique los campos ")
        }
    });

    function generarNotificacion(psUrl) {
        var tokensArray = $("#tokens").find('p');
        var tokens = [];
        $.each(tokensArray, function () {
            tokens.push('\'' + $(this).text() + '\'');
        });
        if (tokens.length > 0) {
            $.ajax({
                url: psUrl,
                type: 'POST',
                dataType: 'json',
                data: { psList: tokens.toString() },
            })
            .done(function (data) {
                if (data.Success) {
                    Command: toastr["success"]("Se a generado la notificacion a los dispositivos indicados", "Solicitud Procesada")

                }
                else {
                    Command: toastr["warning"]("No se pudo realizar la solicitud", "Intentelo de nuevo")
                }
            })
            .fail(function () {
                Command: toastr["error"]("No hay conexion con el servidor", "Intentelo de nuevo")
            })
            .always(function () {
                $("#buscar_txt").val("");
            });
        } else {
            Command: toastr["warning"]("Ingrese un numero de control valido para poder realizar esta operacion", "¡Verifique los campos ")
        }
    }


    $("#buscar_btn").on('click', function () {
        var loBusqueda = $("#buscar_txt").val().trim();
        if (loBusqueda.length > 0 && validarNoControl(loBusqueda)) {
            $.ajax({
                url: '../../Gnosis/ValidarToken',
                type: 'POST',
                dataType: 'json',
                data: { psNoControl: loBusqueda },
            })
            .done(function (data) {
                if (data.Success) {
                    var isMensaje = false;
                    var tokensArray = $("#tokens").find('p');
                    $.each(tokensArray, function () {
                        if ($(this).text() == data.Mensaje) {
                            Command: toastr["info"]("El numero de control ya se encuentra en la lista", "Ya existe un token en la lista")
                            isMensaje = true;
                            return;
                        }
                    });
                    if (!isMensaje) {
                        Command: toastr["success"]("Numero de control agregado a la lista", "Informacion correcta")
                        $("#tokens").append('<p>' + data.Mensaje + '</p>');
                    }
                }
                else {
                    Command: toastr["warning"]("El numero de control aún no tiene instalada la aplicacion", "Informacion no disponible")
                }
            })
            .fail(function () {
                Command: toastr["error"]("No hay conexion con el servidor", "Intentelo de nuevo")
            })
            .always(function () {
                $("#buscar_txt").val("");
            });

        }
        else {
            Command: toastr["warning"]("Ingrese un numero de control valido", "¡Verifique los campos ")
        }
    });


    function validarNoControl(NoControl) {
        if (NoControl == "/topics/noticias_sii")
            return true;
        var regex = new RegExp("((R|C){1})*([1,2]{1})([0-9]{1})([0-9]{3})([0-9]{3,5})");
        if (regex.test(NoControl))
            return true;
        return false;
    }
})();