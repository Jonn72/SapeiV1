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


    $("#btnEnviar").on('click', function (event) {
        event.preventDefault();
        var obj = new Object();
        obj.titulo = $("#titulo").val().trim();
        var nmb = $("#urlvideo").val().trim();
        urlSitio = nmb.replace("watch?v=","embed/");
        obj.url = urlSitio;
        if ($("#cbxPublicar").is(':checked'))
            obj.activa = true;
        else
            obj.activa = false;
        var urlServerPost = urlServer + "/api/Videos";
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
                MensajesToastr("success", "Se ha publicado el video", "Publicación exitosa");
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
            url: urlServer + "/api/Videos",
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
            url: urlServer + '/api/Videos/' + data[0],
            type: 'PUT',
            headers: {
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjY2NlZTFhOS0yZGUzLTRhNDctOTA2OC1lNjZhMWFiNzA3NzMiLCJuYW1laWQiOiJiNWQyMzNmMC02ZWMyLTQ5NTAtOGNkNy1mNDRkMTZlYzg3OGYiLCJub21icmUiOiJOb21icmUgVXN1YXJpbyIsImFwZWxsaWRvcyI6IkFwZWxsaWRvcyBVc3VhcmlvIiwiZW1haWwiOiJlbWFpbC51c3VhcmlvQGRvbWluaW8uY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYWRvciIsIm5iZiI6MTYwMzgxMzcwNCwiZXhwIjoxNjAzOTAwMTA0LCJpc3MiOiJodHRwczovL3RsYWh1YWMudGVjbm0ubXgvIiwiYXVkIjoiaHR0cHM6Ly90bGFodWFjLnRlY25tLm14L2FwaS9iYW5uZXJzIn0.unfmL5dE1iwV3FAad3Bb1hBNPByzcJU0OWNhzHuH7o4'
            },
            contentType: 'application/json',
            dataType: 'json',
            data: Json,
        })
            .done(function (data) {
                if (valor)
                    MensajesToastr("success", "Se ha publicado el video", "Publicación exitosa");
                else
                    MensajesToastr("success", "Se bloqueado el video", "Desactivación exitosa");

            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {

            });

    }

})();
