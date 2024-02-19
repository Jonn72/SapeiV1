$(document).ready(function () {
     DesactivaBotones();
     QuitaBuscar();
     $('#input-id').fileinput('reset');
});
$('#txtFile').fileinput({
     uploadAsync: false,
     language: 'es',
     showPreview: true,
     showCaption: true,
     showUpload: true,
     minFileCount: 1,
     maxFileCount: 1,
     uploadUrl: '../../../Administracion/LogosJson',
     allowedFileExtensions: ['jpg', 'png', 'bmp'],
     uploadExtraData: function() { return {piId: $("#hidId").val(), psDescripcion: $("#txtDescripcion").val()}; }
});
$("#btnNuevo").click(function Atras(evento) {
     $('#input-id').fileinput('reset');
     $("#txtDescripcion").val("")
})
$('#txtFile').on('filebatchuploadsuccess', function (event, data) {
     var form = data.form, files = data.files, extra = data.extra,
         response = data.response, reader = data.reader;
     if (response.Success == true) {
          MensajesToastr("info", "Solicitud Procesada", "El logo se guardo correctamente");
          $('#BodyPrincipal').load('../../../../Administracion/Logos');
     }
     else {
          MensajesToastr("info", "Solicitud Procesada", response.Mensaje);
     }
     $('#txtFile').fileinput('reset');
});

$('#txtFile').on('filebatchuploaderror', function (event, data, msg) {
     MensajesToastr("info", "Solicitud Procesada", "El archivo no tiene el formato correcto");
});


