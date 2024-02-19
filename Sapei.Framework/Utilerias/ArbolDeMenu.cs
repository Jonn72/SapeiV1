using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sapei.Framework.Utilerias
{
     public class ArbolDeMenu
     {
          class Nodo
          {
               public string Clave { get; set; }

               public string Padre { get; set; }

               public int Nivel { get; set; }

               public string Descripcion { get; set; }

               public string Url { get; set; }
               public string Icono { get; set; }
               public List<Nodo> SubMenu { get; set; }

               public bool Permiso { get; set; }

               public Nodo(string psClave, string psPadre, int piNivel, string psDescripcion, string psUrl, bool pbPermiso)
               {
                    Clave = psClave.Trim();
                    Padre = psPadre.Trim();
                    Nivel = piNivel;
                    Descripcion = psDescripcion.Trim();
                    Url = psUrl;
                    SubMenu = new List<Nodo>();
                    Permiso = pbPermiso;
               }
               public Nodo(string psClave, string psPadre, int piNivel, string psDescripcion, string psUrl, bool pbPermiso, string psIcono)
               {
                    Clave = psClave.Trim();
                    Padre = psPadre.Trim();
                    Nivel = piNivel;
                    Descripcion = psDescripcion.Trim();
                    Url = psUrl;
                    Icono = psIcono;
                    SubMenu = new List<Nodo>();
                    Permiso = pbPermiso;
               }
               public bool Inserta(Nodo poNodo)
               {
                    if (Clave == poNodo.Padre)
                    {
                         SubMenu.Add(poNodo);
                         return true;
                    }
                    if (SubMenu.Count == 0)
                         return false;
                    foreach (Nodo psNodo in SubMenu)
                    {
                         if (psNodo.Inserta(poNodo))
                              return true;
                    }
                    return false;
               }
               public string ToStringHtml()
               {
                    StringBuilder loHtml = new StringBuilder();
                    StringBuilder loSubMenu = new StringBuilder();
                    if (!string.IsNullOrEmpty(Url))
                    {
                         if (!Permiso)
                              return "";
                         loHtml.Append("<li>\n");
                         loHtml.Append(" <a data-ajax=\"true\" ");
                         loHtml.Append(" data-ajax-method=\"GET\" data-ajax-mode=\"replace\" ");
                         loHtml.AppendFormat(" data-ajax-update=\"#BodyPrincipal\" href=\"{0}\">", Url.Trim());
                         loHtml.Append(Descripcion);
                         loHtml.Append("\n</a>");
                         loHtml.Append("</li>\n");
                         return loHtml.ToString();
                    }
                    foreach (Nodo psNodo in SubMenu)
                    {
                         loSubMenu.Append(psNodo.ToStringHtml() + "\n");
                    }
                    if (string.IsNullOrEmpty(loSubMenu.ToString().Trim()))
                         return "";
                    switch (Nivel)
                    {
                         case 1:

                              loHtml.Append("<li>\n");
                              loHtml.Append("     <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
                              loHtml.Append(Descripcion);
                              loHtml.Append("     <b class=\"caret\"></b></a>");
                              loHtml.Append("     <ul class=\"dropdown-menu\">");
                              loHtml.Append(loSubMenu.ToString());
                              loHtml.Append("     </ul>");
                              loHtml.Append("</li>\n");
                              break;
                         default:

                              loHtml.Append("<li class=\"dropdown-submenu\">\n");
                              loHtml.Append("     <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
                              loHtml.Append(Descripcion);
                              loHtml.Append("     </a>");
                              loHtml.Append("     <ul class=\"dropdown-menu\">");
                              loHtml.Append(loSubMenu.ToString());
                              loHtml.Append("     </ul>");
                              loHtml.Append("</li>\n");
                              break;
                    }
                    return loHtml.ToString();
               }
               public string ToStringHtmlVertical()
               {
                    StringBuilder loHtml = new StringBuilder();
                    StringBuilder loSubMenu = new StringBuilder();
                    if (!string.IsNullOrEmpty(Url))
                    {
                         if (!Permiso)
                              return "";
                         loHtml.Append("<li>\n");
                         loHtml.Append(" <a data-ajax=\"true\" ");

                         loHtml.Append(" data-ajax-method=\"GET\" data-ajax-mode=\"replace\" ");
                         loHtml.AppendFormat(" data-ajax-update=\"#BodyPrincipal\" href=\"{0}\">", Url.Trim());
                         if (Nivel == 1)
                              loHtml.AppendFormat(" <i class=\"fa {0}\"></i>", Icono);
                         loHtml.Append(Descripcion);
                         loHtml.Append("\n</a>");
                         loHtml.Append("</li>\n");
                         return loHtml.ToString();
                    }
                    foreach (Nodo psNodo in SubMenu)
                    {
                         loSubMenu.Append(psNodo.ToStringHtmlVertical() + "\n");
                    }
                    if (string.IsNullOrEmpty(loSubMenu.ToString().Trim()))
                         return "";
                    switch (Nivel)
                    {
                         case 1:

                              loHtml.Append("<li>\n");
                              loHtml.AppendFormat("  <a><i class=\"fa {0}\"></i>", Icono);
                              loHtml.Append(Descripcion);
                              loHtml.Append("     <span class=\"fa fa-chevron-down\"></span></a>");
                              loHtml.Append("     <ul class=\"nav child_menu\">");
                              loHtml.Append(loSubMenu.ToString());
                              loHtml.Append("     </ul>");
                              loHtml.Append("</li>\n");
                              break;
                         default:

                              loHtml.Append("<li>\n");
                              loHtml.Append("   <a>");
                              loHtml.Append(Descripcion);
                              loHtml.Append("     <span class=\"fa fa-chevron-down\"></span></a>");
                              loHtml.Append("   </a>");
                              loHtml.Append("   <ul class=\"nav child_menu\">");
                              loHtml.Append("     <li class=\"sub_menu\">\n");
                              loHtml.Append(loSubMenu.ToString());
                              loHtml.Append("     </li>\n");
                              loHtml.Append("   </ul>");
                              loHtml.Append("</li>\n");
                              break;
                    }
                    return loHtml.ToString();
               }

          }

          List<Nodo> _oRaiz;

          public ArbolDeMenu()
          {
               _oRaiz = new List<Nodo>();
          }

          public void Insertar(string psClave, string psPadre, int piNivel, string psDescripcion, string psUrl, bool pbPermiso)
          {
               if (psPadre.Trim() == "0" && piNivel == 1)
               {
                    _oRaiz.Add(new Nodo(psClave, psPadre, piNivel, psDescripcion, psUrl, pbPermiso));
                    return;
               }
               foreach (Nodo loNodo in _oRaiz)
               {
                    if (loNodo.Clave.Trim() == psPadre.Trim().ElementAt(0).ToString())
                    {
                         loNodo.Inserta(new Nodo(psClave, psPadre, piNivel, psDescripcion, psUrl, pbPermiso));
                         return;
                    }
               }
          }
          public void Insertar(string psClave, string psPadre, int piNivel, string psDescripcion, string psUrl, bool pbPermiso, string psIcono)
          {
               string lsPadre;
               if (psPadre.Trim() == "0" && piNivel == 1)
               {
                    _oRaiz.Add(new Nodo(psClave, psPadre, piNivel, psDescripcion, psUrl, pbPermiso, psIcono));
                    return;
               }
               foreach (Nodo loNodo in _oRaiz)
               {
                    lsPadre = psClave.Split('.')[0];
                    if (loNodo.Clave.Trim() == lsPadre)
                    {
                         loNodo.Inserta(new Nodo(psClave, psPadre, piNivel, psDescripcion, psUrl, pbPermiso, psIcono));
                         return;

                    }
               }
          }
          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public string ToStringHtml()
          {
               StringBuilder loMenu;
               loMenu = new StringBuilder();
               foreach (Nodo loNodo in _oRaiz)
               {
                    loMenu.Append(loNodo.ToStringHtml() + "\n");
               }
               return loMenu.ToString();
          }
          /// <summary>
          /// 
          /// </summary>
          /// <returns></returns>
          public string ToStringHtmlVertical()
          {
               StringBuilder loMenu;
               loMenu = new StringBuilder();
               foreach (Nodo loNodo in _oRaiz)
               {
                    loMenu.Append(loNodo.ToStringHtmlVertical() + "\n");
               }
               return loMenu.ToString();
          }
     }
}