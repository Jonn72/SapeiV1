var $rowTbl;

$(document).ready(function () {

    
    var fecha = new Date();
    var dia = ("0" + fecha.getDate()).slice(-2);
    var mes = ("0" + (fecha.getMonth() + 1)).slice(-2);
    var hoy = fecha.getFullYear() + "-" + (mes) + "-" + (dia);
    $('#datatable-buttons').DataTable().column(4).visible(false);

    $('#btnGuardar').attr('disabled', true);

    $('#txtFecha').val(hoy);
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        $('#btnGuardar').attr('disabled', false);
        var $table = $('#datatable-buttons').DataTable();
        var $rowParent = $(this).parents("tr");//row child
        var $row = $rowParent;
        var data = $table.row($row).data();
        $("#hidId").val(data[0]);
        
        data[1] = data[1].split(" ")[0];
        data[1] = data[1].replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");
        $("#txtFecha").val(data[1]);
        $("#txtLugar").val(data[2]);
        $("#hidIdMaterial").val(data[4]);
    });
    $("#frmMemorias").submit(function (event) {
        var material = $("#hidIdMaterial").val();
        var fecha = $("#txtFecha").val();
        var lugar = $("#txtLugar").val();
        var id = $("#hidId").val();
        $.ajax({
            url: '../../../CentroInformacion/EditaMemoriasJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piId: id, piMaterial: material, psFecha: fecha, psLugar: lugar
            },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Memorias');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });

    

});
