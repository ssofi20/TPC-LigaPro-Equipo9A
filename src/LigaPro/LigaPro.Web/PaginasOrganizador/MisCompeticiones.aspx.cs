using LigaPro.Datos;
using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class MisCompeticiones : System.Web.UI.Page
    {
        public string SepararEspacios(string texto)
        {
            return System.Text.RegularExpressions.Regex.Replace(texto, "([a-z])([A-Z])", "$1 $2");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                    if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Organizador)
                    {
                        CompeticionDatos datos = new CompeticionDatos();
                        List<Competicion> lista = datos.listarCompeticion();
                        dgvItems.DataSource = lista.Where(x => x.Activo == true).ToList();
                        dgvItems.DataBind();

                    }
                    else
                    {
                        Response.Redirect("/Auth/InicioSesion.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void dgvItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}