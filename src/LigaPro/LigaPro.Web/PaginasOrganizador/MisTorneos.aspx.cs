using LigaPro.Datos;
using LigaPro.Domain;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class MisTorneos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Seguridad básica
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarTorneos();
                CargarComboEstados();
            }
        }

        private void CargarTorneos()
        {
            Usuario usuario = (Usuario)Session["UsuarioLogueado"];
            try
            {
                TorneoDatos datos = new TorneoDatos();
                OrganizadorDatos orgDatos = new OrganizadorDatos();
                Organizador org = orgDatos.ObtenerInfoAdmin(usuario.Id);
                List<Torneo> lista = datos.listarCompeticion(org.Id);

                var listaActiva = lista.Where(x => x.Activo).ToList();

                if (listaActiva.Count > 0)
                {
                    rptTorneos.DataSource = listaActiva;
                    rptTorneos.DataBind();
                    pnlVacio.Visible = false;
                }
                else
                {
                    rptTorneos.DataSource = null;
                    rptTorneos.DataBind();
                    pnlVacio.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Manejar error
            }
        }

        private void CargarComboEstados()
        {
            ddlEstadoEditar.DataSource = Enum.GetValues(typeof(EstadoCompetencia));
            ddlEstadoEditar.DataBind();
        }

        protected void rptTorneos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idTorneo = int.Parse(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    CargarDatosParaEditar(idTorneo);
                    // Abrir modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopEdit", "abrirModalEditar();", true);
                    break;

                case "ConfirmarEliminar":
                    // Preparamos el modal de eliminación
                    hfIdTorneoEliminar.Value = idTorneo.ToString();
                    // Abrir modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDel", "abrirModalEliminar();", true);
                    break;

                case "Partidos":
                    // Redirigir a la gestión de partidos/equipos
                    Response.Redirect($"/PaginasPartidos/GestionarPartidos.aspx?id={idTorneo}");
                    break;
            }
        }

        // --- LÓGICA DE EDICIÓN ---
        private void CargarDatosParaEditar(int id)
        {
            TorneoDatos datos = new TorneoDatos();
            Torneo comp = datos.buscarPorId(id);

            hfIdTorneoEditar.Value = comp.Id.ToString();
            txtNombreEditar.Text = comp.Nombre;
            ddlEstadoEditar.SelectedValue = comp.Estado.ToString(); // Asegúrate que coincida con el Enum

            // Reglas
            txtPvEditar.Text = comp.Reglas.PuntosPorVictoria.ToString();
            txtPeEditar.Text = comp.Reglas.PuntosPorEmpate.ToString();
            txtTasEditar.Text = comp.Reglas.TarjetasAmarillasParaSuspension.ToString();
            txtPsrdEditar.Text = comp.Reglas.PartidosSuspensionPorRojaDirecta.ToString();

            chkFasesEditar.Checked = comp.TieneFaseDeGrupos;
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hfIdTorneoEditar.Value);
                TorneoDatos datos = new TorneoDatos();
                Torneo comp = datos.buscarPorId(id); // Traemos el original

                // Actualizamos valores
                comp.Nombre = txtNombreEditar.Text;
                comp.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), ddlEstadoEditar.SelectedValue);
                comp.Reglas.PuntosPorVictoria = int.Parse(txtPvEditar.Text);
                comp.Reglas.PuntosPorEmpate = int.Parse(txtPeEditar.Text);
                comp.Reglas.TarjetasAmarillasParaSuspension = int.Parse(txtTasEditar.Text);
                comp.Reglas.PartidosSuspensionPorRojaDirecta = int.Parse(txtPsrdEditar.Text);
                comp.TieneFaseDeGrupos = chkFasesEditar.Checked;

                datos.modificarTorneo(comp);

                // Recargar lista
                CargarTorneos();
            }
            catch (Exception ex)
            {
                // Manejar error visualmente
            }
        }

        // --- LÓGICA DE ELIMINACIÓN ---
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idTorneo = int.Parse(hfIdTorneoEliminar.Value);
                TorneoDatos datos = new TorneoDatos();
                datos.DesactivarTorneo(idTorneo);

                CargarTorneos();
            }
            catch (Exception ex) { }
        }
    }
}