var x;
var y;
$("#nueva_reticula").click(function (event) {

    var carreraAbrv = $("#cboCarreras_reticula").val().trim();
    $('#buscar_reticula').modal('hide');
    $('#BodyPrincipal').load('../../../../Reticula/NuevaReticula', { psCarreraAbrv: carreraAbrv });
    event.preventDefault();
    if ($('.modal-backdrop').is(':visible')) {
        $('#BodyPrincipal').removeClass('modal-open');
        $('.modal-backdrop').remove();
    };
})

$("#regresar_cards").click(function (event) {
    $('#BodyPrincipal').load('../../../../ServiciosEscolares/Reticulas');
    event.preventDefault();
})


$("#btnAgregar").click(function (event) {
    $("#guardar_modificar").hide();
    $("#guardar_materia").show();
})

$("#cboMateria").on('input', function () {
    var val = this.value;
    var valor;
    var carrera;

    if ($('#txtMateria option').filter(function () {
        return this.value === val;
    }).length) {
        valor = $('#txtMateria [value="' + val + '"]').data('value');
        carrera = valor.split('-')[1];
        $('option').each(function () {
            if ($(this).val() == carrera) {
                $(this).show();
            } else {
                $(this).hide();
            }
        })
        $('#txtMateria').val(valor.split('-')[0]);
    }
});

$("#cboEspecialidad").on('input', function () {
    var val = this.value;
    var valor;
    var carrera;

    if ($('#txtEspecialidad option').filter(function () {
        return this.value === val;
    }).length) {
        valor = $('#txtEspecialidad [value="' + val + '"]').data('value');
        carrera = valor.split('-')[1];
        $('option').each(function () {
            if ($(this).val() == carrera) {
                $(this).show();
            } else {
                $(this).hide();
            }
        })
        $('#txtEspecialidad').val(valor.split('-')[0]);
    }
});

function obtenerCoord(xs, ys) {
    x = xs;
    y = ys;
    console.log(x + ' ' + y);
}

$(".guardar_materia").click(function (event) {
    $("#guardar_modificar").hide();
    $("#guardar_materia").show();
    let estatus;
    let carrera = $("#carrera").val();
    let reticula = $("#reticula").val();
    let materia = $("#cboMaterias").val();
    let creditos_materia = $("#creditos_materia").val();
    let horas_teoricas = $("#horas_teoricas").val();
    let orden_certificado = $("#orden_certificado").val();
    let especialidad = $("#cboEspecialidad").val();
    if ($("#activo").prop('checked')) {
        estatus = 'A';
    } else {
        estatus = 'N';
    }
    let horas_practicas = $("#horas_practicas").val();
    let creditos_prerequisito = $("#creditos_prerequisito").val();
    let clave_oficial_materia = $("#clave_oficial_materia").val();
    let semestre = x;
    let renglon = y;

    var jsonObject = {
        carrera: carrera,
        reticula: reticula,
        materia: materia,
        creditos_materia: creditos_materia,
        horas_teoricas: horas_teoricas,
        horas_practicas: horas_practicas,
        orden_certificado: orden_certificado,
        semestre_reticula: semestre,
        creditos_prerrequisito: creditos_prerequisito,
        especialidad: especialidad,
        clave_oficial_materia: clave_oficial_materia,
        estatus_materia_carrera: estatus,
        programa_estudios: "",
        renglon: renglon
    };
    console.log(jsonObject);
    $.ajax({
        asyn: false,
        url: '../../../../Reticula/AgregarMateriaReticula',
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
    var carreraAbrv = $("#siglas").val().trim();
    $('#BodyPrincipal').load('../../../../Reticula/NuevaReticula', { psCarreraAbrv: carreraAbrv });
    event.preventDefault();
    if ($('.modal-backdrop').is(':visible')) {
        $('#BodyPrincipal').removeClass('modal-open');
        $('.modal-backdrop').remove();
    };
});

$("#modificar_materia").click(function (event) {
    $("#guardar_modificar").show();
    $("#guardar_materia").hide();


    let carrera = $("#carrera").val();
    let reticula = $("#reticula").val();
    let renglon = y;
    let semestre = x;

    var json = {
        carrera: carrera,
        reticula: reticula,
        semestre_reticula: semestre,
        renglon: renglon

    }
    $.ajax({
        asyn: false,
        url: '../../../../Reticula/BuscarMateriaReticula',
        type: 'POST',
        dataType: 'json',
        data: json,
    }).done(function (data) {

        if (typeof (data.Success) !== "undefined") {
            MensajesToastr("error", "Solicitud Procesada", "Error, no se pudo cargar la materia");
        }
        else {

            $('#cboMaterias option[value=' + data.materia + ']').attr('selected', 'selected');
            $("#creditos_materia").val(data.creditos_materia);
            $("#horas_teoricas").val(data.horas_teoricas);
            $("#horas_practicas").val(data.horas_practicas);
            $("#orden_certificado").val(data.orden_certificado);
            $("#creditos_prerequisito").val(data.creditos_prerrequisito);
            $('#cboEspecialidad option[value=' + data.especialidad + ']').attr('selected', 'selected');
            $("#clave_oficial_materia").val(data.clave_oficial_materia);
            if (data.estatus_materia_carrera == 'A') {
                $("#activo").prop('checked');
            } else {
                $("#activo").prop('unchecked');
            }

            $("#agregar_materia").modal('show');
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    event.preventDefault();
})