function CargaComboPeriodo(top, comboTipo) {
     $.ajax({
          url: '../../../Generales/RegresaComboPeriodo',
          type: 'POST',
          dataType: 'json',
          data: { piTop: top, piFiltro: comboTipo },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existen periodos registrados");
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
               MensajesToastrErrorConexion();
          });
}
function CargaComboPeriodo(top, comboTipo, select) {
     $.ajax({
          url: '../../../Generales/RegresaComboPeriodo',
          type: 'POST',
          dataType: 'json',
          data: { piTop: top, piFiltro: comboTipo },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    MensajesToastr("info", "Solicitud Procesada", "No existen periodos registrados");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $("#cboPeriodo").empty();
                    for (; resultado.data[i];) {
                         if (typeof (select) !== "undefined" && resultado.data[i].Valor.trim() === select.trim()) {
                              $("#cboPeriodo").append('<option value=' + resultado.data[i].Valor + ' selected>' + resultado.data[i].Descripcion + '</option>');
                         }

                         else {
                              $("#cboPeriodo").append('<option value=' + resultado.data[i].Valor + '>' + resultado.data[i].Descripcion + '</option>');
                         }
                         i++;
                    }
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
}
function CargaComboCarreras() {
    $.ajax({
        url: '../../../Generales/RegresaComboCarreras',
        type: 'POST',
        dataType: 'json',
        data: { },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined") {
                MensajesToastr("info", "Solicitud Procesada", "No existen periodos registrados");
            }
            else {
                var resultado = JSON.parse(data);
                var i = 0;
                $("#cboCarreras").empty();
                for (; resultado.data[i];) {
                    $("#cboCarreras").append('<option value=' + resultado.data[i].carrera + '>' + resultado.data[i].nombre_carrera + '</option>');
                    i++;
                }
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}
