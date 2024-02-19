using Sapei.Framework.ProcesoMQ;
using Sapei.Framework.Utilerias;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Sapei.Procesos
{
    /// <summary>
    /// Esta clase maneja los metodos para los procesos
    /// </summary>
    public partial class Procesos : IDisposable
    {
        private enmProcesosMQ _enProceso;
        private VariableSesionProcesos _oProcesos;
        SistemaSapei _oSistemaSapei;
        InformacionMensaje _oInformacion;
        /// <summary>
        /// 
        /// </summary>
        public Procesos()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="poSistemaSapei"></param>
        public Procesos(SistemaSapei poSistemaSapei)
        {
            _oSistemaSapei = poSistemaSapei;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Delegado para el log de mensaje
        /// </summary>
        /// <param name="psMensaje">Mensaje de error</param>
        /// /// <param name="pemTipoError">Mensaje de error</param>
        public delegate void LogMessageDelegate(string psMensaje, EventLogEntryType pemTipoError);

        /// <summary>
        /// FUncion para escribir el mensaje de log
        /// </summary>

        public event LogMessageDelegate LogMessage;

        private void Procesando()
        {
            switch (_enProceso)
            {
                case enmProcesosMQ.CARGAACADEMICA:

                    ProcesaCargasAcademicas(_oProcesos._oInformacion.Folio, _oProcesos._oInformacion.Periodo);
                    break;
                default:
                    this.LogMessage(string.Format("No existe el modulo{0} para procesarlo", _enProceso.ToString()), EventLogEntryType.Error);
                    break;
            }
        }

        /// <summary>
        /// Procesa el mensaje 
        /// </summary>
        /// <param name="poInformacion">Informacion para procesar el mensaje</param>
        public void ProcesandoMensaje(Object poInformacion)
        {
            _oInformacion = poInformacion as InformacionMensaje;
            try
            {
                if (_oInformacion.Folio == 0)
                {
                    this.LogMessage( "No existe el folio cero", EventLogEntryType.FailureAudit);
                    return;
                }
                _enProceso = (enmProcesosMQ)_oInformacion.Proceso;
                //Se instancia la variable del sistema segun el numero de modulos en el sistema
                if (_oSistemaSapei == null)
                {
                    _oSistemaSapei = new SistemaSapei(Sapei.Framework.Configuracion.enmSistema.PROCESO);
                }
                _oProcesos = new VariableSesionProcesos(_oSistemaSapei,_oInformacion);
                Procesando();
            }
            catch (Exception ex)
            {
                this.LogMessage(ex.ToString(), EventLogEntryType.Error);
            }
        }

	}
}
