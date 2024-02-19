

$("#btnGrd").click(function (event) {
    let clave_materia = $("#clv_materia").val();
    let clave_area = $("#cboClaveArea").val();
    let nivel_escolar = $("#cboNivEscolar").val();
    let tipo_materia = $("#cboTipoMateria").val();
    let nombre_materia = $("#nombre_mteria").val();
    let nombre_abreviado_materia = $("#nombre_abrv_materia").val();

    var jsonObject = {
        clave_materia: clave_materia,
        clave_area: clave_area,
        nivel_escolar: nivel_escolar,
        tipo_materia: tipo_materia,
        nombre_materia: nombre_materia,
        nombre_abreviado_materia: nombre_abreviado_materia
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/AgregarMateriaJson',
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
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})

function limpiarCamposNuevo() {
    $("#clv_materia").val("");
    $("#nombre_mteria").val("");
    $("#nombre_abrv_materia").val("");
    $("#cboClaveArea").val("0");
    $("#cboNivEscolar").val("0");
    $("#cboTipoMateria").val("0");
}

$("#nuevo").click(function () {
    $("#btnMdf").hide();
    $("#btnGrd").show();
    limpiarCamposNuevo();
})


$("#modificar").click(function () {
    $("#btnGrdMod").hide();
    $("#buscar").show();
    $("#btnMdf").hide();
    $("#btnGrd").hide();
    $("#btnElmn").hide();
    $("#buscarMod").show();
    $("#txteliminarmateria").hide();
    $("#txtmodmateria").show();
})

$("#eliminar").click(function () {
    $("#buscarMod").hide();
    $("#btnElmn").show();
    $("#txteliminarmateria").show();
    $("#txtmodmateria").hide();
})

$("#buscarMod").click(function () {
    $("#modificar_materias").modal('hide');
    $("#btnMdf").show();

    var materia = $("#cboClaveMateria").val();
    var json = {materia: materia}
    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/BuscarMateriaJson',
        type: 'POST',
        dataType: 'json',
        data: json,
    }).done(function (data) {

        if (typeof (data.Success) !== "undefined") {
            MensajesToastr("error", "Solicitud Procesada", "Error, no se pudo cargar la materia");
        }
        else {

            $("#clv_materia").val(data.materia);
            $('#cboClaveArea option[value=' + data.clave_area + ']').attr('selected', 'selected');
            $('#cboNivEscolar option[value=' + data.nivel_escolar + ']').attr('selected', 'selected');
            $('#cboTipoMateria option[value=' + data.tipo_materia + ']').attr('selected', 'selected');
            $("#nombre_mteria").val(data.nombre_completo_materia);
            $("#nombre_abrv_materia").val(data.nombre_abreviado_materia);
         
            $("#nuevo_materias").modal('show');
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    event.preventDefault();
})

$("#btnMdf").click(function (event) {
    let clave_materia = $("#clv_materia").val();
    let clave_area = $("#cboClaveArea").val();
    let nivel_escolar = $("#cboNivEscolar").val();
    let tipo_materia = $("#cboTipoMateria").val();
    let nombre_materia = $("#nombre_mteria").val();
    let nombre_abreviado_materia = $("#nombre_abrv_materia").val();

    var jsonObject = {
        clave_materia: clave_materia,
        clave_area: clave_area,
        nivel_escolar: nivel_escolar,
        tipo_materia: tipo_materia,
        nombre_materia: nombre_materia,
        nombre_abreviado_materia: nombre_abreviado_materia
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/ModificarMateriaJson',
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
    $("#nuevo_materias").modal('hide');
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})




$("#btnElmn").click(function (event) {
    let clave_materia = $("#cboClaveMateria").val();

    var jsonObject = {
        clave_materia: clave_materia,
    };

    $.ajax({
        asyn: false,
        url: '../../../../ServiciosEscolares/EliminarMateriaJson',
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
    $("#modificar_materias").modal('hide');
    $('#cboTipoMateria option[value=0]').attr('selected', 'selected');
    $('#BodyPrincipal').load('../../../ServiciosEscolares/Reticulas');

    event.preventDefault();
})

$("#cboClaveMateria").on('input', function () {
    var val = this.value;
    var valor;
    var carrera;

    if ($('#txtMaterias option').filter(function () {
        return this.value === val;
    }).length) {
        valor = $('#txtMaterias [value="' + val + '"]').data('value');
        carrera = valor.split('-')[1];
        $('option').each(function () {
            if ($(this).val() == carrera) {
                $(this).show();
            } else {
                $(this).hide();
            }
        })
        $('#txtMaterias').val(valor.split('-')[0]);
    }
});