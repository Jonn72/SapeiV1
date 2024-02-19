var esNuevo = true;
var filaEliminar;
$(document).ready(function () {

    $("#AjaxformId").submit(function (event) {
        GuardaAjax()
    });     

    function GuardaAjax() {
        var dataString;
        event.preventDefault();
        event.stopImmediatePropagation();
        if (!Validacion()) {
            return;
        }
        dataString = new FormData($("#AjaxformId").get(0));
        contentType = false;
        processData = false;
        $.ajax({
            type: "POST",
            url: "../../../Financieros/RegistrarServicio",
            data: dataString,
            dataType: "json",
            contentType: contentType,
            processData: processData,
            success: function (result) {
                onAjaxRequestSuccess(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                MensajesToastrErrorConexion();
            }
        });
    }
});



function Validacion() {
    if ($("#clave").val().trim().length == 0 || $("#concepto").val().trim().length == 0 || $("#monto").val().trim().length == 0 || $("#dias_vigencia").val().trim().length == 0) {
        MensajesToastr("info", "Todos los campos son requeridos", "");
        return false;
    }
    if (!esNuevo)
        return true;

    var valor = BuscaValor($("#clave").val(), 0);
    if (valor) {
        MensajesToastr("info", "Ya existe un servicio registrado con la clave: " + $("#clave").val(), "");
        return false;
    }
    return true;
}
var onAjaxRequestSuccess = function (result) {
    MensajesToastr(result.Tipo, result.Mensaje, "");
    if (!result.Error) {
        var fila = [];
        fila[0] = $("#clave").val();
        fila[1] = $("#concepto").val();
        fila[2] = $("#monto").val();
        fila[3] = $("#dias_vigencia").val();
        if ($('#activo').is(':checked'))
            fila[4] = 'SI';
        else
            fila[4] = 'NO';
        fila[5] = '<a class="btn btn-primary btn-xs">' +
            ' <i class="fa fa-pencil" ></i></a>';
            //'&nbsp;' +
            //'<a class="btn btn-danger btn-xs">' +
            //' <i class="fa fa-trash"></i></a>';
        if (!esNuevo) {
            EliminarFila1(filaEliminar);
        }
        AgregaFila(fila);
        $('#clave').prop('readonly', false);
        $("#divModalNuevo .close").click()
        $('#AjaxformId').get(0).reset();
        //$("#BodyPrincipal").load("../../Financieros/Servicios");
    }
}  

$('#datatable-buttons tbody').on('click', 'a.btn-primary', function (event) {
    var $table = $('#datatable-buttons').DataTable();
    var $row = $(this).parents("tr");
    var data = $table.row($row).data();
    esNuevo = false;
    $("#clave").val(data[0]);
    $("#concepto").val(data[1]);
    $("#monto").val(data[2]);
    $("#dias_vigencia").val(data[3]);
    if (data[4].trim() == "SI")
        $("#activo").prop('checked', true);
    else
        $("#activo").prop('checked', false);
    $("#divModalNuevo").modal('show');
    $('#clave').prop('readonly', true);
    filaEliminar = $row;
    event.preventDefault();
});
$('#btnAtras').on('click', 'a.btn-primary', function (event) {
    esNuevo = true;
    $('#AjaxformId').get(0).reset();
    $('#clave').prop('readonly', false);
    event.preventDefault();
});