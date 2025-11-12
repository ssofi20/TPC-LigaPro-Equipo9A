using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;


namespace LigaPro.Negocio
{
    public class Seguridad
    {
        //Genera un hash seguro de una contraseña usando BCrypt.
        public string HashPassword(string passwordPlano)
        {
            return BCryptNet.HashPassword(passwordPlano);
        }
        //Verifica si una contraseña en texto plano coincide con un hash guardado. True si coinciden, False si no.</returns>
        public bool VerifyPassword(string passwordPlano, string passwordHashDB)
        {
            try
            {
                return BCryptNet.Verify(passwordPlano, passwordHashDB);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
