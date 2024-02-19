$(document).ready(function () {

});
$('#dtpFecha,#dtEqFecha').datetimepicker({
    viewMode: 'years',
    format: 'DD-MM-YYYY',
    locale: 'es',
    defaultDate: new Date()
});
$("#btnGenerar").click(function Nuevo(evento) {
    var control = $("#txtNoControl").val();
    var numero = $("#txtNumero").val();
    var libro = $("#txtLibro").val();
    var foja = $("#txtFoja").val();
    var iniciales = $("#txtIniciales").val();
    var director = $("#cboDirectores option:selected").text();
    var tipo = $('input[name=rbtTipo]:checked').val();
    var fecha = $("#txtFecha").val();
    var redondeo = "N";
    var expedida = $("#txtEqExpedida").val();
    var fechaEqu = $("#txtEqFecha").val();
    var folio = $("#txtEqFolio").val();
    if ($('#cbxRedondeo').is(":checked"))
        redondeo = "S";
    $("#divCargando").show();
    $("#divPDF").hide();
    $('#divPDF').load('../../../../DocumentosOficiales/Certificados', {
        psControl: control, psIniciales: iniciales, psNumero: numero, psLibro: libro, psFoja: foja, psTipo: tipo, psRedondeo: redondeo, psDirector: director, psFecha: fecha,
        psExpedida: expedida, psFolio: folio, psFechaEq: fechaEqu}, displaySection);
    event.preventDefault();
})

function displaySection() {   
    $("#divCargando").hide();
    $("#divPrevPDF").show();
    $("#divPDF").show();
}

$("input").keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

$('input[type=radio][name=rbtTipo]').change(function () {
    var tipo = $('input[name=rbtTipo]:checked').val();
    $("#divEquivalencia").hide();
    if (tipo == "EQU") {
        $("#divEquivalencia").show();
    }
});
