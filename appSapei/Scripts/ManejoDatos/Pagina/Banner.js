(function () {
    var JsonGet;
    var urlBanner;
    var urlContenido;
    var prioridad;
    var urlServer;
    var object;
    var hostname = location.hostname;
    switch (hostname) {
        case "localhost":
            urlServer = "http://192.168.9.246";
            //urlServer = "http://localhost:57878"; 
            break;
        case "sapei.tlahuac.tecnm.mx":
            urlServer =  "https://tlahuac.tecnm.mx";
            break;
        case "192.168.9.245":
            urlServer = "http://192.168.9.246";
            break;
    }

    $("#divArchivo").show();
    $("#divUrl").hide();
    $("#banners").hide();
    Dropzone.autoDiscover = false;
    $("#dropzone_banner").dropzone({
        maxFilesize: 10,
        maxFiles: 1,
        dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
        dictInvalidFileType: 'Formato de archivo no permitido',
        dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG',
        renameFile: function (file) {
            return 'banner.' + file.name.split(".")[1];
        }, 
        init: function () {
            this.on("processing", function (file) {
                this.options.url = urlServer + "/api/UploadImages";
            });
            this.on('success', function (file, response) {
                urlBanner = response;
            })
        }
    });
    $("#dropzone_archivo").dropzone({
        maxFilesize: 10,
        maxFiles: 1,
        url: urlServer,
        dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
        dictInvalidFileType: 'Formato de archivo no permitido',
        dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG,.PDF,.pdf,.rar',
        renameFile: function (file) {
            return 'banner.' + file.name.split(".")[1];
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
        obj.fechapublicacion = new Date();
        obj.urlimagen = urlBanner;
        var tipo = $('input[name=rbtPublicacion]:checked').val();
        if (tipo == 'U')
            urlContenido = $("#urlvideo").val().trim();
        obj.urlcontenido = urlContenido;
        var prio = $('input[name=rbtPrioridad]:checked').val();
        if (prio == '1')
            prioridad = 1;
        if (prio == '2')
            prioridad = 2;
        if (prio == '3')
            prioridad = 3;
        if (prio == '4')
            prioridad = 4;
        obj.prioridad = prioridad;
        if ($("#cbxPublicar").is(':checked'))
            obj.activa = true;
        else
            obj.activa = false;
        var urlServerPost = urlServer + "/api/Banners";
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
            url: urlServer + "/api/Banners",
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
                htmlHead = htmlHead + "<th>Prioridad</th>";
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
        var Vprioridad = new Array(3);
        Object.keys(JsonGet).forEach(function (key) {
            class_activa = "success";
            if (JsonGet[key].activa)
                class_activa = "info"
            id_boton = JsonGet[key].id;
            if (JsonGet[key].prioridad == 4)
            {
                Vprioridad[0] = "Muy Alta"
                Vprioridad[1] = "Alta"
                Vprioridad[2] = "Media"
                Vprioridad[3] = "Baja"
            }
            if (JsonGet[key].prioridad == 3) {
                Vprioridad[0] = "Alta"
                Vprioridad[1] = "Muy alta"
                Vprioridad[2] = "Media"
                Vprioridad[3] = "Baja"
            }
            if (JsonGet[key].prioridad == 2) {
                Vprioridad[0] = "Media"
                Vprioridad[1] = "Muy alta"
                Vprioridad[2] = "Alta"
                Vprioridad[3] = "Baja"
            }
            if (JsonGet[key].prioridad == 1) {
                Vprioridad[0] = "Baja"
                Vprioridad[1] = "Muy alta"
                Vprioridad[2] = "Alta"
                Vprioridad[3] = "Media"
            }
            htmlTags = htmlTags + '<tr>' + '<td>' + JsonGet[key].id + '</td>'
                + '<td>' + JsonGet[key].titulo + '</td>'
                //+ '<td> <a class="btn btn-success" > <span class="fa fa-edit"></span></a >'
                + '<td><a id="btnPublicar' + id_boton + '" class="btn btn-' + class_activa + '" data-toggle="tooltip" data-placement="top" title="No Publicar Listas"><span class="fa fa-eye"></span></a></td>'
                + '<td>' + ' <form> <select class="btnPrio'+id_boton+'" name="btnPrioridad" id="btnPrioridad' + id_boton +'"><option value="1">' + Vprioridad[0] + '</option> <option value="2">' + Vprioridad[1] + '</option></option> <option value="3">' + Vprioridad[2] + '</option> <option value="4">' + Vprioridad[3] + '</option></select></form>' + '</td>' 
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
            url: urlServer + '/api/Banners/' + data[0],
            type: 'PUT',
            contentType: 'application/json',
            dataType: 'json',
            data: Json,
        })
            .done(function (data) {
                if (valor) 
                    MensajesToastr("success", "Se ha publicado el Banner", "Publicación exitosa");
                else
                    MensajesToastr("success", "Se bloqueado el Banner", "Desactivación exitosa");

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

    function cambioPrioridad(val,ids) {
        var Json;
        //var opcion = document.getElementById("btnPrioridad" + JsonGet[key].id);
        //var seleccion = opcion.options[opcion.selectedIndex].text;
        var prio;
        Object.keys(JsonGet).forEach(function (key) {
            if (JsonGet[key].id == ids) {
                if (val == "Baja")
                    prio = 1;
                if (val == "Media")
                    prio = 2;
                if (val == "Alta")
                    prio = 3;
                if (val == "Muy alta")
                    prio = 4;
                JsonGet[key].prioridad = prio;
                valor = JsonGet[key].prioridad;
                Json = JsonGet[key];
            }
        })

        Json = JSON.stringify(Json);
        $.ajax({
            url: urlServer + '/api/Banners/' + ids,
            type: 'PUT',
            contentType: 'application/json',
            dataType: 'json',
            data: Json,
        })
            .done(function (data) {
                    MensajesToastr("success", "Se ha cambiado la prioridad", "Cambio hecho");
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {

            });
    }

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
