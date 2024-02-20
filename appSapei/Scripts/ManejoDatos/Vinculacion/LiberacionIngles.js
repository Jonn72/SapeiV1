$("#btnGuardar2").click(function Nuevo(event) {
    var Periodo = $("#hidPeriodo").val();
    var NoControl = $("#hidNoControl").val();
    var TipoLiberacion = $("#cboTipo_liberacion").val();
    var FechaLiberacion = $("#txtFechaLiberacion").val();
    var Promedio = $("#txtPromedio").val();



    if (!ValidaCampos(Promedio)) {
        MensajesToastr("info", "Solicitud Procesada", "Complete todos los campos");
    }
    else {
        $.ajax({
            asyn: false,
            url: '../../../Vinculacion/ManejoLiberacion',
            type: 'POST',
            dataType: 'json',
            data: { psPeriodo: Periodo, psNoControl: NoControl, psTipoLiberacion: TipoLiberacion, psFechaLiberacion: FechaLiberacion, psPromedio: Promedio },
        })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                LimpiaControles();
                $('#BodyPrincipal').load('../../../../Vinculacion/LiberacionIngles');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    }
    event.preventDefault();
})

function ValidaCampos(Promedio) {

    if (Promedio.length == 0) {
        $("#txtPromedio").get(0).setCustomValidity('Campo Requerido');
        return false;
    }
    return true;
}

function LimpiaControles() {
    $("#divNoControl").show();
    $("#frmLiberacionIngles").hide();
    $("#hidNoControl").val("");
    $("#hidPeriodo").val("");
    $("txtNombreEstudiante").val("");
    $("#txtFechaLiberacion").val("");
    $("#txtPromedio").val("");
}

$('#txtFechaLiberacion').datetimepicker({
    viewMode: 'days',
    format: 'DD/MM/YYYY',
    locale: 'es',
    defaultDate: 'now'
});

$("#btnBuscar").click(function Nuevo(evento) {
    RegresaEstudianteDatos($("#txtNoControl").val());
    evento.preventDefault();
})

function RegresaEstudianteDatos(noControl) {
    if (!esFormatoValidoAlert())
        return
    Cargar(noControl);
}

function esFormatoValidoAlert() {
    var noControl = $("#txtNoControl").val();
    if (!ex_no_control.test(noControl)) {
        MensajesToastr("warning", "Solicitud Procesada", "Ingrese un no. de control valido");
        return false;
    }
    return true;
}

function Cargar(noControl) {
    $.ajax({
        url: '../../../../Vinculacion/CargaLiberacion',
        type: 'POST',
        dataType: 'json',
        data: { psNoControl: noControl },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined" && !data.Success) {
                 MensajesToastr("info", "Solicitud Procesada", "No se encontro el estudiante con no. de control: " + noControl + "");
             }
             else { 
                 var resultado = JSON.parse(data);
                 $("#divNoControl").hide();
                 $("#frmLiberacionIngles").show();
                 $("#txtFechaLiberacion").val(resultado.data[0].fecha_liberacion);
                 $("#hidNoControl").val(resultado.data[0].no_de_control);
                 $("#hidPeriodo").val(resultado.data[0].periodo);
                 $("#txtNombreEstudiante").val(resultado.data[0].nombre);
                 $("#txtPromedio").val(resultado.data[0].promedio_o_puntos);
                 $("#cboTipo_liberacion").val(resultado.data[0].id_liberacion.trim()).change();
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
}

function Regresa(noControl) {
    if (!FormatoValido(noControl))
        return
    Cargar(noControl);
}

function FormatoValido(noControl1) {
    var noControl = noControl1;
    if (!ex_no_control.test(noControl)) {
        MensajesToastr("warning", "Solicitud Procesada", "Ingrese un no. de control valido");
        return false;
    }
    return true;
}
