using Sapei.Framework.Configuracion;

namespace appSapei.Models
{
     public class UsuarioInicioSesion
     {
          public string Nombre { get; set; }

          public string Contrasenia { get; set; }

          public bool Recordar { get; set; }

          public string Clase { get; set; }

          public string Mensaje { get; set; }

          public string Index { get; set; }

          public bool Success { get; set; }

          public enmTipoUsuario TipoUsuario{ get; set; }
          public string IP { get; set; }
     }
}