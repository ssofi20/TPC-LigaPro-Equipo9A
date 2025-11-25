using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasJugador
{
    public partial class MisEquipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Por Seguridad: Verificar si hay usuario logueado
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("~/Auth/InicioSesion.aspx");
                return;
            }

            // Si ya hay un usuario cargar equipos solo la primera vez (no en postbacks)
            if (!IsPostBack)
            {
                CargarEquipos();
            }
        }

        protected void CargarEquipos()
        {
            Usuario usuarioLogueado = (Usuario)Session["UsuarioLogueado"];

            EquipoNegocio equipoNegocio = new EquipoNegocio();

            try
            {
                List<Equipo> listaEquipos = equipoNegocio.ListarEquiposPorCreador(usuarioLogueado.Id);

                if (listaEquipos != null && listaEquipos.Count > 0)
                {
                    rptEquipos.DataSource = listaEquipos;
                    rptEquipos.DataBind();
                    pnlSinEquipos.Visible = false;
                }
                else
                {
                    pnlSinEquipos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Session["Error"] = ex.Message;
                //Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void btnGuardarEquipo_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            Usuario usuarioLogueado = (Usuario)Session["UsuarioLogueado"];
            string nombreEquipo = txtNombreEquipo.Text;

            // Manejo de Imagen
            string rutaImagen = null;
            if (fuEscudoEquipo.HasFile)
            {
                try
                {
                    string carpeta = Server.MapPath("~/Uploads/Escudos/");
                    if (!Directory.Exists(carpeta))
                        Directory.CreateDirectory(carpeta);

                    string nombreArchivo = "Eq_" + DateTime.Now.Ticks + "_" + Path.GetFileName(fuEscudoEquipo.FileName);
                    string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                    fuEscudoEquipo.SaveAs(rutaCompleta);
                    rutaImagen = "/Uploads/Escudos/" + nombreArchivo;
                }
                catch (Exception)
                {
                    rutaImagen = null;
                }

            }

            // Crear objeto Equipo
            Equipo nuevoEquipo = new Equipo();
            nuevoEquipo.Nombre = nombreEquipo;
            nuevoEquipo.IdUsuarioCreador = usuarioLogueado.Id;
            nuevoEquipo.Imagen = rutaImagen;

            // Guardar en BD
            EquipoNegocio negocio = new EquipoNegocio();
            try
            {
                negocio.CrearEquipo(nuevoEquipo);

                // Recargar la página para mostrar el nuevo equipo
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                // Mostrar error
            }
        }
    }
}