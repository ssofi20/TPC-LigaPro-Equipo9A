using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LigaPro.Datos;

namespace LigaPro.Web.PaginasOrganizador
{
    public partial class ModificarPerfilAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                    if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Organizador)
                    {
                        OrganizadorDatos datos = new OrganizadorDatos();
                        Organizador user = datos.ObtenerInfoAdmin(usuario.Id);

                        txtNombrePublico.Text = user.NombrePublico;
                        txtEmail.Text = user.EmailContacto;
                        txtTelefonoContacto.Text = user.NumeroTelefono;
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
            } else
            {
                Response.Redirect("/Auth/InicioSesion.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
                if (usuario != null && usuario.Id != 0 && usuario.Rol == Domain.RolUsuario.Organizador)
                {
                    OrganizadorDatos datos = new OrganizadorDatos();
                    Organizador nuevo = datos.ObtenerInfoAdmin(usuario.Id);

                    nuevo.NombrePublico = txtNombrePublico.Text;
                    nuevo.EmailContacto = txtEmail.Text;
                    nuevo.NumeroTelefono = txtTelefonoContacto.Text;

                    datos.modificarAdmin(nuevo);

                    Response.Redirect("PerfilAdmin.aspx", false);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerfilAdmin.aspx", false);
        }
    }
}