
var $rowTbl;

//FUNCIÓN PARA QUE LETRAS QUE INGRESEN POR TECLADO SE PONGAN EN MAYUSCULAS
function mayus(e) {
    e.value = e.value.toUpperCase();
}

$(document).ready(function () {

    // scripts para nueva dependencia
    $("#NewDependencia").click(function () {
        document.getElementById("txtRfc").disabled = false;
        $("#txtRfc").val("");
        $("#txtDependencia").val("");
        $("#txtTitular").val("");
        $("#txtCargoTitular").val("");
        $("#txtTelefono").val("");
        $("#txtCalle").val("");
        $("#txtNoDomicilio").val("");
        $("#txtCodPostal").val("");
        $("#cboColonia").val("");
        $("#txtEstado").val("");
        $("#txtCiudad").val("");
        $('#MyModal').modal('show'); // abrir
    });

    $("#btnCerrar").click(function () {     //agregue
        $('#modCargando').modal('hide'); // cerrar  //agregue
    });

    // scripts para editar dependencia
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        document.getElementById("txtRfc").disabled = true; //agregue
        $("#txtRfc").val(data[1]);
        $("#txtDependencia").val(data[2]);
        $("#txtTitular").val(data[3]);
        $("#txtCargoTitular").val(data[4]);
        $("#txtTelefono").val(data[5]);
        $("#txtCalle").val(data[6]);
        $("#txtNoDomicilio").val(data[7]);
        $("#txtCodPostal").val(data[9]);
        autocompleta('Estudiante', data[9]);//agregre
        setTimeout(1000);
        $("#cboColonia").val(data[8]);
        $('#MyModal').modal('show'); // abrir
    });

    // scripts para eliminar dependencia
    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#RfcEtxt").val(data[1]);
        $('#ModalEleminar').modal('show'); // abrir
    });

    //necessito agregar un frmagregar programa
    $("#frmEleminarDependencia").submit(function (evento) {
        var rfc = $("#RfcEtxt").val();
        $.ajax({
            url: '../../../Vinculacion/EliminaDependenciaJsonSS',
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
                      //CambiaValorTabla(fin, 3, 0);
                      $('#BodyPrincipal').load('../../../../Vinculacion/AltaDependenciaSS');//agregue
                  }
              })
          .fail(function (data) {
              MensajesToastrErrorConexion();
          });
        evento.preventDefault();
    });

    $("#frmAltaDependencia").submit(function (evento) {
        var dependencia = $("#txtDependencia").val();
        var rfc = $("#txtRfc").val();
        var calle = $("#txtCalle").val();
        var nodomicilio = $("#txtNoDomicilio").val();
        var codigopostal = $("#txtCodPostal").val();
        var titular = $("#txtTitular").val();
        var cargotitular = $("#txtCargoTitular").val();
        var telefono = $("#txtTelefono").val();
        var colonia = $("#cboColonia").val();

        $.ajax({
            url: '../../../Vinculacion/AltaDependenciaJsonSS',
            type: 'POST',
            dataType: 'json',
            data: {
                psDependencia: dependencia, psRfc: rfc, psTitular: titular, psCargotitular: cargotitular,
                psTelefono: telefono, psCalle: calle, psNodomicilio: nodomicilio, psCodigopostal: codigopostal,
                psColonia: colonia
            },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("error", "Solicitud Procesada", "Error al Guardar");
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Dependencia Registrada");                      
                      $('#MyModal').modal('hide'); // cerrar
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Vinculacion/AltaDependenciaSS');//agregue
                      //CambiaValorTabla(fin, 3, 0);
                  }
              })
              .fail(function (data) {
                  MensajesToastrErrorConexion();
              });
        evento.preventDefault();
    });

});

