
(function () {
    $("#txtHoraInicial").datetimepicker({
        format: 'HH:mm'
    });

    $("#txtHoraFinal").datetimepicker({
        format: 'HH:mm'
    });


    $("#ddlPuesto").on('change', function () {
        var loClavePuesto = $(this).val();
        $.ajax({
            url: '../../Incidencias/CargarEmpleadoPuesto',
            type: 'POST',
            dataType: 'json',
            data: { psClavePuesto: loClavePuesto },
        })
        .done(function (data) {
            if (data != null) {
                var locarga = JSON.parse(data);
                $('#ddlEmpleado').find('option').remove().end().append('<option value="-1">--SELECCIONE UNA OPCION--</option>');
                $.each(locarga.data, function (index, diccionario) {
                    $("#ddlEmpleado").append("<option value=" + diccionario.rfc + ">" + diccionario.nombre + "</option>");

                });
            }
        })
        .fail(function () {
            console.log("error");
        })
        .always(function (data) {
        });
    });

    //Metodo que permite cargar la tabla carga de horario 

    cargarTabla();

    function cargarTabla()
    {
        $("#tablag").load('../../Incidencias/RHConsultarHorario', function () {
            $(".delete_row").each(function (i, item) {
                $(document).on('click', '.delete_row', function () {
                    $(".eliminarHorario").attr('id', $(this).attr("id"));
                    $("#modalEliminar").modal('show');
                });
               
            });
        });
    }

    
    $(".eliminarHorario").on('click', function () {
        var loId = $(".eliminarHorario").attr('id');

        var loRow = $("table tbody tr:selected");

        console.log(loRow);
        if (loId > 0) {
            /*$.ajax({
                url: '../../Incidencias/RHEliminarHorario',
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
            });*/
        } else {
            Command: toastr["danger"]("No se pudo realizar la operacion", "¡Intentelo mas tarde!")
            $("#modalEliminar").modal('hide');
        }
    });


    
    $("#btnBuscar").on('click', function () {
        var loRFC = $("#ddlEmpleado").val();
        var loDia = parseInt($("#ddlDia").val());
        var loPeriodo = $("#ddlPeriodo").val();
        if (loRFC.length > 0 && loRFC != "-1") {

            $.ajax({
                url: '../Incidencias/RHBuscarhorario',
                type: 'POST',
                dataType: 'json',
                data: { psRFC: loRFC, piDía_semana: loDia, psPeriodo: loPeriodo },
            })
.done(function () {
    console.log("success");
})
.fail(function (jqXHR, textStatus, errorThrown) {
    console.log("error" + jqXHR.responseText);
})
.always(function () {
    console.log("complete");
});


        }
        else {
            Command: toastr["warning"]("Seleccione un empleado")
        }

    });


    $("#btnGuardar").on('click', function () {
        var loRFC = $("#ddlEmpleado").val();
        var loNombre = $("#ddlEmpleado option:selected").text();
        var loDia = parseInt($("#ddlDia").val());
        var loHoraInicial = $("#txtHoraInicial").val();
        var loHoraFinal = $("#txtHoraFinal").val();
        var loPeriodo = $("#ddlPeriodo").val();
        if(loRFC.length > 0 && loRFC!="-1" && loNombre.length>0)
        {
            if(loDia >= 0)
            {
                if(loHoraInicial.length > 0)
                {
                    if(loHoraFinal.length > 0)
                    {
                        if(loPeriodo.length>0)
                        {
                            $.ajax({
                                url: '../../Incidencias/RHAgregarCargaHorario',
                                type: 'POST',
                                dataType: 'json',
                                data: {  psNombre: loNombre,  psRFC: loRFC,  piDia: loDia, psHinicio: loHoraInicial, psHfin: loHoraFinal,  psPeriodo: loPeriodo },
                            })
                    .done(function (data) {
                        if (data.Success) {
                            $("#txtHoraFinal").val("");
                            $("#txtHoraInicial").val("");
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
                            Command: toastr["warning"]("Seleccione un periodo escolar valido", "¡Verifique los campos!")
                        }
                    } else {
                        Command: toastr["warning"]("Introduzca una Hora de termino", "¡Verifique los campos!")
                    }
                } else {
                    Command: toastr["warning"]("Introduzca una Hora de inicio", "¡Verifique los campos!")
                }
            } else {
                Command: toastr["warning"]("seleccione un dia", "¡Verifique los campos!")
            }
        } else
        {
            Command: toastr["warning"]("Seleccione un empleado", "¡Verifique los campos!")
        }
    });

})();

//if ($("#").val() = -1) {
//    $("#")[0].disabled = false;
//    $("#").val("");
//}
//else {
//    $("#")[0].disabled = true;
//    $("#").val(RFC);
//}



