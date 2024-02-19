$(document).ready(function () {
    init_dataTables();
});

function init_dataTables() {
    console.log('run_datatables');

    if (typeof ($.fn.DataTable) === 'undefined') { return; }
    console.log('init_DataTables');

    var handleDataTableButtons = function () {
        if ($("#datatable-buttons").length) {
            $("#datatable-buttons").DataTable({
                dom: '<"top"B<f>>rt<"bottom"ilp><"clear">',
                select: true,
                select: {
                    style: 'single'
                },
                buttons: [
                    {
                        extend: "copy",
                        className: "btn-success"
                    },
                    {
                        extend: "csv",
                        className: "btn-success"
                    },
                    {
                        extend: "excel",
                        className: "btn-success"
                    },
                    {
                        extend: "pdfHtml5",
                        className: "btn-success"
                    },
                    {
                        extend: "print",
                        className: "btn-success"
                    },
                ],
                responsive: true,
                retrieve: true,
            });
        }
    };

    TableManageButtons = function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
            }
        };
    }();

    /* $('#datatable').dataTable({ });*/

    $('#datatable-keytable').DataTable({
        keys: true
    });

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });

    $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });

    var $datatable = $('#datatable-checkbox');

    $datatable.dataTable({
        'order': [[1, 'asc']],
        'columnDefs': [
            { orderable: false, targets: [0] }
        ]
    });
    $datatable.on('draw.dt', function () {
        $('checkbox input').iCheck({
            checkboxClass: 'icheckbox_flat-green'
        });
    });

    TableManageButtons.init();

}



function DesactivaBotones() {
    var table = $('#datatable-buttons').DataTable();
    //table.buttons().remove();
    $("#datatable-buttons_length").remove();
}
function DesactivaBoton(id) {
    var table = $('#datatable-buttons').DataTable();
    var botones = id.split(",");
    $.each(botones, function (index, value) {
        table.button(value).remove();
    });

}
function QuitaBuscar() {
    $(".dataTables_filter").remove();
    //$(".dataTables_filter").hide();
}

function QuitarMostrar() {
    $(".dataTables_length").remove();
    //$(".dataTables_length").hide();
}


function AgregaFila(fila) {
    var table = $('#datatable-buttons').DataTable();
    var rowNode = table
        .row.add(fila)
        .draw()
        .node();
}

function ocultar_columnas(columna) {
    var table = $('#datatable-buttons').DataTable();
    table.column(columna).visible(false);

}

function mostrar_columnas(columna) {

    var table = $('#datatable-buttons').DataTable();
    table.column(columna).visible(true);

}

function ocultar_buscar() {
    $("#datatable-buttons_filter").remove();
    //$("#datatable-buttons_filter").css("visibility", "hidden");
}



function MostrarTodos() {
    var table = $('#datatable-buttons').DataTable();
    table.page.len(-1).draw();
}

function RegresaMaxValor(columna) {
    var table = $('#datatable-buttons').DataTable();
    var valor = table
        .column(columna)
        .data()
        .sort()
        .reverse()[0];
    return valor;
}

function filtraTabla(valor, columna) {
    var table = $('#datatable-buttons').DataTable();
    table.columns(columna).search(
        valor,
        false,
        true
    ).draw();
}
function BuscaValor(valor, columna) {
    var table = $('#datatable-buttons').DataTable();
    var idx = table
        .columns(columna)
        .data()
        .eq(0) // Reduce the 2D array into a 1D array of data
        .indexOf(valor);

    if (idx === -1) {
        return false;
    }
    return true;
}
function EliminarFila(valor, columna) {
    var table = $('#datatable-buttons').DataTable();
    var idx = table
        .columns(columna)
        .data()
        .eq(0) // Reduce the 2D array into a 1D array of data
        .indexOf(valor);
    if (idx != -1) {
        table
            .row(idx)
            .remove()
            .draw();
    }
}

function EliminarFila1(fila) {
    var table = $('#datatable-buttons').DataTable();
    table
        .row(fila)
        .remove()
        .draw();
}

function QuitarOrden() {

    var table = $('#datatable-buttons').DataTable();
    table.orderable(false);
}

