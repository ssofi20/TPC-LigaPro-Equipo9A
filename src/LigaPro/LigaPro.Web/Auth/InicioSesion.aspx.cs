using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                //Llamar al método de logueo
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = usuarioNegocio.Loguear(email, password);

                if (usuario != null)
                {
                    Session["UsuarioLogueado"] = usuario;

                    //Redirigir segun el rol
                    if (usuario.Rol == Domain.RolUsuario.Organizador)
                    {
                        Response.Redirect("PerfilAdmin.aspx", false);
                    }
                    else if (usuario.Rol == Domain.RolUsuario.Jugador)
                    {
                        Response.Redirect("PerfilJugador.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                }
                else
                {
                    lblError.Text = "Email o contraseña incorrectos.";
                    lblError.Visible = true;
                }
            }
            catch (Exception)
            {
                lblError.Text = "Ocurrió un error inesperado. Por favor, intente más tarde.";
                lblError.Visible = true;
            }
        }
    }
}