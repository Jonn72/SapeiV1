$("input").keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

$('#dtpFechaIniPlaza').datetimepicker({
    viewMode: 'years',
    format: 'DD/MM/YYYY',
    locale: 'es',
    defaultDate: new Date()
});

$("#btnGuardarCategoria").on('click', function (evento) {
    var categoria = $("#txtClaveCategoria").val();
    var descripcion = $("#txtDescripcionCategoria").val();
    if (!ValidaValoresCategoria(categoria, descripcion)) {
        evento.preventDefault();
        return;
    }
    $.ajax({
        asyn: false,
        url: '../../../RecursosHumanos/GuardaCategoriaPlaza',
        type: 'POST',
        dataType: 'json',
        data: { psCategoria: categoria, psDescripcion: descripcion},
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Categoria Guardada");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    evento.preventDefault();
});

$("#btnGuardarAsignacion").on('click', function (evento) {
    var rfc = $("#txtRFCAsignar").val();
    var categoria = $("#txtCategoriaAsignar").val();
    var horas = $("#txtHorasAsignar").val();
    var interinato = $("#cboInterinatoAsignar").val();
    var fecha = $("#txtFechaAsignar").val();
    if (!ValidaValoresPlazas(rfc, categoria, horas))
        return;
    $.ajax({
        asyn: false,
        url: '../../../RecursosHumanos/GuardaPersonalPlaza',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: rfc, psCategoria: categoria, psInterinato: interinato, piHoras: horas, psFecha: fecha },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Categoria Guardada");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    evento.preventDefault();
});

$("#btnEliminarAsignacion").on('click', function (evento) {
    var id = $("#txtCategoriaEliminar").val();
    if (!ValidaValoresEliminar(id))
        return;
    $.ajax({
        asyn: false,
        url: '../../../../RecursosHumanos/EliminaPlazasPersonal',
        type: 'POST',
        dataType: 'json',
        data: { psId: id },
    })
        .done(function (data) {

            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
            }
            else {
                LimpiarEliminar();
                MensajesToastr("success", "Solicitud Procesada", "Plaza Eliminada");
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    evento.preventDefault();
});

function ValidaValoresCategoria(categoria, descripcion) {

    if (categoria.trim().length === 0) {
        MensajesToastr("error", "Validación de campos", "Ingrese una categoria");
        return false;
    }
    if (descripcion.trim().length === 0) {
        MensajesToastr("error", "Validación de campos", "Ingrese una descripcion para la categoria");
        return false;
    }
    return true;
}

function ValidaValoresPlazas(rfc, categoria, horas) {
    if (rfc.trim().length === 0) {
        MensajesToastr("error", "Validación de campos", "Seleccione un RFC");
        return false;
    }
    if (categoria.trim().length === 0) {
        MensajesToastr("error", "Validación de campos", "Seleccione una categoria");
        return false;
    }
    if (horas <= 0) {
        MensajesToastr("error", "Validación de campos", "Las horas deben ser mayor a 0");
        return false;
    }
    return true;
}

function ValidaValoresEliminar(id) {

    if (id.trim().length === 0) {
        LimpiarEliminar();
        MensajesToastr("error", "Validación de campos", "Seleccione una plaza a eliminar");
        return false;
    }
    return true;
}

function rfcplazas() {
    var html = "";
    var RFC = $("#txtRFCEliminar").val();
    $.ajax({
        url: '../../../RecursosHumanos/RegresaPlazas',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: RFC }
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                //MensajesToastr("info", "Solicitud Procesada", "No tiene plazas asignadas");
            }
            else {

                var resultado = JSON.parse(data);             
                var obj = resultado.data;
                Object.keys(obj).forEach(function (key) {
                    html = html + JSON.stringify(obj[key].list_categoria);
                });
                $('#listCategoriaEliminar').html(html);
                $("#row_buttons_search").hide();
                $("#row_list").show();
                $("#row_buttons").show();
                $('#listCategoriaEliminar').html(html);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}

function LimpiarEliminar() {
    
    $("#txtCategoriaEliminar").val('');
    $("#txtRFCEliminar").val('');
    $("#listCategoriaEliminar").empty();
    $("#row_buttons_search").show();
    $("#row_list").hide();
    $("#row_buttons").hide();

}

$("#btnAbrirCategorias").on('click', function (evento) {
    $('#divTablaGeneral').load('../../../../RecursosHumanos/MostrarCategoriasPlazas/');
    $("#divModal").modal("show");
    evento.preventDefault();
});

$("#btnAbrirPlazas").on('click', function (evento) {
    $('#divTablaGeneral').load('../../../../RecursosHumanos/MostrarPersonalPlazas/');
    $("#divModal").modal("show");
    evento.preventDefault();
});
