$(document).ready(function () {
    QuitaBuscar();
});

$("#nuevaSolicitud").click(function (event) {
    var paso = 0;
    if (paso == 0) {
        var json = {}
        $.ajax({
            asyn: false,
            url: '../../../../MantenimientoEquipos/Contador',
            type: 'POST',
            dataType: 'json',
            data: json,
        }).done(function (data) {
            if (typeof (data.Success) !== "undefined") {
                MensajesToastr("error", "Solicitud Procesada", "Error, no se pudo cargar la materia");
            }
            else {
                console.log(data);
                var r = JSON.parse(data);
                if (r.data[0].cuenta <= 3) {
                    $("#nuevaSolicitud").hide();
                    $("#divnuevasolicitud").show();
                } else {
                    MensajesToastr("info", "Solicitud Procesada", "Error, no se puden enviar más solicitudes, hay 3 activas");

                }
            }
        }).fail(function (data) {
            MensajesToastrErrorConexion();
        });
        event.preventDefault();

        limpiar();
    }
})


$("#cancelarSolicitud").click(function (event) {
    $("#nuevaSolicitud").show();
    $("#divnuevasolicitud").hide();
})

$("#revisarSolicitud").click(function Nuevo(evento) {
    $("#btnFirmarSolicitud").show();
    var checkSelected;
    if ($('#checkRecursosMateriales').prop('checked')) {
        checkSelected = $("#checkRecursosMateriales").val();
    } else if ($('#checkMantenimietoEquipo').prop('checked')) {
        checkSelected = $("#checkMantenimietoEquipo").val();
    } else if ($('#checkCentroConputo').prop('checked')) {
        checkSelected = $("#checkCentroConputo").val();
    } else {
        MensajesToastr("error", "Solicitud Procesada", "Debe seleccionar alguna área a solicitar");
        return;
    }
    $("#btnFirmarOrden").hide();
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../MantenimientoEquipos/RegresaSolicitudPrevia', { psValor: "1", psTipo_solicitud: checkSelected, psDescripcion: $("#txtdescripcion").val()}, displaySection);
    event.preventDefault();
})

function displaySection() {
    $("#vistaPreviaSolicitud").modal("show");
    $("#divCargando").hide();
    $("#divPDF").show();
}
var band;
$("#btnFirmarSolicitud").click(function () {
    band = 1;
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})
$("#btnEnviarFirmar").click(function () {
    var firma = md5($("#txtFirma").val());
    if (band == 1) {
        var checkSelected;
        if ($('#checkRecursosMateriales').prop('checked')) {
            checkSelected = $("#checkRecursosMateriales").val();
        } else if ($('#checkMantenimietoEquipo').prop('checked')) {
            checkSelected = $("#checkMantenimietoEquipo").val();
        } else if ($('#checkCentroConputo').prop('checked')) {
            checkSelected = $("#checkCentroConputo").val();
        }
        var descripcion = $("#txtdescripcion").val();
        $.ajax({
            url: '../../../MantenimientoEquipos/FirmaSolicitudMantenimiento',
            type: 'POST',
            dataType: 'json',
            data: {
                psFirma: firma,
                psTipo_solicitud: checkSelected,
                psDescripcion: descripcion
            },
        })
            .done(function (data) {
                if (typeof (data.Success) == "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
                }
                else if (typeof (data.Success) == false) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {
            });
        $("#nuevaSolicitud").show();
        $("#divnuevasolicitud").hide();
    }
    if (band == 2) {
        $.ajax({
            url: '../../../MantenimientoEquipos/FirmaOrdenMantenimientoFinalizado',
            type: 'POST',
            dataType: 'json',
            data: {
                psFirma: firma,
                psFolio: folioGlobal
            },
        })
            .done(function (data) {
                if (typeof (data.Success) == "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "Error de solicitud");
                }
                else if (typeof (data.Success) == false) {
                    MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {
            });
    }
    event.preventDefault();
})


function limpiar() {
    $("#txtdescripcion").val("");

}

$(".verDocumento").click(function (event) {
    $("#btnFirmarSolicitud").hide();
    var periodo = $(this).parents("tr").find("td")[0].innerHTML;
    var folio = $(this).parents("tr").find("td")[1].innerHTML;
    var estatus = $(this).parents("tr").find("td")[4].innerHTML;
    if (estatus = "PENDIENTE") {
        $("#btnFirmarSolicitud").hide();
        $("#btnFirmarOrden").hide();
    }
    if (estatus = "LIBERADO") {
        $("#btnFirmarSolicitud").hide();
        $("#btnFirmarOrden").show();
    }
    if (estatus = "FINALIZADO") {
        $("#btnFirmarSolicitud").hide();
        $("#btnFirmarOrden").hide();
    }
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../MantenimientoEquipos/RegresaSolicitud', { psValor: "2", psPeriodo: periodo, psFolio: folio }, displaySections);
    event.preventDefault();
})

function displaySections() {
    $("#vistaPreviaSolicitud").modal("show");
    $("#divCargando").hide();
    $("#divPDF").show();
}
var folioGlobal;
$(".verDocumentoOrden").click(function (event) {
    folioGlobal = $(this).parents("tr").find("td")[1].innerHTML;
    var periodo = $(this).parents("tr").find("td")[0].innerHTML;
    var estatus = $(this).parents("tr").find("td")[3].innerHTML;
    console.log(estatus);
    if (estatus == "LIBERADO") {
        $("#btnFirmarSolicitud").hide();
        $("#btnFirmarOrden").show();
    }
    if (estatus == "FINALIZADO") {
        $("#btnFirmarSolicitud").hide();
        $("#btnFirmarOrden").hide();
    }

    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../MantenimientoEquipos/RegresaOrden', { psValor: "2", psPeriodo: periodo, psFolio: folioGlobal }, displaySection);
    event.preventDefault();
})

$("#btnFirmarOrden").click(function (event) {
    band = 2;
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})