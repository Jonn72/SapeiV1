namespace Sapei.Framework.BaseDatos
{
     /// <summary>
     /// Clase para almacenar la definicion de la columna
     /// </summary>
     public class DefinicionColumna
     {
          /// <summary>
          /// NOmbre de la columna
          /// </summary>
          public string Nombre { get; set; }

          /// <summary>
          /// Tipo de datos
          /// </summary>
          public string TipoDato { get; set; }

          /// <summary>
          /// Tipo nativo
          /// </summary>
          public string TipoNativo { get; set; }

          /// <summary>
          /// Longitud del campo
          /// </summary>
          public int Longitud { get; set; }

          /// <summary>
          /// Precision del campo
          /// </summary>
          public int Precision { get; set; }

          /// <summary>
          /// Orden del campo
          /// </summary>
          public int Orden { get; set; }

          /// <summary>
          /// Es llave primaria
          /// </summary>
          public int EsPk { get; set; }

          /// <summary>
          /// Permite camposo nullos
          /// </summary>
          public bool PermiteNulo { get; set; }

          /// <summary>
          /// Tipo de valor en C#
          /// </summary>
          public string TipoCSharp { get; set; }

          /// <summary>
          /// Valor por omision
          /// </summary>
          public string ValorPredeterminado { get; set; }

          /// <summary>
          /// Gets or sets the descripcion.
          /// </summary>
          /// <value>
          /// The descripcion.
          /// </value>
          public string Descripcion { get; set; }

          /// <summary>
          /// Gets or sets a value indicating whether [es identity].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [es identity]; otherwise, <c>false</c>.
          /// </value>
          public bool EsIdentity { get; set; }
          /// <summary>
          /// Gets or sets a value indicating whether this <see cref="DefinicionColumna"/> is ordenar.
          /// </summary>
          /// <value>
          ///   <c>true</c> if ordenar; otherwise, <c>false</c>.
          /// </value>
          public bool Ordenar { get; set; }
          /// <summary>
          /// Gets or sets a value indicating whether [es campo empresa].
          /// </summary>
          /// <value>
          ///   <c>true</c> if [es campo empresa]; otherwise, <c>false</c>.
          /// </value>
          public bool EsCampoEmpresa { get; set; }
     }
}
