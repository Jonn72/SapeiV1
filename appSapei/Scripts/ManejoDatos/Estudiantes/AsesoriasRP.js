$(document).ready(function () {
    $("#addAsesoria").click(function () {
        RegresaMaxAsesoria($('#txtNumeroAse'), $("#txtIdAsesoria"), $("#txtTemas"), $("#txtSolucion"), $("#cboTipo"), $("#txtIdAsesoria"));
    });
    $("#frmAsesoria").submit(function (evento) {
        var temas = $("#txtTemas").val();
        var solucion = $("#txtSolucion").val();
        var tipo = $("#cboTipo").val();
        var numero = $("#txtNumeroAse").val();
        var proyecto = $("#txtIdPrograma").val();
        var folio = $("#txtFolio").val();
        var idAsesoria = $("#txtIdAsesoria").val() ;
        $.ajax({
            url: '../../../Estudiante/AltaAsesoriaJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { psTemas: temas, psSolucion: solucion, psTipo: tipo, psNumero: numero, piPrograma: proyecto, piFolio: folio, piId: idAsesoria },
        })
                  .done(function (data) {
                      if (typeof (data.Success) !== "undefined" && !data.Success) {
                          MensajesToastr("error", "Solicitud Procesada", "Error al guardar");
                      }
                      else {
                          MensajesToastr("success", "Solicitud Procesada", "Asesoria Registrada");
                          $('#GenerarAsesoria').modal('hide');
                          $('body').removeClass('modal-open');
                          $('.modal-backdrop').remove();
                          $('#BodyPrincipal').load('../../../../Estudiante/AsesoriasRP');
                      }
                  })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        var NumeroAsesoria = data[1];
        RegresaDatosAsesoria(NumeroAsesoria, $("#txtIdAsesoria"), $("#txtTemas"), $('#txtSolucion'), $('#cboTipo'), $('#txtNumeroAse'));
        $('#GenerarAsesoria').modal('show');
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#divCargando").show();
        $("#divPDF").hide();
        $('#divPDF').load('../../../../DocumentosOficiales/RegresaAsesoriaRP', { piNumeroAsesoria: data[1] }, displaySection);
    });
});

function RegresaDatosAsesoria(NumeroAsesoria, txtIdAsesoria, txtTemas, txtSolucion, cboTipo, txtNumeroAse) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaDatosAsesoriaRP',
        type: 'POST',
        dataType: 'json',
        data: { psNumeroAsesoria: NumeroAsesoria },
    })
    .done(function (data) {
        if (typeof (data.Success) !== "undefined") {
            $(txtIdAsesoria).val('');
            $(txtTemas).val('');
            $(txtSolucion).val('');
            $(cboTipo).val('');
            $(txtNumeroAse).val('');
        }
        else {
            var resultado = JSON.parse(data);
            var i = 0;
            $(txtIdAsesoria).val(resultado.data[0].id);
            $(txtTemas).val(resultado.data[0].descripcion);
            $(txtSolucion).val(resultado.data[0].solucion);
            $(txtNumeroAse).val(resultado.data[0].numero_asesoria);
            document.ready = document.getElementById("cboTipo").value = resultado.data[0].tipo;
        }
    })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}

function RegresaMaxAsesoria(txtNumeroAse, txtIdAsesoria, txtTemas, txtSolucion,cboTipo, txtIdAsesoria) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Estudiante/RegresaMaxAsesoriaRP',
        type: 'POST',
        dataType: 'json',
        data: { },
    })
        .done(function (data) {
            if (typeof (data.Success) !== "undefined") {
                $(txtIdAsesoria).val('');
                $(cboTipo).val('')
                $(txtTemas).val('');
                $(txtSolucion).val('');
                $(txtIdAsesoria).val('');
            }
            else {
                var resultado = JSON.parse(data);
                var i = 0;
                $(txtNumeroAse).val(resultado.data[0].numero_asesoria + 1);
                $(cboTipo).val('')
                $(txtTemas).val('');
                $(txtSolucion).val('');
                $(txtIdAsesoria).val(0);
                $('#GenerarAsesoria').modal('show');
            }
        })
        .fail(function (data) {
            MensajesToastrErrorConexion();
        });
    return esValido;
}

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}