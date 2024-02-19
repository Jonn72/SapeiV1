$(document).ready(function () {
     // Toolbar extra buttons
     var btnFinish = $('<button></button>').text('Guardar')
                                      .addClass('btn btn-info')
                                      .on('click', function () {
                                           if (!$(this).hasClass('disabled')) {
                                                var elmForm = $("#myForm");
                                                if (elmForm) {
                                                     elmForm.validator('validate');
                                                     var elmErr = elmForm.find('.has-error');
                                                     if (elmErr && elmErr.length > 0) {
                                                          MuestraMensaje("Faltan Datos por Capturar", 1);
                                                          return false;
                                                     } else {
                                                          MuestraMensaje("Se guardo correctamente", 0);
                                                          GuardaDatosAjax();
                                                          elmForm.submit();
                                                          return false;
                                                     }
                                                }
                                           }
                                      });
     var btnCancel = $('<button></button>').text('Cancelar')
                                      .addClass('btn btn-danger')
                                      .on('click', function () {
                                           $('#smartwizard').smartWizard("reset");
                                           $('#myForm').find("input, textarea").val("");
                                      });



     // Smart Wizard
     $('#smartwizard').smartWizard({
          selected: 0,
          theme: 'dots',
          transitionEffect: 'none',
          toolbarSettings: {
               toolbarPosition: 'bottom',
               toolbarExtraButtons: [btnFinish, btnCancel]
          },
          anchorSettings: {
               markDoneStep: true, // add done css
               markAllPreviousStepsAsDone: true, // When a step selected by url hash, all previous steps are marked done
               removeDoneStepOnNavigateBack: true, // While navigate back done step after active step will be cleared
               enableAnchorOnDoneStep: true // Enable/Disable the done steps navigation
          }
     });

     $("#smartwizard").on("leaveStep", function (e, anchorObject, stepNumber, stepDirection) {
          var elmForm = $("#form-step-" + stepNumber);
          // only on forward navigation, that makes easy navigation on backwards still do the validation when going next
          //Validacion del numero de control
          var validacionPropia = true;
          if (stepNumber == 0) {
               validacionPropia = esFormatoValido();
          }
          else if(stepNumber == 1)
          {
               validacionPropia = ValidaCamposStep1();
          }
          else if (stepNumber == 3) {
               validacionPropia = ValidaCamposStep3();
          }
          if (stepDirection === 'forward' && elmForm) {
               elmForm.validator('validate');
               if (!validacionPropia)
                    return false;
               var elmErr = elmForm.children('.has-error');
               if (elmErr && elmErr.length > 0) {
                    // Form validation failed
                    return false;
               }
          }
          return true;
     });

     $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection) {
          // Enable finish button only on last step
          if (stepNumber == 6) {
               $('.btn-finish').removeClass('disabled');
               var nombre = $("#txtNombre").val() + " " + $("#txtPaterno").val() + " " + $("#txtMaterno").val();
               $("#spNoControl").text($("#txtNoControl").val());
               $("#spNombreCompleto").text(nombre);
               $("#spCurp").text($("#txtCURP").val());
               $("#spFechaNacimiento").text($("#txtFechaNacimiento").val());
               $("#spCarrera").text($("#cboCarreraReticula option:selected").text());
               $("#spPeriodo").text($("#cboPeriodo option:selected").text());
               $("#spPlanEstudios").text($("#cboPlanEstudios option:selected").text());

          } else {
               $('.btn-finish').addClass('disabled');
          }
     });

     $('#dtpFecha, #dtpFechaElaboracion').datetimepicker({
          viewMode: 'years',
          format: 'YYYY/MM/DD',
          locale: 'es',
          defaultDate: new Date()
     });

     $("input").blur(function () {
          $(this).get(0).setCustomValidity('');
     });

});
function ValidaCamposStep1() {
     var nombre = $("#txtNombre").val();
     var paterno = $("#txtPaterno").val();
     var materno = $("#txtMaterno").val();
     var curp = $("#txtCURP").val();
     var correo = $("#txtCorreo").val();
     var telefono = $("#txtTelefono").val();
     var celular = $("#txtCelular").val();
     var fecha = $("#txtFechaNacimiento").val();
     var domicilio = $("#txtCalle").val();
     var numero = $("#txtNoDomicilio").val();
     var cp = $("#txtCodPostal").val();
     
     if (ex_nombres.test(nombre))
          $("#txtNombre").get(0).setCustomValidity('');
     else {
          $("#txtNombre").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(paterno))
          $("#txtPaterno").get(0).setCustomValidity('');
     else {
          $("#txtPaterno").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(materno))
          $("#txtMaterno").get(0).setCustomValidity('');
     else {
          $("#txtMaterno").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_correo.test(correo))
          $("#txtCorreo").get(0).setCustomValidity('');
     else {
          $("#txtCorreo").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_telefono.test(telefono))
          $("#txtTelefono").get(0).setCustomValidity('');
     else {
          $("#txtTelefono").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_telefono.test(celular))
          $("#txtCelular").get(0).setCustomValidity('');
     else {
          $("#txtCelular").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (fecha.length == 0) {
          $("#txtFechaNacimiento").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (domicilio.length == 0) {
          $("#txtCalle").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (numero.length == 0) {
          $("#txtNoDomicilio").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     if (cp.length == 0) {
          $("#txtCodPostal").get(0).setCustomValidity('Campo Requerido');
          return false;
     }
     return true;
}
function ValidaCamposStep3() {
     var nombreP = $("#txtNombrePadre").val();
     var nombreM = $("#txtNombreMadre").val();
     var domicilioP = $("#txtDomicilioPadre").val();
     var cpP = $("#txtCodPostalPadre").val();
     var domicilioM = $("#txtDomicilioMadre").val();
     var cpM = $("#txtCodPostalMadre").val();
     if (ex_nombres.test(nombreP))
          $("#txtNombrePadre").get(0).setCustomValidity('');
     else {
          $("#txtNombrePadre").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (ex_nombres.test(nombreM))
          $("#txtNombreMadre").get(0).setCustomValidity('');
     else {
          $("#txtNombreMadre").get(0).setCustomValidity('Formato Incorrecto');
          return false;
     }
     if (domicilioP.length == 0 ||  cpP.length == 0) {
          return false;
     }
     if (domicilioM.length == 0 ||  cpM.length == 0) {
          return false;
     }
     return true;
}
function GuardaDatosAjax() {
     $.ajax({
          url: '../../../Estudiantes/GuardaNuevo',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
     .done(function (data) {
          if (typeof (data.Success) !== "undefined") {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No hay registro de entidades federativas en la base</strong>");
          }
          else {
               $(".alert").addClass('alert-info');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Se guardo correctamente</strong>");
          }
     })
     .fail(function (data) {
          $(".alert").addClass('alert-danger');
          $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Estados de México</strong>");
     })
     .always(function () {
          setTimeout(function () {
               if ($(".alert").hasClass('alert-danger')) {
                    $(".alert").removeClass('alert-danger');
               } else if ($(".alert").hasClass('alert-warning')) {
                    $(".alert").removeClass('alert-warning');
               } else if ($(".alert").hasClass('alert-info')) {
                    $(".alert").removeClass('alert-info');
               } else {
                    $(".alert").removeClass('alert-success');
               }
               $(".alert strong").remove();
               $(".alert").hide();
          }, 2000);
     });
}