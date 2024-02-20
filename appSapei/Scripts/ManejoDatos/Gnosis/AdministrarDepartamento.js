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
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

$("#actualizar_btn").on('click', function () {
    var loClave_txt = $('#clave_depto').val().trim();
    var loNombre_txt = $('#nombre_depto').val().trim();
    var loNombre_jefe_txt = $('#nombre_jefe_depto').val().trim();
    var loSelect_api = $("#select_api").val();
    var loUrlImagen_txt = $('#urlImagen').val().trim();
    if (loClave_txt.length > 0) {
        loClave_txt = loClave_txt.replace("'", "''");
        if (loNombre_txt.length > 0) {
            loNombre_txt = loNombre_txt.replace("'", "''");
            if (loNombre_jefe_txt.length > 0) {
                loNombre_jefe_txt = loNombre_jefe_txt.replace("'", "''");
                if (loUrlImagen_txt.length > 0) {
                    loUrlImagen_txt = loUrlImagen_txt.replace("'", "''");
                    Command: toastr["info"]("Espere un momento...", "")
                    $.ajax({
                        url: '../../Gnosis/ConfigurarDepartamento',
                        type: 'POST',
                        dataType: 'json',
                        data: { psClave: loClave_txt, psNombre: loNombre_txt, psNombreJefe: loNombre_jefe_txt, psAPI: loSelect_api, psUrlImagen: loUrlImagen_txt, psTipo: 2 },
                    })
                    .done(function (data) {
                        if (data.Success) {
                            Command: toastr["success"]("Se ha actualizado la información de manera correcta.", "¡Información Actualizada!")
                        }
                        else {
                            Command: toastr["info"]("No se pudo realizar la operacion solicitada", "¡Surgio un problema!")
                        }
                    })
                    .fail(function () {
                        Command: toastr["error"]("No se pudo realizar la operacion solicitada", "¡Sin conexion al servidor!")
                    })
                    .always(function (data) {
                        //se limpian los campos
                        if (data.Success) {
                            $('#BodyPrincipal').load('../../../../Gnosis/AdministrarDepartamento');
                        }
                    });

                }
                else {
                    Command: toastr["warning"]("Ingrese un url o carga un imagen", "¡Verifique los campos!")
                }
            }
            else {
                Command: toastr["warning"]("Ingrese un nombre de jefe de departamento", "¡Verifique los campos!")
            }
        }
        else {
            Command: toastr["warning"]("Ingrese un nombre de departamento", "¡Verifique los campos!")
        }
    }
    else {
        Command: toastr["warning"]("Ingrese una clave", "¡Verifique los campos!")
    }

});

$("#actualizar_btn").hide();

$("#crear_btn").on('click', function () {
    var loClave_txt = $('#clave_depto').val().trim();
    var loNombre_txt = $('#nombre_depto').val().trim();
    var loNombre_jefe_txt = $('#nombre_jefe_depto').val().trim();
    var loSelect_api = $("#select_api").val();
    var loUrlImagen_txt = $('#urlImagen').val().trim();

    if (loClave_txt.length > 0) {
        loClave_txt = loClave_txt.replace("'", "''");
        if (loNombre_txt.length > 0) {
            loNombre_txt = loNombre_txt.replace("'", "''");
            if (loNombre_jefe_txt.length > 0) {
                loNombre_jefe_txt = loNombre_jefe_txt.replace("'", "''");
                if (loUrlImagen_txt.length > 0) {
                    loUrlImagen_txt = loUrlImagen_txt.replace("'", "''");
                    Command: toastr["info"]("Espere un momento...", "")
                    $.ajax({
                        url: '../../Gnosis/ConfigurarDepartamento',
                        type: 'POST',
                        dataType: 'json',
                        data: { psClave: loClave_txt, psNombre: loNombre_txt, psNombreJefe: loNombre_jefe_txt, psAPI: loSelect_api, psUrlImagen: loUrlImagen_txt, psTipo: 1 },
                    })
                    .done(function (data) {
                        if (data.Success) {
                            Command: toastr["success"]("Se ha guardado la información de manera correcta.", "¡Información Almacenada!")
                        }
                        else {
                            Command: toastr["info"]("No se pudo realizar la operacion solicitada", "¡Surgio un problema!")
                        }
                    })
                    .fail(function () {
                        Command: toastr["error"]("No se pudo realizar la operacion solicitada", "¡Sin conexion al servidor!")
                    })
                    .always(function (data) {
                        //se limpian los campos
                        if (data.Success) {
                            $('#BodyPrincipal').load('../../../../Gnosis/AdministrarDepartamento');
                        }
                    });

                }
                else {
                    Command: toastr["warning"]("Ingrese un url o carga un imagen", "¡Verifique los campos!")
                }
            }
            else {
                Command: toastr["warning"]("Ingrese un nombre de jefe de departamento", "¡Verifique los campos!")
            }
        }
        else {
            Command: toastr["warning"]("Ingrese un nombre de departamento", "¡Verifique los campos!")
        }
    }
    else {
        Command: toastr["warning"]("Ingrese una clave", "¡Verifique los campos!")
    }
});

$("#cancelar_btn").hide();
$("#dropzone").dropzone({
    maxFilesize: 1,
    maxFiles: 4,
    params: {
        psTipo: 'Departamento'
    },
    dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
    dictInvalidFileType: 'Formato de archivo no permitido',
    dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
    acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG',
    init: function () {
        this.on('success', function (file, response) {
            if (response.success) {
                cargarImagenServer(response.response);
                $("#urlImagen").val(response.response);
                var total = $(".dz-filename").length;
                $(".dz-filename").each(function (index) {
                    if (index === total - 1) {
                        $(this).find("span").html(response.response);
                    }
                });
                $("#cancelar_btn").show();
                $(".dz-preview").each(function (index) {
                    $(this).on("click", function () {
                        var name = $(this).find(".dz-details").find(".dz-filename").find("span").text();
                        cargarImagenServer(name);
                        $("#urlImagen").val(name);
                    });
                });

            }

        })
    }
});

$("#clave_depto").keyup(function () {
    if ($("#clave_depto").val().trim().length > 0) {
        $("#cancelar_btn").show();
    } else if ($("#nombre_depto").val().trim() == 0 && $("#nombre_jefe_depto").val().trim() == 0 && $("#urlImagen").val().trim() == 0) {
        $("#cancelar_btn").hide();
    }
});

$("#nombre_depto").keyup(function () {
    if ($("#nombre_depto").val().trim().length > 0) {
        $("#cancelar_btn").show();
    } else if ($("#clave_depto").val().trim() == 0 && $("#nombre_jefe_depto").val().trim() == 0 && $("#urlImagen").val().trim() == 0) {
        $("#cancelar_btn").hide();
    }
});

$("#nombre_jefe_depto").keyup(function () {
    if ($("#nombre_jefe_depto").val().trim().length > 0) {
        $("#cancelar_btn").show();
    } else if ($("#clave_depto").val().trim() == 0 && $("#nombre_depto").val().trim() == 0 && $("#urlImagen").val().trim() == 0) {
        $("#cancelar_btn").hide();
    }
});


$("#urlImagen").keyup(function () {
    if ($("#urlImagen").val().trim().length == 0) {
        $("#banner_imagen_pre").attr("src", "");
    }

    if ($("#urlImagen").val().trim().length > 0) {
        $("#banner_imagen_pre").attr("src", $("#urlImagen").val().trim());
        $("#cancelar_btn").show();
    } else if ($("#nombre_depto").val().trim() == 0 && $("#nombre_jefe_depto").val().trim() == 0 && $("#clave_depto").val().trim() == 0) {
        $("#cancelar_btn").hide();
    }
});


$(".card-image").find("img").each(function () {
    var url = $(this).attr("src");
    url = cargarImagen(url);
    if (url != "") {
        $(this).attr("src", url);
    }
});

function cargarImagen(url) {
    if (location.hostname != "sii.ittlahuac.edu.mx") {
        return url.replace("sii.ittlahuac.edu.mx", "192.168.9.245");
    }
    return "";
}


$(".card_horizontal").each(function () {
    $(this).on('click', function () {
        var url_img = $(this).find(".card-image").find('img').attr('src');
        $("#banner_imagen_pre").attr('src', url_img);
        $("#crear_btn").hide();
        $("#actualizar_btn").show();
        $("#cancelar_btn").show();
        url_img = url_img.replace("https://", "");
        url_img = url_img.replace("sii.ittlahuac.edu.mx", "");
        url_img = url_img.replace("192.168.9.245", "");
        url_img = url_img.replace(":88", "");
        $("#urlImagen").val(url_img);
        $("#titulo_txt").html('Editar Departamento');
        var valores = $(this).find('.card-content').find('p');
        valores.each(function (key, value) {
            switch (key) {
                case 2:
                    $("#clave_depto").val($(this).text());
                    $("#clave_depto").attr('disabled', true);
                    break;
                case 0:
                    $("#nombre_depto").val($(this).text());
                    break;
                case 1:
                    $("#nombre_jefe_depto").val($(this).text());
                    break;
                case 3:
                    if ($(this).text().trim() === "Activo")
                        $("[name=select_api]").val(1);
                    else
                        $("[name=select_api]").val(0);
                    break;
            }
        });

    })
});


function cargarImagenServer(ruta) {
    var hostname = location.hostname;
    switch (hostname) {
        case "localhost":
            $("#banner_imagen_pre").attr("src", "http://localhost" + ruta);
            break;
        case "sii.ittlahuac.edu.mx":
            $("#banner_imagen_pre").attr("src", "https://sii.ittlahuac.edu.mx:88" + ruta);
            break;
        case "192.168.9.245":
            $("#banner_imagen_pre").attr("src", "https://192.168.9.245:88" + ruta);
            break;
    }
}

$("#cancelar_btn").on('click', function () {
    $("#clave_depto").val("");
    $("#nombre_depto").val("");
    $("#nombre_jefe_depto").val("");
    $("[name=select_api]").val(0);
    $("#urlImagen").val("");
    $("#actualizar_btn").hide();
    $("#crear_btn").show();
    $("#banner_imagen_pre").attr("src", "");
    Dropzone.forElement("#dropzone").removeAllFiles(true);
    $("#clave_depto").attr('disabled', false);
    $("#titulo_txt").html('Crear Departamento');
    $(this).hide();
});

})();

