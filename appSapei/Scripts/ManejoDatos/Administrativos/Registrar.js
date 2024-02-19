var $rowTbl;
$(document).ready(function () {
    $('#btnEliminar').off();
    $('#datatable-buttons').DataTable().column(2).visible(false);
    $('#datatable-buttons').DataTable().rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        data[0] = atob(data[0]);
        switch (data[1]) {
            case "Admin":
                data[1] = "Administrativos"
                break;
            case "Docentes":
                data[1] = "Docentes"
                break;
            case "AdDoc":
                data[1] = "Administrativo/Docente"
                break;
        }

        this.data(data);
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#hidId").val(data[2]);
        switch (data[1]) {
            case "Administrativos":
                $("#sGrupo").val("Admin");
                break;
            case "Docentes":
                break;
            case "Administrativo/Docente":
                $("#sGrupo").val("AdDoc");
                break;
        }
        $('#sGrupo').trigger('change');

    });
    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function () {
        $rowTbl = $(this).parents("tr");
        $("#myModal").modal();
    });
    function habilitaUsuario() {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        var rfc = data[0];
        $.ajax({
            url: "../../../AplicacionAdministrativos/HabilitaUsuario",
            type: "POST",
            dataType: "json",
            data: { psRfc: rfc }
        }).done(function (data) {
            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            } else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $('#BodyPrincipal').load('../../../../AplicacionAdministrativos/Registrar');
            }
        });
    }
    $('#datatable-buttons tbody').on('click', 'a.btn-warning', habilitaUsuario);
    
    $('#datatable-buttons tbody').on('click', 'a.btn-success',habilitaUsuario);
    $('#btnEliminar').click(function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var data = $table.row($rowTbl).data();

        $.ajax({
            url: '../../../AplicacionAdministrativos/EliminaUsuario',
            type: 'POST',
            dataType: 'json',
            data: { psUsuario: data[0] }
        }).done(function (data) {
            if (typeof (data.Success) === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            }else{
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $('#BodyPrincipal').load('../../../../AplicacionAdministrativos/Registrar');
            }
        }).fail(function (data) {
            MensajesToastrErrorConexion();
        });

    });
    $('#sGrupo').select2({ theme: "classic" });
    $("#frmRegistro").submit(function (event) {
        var usuario = $("#hidId").val();
        var grupo = $("#sGrupo").val();
        event.preventDefault();
        //var grupo = $("#sGrupo").val();
        $.ajax({
            url: '../../../AplicacionAdministrativos/GuardaUsuario',
            type: 'POST',
            dataType: 'json',
            data: { piIdGrupo: usuario, psGrupo: grupo },
        }).done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../AplicacionAdministrativos/Registrar');
                }
            }).fail(function (data) {
                MensajesToastrErrorConexion();
            });
        
    });
});



