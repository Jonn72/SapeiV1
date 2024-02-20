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
                    var tokensArray = $("#tokens").find('span');
                    $.each(tokensArray, function () {
                        if ($(this).text() == data.Mensaje) {
                            Command: toastr["info"]("El numero de control ya se encuentra en la lista", "Ya existe un token en la lista")
                            isMensaje = true;
                            return;
                        }
                    });
                    if (!isMensaje) {
                        Command: toastr["success"]("Numero de control agregado a la lista", "Informacion correcta")
                        $("#tokens").append('<div id="card_element" style="margin-right:15px; display: inline-block; ; background:#fafafa; border-radius:5px; height:160px; width:150px; overflow:hidden; margin-bottom:10px; box-shadow:0px 1px 8px #aaa;">' +
                                '<p>No. Control:</p>' +
                                '<p>' + loBusqueda + '</p>' +
                                '<p>Token:</p>' +
                                '<div style="width:100%; display:table; text-align:center">' +
                                   '<span class="label label-info">' + data.Mensaje + '</span>' +
                                '</div>' +
                                '<br />' +
                                '<button id="eliminarControl" class="btn btn-block btn-danger"> <span class="fa fa-trash"></span></button>' +
                                '</div>');
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

    $("#enviar").on('click', function (event) {
        event.preventDefault();
        var loTitulo = $("#titulo").val().trim();
        var loDescripcion = $("#descripcion").val().trim();
        var loUrlImagen = $("#urlimagen").val().trim();
        var loUrlVideo = $("#urlvideo").val().trim();
        var loIconDepto = $("#img_depto").text().trim();

        var tokensArray = $("#tokens").find('span');
        var loTokens = [];
        $.each(tokensArray, function () {
            if ($(this).text().trim().length > 0)
                loTokens.push('\'' + $(this).text() + '\'');
        });

        if (loTokens.length > 0) {

            if (loTitulo.length > 0) {
                if (loDescripcion.length > 0) {
                    loTitulo = loTitulo.replace('\'', '\'\'');
                    loDescripcion = loDescripcion.replace('\'', '\'\'');
                    $.ajax({
                        url: '../../Gnosis/EnviarNotificacionPersonalizada',
                        type: 'POST',
                        dataType: 'json',
                        data: { psTitulo: loTitulo, psDescripcion: loDescripcion, psUrlImagen: loUrlImagen, psUrlVideo: loUrlVideo, psIconDepto: loIconDepto, psTokens: loTokens.toString() },
                    })
                    .done(function (data) {
                        if (data.Success) {
                            Command: toastr["success"](data.Mensaje, "")
                            $("#save_form")[0].reset();
                            limpiarPrevio();
                            limpiarMensaje();
                            Dropzone.forElement("#dropzone").removeAllFiles(true);
                        } else {
                            Command: toastr["info"](data.Mensaje, "")
                            limpiarMensaje();
                        }
                    })
                    .fail(function (data) {
                        Command: toastr["error"]("No hay conexion con el servidor", "Verifica tu conexion")
                    })
                    .always(function () {

                    });
                } else {
                    Command: toastr["warning"]("Introduzca un descripcion válida", "Verifica los campos")
                }
            } else {
                Command: toastr["warning"]("Introduzca un título válido", "verifica los campos")

            }
        } else {
            Command: toastr["warning"]("Ingrese un numero de control valido para poder realizar esta operacion", "¡Verifique los campos ")
        }
    });


    $("#banners").hide();
    Dropzone.autoDiscover = false;
    $("#dropzone").dropzone({
        maxFilesize: 1,
        maxFiles: 4,
        params: {
            psTipo: 'Publicacion'
        },
        dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
        dictInvalidFileType: 'Formato de archivo no permitido',
        dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG',
        init: function () {
            this.on('success', function (file, response) {
                if (response.success) {
                    cargarImagenServer(response.response);
                    $("#urlimagen").val(response.response);
                    var total = $(".dz-filename").length;
                    $(".dz-filename").each(function (index) {
                        if (index === total - 1) {
                            $(this).find("span").html(response.response);
                        }
                    });
                    $("#banners").show();
                    ocultarBVideo();

                    $(".dz-preview").each(function (index) {
                        $(this).on("click", function () {
                            var name = $(this).find(".dz-details").find(".dz-filename").find("span").text();
                            cargarImagenServer(name);
                            $("#urlimagen").val(name);
                            $("#banners").show();
                            ocultarBVideo();
                        });
                    });

                }

            })
        }
    });

    $("#titulo").keyup(function () {
        if ($(this).val().trim().length > 0)
            $(".titulo_pre").text($(this).val().trim());
        else
            $(".titulo_pre").text("Titulo de la publicación");

    });

    $("#descripcion").keyup(function () {
        if ($(this).val().trim().length > 0)
            $(".descripcion_pre").text($(this).val());
        else
            $(".descripcion_pre").text("Descripción de la publicación");
    });

    $("#urlvideo").keyup(function () {
        if ($(this).val().trim().length > 0) {
            $("#banners").show();
            ocultarBImagen();
            var url = $(this).val().trim();
            if (url.length > 0) {
                if (url.toUpperCase().indexOf("YOUTUBE") >= 0) {
                    url = url.replace("watch?v=", "embed/");
                }
            }
            $("iframe.banner_video_pre").attr("src", url);
            $(this).val(url);
        } else {
            reiniciarVista();
        }
    });

    $("#urlimagen").keyup(function () {
        if ($(this).val().trim().length > 0) {
            $("#banners").show();
            ocultarBVideo();
            $("img.banner_imagen_pre").attr("src", $(this).val());
        } else {
            reiniciarVista();
        }
    });


    function ocultarBImagen() {
        $("#banner_video").show();
        $("#banner_imagen").hide();
        $("#content_imagen").hide();
        $(".dropzone_form").hide();
        $("#urlimagen").val("");
    }

    function ocultarBVideo() {
        $("#banner_video").hide();
        $("#banner_imagen").show();
        $("#content_video").hide();
        $(".dropzone_form").show();
        $("#urlvideo").val("");
    }

    function reiniciarVista() {
        $("#banner_titulo").show();
        $("#banners").hide();
        $("#content_video").show();
        $("#content_imagen").show();
        $("#urlimagen").val("");
        $("#urlvideo").val("");
        $(".dropzone_form").show();
    }

    function limpiarPrevio() {
        reiniciarVista();
        $(".titulo_pre").text("Titulo de la publicación");
        $(".descripcion_pre").text("Descripción de la publicación");
        $("img.banner_imagen_pre").attr("src", '');
    }

    function cargarImagenServer(ruta) {
        var hostname = location.hostname;
        switch (hostname) {
            case "localhost":
                $(".banner_imagen_pre").attr("src", "http://localhost" + ruta);
                break;
            case "sii.ittlahuac.edu.mx":
                $(".banner_imagen_pre").attr("src", "https://sii.ittlahuac.edu.mx:88" + ruta);
                break;
            case "192.168.9.245":
                $(".banner_imagen_pre").attr("src", "https://192.168.9.245:88" + ruta);
                break;
        }
    }

    if (location.hostname != "sii.ittlahuac.edu.mx") {
        var url = $("#logo_perfil").attr("src");
        url = url.replace("sii.ittlahuac.edu.mx", "192.168.9.245");
        $("#logo_perfil").attr("src", url);
    }

    function validarNoControl(NoControl) {
        if (NoControl == "/topics/noticias_sii")
            return true;
        var regex = new RegExp("((R|C){1})*([1,2]{1})([0-9]{1})([0-9]{3})([0-9]{3,5})");
        if (regex.test(NoControl))
            return true;
        return false;
    }

    $(document.body).on('click', '#eliminarControl', function () {
        var elem = $(this).parent();
        elem.remove();
    });
})();