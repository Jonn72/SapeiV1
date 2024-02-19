function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}

//AUTO COMPLETA LOS DATOS DE LA DEPENDENCIA CON EL RFC INGRESADO
function autocompletaDep() {
    var loDependencia = $("#cboDependencia").val();
    if (loDependencia.trim().length > 0) {
        RegresaDatosRFC(loDependencia, $("#txtRazonsocial"), $('#txtNombreDependencia'), $('#txtTitular'), $('#txtCargoTitular'), $('#txtTelefono'));
        RegresaNombreProgramas(loDependencia, $("#cboPrograma"));
    }
}

//LISTA DESPLEGABLE DE LOS PROGRAMAS QUE LA DEPENDENCIA CON EL RFC INGRESADO
function RegresaNombreProgramas(psRfc, cboPrograma) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaNombreProgramaSS',
        type: 'POST',
        dataType: 'json',
        data: { psRfc: psRfc },
    })
    .done(function (data) {
        if (typeof (data.Success) !== "undefined") {
            cboPrograma.empty();
            MensajesToastr("info", "Solicitud Procesada", "Esta dependencia no tiene programa registrado"); //CAMBIE ERROR POR SUCCESS           
        }
        else {
            var resultado = JSON.parse(data);
            var i = 0;
            cboPrograma.empty();
            cboPrograma.append('<option selected></option>');
            for (; resultado.data[i];) {
                cboPrograma.append('<option value=' + resultado.data[i].id + '>' + resultado.data[i].nombre + '</option>');
                i++;
            }

            var val = $("#hidPrograma").val();

            if (val != null)
                $('#cboPrograma option[value=' + val + ']').attr('selected', 'selected');
        }
    })
    .fail(function (data) {
        MensajesToastrErrorConexion();
    });
    return esValido;
}

//REGRESA LOS DATOS DE LA DEPENDENCIA CON EL RFC
function RegresaDatosRFC(psRFC, txtRazonsocial, txtNombreDependencia, txtTitular, txtCargoTitular, txtTelefono) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaDatosDependenciaSS',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: psRFC },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined") {
                $("#txtRFC").val('');
                $("#txtRazonsocial").val('');
                $("#txtNombreDependencia").val('');
                $("#txtTitular").val('');
                $("#txtCargoTitular").val('');
                $("#txtTelefono").val('');

                MensajesToastr("info", "Solicitud Procesada", "Este RFC no esta registrado en la base");
            }
            else {
                var resultado = JSON.parse(data);
                var i = 0;
                $(txtRazonsocial).val(resultado.data[0].razon_social);
                $(txtNombreDependencia).val(resultado.data[0].dependencia);
                $(txtTitular).val(resultado.data[0].titular);
                $(txtCargoTitular).val(resultado.data[0].puesto_cargo);
                $(txtTelefono).val(resultado.data[0].telefono);

                $("#txtCalle").val(resultado.data[0].domicilio);
                $("#txtNumero").val(resultado.data[0].numero);
                $("#txtCodPostal").val(resultado.data[0].cod_post);
                $("#txtColonia").val(resultado.data[0].colonia);
                $("#txtCiudad").val(resultado.data[0].ciudad_localidad);
                $("#txtEstado").val(resultado.data[0].entidad);
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    return esValido;
}

//AUTO COMPLETA NOMBRE DEL PROGRAMA REGISTRADO CON EL RFC DE LA DEPENDENCIA
function autocompletaPro() {
    var loPrograma = $("#cboPrograma").val();
    if (loPrograma != "") {
        RegresaDatosPrograma(loPrograma, $("#txtResponsable"), $("txtCargoResponsablePrograma"), $("txtCorreo"), $("txtDepartamento"), $("TipoProgramatxt"), $("txtObjetivo"));
    }
}

//REGRESA LOS DATOS DEL PROGRAMA DEL MODAAL NUEVO PROGRAMA
function RegresaDatosPrograma(psPrograma, txtResposable, txtCargoResponsablePrograma, txtCorreo) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaDatosProgramaSS',
        type: 'POST',
        dataType: 'json',
        data: { psPrograma: psPrograma },
    })
           .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                   $("#cboPrograma").val('');
                   $("#txtResponsable").val('');
                   $("#txtCargoResponsablePrograma").val('');
                   $("#txtCorreo").val('');
                   $("#txtDepartamento").val('');
                   $("#txtObjetivo").val('');
                   $("#TipoProgramatxt").val('');

                   MensajesToastr("info", "Solicitud Procesada", "El RFC no cuenta con ningun registro de programas");
               }
               else {
                   var resultado = JSON.parse(data);
                   var i = 0;
                   $("#txtResponsable").val(resultado.data[0].responsable);
                   $("#txtCargoResponsablePrograma").val(resultado.data[0].cargo_responsable);
                   $("#txtCorreo").val(resultado.data[0].correo_titular);
                   $("#txtDepartamento").val(resultado.data[0].departamento);
                   $("#txtObjetivo").val(resultado.data[0].objetivo);
                   document.ready = document.getElementById("TipoProgramatxt").value = resultado.data[0].id_tipo_programa;
                   //$("#txtRFC").val(resultado.data[0].rfc);
               }
           })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}


//FUNCIÓN PARA QUE LETRAS QUE INGRESEN POR TECLADO SE PONGAN EN MAYUSCULAS
$("tag").keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

function mayus(e) {
    e.value = e.value.toUpperCase();
}

//FUNCION DEL BOTON EXTERNO DE LA MODALIDAD DE SERVICIO SOCIAL PARA ABRIR MODAL DE SOLICITUD
$(document).on('click', '#btnExterno', function () {
    $('#ModalExterno').modal('show');
});

//FUNCION DEL BOTON NUEVO, QUE ABRE OTRO MODAL DENTRO DEL MISMO Y AL GUARDAR LIMPIA LOS CAMPOS  
$(document).on('click', '#agregar_programa', function () {
    var select = document.getElementById("cboDependencia");
    $("#Dependenciatxt").val(select.value);
    $('#ModalAgregarPrograma').modal('show');
    $("#Programatxt").val("");
    $("#Responsabletxt").val("");
    $("#CargoResponsabletxt").val("");
    $("#Correotxt").val("");
    $("#Departamentotxt").val("");
    $("#Objetivotxt").val("");
});

////radio buttons
//var valor = '';

//$("input[name='tipo_actividades']").on('change', function () {
//    valor = $(this).val();
//    alert(valor);
//});

$(document).ready(function () {
    //FUNCION DEL BOTON ACEPTAR DEL CURSO
    $("#btnSolicitud").click(function () {
        $('#smartwizard').smartWizard('goToStep', 7);
    });

    $("#btnCurso").click(function () {
        $.ajax({
            url: '../../../Estudiante/CargaEstadoInicialJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {},
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al continuar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Enviado");
                    $('#smartwizard').smartWizard("next");
                    $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    });

    //FUNCION DEL BOTON INTERNO DE LA MODALIDAD DE SERVICIO SOCIAL QUE DESPLIEGA UN MODAL PARA LA SELECCION DEL TURNO
    $("#btnInterno").click(function () {
        $("#interno").modal('show');
    });

    //FUNCION PARA GUARDAR EL TURNO EN EL QUE SE REALIZARA EL SERVICIO SOCIAL INTERNO
    $("#btnsaveinterno").click(function () {
        var turno = $("#txtTurnos").val();
        $.ajax({
            url: '../../../Estudiante/GuardarSolicitudInternoJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {
                psTurno: turno
            },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Turno Procesado", data.Mensaje);
                }
                else {
                    MensajesToastr("success", "Turno Procesado", "Modalidad registrado");
                    $('#interno').modal('hide'); // cerrar
                    $('#smartwizard').smartWizard('goToStep', 2);
                    $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                    setTimeout(1000);
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    });

    $("#frmSolicitudServicioSocial").submit(function (evento) {
        var programa = $("#cboPrograma").val();
        var actividades = $("#tags_1").val();
        var tipoactividad = $("#selectTipoActividades").val();

        if (modalidad == "ext") {
            if (programa == "" || actividades == "" || tipoactividad=="") {
                MensajesToastr("info", "Debes capturar todos los campos");
                return false;
            }
            $.ajax({
                url: '../../../Estudiante/GuardarSolicitudServicioSocialExternoSS',
                type: 'POST',
                dataType: 'json',
                data: {
                    psTipo_actividad: tipoactividad, psActividades: actividades, piId: programa
                },
            })
                .done(function (data) {
                    if (typeof (data.Success) !== "undefined" && !data.Success) {

                        MensajesToastr("error", "Solicitud Procesada", data.Mensaje);
                    }
                    else {
                        MensajesToastr("success", "Solicitud Procesada", "Solicitud registrada");
                        $('#smartwizard').smartWizard('goToStep', 5);
                        $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                    }
                })
                .fail(function (data) {
                    MensajesToastrErrorConexion();
                });
            evento.preventDefault();
        }
        else if (modalidad == "int")
        {
            if (actividades == "" || tipoactividad == "") {
                MensajesToastr("info", "Debes capturar todos los campos");
                return false;
            }
            $.ajax({
                url: '../../../Estudiante/GuardarSolicitudServicioSocialInternoSS',
                type: 'POST',
                dataType: 'json',
                data: {
                    psActividades: actividades, psTipo_actividad: tipoactividad
                },
            })
                .done(function (data) {
                    if (typeof (data.Success) !== "undefined" && !data.Success) {

                        MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
                    }
                    else {
                        MensajesToastr("success", "Solicitud Procesada", "Solicitud registrada");
                        $('#smartwizard').smartWizard('goToStep', 5);
                        $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                    }
                })
                .fail(function (data) {
                    MensajesToastrErrorConexion();
                });
            evento.preventDefault();
        }
    });

    //FUNCION DEL BOTON ACEPTAR DE CARTA COMPROMISO
    $("#btnCartaCompromiso").click(function () {
        $.ajax({
            url: '../../../Estudiante/CartaCompromisoJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {},
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "Error al continuar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Carta de presentación confirmada Enviado");
                    $('#smartwizard').smartWizard('goToStep', 6);
                    $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    });

    //FUNCION PARA GUARDAR EL VALOS DE LOS RADIOBUTTONS DE LA AUTOEVALUACION
    $("#radioReporte1").submit(function (evento) {
        var p_1 = $('input:radio[name=rdbcountry]:checked').val();
        var p_2 = $('input:radio[name=rdbcountry2]:checked').val();
        var p_3 = $('input:radio[name=rdbcountry3]:checked').val();
        var p_4 = $('input:radio[name=rdbcountry4]:checked').val();
        var p_5 = $('input:radio[name=rdbcountry5]:checked').val();
        var p_6 = $('input:radio[name=rdbcountry6]:checked').val();
        var p_7 = $('input:radio[name=rdbcountry7]:checked').val();
        if (p_1 == null || p_2 == null || p_3 == null || p_4 == null || p_5 == null || p_6 == null || p_7 == null) {
            MensajesToastr("info", "Debes calificar todas las preguntas");
            return false;
        }
        $.ajax({
            url: '../../../Estudiante/GuardarReportesJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {
                pspreg1: p_1, pspreg2: p_2, pspreg3: p_3, pspreg4: p_4, pspreg5: p_5, pspreg6: p_6, pspreg7: p_7
            },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error ", "Reporte Procesado", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Reporte Procesado", "Reporte registrado");                    
                    $('#interno').modal('hide'); // cerrar
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        evento.preventDefault();
    });

    //FUNCION PARA DESCARGAR EL PDF DE EVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 1
    $("#ReporteB1-2").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/EvaluacionCualitativa1SS/1', displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE REPORTE BIMESTRAL DE SERVICIO SOCIAL 1
    $("#ReporteB1-1").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/ReporteBimestral1SS/1', displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE AUTOEVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 1
    $("#ReporteB1-3").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        var noReporte = 1;
        $('#divPDF').load('../../../../DocumentosOficiales/AutoEvaluacionCualitativaPrestadorSS', { psReporte: noReporte }, displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE AUTOEVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 2
    $("#ReporteB2-3").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        var noReporte = 2;
        $('#divPDF').load('../../../../DocumentosOficiales/AutoEvaluacionCualitativaPrestadorSS', { psReporte: noReporte }, displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE EVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 2
    $("#ReporteB2-2").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/EvaluacionCualitativa1SS/2', displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE REPORTE BIMESTRAL DE SERVICIO SOCIAL 2
    $("#ReporteB2-1").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/ReporteBimestral1SS/2', displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE AUTOEVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 3
    $("#ReporteB3-3").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        var noReporte = 3;
        $('#divPDF').load('../../../../DocumentosOficiales/AutoEvaluacionCualitativaPrestadorSS', { psReporte: noReporte }, displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE EVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 3
    $("#ReporteB3-2").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/EvaluacionCualitativa1SS/3', displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE REPORTE BIMESTRAL DE SERVICIO SOCIAL 3
    $("#ReporteB3-1").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/ReporteBimestral1SS/3', displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE AUTOEVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 4
    $("#ReporteB4-3").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        var noReporte = 4;
        $('#divPDF').load('../../../../DocumentosOficiales/AutoEvaluacionCualitativaPrestadorSS', { psReporte: noReporte }, displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE EVALUACION DE ACTIVIDADES POR EL PRESTADOR DE SERVICIO SOCIAL 
    $("#ReporteB4-4").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        var noReporte = 4;
        $('#divPDF').load('../../../../DocumentosOficiales/EvaluacionActividadesPrestadorSS', { psReporte: noReporte }, displaySection);
    });

    //FUNCION PARA DESCARGAR EL PDF DE EVALUACION CUALITATIVA DEL PRESTADOR DE SERVICIO SOCIAL 4
    $("#ReporteB4-2").click(function (event) {
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/EvaluacionCualitativa1SS/4', displaySection);
    });

    var estado = $("#hidEstado").val();
    var modalidad = $("#hidModalidad").val();

    //FUNCION PARA GUARDAR EL VALOS DE LOS RADIOBUTTONS DE LA EVALUACION DE LAS ACTIVIDADES
    $("#radioEvaluacionActividades").submit(function (evento) {
        var p_1 = $('input:radio[name=rdbcountry]:checked').val();
        var p_2 = $('input:radio[name=rdbcountry2]:checked').val();
        var p_3 = $('input:radio[name=rdbcountry3]:checked').val();
        var p_4 = $('input:radio[name=rdbcountry4]:checked').val();
        var p_5 = $('input:radio[name=rdbcountry5]:checked').val();
        var p_6 = $('input:radio[name=rdbcountry6]:checked').val();
        var p_7 = $('input:radio[name=rdbcountry7]:checked').val();
        var p_8 = $('input:radio[name=rdbcountry8]:checked').val();
        if (p_1 == null || p_2 == null || p_3 == null || p_4 == null || p_5 == null || p_6 == null || p_7 == null || p_8 == null) {
            MensajesToastr("info", "Debes calificar todas las preguntas");
            return false;
        }
        $.ajax({
            url: '../../../Estudiante/GuardarReporteEvaluacionActividadesJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {
                pspreg1: p_1, pspreg2: p_2, pspreg3: p_3, pspreg4: p_4, pspreg5: p_5, pspreg6: p_6, pspreg7: p_7, pspreg8: p_8
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("error ", "Reporte Procesado", "Error al guardar");
                  }
                  else {
                      MensajesToastr("success", "Reporte Procesado", "Reporte registrado");
                      $('#interno').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                  }
              })
              .fail(function (data) {
                  MensajesToastrErrorConexion();
              });
        evento.preventDefault();
    });

    //FUNCION DEL BOTON GENERAR DE LA VISTA REPORTES PARA VISUALIZAR EL MODAL DE AUTOEVALUACION CUALITATIVA
        $("#btnReporte1").click(function () {
            $("#Reporte1").modal('show');
        });

    //FUNCION DEL BOTON GENERAR DE LA VISTA REPORTES PARA VISUALIZAR EL MODAL DE EVALUACION DE ACTIVIDADES
        $("#btnReporteActividades").click(function () {
            $("#EvaluacionActividades").modal('show');
    });

    //FUNCION PARA REGISTRAR UN NUEVO PROGRAMA POR PARTE DEL ESTUDIANTE
    $("#frmAltaPrograma").submit(function (evento) {
        var dependencia = $("#Dependenciatxt").val();
        var programa = $("#Programatxt").val();
        var correo_titular = $("#Correotxt").val();
        var responsable = $("#Responsabletxt").val();
        var cargoresponsable = $("#CargoResponsabletxt").val();
        var departamento = $("#Departamentotxt").val();
        var objetivo = $("#Objetivotxt").val();
        var tipoprograma = $("#selectTipoPrograma").val();
        $.ajax({
            url: '../../../Estudiante/AltaProgramaExternoJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {
                psDependencia: dependencia, psPrograma: programa, psCorreo: correo_titular, psResponsable: responsable, psCargoResponsablePrograma: cargoresponsable, psDepartamento: departamento, psTipoprograma: tipoprograma, psObjetivo: objetivo
            },
        })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "El programa ya esta registrado");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Programa Registrado");
                $('#ModalAgregarPrograma').modal('hide'); // cerrar
                RegresaNombreProgramas(dependencia, $("#cboPrograma"));
                //CambiaValorTabla(fin, 3, 0);
            }
        })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
    });

    function muestra() {
        $('#MensajeDatos').modal('show');
    }

    //FUNCION PARA GUARDAR  SOLICITUD DE CARTA DE PRESENTACION
    $("#frmSolicitud").submit(function (evento) {
        var turno = $("#txtTurno").val();
        alert(turno);
        $.ajax({
            url: '../../../Estudiante/GuardarSolicitudExternoJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {
                psTurno: turno
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {

                      MensajesToastr("error ", "Solicitud Procesada", "Error al guardar");
                  }
                  else {         
                      MensajesToastr("success", "Solicitud Procesada", "Solicitud registrada");
                      $('#ModalExterno').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#smartwizard').smartWizard('goToStep', 2);
                      $('#BodyPrincipal').load('../../../../Estudiante/ProcesoSS');
                      setTimeout(muestra,2500);
                  }
                  
              })
              .fail(function (data) {
                  MensajesToastrErrorConexion();
            });
        evento.preventDefault();
    });   

    // Smart Wizard
    $('#smartwizard').smartWizard({
        selected: estado,
        keyNavigation: false,
        theme: 'default',
        transitionEffect: 'fade',
        toolbarSettings: false,
        lang: false,
    });

    $(".btn-toolbar").hide();

    $("#theme_selector").on("change", function () {
        // Change theme
        $('#smartwizard').smartWizard("theme", $(this).val());
        return true;
    });

    // Set selected theme on page refresh
    $("#theme_selector").change();
});

   