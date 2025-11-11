using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class OrganizadorDatos
    {
        // Aquí van los métodos para interactuar con la base de datos relacionados con los organizadores

        public void InsertarOrganizador(Organizador organizador)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = "INSERT INTO Organizadores (IdUsuario, NombrePublico, Logo, EmailContacto, NumeroTelefono) " +
                               "VALUES (@IdUsuario, @NombrePublico, @Logo, @EmailContacto, @NumeroTelefono)";

                datos.setearConsulta(query);

                datos.setearParametro("@IdUsuario", organizador.IdUsuario);
                datos.setearParametro("@NombrePublico", organizador.NombrePublico);

                datos.setearParametro("@Logo", (object)organizador.Logo ?? DBNull.Value);

                datos.setearParametro("@EmailContacto", organizador.EmailContacto);
                datos.setearParametro("@NumeroTelefono", organizador.NumeroTelefono);

                datos.ejecutarAccion();
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
