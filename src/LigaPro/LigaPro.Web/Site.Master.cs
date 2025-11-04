using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelNavegacionPublica.Visible = false;
            PanelNavegacionJugador.Visible = false;
            PanelNavegacionOrganizador.Visible = false;

            if (Session["usuario"] == null)
            {
                PanelNavegacionPublica.Visible = true;
            }
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];

                if (usuario.Rol == Domain.RolUsuario.Jugador)
                {
                    PanelNavegacionJugador.Visible = true;
                }
                else if (usuario.Rol == Domain.RolUsuario.Organizador)
                {
                    PanelNavegacionOrganizador.Visible = true;
                }
                else
                {
                    PanelNavegacionPublica.Visible = true;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }
}