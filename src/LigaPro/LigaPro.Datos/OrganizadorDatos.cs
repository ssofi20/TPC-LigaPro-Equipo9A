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
                string query = "INSERT INTO Organizadores(IdUsuario, NombrePublico, Logo, EmailContacto, NumeroTelefono) VALUES(@idUsuario, @nombrePublico, @logo, @emailContacto, @numeroTel)";

                datos.setearConsulta(query);

                datos.setearParametro("@idUsuario", organizador.IdUsuario);
                datos.setearParametro("@nombrePublico", organizador.NombrePublico);
                datos.setearParametro("@logo", (object)organizador.Logo ?? DBNull.Value);
                datos.setearParametro("@emailContacto", organizador.EmailContacto);
                datos.setearParametro("@numeroTel", organizador.NumeroTelefono);

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
