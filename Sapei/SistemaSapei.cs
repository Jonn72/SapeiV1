using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Sapei.Framework;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Configuracion;
using Sapei.Framework.Utilerias;

namespace Sapei
{
	/// <summary>
	/// Clase principal ´para el manejor del sistema gobweb
	/// </summary>
	[Serializable]
	public class SistemaSapei : Sistema
	{
		#region Variables

		#endregion

		#region Propiedades
		public bool BloqueaUsuario { get; set; }
		public bool esConexionLocalHost { get; set; }
		public bool esConexionInterna { get; set; }
		public string RutaUploads { get; set; }
		#endregion

		#region Constructores

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public SistemaSapei()
			 : base()
		{
		}

		/// <summary>
		/// Constructor sobre cargado para configurar conforme de un arhivo .ini
		/// </summary>
		/// <param name="psArchivoINI">Nombre del archivo .ini</param>
		/// <param name="penPlataforma">Plataforma del sistema</param>
		/// <param name="penSistema">Version del Sistema</param>
		public SistemaSapei(Sapei.Framework.Configuracion.enmSistema penSistema)
			 : base(penSistema)
		{
		}

		#endregion

		#region Funciones

		public void IniciaSesion()
		{
			this.Sesion.Pais = Framework.Configuracion.enmPais.MEXICO;
			this.Sesion.Lenguaje = Framework.Configuracion.enmLenguaje.ESPAÑOL;
		}
		/// <summary>
		/// Funcion que inicia sesion con el nuevo framework
		/// </summary>
		/// <param name="piEmpresa">Empresa</param>
		/// <param name="piEjercicio">Ejercicio</param>
		/// <param name="piPeriodo">Periodo</param>
		/// <param name="piUsuario">Usuario</param>          
		public void IniciaSesion(short piInstitucion, SisUsuario poUsuario)
		{
			this.Sesion.Pais = Framework.Configuracion.enmPais.MEXICO;
			this.Sesion.Lenguaje = Framework.Configuracion.enmLenguaje.ESPAÑOL;
			this.CargaDatosInstitucion(piInstitucion);
			this.CargaSesionUsuario(poUsuario);
			this.CargaMenuUsuario();
		}
		/// <summary>
		/// Funcion para cerrar sesion, solo se reinicia la configuracion de usuario
		/// </summary>
		public void CerrarSesion()
		{
			this.Sesion.Usuario = new ConfiguracionUsuario();
		}
		public void RenuevaParametros()
		{
			Aspirantes_Periodos loPeriodo = new Aspirantes_Periodos(this);
			this.Sesion.Periodo.ActivaRegistroAspirantes = loPeriodo.EsProcesoActivo();
		}
		public string SolicitaContraseña(string psUsuario, string psTipo)
		{
			DataTable ldtTabla = new DataTable(); 
			List<Framework.BaseDatos.ParametrosSQL> loParametros = new List<Framework.BaseDatos.ParametrosSQL>();
			loParametros.Add(new Framework.BaseDatos.ParametrosSQL("@usuario", psUsuario));
			loParametros.Add(new Framework.BaseDatos.ParametrosSQL("@tipo", psTipo));
			using (var loConexion = new ManejaConexion(this.Conexion))
			{
				ldtTabla.Load(this.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pap_sis_solicita_contraseña", loParametros));
			}
			if (ldtTabla.Columns.Count == 1)
			{
				return ldtTabla.Rows[0].RegresaValor<string>("mensaje");
			}
			this.Sesion.Usuario.SolicitudContraseñaActiva = true;
			this.Sesion.Usuario.Usuario = psUsuario;
			this.Sesion.Usuario.Contraseña = ldtTabla.Rows[0].RegresaValor<string>("contraseña");
			this.Sesion.Usuario.Correo = ldtTabla.Rows[0].RegresaValor<string>("correo");
			this.Sesion.Usuario.Nombre = ldtTabla.Rows[0].RegresaValor<string>("nombre");
			return "";
		}
		/// <summary>
		/// Funcion que carga los datos para la empresa
		/// </summary>
		/// <param name="piInstitucion">Empresa</param>
		private void CargaDatosInstitucion(short piInstitucion)
		{
			Institucion loInstitucion;
			loInstitucion = new Institucion(this);
			loInstitucion.Cargar(piInstitucion);
			this.Sesion.Institucion.Numero = piInstitucion;
			this.Sesion.Institucion.Nombre = loInstitucion.Nombre;
			this.Sesion.Institucion.RazonSocial = string.Format("[{0}] {1}", piInstitucion, loInstitucion.RazonSocial);
			this.Sesion.Institucion.Direccion = loInstitucion.Calle + " " + loInstitucion.NoExterior;
			this.Sesion.Institucion.RFC = loInstitucion.RFC;
			this.Sesion.Institucion.NombreDirector = loInstitucion.RegresaNombreDirector();
		}


		/// <summary>
		/// Funcion que carga la informacion para el usuario
		/// </summary>
		/// <param name="piEmpresa">Empresa</param>
		/// <param name="piUsuario">Usuario</param>
		private void CargaSesionUsuario(SisUsuario poUsuario)
		{

			this.Sesion.Usuario.Usuario = poUsuario.Usuario;
			this.Sesion.Usuario.Contraseña = poUsuario.Contraseña;
			this.Sesion.Usuario.Nombre = poUsuario.Nombre;
			this.Sesion.Usuario.RolUsuario = poUsuario.Rol;
			this.Sesion.Usuario.Correo = poUsuario.Correo;
			if(Convert.ToString(poUsuario.Rol) == "PER") 
			{
				this.Sesion.Usuario.TipoUsuario = enmTipoUsuario.PERSONAL;
			}
            else { 
			this.Sesion.Usuario.TipoUsuario = (enmTipoUsuario)poUsuario.TipoUsuario;
			}
			this.Sesion.Usuario.Permisos = poUsuario.Permisos;
			this.Sesion.Usuario.PermisosFuncion = poUsuario.PermisosFuncion;
			this.Sesion.Usuario.PermisosCarreras = poUsuario.PermisosCarreras;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="poUsuario"></param>
		private void CargaMenuUsuario()
		{
			this.Sesion.Menus.MenuVertical = RegresaMenuVertical();
		}

		/// <summary>
		/// 
		/// </summary>
		private string RegresaMenuVertical()
		{
			StringBuilder lsQuery;
			lsQuery = new StringBuilder();
			lsQuery.Append("(Select S.clave, padre, descripcion, nivel, url, icono From sis_menus S, sis_menus_rol R");
			lsQuery.Append(" where");
			lsQuery.AppendFormat(" (SUBSTRING(S.clave,1,LEN(TRIM(R.clave))+1) =  TRIM(R.clave)+'.') AND tipo_usuario = '{0}')", Sesion.Usuario.RolUsuario);
			lsQuery.Append(" UNION ");
			lsQuery.Append("(Select S.clave, padre, descripcion, nivel, url, icono From sis_menus S, sis_menus_rol R");
			lsQuery.Append(" where");
			lsQuery.AppendFormat(" S.clave =  R.clave AND tipo_usuario = '{0}')", Sesion.Usuario.RolUsuario);
			return GeneraArbolMenuVertical(Conexion.RegresaDataTable(lsQuery));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="poData"></param>
		/// <returns></returns>
		private string GeneraArbolMenu(DataTable poData)
		{
			StringBuilder loMenu;
			ArbolDeMenu loArbol;
			string lsClave;
			string lsUrl;
			bool lbTienePermiso;
			loArbol = new ArbolDeMenu();
			loMenu = new StringBuilder();
			Sesion.Usuario.FuncionesPermitidas = new List<string>();
			foreach (DataRow loRow in poData.Rows)
			{
				lsClave = loRow.RegresaValor<string>("Clave");
				lbTienePermiso = TienePermisoFuncion(lsClave);
				lsUrl = loRow.RegresaValor<string>("URL");
				loArbol.Insertar(lsClave, loRow.RegresaValor<string>("Padre"), loRow.RegresaValor<int>("Nivel"), loRow.RegresaValor<string>("Descripcion"), lsUrl, lbTienePermiso);
				if (!string.IsNullOrEmpty(lsUrl) && lbTienePermiso)
					Sesion.Usuario.FuncionesPermitidas.Add(lsUrl);
			}
			return loArbol.ToStringHtml();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="poData"></param>
		/// <returns></returns>
		private string GeneraArbolMenuVertical(DataTable poData)
		{
			StringBuilder loMenu;
			ArbolDeMenu loArbol;
			string lsClave;
			string lsUrl;
			bool lbTienePermiso;
			loArbol = new ArbolDeMenu();
			loMenu = new StringBuilder();
			Sesion.Usuario.FuncionesPermitidas = new List<string>();
			foreach (DataRow loRow in poData.Rows)
			{
				lsClave = loRow.RegresaValor<string>("clave");
				lbTienePermiso = TienePermiso2Funcion(lsClave);
				lsUrl = loRow.RegresaValor<string>("url");
				if (!string.IsNullOrEmpty(lsUrl) && !lbTienePermiso)
					continue;
				loArbol.Insertar(lsClave, loRow.RegresaValor<string>("padre"), loRow.RegresaValor<int>("nivel"), loRow.RegresaValor<string>("descripcion"), lsUrl, lbTienePermiso, loRow.RegresaValor<string>("icono"));
			}
			return loArbol.ToStringHtmlVertical();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="psClave"></param>
		/// <returns></returns>
		public bool TienePermisoFuncion(string psClave)
		{
			if (Sesion.Usuario.RolUsuario == enmRolUsuario.ASP || Sesion.Usuario.RolUsuario == enmRolUsuario.ALU)
				return true;
			return Sesion.Usuario.Permisos.Contains(psClave.Trim());
		}
		public bool TienePermiso2Funcion(string psClave)
		{
			if (Sesion.Usuario.RolUsuario == enmRolUsuario.ASP)
				return true;
			return Sesion.Usuario.PermisosFuncion.Contains(psClave.Trim());
		}
		public bool TienePermisoCarrera(string psCarrera)
		{
			return Sesion.Usuario.PermisosCarreras.Contains(psCarrera.Trim());
		}
		#endregion
	}
}
