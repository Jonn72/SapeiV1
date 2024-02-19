$("#btnGuardar").click(function (event) {
    var NoControl = $("#hidNoControl").val();
    var PeriodoEscolar = $("#cboPeriodoEscolar").val();
    var FechaActo = $("#txtFechaActo").val();
    var OpcTitulacion = $("#cboTiposTitulacion").val();


    if (!ValidaCampos(FechaActo)) {
        MensajesToastr("info", "Solicitud Procesada", "Complete todos los campos");
    }
    else {
        $.ajax({
            asyn: false,
            url: '../../../ServiciosEscolares/ManejoTitulados',
            type: 'POST',
            dataType: 'json',
            data: { psNoControl: NoControl, psPeriodo: PeriodoEscolar, psFechaActo: FechaActo, psTipoTitulacion: OpcTitulacion},
        })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                LimpiaControles();
                $('#BodyPrincipal').load('../../../../ServiciosEscolares/AlumnosTitulados');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    }
    event.preventDefault();
})

function ValidaCampos(FechaActo) {

    if (FechaActo.length == 0) {
        $("#txtFechaActo").get(0).setCustomValidity('Campo Requerido');
        return false;
    }
    return true;
}

function LimpiaControles() {
    $("#divNoControl").show();
    $("#frmAlumnosTitulados").hide();
    $("#hidNoControl").hide();
    $("#cboPeriodoEscolar").val("");
    $("#txtFechaActo").val("");
    $("#cboTiposTitulacion").val("");
}

$('#dtpFechaActo').datetimepicker({
    viewMode: 'days',
    format: 'DD/MM/YYYY',
    locale: 'es',
    defaultDate: 'now'
});

$("#btnNuevo").click(function Nuevo(evento) {
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
        url: '../../../../ServiciosEscolares/CargaTitulados',
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
                 $("#frmAlumnosTitulados").show();
                 $("#hidNoControl").val(resultado.data[0].no_de_control);
                 $("#txtNombreEstudiante").val(resultado.data[0].nombre);
                 $("#txtNombreEstudiante").prop("disabled", true);
                 $("#cboPeriodoEscolar").val(resultado.data[0].periodo_titulacion).change();
                 $("#txtFechaActo").val(resultado.data[0].fecha_acto);
                 $("#cboTiposTitulacion").val(resultado.data[0].id_tipo).change();
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
