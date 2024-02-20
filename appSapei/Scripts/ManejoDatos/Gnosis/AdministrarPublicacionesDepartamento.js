
(function () {
    var fila;
    $('#datatable-buttons tbody').on('click', 'tr', function () {
        $(this).find("button").on('click', function () {
            $(".validar_mensaje").modal('show');
            $(".eliminar_pub").attr('id', $(this).attr('id'));
        })
        var table = $('#datatable-buttons').DataTable();
        fila = table.row($(this));
    });

    $('#datatable-buttons tbody').on('click', 'tr', function () {
        $(this).find("button").on('click', function () {
            $(".validar_mensaje").modal('show');
            $(".eliminar_pub").attr('id', $(this).attr('id'));
        })
        var table = $('#datatable-buttons').DataTable();
        fila = table.row($(this));
    });

    $(".eliminar_pub").on('click', function () {
        var id_publcacion = $.trim($(this).attr('id'));
        id_publcacion=parseInt(id_publcacion);
        if (id_publcacion > 0)
        {
            $.ajax({
                url: '../../Gnosis/EliminarPublicacion',
                type: 'POST',
                dataType: 'Json',
                data: { psId: id_publcacion },
            })
            .done(function (data) {
                if (data.Success)
                {
                    Command: toastr["success"](data.Mensaje, "Solicitud Procesada")
                    fila.remove().draw(false);
                    
                } else {
                    Command: toastr["info"](data.Mensaje, "Surgio un problema")
                   
                }
            })
            .fail(function () {
                Command: toastr["error"]("No hay conexión con el servidor", "Verifica tu conexion")
            })
            .always(function () {
                $(".validar_mensaje").modal('hide');
            });
        }else
        {
            Command: toastr["warning"]("No se pudo realizar la operacion solicitada", "Intentelo de nuevo")
            $(".validar_mensaje").modal('hide');
        }
    });

})();