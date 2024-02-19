using appSapei.App_Start;
using Sapei;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Security;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace appSapei.Controllers
{
	public class FacilitadoresController : Controller
	{
		IntPtr wow64Value = IntPtr.Zero;
		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.FCL)]
		[HttpGet]
		public ActionResult Index()
		{
			try
			{
				return View();
			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return RedirectToAction("Index", "Home");
			}
		}

		[SessionExpire(Sapei.Framework.Configuracion.enmRolUsuario.FCL)]
		public PartialViewResult CapturaCalificaciones(string psNivel = null, string psGrupo = null, string psNMateria = null)
		{
			try
			{
				string lsPeriodo;
				string lsUsuario;
				lsUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				lsPeriodo = SesionSapei.Sistema.Sesion.Periodo.PeriodoActual;
				if (string.IsNullOrEmpty(psGrupo))
				{

					ViewData["periodo"] = lsPeriodo;
					ViewData["docente"] = lsUsuario;
					ViewData["periodo_desc"] = lsPeriodo.RegresaDescripcionPeriodo();
					ViewData["Tabla"] = loPersonal.RegresaGruposFacilitador(lsPeriodo, lsUsuario);

					ViewData["Titulo"] = "Lista de Grupos";
					ViewData["Encabezados"] = new List<string> { "Nivel", "Grupo", " " };

					return PartialView("CapturaCalificaciones");
				}
				else
				{
					
					ViewData["docente"] = lsUsuario;
					ViewData["Tabla"] = loPersonal.RegresaAlumnosFacilitador(lsPeriodo, psNivel, psGrupo);
					ViewData["Titulo"] = "Lista de Alumnos de la Materia " + psNMateria + " del Grupo " + psGrupo;
					ViewData["Objeto"] = loPersonal.RegresaObjetoCapturaFacilitador(lsPeriodo, psNivel, psGrupo);
					return PartialView("Calificaciones");
				}

			}
			catch (Exception ex)
			{
				SesionSapei.Sistema.GrabaLog(ex);
				return PartialView("Index");
			}
		}
		public FileContentResult EEX(string psPeriodo = null, string psNivel = null, string psGrupo = null)
		{
			try
			{
				Personal loPersonal;
				loPersonal = new Personal(SesionSapei.Sistema);
				DataTable loDt = loPersonal.RegresaAlumnosFacilitadorExcel(psPeriodo, psNivel, psGrupo);
				DataTable loDt2 = loPersonal.RegresaDatosFacilitadorExcel(psPeriodo, psNivel, psGrupo);
				string[] columns = { "Numero", "No. de control", "Nombre" };
				byte[] filecontent = ExportExcelController.ExportExcel2(loDt, loDt2, psNivel, true, columns);
				return File(filecontent, ExportExcelController.ExcelContentType, psNivel + "-" + psGrupo + ".xlsx");
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return null;
			}
		}

		public JsonResult RegistraCalificacionFacilitadorJSON(string lsobjeto)
		{
			try
			{
				string lsMensaje;
				List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
				DataTable loDt = new DataTable();
				System.Data.SqlClient.SqlDataReader loReader;
				using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
				{
					loParametros.Add(new ParametrosSQL("@objeto", lsobjeto));
					loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pam_fcl_captura_calificacion", loParametros);
					lsMensaje = "";
					if (loReader.HasRows)
					{
						while (loReader.Read())
						{
							lsMensaje = loReader.GetString(0);
						}
					}
					loReader.Close();
					loReader.Dispose();
					if (lsMensaje.Trim() != "1")
					{
						ViewData["Mensaje"] = "Error de Captura";
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(lsMensaje, false);
					}
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeJsonBusqueda(false);
			}
		}

		public JsonResult FirmaActa(string psFirma, string psPeriodo, string psNivel, string psGrupo, string psFacilitador)
		{

			try
			{
				string firmaCifrada = Sapei.Framework.Utilerias.Funciones.FuncionesCifrado.RegresaFirmaPersonalMD5(psFirma);

				if (firmaCifrada.Trim() != SesionSapei.Sistema.Sesion.Usuario.Contraseña)
				{
					return ManejoMensajesJson.RegresaMensajeJsonBusqueda("No se reconoce la contraseña", false);
				}
                else 
				{ 
					using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
					{
						string psUsuario = SesionSapei.Sistema.Sesion.Usuario.Usuario;
						PersonalSoicitud loPersonal_Solicitud;
						loPersonal_Solicitud = new PersonalSoicitud(SesionSapei.Sistema);
						loPersonal_Solicitud.RegistraCadenaFIELActaCLE(psPeriodo, psNivel, psGrupo, psFacilitador, enmTiposDocumentos.ActaCLE);
						return ManejoMensajesJson.RegresaMensajeJsonBusqueda(true);
					}
				}
			}

			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				return ManejoMensajesJson.RegresaMensajeAlertError();
			}
		}


		public PartialViewResult Latex()
	{

			string rutaCompleta = @" C:\\Users\\Manuel\\Source\\Repos\\Sapei\\appSapei\\texample.tex";
			List<ParametrosSQL> loParametros = new List<ParametrosSQL>();
			System.Data.SqlClient.SqlDataReader loReader;
			string Gtex = "";
			using (var loConexion = new ManejaConexion(SesionSapei.Sistema.Conexion))
			{
				loReader = SesionSapei.Sistema.Conexion.EjecutaProcedimientoAlmacenadoDataReader("pac_tex_genera_sgc");
				if (loReader.HasRows)
				{
					while (loReader.Read())
					{
						Gtex = loReader.GetString(0);
					}
				}
				loReader.Close();
				loReader.Dispose();
			}
			var url = $"http://localhost:31678/WebService1.asmx/HelloWorld";
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = 0;
			var bytes = default(byte[]);
			try
			{
				if (System.IO.File.Exists(rutaCompleta))
				{
					System.IO.File.Delete(rutaCompleta);
				}
                
				using (FileStream fs = System.IO.File.Create(rutaCompleta))
				{
					byte[] info = new UTF8Encoding(true).GetBytes(Gtex);
					// Add some information to the file.
					fs.Write(info, 0, info.Length);

				}
				using (WebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (Stream strReader = response.GetResponseStream())
					{
						if (strReader == null) return PartialView("Index");
						using (StreamReader objReader = new StreamReader(strReader))
						{
							using (MemoryStream ms = new MemoryStream())
							{
								objReader.BaseStream.CopyTo(ms);
								bytes = ms.ToArray();

								string base64 = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
								base64 = base64.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n", "").Replace("<base64Binary xmlns=\"http://tempuri.org/\">", "").Replace("</base64Binary>", "");
								ViewData["pdfbase64"] = base64;
								ViewData["mensaje"] = "Documento generado correctamente";
								return PartialView("../Generales/VisorPDF");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Sapei.Framework.Utilerias.Funciones.FuncionesWeb.ManejaExcepcion(ex);
				ViewData["mensaje"] = "Error al descargar reporte";
				return PartialView("AvisosGenerales", "Generales");
			}
		}
	}
}