$("#btnFormato").click(function (event) {
    event.preventDefault();
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../DocumentosOficiales/RegresaLiberacionInformeRP', displaySection);
});

function displaySection() {
    genera = false;
    $('#divModalVisorPDF').modal('show');
    $("#divCargando").hide();
    $("#divPDF").show();
}