(function () {
    var urlBanner;
    var urlContenido;
    var urlEncabezado = new Array();
    var n;
    var o = 0;

    var urlServer;
    var hostname = location.hostname;

    switch (hostname) {
        case "localhost":
            urlServer = "http://192.168.9.246";
            break;
        case "sapei.tlahuac.tecnm.mx":
            urlServer = "https://tlahuac.tecnm.mx";
            break;
        case "192.168.9.245":
            urlServer = "https://192.168.9.246";
            break;
    }
    $("#tbody").jPut({
        ajax_url: urlServer + "/api/Noticia",
        name: "tbody_template",
        done: function (data) {
            JsonGet = data;
            var htmlHead = "<tr>";
            htmlHead = htmlHead + "<th>No.</th>";
            htmlHead = htmlHead + "<th>Titulo</th>";
            htmlHead = htmlHead + "<th>Acciones</th>";
            htmlHead = htmlHead + '</tr>';
            CargarTablaBanners();
            $('#tabla-json thead').append(htmlHead);
        },
        error: function (msg) {
            alert('Error Message:' + msg);    //On error
        }
    });
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
            return 'noticia.' + file.name.split(".")[1];
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
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG',
        renameFile: function (file) {
            return 'noticia.' + file.name.split(".")[1];
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
    $("#dropzone_encabezado").dropzone({
        maxFilesize: 10,
        maxFiles: 10,
        parallelUploads: 10,
        url: urlServer,
        dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
        dictInvalidFileType: 'Formato de archivo no permitido',
        dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG',
        renameFile: function (file) {
            return 'noticia.' + file.name.split(".")[1];
        },
        init: function () {
            this.on("processing", function (file) {

                this.options.url = urlServer + "/api/UploadImages";
            });
            this.on('success', function (file, response) {
                urlEncabezado[o] = response;
                //alert(urlEncabezado[o]);
                o++;
                var MyDropzone = this;
                n = MyDropzone.files.length;
            })
        }
    });
    $("#btnEnviar").on('click', function (event) {
        event.preventDefault();
        var obj = new Object();

        var ult;
        alert("send");
        obj.titulo = $("#titulo").val().trim();
        obj.cuerpo = $("#cuerpo").val().trim();
        obj.fechapublicacion = new Date();
        obj.urlimagen = urlBanner;
        obj.urlimagenencabezado = urlContenido;
        var tipo = $('input[name=rbtPublicacion]:checked').val();
        if (tipo == 'U')
            urlContenido = $("#urlvideo").val().trim();
        if ($("#cbxPublicar").is(':checked'))
            obj.activa = true;
        else
            obj.activa = false;
        obj.activait = false;
        var urlServerPost = urlServer + "/api/Noticia";
        obj = JSON.stringify(obj);
        $.ajax({
            url: urlServerPost,
            type: 'POST',
            async: true,
            contentType: "application/json; charset=utf-8", // this
            dataType: "json", // and this
            crossDomain: true,
            contentType: 'application/json',
            dataType: 'json',
            data: obj,

        })
            .done(function (data) {
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            })
            .always(function () {

            });

        Object.keys(JsonGet).forEach(function (key) {
            ult = JsonGet[key].id + 1;
        })
        // for (var i = 0; i < n; i++) {

        function enviar(ims) {
            var obj2 = new Object();
            if (ims < n) {
                obj2.urlimagen = urlEncabezado[ims];
                var urlServerPost2 = urlServer + "/api/Galerias";
                obj2.idnoticia = parseInt(ult);
                obj2 = JSON.stringify(obj2);
                $.ajax({
                    url: urlServerPost2,
                    type: 'POST',
                    async: true,
                    crossDomain: true,
                    contentType: 'application/json',
                    dataType: 'json',
                    data: obj2,
                })
                    .done(function (data) {
                        ob2 = null;
                        enviar(ims + 1);
                    })
                    .fail(function (data) {
                        MensajesToastrErrorConexion();
                    })
                    .always(function () {

                    });
            }
        }
        enviar(0);
        // }

    });

    function LimpiaControles() {
        $("#titulo").val("");
        urlBanner = "";
        urlContenido = "";
    }

    $('input[type=radio][name=rbtPublicacion]').change(function () {

        var tipo = $('input[name=rbtPublicacion]:checked').val();
        $("#divArchivo").show();

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
            url: urlServer + '/api/Noticia/' + data[0],
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
})();



