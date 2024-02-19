var $rowTbl;
$(document).ready(function () {
    $('#datatable-buttons').DataTable().column(5).visible(false);
    $('#btnGuardar').attr('disabled', true);
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        $('#btnGuardar').attr('disabled', false);
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        
        $("#txtClaveIsbn").val(data[1]);
        $("#txtNoPaginas").val(data[2]);
        $("#txtCapitulos").val(data[3]);
        $("#hidId").val(data[0]);
        $("#hidIdMat").val(data[5]);
        $("#txtEdicion").val(data[6]);
        $("#txtClasificacion").val(data[7]);
    });
    $("#frmLibros").submit(function (event) {
        var id_mat_bib = $("#hidIdMat").val();
        var clave = $("#txtClaveIsbn").val();
        var nPaginas = $("#txtNoPaginas").val();
        var capitulos = $("#txtCapitulos").val();
        var clasificacion = $("#txtClasificacion").val();
        var id = $("#hidId").val();
        var edicion = $("#txtEdicion").val();
        $.ajax({
            url: '../../../CentroInformacion/EditaLibroJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piId: id, piId_mat_bib: id_mat_bib,
                psISBN: clave, piNo_paginas: nPaginas,
                piCapitulos: capitulos, psEdicion: edicion,
                psClasificacion: clasificacion
            },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Libros');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });
});
