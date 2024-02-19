using System.Web;
using System.Web.Optimization;

namespace appSapei
{
     public class BundleConfig
     {
          // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
          public static void RegisterBundles(BundleCollection bundles)
          {
               bundles.IgnoreList.Clear();

               bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

               bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*",
    "~/Scripts/jquery.validate.unobtrusive.min.js"));

               bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
				   "~/Scripts/angular.min.js",
					"~/Scripts/AngularController/config.js"));

			bundles.Add(new StyleBundle("~/Content/login").Include(
                    "~/Content/login/vendor/icon-sets.css",
                    "~/Content/login/main1.css",
                    "~/Content/login/main.css",
                    "~/Content/login/pace.css"
                    ));
               bundles.Add(new StyleBundle("~/Content/tablas").Include(
                    "~/Content/Controles/DataTable/dataTables.bootstrap.css",
                    "~/Content/Controles/DataTable/buttons.bootstrap.css",
                    "~/Content/Controles/DataTable/fixedHeader.bootstrap.css",
                    "~/Content/Controles/DataTable/responsive.bootstrap.css",
                    "~/Content/Controles/DataTable/scroller.bootstrap.css",
                    "~/Content/Controles/DataTable/select.dataTables.min.css"
                    ));

               bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                    "~/Content/bootstrap.css"));

               bundles.Add(new StyleBundle("~/Content/controlescss").Include(
                    "~/Content/Controles/dropDown.css",
                    "~/Content/Controles/datePiker4.css",
                    "~/Content/Controles/datetimepiker.css",
                    "~/Content/Controles/nprogress.css",
                    "~/Content/Controles/smart_wizard.css",
                    "~/Content/Controles/smart_wizard_theme_dots.css",
                    "~/Content/Controles/smart_wizard_theme_arrows.css",
                    "~/Content/Controles/fileinput.min.css",
                    "~/Content/Controles/fileinput-rtl.min.css",
                    "~/Content/Controles/flat_green.css",
                    "~/Content/Controles/toastr.min.css",
                    "~/Content/Controles/select/select2.min.css",
                    "~/Content/Controles/cursor.css"
                    ));
               bundles.Add(new StyleBundle("~/Content/captchacss").Include(
                    "~/Content/Controles/captcha.css",
                    "~/Content/Controles/jquery.motionCaptcha.0.2.css"
                    ));
               bundles.Add(new StyleBundle("~/Content/fontcss").Include(
                    "~/Content/login/vendor/icon-sets.css",
                    "~/Content/font/font-awesome.css"));

               bundles.Add(new ScriptBundle("~/bundles/login").Include(
                    "~/Scripts/md5.js",
                    "~/Scripts/validacionesControles.js",
                    "~/Scripts/login/login.js",
                    "~/Scripts/login/pace.js"));

               bundles.Add(new ScriptBundle("~/bundles/controles").Include(
                    "~/Scripts/Componentes/moment.js",
                    "~/Scripts/Componentes/select.js",
                    "~/Scripts/manejoAlertas.js",
                    "~/Scripts/Componentes/jquery.smartWizard.min.js",
                    "~/Scripts/Componentes/bootbox.min.js",
                    "~/Scripts/Componentes/toastr.min.js",
                    "~/Scripts/Componentes/iCheck.min.js",
					"~/Scripts/Componentes/diversos.js",
					"~/Scripts/Componentes/datePiker4.js",
                    "~/Scripts/Componentes/knockout-2.2.0.js",
                    "~/Scripts/Componentes/select/select2.min.js"
                    ));

               bundles.Add(new ScriptBundle("~/bundles/tablas").Include(
                    "~/Scripts/Componentes/DataTable/jquery.dataTables.js",
                    "~/Scripts/Componentes/DataTable/dataTables.buttons.js",
                    "~/Scripts/Componentes/DataTable/buttons.flash.js",
                    "~/Scripts/Componentes/DataTable/buttons.html5.js",
                    "~/Scripts/Componentes/DataTable/buttons.print.js",
                    "~/Scripts/Componentes/DataTable/dataTables.bootstrap.min.js",
                    "~/Scripts/Componentes/DataTable/buttons.bootstrap.min.js",
                    "~/Scripts/Componentes/DataTable/dataTables.fixedHeader.min.js",
                    "~/Scripts/Componentes/DataTable/dataTables.keyTable.min.js",
                    "~/Scripts/Componentes/DataTable/dataTables.responsive.min.js",
                    "~/Scripts/Componentes/DataTable/responsive.bootstrap.js",
                    "~/Scripts/Componentes/DataTable/dataTables.scroller.min.js",
                    "~/Scripts/Componentes/DataTable/dataTables.select.min.js",
                    "~/Scripts/Componentes/DataTable/jszip.js",
                    "~/Scripts/Componentes/DataTable/pdfmake.js",
                    "~/Scripts/Componentes/DataTable/vfs_fonts.js"
                    ));

               bundles.Add(new ScriptBundle("~/bundles/captcha").Include(
                    "~/Scripts/Captcha/captcha.js",
                    "~/Scripts/jquery.placeholder.1.1.1.min.js",
                    "~/Scripts/Captcha/jquery.motionCaptcha.0.2.js"));

               bundles.Add(new ScriptBundle("~/bundles/generales").Include(
                    "~/Scripts/nprogress.js"
                    ));

               bundles.Add(new ScriptBundle("~/bundles/herramientas").Include(
                    "~/Scripts/md5.min.js",
                    "~/Scripts/fastclick.js",
                    "~/Scripts/validacion.js",
                    "~/Scripts/jquery.smartWizard.js",
                    "~/Scripts/jquery.unobtrusive-ajax.min.js",
                    "~/Scripts/validacionesControles.js",

                    "~/Scripts/funcionesCargaCombos.js",
                    "~/Scripts/mensajesGenosis.js"
                    ));

          }
     }
}