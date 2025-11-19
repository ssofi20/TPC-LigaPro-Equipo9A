using LigaPro.Datos;
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
    public partial class ModificarSeguridadJugador : System.Web.UI.Page
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
                    Seguridad seguridad = new Seguridad();
                    JugadorDatos datos = new JugadorDatos();
                    if (seguridad.VerifyPassword(txtOldPass.Text, usuario.PasswordHash))
                    {
                        usuario.PasswordHash = seguridad.HashPassword(txtNewPass.Text);
                        datos.modificarPassJugador(usuario);

                        Response.Redirect("PerfilAdmin.aspx", false);
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
            Response.Redirect("PerfilJugador.aspx", false);

        }
    }
}