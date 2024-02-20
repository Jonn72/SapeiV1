
$("#btnGrdCarrera").click(function (event) {
    let carrera = $("#carrera_id").val();
    let nivel_escolar = $("#cboNivEscolarCarrera").val();
    let clave_oficial = $("#clv_oficial").val();
    let nombre_carrera = $("#nombre_carrera").val();
    let nombre_reducido = $("#nombre_abrv_carrera").val();
    let siglas = $("#siglas_carrera").val();
    let fecha_inicio = $("#txtFechaInicioCarrera").val();
    let fecha_termino = $("#txtFechaFinCarrera").val();
    let creditos_totales = $("#creditos_totales").val();
    let clave = $("#cboClaveArea_Carrera").val();

    var jsonObject = {
        carrera: carrera,
        reticula: 9,
        nivel_escolar: nivel_escolar,
        clave_oficial: clave_oficial,
        nombre_carrera: nombre_carrera,
        nombre_reducido: nombre_reducido,
        siglas: siglas,
        carga_maxima: 0,
        carga_minima: 0,
        fecha_inicio: fecha_inicio,
        fecha_termino: fecha_termino,
        clave_cosnet: '',
        creditos_totales: creditos_totales,
        id_area_carr: "NULL",
        id_sub_area_carr: "NULL",
        id_nivel_carr: "NULL",
        consecutivo_carrera: "NULL",
        nivel: "NULL",
        clave: clave,
        modalidad: ''

        
    };

    var jsonCarrera = {
        carrera: carrera
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/AgregarCarreraAbrvJson',
        type: 'POST',
        dataType: 'json',
        data: jsonCarrera,
    }).done(function (data) {

        if (typeof (data.Success) === "undefined" || !data.Success) {
        }
        else {
            MensajesToastr("success", "Solicitud Procesada", data.Mensaje);
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/AgregarCarreraJson',
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
    $("#nuevo_carrera").modal('hide');
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
});

$("#carrera_nuevo").click(function (event) {
    limpiarCamposNuevo();
    $("#btnGrdCarrera").show();
    $("#buscarCarrera").hide();
    $("#btnMdfCarrera").hide();
    $("#txtNuevoCarrera").show();
    $("#txtModCarrera").hide();
});

$("#carera_modificar").click(function (event) {
    $("#btnElmnCarreras").hide();
    $("#btnGrdCarrera").hide();
    $("#buscarCarrera").show();
    $("#txtNuevoCarrera").hide();
    $("#txtModCarrera").show();
    $("#buscarModCarreras").show();
    $("#txteliminarcarrera").hide();
    $("#txtbuscarcarrera").show();

});

$("#carrera_eliminar").click(function (event) {
    $("#btnElmnCarreras").show();
    $("#buscarCarrera").show();
    $("#txteliminarcarrera").show();
    $("#buscarModCarreras").hide();
    $("#txtbuscarcarrera").hide();
});


$("#buscarModCarreras").click(function (event) {
    $("#modificar_carreras").modal('hide');
    $("#btnMdfCarrera").show();
    $("#buscarCarrera").hide();

    var carrera = "";
    var siglas = $("#cboCarreras").val();
    var reticula = 1;
    var json = {
        carrera: carrera,
        reticula: reticula,
        siglas: siglas
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/BuscarCarreraJson',
        type: 'POST',
        dataType: 'json',
        data: json,
    }).done(function (data) {

        if (typeof (data.Success) !== "undefined") {
            MensajesToastr("error", "Solicitud Procesada", "Error, no se pudo cargar la carrera");
        }
        else {

            $("#carrera_id").val(data.carrera);
            $('#cboNivEscolarCarrera option[value=' + data.nivel_escolar + ']').attr('selected', 'selected');
            $("#clv_oficial").val(data.clave_oficial);
            $("#nombre_carrera").val(data.nombre_carrera);
            $("#nombre_abrv_carrera").val(data.nombre_reducido);
            $("#siglas_carrera").val(data.siglas);
            $("#txtFechaInicioCarrera").val(data.fecha_inicio);
            $("#txtFechaFinCarrera").val(data.fecha_termino);
            $('#cboTipoMateria option[value=' + data.tipo_materia + ']').attr('selected', 'selected');
            $("#creditos_totales").val(data.creditos_totales);
            $('#cboClaveArea_Carrera option[value=' + data.clave + ']').attr('selected', 'selected');


            $("#nuevo_carrera").modal('show');
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    $('#cboCarreras option[value=0]').attr('selected', 'selected');
    event.preventDefault();
});


$("#btnMdfCarrera").click(function (event) {
    let carrera = $("#carrera_id").val();
    let nivel_escolar = $("#cboNivEscolarCarrera").val();
    let clave_oficial = $("#clv_oficial").val();
    let nombre_carrera = $("#nombre_carrera").val();
    let nombre_reducido = $("#nombre_abrv_carrera").val();
    let siglas = $("#siglas_carrera").val();
    let fecha_inicio = $("#txtFechaInicioCarrera").val();
    let fecha_termino = $("#txtFechaFinCarrera").val();
    let creditos_totales = $("#creditos_totales").val();
    let clave = $("#cboClaveArea_Carrera").val();

    var jsonObject = {
        carrera: carrera,
        reticula: 0,
        nivel_escolar: nivel_escolar,
        clave_oficial: clave_oficial,
        nombre_carrera: nombre_carrera,
        nombre_reducido: nombre_reducido,
        siglas: siglas,
        carga_maxima: 0,
        carga_minima: 0,
        fecha_inicio: fecha_inicio,
        fecha_termino: fecha_termino,
        creditos_totales: creditos_totales,
        clave: clave
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/ModificarCarreraJson',
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
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    $("#nuevo_carrera").modal('hide');
    event.preventDefault();


})

$("#btnElmnCarreras").click(function (event) {
    var carrera = "";
    var siglas = $("#cboCarreras").val();
    var reticula = 1;
    var json = {
        carrera: carrera,
        reticula: reticula,
        siglas: siglas
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/EliminarCarreraJson',
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
    $("#modificar_carrera").modal('hide');
    $('#cboCarreras option[value=0]').attr('selected', 'selected');
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})

function limpiarCamposNuevo() {
    $("#carrera_id").val("");
    $("#cboNivEscolarCarrera").val("0");
    $("#clv_oficial").val("");
    $("#nombre_carrera").val("");
    $("#nombre_abrv_carrera").val("");
    $("#siglas_carrera").val
    $('#dtpFechaInicioCarrera').datetimepicker({
        viewMode: 'years',
        format: 'YYYY/MM/DD',
        locale: 'es',
        defaultDate: new Date()
    });
    $('#dtpFechaFinCarrera').datetimepicker({
        viewMode: 'years',
        format: 'YYYY/MM/DD',
        locale: 'es',
        defaultDate: new Date()
    });
    $("#creditos_totales").val("");
    $("#cboClaveArea_Carrera").val("0");
}

$('#dtpFechaFinCarrera').datetimepicker({
    viewMode: 'years',
    format: 'YYYY-MM-DD',
    locale: 'es',
    defaultDate: new Date()
});

$('#dtpFechaInicioCarrera').datetimepicker({
    viewMode: 'years',
    format: 'YYYY-MM-DD',
    locale: 'es',
    defaultDate: new Date()
});

$("#cboCarreras").on('input', function () {
    var val = this.value;
    var valor;
    var carrera;

    if ($('#txtCarrera option').filter(function () {
        return this.value === val;
    }).length) {
        valor = $('#txtCarrera [value="' + val + '"]').data('value');
        carrera = valor.split('-')[1];
        $('option').each(function () {
            if ($(this).val() == carrera) {
                $(this).show();
            } else {
                $(this).hide();
            }
        })
        $('#txtCarrera').val(valor.split('-')[0]);
    }
});