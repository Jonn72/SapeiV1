$("#frmTutores").submit(function (event) {
    var rfc = $("#txtRFC").val();
    var estatus = "";
    if ($("#chkActivo").is(':checked')) {
        estatus = true
    }
    else {
        estatus = false;
    }

    $.ajax({
        url: '../../../DesarrolloAcademico/GuardaTutor',
        type: 'POST',
        dataType: 'json',
        data: { psRFC:rfc, pbEstatus: estatus},
    })
    .done(function (data) {
        if (typeof (data.Success) === "undefined" || !data.Success) {
             MensajesToastr("info", "Solicitud Procesada", "El RFC no corresponde a ningun docente activo");
        }
        else {
             MensajesToastr("success", "Solicitud Procesada", "Se Guardo Correctamente");
        }
    })
    .fail(function (data) {
         MensajesToastrErrorConexion();
    })
    .always(function () {
        $('#BodyPrincipal').load('../../../../DesarrolloAcademico/Tutores');
    });
    event.preventDefault();
});
$('#datatable-buttons tbody').on('dblclick', 'tr', function () {
     var data = $('#datatable-buttons').DataTable().row(this).data();

    var rfc;
    var estatus
    rfc = data[0];
    estatus = data[3];
  
    $("#txtRFC").val(rfc);
   if (estatus === "ACTIVO") {
    $("#chkActivo").attr('checked', true);
   }
   else {
       $("#chkActivo").attr('checked', false);
   }
});

$("#txtRFC").keyup(function () {
     var valor = $(this).val();
     $(this).val(valor.toUpperCase());
});