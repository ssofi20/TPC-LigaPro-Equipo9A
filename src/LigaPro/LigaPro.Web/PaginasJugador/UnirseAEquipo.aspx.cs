using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasJugador
{
    public partial class UnirseAEquipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Seguridad: Si no está logueado, afuera
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
            }

            Usuario usuario = (Usuario)Session["UsuarioLogueado"];

            if (!IsPostBack)
            {
                EquipoNegocio negocio = new EquipoNegocio();
                List<Equipo> equipos = negocio.ListarTodosLosEquipos(usuario.Id);

                if (equipos.Count > 0)
                {
                    rptEquiposEncontrados.DataSource = equipos;
                    rptEquiposEncontrados.DataBind();
                    pnlSinResultados.Visible = false;
                }
                else
                {
                    rptEquiposEncontrados.DataSource = null;
                    rptEquiposEncontrados.DataBind();
                    pnlSinResultados.Visible = true;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBusqueda.Text.Trim();

            Usuario usuario = (Usuario)Session["UsuarioLogueado"];
            EquipoNegocio negocio = new EquipoNegocio();

            // Si está vacío, no buscamos nada o limpiamos
            if (string.IsNullOrEmpty(textoBusqueda))
            {
                List<Equipo> resultados = negocio.ListarTodosLosEquipos(usuario.Id);

                if (resultados.Count > 0)
                {
                    rptEquiposEncontrados.DataSource = resultados;
                    rptEquiposEncontrados.DataBind();
                    pnlSinResultados.Visible = false;
                }
                else
                {
                    rptEquiposEncontrados.DataSource = null;
                    rptEquiposEncontrados.DataBind();
                    pnlSinResultados.Visible = true;
                }
            }


            try
            {
                List<Equipo> resultados = negocio.BuscarEquipos(textoBusqueda, usuario.Id);

                if (resultados.Count > 0)
                {
                    rptEquiposEncontrados.DataSource = resultados;
                    rptEquiposEncontrados.DataBind();
                    pnlSinResultados.Visible = false;
                }
                else
                {
                    rptEquiposEncontrados.DataSource = null;
                    rptEquiposEncontrados.DataBind();
                    pnlSinResultados.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Manejar error
            }
        }


        protected void rptEquiposEncontrados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Unirse")
            {
                try
                {
                    // 1. Obtener IDs
                    int idEquipo = int.Parse(e.CommandArgument.ToString());
                    Usuario usuario = (Usuario)Session["UsuarioLogueado"];

                    // 2. Llamar al negocio para crear solicitud
                    EquipoNegocio negocio = new EquipoNegocio();
                    negocio.CrearSolicitud(usuario.Id, idEquipo);

                    // 3. ACTUALIZACIÓN VISUAL DEL BOTÓN
                    Button btn = (Button)e.CommandSource;
                    btn.Text = "Solicitud Enviada";
                    btn.Enabled = false;
                    btn.CssClass = "btn btn-secondary w-100 mt-2 rounded-pill";

                    // 4. MOSTRAR EL MODAL LINDO
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopExito", "mostrarModalExito();", true);
                }
                catch (Exception ex)
                {
                    // En caso de error, aquí sí podrías dejar un alert o usar otro modal de error
                    string script = $"alert('Error al enviar solicitud: {ex.Message}');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertaError", script, true);
                }
            }

        }
    }
}