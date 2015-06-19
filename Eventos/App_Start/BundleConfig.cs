using System.Web;
using System.Web.Optimization;

namespace Eventos
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
          "~/Scripts/materialize.js"));

            bundles.Add(new ScriptBundle("~/bundles/JSEvento").Include(
                      "~/Scripts/JSEvent.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/lugares").Include(
                       "~/Scripts/knockout-3.3.0.js",
                      "~/Scripts/_lugar.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/eventos").Include(
                      "~/Scripts/knockout-3.3.0.js",
                     "~/Scripts/_evento.js"
                     ));


            bundles.Add(new ScriptBundle("~/bundles/tipos").Include(
                  "~/Scripts/knockout-3.3.0.js",
                 "~/Scripts/_tipo.js"
                 ));

            bundles.Add(new ScriptBundle("~/bundles/facturas").Include(
                  "~/Scripts/knockout-3.3.0.js",
                 "~/Scripts/_factura.js"
                 ));

            bundles.Add(new ScriptBundle("~/bundles/craft").Include(
                 "~/Scripts/knockout-3.3.0.js", 
                "~/Scripts/_craft.js"
               
                ));


            bundles.Add(new ScriptBundle("~/bundles/ventas").Include(
                "~/Scripts/knockout-3.3.0.js",
               "~/Scripts/Entrada_Venta.js"

               ));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/Scripts/knockout-3.3.0.js",
               "~/Scripts/_usuario.js"

               ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/materialize.css",
                      "~/Content/StyleEvent.css"));
        }
    }
}
