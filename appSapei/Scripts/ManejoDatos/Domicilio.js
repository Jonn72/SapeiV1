function autocompleta(tipo, valor)
{
     var loCP = $("#txtCodPostal").val();
     if (loCP.trim().length > 0) {
          if (tipo === "Estudiante" || tipo === "Personal" || tipo === "Aspirante")
               RegresaDatosCodigoPostal(loCP, $("#cboColonia"), $('#txtCiudad'), $('#txtEstado'), valor);
          else if (tipo === "Padre" )
               RegresaDatosCodigoPostal(loCP, $("#cboColoniaPadre"),  $('#txtCiudadPadre'), $('#txtEstadoPadre'), valor);
          else if (tipo === "Madre")
               RegresaDatosCodigoPostal(loCP, $("#cboColoniaMadre"),  $('#txtCiudadMadre'), $('#txtEstadoMadre'), valor);
          else if (tipo === "Empresa")
               RegresaDatosCodigoPostal(loCP, $("#cboColoniaMadre"),  $('#txtCiudadMadre'), $('#txtEstadoMadre'), valor);
     }
}

function RegresaDatosCodigoPostal(psCodigoPostal, cboColonia,  txtCiudad, txtEstado, valor)
{
     var esValido = false;
     $.ajax({
          sync:false,
          url: '../../../Generales/RegresaDomicilio',
          type: 'POST',
          dataType: 'json',
          data: { psCodigoPostal: psCodigoPostal },
     })
          .done(function (data) {
               if (typeof (data.Success) !== "undefined") {
                    cboColonia.empty();
                    $("#txtCodPostal").val('');
                   $(txtCiudad).text('');
                   $(txtCiudad).val('');
                    $(txtEstado).text('');
                   $(txtEstado).val('');

                   MensajesToastr("error", "Solicitud Procesada", "Este Código Postal no esta registrado en la base");
               }
               else {
                    var resultado = JSON.parse(data);
                    var i = 0;
                    $(txtCiudad).val(resultado.data[0].Ciudad);
                   $(txtEstado).val(resultado.data[0].Entidad);
                   $(txtCiudad).text(resultado.data[0].Ciudad);
                   $(txtEstado).text(resultado.data[0].Entidad);
                    cboColonia.empty();

                    for (;resultado.data[i];) {
                         cboColonia.append('<option value=' + resultado.data[i].id + '>' + resultado.data[i].Colonia + '</option>');
                         i++;
                    }

                    var vali = $("#hidColonia").val();
                   if ($.trim(vali).length != 0)
                         $('#cboColonia option[value=' + vali + ']').attr('selected', 'selected');
                      
               }
          })
          .fail(function (data) {
               MensajesToastrErrorConexion();
          });
     return esValido;
}
