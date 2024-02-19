using System;
using System.Linq;
using Sapei.Framework.Datos;
using Sapei.Framework.Configuracion;
using System.Text;
using System.Collections.Generic;
using Sapei.Framework.Utilerias;
using System.Data;

namespace Sapei
{
     [Serializable]
     public partial class SisUsuario
     {
          #region variables
          private SistemaSapei _oSistema;
          private enmTipoUsuario _enmTipoUsuario;
          private string _sUsuario;
          private string _sContraseña;
          private string _sNombre;
		private string _sCorreo;
		private enmRolUsuario _sRolUsuario;
          private List<string> _oPermisos;
          private List<string> _oPermisosFuncion;
          private List<string> _oPermisosCarreras;
          private string _sIndex;
          private bool _bEOF;
          #endregion

          #region Propiedades
          public string Correo
          {
               get
               {
                    return this._sCorreo;
               }
          }

          public enmRolUsuario Rol
          {
               get
               {
                    return this._sRolUsuario;
               }
               set
               {
                    this._sRolUsuario = value;
               }
          }

          public string Nombre
          {
               get
               {
                    return this._sNombre;
               }
          }
          public string Contraseña
          {
               get
               {
                    return this._sContraseña;
               }
          }
          public string Usuario
          {
               get
               {
                    return this._sUsuario;
               }
          }
          public enmTipoUsuario TipoUsuario
          {
               get 
               { 
                    return _enmTipoUsuario; 
               }
          }
          public List<string> Permisos
          {
               get
               {
                    return _oPermisos;
               }
          }
          public List<string> PermisosFuncion
          {
               get
               {
                    return _oPermisosFuncion;
               }
          }
          public List<string> PermisosCarreras
          {
               get
               {
                    return _oPermisosCarreras;
               }
          }

          public string Index
          {
               get
               {
                    return this._sIndex;
               }
          }
          public bool EOF
          {
               get
               {
                    return this._bEOF;
               }
          }
          #endregion
          #region Contructor

          /// <summary>
          /// Inicia una nueva instancia de la clase Cia.
          /// </summary>
          /// <param name="poSistemaSII">Clase del Sistema</param>
          public SisUsuario(SistemaSapei poSistemaSII)
          {
               _oSistema = poSistemaSII; 
          }

          #endregion
   
          #region Funciones

          /// <summary>
          /// Carga un registro especifico segun el tipo de usuario (PERONAL, DOCENTE, ESTUDIANTE o ASPIRANTE)		
          /// </summary>
          /// <param name="piInstitucion">Institucion</param>
          /// <param name="piClave">Clave</param>
          public void Cargar(string psCuentaUsuario,  string psContraseña,  enmTipoUsuario  penmTipoUsuario)
          {
               _enmTipoUsuario = penmTipoUsuario;
               _sUsuario = psCuentaUsuario;
               _sContraseña = psContraseña;
               CargaUsuarioPorTipo();
          }

          private void CargaUsuarioPorTipo()
          {
               switch (_enmTipoUsuario)
               {
                    case enmTipoUsuario.ADMIN:
                    case enmTipoUsuario.PERSONAL:
                    case enmTipoUsuario.DOCENTE:
                         CargaUsuarioPersonal();
                         break;
                         //
                    case enmTipoUsuario.ESTUDIANTE:
                         CargaUsuarioEstudiante();
                         break;
                    case enmTipoUsuario.ASPIRANTE:
                         CargaUsuarioAspirante();
                         //
                         break;
                    case enmTipoUsuario.INSTRUCTOR:
                         CargaUsuarioInstructor();
                         break;
               }
          }
          /// <summary>
          /// Carga los usuarios que laboran en la institucion
          /// </summary>
          private void CargaUsuarioPersonal()
          {
               Acceso loPersonal;
               loPersonal = new Acceso(_oSistema);

               loPersonal.Cargar(_sUsuario);
               if (loPersonal.EOF)
               {
                    return;
               }
               _sNombre = loPersonal.nombre_usuario;
               _sRolUsuario = loPersonal.tipo_usuario.ToEnum<enmRolUsuario>();
               _sIndex = "Personal";
               if (_sRolUsuario == enmRolUsuario.DOC || _sRolUsuario == enmRolUsuario.DAC || _sRolUsuario == enmRolUsuario.PER || _sRolUsuario == enmRolUsuario.FCL)
                    _oPermisosFuncion = loPersonal.RegresaPermisosFuncionRol(_sRolUsuario);
               else
                    _oPermisosFuncion = loPersonal.RegresaPermisosFuncionUsuario(_sUsuario);
               if(_sRolUsuario == enmRolUsuario.DAC)
                    _oPermisosCarreras = loPersonal.RegresaPermisosCarrerasUsuario(_sUsuario);
          }
          /// <summary>
          /// Carga los usuarios que laboran en la institucion
          /// </summary>
          private void CargaUsuarioEstudiante()
          {
               Alumno loAlumno;
               loAlumno = new Alumno(_oSistema);

               loAlumno.Cargar(_sUsuario);
               if (loAlumno.EOF)
               {
                    //esto se cumple cuando no existe el usuario                  
                    return;
               }
               _bEOF = false;
               _sNombre = loAlumno.nombre_alumno + ' ' + loAlumno.apellido_paterno + ' ' + loAlumno.apellido_materno;
               _sRolUsuario = enmRolUsuario.ALU;
               _sIndex = "Estudiante";
               _oPermisosFuncion = loAlumno.RegresaPermisosFuncionRol(enmRolUsuario.ALU);
          }
          private void CargaUsuarioAspirante()
          {
               Aspirante_datos loAspirante;
               _bEOF = true;
               loAspirante = new Aspirante_datos(_oSistema);

               loAspirante.Cargar(_sUsuario);
               if (loAspirante.EOF)
               {
                    //esto se cumple cuando no existe el usuario                  
                    return;
               }
               _sNombre = loAspirante.nombre;
               _sRolUsuario = enmRolUsuario.ASP;
               _sIndex = "Aspirante";
          }
          private void CargaUsuarioInstructor()
          {
               Extra_entrenador loPersonal;
               DataTable loDatos;
               loPersonal = new Extra_entrenador(_oSistema);
               loDatos = loPersonal.RegresaDatosEntrenador(_sUsuario);
               _sUsuario = loDatos.RegresaValorFila<int>("id").ToString();
               _sNombre = loDatos.RegresaValorFila<string>("nombre") + " " + loDatos.RegresaValorFila<string>("paterno") + " " + loDatos.RegresaValorFila<string>("materno");
               _sRolUsuario = enmRolUsuario.INS;
               _sIndex = "Personal";
               _oPermisosFuncion = loPersonal.RegresaPermisosFuncionRol(enmRolUsuario.INS);
          }
          #endregion
     }
}
