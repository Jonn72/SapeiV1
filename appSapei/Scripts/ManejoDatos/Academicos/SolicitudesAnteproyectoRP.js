function RegresaDatosAnteproyecto(psProyecto, Comentariostxt) {
    var esValido = false;
    $.ajax({
        sync: false,
        url: '../../../Academicos/RegresaDatosAnteproyectoRP',
        type: 'POST',
        dataType: 'json',
        data: { piId: psProyecto },
    })
         .done(function (data) {
             if (typeof (data.Success) !== "undefined") {
                 $(Delimitaciontxt).val('');
                 $(ObjetivoGtxt).val('');
                 $(tags_3).val('');
                 $(Duraciontxt).val('');
                 $(tags_4).val('');
                 $(Justificaciontxt).val('');
                 $(Comentariostxt).val('');
                 MensajesToastr("error", "Solicitud Procesada", "Al solicitar los datos");
             }
             else {
                 var resultado = JSON.parse(data);
                 var i = 0;
                 document.getElementById("delimitaciones").innerHTML = resultado.data[0].delimitaciones;
                 document.getElementById("objetivoG").innerHTML = resultado.data[0].objetivo_general;
                 var objetivosE = resultado.data[0].objetivo_especificos;
                 var array = objetivosE.split(",");
                 var objetivos = " ";
                 for (var i = 0; i < array.length; i++) {
                     var objetivo = "- " + array[i] + "<br />";
                      objetivos = objetivos + objetivo;
                 }
                 document.getElementById("objetivos").innerHTML = objetivos;
                 var duracion = resultado.data[0].duracion;
                 if (duracion != 0) {
                     document.getElementById("Duracion").innerHTML = duracion;
                     $("#Cd").css("display", "block");
                 }                 
                 var actividad = resultado.data[0].actividades;
                 var arrayactividades = actividad.split(",");
                 var actividades = " ";
                 for (var i = 0; i < arrayactividades.length; i++) {
                     var act = "- " + arrayactividades[i] + "<br />";
                     actividades = actividades + act;
                 }
                 document.getElementById("actividades").innerHTML = actividades;
                 document.getElementById("justificacion").innerHTML = resultado.data[0].justificacion;
                 var url = ('data:image/jpg;base64,' + resultado.data[0].ubicacion);
                 document.getElementById("mapa").src = url;
                 var comentarios = resultado.data[0].observaciones.trim();
                 $(Comentariostxt).val('');
                 if (comentarios != "") {
                     $(Comentariostxt).val(comentarios);
                 }
                 
             }
         })
         .fail(function (data) {
             MensajesToastrErrorConexion();
         });
    return esValido;
}
$(document).ready(function () {
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        document.getElementById("Proyectotxt").innerHTML = data[3];
        var psProyecto = data[1];
        $("#idProyecto").val(data[1]);
        RegresaDatosAnteproyecto(psProyecto, $('#Comentariostxt'));
        $('#verAnteproyecto').modal('show');
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-success', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#txtId").val(data[1]);
        $("#txtProyecto").val(data[3]);
        $('#ConfirmaAnteproyecto').modal('show'); // abrir
    });

    $('#datatable-buttons tbody').on('click', 'a.btn-primary', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $row = $(this).parents("tr");
        var data = $table.row($row).data();
        $("#Id").val(data[1]);
        $("#Proyecto").val(data[3]);
        $('#ActualizaDuracion').modal('show'); // abrir
    });

    $("#btnAcepta").click(function () {
        var idProyecto = $("#txtId").val();
        $.ajax({
            url: '../../../Academicos/ValidaAnteproyectoJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { piProyecto: idProyecto },
        })
              .done(function (data) {
                  if (typeof (data.Success) !== "undefined" && !data.Success) {
                      MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                  }
                  else {
                      MensajesToastr("success", "Solicitud Procesada", "Validación Correcta");
                      $('#ConfirmaAnteproyecto').modal('hide');
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $('#BodyPrincipal').load('../../../../Academicos/SolicitudesAnteproyectosRP');
                  }
              })
              .fail(function (data) {
                  MensajesToastrErrorConexion();
              });
    });

    $("#frmComentariosAnteproyecto").submit(function (evento) {
        var proyecto = $("#idProyecto").val();
        var comentarios = $("#Comentariostxt").val();
        if (comentarios.trim() == "") {
            MensajesToastr("error", "Necesitas llenar el campo de comentarios");
            return false;
        }
        $.ajax({
            url: '../../../Academicos/CorreccionAnteproyectoJsonRP',
            type: 'POST',
            dataType: 'json',
            data: { piProyecto: proyecto, psComentarios: comentarios },
        })
                  .done(function (data) {
                      if (typeof (data.Success) !== "undefined" && !data.Success) {
                          MensajesToastr("info", "Solicitud Procesada <br />" + data.Mensaje);
                      }
                      else {
                          MensajesToastr("success", "Solicitud Procesada", "Comentarios enviados");
                          $('body').removeClass('modal-open');
                          $('.modal-backdrop').remove();
                          $('#BodyPrincipal').load('../../../../Academicos/SolicitudesAnteproyectosRP');
                          $('#verAnteproyecto').modal('hide');                          
                          //CambiaValorTabla(fin, 3, 0);
                      }
                  })
                  .fail(function (data) {
                      MensajesToastrErrorConexion();
                  });
        evento.preventDefault();
    });
});