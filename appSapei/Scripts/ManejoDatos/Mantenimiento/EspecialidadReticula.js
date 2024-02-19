$("#btnGrdEspecialidad").click(function (event) {
    let especialidad = $("#especialidad").val();
    let carrera = $("#cboCarrerasEspecialidadC").val();
    let nombre_especialidad = $("#nombre_especialidad").val();
    let periodo_inicio = $("#periodo_inicio_es").val();
    let periodo_fin = $("#periodo_fin_es").val();
    let creditos_optativos = $("#creditos_optativos").val();
    let creditos_especialidad = $("#creditos_especialidad").val();
    let clave_especialidad = $("#clave_especialidad").val();

    var jsonObject = {
        especialidad: especialidad,
        carrera: carrera,
        reticula: 0,
        nombre_especialidad: nombre_especialidad,
        periodo_inicio: periodo_inicio,
        periodo_termino: periodo_fin,
        creditos_optativos: creditos_optativos,
        creditos_especialidad: creditos_especialidad,
        clave_especialidad: clave_especialidad
    }

    var jsonCarrera = {
        carrera: carrera
    }

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/AgregarEspecialidadJson',
        type: 'POST',
        dataType: 'json',
        data: jsonObject,
    }).done(function (data) {

        if (typeof (data.Success) === "undefined" || !data.Success) {
            MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
        }
        else {
            MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    limpiarCamposNuevo();
    $("#nuevo_especialidad").modal("hide");
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})

$("#buscarModEspecialidad").click(function (event) {
    $("#modificar_especialidad").modal('hide');
    $("#btnModEspecialidad").show();

    var especialidad = $("#cboEspecialidades").val();
    var json = { especialidad: especialidad }

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/BuscarEspecialidadJson',
        type: 'POST',
        dataType: 'json',
        data: json,
    }).done(function (data) {

        if (typeof (data.Success) !== "undefined") {
            MensajesToastr("error", "Solicitud Procesada", "Error, no se pudo cargar la especialidad");
        }
        else {

            $("#especialidad").val(data.especialidad);
            $('#cboCarrerasEspecialidad option[value=' + data.reticula + ']').attr('selected', 'selected');
            $("#nombre_especialidad").val(data.nombre_especialidad);
            $("#periodo_inicio_es").val(data.periodo_inicio);
            $("#periodo_fin_es").val(data.periodo_termino);
            $("#creditos_optativos").val(data.creditos_optativos);
            $("#creditos_especialidad").val(data.creditos_especialidad);
            $("#clave_especialidad").val(data.clave_especialidad);

            $("#nuevo_especialidad").modal('show');
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    event.preventDefault();
})


$("#btnModEspecialidad").click(function (event) {
    let especialidad = $("#especialidad").val();
    let carrera = $("#cboCarrerasEspecialidad").val();
    let nombre_especialidad = $("#nombre_especialidad").val();
    let periodo_inicio = $("#periodo_inicio_es").val();
    let periodo_fin = $("#periodo_fin_es").val();
    let creditos_optativos = $("#creditos_optativos").val();
    let creditos_especialidad = $("#creditos_especialidad").val();
    let clave_especialidad = $("#clave_especialidad").val();

    var jsonObject = {
        especialidad: especialidad,
        carrera: carrera,
        reticula: 0,
        nombre_especialidad: nombre_especialidad,
        periodo_inicio: periodo_inicio,
        periodo_termino: periodo_fin,
        creditos_optativos: creditos_optativos,
        creditos_especialidad: creditos_especialidad,
        clave_especialidad: clave_especialidad
    }   

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/ModificarEspecialidadJson',
        type: 'POST',
        dataType: 'json',
        data: jsonObject,
    }).done(function (data) {

        if (typeof (data.Success) === "undefined" || !data.Success) {
            MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
        }
        else {
            MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    $("#nuevo_especialidad").modal('hide');
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})

$("#btnElmnEspecialidad").click(function (event) {
    var especialidad = $("#cboEspecialidades").val();
    var json = {
        especialidad: especialidad
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/ElimnarEspecialidadJson',
        type: 'POST',
        dataType: 'json',
        data: json,
    }).done(function (data) {

        if (typeof (data.Success) === "undefined" || !data.Success) {
            MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
        }
        else {
            MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    $("#modificar_especialidad").modal('hide');
    $('#cboEspecialidades option[value=0]').attr('selected', 'selected');
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})

$("#cboEspecialidades").on('input', function () {
    var val = this.value;
    var valor;
    var carrera;

    if ($('#txtEspecialidades option').filter(function () {
        return this.value === val;
    }).length) {
        valor = $('#txtEspecialidades [value="' + val + '"]').data('value');
        carrera = valor.split('-')[1];
        $('option').each(function () {
            if ($(this).val() == carrera) {
                $(this).show();
            } else {
                $(this).hide();
            }
        })
        $('#txtEspecialidades').val(valor.split('-')[0]);
    }
});


$("#especialidad_eliminar").click(function (event) {
    $("#txteliminarespecialidad").show();
    $("#txtmodespecialidad").hide();
    $("#buscarModEspecialidad").hide();
    $("#btnElmnEspecialidad").show();



})

$("#especialidad_modificar").click(function (event) {
    $("#txteliminarespecialidad").hide();
    $("#btnElmnEspecialidad").hide();
    $("#txtmodespecialidad").show();
    $("#btnModEspecialidad").show();
    $("#cboCar").hide();
    $("#cboCarMod").show();
    $("#btnGrdEspecialidad").hide();
    $("#cboEspecialidades").val();
    $("#buscarModEspecialidad").show();


})

$("#especialidad_nuevo").click(function (event) {
    $("#cboCar").show();
    $("#cboCarMod").hide();
    $("#btnGrdEspecialidad").show();
    $("#btnModEspecialidad").hide();
    limpiarCamposNuevo();
})


function limpiarCamposNuevo() {
    $("#especialidad").val("");
    $("#cboCarrerasEspecialidadc").val("0");
    $("#cboCarrerasEspecialidad").val("0");
    $("#nombre_especialidad").val("");
    $("#periodo_inicio_es").val("");
    $("#periodo_fin_es").val("");
    $("#creditos_optativos").val("");
    $("#creditos_especialidad").val("");
    $("#clave_especialidad").val("");
}