using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigaPro.Datos;
using LigaPro.Domain;
using LigaPro.Domain.Actores;

namespace LigaPro.Negocio
{
    public class UsuarioNegocio
    {
        public Usuario Loguear(string email, string passwordPlano)
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            try
            {
                // 1. Obtener el usuario por email
                Usuario usuario = usuarioDatos.GetUsuarioPorEmail(email);

                // 2. Verificar si el usuario existe
                if (usuario == null)
                {
                    return null; // Usuario no encontrado
                }

                // 3. Verificar la contraseña
                Seguridad seguridad = new Seguridad();
                if (seguridad.VerifyPassword(passwordPlano, usuario.PasswordHash))
                {
                    // ¡Contraseña correcta!
                    return usuario;
                }
                else
                {
                    // Contraseña incorrecta
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Opcional: registrar el error
                throw ex;
            }
        }
    }
}
