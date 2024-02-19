//AUTO COMPLETA NOMBRE de los programas que tiene el departamento seleccionado 
function autocompletaprogramasdepartamento() {
    var loDepartamento = $("#Departamento").val();
    if (loDepartamento.trim().length > 0) {             
        RegresaDatosPrograma(loDepartamento, $("#cboPrograma"));
        $("#Responsabletxt").val('');
        $("#CargoResponsableProgramatxt").val('');
        $("#Correotxt").val('');
        $("#Departamentotxt").val('');
        $("#Objetivotxt").val('');
        $("#TipoProgramatxt").val('');
    }
}

//LISTA DESPLEGABLE DE LOS PROGRAMAS DEL DEPARTAMENTO SELECCIONADO
function RegresaDatosPrograma(loDepartamento, cboPrograma) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Vinculacion/RegresaNombreDepartamentoSS',
        type: 'POST',
        dataType: 'json',
        data: { psDepartamento: loDepartamento },
    })
    .done(function (data) {
        if (typeof (data.Success) !== "undefined") {
            cboPrograma.empty();
            MensajesToastr("info", "Solicitud Procesada", "Este departamento no tiene programa registrado");
            $("#cboPrograma").val('');
            $("#Responsabletxt").val('');
            $("#CargoResponsableProgramatxt").val('');
            $("#Correotxt").val('');
            $("#Departamentotxt").val('');
            $("#Objetivotxt").val('');
            $("#TipoProgramatxt").val('');
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

//AUTO COMPLETA LOS DATOS DEL PROGRAMA SELECCIONADO 
function autocompletadatosprograma() {
    var loPrograma = $("#cboPrograma").val();
    if (loPrograma != null) {
        RegresaDatosProgramaDepartamentoSS(loPrograma, $("#Responsabletx"), $("CargoResponsableProgramatxt"), $("Correotxt"), $("Departamentotxt"), $("TipoProgramatxt"), $("Objetivotxt"));
    }
}

//REGRESA LOS DATOS DEL PROGRAMA SELECCIONADO
function RegresaDatosProgramaDepartamentoSS(psPrograma, Responsabletxt, CargoResponsableProgramatxt, Correotxt, Departamentotxt, TipoProgramatxt, Objetivotxt) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Vinculacion/RegresaDatosProgramaDepartamentoSS',
        type: 'POST',
        dataType: 'json',
        data: { psPrograma: psPrograma },
    })
           .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                   $("#cboPrograma").val('');
                   $(Responsabletxt).val('');
                   $(CargoResponsableProgramatxt).val('');
                   $(Correotxt).val('');
                   $(Departamentotxt).val('');
                   $(Objetivotxt).val('');
                   $(TipoProgramatxt).val('');

                   MensajesToastr("error", "Solicitud Procesada", "Este Programa no esta registrado en la base");
               }
               else {
                   var resultado = JSON.parse(data);
                   var i = 0;
                   $("#Responsabletxt").val(resultado.data[0].responsable);
                   $("#CargoResponsableProgramatxt").val(resultado.data[0].cargo_responsable);
                   $("#Correotxt").val(resultado.data[0].correo_titular);
                   $("#Objetivotxt").val(resultado.data[0].objetivo);
                   document.ready = document.getElementById("TipoProgramatxt").value = resultado.data[0].id_tipo_programa;
               }
           })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}

//FUNCION DEL BOTON NUEVO, QUE ABRE OTRO MODAL DENTRO DEL MISMO Y AL GUARDAR LIMPIA LOS CAMPOS  
$(document).on('click', '#agregar_departamento', function () {
    $('#ModalAgregarPrograma').modal('show');
    $("#Departamentotxt").val("");
    $("#txtPrograma").val("");
    $("#txtResponsable").val("");
    $("#txtCargoResponsablePrograma").val("");
    $("#txtCorreo").val("");
    $("#txtObjetivo").val("");
    $("#selectTipoPrograma").val("");
});

//FUNCIÓN PARA ASIGNAR UN DATO DE LA ETIQUETA SELECT EN UN TEXT (DEPARTAMENTO)
var mostrarDepartamento = function (x) {
    document.getElementById('txtDepartamento').value = x;
}

var mostrarPrograma = function (y) {
    document.getElementById('TipoProgramatxt').value = y;
}

//FUNCIÓN PARA QUE LETRAS QUE INGRESEN POR TECLADO SE PONGAN EN MAYUSCULAS
function mayus(e) {
    e.value = e.value.toUpperCase();
}

//FUNCIÓN PARA ASIGNAR EL MISMO NUMERO DE CONTROL
$("#txtControl").keyup(function () {
    var value = $(this).val();
    $("#Controltxt").val(value);
});

//FUNCION DEL BOTON ASIGNAR
$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-warning', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#Controltxt").val(data[2]);
        $('#ModalAsignarPrograma').modal('show');//abrir modal            
    });

    //FUNCION DEL BOTON VISUALIZAR CARTA DE PRESENTACIÓN INTERNO DE SERVICIO SOCIAL
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {        
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        event.preventDefault();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaCartaPresentacionSS', { psNoControl: data[2], psFolio: data[1] }, displaySection());

    });
});

//FUNCION PARA VALIDAR NUMERO DE CONTROL
$("#frmSolicitudInterno").submit(function (evento) {
    var control = $("#Controltxt").val();
    var programa = $("#cboPrograma").val();
    $.ajax({
        url: '../../../Vinculacion/ActualizarProgramaInternoSSJson',
        type: 'POST',
        dataType: 'json',
        data: {
            piPrograma: programa, psControl: control
        },
    })
          .done(function (data) {
              if (typeof (data.Success) !== "undefined" && !data.Success) {

                  MensajesToastr("error ", "Solicitud Procesada", "Error al guardar");
              }
              else {
                  MensajesToastr("success", "Solicitud Procesada", "Carta de Aceptación registrada");
                  $('#ModalAceptacion').modal('hide'); // cerrar
                  $('body').removeClass('modal-open');
                  $('.modal-backdrop').remove();
                  $('#BodyPrincipal').load('../../../../Vinculacion/CartaPresentacionInternoSS');
              }
          })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
    evento.preventDefault();
});

//FUNCION PARA REGISTRAR UN NUEVO PROGRAMA POR PARTE VINCULACION
$("#frmAltaPrograma").submit(function (evento) {
        var departamento = $("#txtDepartamento").val();
        var programa = $("#txtPrograma").val();
        var responsable = $("#txtResponsable").val();
        var cargoresponsable = $("#txtCargoResponsablePrograma").val();
        var correo_titular = $("#txtCorreo").val();
        var objetivo = $("#txtObjetivo").val();
        var tipoprograma = $("#selectTipoPrograma").val();

        $.ajax({
            url: '../../../Vinculacion/AltaProgramaInternoSSJson',
            type: 'POST',
            dataType: 'json',
            data: {
                psPrograma: programa, psCorreo: correo_titular, psResponsable: responsable, psCargoResponsablePrograma: cargoresponsable, psDepartamento: departamento, psTipoprograma: tipoprograma, psObjetivo: objetivo
            },
        })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined" && !data.Success) {
                MensajesToastr("error", "Solicitud Procesada", "El programa ya esta registrado en el departamento");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Programa Registrado");
                $('#ModalAgregarPrograma').modal('hide'); // cerrar   
                RegresaDatosPrograma(departamento, $("#cboPrograma"));
                //CambiaValorTabla(fin, 3, 0);
                    RegresaDatosPrograma(departamento, $("#cboPrograma"));      
            }
        })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
    evento.preventDefault();
    });


function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}
