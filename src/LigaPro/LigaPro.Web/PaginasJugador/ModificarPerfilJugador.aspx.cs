using LigaPro.Datos;
using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.PaginasJugador
{
    public partial class ModificarPerfilJugador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                    if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Jugador)
                    {
                        JugadorDatos datos = new JugadorDatos();
                        Jugador user = datos.ObtenerInfoJugador(usuario.Id);

                        txtNombre.Text = user.Nombres;
                        txtApellido.Text = user.Apellidos;
                        txtFechaNacimiento.Text = user.FechaNacimiento.ToString("yyyy-MM-dd");
                        txtNombreUsuario.Text = usuario.NombreUsuario;
                        txtEmail.Text = usuario.Email;
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Jugador)
                {
                    JugadorDatos datos = new JugadorDatos();    
                    Jugador nuevo = datos.ObtenerInfoJugador(usuario.Id);

                    nuevo.Nombres = txtNombre.Text;
                    nuevo.Apellidos = txtApellido.Text;
                    nuevo.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                    usuario.NombreUsuario = txtNombreUsuario.Text;
                    usuario.Email = txtEmail.Text;

                    datos.modificarJugador(nuevo, usuario);
                    Response.Redirect("PerfilJugador.aspx", false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerfilJugador.aspx", false);
        }
    }
}