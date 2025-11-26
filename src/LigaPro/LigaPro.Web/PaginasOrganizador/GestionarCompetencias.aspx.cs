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
    public partial class GestionarCompetencias : System.Web.UI.Page
    {
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
                       // dgvItems.DataSource = datos.listarCompeticion();
                        dgvItems.DataBind();

                        // Panel Modificacion
                        int id = int.Parse(dgvItems.SelectedDataKey.Value.ToString());
                       /*Competicion aux = datos.buscarPorId(id);

                        txtNombre.Text = aux.Nombre.ToString();
                        ddlOrganizador.Text = aux.IdOrganizador.ToString();
                        txtPv.Text = aux.IdReglamento.ToString();*/
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
            else
            {
                Response.Redirect("/Auth/InicioSesion.aspx", false);
            }            
        }

        protected void dgvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvItems.SelectedDataKey.Value.ToString();
            PanelSeleccionar.Visible = false;
            PanelModificar.Visible = true;
        }

        protected void rbConFases_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbSinFases_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}