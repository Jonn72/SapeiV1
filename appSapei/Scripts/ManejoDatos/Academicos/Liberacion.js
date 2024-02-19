var periodo;
$(document).ready(function () {
});

$("#btnGenerar").on('click', function (evento) {
     event.preventDefault();
     var promedio = $("#txtPromedio").val();
     var control = $("#txtNoControl").val();
     var vobo = $("#txtVoBo").val();
     if (!ex_no_control.test(control)) {
          MensajesToastr("info", "Solicitud Procesada", "No. de control incorrecto");
          return;
     }
     if (promedio.length == 0 || 1 > promedio || promedio > 4  ) {
          MensajesToastr("info", "Solicitud Procesada", "El promedio debe ser de 1 a 4");
          return;
     }
     $("#divVisor").show();
     $("#divCargando").show();
     $("#divPDF").hide();
     $('#divPDF').load('../../../../DocumentosOficiales/LiberacionCredito', {psControl: control, psPromedio:promedio, psVoBo:vobo}, displaySection);
})
function displaySection() {
     $("#divCargando").hide();
     $("#divPDF").show();
}