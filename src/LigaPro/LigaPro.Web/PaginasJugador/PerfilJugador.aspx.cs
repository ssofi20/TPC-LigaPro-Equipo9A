using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasJugador
{
    public partial class PerfilJugador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
                return;
            }
        }
    }
}