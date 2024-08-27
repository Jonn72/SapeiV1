$(document).ready(function () {
    var tipo = $("#hidTipoAutorizacion").val();
    var comboTipo;
    if (tipo.toLowerCase().indexOf("verano") !== -1)
        comboTipo = 2;
    else
        comboTipo = 1;
    CargaComboPeriodo(2, comboTipo);
    $("#txtNoControl").focus();
});
$('#txtLineaCaptura').on('keypress', function (e) {
    if (e.which === 13) {
        CargaDatos();
    }
});
$("#btnAnterior").click(function Atras(evento) {
    event.preventDefault();
    LimpiaControles();
})
function CargaDatos() {
    var control = $("#txtNoControl").val();
    var periodo = $('#cboPeriodo').val();
    var linea_captura = $("#txtLineaCaptura").val();
    if (!ValidaCampo($("#txtNoControl"), 'NOCONTROL')) {
        MensajesToastr("info", "Solicitud Procesada", "Ingrese una no. de control valido");
        return;
    }
    $.ajax({
        url: '../../../Financieros/CargaDatosPagoServicio',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: control, psPeriodo: periodo, psLineaCaptura: linea_captura },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && data.Success == false) {
                MensajesToastr("info", "Solicitud Procesada", data.Mensaje);
            }
            else {
                var resultado = JSON.parse(data);
                var valor = 0;
                $("#divCombo").hide();
                $("#divDatos").show();
                $("#txtNoControl1").val($("#txtNoControl").val());
                $("#txtNombre").val(resultado.data[0].nombre);
                $('#txtMonto').val(resultado.data[0].monto);
                $('#txtConcepto').val(resultado.data[0].concepto);
                valor = resultado.data[0].pago_registrado;
                if (valor !== null && valor == true) {
                    MensajesToastr("info", "Solicitud Procesada", "El estudiante ya tiene autorizado este periodo");
                    $("#btnGuardar").prop("disabled", true);
                }
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}
$("#btnGuardar").click(function (event) {
    event.preventDefault();
    var noControl = $("#txtNoControl").val();
    var periodo = $('#cboPeriodo').val();
    var linea_captura = $('#txtLineaCaptura').val(); 

    $.ajax({
        url: '../../../Financieros/GuardaPagoServicio',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: noControl, psPeriodo: periodo, psLineaCaptura: linea_captura },
    })
        .done(function (data) {

            if (!data.Success) {
                MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
                LimpiaControles();
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        })
        .always(function () {
        });
})

function LimpiaControles() {
    $("#divCombo").show();
    $("#divDatos").hide();
    $("#txtNoControl").val('');
    $("#txtLineaCaptura").val('');
    $("#txtNombre").val('');
    $("#btnGuardar").prop("disabled", false);
    $("#txtNoControl").focus();
}

