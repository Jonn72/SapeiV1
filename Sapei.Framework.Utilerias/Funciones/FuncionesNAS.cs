using Sapei.Framework.Utilerias.ExpedienteDigital;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;

namespace Sapei.Framework.Utilerias.Funciones
{
	public static class FuncionesNAS
	{

		#region Generales

		public static bool GeneraCarpeta(string psRuta)
		{

			if (Directory.Exists(psRuta))
			{
				return true;
			}
			else
			{
				Directory.CreateDirectory(psRuta);
			}

			return true;
		}

		#endregion
		#region Alumno

		#endregion

	}
}

