$(document).ready(function () {
    //oculta campo id de la tabla
    $('#datatable-buttons').DataTable().column(0).visible(false);
    $('#datatable-buttons').DataTable().column(1).visible(false);
    $('#datatable-buttons').DataTable().rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        var selected = data[6];
        if (selected === 'True')
            data[6] = '<span class="glyphicon glyphicon-ok"></span>';
        else
            data[6] = '<span class="glyphicon glyphicon-remove"></span>';
        this.data(data);
    });
   
    $("#frmAdeudos").submit(function (event) {
        var usuario = $("#txtUsuario").val();
        $('#BodyPrincipal').load('../../../../CentroInformacion/Adeudos', 'psUsuario=' + usuario);
        event.preventDefault();
    });

});