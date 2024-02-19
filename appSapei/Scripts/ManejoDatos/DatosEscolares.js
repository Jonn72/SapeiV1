$(document).ready(function () {
     //Carga de Combos
     //Select cboCarreraReticula
     $.ajax({
          url: '../../../Generales/RegresaComboCarreraReticula',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existen carreras registradas</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboCarreraReticula").empty();
                    for (; resultado.data[i];) {
                         $("#cboCarreraReticula").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
                    CargaComboEspecialidad();
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Carreras</strong>");
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
     //cboPeriodo
     $.ajax({
          url: '../../../Generales/RegresaComboPeriodo',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existen periodos registradas</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboPeriodo").empty();
                    for (; resultado.data[i];) {
                         $("#cboPeriodo").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Periodos</strong>");
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

     //cboIngresoAlPlantel
     $.ajax({
          url: '../../../Generales/RegresaComboTipoIngresoPlantel',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp;No existen carreras registradas</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboIngresoPlantel").empty();
                    for (; resultado.data[i];) {
                         $("#cboIngresoPlantel").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Tipo de Ingreso al Plantel</strong>");
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
     //cboPlan de Estudios
     $.ajax({
          url: '../../../Generales/RegresaComboPlanEstudios',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existen planes de estudios registrados</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboPlanEstudios").empty();
                    for (; resultado.data[i];) {
                         $("#cboPlanEstudios").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Plan de Estudios</strong>");
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
     //cboNivelEscolar
     $.ajax({
          url: '../../../Generales/RegresaComboNivelEscolar',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existen nivel escolar registrados</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboNivelEscolar").empty();
                    for (; resultado.data[i];) {
                         $("#cboNivelEscolar").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de nivel escoalar</strong>");
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
    
     $.ajax({
          url: '../../../Generales/RegresaComboEstatusAlumno',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existe estatus de estudiantes</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboEstatusAlumno").empty();
                    for (; resultado.data[i];) {
                         $("#cboEstatusAlumno").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de estatus de alumno</strong>");
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

     $('#cboCarreraReticula').on('change', function () {
          CargaComboEspecialidad();
     })
     
     //Select cboCarreraReticula
     $.ajax({
          url: '../../../Generales/RegresaComboTipoEscuelaProcedencia',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existen carreras registradas</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboTipoEscuelaProcedencia").empty();
                    for (; resultado.data[i];) {
                         $("#cboTipoEscuelaProcedencia").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de Carreras</strong>");
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
});

function CargaComboEspecialidad()
{
     var valor = $("#cboCarreraReticula").val();
     var carrera = valor.split("-")[0];
     var reticula = valor.split("-")[1];
      $.ajax({
           url: '../../../Generales/RegresaComboEspecialidad',
           type: 'POST',
           dataType: 'json',
           data: {psCarrera : carrera, psReticula: reticula},
      })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No existe especialidad de la carreras seleccionada</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboEspecialidad").empty();
                    $("#cboEspecialidad").append('<option value= 0> SIN ESPECIALIDAD </option>');
                    for (; resultado.data[i];) {
                         $("#cboEspecialidad").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         i++;
                    }
               }
          })
          .fail(function (data) {
               $(".alert").addClass('alert-danger');
               $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; Error al cargar lista de especialidades de alumno</strong>");
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