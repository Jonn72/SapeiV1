using Sapei.Framework.Bitacora;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSapei.Clases
{
	public static class ProcesosMQ
	{
		#region Expediente Digital
		public static void ArchivaCargasAcademicas()
		{
			BitacoraProcesos loBitacora;
			try
			{
				loBitacora = new BitacoraProcesos(SesionSapei.Sistema, enmProcesosMQ.CARGAACADEMICA);
				loBitacora.Iniciar();
			}
			catch (Exception ex)
			{
				
			}
		}
		#endregion
	}
}