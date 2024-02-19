using System;
using Sapei;
using Sapei.Framework;
using Sapei.Framework.Bitacora;
using Sapei.Framework.ProcesoMQ;

namespace Sapei
{
    /// <summary>
    /// 
    /// </summary>
    public struct VariableSesionProcesos
    {
        /// <summary>
        /// The _o sistema
        /// </summary>
        public Sistema _oSistema;
        /// <summary>
        /// The _o informacion
        /// </summary>
        public InformacionMensaje _oInformacion;
        /// <summary>
        /// The _o proceso
        /// </summary>
        public BitacoraProcesos _oProceso;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableSesionProcesos"/> struct.
        /// </summary>
        /// <param name="poSistema">The po sistema.</param>
        /// <param name="poInformacion">The po informacion.</param>
        /// <param name="pbIniciaSesion">if set to <c>true</c> [pb inicia sesion].</param>
        public VariableSesionProcesos(Sistema poSistema, InformacionMensaje poInformacion)
        {
            this._oSistema = poSistema;
            this._oInformacion = poInformacion;
            this._oProceso = new BitacoraProcesos(poSistema, poInformacion.Folio);
            this._oProceso.Procesando();
        }
    }
}
