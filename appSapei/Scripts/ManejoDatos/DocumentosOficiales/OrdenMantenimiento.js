$(document).ready(function () {
    QuitaBuscar();
});

function displaySection() {
    $("#vistaPreviaSolicitud").modal("show");
    $("#divCargando").hide();
    $("#divPDF").show();
}

var folioGlobal;
var numFirma;
var areaSolicitada;
var tipoServicio;
var asignado;
var trabajoRealizado;

$(".verDocumento").click(function (event) {
    folioGlobal = $(this).parents("tr").find("td")[1].innerHTML;
    areaSolicitada = $(this).parents("tr").find("td")[3].innerHTML;
    $("#btnFirmarSolicitud").hide();
    var periodo = $(this).parents("tr").find("td")[0].innerHTML;
    var folio = $(this).parents("tr").find("td")[1].innerHTML;
    var estatus = $(this).parents("tr").find("td")[4].innerHTML;

    console.log(estatus);

    if (estatus == 'ACEPTADO') {
        $('.btnAsignar').hide();
        $('.btnLiberar').show();
        $('.btnFirmarOrden').hide();
    }
    if (estatus == 'PENDIENTE') {
        $('.btnAsignar').show();
        $('.btnLiberar').hide();
        $('.btnFirmarOrden').hide();
    }
    estatus = '';
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../MantenimientoEquipos/RegresaSolicitud', { psValor: "2", psPeriodo: periodo, psFolio: folio }, displaySection);
    event.preventDefault();
})

$(".btnAsignar").click(function (event) {
    numFirma = 1;
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})

$("#btnEnviarFirmar").click(function () {
    
    var firma = md5($("#txtFirma").val());
    if (numFirma = 1) {
        $.ajax({
            url: '../../../MantenimientoEquipos/FirmaSolicitudMantenimientoAceptado',
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
        $("#nuevaSolicitud").show();
        $("#divnuevasolicitud").hide();
    }
    if (numFirma = 2) {
        if ($('#checkInterno').prop('checked')) {
            checkSelected = $("#checkInterno").val();
        } else if ($('#checkExterno').prop('checked')) {
            checkSelected = $("#checkExterno").val();
        }


        $.ajax({
            url: '../../../MantenimientoEquipos/FirmaOrdenMantenimientoLiberada',
            type: 'POST',
            dataType: 'json',
            data: {
                psFirma: firma,
                psFolio: folioGlobal,
                psAreaSolicitada: areaSolicitada,
                psTipoMantenimiento: checkSelected,
                psTipoServicio: tipoServicio,
                psAsignado: asignado,
                psTrabajoRealizado: trabajoRealizado
            },
        })
            .done(function (data) {
                if (typeof (data.Success) == "undefined") {
                    MensajesToastr("info", "Orden Procesada", "Error de orden");
                }
                else if (typeof (data.Success) == false) {
                    MensajesToastr("info", "Orden Procesada", data.Mensaje);
                }
                else {
                    MensajesToastr("success", "Orden Procesada", data.Mensaje);
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            }).always(function () {
            });
    }

    event.preventDefault();
})


$("#btnVisualizarOrden").click(function Nuevo(evento) {
    tipoServicio = $("#tipo_servicio").val();
    asignado = $("#asignado_a").val();
    trabajoRealizado = $("#txtTrabajoRealizado").val();

    $('.btnAsignar').hide();
    $('.btnLiberar').hide();
    $('.btnFirmarOrden').show();
    $("#btnFirmarSolicitud").show();
    var checkSelected;
    if ($('#checkInterno').prop('checked')) {
        checkSelected = $("#checkInterno").val();
    } else if ($('#checkExterno').prop('checked')) {
        checkSelected = $("#checkExterno").val();
    } else {
        MensajesToastr("error", "Solicitud Procesada", "Debe seleccionar alguna área a solicitar");
        return;
    }

    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../MantenimientoEquipos/RegresaOrdenPreview', { psValor: "1", psFolio: folioGlobal, psTipo_Mantenimiento: checkSelected, psTrabajoRealizado: $("#txtTrabajoRealizado").val(), psTipo_Servicio: $("#tipo_servicio").val(), psAsignado: $("#asignado_a").val() }, displaySection);
    event.preventDefault();
})

$('.btnFirmarOrden').click(function () {
    numFirma = 2;
    $('#divModalFirmaElectronica').modal('show');
    event.preventDefault();
})

$(".verDocumentoOrden").click(function (event) {
    folioGlobal = $(this).parents("tr").find("td")[1].innerHTML;
    var periodo = $(this).parents("tr").find("td")[0].innerHTML;
    var estatus = $(this).parents("tr").find("td")[4].innerHTML;

    if (estatus == 'LIBERADO') {
        $('.btnAsignar').hide();
        $('.btnLiberar').hide();
        $('.btnFirmarOrden').hide();
    }
    if (estatus == 'FINALIZADO') {
        $('.btnAsignar').hide();
        $('.btnLiberar').hide();
        $('.btnFirmarOrden').hide();
    }
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../MantenimientoEquipos/RegresaOrden', { psValor: "2", psPeriodo: periodo, psFolio: folioGlobal }, displaySection);
    event.preventDefault();
})