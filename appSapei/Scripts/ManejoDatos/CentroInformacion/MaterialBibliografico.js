var $rowTbl;
function MaterialViewModel(url, tipo, datos) {

            this.url = url;
            this.select = ko.observableArray([]);
            this.selectedItem = ko.observable();
            var self = this;
            this.GetValues=function() {    
                $.ajax({
                url: self.url,
                cache: true,
                contentType: 'application/json',
                type: "GET",
                data: datos,
                success: function (result) {
                     var rs = $.parseJSON(result);
                     var bandera = false;
                    rs.data.forEach(function (n) {
                         self.select.push(n);
                         bandera = true;
                    });
                    if (!bandera) {
                         MensajesToastr("info", "Carga de Catalogos", "Debe cargar primero los catalogos " + tipo);
                         $('#btnGuardar').attr("disabled", true);
                    }

                },
                error: function (jqXHR) {
                    MensajesToastrErrorConexion();
                },
                complete: function() {

                }
            });
            }
} 
var carrerasViewModel = new MaterialViewModel('../../../Generales/RegresaComboCarreras', 'Carreras', { 'pbOpcionTodas': 'true' });
var editorialViewModel = new MaterialViewModel('../../../CentroInformacion/ConsultaEditorialJson', 'Editorial', null);
var autoresViewModel = new MaterialViewModel('../../../CentroInformacion/ConsultaAutoresJson','Autores',null);


$(document).ready(function () {
    

    editorialViewModel.GetValues();
    carrerasViewModel.GetValues();
    autoresViewModel.GetValues();
    ko.applyBindings(editorialViewModel);
    ko.applyBindings(carrerasViewModel);
    ko.applyBindings(autoresViewModel);

    
    $('#btnEliminar').off();
    $('#sAutor').select2({theme:"classic"});
    $('#sCarrera').select2({ theme: "classic" });
    $('#sEditorial').select2({ theme: "classic" });
    $('#sTipoMaterial').select2();
    
    var ocultar = function () {
        var disableInput = function() {
            $(this).prop('disabled', true);
            //$("input").prop('disabled', false);
        };
        var enableInput = function () {
            $(this).prop('disabled', false);
            //$("input").prop('disabled', false);
        };
        var opcion = $('#sTipoMaterial').val();
        $('#sAutor').removeAttr('disabled');
        $('#sEditorial').removeAttr('disabled');
        switch (opcion) {
            case "Memorias":
                $("#pfrmMemorias").show();
                $("#pfrmMemorias input").each(enableInput);

                $("#pfrmLibros input").each(disableInput);
                $("#pfrmLibros").hide();

                $("#pfrmCDs").hide();
                $("#pfrmCDs input").each(disableInput);

                $("#pfrmRevistas").hide();
                $("#pfrmRevistas input").each(disableInput);

                $("#pfrmTesis").hide();
                $("#pfrmTesis input").each(disableInput);

                $("#sAutor").val("1").change();
                $('#sAutor').attr('disabled', 'disabled');
                
                $("#sEditorial").val("1").change();
                $('#sEditorial').attr('disabled', 'disabled');
                break;
            case "Libros":
                $("#pfrmMemorias").hide();
                $("#pfrmMemorias input").each(disableInput);

                $("#pfrmLibros input").each(enableInput);
                $("#pfrmLibros").show();

                $("#pfrmCDs").hide();
                $("#pfrmCDs input").each(disableInput);

                $("#pfrmRevistas").hide();
                $("#pfrmRevistas input").each(disableInput);

                $("#pfrmTesis").hide();
                $("#pfrmTesis input").each(disableInput);
                
                break;
            case "CDS":
                $("#pfrmMemorias").hide();
                $("#pfrmMemorias input").each(disableInput);

                $("#pfrmLibros input").each(disableInput);
                $("#pfrmLibros").hide();

                $("#pfrmCDs").show();
                $("#pfrmCDs input").each(enableInput);

                $("#pfrmRevistas").hide();
                $("#pfrmRevistas input").each(disableInput);

                $("#pfrmTesis").hide();
                $("#pfrmTesis input").each(disableInput);
                break;
            case "Revistas":
                $("#pfrmMemorias").hide();
                $("#pfrmMemorias input").each(disableInput);

                $("#pfrmLibros input").each(disableInput);
                $("#pfrmLibros").hide();

                $("#pfrmCDs").hide();
                $("#pfrmCDs input").each(disableInput);

                $("#pfrmRevistas").show();
                $("#pfrmRevistas input").each(enableInput);

                $("#pfrmTesis").hide();
                $("#pfrmTesis input").each(disableInput);
                break;
            case "Tesis":
                $("#pfrmMemorias").hide();
                $("#pfrmMemorias input").each(disableInput);

                $("#pfrmLibros input").each(disableInput);
                $("#pfrmLibros").hide();

                $("#pfrmCDs").hide();
                $("#pfrmCDs input").each(disableInput);

                $("#pfrmRevistas").hide();
                $("#pfrmRevistas input").each(disableInput);

                $("#pfrmTesis").show();
                $("#pfrmTesis input").each(enableInput);

                $("#sAutor").val("1").change();
                $('#sAutor').attr('disabled', 'disabled');

                $("#sEditorial").val("1").change();
                $('#sEditorial').attr('disabled', 'disabled');
                break;
        }
    };
    $('#sTipoMaterial').on('select2:select', ocultar);
    ocultar();

    $('#datatable-buttons tbody').on('click', 'a.btn-info', function () {
        var $table = $('#datatable-buttons').DataTable();
        var $rowParent = $(this).parents("tr");//row child
        var $row;
        $('#sAutor').removeAttr('disabled');
        $('#sEditorial').removeAttr('disabled');
        if ($(this).parents("tr").hasClass("child")) {
            $row = $rowParent.prev("tr");//get row parent
        }else {
            $row = $rowParent;
        }
        var data = $table.row($row).data();

        limpiar("#pfrmMemorias");
        limpiar("#pfrmLibros");
        limpiar("#pfrmCDs");
        limpiar("#pfrmRevistas");
        limpiar("#pfrmTesis");

        $("#hidId").val(data[0]);
        var txtAutor = data[3];

        $("#sAutor").val($("#sAutor option:contains(" + txtAutor + ")").val());
        $('#sAutor').trigger('change');
        var txtEdicion = data[4];

        $("#sEditorial").val($("#sEditorial option:contains(" + txtEdicion + ")").val());
        $('#sEditorial').trigger('change');

        var txtCarrera = data[5];
        $("#sCarrera").val($("#sCarrera option:contains(" + txtCarrera + ")").val());
        $('#sCarrera').trigger('change');

        $("#txtTitulo").val(data[1]);

        data[2] = data[2].split(" ")[0];
        data[2] = data[2].replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");

        $('#txtFecha').attr('disabled', 'disabled');

        //$("#txtFecha").val(data[2]);
        $("#txtExistencia").val(data[2]);
        
        switch (data[6].toUpperCase()) {
            case "MEMORIAS"://memorias
                $("#sTipoMaterial").val("Memorias");
                $('#sTipoMaterial').trigger('change');
                cargarMemorias(data[0]);
                break;
            case "LIBRO"://Libros
                
                $("#sTipoMaterial").val("Libros");
                $('#sTipoMaterial').trigger('change');
                cargarLibro(data[0]);
                break;
            case "CD"://CDS
                $("#sTipoMaterial").val("CDS");
                $('#sTipoMaterial').trigger('change');
                cargarCDs(data[0]);
                break;
            case "REVISTA"://Revistas
                $("#sTipoMaterial").val("Revistas");
                $('#sTipoMaterial').trigger('change');
                cargarRevistas(data[0]);
                break;
            case "TESIS"://Tesis
                $("#sTipoMaterial").val("Tesis");
                $('#sTipoMaterial').trigger('change');
                cargarTesis(data[0]);
        }
            ocultar();
    });
    $('#datatable-buttons tbody').on('click', 'a.btn-danger', function () {
       
        if ($(this).parents("tr").hasClass("child"))
            //cuando un registro ocupa 2 reglones
            $rowTbl = $(this).parents("tr").prev("tr");
        else
            $rowTbl = $(this).parents("tr");

        $("#myModal").modal();
    });
    $('#btnEliminar').click(function (event) {
        var $table = $('#datatable-buttons').DataTable();
        var data = $table.row($rowTbl).data();
        
        
        $.ajax({
            url: '../../../CentroInformacion/EliminaMaterialJson',
            type: 'POST',
            dataType: 'json',
            data: { piId: data[0], piTipoMaterial:data[8]}
        }).done(function (data) {

            if (typeof data.Success === "undefined" || !data.Success) {
                MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
            }
            else {
                MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                $('#BodyPrincipal').empty();
                $('#BodyPrincipal').load('../../../../CentroInformacion/Material');
                            
            }
        }).fail(function (data) {
                MensajesToastrErrorConexion();
            });

    });
    $("#frmMaterial").submit(function (event) {
        
        var autor= $("#sAutor option:selected").val();
        var carrera = $("#sCarrera").val();
        var editorial = $("#sEditorial").val();
        var categoria = $("#sCategoria").val();
        var titulo = $("#txtTitulo").val();
        var fecha =$("#txtFecha").val();
        var existencia= $("#txtExistencia").val();
        var id = $("#hidId").val();

        var opcion = $('#sTipoMaterial').val();
        var biblioteca = {};
        var url_controlador = "";
        
        switch (opcion) {
            case "Memorias":
                url_controlador = '../../../CentroInformacion/GuardaMemoriasJson';
                var idMemorias = $('#hidIdMemorias').val();
                var fechaMemorias = $('#txtFechaMemorias').val();
                var lugarMemorias = $('#txtLugarMemorias').val();
                biblioteca = {
                    piId: id, piAutor: autor,
                    psCarrera: carrera, piEdicion: editorial,
                    psTitulo: titulo,
                    psFecha: fecha, piExistencia: existencia,
                    piIdMemorias: idMemorias, psFechaPb: fechaMemorias,
                    psLugar: lugarMemorias
                };
                break;
            case "Libros":
                url_controlador = '../../../CentroInformacion/GuardaLibroJson';
                var claveIsb = $("#txtClaveIsbn").val();
                var paginas = $("#txtNoPaginas").val();
                var capitulos = $("#txtCapitulos").val();
                var idL = $("#hidIdLibro").val();
                var edicion = $("#txtEdicionLb").val();
                var clasificacion = $("#txtClaveLC").val();

                biblioteca = {
                    piId: id, piAutor: autor,
                    psCarrera: carrera, piEdicion: editorial,
                    psTitulo: titulo,
                    psFecha: fecha, piExistencia: existencia,
                    piIdL: idL, psISBN: claveIsb,
                    piNo_paginas: paginas, piCapitulos: capitulos,
                    psEdicion:edicion, psClasificacion: clasificacion
                };
                break;
            case "CDS":
                url_controlador = '../../../CentroInformacion/GuardaCDJson';
                var id_Cd = $("#hidIdCD").val();
                var descripcion = $("#txtDescripcionCD").val();
                var duracion = $("#txtDuracion").val();
      
                biblioteca = {
                    piId: id, piAutor: autor,
                    psCarrera: carrera, piEdicion: editorial,
                    psTitulo: titulo,
                    psFecha: fecha, piExistencia: existencia,
                    piId_Cd : id_Cd, psDescripcion : descripcion, pfDuracion : duracion
                };
                break;
            case "Revistas":
                url_controlador = '../../../CentroInformacion/GuardaRevistaJson';
                var secciones=$('#txtSeccionesR').val();
                var publicacion=$('#txtPublicacionR').val();
                var Idrevista = $('#hidIdRevista').val();
                var edicion = $("#txtEdicionRv").val();
                biblioteca = {
                    piId: id, piAutor: autor,
                    psCarrera: carrera, piEdicion: editorial,
                    psTitulo: titulo,
                    psFecha: fecha, piExistencia: existencia,
                    piIdR: Idrevista,piSecciones: secciones,
                    psFechaP: publicacion,psEdicion:edicion
                };
                break;
            case "Tesis":
                url_controlador = '../../../CentroInformacion/GuardaTesisJson';
                var paginasT = $('#txtPaginasT').val();
                var publicacion = $('#txtFechaPublicacionT').val();
                var IdTesis = $('#hidIdTesis').val();
                biblioteca = {
                    piId: id, piAutor: autor,
                    psCarrera: carrera, piEdicion: editorial,
                    psTitulo: titulo,
                    psFecha: fecha, piExistencia: existencia,
                    piIdT: IdTesis, psFechaP:publicacion,
                    piPaginas: paginasT
                };
                break;
        }
        $.ajax({
            url: url_controlador,
            type: 'POST',
            dataType: 'json',
            data: biblioteca,
        }).done(function (data) {

                if (typeof (data.Success) === "undefined" || !data.Success) {
                    MensajesToastr("info", "Solicitud Procesada", "Error al guardar");
                }
                else {
                    MensajesToastr("success", "Solicitud Procesada", "Registro Correcto");
                    $('#BodyPrincipal').load('../../../../CentroInformacion/Material');
                }
            })
            .fail(function (data) {
                MensajesToastrErrorConexion();
            });
        event.preventDefault();
    });

    function cargarLibro(id_Material) {
        $.ajax({
            url: '../../../CentroInformacion/ConsultaLibroJson',
            cache: true,
            data: { piId_Mat: id_Material},
            contentType: 'application/json',
            type: "GET",
            success: function (result) {
                
                var rs = $.parseJSON(result);
                rs.data.forEach(function (n) {
                    console.log(rs);
                    $("#txtClaveIsbn").val(n.isbn);
                    $("#txtNoPaginas").val(n.no_paginas);
                    $("#txtCapitulos").val(n.capitulos);
                    $("#txtEdicionLb").val(n.edicion);
                    $("#txtClaveLC").val(n.clasificacion);
                    $("#hidIdLibro").val(n.id_libro);

                });
                
            },
            error: function (jqXHR) {
                MensajesToastrErrorConexion();
            }
        });
    }
    function cargarMemorias(id_Material) {
        $.ajax({
            url: '../../../CentroInformacion/ConsultaMemoriasJson',
            cache: true,
            data: { piId_Mat: id_Material },
            contentType: 'application/json',
            type: "GET",
            success: function (result) {
                //console.log(result);
                var rs = $.parseJSON(result);
                rs.data.forEach(function (n) {
                    $('#hidIdMemorias').val(n.id_mem_res);
                    var fechaP = n.fecha_publicacion.split("T")[0];
                    fechaP = fechaP.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");
                    $('#txtFechaMemorias').val(fechaP);
                    $('#txtLugarMemorias').val(n.lugar_p);
                });
            },
            error: function (jqXHR) {
                MensajesToastrErrorConexion();
            }
        });
    }
    function cargarCDs(id_Material) {
        $.ajax({
            url: '../../../CentroInformacion/ConsultaCDJson',
            cache: true,
            data: { piId_Mat: id_Material },
            contentType: 'application/json',
            type: "GET",
            success: function (result) {
                var rs = $.parseJSON(result);
                rs.data.forEach(function (n) {
                    
                    $('#txtDescripcionCD').val(n.descripcion);
                    $('#txtDuracion').val(n.duracion);
                    $('#hidIdCD').val(n.id_cds);
                });
            },
            error: function (jqXHR) {
                MensajesToastrErrorConexion();
            }
        });
    }
    function cargarRevistas(id_Material) {
        $.ajax({
            url: '../../../CentroInformacion/ConsultaRevistaJson',
            cache: true,
            data: { piId_Mat: id_Material },
            contentType: 'application/json',
            type: "GET",
            success: function (result) {
                var rs = $.parseJSON(result);
                rs.data.forEach(function (n) {
                    console.log(rs);
                    $('#txtSeccionesR').val(n.secciones);
                    var fechaP = n.fecha_p.split("T")[0];
                    fechaP = fechaP.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");
                    $('#txtPublicacionR').val(fechaP);
                    $("#txtEdicionRv").val(n.edicion);

                    $('#hidIdRevista').val(n.id_revista);
                });
            },
            error: function (jqXHR) {
                MensajesToastrErrorConexion();
            }
        });
    }
    function cargarTesis(id_Material) {
        $.ajax({
            url: '../../../CentroInformacion/ConsultaTesisJson',
            cache: true,
            data: { piId_Mat: id_Material },
            contentType: 'application/json',
            type: "GET",
            success: function (result) {
                var rs = $.parseJSON(result);
                rs.data.forEach(function (n) {
                    $('#txtPaginasT').val(n.no_paginas);
                    var fechaP = n.fecha_p.split("T")[0];
                    fechaP = fechaP.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");
                    $('#txtFechaPublicacionT').val(fechaP);
                    $('#hidIdTesis').val(n.id_tesis);
                });
            },
            error: function (jqXHR) {
                MensajesToastrErrorConexion();
            }
        });
    }

    function limpiar(div) {

        $(div+" > input").each(function () {
            $(this).val('');
        });
    }

    $('#dtpFechaMemorias, #dtpFecha, #dtpFechaPublicacionT, #txtPublicacionR').datetimepicker({
         format: 'DD/MM/YYYY',
         locale: 'es',
         defaultDate: new Date()
    });
    $("input").blur(function () {
         $(this).val($(this).val().toUpperCase());
    });

});

