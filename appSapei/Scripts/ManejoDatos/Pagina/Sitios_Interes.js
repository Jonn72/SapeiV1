(function () {
    var JsonGet;
    var urlImagen;
    var urlSitio;
    var urlServer;
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
            urlServer = "http://192.168.9.246";
            break;
    }

    $("#divArchivo").show();
    $("#divUrl").hide();
    $("#banners").hide();
    Dropzone.autoDiscover = false;
    $("#dropzone_imagen").dropzone({
        maxFilesize: 400,
        maxFiles: 1,
        dictMaxFilesExceeded: 'No se pueden subir más archivos, Limite superado!.',
        dictInvalidFileType: 'Formato de archivo no permitido',
        dictFallbackMessage: 'Tu navegador no soporta esta funcion.',
        acceptedFiles: '.jpeg,.jpg,.png,.JPEG,.JPG,.PNG',
        renameFile: function (file) {
            return 'sitio_interes.' + file.name.split(".")[1];
        },
        init: function () {
            this.on("processing", function (file) {
                this.options.url = urlServer + "/api/UploadImages";
            });
            this.on('success', function (file, response) {
                urlImagen = response;
            })
        }
    });
    $("#btnPublicarSitio").on('click', function (event) {
        event.preventDefault();
        var obj = new Object();
        if (urlImagen == null) {
            MensajesToastr("warning", "Debe seleccionar una imagen", "Vuelve a intentarlo");
            return;
        }
        obj.titulo = $("#titulo").val().trim();
        obj.imagen = urlImagen;
        urlSitio = $("#urlsitio").val().trim();
        obj.link = urlSitio;
        if ($("#cbxPublicar").is(':checked'))
            obj.activa = true;
        else
            obj.activa = false;
        var urlServerPost = urlServer + "/api/Sitios_Interes";
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
                MensajesToastr("success", "Se ha publicado el sitio de interes", "Publicación exitosa");
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
        urlImagen = "";
        urlSitio = "";
    }
    function CargaNuevoSitioInteresTabla(data, nuevo) {
        var obj = JSON.parse(JsonGet);
        alert(JSON.stringify(data))
        obj.push(JSON.stringify(data));
    }


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
            url: urlServer + "/api/Sitios_Interes",
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
                htmlHead = htmlHead + '</tr>';
                CargarTablaSitios();
                $('#tabla-json thead').append(htmlHead);

            },
            error: function (error) {

            }
        });


    });


    function CargarTablaSitios() {
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
                + '<td><a id="btnPublicarSitio' + id_boton + '" class="btn btn-' + class_activa + '" data-toggle="tooltip" data-placement="top" title="No Publicar Listas"><span class="fa fa-eye"></span></a></td>'
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
                if ($('#btnPublicarSitio' + JsonGet[key].id).hasClass('btn-success')) {
                    $('#btnPublicarSitio' + JsonGet[key].id).addClass('btn-info');
                    $('#btnPublicarSitio' + JsonGet[key].id).removeClass('btn-success');
                }
                else {
                    $('#btnPublicarSitio' + JsonGet[key].id).addClass('btn-success');
                    $('#btnPublicarSitio' + JsonGet[key].id).removeClass('btn-info');
                }
            }
        })

        Json = JSON.stringify(Json);
        $.ajax({
            url: urlServer + '/api/Sitios_Interes/' + data[0],
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
    
})();
