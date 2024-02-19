$(document).ready(function () {    
    $('#datatable-buttons tbody').on('click', 'a.btn-warning', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $('#txtNoControl').val(data[1]);
        $('#txtNombre').val(data[2]);
        RegresaDatosOficinasRFC($("#cboRfc").val());

        $('#txtFolio').val('');
        $('#GenerarExterno').modal('show');
    });

    $("#btnGenerar").click(function () {
        var control = $("#txtNoControl").val();
        var dependencia = $("#cboRfc").val();
        var folio = $("#txtFolio").val();
        if (folio.trim() == "") {
            MensajesToastr("info", "Solicitud Procesada","Debes completar los campos requeridos");
            return
        }
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaCartaPresentacionRP', { psNoControl: control, psDependencia: dependencia, psFolio: folio }, displaySection);
        $('#GenerarExterno').modal('hide');
    });

    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function (event)
    {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        document.getElementById('foliotext').innerHTML = data[2];
        document.getElementById('noControltext').innerHTML = data[4];
        $("#txtFolio").val(data[2]);
        $("#txtnoControl").val(data[4]);
        $('#cancelaFolio').modal('show'); // abrir
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        document.getElementById('textfolio').innerHTML = data[2];
        document.getElementById('textnoControl').innerHTML = data[4];
        $("#Foliotxt").val(data[2]);
        $("#noControltxt").val(data[4]);
        $('#activaFolio').modal('show'); // abrir
    });

    $("#frmCancelaFolio").submit(function (evento) {
        var folio = $("#txtFolio").val();
        var noControl = $("#txtnoControl").val();
        $.ajax({
            url: '../../../Vinculacion/CancelaFolioJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psFolio: folio, psnoControl : noControl
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Solicitud Cancelada");
                      $('#BodyPrincipal').load('../../../../Vinculacion/SolicitudesRP');    
                      $('#cancelaFolio').modal('hide'); // abrir
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      //CambiaValorTabla(fin, 3, 0);
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
        $('#cancelaFolio').modal('hide'); // cerrar
    });
    $("#frmActivaFolio").submit(function (evento) {
        var folio = $("#Foliotxt").val();
        var noControl = $("#noControltxt").val();
        $.ajax({
            url: '../../../Vinculacion/ActivaFolioJsonRP',
            type: 'POST',
            dataType: 'json',
            data: {
                psFolio: folio, psnoControl: noControl
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Solicitud Activada");
                      $('#BodyPrincipal').load('../../../../Vinculacion/SolicitudesRP');
                      $('#activaFolio').modal('hide'); // abrir
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      //CambiaValorTabla(fin, 3, 0);
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
        $('#activaFolio').modal('hide'); // cerrar
    });
});

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}

$('#cboRfc').on('change', function (e) {
    var rfc = this.value;
    RegresaDatosOficinasRFC(rfc);
    e.preventDefault();
});

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
                
                var i = 0;
                $("#cboOficinasRegistros").empty();

                for (; resultado.data[i];) {
                    $("#cboOficinasRegistros").append('<option value=' + resultado.data[i].id + '>' + resultado.data[i].oficina + '</option>');
                    i++;
                }
                if (i > 0)
                    $("#divOficinas").show()
                //$.each(obj, function (index, value) {
                //    cboColonia.append('<option value=' + value.id + '>' + value.oficina + '</option>');
                //});
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
}
