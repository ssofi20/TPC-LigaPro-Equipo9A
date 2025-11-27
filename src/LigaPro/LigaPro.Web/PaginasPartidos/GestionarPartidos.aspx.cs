using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasPartidos
{
    public partial class GestionarPartidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string panel = Request.QueryString["panel"];

                PanelCargarResultados.Visible = false;
                PanelVerEquipos.Visible = false;
                PanelGenerarFixture.Visible = false;

                switch (panel)
                {
                    case "VerEquipos":
                        PanelVerEquipos.Visible = true;
                        break;

                    case "Resultados":
                        PanelCargarResultados.Visible = true;
                        break;

                    case "Fixture":
                        PanelGenerarFixture.Visible = true;
                        break;

                    default:
                        PanelVerEquipos.Visible = false;
                        PanelGenerarFixture.Visible = false;
                        PanelCargarResultados.Visible = true; // panel por defecto si querés
                        break;
                }

                // Recuperás el ID si lo necesitás:
                string id = Request.QueryString["id"];
                if (id != null)
                {
                    // cargar datos según el id
                }
            }
        }
    }
}