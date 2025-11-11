using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class UsuarioDatos
    {
        // Aquí van los métodos para interactuar con la base de datos relacionados con los usuarios
        
        public int InsertarUsuario(Usuario usuario)
        {
            // Lógica para insertar un nuevo usuario en la base de datos
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "INSERT INTO Usuarios (Email, PasswordHash, NombreUsuario, RolUsuario) " +
                               "VALUES (@Email, @PasswordHash, @NombreUsuario, @RolUsuario); " +
                               "SELECT SCOPE_IDENTITY();";

                datos.setearConsulta(query);

                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@PasswordHash", usuario.PasswordHash);
                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Rol", usuario.Rol.ToString());
                datos.setearParametro("@FechaRegistro", usuario.FechaRegistro);

                object resultado = datos.ejecutarScalar();
                return Convert.ToInt32(resultado);

            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Nuevo método para verificar si un email ya existe
        public bool VerificarEmailExistente(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(1) FROM Usuarios WHERE Email = @Email");
                datos.setearParametro("@Email", email);

                int count = Convert.ToInt32(datos.ejecutarScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
