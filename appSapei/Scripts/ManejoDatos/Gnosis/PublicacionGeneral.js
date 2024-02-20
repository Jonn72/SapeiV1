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
            if (url.length > 0)
            {
                if(url.toUpperCase().indexOf("YOUTUBE")>=0)
                {
                    url = url.replace("watch?v=","embed/");
                }
            }
            $(this).val(url.trim());
            $("iframe.banner_video_pre").attr("src", url);
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


    $("#enviar").on('click', function (event) {
        event.preventDefault();
        var loTitulo = $("#titulo").val().trim();
        var loDescripcion = $("#descripcion").val().trim();
        var loUrlImagen = $("#urlimagen").val().trim();
        var loUrlVideo = $("#urlvideo").val().trim();
        var loIconDepto = $("#img_depto").text().trim();
        if (loTitulo.length > 0) {
            if (loDescripcion.length > 0) {
                loTitulo = loTitulo.replace('\'', '\'\'');
                loDescripcion = loDescripcion.replace('\'', '\'\'');
                $.ajax({
                    url: '../../Gnosis/EnviarNotificacion',
                    type: 'POST',
                    dataType: 'json',
                    data: { psTitulo: loTitulo, psDescripcion: loDescripcion, psUrlImagen: loUrlImagen, psUrlVideo: loUrlVideo, psIconDepto: loIconDepto },
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
    });

    
    function limpiarPrevio() {
        reiniciarVista();
        $(".titulo_pre").text("Titulo de la publicación");
        $(".descripcion_pre").text("Descripción de la publicación");
        $("img.banner_imagen_pre").attr("src", '');
    }

    function cargarImagenServer(ruta) {
        var hostname = location.hostname;
        switch(hostname)
        {
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

    if(location.hostname!="sii.ittlahuac.edu.mx")
    {
        var url = $("#logo_perfil").attr("src");
        url = url.replace("sii.ittlahuac.edu.mx", "192.168.9.245");
        $("#logo_perfil").attr("src", url);
    }

})();