
namespace Sapei.Framework.Utilerias
{
     /// <summary>
     /// Clase para manejo de archivo binarios con nombres
     /// </summary>
     public class Archivo
     {
          /// <summary>
          /// NOmbre del archivoi
          /// </summary>
          public string Nombre { get; set; }

          /// <summary>
          /// Archivo en binario
          /// </summary>
          public byte[] Buffer { get; set; }

          /// <summary>
          /// Crear una nueva instancia para la clase Archivo
          /// </summary>
          public Archivo()
          {
               Nombre = "";
               Buffer = null;
          }

          /// <summary>
          /// Crear una nueva instancia para la clase Archivo
          /// </summary>
          /// <param name="psNombre">NOmbre del archivo</param>
          /// <param name="pabyArchivo">Archivo binario</param>
          public Archivo(string psNombre, byte[] pabyArchivo)
          {
               Nombre = psNombre;
               Buffer = pabyArchivo;
          }
     }
}
