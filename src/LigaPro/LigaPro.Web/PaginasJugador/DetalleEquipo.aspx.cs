using LigaPro.Domain.Actores;
using LigaPro.Domain.Relaciones;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasJugador
{
    public partial class DetalleEquipo : System.Web.UI.Page
    {
        public int IdEquipoSeleccionado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
                return;
            }

            string idStr = Request.QueryString["id"];
            if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int idTemp))
            {
                Response.Redirect("MisEquipos.aspx");
                return;
            }
            IdEquipoSeleccionado = idTemp;

            if (!IsPostBack)
            {
                CargarDatosEquipo();
                CargarJugadores();
                CargarSolicitudes();
            }
        }

        // -- MÉTODOS DE CARGA DE DATOS -- //

        // CARGAR DATOS DEL EQUIPO
        private void CargarDatosEquipo()
        {
            EquipoNegocio negocio = new EquipoNegocio();
            try
            {
                Equipo equipo = negocio.ObtenerEquipoPorId(IdEquipoSeleccionado);
                Usuario usuario = (Usuario)Session["UsuarioLogueado"];

                if (equipo == null || equipo.IdUsuarioCreador != usuario.Id)
                {
                    Response.Redirect("MisEquipos.aspx");
                    return;
                }

                lblNombreEquipo.Text = equipo.Nombre;
                imgEscudo.ImageUrl = !string.IsNullOrEmpty(equipo.Imagen) ? equipo.Imagen : "/Uploads/default-team.png";
            }
            catch (Exception) { }
        }

        // CARGAR LOS JUAGDORES DEL EQUIPO
        private void CargarJugadores()
        {
            EquipoNegocio negocio = new EquipoNegocio();
            try
            {
                List<JugadorEquipo> lista = negocio.ListarJugadoresDeEquipo(IdEquipoSeleccionado);
                lblCantidadJugadores.Text = lista.Count.ToString();
                rptJugadores.DataSource = lista;
                rptJugadores.DataBind();
            }
            catch (Exception ex){
                throw ex;
            }
        }

        // CARGAR LAS SOLICITUDES PENDIENTES DE JUGADORES
        private void CargarSolicitudes()
        {
            EquipoNegocio negocio = new EquipoNegocio();
            try
            {
                List<Solicitud> listaSolicitudes = negocio.ListarSolicitudesPendientes(IdEquipoSeleccionado);

                rptSolicitudes.DataSource = listaSolicitudes;
                rptSolicitudes.DataBind();

                lblCantidadSolicitudes.Text = listaSolicitudes.Count.ToString();
                badgeSolicitudes.InnerText = listaSolicitudes.Count.ToString();

                // Ocultar badge si es 0
                badgeSolicitudes.Visible = listaSolicitudes.Count > 0;
            }
            catch { }
        }

        // --- ACCIONES DE EDICIÓN ---
        protected void btnAbrirEditar_Click(object sender, EventArgs e)
        {
            // Pre-llenar el modal con el nombre actual antes de abrirlo
            txtNombreEditar.Text = lblNombreEquipo.Text;

            // Usar ScriptManager para abrir el modal tras el PostBack
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "abrirModalEditar();", true);
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            try
            {
                EquipoNegocio negocio = new EquipoNegocio();
                Equipo equipo = negocio.ObtenerEquipoPorId(IdEquipoSeleccionado);

                // Actualizar Nombre
                equipo.Nombre = txtNombreEditar.Text;

                // Actualizar Imagen si se subió una nueva
                if (fuEscudoEditar.HasFile)
                {
                    string carpeta = Server.MapPath("~/Uploads/Escudos/");
                    if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                    string nombreArchivo = "Eq_" + DateTime.Now.Ticks + "_" + Path.GetFileName(fuEscudoEditar.FileName);
                    fuEscudoEditar.SaveAs(Path.Combine(carpeta, nombreArchivo));
                    equipo.Imagen = "/Uploads/Escudos/" + nombreArchivo;
                }

                negocio.ActualizarEquipo(equipo); 

                // Recargar pantalla
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                // Manejar error
            }
        }

        // --- ACCIONES DE ELIMINACIÓN ---
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                EquipoNegocio negocio = new EquipoNegocio();
                negocio.EliminarEquipo(IdEquipoSeleccionado);
                Response.Redirect("MisEquipos.aspx");
            }
            catch (Exception) { }
        }

        // --- ACCIONES DE SOLICITUDES ---
        protected void rptSolicitudes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idUsuarioSolicitante = int.Parse(e.CommandArgument.ToString());
            EquipoNegocio negocio = new EquipoNegocio();

            if (e.CommandName == "Aceptar")
            {
                negocio.AceptarSolicitud(idUsuarioSolicitante, IdEquipoSeleccionado);
            }
            else if (e.CommandName == "Rechazar")
            {
                // Solo borrar de Solicitudes
                negocio.RechazarSolicitud(idUsuarioSolicitante, IdEquipoSeleccionado);
            }

            // Recargar ambas listas
            CargarSolicitudes();
            CargarJugadores();
        }

        protected void btnBajaJugador_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idJugador = int.Parse(btn.CommandArgument);
            EquipoNegocio negocio = new EquipoNegocio();

            negocio.EliminarJugadorDeEquipo(idJugador, IdEquipoSeleccionado);
            CargarJugadores();
        }
    }
}