using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
namespace Sapei
{
    /// <summary>
    /// Clase para manejra los reportes generales
    /// </summary>
    public class ReportesGenerales:IDisposable
    {
        #region Variables

        /// <summary>
        /// Variable del sistema
        /// </summary>
        private SistemaSapei _oSistema;

        #endregion

        #region Propiedades

        #region Propiedades para la Salida de los reportes con XML

        /// <summary>
        /// Gets or sets the nombre archivo zip polizas.
        /// </summary>
        /// <value>
        /// The nombre archivo zip polizas.
        /// </value>
        public string NombreArchivoZip { get; set; }

        /// <summary>
        /// Gets or sets the archivo zip polizas.
        /// </summary>
        /// <value>
        /// The archivo zip polizas.
        /// </value>
        public byte[] ArchivoZIP { get; set; }

        #endregion

        #region Propiedades  para la entrada en general de la clase

        /// <summary>
        /// 
        /// </summary>
        public string RutaReporteVacio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RutaReportes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RutaServidor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NombreReportedeSalida { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string NombreBitacoraSalida { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] ArchivoSalida { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] ArchivoSalidaBitacora { get; private set; }

        #endregion

        #region Propiedades para los parametros de entrada del los reportes


        #endregion

        #region propiedades y variables para manejo de crystal

        /// <summary>
        /// Gets or sets the buffer archivo.
        /// </summary>
        /// <value>
        /// The buffer archivo.
        /// </value>
        public byte[] BufferArchivo { get; set; }

        /// <summary>
        /// Gets or sets the buffer bitacora.
        /// </summary>
        /// <value>
        /// The buffer bitacora.
        /// </value>
        public byte[] BufferBitacora { get; set; }

        /// <summary>
        /// Gets or sets the nombre reporte salida.
        /// </summary>
        /// <value>
        /// The nombre reporte salida.
        /// </value>
        public string NombreReporteSalida { get; set; }

        /// <summary>
        /// Gets or sets the nombre reporte bitacora.
        /// </summary>
        /// <value>
        /// The nombre reporte bitacora.
        /// </value>
        public string NombreReporteBitacora { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public enmTipoArchivo TipoArchivo { get; set; }
	
		#endregion

		#endregion

		#region Constructores

		/// <summary>
		/// Crea una nueva instacia de ReportesGenerales
		/// </summary>
		/// <param name="poSistema">Variable del sistema</param>
		/// <param name="pbGeneraXML">Deterimina si la instacia creara archivos xml</param>
		/// <param name="psRutaServidor">Ruta del servidor para buscar los archivos de los sellos</param>          
		public ReportesGenerales(SistemaSapei poSistema, bool pbGeneraXML, string psRutaServidor)
        {
            this._oSistema = poSistema;
            this.RutaServidor = psRutaServidor;
            this.TipoArchivo = enmTipoArchivo.pdf;
            this.RutaReportes = "";
        }

        /// <summary>
        /// Crea una nueva instancia de reportes generales solo con la variable del sistema
        /// </summary>
        /// <param name="poSistema"></param>
        public ReportesGenerales(SistemaSapei poSistema)
        {
            this._oSistema = poSistema;
            this.RutaReportes = string.Format("{0}Reportes\\RDLC\\", AppDomain.CurrentDomain.BaseDirectory);
            this.TipoArchivo = enmTipoArchivo.pdf;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Funciones
        public DataTable RegresaDatosReporteSGE(string psClave)
        {
            StringBuilder lsCadena;
            lsCadena = new StringBuilder();
            lsCadena.Append("SELECT clave, titulo, referencia_norma,");
            lsCadena.Append("(select logo from sis_logos where id = D.logo) logo,codigo");
            lsCadena.Append(" FROM documentos_sgc D");
            lsCadena.AppendFormat(" WHERE clave = '{0}'", psClave);
            return _oSistema.Conexion.RegresaDataTable(lsCadena);
        }
        public DataTable RegresaFondoMembretado(string psDescripcion)
        {
            StringBuilder lsCadena;
            lsCadena = new StringBuilder();
            lsCadena.Append("SELECT id, descripcion, logo");
            lsCadena.Append(" FROM sis_logos D");
            lsCadena.AppendFormat(" WHERE descripcion = '{0}'", psDescripcion);
            return _oSistema.Conexion.RegresaDataTable(lsCadena);
        }
        #endregion

        #region Aspirantes
        /// <summary>
        /// Funcion que crea el reporte para el libro diario de conac
        /// </summary>
        /// <returns>Binario del reporte</returns>
        public byte[] RegresaFichaAspirante() 
        {
            Sapei.Reportes.Reporte loReporte;
            Aspirantes_Periodos loPeriodo = new Aspirantes_Periodos(_oSistema);
            DateTime loFecha = loPeriodo.RegresaFechaHoraExamen();
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDT;
            Aspirante loAspirante = new Aspirante(_oSistema);
            loDT = loAspirante.RegresaDatosCompletos(this._oSistema.Sesion.Usuario.Usuario, true);
            if (loDT.Columns.Count == 1)
            {
                return ReporteVacio(loDT.Rows[0].ItemArray[0].ToString());
            }
            loReporte.AgregaOrigenMapeo("DataSet1", loDT);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaReporteAspirantes2AlumnosInscritos(string psPeriodo)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaDatosAspirantes2AlumnosInscritos(psPeriodo);
            loReporte.AgregaParametro("periodo", psPeriodo.RegresaDescripcionPeriodo());
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        #endregion
        #region Alumnos

        public byte[] RegresaDatosBoletaCalificacion(string psPeriodo, string psNoControl)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDatos = new Alumno(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosEncabezadoBoleta(psNoControl, psPeriodo));
            loReporte.AgregaOrigenMapeo("DataSet2", loDatos.RegresaDatosCuerpoBoleta(psPeriodo, psNoControl));
            loReporte.AgregaOrigenMapeo("DataSet3", loDatos.RegresaDatosAcumuladoBoleta(psNoControl, psPeriodo));
            return loReporte.ExportaReporte();
        }

        #endregion 
        #region CLE
        /// <summary>
        /// Funcion que crea el reporte para el libro diario de conac
        /// </summary>
        /// <returns>Binario del reporte</returns>
        public byte[] RegresaListaGruposIngles()
        {

            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            loReporte.AgregaParametro("periodo", this._oSistema.Sesion.Periodo.Identificacionlarga);
            Cle_Grupos loDatos = new Cle_Grupos(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaTablaGruposImprimir(this._oSistema.Sesion.Periodo.PeriodoActual));
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaListaReinscripcionIngles(string psPeriodo)
        {

            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            loReporte.AgregaParametro("periodo", psPeriodo.RegresaDescripcionPeriodo());
            Cle_Lista_Seleccion loDatos = new Cle_Lista_Seleccion(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaLista(psPeriodo));
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] HorariosIngles(string psNoControl)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Cle_Seleccion loDatos = new Cle_Seleccion(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaListaGruposInscritos(psNoControl));
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] GeneraConstancias(string psNoControl, enmCleDocumentos penmTipo)
        {
            Sapei.Reportes.Reporte loReporte;
            DataTable loDt;
            string lsCadena;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Cle_Liberacion_Ingles loDatos = new Cle_Liberacion_Ingles(_oSistema);
            loDt = loDatos.RegresaDatosConstancia(psNoControl, penmTipo);
            lsCadena = loDt.Rows[0].Field<string>("cadena_validacion");
            if (!string.IsNullOrEmpty(lsCadena))
            {
                loDt.Columns["qr_validacion"].ReadOnly = false;
                loDt.Rows[0].SetField("qr_validacion", lsCadena.RegresaQRValidacionDocumentos());
            }
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }

        public byte[] GeneraDocumento(string psOficio, string psFecha, string psNoControl, enmCleDocumentos penmTipo)
        {
            Sapei.Reportes.Reporte loReporte;
            DataTable loDt;
            string lsCadena;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Cle_Liberacion_Ingles loDatos = new Cle_Liberacion_Ingles(_oSistema);
            loDt = loDatos.RegresaDatosConstancia(psNoControl, penmTipo, psOficio, psFecha);
            lsCadena = loDt.Rows[0].Field<string>("cadena_validacion");
            if (!string.IsNullOrEmpty(lsCadena))
            {
                loDt.Columns["qr_validacion"].ReadOnly = false;
                loDt.Rows[0].SetField("qr_validacion", lsCadena.RegresaQRValidacionDocumentos());
            }
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        #endregion
        #region Facilitadores

        public byte[] RegresaGrupoCalificacionActasFacilitador(string psPeriodo, string psNivel, string psGrupo, string psFacilitador)
        {
            DataTable ldtDatos;
            string lsCadena;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            ldtDatos = loDatos.RegresaDatosActaFacilitador(psPeriodo, psNivel, psGrupo, psFacilitador);
            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_fcl");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    ldtDatos.Columns["qr_fcl"].ReadOnly = false;
                    ldtDatos.Rows[0].SetField("qr_fcl", ldtDatos.Rows[0].Field<string>("cadena_fcl").RegresaCodigoQR());
                   
                }
            }
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", loDatos.RegresaAlumnosActaFacilitador(psPeriodo, psNivel, psGrupo));
            return loReporte.ExportaReporte();
        }

        #endregion 
        #region Doc. Oficiales

        public DataTable RegresaDatosFirmasExtraescolares(int piIdActividad)
		{
			StringBuilder lsCadena;
			lsCadena = new StringBuilder();
			lsCadena.Append("SELECT ");
			lsCadena.AppendFormat("(select UPPER(paterno +' '+ materno +' '+ nombre) enytrenador from extra_entrenador where id = (select id_entrenador from extra_actividades where id = {0})) entrenador,", piIdActividad);
			lsCadena.AppendFormat("(select descripcion from extra_actividades where id = {0}) actividad ,", piIdActividad);
			lsCadena.AppendFormat("(select case tipo when 'DEP' then 'JOSE AMINN ALCÁNTARA VAZQUEZ' ELSE 'FLORENTINO TORRES ESPINOZA' END from extra_actividades where id = {0}) coordinador ,", piIdActividad);
			lsCadena.Append("(select jefe_area from jefes where clave_area = '120400') jefe");
			return _oSistema.Conexion.RegresaDataTable(lsCadena);

        }

        public byte[] RegresaDocOficial(string psId, string psNoControl, string psPeriodo, string psSemestre, string psTipoDoc, string psFechaEmision, string psOficio, string psNomSeguro, string psNoSeguro, string psIniSeguro, string psFinSeguro, string psImss, string psFechaTitulo, string psNombreDirector, string psNombreInstituto)
        {
            switch (psTipoDoc)
            {
                case "1":
                    return RegresaDiplomaEspecialidad(psNoControl);

                case "2":
                    return RegresaConstancia(psId, psPeriodo, psNoControl, psFechaEmision, psOficio, psNomSeguro, psNoSeguro, psIniSeguro, psFinSeguro, psImss, psFechaTitulo, psNombreDirector, psNombreInstituto);


            }

            return null;
        }      


        public byte[] RegresaOrdenMantenimiento(string psValor, string psPeriodo, string psTipo_Mantenimiento, string psFolio, string psTipo_Servicio, string psTrabajoRealizado, string psAsignado, string psVerificado, string psFechaLiberacion)
        {
            switch (psValor)
            {
                case "1":
                    return RegresaOrdenPrevia(psPeriodo, psTipo_Mantenimiento, psFolio,  psTipo_Servicio,  psTrabajoRealizado,  psAsignado,  psVerificado,  psFechaLiberacion);
                case "2":
                    return RegresaOrden(psPeriodo, psFolio);

            }
            return null;
        }


        public byte[] RegresaOrden(string psPeriodo, string psFolio)
        {
            string lsCadena;
            string lsCadena2;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            string lsTipoSolicitud;
            if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.SAD)
                lsTipoSolicitud = "CC";
            else
                lsTipoSolicitud = "XX";
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@tipo_mantenimiento", ""));
                loParametros.Add(new ParametrosSQL("@folio", psFolio));
                loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario.ToString().Trim()));
                loParametros.Add(new ParametrosSQL("@tipo_servicio", ""));
                loParametros.Add(new ParametrosSQL("@fecha_realizado", ""));
                loParametros.Add(new ParametrosSQL("@asignado", ""));
                loParametros.Add(new ParametrosSQL("@trabajo", ""));
                loParametros.Add(new ParametrosSQL("@estatus", ""));
                loParametros.Add(new ParametrosSQL("@bandera", 3));
                loParametros.Add(new ParametrosSQL("@tipo_solicitud", lsTipoSolicitud));

                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_orden", loParametros));
                lsCadena = loDt.Rows[0].Field<string>("firma_liberado");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    loDt.Columns["qr_liberado"].ReadOnly = false;
                    loDt.Rows[0].SetField("qr_liberado", loDt.Rows[0].Field<string>("firma_liberado").RegresaCodigoQR());
                }
                lsCadena2 = loDt.Rows[0].Field<string>("firma_recibido");
                if (!string.IsNullOrEmpty(lsCadena2))
                {
                    loDt.Columns["qr_recibido"].ReadOnly = false;
                    loDt.Rows[0].SetField("qr_recibido", loDt.Rows[0].Field<string>("firma_recibido").RegresaCodigoQR());
                }
            }
            loReporte.AgregaParametro("folio", psFolio);
            loReporte.AgregaParametro("Verificado", "M en C Daniel Torres Alvarado");
            loReporte.AgregaParametro("aprobado", _oSistema.Sesion.Usuario.Nombre);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaOrdenPrevia(string psPeriodo, string psTipo_Mantenimiento, string psFolio, string psTipo_Servicio, string psTrabajoRealizado, string psAsignado, string psVerificado, string psFechaLiberacion)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            string lsTipoSolicitud;
            if (_oSistema.Sesion.Usuario.RolUsuario == Framework.Configuracion.enmRolUsuario.SAD)
                lsTipoSolicitud = "CC";
            else
                lsTipoSolicitud = "XX";
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano));
                loParametros.Add(new ParametrosSQL("@tipo_mantenimiento", ""));
                loParametros.Add(new ParametrosSQL("@folio", psFolio));
                loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario));
                loParametros.Add(new ParametrosSQL("@tipo_servicio", ""));
                loParametros.Add(new ParametrosSQL("@fecha_realizado", ""));
                loParametros.Add(new ParametrosSQL("@asignado", ""));
                loParametros.Add(new ParametrosSQL("@trabajo", ""));
                loParametros.Add(new ParametrosSQL("@estatus", ""));
                loParametros.Add(new ParametrosSQL("@bandera", 4));
                loParametros.Add(new ParametrosSQL("@tipo_solicitud", lsTipoSolicitud));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_orden", loParametros));
            }
            loReporte.AgregaParametro("folio", psFolio);
            loReporte.AgregaParametro("tipo_mantenimiento", psTipo_Mantenimiento);
            loReporte.AgregaParametro("tipo_servicio", psTipo_Servicio);
            loReporte.AgregaParametro("asignado", psAsignado);
            loReporte.AgregaParametro("fecha_liberacion", DateTime.Now.ToString());
            loReporte.AgregaParametro("descripcion", psTrabajoRealizado);
            loReporte.AgregaParametro("Verificado", _oSistema.Sesion.Usuario.Nombre);

            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            return loReporte.ExportaReporte();
        }


        public byte[] RegresaSolicitudFirmada(string psPeriodo,  string psFolio)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            string lsCadena;
            string lsCadena2;
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@tipo_solicitud", "CC"));
                loParametros.Add(new ParametrosSQL("@folio", psFolio));
                loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario.ToString().Trim()));
                loParametros.Add(new ParametrosSQL("@descripcion", ""));
                loParametros.Add(new ParametrosSQL("@estatus", ""));
                loParametros.Add(new ParametrosSQL("@bandera", 3));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_solicitud", loParametros));
                    lsCadena = loDt.Rows[0].Field<string>("firma_solicitante");
                    if (!string.IsNullOrEmpty(lsCadena))
                    {
                        loDt.Columns["qr_solicitante"].ReadOnly = false;
                        loDt.Rows[0].SetField("qr_solicitante", loDt.Rows[0].Field<string>("firma_solicitante").RegresaCodigoQR());
                    }
                    lsCadena2 = loDt.Rows[0].Field<string>("firma_recibido");
                    if (!string.IsNullOrEmpty(lsCadena2))
                    {
                    loDt.Columns["qr_recibido"].ReadOnly = false;
                    loDt.Rows[0].SetField("qr_recibido", loDt.Rows[0].Field<string>("firma_recibido").RegresaCodigoQR());
                    }

            }
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaSolicitudPrevia(string  psPeriodo, string psTipo_solicitud,   string psDescripcion)
        {
   
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            DataTable loDt = new DataTable();

            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loParametros.Add(new ParametrosSQL("@tipo_solicitud", psTipo_solicitud));
                loParametros.Add(new ParametrosSQL("@folio", ""));
                loParametros.Add(new ParametrosSQL("@usuario", _oSistema.Sesion.Usuario.Usuario.ToString().Trim()));
                loParametros.Add(new ParametrosSQL("@descripcion", ""));
                loParametros.Add(new ParametrosSQL("@estatus", ""));
                loParametros.Add(new ParametrosSQL("@bandera", ""));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_mantenimiento_solicitud", loParametros));
            }
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);

            loReporte.AgregaParametro("folio", "1");
            loReporte.AgregaParametro("tipo_solicitud", psTipo_solicitud);
            loReporte.AgregaParametro("fecha_elaboracion", DateTime.Now.ToString());
            loReporte.AgregaParametro("descripcion", psDescripcion);
            return loReporte.ExportaReporte();
        }


        public byte[] RegresaDiplomaEspecialidad(string psNoControl)
        {

            DataTable ldtDatos;
            string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.CargarVistaDatosCompletos(psNoControl);
            lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("nombre", lsNombre);
            loReporte.AgregaParametro("especialidad", ldtDatos.Rows[0].RegresaValor<string>("descripcion_especialidad").Capitalizadas());
            loReporte.AgregaParametro("carrera", ldtDatos.Rows[0].RegresaValor<string>("nombre_carrera").Capitalizadas());
            loReporte.AgregaParametro("director", _oSistema.Sesion.Institucion.NombreDirector.Capitalizadas());
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaConstancia(string psId, string psPeriodo, string psControl, string psFechaEmision, string psOficio, string psNomSeguro, string psNoSeguro, string psIniSeguro, string psFinSeguro, string psImss, string psFechaTitulo, string psNombreDirector, string psNombreInstituto)
        {
            switch (psId)
            {
                case "1":
                    return RegresaKardex(psControl, psOficio);
                case "3":
                    return RegresaConstanciaGeneral(psId, psControl, psOficio, psPeriodo);
                case "4":
                    return RegresaConstanciaGeneral(psId, psControl, psOficio, psPeriodo);
                case "5":
                    return RegresaConstanciaGeneral_2(psId, psControl, psOficio, psPeriodo, psNomSeguro, psNoSeguro, psIniSeguro, psFinSeguro);
                case "6":
                    return RegresaConstanciaGeneral(psId, psControl, psOficio, psPeriodo);
                case "7":
                    return RegresaConstanciaGeneral_3(psId, psControl, psOficio, psPeriodo, psImss);
                case "8":
                    return RegresaConstanciaGeneral(psId, psControl, psOficio, psPeriodo);
                case "9":
                    return RegresaConstanciaFormatoUnico(psId, psPeriodo, psControl, psOficio);
                case "10":
                    return RegresaConstanciaUnSemestre(psId, psPeriodo, psControl, psOficio);
                case "11":
                    return RegresaConstanciaTiraDeMaterias(psId, psPeriodo, psControl, psOficio);
                case "12":
                    return RegresaConstanciaEgreso(psId, psPeriodo, psControl, psOficio);
                case "13":
                    return RegresaConstanciaMateriasEgreso(psId, psPeriodo, psControl, psOficio);
                case "14":
                    return RegresaConstanciaTituloEnTramite(psId, psControl, psOficio, psFechaTitulo);
                case "15":
                    return RegresaConstanciaEgreso(psId, psPeriodo, psControl, psOficio);
                case "16":
                    return RegresaConstanciaTraslado(psId, psControl, psOficio, psNombreDirector, psNombreInstituto);
                case "17":
                    return RegresaConstanciaMaterias(psId, psControl, psOficio, psPeriodo);

            }


            return null;
        }
        public byte[] RegresaConstanciaMaterias(string psId, string psControl, string psOficio, string psPeriodo)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_materias", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaTraslado(string psId, string psControl, string psOficio, string psNombreDirector, string psNombreInstituto)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_general", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaParametro("nombre_director", psNombreDirector);
            loReporte.AgregaParametro("instituto", psNombreInstituto);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaTituloEnTramite(string psId, string psControl, string psOficio, string psFechaTitulo)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_control", psControl));
                //loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_egresado", loParametros));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_titulo_tramite", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("fecha_titulacion", psFechaTitulo);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaMateriasEgreso(string psId, string psPeriodo, string psControl, string psOficio)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_materias_egresados", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaEgreso(string psId, string psPeriodo, string psControl, string psOficio)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_control", psControl));
                loParametros.Add(new ParametrosSQL("@id_doc", psId));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_egresado_avance", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaUnSemestre(string psId, string psPeriodo, string psControl, string psOficio)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constacia_un_semestre", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaTiraDeMaterias(string psId, string psPeriodo, string psControl, string psOficio)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_tiramaterias", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaFormatoUnico(string psId, string psPeriodo, string psControl, string psOficio)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loParametros.Add(new ParametrosSQL("@periodo", psPeriodo));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_fua", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaGeneral_3(string psId, string psControl, string psOficio, string psPeriodo, string psImss)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_general", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaParametro("imss", psImss);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaGeneral_2(string psId, string psControl, string psOficio, string psPeriodo, string psNomSeguro, string psNoSeguro, string psFechaIniSeguro, string psFechaFinSeguro)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_general", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaParametro("nom_seguro", psNomSeguro);
            loReporte.AgregaParametro("no_seguro", psNoSeguro);
            loReporte.AgregaParametro("fecha_ini_seguro", psFechaIniSeguro);
            loReporte.AgregaParametro("fecha_fin_seguro", psFechaFinSeguro);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaGeneral(string psId, string psControl, string psOficio, string psPeriodo)
        {

            //string lsNombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_constancia_general", loParametros));
            }
            //lsNombre = string.Format("{0} {1} {2}", ldtDatos.Rows[0].RegresaValor<string>("nombre"), ldtDatos.Rows[0].RegresaValor<string>("paterno"), ldtDatos.Rows[0].RegresaValor<string>("materno")).ToUpper();

            loReporte.AgregaParametro("oficio", psOficio);
            loReporte.AgregaParametro("no_control", psControl);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
            loReporte.AgregaParametro("constancia", psId);
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaKardex(string psControl, string psOficio)
        {

            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            DataTable loDt = new DataTable();
            List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
            using (var loConexion = new ManejaConexion(_oSistema.Conexion))
            {
                loParametros.Add(new ParametrosSQL("@no_de_control", psControl));
                loDt.Load(_oSistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_datos_kardex", loParametros));
            }
			loReporte.AgregaParametro("oficio", psOficio);
			loReporte.AgregaParametro("no_control", psControl);
			loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActualSinVerano.RegresaDescripcionLargaPeriodo("/").ToUpper().Trim());
			loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));
            return loReporte.ExportaReporte();
		}

        public byte[] RegresaCargaAcademica(string psPeriodo, string psNoControl, bool pbFiel = false)
        {

            DataTable ldtDatos;
            string lsCadena;
            byte[] result;
            Sapei.Reportes.Reporte loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            
                Alumno loDato = new Alumno(_oSistema);
                using (ldtDatos = loDato.RegresaDatosCargaAcademica(psPeriodo, psNoControl, pbFiel))
                {
                    if (pbFiel)
                    {
                        lsCadena = ldtDatos.Rows[0].Field<string>("cadena_alumno");
                        if (!string.IsNullOrEmpty(lsCadena))
                        {
                            ldtDatos.Columns["qr_alumno"].ReadOnly = false;
                            ldtDatos.Rows[0].SetField("qr_alumno", ldtDatos.Rows[0].Field<string>("cadena_alumno").RegresaCodigoQR());
                            lsCadena = ldtDatos.Rows[0].Field<string>("cadena_dep");
                            if (!string.IsNullOrEmpty(lsCadena))
                            {
                                ldtDatos.Columns["qr_dep"].ReadOnly = false;
                                ldtDatos.Rows[0].SetField("qr_dep", ldtDatos.Rows[0].Field<string>("cadena_dep").RegresaCodigoQR());
                                //Se genera e inserta qr de validacion de documento
                                ldtDatos.Columns["qr_valida_documento"].ReadOnly = false;
                                ldtDatos.Rows[0].SetField("qr_valida_documento", ldtDatos.Rows[0].Field<string>("id_valida_documento").RegresaQRValidacionDocumentos());

                            }
                        }

                    }
                    loReporte.AgregaParametro("periodo", psPeriodo);
                    loReporte.AgregaParametro("no_de_control", psNoControl);
                    loReporte.AgregaParametro("descripcion_periodo", psPeriodo.RegresaDescripcionPeriodo());

                    loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
                    loReporte.AgregaOrigenMapeo("DataSet2", RegresaDatosReporteSGE("cargaacademica"));

                     result = loReporte.ExportaReporte();
                }
            loReporte = null;
            return result;
        }
        public byte[] RegresaCargasAcademicasFiel(string psPeriodo, string psCarrera, string psSemestre)
        {

            DataTable ldtDatos;
            string lsCadena;
            string lsNoControl;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaDatosCargaAcademica(psPeriodo, psCarrera, psSemestre);
            lsNoControl = "";
            ldtDatos.Columns["qr_alumno"].ReadOnly = false;
            ldtDatos.Columns["qr_dep"].ReadOnly = false;
            ldtDatos.Columns["qr_valida_documento"].ReadOnly = false;

            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                if (lsNoControl == loFIla.Field<string>("no_de_control").Trim())
                {
                    continue;
                }
                lsNoControl = loFIla.Field<string>("no_de_control").Trim();

                if (!string.IsNullOrEmpty(loFIla.Field<string>("cadena_alumno")))
                {
                    loFIla.SetField("qr_alumno", loFIla.Field<string>("cadena_alumno").RegresaCodigoQR());
                    lsCadena = loFIla.Field<string>("cadena_dep");
                    if (!string.IsNullOrEmpty(lsCadena))
                    {
                        loFIla.SetField("qr_dep", loFIla.Field<string>("cadena_dep").RegresaCodigoQR());
                        loFIla.SetField("qr_valida_documento", loFIla.Field<string>("id_valida_documento").RegresaQRValidacionDocumentos());
                    }
                }
            }
            loReporte.AgregaParametro("periodo", psPeriodo);
            loReporte.AgregaParametro("descripcion_periodo", psPeriodo.RegresaDescripcionPeriodo());

            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaDatosReporteSGE("cargaacademica"));
            return loReporte.ExportaReporte();
        }

        public byte[] ConstanciaLiberacionAC(string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaDatosConstanciaLiberacionAC(psNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaCertificado(DataTable loDatos, string psIniciales, string psNumero, string psLibro, string psFoja, string psTipo, string psRedondeo, string psDirector, string psFecha, string psExpedida, string psFolio, string psFechaEq)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            loReporte.AgregaParametro("tipo", psTipo);
            loReporte.AgregaParametro("numero", psNumero);
            loReporte.AgregaParametro("fojas", psFoja);
            loReporte.AgregaParametro("rubrica", psIniciales);
            loReporte.AgregaParametro("redondeo", psRedondeo);
            loReporte.AgregaParametro("libro", psLibro);
            loReporte.AgregaParametro("director", psDirector);
            loReporte.AgregaParametro("fecha", psFecha);
            loReporte.AgregaParametro("expedida", psExpedida);
            loReporte.AgregaParametro("folio", psFolio);
            loReporte.AgregaParametro("fechaEq", psFechaEq);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos);
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaDocumentosEscolares(string psNoControl, string psNombreDoc)
        {

            DataTable ldtDatos;
            string lsNoControl;
            string lsNombreDoc;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaDatosDocumentosEscolares(psNoControl, psNombreDoc);
            lsNoControl = "";
            lsNombreDoc = "";
            ldtDatos.Columns["qr_alumno"].ReadOnly = false;
            ldtDatos.Columns["qr_valida_documento"].ReadOnly = false;

            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                if (lsNoControl == loFIla.Field<string>("no_de_control").Trim())
                {
                    continue;
                }
                lsNoControl = loFIla.Field<string>("no_de_control").Trim();

                if (!string.IsNullOrEmpty(loFIla.Field<string>("cadena_alumno")))
                {
                    loFIla.SetField("qr_alumno", loFIla.Field<string>("cadena_alumno").RegresaCodigoQR());
                    loFIla.SetField("qr_valida_documento", loFIla.Field<string>("id_valida_documento").RegresaQRValidacionDocumentos());
                }
            }

            lsNombreDoc = psNombreDoc.ToLower();

            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
                
                if(lsNombreDoc == "avisodeprivacidad" || lsNombreDoc == "cartaresponsiva" || lsNombreDoc == "formatonopertenciaaotroplantel")
			    {
                    loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));
                 }
			    else
			    {
                     loReporte.AgregaOrigenMapeo("DataSet2", RegresaDatosReporteSGE(lsNombreDoc));
                }
            return loReporte.ExportaReporte();
        }

        #endregion
        #region Pagos
        public byte[] RegresaFichaPagoVehicular(string psTipo)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Control_Vehicular_Pago loDato = new Control_Vehicular_Pago(_oSistema);
            ldtDatos = loDato.RegresaDatosFichaPago(psTipo);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaFichaPagoReinscripcion(string psControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumnos_Historial_Pagos loDato = new Alumnos_Historial_Pagos(_oSistema);
            ldtDatos = loDato.RegresaDatosFichaPagoActual(psControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaFichaPagoServicio(string psControl, string psServicio)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumnos_Historial_Servicios loDato = new Alumnos_Historial_Servicios(_oSistema);
            ldtDatos = loDato.RegresaDatosFichaServicio(psControl, psServicio);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        #endregion  
        #region Extraescolares
        public byte[] RegresaRegistroParticipantes(string psPeriodo, int piIdActividad)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Extra_actividades_inscrito loDato = new Extra_actividades_inscrito(_oSistema);
            ldtDatos = loDato.RegresaTablaEstudiantesIncritos(psPeriodo, piIdActividad);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaDatosReporteSGE("participantesactividades"));
            loReporte.AgregaOrigenMapeo("DataSet3", RegresaDatosFirmasExtraescolares(piIdActividad));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaCumplimiento(string psPeriodo, string psControl, string psNombre, string psActividad, string psEntrenador, string psCarrera, string psSemestre, string psDesempeño, string psPromedio, string psOficio, bool pbEs15, string psCredito, byte[] poQr)
        {
            DataTable loTabla;
            DataRow loRow;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            loTabla = new DataTable();

            loTabla.Columns.Add("no_de_control");
            loTabla.Columns.Add("periodo");
            loTabla.Columns.Add("nombre");
            loTabla.Columns.Add("carrera");
            loTabla.Columns.Add("semestre");
            loTabla.Columns.Add("descripcion");
            loTabla.Columns.Add("entrenador");
            loTabla.Columns.Add("promedio");
            loTabla.Columns.Add("desempeño");
            loTabla.Columns.Add("folio");
            loTabla.Columns.Add("qr", typeof(byte[]));

            loRow = loTabla.NewRow();
            loRow["no_de_control"] = psControl;
            loRow["periodo"] = psPeriodo.RegresaDescripcionLargaPeriodo();
            loRow["nombre"] = psNombre;
            loRow["carrera"] = psCarrera;
            loRow["semestre"] = psSemestre;
            loRow["descripcion"] = psActividad;
            loRow["entrenador"] = psEntrenador;
            loRow["promedio"] = Convert.ToInt32(psPromedio);
            loRow["desempeño"] = psDesempeño;
            loRow["folio"] = psOficio;
            loRow["qr"] = poQr;
            loTabla.Rows.Add(loRow);
            if (pbEs15)
            {
                loReporte.AgregaParametro("credito", psCredito);
                loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));
            }
            loReporte.AgregaOrigenMapeo("DataSet1", loTabla);


            return loReporte.ExportaReporte();
        }
        public byte[] RegresaConstanciaCumplimiento(string psPeriodo, string psGrupo, bool pbEs15, string psCredito)
        {
            Sapei.Reportes.Reporte loReporte;
            DataTable loDt;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            switch (psCredito)
            {
                case "TUT":
                    Tutorias_Inscritos loInscrito = new Tutorias_Inscritos(_oSistema);
                    loInscrito.GeneraFolioLiberacionGrupo(psPeriodo, psGrupo);
                    loDt = loInscrito.RegresaTablaEstudiantesIncritos(psPeriodo, null, psGrupo, true);
                    break;
                default:
                    Extra_actividades_inscrito loExtra = new Extra_actividades_inscrito(_oSistema);
                    loExtra.GeneraFolioLiberacionGrupo(psPeriodo, psGrupo);
                    loDt = loExtra.RegresaActividadesPorEstudiante(psPeriodo, "", psGrupo, pbEs15, true);
                    break;

            }

            if (loDt == null || loDt.Rows.Count == 0)
                return ReporteVacio();
            if (pbEs15)
            {
                loReporte.AgregaParametro("credito", psCredito);
                loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));
            }
            loReporte.AgregaOrigenMapeo("DataSet1", loDt);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaResultadosPorActividad(string psPeriodo, int piIdActividad)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Extra_actividades_inscrito loDato = new Extra_actividades_inscrito(_oSistema);
            ldtDatos = loDato.RegresaTablaEstudiantesIncritos(psPeriodo, piIdActividad, true);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaDatosReporteSGE("resultadosparticipantesactividades"));
            loReporte.AgregaOrigenMapeo("DataSet3", RegresaDatosFirmasExtraescolares(piIdActividad));

            return loReporte.ExportaReporte();
        }
        #endregion
        #region Generales
        public byte[] ReporteVacio(string psMensaje = "")
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReporteVacio);
            loReporte.AgregaParametro("Mensaje", psMensaje);
            return loReporte.ExportaReporte();
        }
        #endregion
        #region Tutorias
        public byte[] RegresaResultadosPorTutoria(string psPeriodo, string psGrupo)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Tutorias_Inscritos loDato = new Tutorias_Inscritos(_oSistema);
            ldtDatos = loDato.RegresaListaInscritosPorGrupo(psPeriodo, psGrupo);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        #endregion
        #region Evaluacion

        public byte[] RegresaEvaluacion(string psPeriodo, string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDatos = new Alumno(_oSistema);
            ldtDatos = loDatos.DatosEvaluacion(psPeriodo, psNoControl);
            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                ldtDatos.Columns["qr_valida_documento"].ReadOnly = false;
                ldtDatos.Rows[0].SetField("qr_valida_documento", ldtDatos.Rows[0].Field<string>("id_valida_documento").RegresaQRValidacionDocumentos());
            }

            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }

        public byte[] RegresaEvlDepto(DataTable ldtDatos)
        {
            //string lsCadena;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            
            /*foreach (DataRow loFIla in ldtDatos.Rows)
            {
                lsCadena = ldtDatos.Rows[0].Field<string>("descripcion");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    ldtDatos.Columns["descripcion"].ReadOnly = false;
                    if(lsCadena != "TOTAL")
                    {
                        lsCadena.Substring(0, 2);
                        ldtDatos.Rows[0].SetField("descripcion", lsCadena);

                    }
                   
                }
            }*/
            //loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosHorarioPersonalRecontratacionVB(psPeriodo, psRFC, psClaveArea, psTipoContrato));
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            //loReporte.AgregaOrigenMapeo("DataSet2", loDatos.RegresaHorarioFacilitador(psID));
            //loReporte.AgregaOrigenMapeo("DataSet3", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }

        #endregion
        #region Calificaciones

        public byte[] RegresaGrupoCalificacionActas(string psPeriodo, string psMateria, string psGrupo)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosActa(psPeriodo, psMateria, psGrupo));
            loReporte.AgregaOrigenMapeo("DataSet2", loDatos.RegresaAlumnosActa(psPeriodo, psMateria, psGrupo));
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaDatosReporteFinal(string psPeriodo,  string psUsuario)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosReporteFinal(psPeriodo, psUsuario));
            loReporte.AgregaOrigenMapeo("DataSet2", loDatos.RegresaMateriasReporteFinal(psPeriodo, psUsuario));
            loReporte.AgregaOrigenMapeo("DataSet3", loDatos.RegresaTotalesReporteFinal(psPeriodo, psUsuario));
            return loReporte.ExportaReporte();
        }
        #endregion
        #region Servicio Social
        public byte[] RegresaCartaSS(string psNoControl, string psFolio)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaCartaSS(psNoControl, psFolio);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            string estudiante = ldtDatos.RegresaValorFila<string>("nombre_estudiante");
            loReporte.AgregaParametro("Nombre_estudiante", estudiante);
            string carrera = ldtDatos.RegresaValorFila<string>("carrera");
            loReporte.AgregaParametro("Carrera", carrera);
            string programa = ldtDatos.RegresaValorFila<string>("nombre");
            loReporte.AgregaParametro("Programa", programa);
            string titular = ldtDatos.RegresaValorFila<string>("titular");
            loReporte.AgregaParametro("Titular", titular);
            string cargo_titular = ldtDatos.RegresaValorFila<string>("puesto_cargo");
            loReporte.AgregaParametro("Puesto", cargo_titular);
            loReporte.AgregaParametro("titulo", "Formato para Carta de Presentación de Servicio Social");
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaCartaExternoSS(string psNoControl, string psDependencia)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaCartaSExternoSS(psNoControl, psDependencia);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            string estudiante = ldtDatos.RegresaValorFila<string>("nombre_estudiante");
            loReporte.AgregaParametro("Nombre_estudiante", estudiante);
            string carrera = ldtDatos.RegresaValorFila<string>("carrera");
            loReporte.AgregaParametro("Carrera", carrera);
            string programa = "s/d";
            loReporte.AgregaParametro("Programa", programa);
            string titular = ldtDatos.RegresaValorFila<string>("titular");
            loReporte.AgregaParametro("Titular", titular);
            string cargo_titular = ldtDatos.RegresaValorFila<string>("puesto_cargo");
            loReporte.AgregaParametro("Puesto", cargo_titular);
            loReporte.AgregaParametro("titulo", "Formato para Carta de Presentación de Servicio Social");
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] SolicitudServicioSocial(string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.SolicitudServicio(psNoControl);
            loReporte.AgregaParametro("titulo", "Formato para Solicitud de Servicio Social");
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);

            return loReporte.ExportaReporte();
        }
        public byte[] CartaCompromiso(string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.CartaCompromiso(psNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaParametro("CartaCompromiso", "Formato para Carta Compromiso de Servicio Social");
            return loReporte.ExportaReporte();
        }
        public byte[] CartaAsignacion(string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.CartaAsignacion(psNoControl);
            loReporte.AgregaParametro("CartaAsignacion", "Formato de Carta de Asignación de Servicio Social");
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] ReporteBimestral(string lsNumero, string psNoReporte)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.Bimestre1(lsNumero);
            DateTime fecha_b1 = ldtDatos.Rows[0].RegresaValor<DateTime>("fecha_bimestre_1");
            DateTime fecha_b2 = ldtDatos.RegresaValorFila<DateTime>("fecha_bimestre_2");
            DateTime fecha_b3 = ldtDatos.RegresaValorFila<DateTime>("fecha_bimestre_3");
            loReporte.AgregaParametro("Fecha_B1", fecha_b1.ToString("dd/MM/yyyy"));
            loReporte.AgregaParametro("Fecha_B2", fecha_b2.ToString("dd/MM/yyyy"));
            loReporte.AgregaParametro("Fecha_B3", fecha_b3.ToString("dd/MM/yyyy"));
            loReporte.AgregaParametro("titulo", "Formato para Reporte Bimestral de Servicio Social");
            loReporte.AgregaParametro("numero", psNoReporte);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] EvaluacionCualitativa(string lsNumero, string psNoReporte)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.EvaluacionCualitativaBimestre1(lsNumero);
            loReporte.AgregaParametro("TituloEvaluacionCualitativaPrestador", "Formato de evaluación cualitativa del prestador de servicio social");
            loReporte.AgregaParametro("numero", psNoReporte);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] AutoEvaluacionCualitativaPrestador(string lsNumero, string psReporte)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Reportes loDato = new SS_Reportes(_oSistema);
            ldtDatos = loDato.AutoEvaluacionCualitativaPrestador(lsNumero, psReporte);
            loReporte.AgregaParametro("TituloAutoEvaluacionCualitativaPrestador", "Formato de autoevaluación cualitativa del prestador de servicio social");
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] EvaluacionActividadesPrestador(string lsNumero, string psReporte)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.EvaluacionActividadesPrestador(lsNumero, psReporte);
            loReporte.AgregaParametro("EvaluacionActividadesPrestador", "Formato de evaluación de las actividades por el prestador de servicio social");
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] TarjetaControl(string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.TarjetaControl(psNoControl);
            loReporte.AgregaParametro("TarjetaControl", "Formato de Tarjeta de Control Servicio Social");
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] CartaTerminoFinal(string psNoControl, string psFolio)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            SS_Solicitud loDato = new SS_Solicitud(_oSistema);
            ldtDatos = loDato.CartaTerminoFinal(psNoControl, psFolio);
            loReporte.AgregaParametro("CartaTerminoFinal", "Formato para Constancia de Terminación de Servicio Social");
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            ldtDatos = loDato.ConsultaCalfinal(psNoControl);
            loReporte.AgregaParametro("Desempeno", ldtDatos.RegresaValorFila<string>("promedio").RegresaDesempeño());
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        public byte[] RegresaPlanTrabajo(string loNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            SS_Solicitud loSolicitud = new SS_Solicitud(_oSistema);
            SS_Estado_Solicitud loEstado = new SS_Estado_Solicitud(_oSistema);
            SS_Actividades_Solicitud loActividades = new SS_Actividades_Solicitud(_oSistema);
            string lsPeriodo = loEstado.RegresaPeriodoResgistroSS(loNoControl);
            ldtDatos = loDato.RegresaPlanTrabajo(loNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            string periodo = ldtDatos.RegresaValorFila<string>("periodo").Substring(ldtDatos.RegresaValorFila<string>("periodo").Length - 1, 1);
            loReporte.AgregaParametro("Periodo", periodo);
            ldtDatos = loActividades.RegresaActividades(loNoControl, lsPeriodo);
            loReporte.AgregaOrigenMapeo("DataSet2", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet3", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        #endregion
        #region Residencias Profesionales
        public byte[] RegresaCartaRP(string psNoControl, string psDependencia, string psFolio)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaCartaRP(psNoControl, psDependencia);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));
            loReporte.AgregaParametro("Folio", psFolio);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaSolicitud(string loNoControl)
        {
            Alumno loDato = new Alumno(_oSistema);
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            string CountResidentes;
            string NombreCarrera;
            ldtDatos = loDato.RegresaSolicitud(loNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            CountResidentes = ldtDatos.RegresaValorFila<string>("residentes");
            NombreCarrera = ldtDatos.RegresaValorFila<string>("nombre_carrera");
            loReporte.AgregaParametro("NumeroResidentes", CountResidentes);
            loReporte.AgregaParametro("Carrera", NombreCarrera);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaEvaluacionSeguimiento(string lsNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaEvaluacionSeguimiento(lsNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            string asesor = ldtDatos.RegresaValorFila<string>("asesor_interno");
            loReporte.AgregaParametro("Asesor_interno", asesor);
            return loReporte.ExportaReporte();
        }
        public byte[] ReporteResidenciasProfesionales(string loNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaEvaluacionSeguimiento(loNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            string asesor = ldtDatos.RegresaValorFila<string>("asesor_interno");
            loReporte.AgregaParametro("Asesor_interno", asesor);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaAsignacionInterno(int piProyecto)
        {
            DataTable ldtDatos;
            RP_Solicitud loSolicitud = new RP_Solicitud(_oSistema);
            RP_Datos_Programa loDato = new RP_Datos_Programa(_oSistema);
            ArrayList myAL = new ArrayList();
            string valor = "";
            string lsAcademico = _oSistema.Sesion.Usuario.Nombre;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            ldtDatos = loDato.RegresaAsignacionInterno(piProyecto);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaParametro("JefeAcademico", lsAcademico);
            loReporte.AgregaParametro("Docente", ldtDatos.RegresaValorFila<string>("asesor"));
            ldtDatos = loSolicitud.ConsultaNombreResidentes(piProyecto);
            for (int i = 0; i < ldtDatos.Rows.Count; i++)
            {
                string datos = ldtDatos.Rows[i].RegresaValor<string>("nombre_residentes");
                myAL.Add(datos);
            }
            String[] myArr = (String[])myAL.ToArray(typeof(string));
            for (int a = 0; a < myArr.Length; a++)
            {
                valor = valor + "  " + myArr[a] + ",";
            }
            loReporte.AgregaParametro("Residentes", valor);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaAsesoria(int piNumeroAsesoria, string psNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            ldtDatos = loDato.RegresaAsesoria(piNumeroAsesoria, psNoControl);
            loReporte.AgregaOrigenMapeo("Datos_Asesoria", ldtDatos);
            string nombreAlumno = ldtDatos.RegresaValorFila<string>("nombre_residente");
            loReporte.AgregaParametro("Nombre_residente", nombreAlumno);
            string carrera = ldtDatos.RegresaValorFila<string>("carrera");
            loReporte.AgregaParametro("Carrera", carrera);
            string asesor = ldtDatos.RegresaValorFila<string>("asesor_interno");
            loReporte.AgregaParametro("Asesor_interno", asesor);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaInformeSemestral(string lsNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            RP_Solicitud loSolicitud = new RP_Solicitud(_oSistema);
            ArrayList myAL = new ArrayList();
            ArrayList myNO = new ArrayList();
            int proyecto;
            string nombre = "";
            string noControl = "";
            ldtDatos = loDato.RegresaInformeSemestral(lsNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            proyecto = ldtDatos.RegresaValorFila<int>("id_programa");
            ldtDatos = loSolicitud.ConsultaNombreResidentes(proyecto);
            for (int i = 0; i < ldtDatos.Rows.Count; i++)
            {
                string datos = ldtDatos.Rows[i].RegresaValor<string>("nombre_residentes");
                myAL.Add(datos);
            }
            String[] myArr = (String[])myAL.ToArray(typeof(string));
            for (int a = 0; a < myArr.Length; a++)
            {
                nombre = nombre + "  " + myArr[a] + ",";
            }
            loReporte.AgregaParametro("Residentes", nombre);
            for (int i = 0; i < ldtDatos.Rows.Count; i++)
            {
                string datos = ldtDatos.Rows[i].RegresaValor<string>("no_de_control");
                myNO.Add(datos);
            }
            String[] myNo = (String[])myNO.ToArray(typeof(string));
            for (int a = 0; a < myNo.Length; a++)
            {
                noControl = noControl + "  " + myNo[a] + ",";
            }
            loReporte.AgregaParametro("No_de_control", noControl);
            ldtDatos = loDato.RegresaAsesoriasInforme(lsNoControl);
            loReporte.AgregaOrigenMapeo("DataSet2", ldtDatos);
            return loReporte.ExportaReporte();
        }
        public byte[] RegresaLiberacionInforme(string lsNoControl)
        {
            DataTable ldtDatos;
            Sapei.Reportes.Reporte loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Alumno loDato = new Alumno(_oSistema);
            RP_Solicitud loSolicitud = new RP_Solicitud(_oSistema);
            ArrayList myAL = new ArrayList();
            string nombre = "";
            string revisor;
            int liIdprograma;
            ldtDatos = loDato.RegresaLiberacionInforme(lsNoControl);
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            revisor = ldtDatos.RegresaValorFila<string>("rfc_revisor");
            loReporte.AgregaParametro("Revisor", revisor);
            liIdprograma = ldtDatos.RegresaValorFila<int>("id_programa");
            ldtDatos = loSolicitud.ConsultaNombreResidentes(liIdprograma);
            for (int i = 0; i < ldtDatos.Rows.Count; i++)
            {
                string datos = ldtDatos.Rows[i].RegresaValor<string>("nombre_residentes");
                myAL.Add(datos);
            }
            String[] myArr = (String[])myAL.ToArray(typeof(string));
            for (int a = 0; a < myArr.Length; a++)
            {
                nombre = nombre + "  " + myArr[a] + ",";
            }
            loReporte.AgregaParametro("Residentes", nombre);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        #endregion
        #region Reinscripcion
        public byte[] RegresaListaReinscripcion(string psCarrera, DataTable poTabla)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            loReporte.AgregaParametro("periodo", _oSistema.Sesion.Periodo.PeriodoActual.RegresaDescripcionPeriodo());
            loReporte.AgregaParametro("carrera", psCarrera.ToEnum<enmCarreras>().RegresaNombreCompeto());
            loReporte.AgregaOrigenMapeo("DataSet1", poTabla);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }
        #endregion
        #region Recontratacion
        public byte[] RegresaDatosHorarioRecontratacion(int psID)
        {
            DataTable ldtDatos;
            string lsCadena;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            ldtDatos = loDatos.RegresaDatosHorarioPersonalRecontratacion(psID);
            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_jefe");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    ldtDatos.Columns["qr_jefe"].ReadOnly = false;
                    ldtDatos.Rows[0].SetField("qr_jefe", ldtDatos.Rows[0].Field<string>("cadena_jefe").RegresaCodigoQR());
                    lsCadena = ldtDatos.Rows[0].Field<string>("cadena_vb");

                    if (!string.IsNullOrEmpty(lsCadena))
                    {
                        ldtDatos.Columns["qr_vb"].ReadOnly = false;
                        ldtDatos.Rows[0].SetField("qr_vb", ldtDatos.Rows[0].Field<string>("cadena_vb").RegresaCodigoQR());
                        lsCadena = ldtDatos.Rows[0].Field<string>("cadena_dir");
                        if (!string.IsNullOrEmpty(lsCadena))
                        {
                            ldtDatos.Columns["qr_dir"].ReadOnly = false;
                            ldtDatos.Rows[0].SetField("qr_dir", ldtDatos.Rows[0].Field<string>("cadena_dir").RegresaCodigoQR());
                        }
                    }
                }
            }
            //loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosHorarioPersonalRecontratacionVB(psPeriodo, psRFC, psClaveArea, psTipoContrato));
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }

        public byte[] RegresaDatosHorarioFacilitador(int psID)
        {
            DataTable ldtDatos;
            string lsCadena;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            ldtDatos = loDatos.RegresaDatosHorarioPersonalFacilitadorFE(psID);
            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_jefe");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    ldtDatos.Columns["qr_jefe"].ReadOnly = false;
                    ldtDatos.Rows[0].SetField("qr_jefe", ldtDatos.Rows[0].Field<string>("cadena_jefe").RegresaCodigoQR());
                    lsCadena = ldtDatos.Rows[0].Field<string>("cadena_vb");

                    if (!string.IsNullOrEmpty(lsCadena))
                    {
                        ldtDatos.Columns["qr_vb"].ReadOnly = false;
                        ldtDatos.Rows[0].SetField("qr_vb", ldtDatos.Rows[0].Field<string>("cadena_vb").RegresaCodigoQR());
                        lsCadena = ldtDatos.Rows[0].Field<string>("cadena_dir");
                        if (!string.IsNullOrEmpty(lsCadena))
                        {
                            ldtDatos.Columns["qr_dir"].ReadOnly = false;
                            ldtDatos.Rows[0].SetField("qr_dir", ldtDatos.Rows[0].Field<string>("cadena_dir").RegresaCodigoQR());
                        }
                    }
                }
            }
            //loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosHorarioPersonalRecontratacionVB(psPeriodo, psRFC, psClaveArea, psTipoContrato));
            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            loReporte.AgregaOrigenMapeo("DataSet2", loDatos.RegresaHorarioFacilitador(psID));
            loReporte.AgregaOrigenMapeo("DataSet3", RegresaFondoMembretado(enmImagenes.FondoCarta.ToString()));

            return loReporte.ExportaReporte();
        }

        public byte[] RegresaDatosContratoAsimilados(string psPeriodo, string psRFC)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosPersonalContratoAsimilados(psPeriodo, psRFC));
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaDatosContratoHonorarios(int psID)
        {
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            loReporte.AgregaOrigenMapeo("DataSet1", loDatos.RegresaDatosPersonalContratoHonorarios(psID));
            return loReporte.ExportaReporte();
        }

        public byte[] RegresaDatosContratoFacilitadorCLE(int psID)
        {
            DataTable ldtDatos;
            string lsCadena;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            ldtDatos = loDatos.RegresaDatosPersonalContratoFacilitadoresCLE(psID);
            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_personal");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    ldtDatos.Columns["qr_personal"].ReadOnly = false;
                    ldtDatos.Rows[0].SetField("qr_personal", ldtDatos.Rows[0].Field<string>("cadena_personal").RegresaCodigoQR());
                    lsCadena = ldtDatos.Rows[0].Field<string>("cadena_jefe");

                    if (!string.IsNullOrEmpty(lsCadena))
                    {
                        ldtDatos.Columns["qr_jefe"].ReadOnly = false;
                        ldtDatos.Rows[0].SetField("qr_jefe", ldtDatos.Rows[0].Field<string>("cadena_jefe").RegresaCodigoQR());
                        lsCadena = ldtDatos.Rows[0].Field<string>("cadena_sub_admin");

                        if (!string.IsNullOrEmpty(lsCadena))
                        {
                            ldtDatos.Columns["qr_sub_admin"].ReadOnly = false;
                            ldtDatos.Rows[0].SetField("qr_sub_admin", ldtDatos.Rows[0].Field<string>("cadena_sub_admin").RegresaCodigoQR());
                            lsCadena = ldtDatos.Rows[0].Field<string>("cadena_rh");

                            if (!string.IsNullOrEmpty(lsCadena))
                            {
                                ldtDatos.Columns["qr_rh"].ReadOnly = false;
                                ldtDatos.Rows[0].SetField("qr_rh", ldtDatos.Rows[0].Field<string>("cadena_rh").RegresaCodigoQR());
                                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_dir");

                                if (!string.IsNullOrEmpty(lsCadena))
                                {
                                    ldtDatos.Columns["qr_dir"].ReadOnly = false;
                                    ldtDatos.Rows[0].SetField("qr_dir", ldtDatos.Rows[0].Field<string>("cadena_dir").RegresaCodigoQR());

                                    ldtDatos.Columns["qr_valida_documento"].ReadOnly = false;
                                    ldtDatos.Rows[0].SetField("qr_valida_documento", ldtDatos.Rows[0].Field<string>("id_valida_documento").RegresaQRValidacionDocumentos());
                                }

                            }
                        }
                    }
                }
            }

            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();

        }

        public byte[] RegresaDatosContratoGeneralFE(int psID)
        {
            DataTable ldtDatos;
            string lsCadena;
            Sapei.Reportes.Reporte loReporte;
            loReporte = new Sapei.Reportes.Reporte(this.RutaReportes);
            Personal loDatos = new Personal(_oSistema);
            ldtDatos = loDatos.RegresaDatosPersonalContratoGeneralFE(psID);
            foreach (DataRow loFIla in ldtDatos.Rows)
            {
                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_personal");
                if (!string.IsNullOrEmpty(lsCadena))
                {
                    ldtDatos.Columns["qr_personal"].ReadOnly = false;
                    ldtDatos.Rows[0].SetField("qr_personal", ldtDatos.Rows[0].Field<string>("cadena_personal").RegresaCodigoQR());
                    lsCadena = ldtDatos.Rows[0].Field<string>("cadena_jefe");

                    if (!string.IsNullOrEmpty(lsCadena))
                    {
                        ldtDatos.Columns["qr_jefe"].ReadOnly = false;
                        ldtDatos.Rows[0].SetField("qr_jefe", ldtDatos.Rows[0].Field<string>("cadena_jefe").RegresaCodigoQR());
                        lsCadena = ldtDatos.Rows[0].Field<string>("cadena_sub_admin");

                        if (!string.IsNullOrEmpty(lsCadena))
                        {
                            ldtDatos.Columns["qr_sub_admin"].ReadOnly = false;
                            ldtDatos.Rows[0].SetField("qr_sub_admin", ldtDatos.Rows[0].Field<string>("cadena_sub_admin").RegresaCodigoQR());
                            lsCadena = ldtDatos.Rows[0].Field<string>("cadena_rh");

                            if (!string.IsNullOrEmpty(lsCadena))
                            {
                                ldtDatos.Columns["qr_rh"].ReadOnly = false;
                                ldtDatos.Rows[0].SetField("qr_rh", ldtDatos.Rows[0].Field<string>("cadena_rh").RegresaCodigoQR());
                                lsCadena = ldtDatos.Rows[0].Field<string>("cadena_dir");

                                if (!string.IsNullOrEmpty(lsCadena))
                                {
                                    ldtDatos.Columns["qr_dir"].ReadOnly = false;
                                    ldtDatos.Rows[0].SetField("qr_dir", ldtDatos.Rows[0].Field<string>("cadena_dir").RegresaCodigoQR());

                                    ldtDatos.Columns["qr_valida_documento"].ReadOnly = false;
                                    ldtDatos.Rows[0].SetField("qr_valida_documento", ldtDatos.Rows[0].Field<string>("id_valida_documento").RegresaQRValidacionDocumentos());
                                }

                            }
                        }
                    }
                }
            }

            loReporte.AgregaOrigenMapeo("DataSet1", ldtDatos);
            return loReporte.ExportaReporte();

        }
        #endregion
    }
}