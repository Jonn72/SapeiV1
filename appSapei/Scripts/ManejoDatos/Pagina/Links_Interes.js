(function () {
    var JsonGet;
    var urlBanner;
    var urlContenido;
    var urlServer;
    var datas;
    var hostname = location.hostname;
    switch (hostname) {
        case "localhost":
            //urlServer = "http://192.168.9.246";
            urlServer = "http://localhost:57878";
            break;
        case "sapei.tlahuac.tecnm.mx":
            urlServer = "https://tlahuac.tecnm.mx";
            break;
        case "192.168.9.245":
            urlServer = "https://192.168.9.246";
            break;
    }

    $("#divArchivo").show();
    $("#divUrl").hide();
    $("#banners").hide();
    $("#btnEnviarEditar").hide();
    $("#btnCancelar").hide();
    $("#urlcargada").hide();
    $("#titulotxts").hide();
    $("#copiar").hide();
    Dropzone.autoDiscover = false;
    $("#dropzone_archivo").dropzone({
        maxFilesize: 10,
        maxFiles: 1,
        url: urlServer,
        dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
        dictInvalidFileType: 'Formato de archivo no permitido',
        dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG,.PDF,.pdf,.rar',
        renameFile: function (file) {
            return 'urlImagen.' + file.name.split(".")[1];
        },
        init: function () {
            this.on("processing", function (file) {
                this.options.url = urlServer + "/api/UploadImages";
            });
            this.on('success', function (file, response) {
                urlContenido = response;
            })
        }
    });

    $("#btnEnviar").on('click', function (event) {
        event.preventDefault();
        var obj = new Object();
        //if (urlBanner == null || urlContenido == null) {
        //    MensajesToastr("warning", "Debe seleccionar una imagen de banner", "Vuelve a intentarlo");
        //    return;
        //}
        obj.titulo = $("#titulo").val().trim();
        obj.urlimagen = urlBanner;
        var tipo = $('input[name=rbtPublicacion]:checked').val();
        if (tipo == 'U')
            urlContenido = $("#urlvideo").val().trim();
        obj.link = urlContenido;

        if ($("#cbxPublicar").is(':checked'))
            obj.activa = true;
        else
            obj.activa = false;
        var urlServerPost = urlServer + "/api/Links_Interes";
        obj = JSON.stringify(obj);
        $.ajax({
            url: urlServerPost,
            type: 'POST',
            crossDomain: true,
            contentType: 'application/json',
            dataType: 'json',
            data: obj,
        })
            .done(function (data) {
                MensajesToastr("success", "Se ha publicado el banner", "Publicación exitosa");
                LimpiaControles();
                //CargaNuevoBannerTabla(data);
                //CargarTablaBanners(JsonGet);
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            })
            .always(function () {

            });
    });

    function LimpiaControles() {
        $("#titulo").val("");
        urlBanner = "";
        urlContenido = "";
    }
    function CargaNuevoBannerTabla(data, nuevo) {
        var obj = JSON.parse(JsonGet);
        alert(JSON.stringify(data))
        obj.push(JSON.stringify(data));
    }
    $('input[type=radio][name=rbtPublicacion]').change(function () {

        var tipo = $('input[name=rbtPublicacion]:checked').val();
        $("#urlvideo").val("");
        if (tipo == "A") {
            $("#divArchivo").show();
            $("#divUrl").hide();
        }
        else {
            $("#divArchivo").hide();
            $("#divUrl").show();
        }

    });


    var settings = {
        "url": urlServer + "/api/login",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "usuario": "prueba", "password": "prueba123" }),
    };

    $.ajax(settings).done(function (response) {

        objs = response;
        toks = objs['token'];

        $.ajax({
            url: urlServer + "/api/Links_Interes",
            type: 'GET',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + toks,
            },
            success: function (result) {
                //CallBack(result);
                JsonGet = result;
                var htmlHead = "<tr>";
                htmlHead = htmlHead + "<th>No.</th>";
                htmlHead = htmlHead + "<th>Titulo</th>";
                htmlHead = htmlHead + "<th>Acciones</th>";
                htmlHead = htmlHead + "<th>Editar</th>";
                htmlHead = htmlHead + '</tr>';
                CargarTablaBanners();
                $('#tabla-json thead').append(htmlHead);

            },
            error: function (error) {

            }
        });


    });


    function CargarTablaBanners() {
        $("#tabla-json tbody").empty();
        var htmlTags = "";
        var class_activa;
        var id_boton;
        Object.keys(JsonGet).forEach(function (key) {
            class_activa = "success";
            if (JsonGet[key].activa)
                class_activa = "info"
            id_boton = JsonGet[key].id;
            htmlTags = htmlTags + '<tr>' + '<td>' + JsonGet[key].id + '</td>'
                + '<td>' + JsonGet[key].titulo + '</td>'
                //+ '<td> <a class="btn btn-success" > <span class="fa fa-edit"></span></a >'
                + '<td><a id="btnPublicar' + id_boton + '" class="btn btn-' + class_activa + '" data-toggle="tooltip" data-placement="top" title="No Publicar Listas"><span class="fa fa-eye"></span></a></td>'
                + '<td><a id="btnEditar' + id_boton + '" class="btn btn-warning" data-toggle="modal" data-placement="top" title="Editar"><span class="fa fa-pencil"></span></a></td>'
                + '</tr>';
        })
        $('#tabla-json tbody').append(htmlTags);
    }
    $('#tabla-json tbody').on('click', 'a.btn-success,a.btn-info', function (event) {
        var $table = $('#tabla-json').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        Publicar(data);
    });

    $('#tabla-json tbody').on('click', 'a.btn-warning', function (event) {
        var $table = $('#tabla-json').DataTable();
        var $row = $(this).parents("tr");
        datas = $table.row($row).data();
        EditarD(datas);

    });


    function EditarD(datas) {
        $("#btnEnviar").hide();
        $("#btnEnviarEditar").show();
        $("#btnCancelar").show();
        $("#titulotxts").show();
        $("#urlcargada").show();
        $("#copiar").show();

        Object.keys(JsonGet).forEach(function (key) {
            if (JsonGet[key].id == datas[0]) {
                $("#titulo").val(JsonGet[key].titulo);
                $("#urlcargada").val(JsonGet[key].link);
                valor = JsonGet[key].activa;
                if (valor == true) {
                    $('#cbxPublicar').prop('checked', true);
                } else {
                    $('#cbxPublicar').prop('checked', false);
                }
                
            }
        })
    }

    $("#btnCancelar").on('click', function (event) {
        $("#btnEnviar").show();
        $("#btnEnviarEditar").hide();
        $("#btnCancelar").hide();
        $("#urlcargada").hide();
        $("#copiar").hide();
        $("#titulotxts").hide();
        $("#titulo").val("");
        $("#urlcargada").val("");
        $("#urlvideo").val("");
        $('#cbxPublicar').prop('checked', false);
        
    })

    $("#btnEnviarEditar").on('click', function (event) {
        var Json;
        Object.keys(JsonGet).forEach(function (key) {
            if (JsonGet[key].id == datas[0]) {
                JsonGet[key].titulo = $("#titulo").val().trim();
                //if ($("#urlcargada").val().trim() != "" && $("#urlvideo").val().trim() == "") {
                //    alert($("#dropzone_archivo").val().trim());
                //} else {
                    JsonGet[key].link = urlContenido;
                    var tipo = $('input[name=rbtPublicacion]:checked').val();
                    if (tipo == 'U')
                        urlContenido = $("#urlvideo").val().trim();
                    JsonGet[key].link = urlContenido;
                //}

                if ($("#cbxPublicar").is(':checked'))
                    JsonGet[key].activa = true;
                else
                    JsonGet[key].activa = false;
                Json = JsonGet[key];
            }
        })
        Json = JSON.stringify(Json);
        $.ajax({
            url: urlServer + '/api/Links_Interes/' + datas[0],
            type: 'PUT',
            contentType: 'application/json',
            dataType: 'json',
            data: Json,
        })
            .done(function (datas) {
                    MensajesToastr("success", "Se ha Editado", "Edición exitosa");

            })
            .fail(function (datas) {
                MensajesToastrErrorConexion();
            }).always(function () {

            });

        $("#btnEnviar").show();
        $("#btnEnviarEditar").hide();
        $("#btnCancelar").hide();
        $("#urlcargada").hide();
        $("#copiar").hide();
        $("#titulotxts").hide();
        $("#titulo").val("");
        $("#urlcargada").val("");
        $("#urlvideo").val("");
        $('#cbxPublicar').prop('checked', false);
    })

    function copyToClipboard(elemento) {
        var $temp = $("<input>")
        $("body").append($temp);
        $temp.val($(elemento).text()).select();
        document.execCommand("copy");
        $temp.remove();
    }

    function Publicar(data) {
        var Json;
        var valor;

        Object.keys(JsonGet).forEach(function (key) {
            if (JsonGet[key].id == data[0]) {
                JsonGet[key].activa = !JsonGet[key].activa;
                valor = JsonGet[key].activa;
                Json = JsonGet[key];
                if ($('#btnPublicar' + JsonGet[key].id).hasClass('btn-success')) {
                    $('#btnPublicar' + JsonGet[key].id).addClass('btn-info');
                    $('#btnPublicar' + JsonGet[key].id).removeClass('btn-success');
                }
                else {
                    $('#btnPublicar' + JsonGet[key].id).addClass('btn-success');
                    $('#btnPublicar' + JsonGet[key].id).removeClass('btn-info');
                }
            }
        })

        Json = JSON.stringify(Json);
        $.ajax({
            url: urlServer + '/api/Links_Interes/' + data[0],
            type: 'PUT',
            contentType: 'application/json',
            dataType: 'json',
            data: Json,
        })
            .done(function (data) {
                if (valor)
                    MensajesToastr("success", "Se ha publicado el Sitio de Interes", "Publicación exitosa");
                else
                    MensajesToastr("success", "Se bloqueado el Sitio de Interes", "Desactivación exitosa");

            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {

            });

    }


    $(document).on('click', function (event) {
        Object.keys(JsonGet).forEach(function (key) {
            var id_boto = JsonGet[key].id;
            if (JsonGet[key].id == id_boto) {
                $("select.btnPrio" + id_boto).change(function () {
                    var selectedPrio = $(this).children("option:selected").text();
                    cambioPrioridad(selectedPrio, id_boto);

                });
            }
        })
    });


    $(document).ready(function () {
        $("#form input").keyup(function () {
            var form = $(this).parents("#form");
            var check = checkCampos(form);
            if (check) {
                $("#btnEnviar").prop("disabled", false);
            }
            else {
                $("#btnEnviar").prop("disabled", true);
            }
        });
    });

    function checkCampos(obj) {
        var camposRellenados = true;
        var tipo = $('input[name=rbtPublicacion]:checked').val();
        obj.find("input").each(function () {
            var $this = $(this);
            if ($this.val().length <= 0) {
                camposRellenados = false;
                return false;
            }
        });
        if (camposRellenados == false) {
            if (tipo == "A") {
                return true;
            } else {
                return false;
            }

        }
        else {
            if (tipo == "U") {
                return true;
            } else {
                return true;
            }

        }
    }

})();
