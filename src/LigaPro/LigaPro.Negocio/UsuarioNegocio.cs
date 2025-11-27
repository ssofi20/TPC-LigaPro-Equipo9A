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
                Usuario usuario = usuarioDatos.GetUsuarioPorEmail(email);

                if (usuario == null)
                {
                    return null;
                }

                Seguridad seguridad = new Seguridad();
                if (seguridad.VerifyPassword(passwordPlano, usuario.PasswordHash))
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
