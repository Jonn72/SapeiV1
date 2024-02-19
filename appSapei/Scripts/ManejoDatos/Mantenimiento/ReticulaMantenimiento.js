var x;
var y;

var idReticula = $("#reticula").attr("value");
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

$("#guardar_materia").click(function (event) {
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

$(".btnAgregar").click(function (event) {
    limpiarCamposNuevo();
    $("#guardar_modificar").hide();
    $("#guardar_materia").show();
})

$(".modificar_materia").click(function (event) {
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
    console.log(json);
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
            console.log(data);
            var r = JSON.parse(data);
            $("#cboMaterias").val(r.data[0].materia);
            $("#creditos_materia").val(r.data[0].creditos_materia);
            $("#horas_teoricas").val(r.data[0].horas_teoricas);
            $("#horas_practicas").val(r.data[0].horas_practicas);
            $("#orden_certificado").val(r.data[0].orden_certificado);
            $("#creditos_prerequisito").val(r.data[0].creditos_prerrequisito);
            $("#cboEspecialidad").val(r.data[0].especialidad);
            $("#clave_oficial_materia").val(r.data[0].clave_oficial_materia);
            if (r.data[0].estatus_materia_carrera == 'A') {
                $("#activo").prop('checked', true);
            } else {
                $("#activo").prop('checket', false);
            }

            $("#agregar_materia").modal('show');
        }
    })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    event.preventDefault();
})



$("#guardar_modificar").click(function (event) {
    let estatus;
    let carrera = $("#carrera").val();
    let reticula = $("#reticula").val();
    let materia = $("#cboMaterias").val();
    let creditos_materia = $("#creditos_materia").val();
    let horas_teoricas = $("#horas_teoricas").val();
    let orden_certificado = $("#orden_certificado").val();
    let especialidad = $("#cboEspecialidad").val();
    if ($("#activo").prop('checked', true)) {
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
        url: '../../../../Reticula/ModificarMateriaReticula',
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



$(".eliminar_materia").click(function (event) {
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
        url: '../../../../Reticula/EliminarMateriaReticula',
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
    var carreraAbrv = $("#siglas").val().trim();
    $('#BodyPrincipal').load('../../../../Reticula/NuevaReticula', { psCarreraAbrv: carreraAbrv });
    event.preventDefault();
    if ($('.modal-backdrop').is(':visible')) {
        $('#BodyPrincipal').removeClass('modal-open');
        $('.modal-backdrop').remove();
    };
})

function limpiarCamposNuevo() {
    $("#cboMaterias").val("");
    $("#creditos_materia").val("");
    $("#horas_teoricas").val("");
    $("#horas_practicas").val("");
    $("#orden_certificado").val("");
    $("#creditos_prerequisito").val("");
    $("#cboEspecialidad").val("");
    $("#clave_oficial_materia").val("");
    $("#activo").prop('checked', false);
}

var columns = document.querySelectorAll('.card_reticula_nueva');
var draggingClass = 'card-posibleseleccionar';
var dragSource;

Array.prototype.forEach.call(columns, function (col) {
    col.addEventListener('dragstart', handleDragStart, false);
    col.addEventListener('dragenter', handleDragEnter, false)
    col.addEventListener('dragover', handleDragOver, false);
    col.addEventListener('dragleave', handleDragLeave, false);
    col.addEventListener('drop', handleDrop, false);
    col.addEventListener('dragend', handleDragEnd, false);
});


var idSujeto;
var idDestino;

var valueSujeto;
var valueSujetox;
var valueSujetoy;

var valueDestino;
var valueDestinox;
var valueDestinoy;

var materiaSujeto;
var materiaDestino;

var materiaSujetoSplit;
var materiaDestinoSplit;

function handleDragStart(evt) {
    dragSource = this;
    evt.target.classList.add(draggingClass);
    evt.dataTransfer.effectAllowed = 'move';
    evt.dataTransfer.setData('text/html', this.innerHTML);
    idSujeto = $(this).attr("id");
    valueSujeto = $(this).attr('data-value');
    console.log(valueSujeto);
    materiaSujeto = $(this).text();
    materiaSujetoSplit = materiaSujeto.split("\n");
    materiaSujeto = materiaSujetoSplit[2].trim();
 }

function handleDragOver(evt) {
    evt.dataTransfer.dropEffect = 'move';
    evt.preventDefault();
}

function handleDragEnter(evt) {
    this.classList.add('card-posibleseleccionar');
}

function handleDragLeave(evt) {
    this.classList.remove('card-posibleseleccionar');

}

function handleDrop(evt) {
    evt.stopPropagation();

    if (dragSource !== this) {
        dragSource.innerHTML = this.innerHTML;
        this.innerHTML = evt.dataTransfer.getData('text/html');
        idDestino = $(this).attr("id");
        valueDestino = $(this).attr('data-value');
        console.log(idDestino);
        materiaDestino = $(this).text();
        materiaDestinoSplit = materiaDestino.split("\n");
        materiaDestino = materiaSujetoSplit[2].trim();

        console.log(materiaDestino);
    }
    if (idSujeto == 'dos' && idDestino == 'uno') {
        this.classList.add('card-acreditada');
        convertidorDestino(valueDestino);
        convertidorSujeto(valueSujeto);
        updateVacio(valueDestinox, valueDestinoy, valueSujetox, valueSujetoy);

    } else if (idSujeto == 'dos' && idDestino == 'dos') {
        
        convertidorDestino(valueDestino);
        convertidorSujeto(valueSujeto);
        update(valueDestinox, valueDestinoy, valueSujetox, valueSujetoy);
    }
    
    evt.preventDefault();
}


function updateVacio(dx, dy, sx, sy) {
    let clave_materia = dx;
    let clave_area = dy;
    let nivel_escolar = sx;
    let tipo_materia = sy;
    let nombre_materia = idReticula;
    let nombre_abreviado_materia = materiaDestino;

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
        url: '../../../../ServiciosEscolares/ModificarOrdenReticulaVacia',
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
}

function update(dx, dy, sx, sy) {
    let clave_materia = dx;
    let clave_area = dy;
    let nivel_escolar = sx;
    let tipo_materia = sy;
    let nombre_materia = idReticula;
    let nombre_abreviado_materia = materiaDestino;

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
            url: '../../../../ServiciosEscolares/ModificarOrdenReticula',
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
}

function convertidorSujeto(sujeto) {
    var Array;
    Array = sujeto.split('-');
    for (var i = 0; i < sujeto.length; i++) {
        if (i == 0) {
            valueSujetox = Array[i];
        }
        if (i == 1) {
            valueSujetoy = Array[i];
        }
    }
}

function convertidorDestino(destino) {
    var Array;
    Array = destino.split('-');
    for (var i = 0; i < destino.length; i++) {
        if (i == 0) {
            valueDestinox = Array[i];
            
        }
        if (i == 1) {
            valueDestinoy = Array[i];
        }
    }
}
function handleDragEnd(evt) {
    Array.prototype.forEach.call(columns, function (col) {
        ['over', 'card-posibleseleccionar'].forEach(function (className) {
            col.classList.remove(className);
        });
    });
    var carreraAbrv = $("#siglas").val().trim();
        $('#BodyPrincipal').load('../../../../Reticula/NuevaReticula', { psCarreraAbrv: carreraAbrv });
}