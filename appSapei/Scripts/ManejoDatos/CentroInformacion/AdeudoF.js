var rowTbl;
$(document).ready(function () {
    //oculta campo id de la tabla
    $('#datatable-buttons').DataTable().column(0).visible(false);
    $('#datatable-buttons').DataTable().column(1).visible(false);

    $("#fldUsuario").show();
    $("#divFecha").hide();
    $('#sTipoSol').select2({ theme: "classic" });
    $('#datatable-buttons').DataTable().rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        var selected = data[6];
        if (selected === 'True')
            data[6] = '<span class="glyphicon glyphicon-ok"></span>';
        else
            data[6] = '<span class="glyphicon glyphicon-remove"></span>';
        this.data(data);
    });
    $("#txtUsuario").focus((e) => {
        $('body,html').css('padding-right', "0px");
    });
    $('#sTipoSol').on('select2:select', function () {
        
        switch ($('#sTipoSol').val()) {
            case "Alumno":
                $("#fldUsuario").show();
                $("#divFecha").hide();
                break;
            case "Fecha":
                $("#fldUsuario").hide();
                $("#divFecha").show();
                break;
            case "Todos":
                $("#fldUsuario").hide();
                $("#divFecha").hide();
                break;

        }
    });
    $("#frmAdeudos").on('submit',function (event) {
        
        switch ($('#sTipoSol').val()) {
            case "Alumno":
                var usuario = $("#txtUsuario").val();
                $('#BodyPrincipal').load('../../../../CentroInformacion/FinancierosAdeudos', 'psUsuario=' + usuario);
                break;
            //case "Fecha":
            //    datos = {
            //        psFechaI: $("txtFechaI").val(),
            //        psFechaF: $("txtFechaF").val()
            //    }
            //    $('#BodyPrincipal').load('../../../../CentroInformacion/FinancierosAdeudosFecha',datos);
            //    break;
            case "Todos":
                $('#BodyPrincipal').load('../../../../CentroInformacion/FinancierosAdeudosTodos');
                break;
        }
        event.preventDefault();
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        $("#mdlAdeudo").modal("show");
        var $rowParent = $(this).parents("tr");//row child
        rowTbl = $rowParent;
    });
    $("#btnPagar").on('click', (e) => {
        pagar('Pagar');
    });
    $("#btnCancelar").on('click', (e) => {
        pagar('Cancelar');
    });
    $("#btnCondonar").on('click', (e) => {
        pagar('Condonar');
    });

    function pagar(psStatus) {
        var $table = $('#datatable-buttons').DataTable();
        var data = $table.row(rowTbl).data();
        var id = data[0];
        var usuario = data[2];
        
        $("#mdlAdeudo").modal("toggle");
        
        $.ajax({
            url: '../../../CentroInformacion/LiquidaAdeudoJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piIdAdeudo: id, psStatus: psStatus,
            },
        })
            .done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    console.log(data);
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {

                 MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../CentroInformacion/FinancierosAdeudos', 'psUsuario=' + usuario, function (rs, st, jq) {
                        $("#modCargando").modal("hide");
                        $("#mdlAdeudo").modal("hide");
                        $('div.modal-backdrop').removeClass('modal-backdrop');
                    });

                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
    }
});