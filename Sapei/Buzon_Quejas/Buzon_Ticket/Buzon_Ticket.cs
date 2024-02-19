using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Sapei.Framework;
using Sapei.Framework.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sapei
{
	/// <summary>
	/// Clase buzon_ticket generada automáticamente desde el Generador de Código SII
	/// </summary>
	[Serializable]
	public partial class Buzon_Ticket:CatalogoFijoElemento
	{
		#region Contructor
		/// <summary>
		/// Inicia una nueva instancia de la clase buzon_ticket.
		/// </summary>
		public Buzon_Ticket():base()
		{
			NombreTabla = "buzon_ticket";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		/// <summary>
		/// Inicia una nueva instancia de la clase buzon_ticket.
		/// </summary>
		/// <param name="poSistema">Clase del Sistema</param>
		public Buzon_Ticket(Sistema poSistema):base (poSistema)
		{
			NombreTabla = "buzon_ticket";
			Propietario= "dbo";
			RutaTabla = string.Format("[{0}].[{1}].[{2}]", _oSistema.Servidor.Principal.BaseDatos.Catalogo, Propietario, NombreTabla);
			CargaPropiedadesdeColumna();

		}
		#endregion
		#region Propiedades
		/// <summary>
		/// Obtiene o establece ticket.Sin descripcion para ticket 
		/// </summary>
		/// <value>
		/// ticket 
		/// </value>
		[Key]
		[Required]
		public Int32 ticket
		{
			get
			{
				return ObtenerValorPropiedad<Int32>("ticket");
			}

			set
			{
				EstablecerValorPropiedad<Int32>("ticket", value);
			}

		}
		/// <summary>
		/// Obtiene o establece fecha_apertura.Sin descripcion para fecha_apertura 
		/// </summary>
		/// <value>
		/// fecha_apertura 
		/// </value>
		[Required]
		public DateTime fecha_apertura
		{
			get
			{
				return ObtenerValorPropiedad<DateTime>("fecha_apertura");
			}

			set
			{
				EstablecerValorPropiedad<DateTime>("fecha_apertura", value);
			}

		}
		/// <summary>
		/// Obtiene o establece status_ticket.Sin descripcion para status_ticket 
		/// </summary>
		/// <value>
		/// status_ticket 
		/// </value>
		[Required]
		public Boolean status_ticket
		{
			get
			{
				return ObtenerValorPropiedad<Boolean>("status_ticket");
			}

			set
			{
				EstablecerValorPropiedad<Boolean>("status_ticket", value);
			}

		}
		/// <summary>
		/// Obtiene o establece titulo.Sin descripcion para titulo 
		/// </summary>
		/// <value>
		/// titulo 
		/// </value>
		[Required]
		[MaxLength (50)]
		[DefaultValue(null)]
		public string titulo
		{
			get
			{
				return ObtenerValorPropiedad<string>("titulo");
			}

			set
			{
				EstablecerValorPropiedad<string>("titulo", value);
			}

		}
		/// <summary>
		/// Obtiene o establece quien_apertura.Sin descripcion para quien_apertura 
		/// </summary>
		/// <value>
		/// quien_apertura 
		/// </value>
		[Required]
		[MaxLength (13)]
		[DefaultValue(null)]
		public string quien_apertura
		{
			get
			{
				return ObtenerValorPropiedad<string>("quien_apertura");
			}

			set
			{
				EstablecerValorPropiedad<string>("quien_apertura", value);
			}

		}
		#endregion
		#region Funciones
		/// <summary>
		/// Carga un registro especifico denotado por las llaves de la tabla buzon_ticket.		/// </summary>
		/// <param name="piticket">ticket</param>
		public void Cargar(Int32 piticket)
		{
			base.Cargar(piticket);
		}
		/// <summary>
		/// Este metodo se declara para que las clases que hereden, implementen el metodo con la carga de los metadatos en
		/// otro metodo estatico. 
		/// </summary>
		protected override void CargaPropiedadesdeColumna()
		{
			 PropiedadesColumna<Int32> loColInt32; 
			 PropiedadesColumna<DateTime> loColDateTime; 
			 PropiedadesColumna<Boolean> loColBoolean; 
			 PropiedadesColumna<string> loColstring; 
			if (!Object.Equals(CamposLlave,null) && !Object.Equals(Propiedades,null)) 
			  return;

			CamposLlave= new Dictionary<string,object>(1);
			Propiedades = new Dictionary<string, Propiedad>(5);

			AgregaCampoLlave("ticket",null);

			loColInt32 = new PropiedadesColumna<Int32>();
			loColInt32.EsPrimaryKey = true;
			loColInt32.Longitud = 4;
			loColInt32.Precision = 10;
			loColInt32.EsRequeridoBD = true;
			loColInt32.CampoId = 0;
			loColInt32.Descripcion = "Sin descripcion para ticket";
			loColInt32.EsIdentity = true;
			loColInt32.Tipo = typeof(Int32);
			AgregarPropiedad<Int32>("ticket", loColInt32); 

			loColDateTime = new PropiedadesColumna<DateTime>();
			loColDateTime.EsPrimaryKey = false;
			loColDateTime.Longitud = 8;
			loColDateTime.Precision = 23;
			loColDateTime.EsRequeridoBD = true;
			loColDateTime.CampoId = 1;
			loColDateTime.Descripcion = "Sin descripcion para fecha_apertura";
			loColDateTime.EsIdentity = false;
			loColDateTime.Tipo = typeof(DateTime);
			AgregarPropiedad<DateTime>("fecha_apertura", loColDateTime); 

			loColBoolean = new PropiedadesColumna<Boolean>();
			loColBoolean.EsPrimaryKey = false;
			loColBoolean.Longitud = 1;
			loColBoolean.Precision = 1;
			loColBoolean.EsRequeridoBD = true;
			loColBoolean.CampoId = 2;
			loColBoolean.Descripcion = "Sin descripcion para status_ticket";
			loColBoolean.EsIdentity = false;
			loColBoolean.Tipo = typeof(Boolean);
			AgregarPropiedad<Boolean>("status_ticket", loColBoolean); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 50;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 3;
			loColstring.Descripcion = "Sin descripcion para titulo";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("titulo", loColstring); 

			loColstring = new PropiedadesColumna<string>();
			loColstring.Valor = null;
			loColstring.EsPrimaryKey = false;
			loColstring.Longitud = 13;
			loColstring.Precision = 0;
			loColstring.EsRequeridoBD = true;
			loColstring.CampoId = 4;
			loColstring.Descripcion = "Sin descripcion para quien_apertura";
			loColstring.EsIdentity = false;
			loColstring.Tipo = typeof(string);
			AgregarPropiedad<string>("quien_apertura", loColstring); 
			}
			#endregion

		}
	}
