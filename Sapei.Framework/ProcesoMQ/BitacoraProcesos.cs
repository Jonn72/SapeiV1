using System;
using System.Collections.Generic;
using System.Data;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Sapei.Framework.BaseDatos;
using Sapei.Framework.ProcesoMQ;
using Sapei.Framework.Utilerias;

namespace Sapei.Framework.Bitacora
{
    /// <summary>
    /// Clase para los processo batch
    /// </summary>
    public class BitacoraProcesos
    {
        #region Variable

        /// <summary>
        /// Variable del sistema
        /// </summary>
        private Sistema _oSistema;

        /// <summary>
        /// Nombre del servidor de cola de mensajes
        /// </summary>
        private string _sServidorColaMensajes;
        /// <summary>
        /// Nombre de la cola de mensajes
        /// </summary>
        private string _sNombreColaMensajes;
        /// <summary>
        /// Funcion para carga los parametros  o solo instanciar la clase
        /// </summary>
        private bool _bCargaParametros;
        /// <summary>
        /// Propiedad para los parametros de entrada
        /// </summary>
        public List<ParametroProceso> PametrosEntrada;
        /// <summary>
        /// Propiedad para los paramertos de salida
        /// </summary>
        private List<ParametroProceso> _oPametrosSalida;
        /// <summary>
        /// Parametros para los errores
        /// </summary>
        private List<ParametroProceso> _oErrores;
        /// <summary>
        /// Esta variable nos permite saber si el mensaje ya fue enviado o no
        /// </summary>
        private bool _bMensajeEnviado;

        #endregion

        #region Propiedades
        /// <summary>
        /// 
        /// </summary>
        public int Folio { get; set; }

        /// <summary>
        /// Propieda para establecer el estatus del proceso
        /// </summary>
        public byte Estatus { get; set; }

        /// <summary>
        /// Propiedad para obtener el folio
        /// </summary>
        public string Periodo { get;  set; }

        /// <summary>
        /// Propiedad para obtener la clave del usaurio
        /// </summary>
        public string Usuario { get; private set; }

        /// <summary>
        /// Propiedad para obtener el modulo 
        /// </summary>
        public int Modulo { get; private set; }

        /// <summary>
        /// Propiedad para obtener el movimiento
        /// </summary>
        public enmProcesosMQ TipoProceso { get; set; }

        /// <summary>
        /// Propiedad para obtener la descripcion de del moviemiento
        /// </summary>
        public string Descripcion { get; set; }



        #endregion

        #region Constructores

  
        /// <summary>
        /// Contructor con la variabel del sistema
        /// </summary>
        /// <param name="poSistema"></param>
        public BitacoraProcesos(Sistema poSistema)
        {
            _oSistema = poSistema;
        }

        /// <summary>
        /// Contructor para carhar la clase con la variable del sistema y el folio          
        /// </summary>
        /// <param name="poSistema"></param>
        /// <param name="piFolio"></param>
        public BitacoraProcesos(Sistema poSistema, int piFolio)
        {
            _oSistema = poSistema;
            _bCargaParametros = true;
            CargaValores(piFolio);
        }

        /// <summary>
        /// Cosntructo para cargar la clase con el folio pero sin los parametros de entrada
        /// Esto para hacer las descarga de los archivos
        /// </summary>
        /// <param name="poSistema"></param>
        /// <param name="piFolio"></param>
        /// <param name="pbCargaPametros"></param>
        public BitacoraProcesos(Sistema poSistema, enmProcesosMQ poTipo)
        {
            _oSistema = poSistema;
            CargaValores(poTipo);
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Carga valore segun el foio
        /// </summary>
        /// <param name="piFolio"></param>
        private void CargaValores(int piFolio)
        {
            CargaValores(0, piFolio);
        }

        /// <summary>
        /// Carga los valores desde la base de datos segun la llave
        /// </summary>
        /// <param name="piModulo">Modulo</param>
        /// <param name="piProceso">Proceso</param>
        /// <param name="psLLave">Llave asociada al modulo y proceso</param>
        /// <param name="piFolio">Folio del proceso</param>
        private void CargaValores(enmProcesosMQ piProceso, int piFolio = -1)
        {
            if (piFolio == -1)
            {
                //Si es un proceso nuevo iniciamos los valores  
                Periodo = _oSistema.Sesion.Periodo.PeriodoActual;
                Usuario = _oSistema.Sesion.Usuario.Usuario;
                TipoProceso = (enmProcesosMQ)piProceso;
                PametrosEntrada = new List<ParametroProceso>();
                _oPametrosSalida = new List<ParametroProceso>();
                _oErrores = new List<ParametroProceso>();
            }
            else
            {
                //Sino cargamos la informacion de la bd
                CargaConfiguracionBD(piFolio);
            }
            //Cambiamos el estatus a iniciado
            Estatus = (byte)enmBitacoraProcesosEstatus.INICIADO;
            _bMensajeEnviado = false;
            //Obtenemos el servidor de colas de mensaje
            _sServidorColaMensajes = ".";

            _sNombreColaMensajes = "SapeiMSQ";
            _sNombreColaMensajes = string.Format("{0}{1}{2}", _sServidorColaMensajes, "\\Private$\\", _sNombreColaMensajes);

        }

        /// <summary>
        /// Funcion que carga la informacion del a bd segun el folio
        /// </summary>
        /// <param name="piFolio"></param>
        private void CargaConfiguracionBD(int piFolio)
        {
            StringBuilder lsQuery;
            TablaGenerica loValores;
            loValores = null;
            try
            {
                Folio = piFolio;
                lsQuery = new StringBuilder();
                lsQuery.Append(" Select periodo, proceso,usuario, estado");
                lsQuery.AppendFormat(" from sis_procesosMQ");
                lsQuery.AppendFormat(" where folio = {0} ", piFolio);
                loValores = _oSistema.Conexion.RegresaTablaGenerica(lsQuery, "sis_procesosMQ");
                if (loValores.TotalFilas > 0)
                {
                    Periodo = Convert.ToString(loValores[0, "periodo"]);
                    TipoProceso = (enmProcesosMQ)Enum.Parse(typeof(enmProcesosMQ), Convert.ToString(loValores[0, "proceso"]));
                    Usuario = Convert.ToString(loValores[0, "usuario"]);
                    Estatus = Convert.ToByte(loValores[0, "estado"]);
                }
            }
            finally
            {
                Sapei.Framework.Utilerias.ManejoObjetos.LiberaTablaGenerica(loValores);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="poArchivoBatch"></param>
        public void Iniciar()
        {
            _oSistema.Conexion.EjecutaEscalar(ArmaConsultaParaIniciarBitacora());
            Folio = (int)_oSistema.Conexion.EjecutaEscalar(ArmaConsultaParaNumeroInterno(Periodo));
            _bMensajeEnviado = false;
            MandaMensaje();
        }
        /// <summary>
        /// Funcion que verifica si un proceso finalizo
        /// </summary>
        /// <param name="piFolio"></param>
        /// <returns></returns>
        public bool TerminoProceso(int piFolio)
        {
            StringBuilder lsQuery;
            lsQuery = new StringBuilder();
            lsQuery.Append(" Select fecha_fin");
            lsQuery.Append(" from sis_procesosMQ");
            lsQuery.AppendFormat(" where Folio = {0} ", piFolio);
            if (!String.IsNullOrEmpty(Convert.ToString(_oSistema.Conexion.EjecutaEscalar(lsQuery))))
            {
                return true;
            }
            return false;
        }

  
        /// <summary>
        /// Arma la consulta para el folio
        /// </summary>          
        /// <returns>Consulta para el folio</returns>
        private StringBuilder ArmaConsultaParaNumeroInterno(string psPeriodo)
        {
            StringBuilder lsQuery;
            lsQuery = new StringBuilder();
            lsQuery.Append(" Select isnull(max(folio),0) as Maximo");
            lsQuery.Append(" from sis_procesosMQ");
            lsQuery.AppendFormat(" where periodo = '{0}'", psPeriodo);
            return lsQuery;
        }

        /// <summary>
        /// Arama la consulta para iniciar la bitacora
        /// </summary>          
        /// <returns>Consutla para iniciar la bitacora</returns>
        private StringBuilder ArmaConsultaParaIniciarBitacora()
        {
            StringBuilder lsQuery;
            lsQuery = new StringBuilder();
            lsQuery.Append(" Insert Into sis_procesosMQ");
            lsQuery.Append(" (periodo, proceso, usuario, fecha_inicio, fecha_fin, procesando, estado)");
            lsQuery.Append("  Values(");
            lsQuery.AppendFormat(" '{0}'", Periodo);
            lsQuery.AppendFormat(" ,'{0}'", TipoProceso.ToString()); 
            lsQuery.AppendFormat(" ,'{0}'", Usuario);
            lsQuery.Append(" ,GETDATE()"); 
            lsQuery.Append(" ,NULL");
            lsQuery.Append(" ,0");
            lsQuery.AppendFormat(" ,{0}", (byte)Estatus);    
            lsQuery.AppendFormat(" )");
            return lsQuery;
        }

        /// <summary>
        /// Arma la consulta para actualizar los parametros de errores
        /// </summary>          
        /// <returns>Consulta para actualizar los parametros de errores</returns>
        private StringBuilder ArmaConsultaParaUpdateErrorBitacora()
        {
            StringBuilder lsQuery;
            lsQuery = new StringBuilder();
            lsQuery.Append(" Update sis_procesosMQ");
            lsQuery.AppendFormat(" set fecha_fin = {0}", DateTime.Now);
            lsQuery.AppendFormat(" , estado = {0}", (byte)Estatus);
            lsQuery.AppendFormat(" where folio = {0}", Folio);
            return lsQuery;
        }

       
        /// <summary>
        /// Arma la consulta para actualizar el estatus de la bitacora
        /// </summary>          
        /// <returns></returns>
        private StringBuilder ArmaConsultaParaUpdateEstatusBitacora()
        {
            StringBuilder lsQuery;
            lsQuery = new StringBuilder();
            lsQuery.Append(" Update sis_procesosMQ");
            lsQuery.AppendFormat(" set estado = {0}, procesando = 0", (byte)Estatus);
            if (Estatus == 3 || Estatus == 4 || Estatus == 5)
                lsQuery.AppendFormat("  ,fecha_fin = getDate()");
            lsQuery.AppendFormat(" where folio = {0}", Folio);
            return lsQuery;
        }

        /// <summary>
        /// funcion que crea la lista de parametro usando argumentos
        /// </summary>
        /// <param name="psNombre">nombre del parametro</param>
        /// <param name="psValor">valor del parametro</param>
        /// <returns></returns>
        public ParametroProceso CreaParametro(string psNombre, string psValor)
        {
            return new ParametroProceso(psNombre, psValor);
        }

        /// <summary>
        /// funcion que crea la lista de parametro usando argumentos
        /// </summary>
        /// <param name="poArg0">parametro 1</param>
        /// <returns>Lista de parametros</returns>
        public List<ParametroProceso> CreaListaParametros(ParametroProceso poArg0)
        {
            return CreaListaParametros(new ParametroProceso[] { poArg0 });
        }

        /// <summary>
        /// funcion que crea la lista de parametro usando argumentos
        /// </summary>
        /// <param name="poArg0">parametro 1</param>
        /// <param name="poArg1">parametro 2</param>
        /// <returns>Lista de parametros</returns>
        public List<ParametroProceso> CreaListaParametros(ParametroProceso poArg0, ParametroProceso poArg1)
        {
            return CreaListaParametros(new ParametroProceso[] { poArg0, poArg1 });
        }

        /// <summary>
        /// funcion que crea la lista de parametro usando argumentos
        /// </summary>          
        /// <param name="poArg0">parametro 1</param>
        /// <param name="poArg1">parametro 2</param>
        /// <param name="poArg2">parametro 3</param>
        /// <returns>Lista de parametros</returns>
        public List<ParametroProceso> CreaListaParametros(ParametroProceso poArg0, ParametroProceso poArg1, ParametroProceso poArg2)
        {
            return CreaListaParametros(new ParametroProceso[] { poArg0, poArg1, poArg2 });
        }

        /// <summary>
        /// funcion que crea la lista de parametro usando argumentos
        /// </summary>          
        /// <param name="poArgs">Argumento de los parametros</param>
        /// <returns>Lista de parametros</returns>
        public List<ParametroProceso> CreaListaParametros(params ParametroProceso[] poArgs)
        {
            List<ParametroProceso> loParametros;
            int liParametro;
            if (poArgs == null)
            {
                throw new SolExcepcion("No hay argumentos definidos de los parametros");
            }
            loParametros = new List<ParametroProceso>();

            for (liParametro = 0; liParametro < poArgs.Length; liParametro++)
            {
                loParametros.Add(poArgs[liParametro]);
            }
            return loParametros;
        }

        
        /// <summary>
        /// Actualizas el estatus unicamente por si el proceso tuviera un estatus intermedio que no representa la finalizacion del proceso
        /// </summary>
        /// <param name="penEstatus"></param>
        private void CambioEstatus(Sapei.Framework.ProcesoMQ.enmBitacoraProcesosEstatus penEstatus)
        {
            Estatus = (byte)penEstatus;
            _oSistema.Conexion.EjecutaEscalar(ArmaConsultaParaUpdateEstatusBitacora());
        }

        /// <summary>
        /// Actualiza el estatus, parametros de salida y/o errores si se llenaron, si no se dejan en nulo esos campos
        /// </summary>
        public void Terminar(Exception poError = null)
        {
            if (!Object.Equals(poError, null))
            {
                CambioEstatus(Sapei.Framework.ProcesoMQ.enmBitacoraProcesosEstatus.TERMINADOERROR);
                if (poError is SolExcepcion)
                {
                    AgregaParametrodeError("Error", poError.Message);
                }
                else
                {
                    AgregaParametrodeError("Error", poError.ToString());
                }
                _oSistema.Conexion.EjecutaEscalar(ArmaConsultaParaUpdateErrorBitacora());
                return;
            }
            CambioEstatus(Sapei.Framework.ProcesoMQ.enmBitacoraProcesosEstatus.TERMINADO);
        }

        
       
        /// <summary>
        /// Funcion que cambia el estatus a procesando
        /// </summary>
        public void Procesando()
        {
            CambioEstatus(Sapei.Framework.ProcesoMQ.enmBitacoraProcesosEstatus.ENPROCESO);
        }

        /// <summary>
        /// Manda el mensaje al Servidor de mensajes
        /// </summary>          
        private void  MandaMensaje()
        {
            InformacionMensaje loInformacionMensaje;
            System.Messaging.Message loMensaje;
            MessageQueue loColaMensajes = null;
            if (_bMensajeEnviado == false)
            {
                loColaMensajes = new MessageQueue(_sNombreColaMensajes);
                if (!MessageQueue.Exists(_sNombreColaMensajes))
                {
                    throw new SolExcepcion("No se ha creado la cola de mensajes. Ejecutar el instalador del servicio");
                }
                loInformacionMensaje = new InformacionMensaje
                {
                    Folio = Folio,
                    Modulo = Modulo,
                    Periodo = Periodo,
                    Proceso = TipoProceso,
                };
                loMensaje = new Message
                {
                    Body = loInformacionMensaje
                };
                //! Mandamos un mensaje con el tipo de proceso
                loColaMensajes.Send(loMensaje, Convert.ToString(TipoProceso));
                _bMensajeEnviado = true;
            }
        }

        /// <summary>
        /// Funcion que agrega parametros de errores
        /// </summary>
        /// <param name="psNombreParametro">Nombre del parametro</param>
        /// <param name="psValorParametro">Valor del parametro</param>
        public void AgregaParametrodeError(string psNombreParametro, string psValorParametro)
        {
            ParametroProceso loParametro = new ParametroProceso(psNombreParametro, psValorParametro);
            if (!_oErrores.Contains(loParametro))
            {
                _oErrores.Add(loParametro);
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        public void TerminaProcesos()
        {
            StringBuilder lsQuery;
            lsQuery = new StringBuilder();
            lsQuery.Append(" Update sis_procesosMQ");
            
            lsQuery.AppendFormat(" Set procesado = 1, estado = {0} ", (int)enmBitacoraProcesosEstatus.TERMINADOPORSERVICIOFINALIZADO);
            lsQuery.AppendFormat(" , fecha_fin = getdate()");
            lsQuery.AppendFormat(" where procesado = 0 or estado in (1,3)  ");
            //_oSistema.Conexion.EjecutaEscalar(lsQuery);
        }

   
        #endregion
    }
}