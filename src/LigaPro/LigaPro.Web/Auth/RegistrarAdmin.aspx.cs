using LigaPro.Domain.Actores;
using LigaPro.Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LigaPro.Web
{
    public partial class RegistrarAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Evento del boton Registrar Administrador. Guarda los datos del nuevo administrador en la base de datos.
        protected void btnRegistrarAdmin_Click(object sender, EventArgs e)
        {
            // Verificar si todos los validadores de la página (Required, Compare, etc.) pasaron.
            if (!Page.IsValid)
            {
                return;
            }

            // 1. Obtener los datos del formulario
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string nombrePublico = txtNombrePublico.Text;
            string numeroTelefono = txtTelefonoContacto.Text;

            // 2. Validar si se cargó o no una imagen 

            string logoUrl = null; // Declaramos la URL del logo aquí
            if (FileUploadControl.HasFile)
            {
                try
                {
                    string folderPath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fileName = Path.GetFileName(FileUploadControl.FileName);
                    string fullPath = Path.Combine(folderPath, fileName);

                    FileUploadControl.SaveAs(fullPath);

                    // Guardamos la ruta relativa para la base de datos
                    logoUrl = "/Uploads/" + fileName;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al subir la imagen: " + ex.Message);
                    // Manejar error de subida de archivo
                    // lblMensaje.Text = "Error al subir la imagen: " + ex.Message;
                    // lblMensaje.ForeColor = System.Drawing.Color.Red;
                    // return;
                }
            }

            // 3. Crear el nuevo administrador
            OrganizadorNegocio organizadorNegocio = new OrganizadorNegocio();

            if (organizadorNegocio.ValidarEmailExistente(email))
            {
                //lblMensaje.Text = "El email ya está registrado. Por favor, utiliza otro email.";
                //lblMensaje.ForeColor = System.Drawing.Color.Red;
                //return;
            }
            
            Usuario nuevoUsuario = new Usuario
            {
                Email = email,
                PasswordHash = password, 
                NombreUsuario = nombrePublico,
                Rol = Domain.RolUsuario.Organizador,
                FechaRegistro = DateTime.Now
            };

            Organizador nuevoOrganizador = new Organizador
            {
                NombrePublico = nombrePublico,
                NumeroTelefono = numeroTelefono,
                Logo = logoUrl, // Asignamos la ruta que guardamos
                EmailContacto = email
            };

            organizadorNegocio.RegistrarNuevoOrganizador(nuevoUsuario, nuevoOrganizador);

            // 4. Envía email, guardar datos en la sesion y redirigir al perfil del nuevo administrador
            //lblMensaje.Text = "Administrador registrado exitosamente.";
            Session["UsuarioLogueado"] = nuevoUsuario;
            Response.Redirect("~/Organizador/PerfilAdmin.aspx");
        }
    }
}