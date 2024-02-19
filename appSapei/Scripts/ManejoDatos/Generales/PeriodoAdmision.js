$(document).ready(function () {
     //Carga de Combos
     //Select cboCarreraReticula
     $.ajax({
          url: '../../../Generales/RegresaComboPeriodoAdmision',
          type: 'POST',
          dataType: 'json',
          data: {},
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    $(".alert").addClass('alert-danger');
                    $(".alert").html("<strong><span class='lnr lnr-cross-cirlce'></span>&nbsp; No hay periodos registrados</strong>");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboPeriodosAdmision").empty();
                    for (; resultado.data[i];) {
                         $("#cboPeriodosAdmision").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
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
});