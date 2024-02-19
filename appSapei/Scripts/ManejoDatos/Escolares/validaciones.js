$('#dtpFechaActo, #dtpFechaCedula').datetimepicker({
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
                     $("#txtNCedula").val(resultado.data[0].no_cedula);
                     $("#txtFechaCedula").val(resultado.data[0].fecha_asig_cedula);
                 }
             })
             .fail(function (data) {
                 MensajesToastrErrorConexion();
             });
    }

    function RegresaEstudianteDatos1(noControl) {
        if (!esFormatoValidoAlert1(noControl))
            return
        Cargar(noControl);
    }

    function esFormatoValidoAlert1(noControl1) {
        var noControl = noControl1;
        if (!ex_no_control.test(noControl)) {
            MensajesToastr("warning", "Solicitud Procesada", "Ingrese un no. de control valido");
            return false;
        }
        return true;
    }
