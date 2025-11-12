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

            if (Session["UsuarioLogueado"] == null)
            {
                PanelNavegacionPublica.Visible = true;
                PanelNavegacionJugador.Visible = false;
                PanelNavegacionOrganizador.Visible = false;
            }
            else
            {
                Usuario usuario = (Usuario)Session["UsuarioLogueado"];

                PanelNavegacionPublica.Visible = false;

                if (usuario.Rol == Domain.RolUsuario.Jugador)
                {
                    PanelNavegacionJugador.Visible = true;
                    PanelNavegacionOrganizador.Visible = false;
                }
                else if (usuario.Rol == Domain.RolUsuario.Organizador)
                {
                    PanelNavegacionJugador.Visible = false;
                    PanelNavegacionOrganizador.Visible = true;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}