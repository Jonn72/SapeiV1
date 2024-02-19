(function () {
    $("#txtFecha").datetimepicker({ format: 'YYYY/MM/DD' })
    $("#cargar_btn").on('click', function () {
        var loFecha = $.trim($("#txtFecha").val());
        var loDescripcion = $.trim($("#txtDescripcion").val());
        var loPeriodo = $("#idPeriodo").val();

        if (loFecha.length > 0) {
            if (loPeriodo.length > 0) {
                if (loDescripcion.length > 0) {
                    
                    $.ajax({
                        url: '../../Incidencias/RHAgregarDiaFeriado',
                        type: 'POST',
                        dataType: 'json',
                        data: { psFecha: loFecha, psPeriodo: loPeriodo, psDescripcion: loDescripcion },
                    })
                    .done(function (data) {
                        if (data.Success) {
                            $("#txtFecha").val("");
                            $("#txtDescripcion").val("");
                            Command: toastr["success"](data.Mensaje, "¡Operacion realizada con exito!")
                            cargarTabla();
                        }
                        else {
                            Command: toastr["warning"](data.Mensaje, "¡Verifique los campos!")
                        }
                    })
                    .fail(function (xhr, status, error) {
                        Command: toastr["error"]("No hay conexion con el servidor", "Verifica tu conexion")
                    })
                    .always(function () {
                    });

                } else {

                    Command: toastr["warning"]("La descripcion no es valida", "¡Verifique los campos!")
                }
            } else {

                Command: toastr["warning"]("Seleccione un periodo valido", "¡Verifique los campos!")
            }
        } else {
            Command: toastr["warning"]("Seleccione una fecha valida", "¡Verifique los campos!")
        }

    });

    cargarTabla();
   

    function cargarTabla()
    {
        $("#tablag").load('../../Incidencias/RHConsultarDiaFeriado', function () {
            $(".delete_row").each(function () {
                $(this).on('click', function () {
                    $(".eleminarDiaFeriado").attr('id', $(this).attr('id'));
                    $("#modalEliminar").modal('show');
                });
            });
        });
    }

    $(".eleminarDiaFeriado").on('click', function () {
        var loId = $(this).attr('id');
        if(loId>0)
        {
            $.ajax({
                url: '../../Incidencias/RHEliminarDiaFeriado',
                type: 'POST',
                dataType: 'json',
                data: { psId: loId },
            })
            .done(function (data) {
                if (data.Success) {
                    Command: toastr["success"](data.Mensaje, "¡Operacion realizada con exito!")
                    cargarTabla();
                }
                else {
                    Command: toastr["warning"](data.Mensaje, "¡Surgio un problema!")
                }
            })
            .fail(function (xhr, status, error) {
                Command: toastr["error"]("No hay conexion con el servidor", "Verifica tu conexion")
            })
            .always(function () {
                $("#modalEliminar").modal('hide');
            });
        } else {
            Command: toastr["danger"]("No se pudo realizar la operacion", "¡Intentelo mas tarde!")
            $("#modalEliminar").modal('hide');
        }
    });
})();
