//function TemporizadorAlert(duracion) {
//     $(window).scrollTop(0);
//     setTimeout(function () {
//          if ($(".alert").hasClass('alert-danger')) {
//               $(".alert").removeClass('alert-danger');
//          } else if ($(".alert").hasClass('alert-warning')) {
//               $(".alert").removeClass('alert-warning');
//          } else if ($(".alert").hasClass('alert-info')) {
//               $(".alert").removeClass('alert-info');
//          } else {
//               $(".alert").removeClass('alert-success');
//          }
//          $(".alert strong").remove();
//          $(".alert").hide();
//     }, duracion);
//}

function MensajesToastr(tipo, titulo, mensaje)
{
     Command: toastr[tipo.trim()](mensaje, titulo)
}
function MensajesToastrErrorConexion() {
     Command: toastr["error"]("No hay conexión con el servidor", "Verifica tu conexion")
}
function MensajesToastrValidacion(mensaje) {
     Command: toastr["info"]("Validación de campo", mensaje)
}

//function MensajeOptionToastr()
//{
//    toastr.options = {
//        "closeButton": false,
//        "debug": false,
//        "newestOnTop": false,
//        "progressBar": false,
//        "positionClass": "toast-top-center",
//        "preventDuplicates": true,
//        "hideDuration": "10000",
//        "timeOut": "50000",
//        "extendedTimeOut": "10000",
//        "showEasing": "swing",
//        "hideEasing": "linear",
//        "showMethod": "fadeIn",
//        "hideMethod": "fadeOut"
//    }

//    Command: toastr["warning"]('<div>¿Está seguro que desea cerrar el ticket?</div><div align="center"><button type="button" id="okBtn" class="btn btn-primary" onclick="cirreticket();">SI</button> &nbsp; <button type="button" id="surpriseBtn" class="btn btn-danger">NO</button></div>')

//    toastr.options = {
//        "closeButton": false,
//        "debug": false,
//        "newestOnTop": false,
//        "progressBar": false,
//        "positionClass": "toast-top-right",
//        "preventDuplicates": false,
//        "onclick": null,
//        "showDuration": "300",
//        "hideDuration": "1000",
//        "timeOut": "5000",
//        "extendedTimeOut": "1000",
//        "showEasing": "swing",
//        "hideEasing": "linear",
//        "showMethod": "fadeIn",
//        "hideMethod": "fadeOut"
//    }
        
//}