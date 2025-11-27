using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public Organizador ObtenerInfoAdmin(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select IdOrganizador, IdUsuario, NombrePublico , Logo , EmailContacto, NumeroTelefono FROM Organizadores  Where IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Organizador aux = new Organizador();
                    aux.Id = (int)datos.Lector["IdOrganizador"];
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.NombrePublico = (string)datos.Lector["NombrePublico"];
                    //aux.Logo = (string)datos.Lector["Logo"];
                    aux.Logo = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Logo"))
                        ? null
                        : datos.Lector.GetString(datos.Lector.GetOrdinal("Logo"));
                    aux.EmailContacto = (string)datos.Lector["EmailContacto"];
                    aux.NumeroTelefono = (string)datos.Lector["NumeroTelefono"];
                    return aux;
                }
                return null;
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

        public void modificarAdmin(Organizador organizador)
        {
            AccesoDatos aux = new AccesoDatos();

            try
            {
                aux.setearConsulta("UPDATE Organizadores SET NombrePublico = @NombrePublico, EmailContacto = @EmailContacto, NumeroTelefono = @NumeroTelefono WHERE IdUsuario = @IdUsuario");

                aux.setearParametro("@NombrePublico", organizador.NombrePublico);
                aux.setearParametro("@EmailContacto", organizador.EmailContacto);
                aux.setearParametro("@NumeroTelefono", organizador.NumeroTelefono);
                aux.setearParametro("@IdUsuario", organizador.IdUsuario);

                aux.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                aux.cerrarConexion();
            }
        }

        public void modificarPassAdmin(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET PasswordHash = @PasswordHash WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@PasswordHash", user.PasswordHash);
                datos.setearParametro("@IdUsuario", user.Id);

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

        public List<Organizador> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Organizador> lista = new List<Organizador>();
            try
            {
                datos.setearConsulta("SELECT IdOrganizador, IdUsuario, NombrePublico, EmailContacto, NumeroTelefono FROM Organizadores ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Organizador aux = new Organizador();
                    aux.Id = (int)datos.Lector["IdOrganizador"];
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.NombrePublico = (string)datos.Lector["NombrePublico"];
                    aux.EmailContacto = (string)datos.Lector["EmailContacto"];
                    aux.NumeroTelefono = (string)datos.Lector["NumeroTelefono"];

                    lista.Add(aux);
                }
                return lista;
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
