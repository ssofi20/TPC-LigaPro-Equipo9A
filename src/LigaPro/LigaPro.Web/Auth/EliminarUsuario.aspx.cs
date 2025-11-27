using LigaPro.Datos;
using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web.Auth
{
    public partial class EliminarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                    if (usuario != null && usuario.Id != 0)
                    { }
                    else
                    {
                        Response.Redirect("/Default.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Response.Redirect("/Default.aspx", false);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                if (usuario != null && usuario.Id != 0)
                {
                    Seguridad seguridad = new Seguridad();
                    UsuarioDatos datos = new UsuarioDatos();
                    if (seguridad.VerifyPassword(txtPass.Text, usuario.PasswordHash))
                    {
                        usuario.PasswordHash = seguridad.HashPassword(txtPass.Text);
                        datos.DesactivarUsuario(usuario);

                        Response.Redirect("/Default.aspx", false);

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
            if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Organizador)
            {
                Response.Redirect("/PaginasOrganizador/PerfilAdmin.aspx", false);
            }else if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Jugador)
            {
                Response.Redirect("/PaginasJugador/PerfilJugador.aspx", false );
            }
        }
    }
}