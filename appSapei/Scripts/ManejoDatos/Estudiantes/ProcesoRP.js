function autocompletaDep() {
    var loRFC = $("#txtRfc").val();
    if (loRFC.trim().length > 0) {
        RegresaDatosRFC(loRFC);
        RegresaNombreProgramas(loRFC, $("#cboPrograma"));
    }
}
function autocompletaPro() {
    var loIdprograma = $('#cboPrograma').val();
    if (loIdprograma != 0) {
        RegresaDatosPrograma(loIdprograma, ("#txtCorreo"), ("#txtDepartamento"), ("#txtResponsable"), ("#txtCargoResponsablePrograma"), ("#opcioncbo"));
    }    
}
function RegresaDatosPrograma(loIdprograma, txtCorreo, txtDepartamento, txtResponsable, txtCargoResponsable, opcioncbo) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaDatosProgramaRP',
        type: 'POST',
        dataType: 'json',
        data: { psIdPrograma: loIdprograma },
    })
    .done(function (data) {
        if (typeof (data.Success) !== "undefined") {
            $(txtCorreo).val('');
            $(txtDepartamento).val('');
            $(txtResponsable).val('');
            $(txtCargoResponsable).val('');
            $(opcioncbo).val('');
        }
        else {
            var resultado = JSON.parse(data);
            var i = 0;
            $(txtCorreo).val(resultado.data[0].correo);
            $(txtDepartamento).val(resultado.data[0].departamento);
            $(txtResponsable).val(resultado.data[0].responsable);
            $(txtCargoResponsable).val(resultado.data[0].cargo);
            document.ready = document.getElementById("opcioncbo").value = resultado.data[0].opcion_programa;
        }
    })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}
function RegresaNombreProgramas(psRFC, cboPrograma) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaNombreProgramaRP',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: psRFC },
    })
    .done(function (data) {
        if (typeof (data.Success) !== "undefined") {
            cboPrograma.empty();
            MensajesToastr("info", "Solicitud Procesada", "No hay programas para esta dependencia");
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
function RegresaDatosRFC(psRFC) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaDatosDependenciaRP',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: psRFC },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {
                 $("#txtRFC").val('');
                 $('#txtNombreDependencia').val("");
                 $('#txtTitular').val("");
                 $('#txtCargoTitular').val("");
                 $('#txtTelefono').val("");

                 MensajesToastr("info", "Solicitud Procesada", "Este RFC no esta registrado");
                 $('#MensajeDatos').modal('show'); // abrir
             }
             else {
                 var resultado = JSON.parse(data);
                 $("#txtRfc").val(psRFC);
                 $('#txtNombreDependencia').val(resultado.data[0].dependencia);
                 $('#txtTitular').val(resultado.data[0].titular);
                 $('#txtCargoTitular').val(resultado.data[0].puesto_cargo);
                 $('#txtTelefono').val(resultado.data[0].telefono);
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}
function EditarAnteproyecto() {

    var mensaje;
    var opcion = confirm("¿Deseas modificar la informacion?");
    if (opcion == true) {
        document.getElementById('Delimitaciontxt').disabled = false
        document.getElementById('ObjetivoGtxt').disabled = false
        document.getElementById('tags_4').disabled = false
        document.getElementById('tags_5').disabled = false
        document.getElementById('Justificaciontxt').disabled = false
        document.getElementById('bntAceptaAnteproyecto').disabled = true
        document.getElementById('Iniciotxt').disabled = false
        document.getElementById('Fintxt').disabled = false
        document.getElementById('Duraciontxt').disabled = false
        $("#hp").css("display", "block");
        $("#ph").css("display", "none");
    }
}
function RegresaEstado() {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaEstadoRP',
        type: 'POST',
        dataType: 'json',
        data: { },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {
                 $("#hidEstado").val();
                 MensajesToastr("info", "Solicitud no procesada");
             }
             else {
                 var resultado = JSON.parse(data);
                 var i = 0;
                 $('#BodyPrincipal').load('../../../../Estudiante/ProcesoRP');
                 $('#smartwizard').smartWizard('goToStep', resultado.data[0].estado);
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}
//FUNCIÓN PARA QUE LETRAS QUE INGRESEN POR TECLADO SE PONGAN EN MAYUSCULAS
function mayus(e) {
    e.value = e.value.toUpperCase();
}


$('#rpinicio').datetimepicker({
    format: 'DD/MM/YYYY'
});
$('#rpfin').datetimepicker({
    format: 'DD/MM/YYYY'
});

$(document).ready(function () {
    var estado = $("#hidEstado").val();

    $("#txtSolicita").click(function () {
        $.ajax({
            sync: false,
            url: '../../../Estudiante/SolicitudCartaRP',
            type: 'POST',
            dataType: 'json',
            data: {  },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada");
                    $('#BodyPrincipal').load('../../../../Estudiante/ProcesoRP');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    });

    $('textarea#txtDelimitacion').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_delimitacion').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_delimitacion').text('');
        }
    });

    $('textarea#txtObjetivoG').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_objetivogeneral').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_objetivogeneral').text('');
        }
    });

    $('textarea#txtJustificacion').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_justificacion').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_justificacion').text('');
        }
    });

    $('textarea#Delimitaciontxt').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_delimitaciontxt').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_delimitaciontxt').text('');
        }
    });

    $('textarea#ObjetivoGtxt').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_objetivogeneraltxt').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_objetivogeneraltxt').text('');
        }
    });

    $('textarea#Justificaciontxt').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_justificaciontxt').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_justificaciontxt').text('');
        }
    });

    $("#NewProyecto").click(function () {
        
        $("#RFCtxt").val($("#txtRfc").val());
        $("#NombreProyectotxt").val('');
        $("#Correotxt").val('');
        $("#Departamentotxt").val('');
        $("#Responsabletxt").val('');
        $("#CargoResponsabletxt").val('');
        $("#cboopcion").val('');
        $("#txtId").val(0);
        $('#AltaPrograma').modal('show'); // abrir
    });

    $("#EditProyecto").click(function () {
        var loIdprograma = $("#cboPrograma").val();
        $.ajax({
            sync: false,
            url: '../../../Estudiante/RegresaDatosProgramaRP',
            type: 'POST',
            dataType: 'json',
            data: { psIdPrograma: loIdprograma },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined") {
                    $("#NombreProyectotxt").val('');
                    $("#Correotxt").val('');
                    $("#Departamentotxt").val('');
                    $("#Responsabletxt").val('');
                    $("#CargoResponsabletxt").val('');
                    $("#cboopcion").val('');
                }
                else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#NombreProyectotxt").val(resultado.data[0].nombre);
                    $("#Correotxt").val(resultado.data[0].correo);
                    $("#Departamentotxt").val(resultado.data[0].departamento);
                    $("#Responsabletxt").val(resultado.data[0].responsable);
                    $("#CargoResponsabletxt").val(resultado.data[0].cargo);
                    document.ready = document.getElementById("cboopcion").value = resultado.data[0].opcion_programa;
                    $('#AltaPrograma').modal('show'); // abrir
                }
    })
    });
//    $("#Ubicacionfile").css("display", "none");

    $("#txtRfc").keyup(function () {
        var value = $(this).val();
        $("#RFCtxt").val(value);
    });

    $("#btnCurso").click(function () {
        $.ajax({
            url: '../../../Estudiante/CargaEstadoInicialJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {  },
        })
      .done(function (data) {
          if (typeof (data.Success) !== "undefined" && !data.Success) {
              MensajesToastr("error", "Solicitud Procesada", "Error al continuar");
          }
          else {
              MensajesToastr("success", "Solicitud Procesada", "Enviado");
              $('#BodyPrincipal').load('../../../../Estudiante/ProcesoRP');
              $('#smartwizard').smartWizard('goToStep', 1);
          }
      })
      .fail(function (data) {
          MensajesToastrErrorConexion();
      });
    });

    $("#frmAltaProyecto").submit(function (evento) {
        var rfcDependencia = $("#RFCtxt").val();
        var nombreProyecto = $("#NombreProyectotxt").val();
        var correo = $("#Correotxt").val();
        var departamento = $("#Departamentotxt").val();
        var responsable = $("#Responsabletxt").val();
        var cargoResponsable = $("#CargoResponsabletxt").val();
        var opcion = $("#cboopcion").val();
        var id = $("#txtId").val();
        $.ajax({
            url: '../../../Estudiante/AltaProyectoJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psRfcDependencia: rfcDependencia, psNombreProyecto: nombreProyecto, psResponsable: responsable, psCargoResponsable: cargoResponsable, psCorreo: correo, psDepartamento: departamento, psOpcion: opcion, psId: id },
        })
                  .done(function (data) {
                      if (typeof (data.Success) !== "undefined" && !data.Success) {
                          MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                      }
                      else {
                          MensajesToastr("success", "Solicitud Procesada", "Proyecto Registrado");
                          $('#AltaPrograma').modal('hide'); // cerrar}
                          $('body').removeClass('modal-open');
                          $('.modal-backdrop').remove();
                          RegresaNombreProgramas(rfcDependencia, $("#cboPrograma"));
                      }
                  })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });

    $("#bntAceptaAnteproyecto").click(function () {
        $.ajax({
            url: '../../../Estudiante/AceptaAnteproyectoJsonRP',
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
             RegresaEstado();
              //CambiaValorTabla(fin, 3, 0);
          }
      })
      .fail(function (data) {
          MensajesToastrErrorConexion();
      });
    });

    $("#frmSolicitud").submit(function (evento) {
        var programa = $("#cboPrograma").val();
        var modalidad = $("#txtModalidad").val();
        $.ajax({
            url: '../../../Estudiante/CartaSolicitudJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psPrograma: programa, psModalidad:modalidad },
        })
                       .done(function (data) {
                           if (typeof (data.Success) !== "undefined" && !data.Success) {
                               MensajesToastr("info", "Solicitud Procesada <br />"+ data.Mensaje);
                           }
                           else {
                               MensajesToastr("success", "Solicitud Procesada", "Solicitud Registrada");
                               RegresaEstado();
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        evento.preventDefault();
    });

    $('#fileUbicacion').fileinput({
        uploadAsync: false,
        language: 'es',
        showPreview: true,
        showCaption: true,
        showUpload: true,
        minFileCount: 1,
        maxFileCount: 1,
        uploadUrl: '../../../Estudiante/AltaAnteproyectoJsonRP',
        allowedFileExtensions: ['jpg', 'png', 'bmp', 'jpeg'],
        uploadExtraData: function () {
            return {
                piPrograma: $("#idPrograma").val(), psDelimitacion: $("#txtDelimitacion").val(), psObjetivoG: $("#txtObjetivoG").val(), psObjetivoE: $("#tags_2").val(), psActividades: $("#tags_2").val(), psJustificacion: $("#txtJustificacion").val(), psFechaInicio: $("#txtInicio").val(), psFechaFin: $("#txtFin").val(), piDuracion: $("#txtDuracion").val(), psObservaciones: " " };
        }
    });

    $('#fileUbicacion').on('filebatchuploadsuccess', function (event, data) {
        var form = data.form, files = data.files, extra = data.extra,
            response = data.response, reader = data.reader;
        if (response.Success == true) {
            MensajesToastr("success", "Solicitud Procesada", "Anteproyecto registrado");
            RegresaEstado();
            $('#fileUbicacion').fileinput('reset');
        }
        else {
            MensajesToastr("info", "Solicitud Procesada", response.Mensaje);
        }
    });

    $('#fileUbicacion').on('filebatchuploaderror', function (event, data, msg) {
        MensajesToastr("info", "Solicitud Procesada", "El archivo no tiene el formato correcto");
    });

    $("#btnSolicitud").click(function () {
        $('#smartwizard').smartWizard('goToStep', 10);
    });
    // Smart Wizard
    $('#smartwizard').smartWizard({
        selected: estado,
        keyNavigation:false,
        theme: 'default',
        transitionEffect: 'fade',
        toolbarSettings: false,
        lang: false,
        showStepURLhash: false,
    });

    $(".btn-toolbar").hide();  

    $("#theme_selector").on("change", function () {
        // Change theme
        $('#smartwizard').smartWizard("theme", $(this).val());
        return true;
    });

    $("#theme_selector").change();


    var url = $("#Ubicacionimage").val();

    $('#Ubicacionfile').fileinput({
        uploadAsync: false,
        language: 'es',
        showPreview: true,
        showCaption: true,
        showUpload: true,
        minFileCount: 1,
        maxFileCount: 1,
        initialPreviewAsData: true,
        initialPreview: [url],
        uploadUrl: '../../../Estudiante/AltaAnteproyectoJsonRP',
        allowedFileExtensions: ['jpg', 'png', 'bmp', 'jpeg'],
        uploadExtraData: function () {
            return { piPrograma: $("#idPrograma").val(), psDelimitacion: $("#Delimitaciontxt").val(), psObjetivoG: $("#ObjetivoGtxt").val(), psObjetivoE: $("#tags_4").val(), psActividades: $("#tags_5").val(), psJustificacion: $("#Justificaciontxt").val(), psFechaInicio: $("#Iniciotxt").val(), psFechaFin: $("#Fintxt").val(), piDuracion: $("#Duraciontxt").val(), psObservaciones: $("#txtObservaciones").val()};
        }
    });

    $('#Ubicacionfile').on('filebatchuploadsuccess', function (event, data) {
        var form = data.form, files = data.files, extra = data.extra,
            response = data.response, reader = data.reader;
        if (response.Success == true) {
            MensajesToastr("success", "Solicitud Procesada", "Anteproyecto registrado");
            RegresaEstado();
        }
        else {
            MensajesToastr("info", "Solicitud Procesada", response.Mensaje);
        }
        $('#Ubicacionfile').fileinput('reset');
    });

    $('#Ubicacionfile').on('filebatchuploaderror', function (event, data, msg) {
        MensajesToastr("info", "Solicitud Procesada", "El archivo no tiene el formato correcto");
    });   
});
