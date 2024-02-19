$("#btnGuardar2").click(function Nuevo(event) {
    var RFC = $("#txtRFC2").val();
    var Estatus = $("#cboEstatus").val();
    var Nombre = $("#txtNombre").val();
    var ApeP = $("#txtPaterno").val();
    var ApeM = $("#txtMaterno").val();
    var NTarjeta = $("#txtNumTarjeta").val();
    var Tpersonal = $("#cboTipoPersonal").val();
    var ClaveArea = $("#cboDeptos").val();
    //var AreaAcad = $("#cboDeptosAds").val();
    var ActividadLaboral = $("#cboAcLab").val();
    var NSS = $("#txtNSS").val();
    var Genero = $('input[name=rbtSexo]:checked').val();
    var FechaNac = $("#txtFechaNacimiento").val();
    var EstadoCivil = $("#cboCivil").val();
    var Nacionalidad = $("#cboNac").val();
    var EstadoNac = $("#cboEstados").val();
    var Estudios = $("#cboEstudios").val();
    var Carrera = $("#txtCarrera").val();
    var FechaTit = $("#txtFechaTitulacion").val();
    var Cedula = $("#txtCedula").val();
    var Correo = $("#txtCorreo").val();
    var Tel = $("#txtTel").val();
    var TelEmergencia = $("#txtTelEm").val();
    var CURP = $("#txtCURP").val();
    var Calle = $("#txtCalle").val();
    var NDomicilio = $("#txtNoDomicilio").val();
    var Id_cp = $("#txtCodPostal").val();
    var col = $("#cboColonia").val();


    if (!ValidaCampos()) {
        return;
    }
    else {
        $.ajax({
            asyn: false,
            url: '../../../RecursosHumanos/GuardaPersonal',
            type: 'POST',
            dataType: 'json',
            data: { psRFC: RFC, psEstatus: Estatus, psNombre: Nombre, psApePaterno: ApeP, psApeMaterno: ApeM, psNTarjeta: NTarjeta, psTPersonal: Tpersonal, psClaveArea: ClaveArea,/*psAreaAcad: AreaAcad,*/ psActividadLaboral: ActividadLaboral, psNSS: NSS, psGenero: Genero, psFechaNac: FechaNac, psEstadoCivil: EstadoCivil, psNacionalidad: Nacionalidad, psEstadoNac: EstadoNac, psEstudios: Estudios, psCarrera: Carrera, psFechaTit: FechaTit, psCedula: Cedula, psCorreo: Correo, psTel: Tel, psTelEmergencia: TelEmergencia, psCURP: CURP, psCalle: Calle, psNDomicilio: NDomicilio, psId_cp: Id_cp, psCol: col },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../RecursosHumanos/RegistraPersonal');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    }
    event.preventDefault();
});


$('#dtpFecha, #dtpFechaTit').datetimepicker({
    viewMode: 'years',
    format: 'YYYY/MM/DD',
    locale: 'es',
    defaultDate: new Date()
});

$("#btnBuscarRFC").click(function Nuevo(evento) {
    var rfc = $("#txtRFC").val();
    if (!ValidaRFC($("#txtRFC")))
        return
    Cargar(rfc);
    evento.preventDefault();
});

function RegresaPersonalDatos(RFC) {

}

function Cargar(RFC) {
    $.ajax({
        url: '../../../../RecursosHumanos/RegresaPersonal',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: RFC }
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {

                MensajesToastr("info", "Solicitud Procesada", "No se encuentra registrado el RFC " + RFC + " ");
                $("#divRFC").hide();
                $("#frmDatos1").show();
                $("#frmDatos2").show();
                $("#frmDom").show();
                $("#grd").show();
                $("#txtRFC2").val(RFC);
            }
            else {
                var resultado = JSON.parse(data);
                $("#divRFC").hide();
                $("#frmDatos1").show();
                $("#frmDatos2").show();
                $("#frmDom").show();
                $("#grd").show();

                $("#txtRFC2").val(RFC);
                $("#cboEstatus").val($.trim(resultado.data[0].status_empleado)).change();
                $("#txtNombre").val(resultado.data[0].nombre_empleado);
                $("#txtPaterno").val(resultado.data[0].apellido_paterno);
                $("#txtMaterno").val(resultado.data[0].apellido_materno);
                $("#txtNumTarjeta").val(resultado.data[0].no_tarjeta);



                $("#cboTipoPersonal").val($.trim(resultado.data[0].tipo_personal)).change();
                $("#cboDeptos").val($.trim(resultado.data[0].clave_area)).change();
                //$("#cboDeptosAds").val($.trim(resultado.data[0].area_academica)).change();
                $("#cboAcLab").val($.trim(resultado.data[0].actividad_laboral)).change();


                $("#txtNSS").val(resultado.data[0].NSS);

                if (resultado.data[0].genero === 'M')
                    $("#rdbMujer").attr('checked', 'checked');
                else
                    $("#rdbHombre").attr('checked', 'checked');

                $("#txtFechaNacimiento").val(resultado.data[0].fecha_nacimiento);


                $("#cboCivil").val($.trim(resultado.data[0].estado_civil)).change();
                $("#cboNac").val($.trim(resultado.data[0].nacionalidad)).change();

                $("#cboEstados").val($.trim(resultado.data[0].estado_nacimiento)).change(); 
                //$('#cboEstados option[value=' + $.trim(resultado.data[0].estado_nacimiento) + ']').attr('selected', 'selected');
                $("#cboEstudios").val($.trim(resultado.data[0].nivel_estudios)).change();



                $("#txtCarrera").val(resultado.data[0].nombre_carrera);
                $("#txtFechaTitulacion").val(resultado.data[0].fecha_titulacion);
                $("#txtCedula").val(resultado.data[0].cedula_profesional);
                $("#txtCorreo").val(resultado.data[0].correo_electronico);
                $("#txtTel").val(resultado.data[0].telefono);
                $("#txtTelEm").val(resultado.data[0].telefono_emergencia);
                $("#txtCURP").val(resultado.data[0].curp);


                $("#txtCalle").val(resultado.data[0].calle);
                $("#txtNoDomicilio").val(resultado.data[0].numero);
                $("#hidColonia").val(resultado.data[0].colonia);
                $("#txtCodPostal").val(resultado.data[0].id_cp);
                autocompleta("Estudiante", resultado.data[0].id_cp);
                


                $("#txtFechaLiberacion").val(resultado.data[0].fecha_liberacion);
                $("#hidNoControl").val(resultado.data[0].no_de_control);
                $("#hidPeriodo").val(resultado.data[0].periodo);
                $("#txtNombreEstudiante").val(resultado.data[0].nombre);
                $("#txtPromedio").val(resultado.data[0].promedio_o_puntos);
                $("#cboTipo_liberacion").val($.trim(resultado.data[0].id_liberacion)).change();
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}

function ValidaCampos() {

    var nombre = $("#txtNombre").val();
    var paterno = $("#txtPaterno").val();
    var materno = $("#txtMaterno").val();
    var fecha = $("#txtFechaNacimiento").val();
    var domicilio = $("#txtCalle").val();
    var numero = $("#txtNoDomicilio").val();
    var cp = $("#txtCodPostal").val();

    if (!ex_nombres.test(nombre)) {
        MensajesToastr("info", "Validación", "Ingresa un nombre valido");
        return false;
    }
    if (!ex_nombres.test(paterno)) {
        MensajesToastr("info", "Validación", "Ingresa el apellido paterno valido");
        return false;
    }
    if (!ex_nombres.test(materno)){
        MensajesToastr("info", "Validación", "Ingresa el apellido materno valido");
        return false;
    }
    if (fecha.length == 0) {
        MensajesToastr("info", "Validación", "Ingresa fecha de nacimiento");
        return false;
    }
    if (domicilio.length == 0) {
        MensajesToastr("info", "Validación", "Ingresa domicilio");
        return false;
    }
    if (numero.length == 0) {
        MensajesToastr("info", "Validación", "Ingresa el número de domicilio");
        return false;
    }
    if (cp.length == 0) {
        MensajesToastr("info", "Validación", "Ingresa código postal valido");
        return false;
    }
    return true;
}


//Función para validar un RFC
// Devuelve el RFC sin espacios ni guiones si es correcto
// Devuelve false si es inválido
// (debe estar en mayúsculas, guiones y espacios intermedios opcionales)
//function rfcValido(rfc, aceptarGenerico = true) {
//    const re = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;
//    var validado = rfc.match(re);

//    if (!validado)  //Coincide con el formato general del regex?
//        return false;

//    //Separar el dígito verificador del resto del RFC
//    const digitoVerificador = validado.pop(),
//        rfcSinDigito = validado.slice(1).join(''),
//        len = rfcSinDigito.length,

//        //Obtener el digito esperado
//        diccionario = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ",
//        indice = len + 1;
//    var suma,
//        digitoEsperado;

//    if (len == 12) suma = 0
//    else suma = 481; //Ajuste para persona moral

//    for (var i = 0; i < len; i++)
//        suma += diccionario.indexOf(rfcSinDigito.charAt(i)) * (indice - i);
//    digitoEsperado = 11 - suma % 11;
//    if (digitoEsperado == 11) digitoEsperado = 0;
//    else if (digitoEsperado == 10) digitoEsperado = "A";

//    //El dígito verificador coincide con el esperado?
//    // o es un RFC Genérico (ventas a público general)?
//    if ((digitoVerificador != digitoEsperado)
//        && (!aceptarGenerico || rfcSinDigito + digitoVerificador != "XAXX010101000"))
//        return false;
//    else if (!aceptarGenerico && rfcSinDigito + digitoVerificador == "XEXX010101000")
//        return false;
//    //return rfcSinDigito + digitoVerificador;
//    return true;
//}



$("#btnBuscarNoControlTabla").on('click', function (evento) {
    $('#divModal').modal("show");
    evento.preventDefault();
})