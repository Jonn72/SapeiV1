$(function () {
    $('.modal-backdrop').remove();
    $("#CountDownTimer").TimeCircles({ time: { Days: { show: false }, Hours: { show: false } } });
    $("#CountDownTimer").TimeCircles({ circle_bg_color: "#414141" });

    $("#CountDownTimer").TimeCircles({ count_past_zero: false }).addListener(countdownComplete);

    function countdownComplete(unit, value, total) {
        if (typeof ($.fn.slideToggle) === 'undefined') { return; }
        if ($("#CountDownTimer").length > 0) {
            if (total <= 0) {
                $('#BodyPrincipal').load('../../../Reticula/Seleccion');
            }
        }

    }
    init_compose();
});

function init_compose() {
    if (typeof ($.fn.slideToggle) === 'undefined') { return; }
    $('.compose').slideToggle();
};

$("#btnSeleccionarGrupo").click(function Nuevo(event) {
    if (!$("#divHorarios input[name='radio_grupo']").is(':checked')) {
        MensajesToastr("info", "Solicitud Procesada", "Debe seleccionar algún grupo");
        return;
    }
    var grupo = $('input:radio[name=radio_grupo]:checked').val();
    var control = $("#txtControl").val()
    $('#BodyPrincipal').load('../../../Reticula/SeleccionGrupoJson', { psControl: control, psGrupo: grupo });
    $("#divModalSeleccion").hide();
    event.preventDefault();
});


$('.btn-warning').click(function Nuevo(event) {
    var name = $(this).attr('name');
    var control = $("#txtControl").val()
    var grupo = name.split("-")[0];
    var repeticion = name.split("-")[1];
    var materia = name.split("-")[2];
    var mensaje;
    event.preventDefault();
    if (repeticion === "N")
        mensaje = "Realmente desea eliminar la materia " + materia;
    else if (repeticion === "S")
        mensaje = "Realmente desea eliminar la materia " + materia + " , al eliminar una materia de repetición, se descargan las materias seleccionadas que tienen como requisito esta materia";
    else
        mensaje = "Realmente desea eliminar la materia " + materia + " , al eliminar una materia de especial, se descargan las materias seleccionadas que tienen como requisito esta materia";

    bootbox.confirm(mensaje, function (result) {
        if (result == false)
            return;
        $('#BodyPrincipal').load('../../../Reticula/SeleccionEliminaGrupoJson', { psControl: control, psGrupo: grupo });

    });
});

$("#btnTerminarSeleccion").click(function Nuevo(event) {
    var control = $("#txtControl").val()
    $('#BodyPrincipal').load('../../../Reticula/SeleccionFinalizarJson', { psControl: control });
    event.preventDefault();

});


$("#btnCargaAcademica").click(function () {
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../DocumentosOficiales/RegresaCargaAcademicaFirmaElectronica', displaySection);
    event.preventDefault();
})
$("#btnFirmarCargaAcademica").click(function () {
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})


function displaySection() {
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
    $('#divModalFirmaElectronica').modal('hide');
    $("#btnFirmarCargaAcademica").show();

}
$("#btnEnviarFirmar").click(function () {
    var firma = $("#txtFirma").val();
    $.ajax({
        url: '../../../Estudiante/FirmaCargaAcademica',
        type: 'POST',
        dataType: 'json',
        data: { psFirma: firma },
    })
        .done(function (data) {
            if (typeof (data.Success) == "undefined") {
                MensajesToastr("error", "Solicitud Procesada", "Error de solicitud");
            }
            else if (typeof (data.Success) == false) {
                MensajesToastr("warning", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                $('.modal-backdrop').remove();
                $('#BodyPrincipal').load('../../../../Estudiante/SeleccionMaterias');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        }).always(function () {
        });
    event.preventDefault();
})