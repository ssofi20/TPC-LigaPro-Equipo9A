using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class GestionarCompetencias : System.Web.UI.Page
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

                        if (ddlOrganizador.Items.Count == 0)
                        {
                            OrganizadorDatos datosOrg = new OrganizadorDatos();
                            List<Organizador> listaOrg = datosOrg.listar();

                            ddlOrganizador.DataSource = listaOrg;
                            ddlOrganizador.DataValueField = "Id";
                            ddlOrganizador.DataTextField = "NombrePublico";
                            ddlOrganizador.DataBind();
                        }

                        PanelSeleccionar.Visible = true;
                        PanelModificar.Visible = false;                        
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
            int id = Convert.ToInt32(dgvItems.SelectedDataKey.Value);
            PanelModificar.Visible = true;
            PanelSeleccionar.Visible = false;


            if (dgvItems.SelectedIndex >= 0)
            {
                CompeticionDatos datos = new CompeticionDatos();
                Competicion aux = datos.buscarPorId(id);

                txtNombre.Text = aux.Nombre;
                ddlOrganizador.SelectedValue = aux.OrganizadorCompetencia.Id.ToString();
                txtPv.Text = aux.Reglas.PuntosPorVictoria.ToString();
                txtPe.Text = aux.Reglas.PuntosPorEmpate.ToString();
                txtTas.Text = aux.Reglas.TarjetasAmarillasParaSuspension.ToString();
                txtPsrd.Text = aux.Reglas.PartidosSuspensionPorRojaDirecta.ToString();

                rbConFases.Checked = aux.Fases;
                rbSinFases.Checked = !aux.Fases;

                panelOpcionesFases.Visible = aux.Fases;

                if (aux.Formato == "Ida") rbIda.Checked = true;
                else if (aux.Formato == "IdaYVuelta") rbIdaVuelta.Checked = true;
            }
        }

        protected void rbConFases_CheckedChanged(object sender, EventArgs e)
        {
            panelOpcionesFases.Visible = rbConFases.Checked;

        }

        protected void rbSinFases_CheckedChanged(object sender, EventArgs e)
        {
            panelOpcionesFases.Visible = false;

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedDataKey == null)
                return;

            int id = Convert.ToInt32(dgvItems.SelectedDataKey.Value);


            CompeticionDatos datos = new CompeticionDatos();
            Competicion nuevo = datos.buscarPorId(id);

            nuevo.Nombre = txtNombre.Text;
            nuevo.OrganizadorCompetencia.Id = int.Parse(ddlOrganizador.SelectedValue);
            nuevo.Reglas.PuntosPorVictoria = int.Parse(txtPv.Text);
            nuevo.Reglas.PuntosPorEmpate = int.Parse(txtPe.Text);
            nuevo.Reglas.TarjetasAmarillasParaSuspension = int.Parse(txtTas.Text);
            nuevo.Reglas.PartidosSuspensionPorRojaDirecta = int.Parse(txtPsrd.Text);

            if (rbConFases.Checked)
            {
                nuevo.Fases = true;
                nuevo.Formato = rbIdaVuelta.Checked ? "IdaYVuelta" : "Ida";
            }
            else
            {
                nuevo.Fases = false;
                nuevo.Formato = null;
            }
            datos.modificarCompetencia(nuevo);
            Response.Redirect("GestionarCompetencias.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarCompetencias.aspx", false);
        }

        protected void btnCancelarBaja_Click(object sender, EventArgs e)
        {
            PanelEliminar.Visible = false;
            PanelSeleccionar.Visible = true;

        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                if (usuario != null && usuario.Id != 0)
                {
                    Seguridad seguridad = new Seguridad();
                    CompeticionDatos datos = new CompeticionDatos();

                    if (seguridad.VerifyPassword(txtPass.Text, usuario.PasswordHash))
                    {
                        usuario.PasswordHash = seguridad.HashPassword(txtPass.Text);

                        int id = Convert.ToInt32(ViewState["id"]);

                        Competicion nuevo = datos.buscarPorId(id);
                        datos.DesactivarCompeticion(nuevo);

                        PanelEliminar.Visible = false;
                        PanelSeleccionar.Visible = true;

                    }
                    else
                    {
                        // mostrar msj contraseña antigua no coincide
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void dgvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                ViewState["id"] = id;

                PanelSeleccionar.Visible = false;
                PanelModificar.Visible = false;
                PanelEliminar.Visible = true;
            }
        }
    }
}