var $rowTbl;

function RegresaDatosRFC(psRFC, txtMision, txtDependencia, txtTitular, txtCargoTitular, txtTelefono) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Vinculacion/RegresaDatosDependenciaRP',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: psRFC },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {
                 $("#txtRFC").val('');
                 $(txtMision).val('');
                 $(txtDependencia).val('');
                 $(txtTitular).val('');
                 $(txtCargoTitular).val('');
                 $(txtTelefono).val('');

                 $("#txtCalle").val('');
                 $("#txtNoDomicilio").val('');
                 $("#txtCodPostal").val('');
                 $("#cboColonia").val('');
                 $("#txtCiudad").val('');
                 $("#txtEstado").val('');
                 MensajesToastr("error", "Solicitud Procesada", "Este RFC no esta registrado en la base");
             }
             else {
                 var resultado = JSON.parse(data);
                 var i = 0;
                 $(txtMision).val(resultado.data[0].mision);
                 document.ready = (document.getElementById("cboGiro").value = resultado.data[0].giro);
                 $(txtDependencia).val(resultado.data[0].dependencia);
                 $(txtTitular).val(resultado.data[0].titular);
                 $(txtCargoTitular).val(resultado.data[0].puesto_cargo);
                 $(txtTelefono).val(resultado.data[0].telefono);

                 $("#txtCalle").val(resultado.data[0].domicilio);
                 $("#txtNoDomicilio").val(resultado.data[0].numero);
                 $("#txtCodPostal").val(resultado.data[0].cod_post);
                 autocompleta('Estudiante', $("#txtCodPostal"));

                 var val = resultado.data[0].id;
                 $('#cboColonia option[value=' + val + ']').attr('selected', true);

                // document.ready = (document.getElementById("cboColonia").value = resultado.data[0].id);              
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}

function RegresaDatosOficinasRFC(psRFC) {

    $.ajax({
        sync: false,
        url: '../../../Vinculacion/RegresaDatosDependenciaOficinasRP',
        type: 'POST',
        dataType: 'json',
        data: { psRFC: psRFC },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined") {
                $("#txtRFCOficina").val('');
                $("#txtOficina").val('');
                $("#txtResponsable").val('');
                MensajesToastr("error", "Solicitud Procesada", "Este RFC no esta registrado en la base");
            }
            else {
                var resultado = JSON.parse(data);
                console.log("Valor");
                console.log(resultado);
                var i = 0;
                $("#cboOficinasRegistros").empty();

                for (; resultado.data[i];) {
                    $("#cboOficinasRegistros").append('<option value=' + resultado.data[i].id + '>' + resultado.data[i].oficina + '</option>');
                    i++;
                }

                //$.each(obj, function (index, value) {
                //    cboColonia.append('<option value=' + value.id + '>' + value.oficina + '</option>');
                //});
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}

$(document).ready(function () {
    $("#NewDependencia").click(function () {        
        document.getElementById("txtRfc").disabled = false;
        $("#txtRfc").val("");
        document.ready = document.getElementById("cboColonia").value = "";
        $("#txtmision").val("")
        $("#txtDependencia").val("");
        $("#txtTitular").val("");
        $("#txtCargoTitular").val("");
        $("#txtTelefono").val("");
        $("#txtCalle").val("");
        $("#txtNoDomicilio").val("");
        $("#txtCodPostal").val("");
        $("#txtCiudad").val("");
        $("#txtEstado").val("");
        $("#cboColonia").val("");
        $("#cboGiro").val("");
        $('#MyModal').modal('show'); // abrir
    });

    $("#btnCerrar").click(function () {
        $('#modCargando').modal('hide'); // cerrar
    });


    $('textarea#txtmision').on('keyup', function () {
        var maxlen = $(this).attr('maxlength');
        var length = $(this).val().length;
        if (length > (maxlen - 5)) {
            $('#textarea_message').text('longitud máxima de ' + maxlen + ' caracteres solamente!')
        }
        else {
            $('#textarea_message').text('');        }
    });

    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        document.getElementById("txtRfc").disabled = true;
        var loRFC = data[1];
        if (loRFC.trim().length > 0) {
            $("#txtRfc").val(data[1]);
            RegresaDatosRFC(loRFC, $("#txtmision"), $('#txtDependencia'), $('#txtTitular'), $('#txtCargoTitular'), $('#txtTelefono'));
        }
        $('#MyModal').modal('show'); // abrir
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        var loRFC = data[1];
        $("#txtRFCOficina").val('');
        $("#txtOficina").val('');
        $("#txtResponsable").val('');
        if (loRFC.trim().length > 0) {
            $("#txtRFCOficina").val(data[1]);
            RegresaDatosOficinasRFC(loRFC);
        }
        $('#ModalOficina').modal('show'); // abrir

    });
    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#RfcEtxt").val(data[1]);
        $('#ModalEleminar').modal('show'); // abrir
    });

    $("#frmAltaDependencia").submit(function (evento) {
        var dependencia = $("#txtDependencia").val();
        var giro = $("#cboGiro").val();
        var mision = $("#txtmision").val();
        var rfc = $("#txtRfc").val();
        var calle = $("#txtCalle").val();
        var nodomicilio = $("#txtNoDomicilio").val();
        var colonia = $("#cboColonia").val()
        var codigopostal = $("#txtCodPostal").val();
        var titular = $("#txtTitular").val();
        var cargotitular = $("#txtCargoTitular").val();
        var telefono = $("#txtTelefono").val();

        $.ajax({
            url: '../../../Vinculacion/AltaDependenciaJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psDependencia: dependencia, psGiro: giro, psMision: mision, psRfc: rfc, psTitular: titular, psCargotitular: cargotitular,
                psTelefono: telefono, psCalle: calle, psNodomicilio: nodomicilio, psIdCodigo: colonia
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("error", "Solicitud Procesada", "La Dependencia ya existe");
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Dependencia Registrada");
                      $('#MyModal').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Vinculacion/AltaDependenciaRP');
                  }
              })
              .fail(function (data) {
                  MensajesToastrErrorConexion();
              });
        evento.preventDefault();
    });

    $("#btnGuardarOficina").click(function (evento) {
        var rfc = $("#txtRFCOficina").val();
        var oficina = $("#txtOficina").val();
        var responsable = $("#txtResponsableOficina").val();

        $.ajax({
            url: '../../../Vinculacion/AltaDependenciaOficinaJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psRFC: rfc, psOficina: oficina, psResponsable: responsable
            },
        })
            .done(function (data) {
                if (typeof (data.Success) !== "undefined" && !data.Success) {
                    MensajesToastr("error", "Solicitud Procesada", "La Dependencia ya existe");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Dependencia Registrada");
                    $('#ModalOficina').modal('hide'); // cerrar
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        evento.preventDefault();
    });

    $("#frmEleminarDependencia").submit(function (evento) {
        var rfc = $("#RfcEtxt").val();
        $.ajax({
            url: '../../../Vinculacion/EliminaDependenciaJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psRfc: rfc
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("error", "Solicitud Procesada", "Error al Eliminar");
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Dependencia Eliminada");
                      $('#ModalEleminar').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Vinculacion/AltaDependenciaRP');
                      //CambiaValorTabla(fin, 3, 0);
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
    });
});

