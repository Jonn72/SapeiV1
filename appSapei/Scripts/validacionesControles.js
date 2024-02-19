
//EXpresiones Regulares
var ex_no_control = /^((B|R|C){1})*([0,1,2]{1})([0-9]{1})(([0-9]{3})|(ZP))([0-9]{3,5})$/;
var ex_nombres = /^([A-ZÑÁÉÍÓÚa-zñáéíóú]+[\s]*)+$/;
var ex_correo = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var ex_folio_aspirante = /^F([1,2]{1})([0-9]{2})([0-9]{4})?$/;
var ex_telefono = /^\d{10}$/;
var ex_año = /^\d{4}$/;
var ex_promedio = /^(\d|-)?(\d|,)*\.?\d*$/;
var ex_moneda = /^[0-9]*\.?[0-9]{1,2}$/;
var ex_referencia = /^\d{8}$/;
var ex_NSS = /^\d{11}$/;
var ex_rfc = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;
var ex_personal = /^[A-ZÑ\x26|a-zñ\x26]{5,30}([0-9]{1,5})?$/;
var ex_aspirante = /^F([1,2]{1})([0-9]{2})([0-9]{4})?$/;
var ex_instructor = /^@([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;

function validaUsuario(usuario) {
    if (ex_rfc.test(usuario))
        return "DOCENTE";
    if (ex_no_control.test(usuario))
        return "ESTUDIANTE";
    if (ex_personal.test(usuario))
        return "PERSONAL";
    if (ex_aspirante.test(usuario))
        return "ASPIRANTE";
    if (ex_instructor.test(usuario))
        return "INSTRUCTOR";
    return "";
}
function validaRFCoNoControl(usuario) {
    if (ex_rfc.test(usuario))
        return "D";
    if (ex_no_control.test(usuario))
        return "E";
    return "";
}
function ValidaCampo(campo, tipo) {
    tipo = tipo.toUpperCase()
    var valor = $(campo).val();
    if (valor.trim().length == 0) {
        $(campo).get(0).setCustomValidity('Campo Requerido');
        return false;
    }
    switch (tipo) {
        case 'NOCONTROL':
            return ValidaNoControl(campo);
        case 'NOMBRE':
            return ValidaNombrePropios(campo);
        case 'CORREO':
            return ValidaCorreo(campo);
        case 'TEL':
            return ValidaTelefono(campo);
        case 'AVG':
            return ValidaPromedio(campo);
        case 'AÑO':
            return ValidaAño(campo);
        case 'MONEDA':
            return ValidaMoneda(campo);
        case 'REFERENCIA':
            return ValidaNoReferencia(campo);
        case 'NSS':
            return ValidaNSS(campo);
        case 'INSTRUCTOR':
            return ValidaInstructor(campo);
        case 'RFC':
            return ValidaRFC(campo);
        default:
            ;
    }
}
function ValidaNoControl(campo) {
    var valor = $(campo).val();
    if (valor.trim().lenth == 0) {
        MensajesToastr("info", "Validación Procesada", "El no. de control esta vacio");
        return false;
    }
    if (ex_no_control.test(valor)) {
        return true;
    }
    MensajesToastr("info", "Validación Procesada", "El no. de control no tiene un formato valido");
    $(campo).val("")
    return false;
}
function ValidaNombrePropios(campo) {
    var valor = $(campo).val();
    if (ex_nombres.test(valor)) {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    $(campo).get(0).setCustomValidity('El campo solo acepta letras o espacios.');
    return false;
}
function ValidaCorreo(campo) {
    var valor = $(campo).val();
    if (ex_correo.test(valor)) {
        return true;
    }
    MensajesToastr("info", "Mensaje de Sistema", "El correo no es valido (ejemplo@dominio.zyx).")
    return false;
}
function ValidaTelefono(campo) {
    var valor = $(campo).val();
    if (ex_telefono.test(valor)) {
        return true;
    }
    MensajesToastr("info", "Mensaje de Sistema", "Ingrese un número de 8 a 10 digitos.")
    return false;
}
function ValidaPromedio(campo) {
    var valor = $(campo).val();
    if (ex_promedio.test(valor)) {
        return true;
    }
    $(campo).val('');
    MensajesToastr("error", "Mensaje de Sistema", "El promedio es incorrecto")
    return false;
}
function ValidaAño(campo) {
    var valor = $(campo).val();
    if (ex_año.test(valor)) {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    $(campo).val('');
    $(campo).get(0).setCustomValidity('Ingrese un año de 4  digitos.');
    return false;
}
function ValidaMoneda(campo) {
    var valor = $(campo).val();
    if (ex_moneda.test(valor)) {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    $(campo).get(0).setCustomValidity('Ingrese un monto a dos digitos.');
    return false;
}
function ValidaNoReferencia(campo) {
    var valor = $(campo).val();
    if (ex_referencia.test(valor)) {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    $(campo).get(0).setCustomValidity('No. de referencia invalido.');
    return false;
}
function ValidaNSS(campo) {
    var valor = $(campo).val();
    var arreglo;
    var i;
    var multiplicador;
    var sumador;
    var verificador;
    var tope;
    var multi;
    $(campo).get(0).setCustomValidity('El NSS es invalido.');
    if (!ex_NSS.test(valor)) {
        MensajesToastr("error", "Mensaje de Sistema","El NSS es invalido.")
        return false;
    }

    if (valor === "00000000000") {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    //el tercer par corresponde al año de nacimiento
    if (valor.substring(4, 6) !== $("#txtFechaNacimiento").val().substring(2, 4)) {
        MensajesToastr("error", "Mensaje de Sistema", "El NSS es incorrecto.")
        return false;
    }
    arreglo = valor.split("");
    multiplicador = 1;
    sumador = 0;
    for (i = 0; i < arreglo.length - 1; i++) {
        multi = arreglo[i] * multiplicador;
        if (multi.toString().length == 2)
            sumador = sumador + parseInt(multi.toString().split("")[0]) + parseInt(multi.toString().split("")[1]);
        else
            sumador = sumador + arreglo[i] * multiplicador;
        if (multiplicador === 1)
            multiplicador = 2;
        else
            multiplicador = 1;
    }
    tope = Math.ceil(sumador / 10) * 10;
    verificador = tope - sumador;
    if (verificador != arreglo[10])
        return false;

    $(campo).get(0).setCustomValidity('');
    return true;
}
function ValidaCampoNSS(campo) {
    var valor = $(campo).val();
    var arreglo;
    var i;
    var multiplicador;
    var sumador;
    var verificador;
    var tope;
    var multi;
    $(campo).get(0).setCustomValidity('El NSS es invalido.');
    if (!ex_NSS.test(valor)) {
        MensajesToastr("info", "Mensaje de Sistema", "El NSS es invalido.")
        return false;
    }

    if (valor === "00000000000") {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    //el tercer par corresponde al año de nacimiento
    if (valor.substring(4, 6) !== $("#txtFechaNacimiento").val().substring(2, 4)) {
        MensajesToastr("error", "Mensaje de Sistema", "El NSS no corresponde a los datos capturados.")
        return false;
    }
    arreglo = valor.split("");
    multiplicador = 1;
    sumador = 0;
    for (i = 0; i < arreglo.length - 1; i++) {
        multi = arreglo[i] * multiplicador;
        if (multi.toString().length == 2)
            sumador = sumador + parseInt(multi.toString().split("")[0]) + parseInt(multi.toString().split("")[1]);
        else
            sumador = sumador + arreglo[i] * multiplicador;
        if (multiplicador === 1)
            multiplicador = 2;
        else
            multiplicador = 1;
    }
    tope = Math.ceil(sumador / 10) * 10;
    verificador = tope - sumador;
    if (verificador != arreglo[10])
        return false;

    return true;
}

function ValidaInstructor(campo) {
    var valor = $(campo).val();
    if (ex_instructor.test(valor)) {
        $(campo).get(0).setCustomValidity('');
        return true;
    }
    $(campo).get(0).setCustomValidity('Nonbre de usuario invalido.');
    return false;
}
function ValidaRFC(campo) {
    var valor = $(campo).val();
    if (ex_rfc.test(valor)) {
        return true;
    }
    MensajesToastr("error", "Mensaje de Sistema", "Ingrese RFC valido")
    return false;
}
