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
    public partial class DetalleEquipo : System.Web.UI.Page
    {
        // Propiedad para acceder al ID fácilmente en toda la clase
        public int IdEquipoSeleccionado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Validar Sesión
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
                return;
            }

            // 2. Validar que venga un ID en la URL
            string idStr = Request.QueryString["id"];
            if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int idTemp))
            {
                Response.Redirect("MisEquipos.aspx"); // Si no hay ID valido, volver
                return;
            }

            IdEquipoSeleccionado = idTemp;

            if (!IsPostBack)
            {
                CargarDatosEquipo();
                CargarJugadores();
            }
        }

        private void CargarDatosEquipo()
        {
            EquipoNegocio negocio = new EquipoNegocio();
            try
            {
                // Obtenemos el equipo
                Equipo equipo = negocio.ObtenerEquipoPorId(IdEquipoSeleccionado);
                Usuario usuarioLogueado = (Usuario)Session["UsuarioLogueado"];

                // VALIDACIÓN DE SEGURIDAD:
                // ¿El equipo existe? ¿Pertenece al usuario logueado?
                if (equipo == null || equipo.IdUsuarioCreador != usuarioLogueado.Id)
                {
                    // Si intenta ver un equipo ajeno, lo mandamos afuera
                    Response.Redirect("MisEquipos.aspx");
                    return;
                }

                // Cargar datos en pantalla
                lblNombreEquipo.Text = equipo.Nombre;

                if (!string.IsNullOrEmpty(equipo.Imagen))
                    imgEscudo.ImageUrl = equipo.Imagen;
                else
                    imgEscudo.ImageUrl = "/Uploads/default-team.png";
            }
            catch (Exception ex)
            {
                Session["Error"] = "Error al cargar equipo: " + ex.Message;
                Response.Redirect("MisEquipos.aspx");
            }
        }

        private void CargarJugadores()
        {
            EquipoNegocio negocio = new EquipoNegocio();
            try
            {
                // Nota: Asegúrate de implementar la lógica real en EquipoNegocio.ListarJugadoresDeEquipo
                // Actualmente tu archivo EquipoNegocio.cs devuelve una lista vacía.
                var listaJugadores = negocio.ListarJugadoresDeEquipo(IdEquipoSeleccionado);

                // Actualizar contador
                lblCantidadJugadores.Text = listaJugadores.Count.ToString();

                // Enlazar al Repeater
                rptJugadores.DataSource = listaJugadores;
                rptJugadores.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de error silencioso o mostrar en un label de error
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBajaJugador_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}