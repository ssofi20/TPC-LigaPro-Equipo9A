using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Domain.Actores.LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasJugador
{
    public partial class TorneosActivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarTorneos();
            }
        }
        private void CargarTorneos()
        {
            TorneoDatos datos = new TorneoDatos();
            try
            {
                List<Torneo> lista = datos.ListarTorneosParaInscripcion();

                if (lista.Count > 0)
                {
                    rptTorneos.DataSource = lista;
                    rptTorneos.DataBind();
                    pnlVacio.Visible = false;
                }
                else
                {
                    pnlVacio.Visible = true;
                }
            }
            catch (Exception ex) { /* Manejar error */ }
        }
        protected void rptTorneos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioLogueado"];
            int idTorneo = int.Parse(e.CommandArgument.ToString());
            TorneoDatos datos = new TorneoDatos();

            if (e.CommandName == "Inscribir")
            {
                // 1. Guardar ID Torneo
                hfIdTorneoInscribir.Value = idTorneo.ToString();

                // 2. Cargar mis equipos que NO estan en ese torneo
                // false = traer equipos NO inscriptos
                List<Equipo> misEquipos = datos.ListarEquiposUsuarioEnTorneo(usuario.Id, idTorneo, false);

                if (misEquipos.Count > 0)
                {
                    ddlMisEquiposInscribir.DataSource = misEquipos;
                    ddlMisEquiposInscribir.DataTextField = "Nombre";
                    ddlMisEquiposInscribir.DataValueField = "Id";
                    ddlMisEquiposInscribir.DataBind();

                    lblErrorInscripcion.Visible = false;
                    btnConfirmarInscripcion.Enabled = true;
                }
                else
                {
                    ddlMisEquiposInscribir.Items.Clear();
                    ddlMisEquiposInscribir.Items.Add(new ListItem("Todos tus equipos ya están inscriptos o no tienes equipos.", "0"));
                    btnConfirmarInscripcion.Enabled = false;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopInsc", "abrirModalInscribir();", true);
            }
            else if (e.CommandName == "Baja")
            {
                // 1. Guardar ID Torneo
                hfIdTorneoBaja.Value = idTorneo.ToString();

                // 2. Cargar mis equipos que SI estan en ese torneo
                // true = traer equipos SI inscriptos
                List<Equipo> misEquiposEnTorneo = datos.ListarEquiposUsuarioEnTorneo(usuario.Id, idTorneo, true);

                if (misEquiposEnTorneo.Count > 0)
                {
                    ddlMisEquiposBaja.DataSource = misEquiposEnTorneo;
                    ddlMisEquiposBaja.DataTextField = "Nombre";
                    ddlMisEquiposBaja.DataValueField = "Id";
                    ddlMisEquiposBaja.DataBind();

                    lblErrorBaja.Visible = false;
                    btnConfirmarBaja.Enabled = true;
                }
                else
                {
                    ddlMisEquiposBaja.Items.Clear();
                    ddlMisEquiposBaja.Items.Add(new ListItem("No tienes equipos en este torneo.", "0"));
                    btnConfirmarBaja.Enabled = false;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopBaja", "abrirModalBaja();", true);
            }
        }

        protected void btnConfirmarInscripcion_Click(object sender, EventArgs e)
        {
            try
            {
                int idTorneo = int.Parse(hfIdTorneoInscribir.Value);
                int idEquipo = int.Parse(ddlMisEquiposInscribir.SelectedValue);

                if (idEquipo == 0) return;

                TorneoDatos datos = new TorneoDatos();
                datos.InscribirEquipo(idTorneo, idEquipo);

                CargarTorneos();

                string mensaje = "Tu equipo ha sido inscripto correctamente en el torneo.";
                string script = $"mostrarExito('{mensaje}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopExitoInscripcion", script, true);
            }
            catch (Exception ex)
            {
                // Manejo de error
            }
        }

        protected void btnConfirmarBaja_Click(object sender, EventArgs e)
        {
            try
            {
                int idTorneo = int.Parse(hfIdTorneoBaja.Value);
                int idEquipo = int.Parse(ddlMisEquiposBaja.SelectedValue);

                if (idEquipo == 0) return;

                TorneoDatos datos = new TorneoDatos();
                datos.BajaEquipo(idTorneo, idEquipo);

                CargarTorneos();

                string mensaje = "El equipo ha sido retirado del torneo exitosamente.";
                string script = $"mostrarExito('{mensaje}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopExitoBaja", script, true);
            }
            catch (Exception ex) { /* Manejar */ }
        }
    }
}