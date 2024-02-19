var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
var d = new Date();
var dayName = days[d.getDay()];

$(document).ready(function () {
    var manejaLoad = function (response, status, http) {
        $('body,html').css('padding-right', "0px");
    };
   
    //oculta campo id de la tabla
    $('#btnOmitir').on('click', function () {
        $('#pnlAdeudo').remove();
    });
    $('#btnAdeudos').on('click', function () {
        $('#BodyPrincipal').load('../../../../CentroInformacion/Adeudos', 'psUsuario=' + $("#spUsuario").text());
    });

    $("#txtMaterial").focus((e) => {
        $('body,html').css('padding-right', "0px");
    });
    $("#txtUsuario").focus((e) => {
        $('body,html').css('padding-right', "0px");
    });
    $('#datatable-buttons').DataTable().column(0).visible(false);
    $('#datatable-buttons').DataTable().column(5).visible(false);

    $('#sTipoSol').select2({ theme: "classic" });
    $('#sTipoSol').on('select2:select', function () {
        $('body,html').css('padding-right', "0px");
        switch ($('#sTipoSol').val()) {
            case "Prestamo":
                $("#fldClave").show();
                $('#btnConsultar').html("Continuar");
                break;
            case "Devolucion":
                $("#fldClave").hide();
                $('#btnConsultar').html("Consultar");
                break;
        }
    });
    $("#txtFechaE").change((e) => {

        $("#spFentrega").text($("#txtFechaE").val());
    });
    function aumentaDias(date, days) {
        var result = new Date(date);
        if (date.getDay() === 5)//viernes
            days = 4;
        if (date.getDay() === 4)//jueves
            days = 4;
        result.setDate(date.getDate() + days);
        return result;
    }
    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $rowParent = $(this).parents("tr");//row child
        var $row = $rowParent;
        var data = $table.row($row).data();
        var id=data[0];
        var usuario = data[4];
        $.ajax({
            url: '../../../CentroInformacion/DevolucionEjemplarJson',
            type: 'POST',
            dataType: 'json',
            data: {
                piId: id,
            },
        }).done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    console.log(data);
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {

                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../CentroInformacion/ConsultaPrestamoJson', 'psUsuario=' + usuario, manejaLoad);
                    $('div.modal-backdrop').removeClass('modal-backdrop');
                }
            }).fail(function (data) {
                MensajesToastrErrorConexion();
                $('#BodyPrincipal').load('../../../../CentroInformacion/ConsultaPrestamoJson', 'psUsuario=' + usuario, manejaLoad);

            });
    });
    $("#frmPrestamo").submit(function (event) {
        var ejemplar = $("#txtMaterial").val();
        var usuario = $("#txtUsuario").val();
        var fecha_limite = $("#txtFechaE").val();
        $.ajax({
            url: '../../../CentroInformacion/GuardaPrestamoJson',
            type: 'POST',
            dataType: 'json',
            data: {
                psUsuario: usuario,
                psEjemplar: ejemplar,
                psFechaEntrega: fecha_limite
            },
        })
            .done(function (data) {
                console.log(data);
                if (typeof (data.Success) === "undefined" || !data.Success) {
                    
                    MensajesToastr("error", "Información", data.Mensaje);
                }
                else {

                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    
                }
                if(usuario!='')
                    $('#BodyPrincipal').load('../../../../CentroInformacion/ConsultaPrestamoJson', 'psUsuario=' + usuario, manejaLoad);
                $('div.modal-backdrop').removeClass('modal-backdrop');
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
                //$('#BodyPrincipal').load('../../../../CentroInformacion/ConsultaPrestamoJson', 'psUsuario=' + usuario, manejaLoad);
            });
        event.preventDefault();
    });
    $("#btnConsultar").click(function (e) {
        $('body,html').css('padding-right', "0px");
        $("#btnConsultar").attr("disabled", true);
        $("#txtFechaE").attr("disabled", false);
        switch ($('#sTipoSol').val()) {
            case "Prestamo":
                var fecha = new Date();
                var dia = ("0" + fecha.getDate()).slice(-2);
                var mes = ("0" + (fecha.getMonth() + 1)).slice(-2);
                var hoy = fecha.getFullYear() + "-" + (mes) + "-" + (dia);
                $("#txtFechaP").val(hoy);
                var fechaDevulucion = aumentaDias(new Date(fecha.toString()), 2);
                var dia = ("0" + fechaDevulucion.getDate()).slice(-2);
                var mes = ("0" + (fechaDevulucion.getMonth() + 1)).slice(-2);
                var fechaD = fechaDevulucion.getFullYear() + "-" + (mes) + "-" + (dia);
                var ejemplar = $("#txtMaterial").val();
                $("#txtFechaE").val(fechaD);

                if (ejemplar === "") {
                    $("#btnConsultar").attr("disabled", false);
                    return;
                }
                
                $.ajax({
                    url: '../../../CentroInformacion/ConsultaEjemplar',
                    type: 'GET',
                    dataType: 'json',
                    data: {
                        psEjemplar: ejemplar
                    },
                }).done(function (data) {
                    var datos = JSON.parse(data);
                    if ((typeof (datos.Success) === "undefined")) {


                        if (typeof (data) === "undefined" || !data) {
                            $("#btnConsultar").attr("disabled", false);
                            MensajesToastr("Error", "Información", data.Mensaje);
                        }
                        else {
                            $("#btnConsultar").attr("disabled", false);
                            datos = JSON.parse(data)[0];

                            $("#spClave").text(datos.id_ejemplar);
                            $("#spTitulo").text(datos.titulo);
                            $("#spReserva").text((datos.Reserva ? "Si" : "No"));
                            $("#spTipo").text(datos.Tipo);

                            if (datos.Reserva) {
                                $("#txtFechaE").val(hoy);
                                $("#txtFechaE").attr("disabled", true);
                            }

                            $("#spFentrega").text($("#txtFechaE").val());

                            $("#mdlPrestamo").modal("show");
                        }
                    } else {
                        $("#btnConsultar").attr("disabled", false);
                        //datos = JSON.parse(data);
                        
                        MensajesToastr("error", "Información", datos.Mensaje);
                    }
                        //$('div.modal-backdrop').removeClass('modal-backdrop');
                    });
                break;
            case "Devolucion":
                $('#BodyPrincipal').load('../../../../CentroInformacion/ConsultaPrestamoJson', 'psUsuario=' + $("#txtUsuario").val(), manejaLoad);
                break;
        }

    });

});
