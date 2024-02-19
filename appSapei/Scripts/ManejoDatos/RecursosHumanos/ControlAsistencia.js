    $(document).ready(function () {

        var current_fs, next_fs, previous_fs; //fieldsets
        var opacity;
        $(".next").click(function () {
            var id = $(this).attr('id')
            if (id === 'btnPaso1') {
                var fechaIni = $("#txtFechaIni").val();
                var fechaFin = $("#txtFechaFin").val();
                if (Date.parse(fechaIni) > Date.parse(fechaFin)) {
                    MensajesToastr("info", "Mensaje de sistema", "La fecha de inicio no debe ser superior a la de termino");
                    return;
                }

            }
            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({opacity: 0 }, {
        step: function (now) {
        // for making fielset appear animation
            opacity = 1 - now;
                 current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                 });
                 next_fs.css({'opacity': opacity });
                },
                duration: 600
            });
        });

        $(".previous").click(function () {

        current_fs = $(this).parent();
            previous_fs = $(this).parent().prev();

            //Remove class active
            $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

            //show the previous fieldset
            previous_fs.show();

            //hide the current fieldset with style
            current_fs.animate({opacity: 0 }, {
        step: function (now) {
        // for making fielset appear animation
        opacity = 1 - now;

                    current_fs.css({
        'display': 'none',
                        'position': 'relative'
                    });
                    previous_fs.css({'opacity': opacity });
                },
                duration: 600
            });
        });

        
        $(".submit").click(function () {
            return false;
        })

        $('#dtpFechaIni, #dtpFechaFin').datetimepicker({
            viewMode: 'years',
            format: 'DD/MM/YYYY',
            locale: 'es',
            defaultDate: new Date()
        });

        $("#btnProcesar ").on('click', function (evento) {
            var fechaIni = $("#txtFechaIni").val();
            var fechaFin = $("#txtFechaFin").val();
            var tipo_personal = $("#cboTipoPersonal").val();
            $('#divTablaGeneral').load('../../../../RecursosHumanos/ProcesaControlAsistencia/', {
                piTipoPersonal: tipo_personal, poFechaInicio:
                    fechaIni, poFechaFin: fechaFin
            }, function () {
                MensajesToastr("info", "Menaje de sistema", "Proceso Terminado");
            });
            evento.preventDefault();
        });

        $("#btnBuscarRFCRegresar").on('click', function (evento) {
            $("#datatable-buttons > tbody").empty();
            evento.preventDefault();
        })
        $("#btnBuscarRFCTabla").on('click', function (evento) {
            $('#divModal').modal("show");
            evento.preventDefault();
        })

    });
    function BuscaRFC(rfc) {
        $('#divTablaGeneral').load('../../../../RecursosHumanos/MuestraRegistrosPersonal/',
            { psRFC: rfc });
        $('#divModal').modal("show");
    }

