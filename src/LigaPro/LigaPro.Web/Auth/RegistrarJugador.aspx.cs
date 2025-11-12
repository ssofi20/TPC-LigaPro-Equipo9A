using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web
{
    public partial class RegistrarJugador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            /// 1. Obtener los datos del formulario
            string nombres = txtNombre.Text;
            string apellidos = txtApellido.Text;
            DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
            string nombreUsuario = txtNombreUsuario.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            /// 2. Creo los objetos para enviar al negocio
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.Email = email;
            nuevoUsuario.NombreUsuario = nombreUsuario;
            nuevoUsuario.PasswordHash = password;

            Jugador nuevoJugador = new Jugador();
            nuevoJugador.Nombres = nombres;
            nuevoJugador.Apellidos = apellidos;
            nuevoJugador.FechaNacimiento = fechaNacimiento;

            /// 3. Llamar al método de registro de jugador
            JugadorNegocio negocio = new JugadorNegocio();
            negocio.RegistrarNuevoJugador(nuevoUsuario, nuevoJugador);

            /// 4. Redirigir a la página de inicio de sesión
            Session["UsuarioLogueado"] = nuevoUsuario;
            Response.Redirect("~/PaginasJugador/PerfilJugador.aspx");
        }
    }
}