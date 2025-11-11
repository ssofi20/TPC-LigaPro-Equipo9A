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
            // 1. Obtener los datos del formulario
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string nombrePublico = txtNombrePublico.Text;
            string numeroTelefono = txtTelefonoContacto.Text;

            // 2. Validar si se cargó o no una imagen 
            if (FileUploadControl.HasFile)
            {
                string folderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = Path.GetFileName(FileUploadControl.FileName);

                string fullPath = Path.Combine(folderPath, fileName);

                FileUploadControl.SaveAs(fullPath);
            }
            else
            {
                // Manejar el caso cuando no se carga una imagen (puedes asignar un logo por defecto o mostrar un mensaje de error)
            }

            // 3. Crear el nuevo administrador (aquí debes llamar a tu lógica de negocio para guardar en la base de datos)
            OrganizadorNegocio organizadorNegocio = new OrganizadorNegocio();
            if (organizadorNegocio.ValidarEmailExistente(email))
            {
                lblMensaje.Text = "El email ya está registrado. Por favor, utiliza otro email.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
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
                Logo = FileUploadControl.HasFile ? "/Uploads/" + Path.GetFileName(FileUploadControl.FileName) : null,
            };
        }
    }
}