(function () {
    var JsonGet;
    var urlBanner;
    var urlContenido;
    var urlServer;
    var datas;
    var hostname = location.hostname;
    switch (hostname) {
        case "localhost":
            urlServer = "https://tlahuac.tecnm.mx";
            //urlServer = "http://localhost:57878";
            break;
        case "sapei.tlahuac.tecnm.mx":
            urlServer = "https://tlahuac.tecnm.mx";
            break;
        case "192.168.9.245":
            urlServer = "https://192.168.9.246";
            break;
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
        url: urlServer + "/api/Convocatorias",
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
           //htmlHead = htmlHead + "<th>Editar</th>";
            htmlHead = htmlHead + '</tr>';
            CargarTablaConvocatorias();
            $('#tabla-json thead').append(htmlHead);

        },
        error: function (error) {

        }
    });


});

function CargarTablaConvocatorias() {
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
            //+ '<td><a id="btnEditar' + id_boton + '" class="btn btn-warning" data-toggle="modal" data-placement="top" title="Editar"><span class="fa fa-pencil"></span></a></td>'
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
            url: urlServer + '/api/Convocatorias/' + data[0],
            type: 'PUT',
            contentType: 'application/json',
            dataType: 'json',
            data: Json,
        })
            .done(function (data) {
                if (valor)
                    MensajesToastr("success", "Se ha publicado la Convocatoria", "Publicación exitosa");
                else
                    MensajesToastr("success", "Se bloqueado el la Convocatoria", "Desactivación exitosa");

            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {

            });

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
        obj.urlimagen = urlBanner;
        var tipo = $('input[name=rbtPublicacion]:checked').val();
        if (tipo == 'U')
            urlContenido = $("#urlvideo").val().trim();
        obj.urlarchivo = urlContenido;
        obj.fechalimite = $("#txtFechaLimite").val();
        if ($("#cbxPublicar").is(':checked'))
            obj.activa = true;
        else
            obj.activa = false;
        var urlServerPost = urlServer + "/api/Convocatorias";
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
                MensajesToastr("success", "Se ha publicado la Convocatoria", "Publicación exitosa");
                //LimpiaControles();
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            })
            .always(function () {

            });
    });

$('#dtpFechaLimite').datetimepicker({
    viewMode: 'years',
    format: 'YYYY/MM/DD',
    locale: 'es',
    defaultDate: new Date()
});
})();

const hoy = new Date();

function formatoFecha(fecha, formato) {
    //
}

formatoFecha(hoy, 'yyyy/mm/dd');

function verificarHora() {


}