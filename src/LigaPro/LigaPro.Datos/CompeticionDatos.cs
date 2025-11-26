using LigaPro.Domain;
using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class CompeticionDatos
    {
        public void agregarComp(Competicion nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Competiciones (IdOrganizador, IdReglamento, Nombre, Estado, FormatoLiga, TieneFaseDeGrupos) values (@IdOrganizador, @IdReglamento, @Nombre, @Estado, @FormatoLiga, @TieneFaseDeGrupos)");
                datos.setearParametro("@IdOrganizador", nuevo.OrganizadorCompetencia.Id);
                datos.setearParametro("@IdReglamento", nuevo.Reglas.Id);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Estado", nuevo.Estado);
                datos.setearParametro("@FormatoLiga", nuevo.Formato);
                datos.setearParametro("@TieneFaseDeGrupos", nuevo.Fases);

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

       /* public List<Competicion> listarCompeticion()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Competicion> lista = new List<Competicion>();
            try
            {
                datos.setearConsulta("SELECT IdOrganizador, IdReglamento, Nombre, Estado, FormatoLiga, TieneFaseDeGrupos FROM Competiciones");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Competicion aux = new Competicion();
                    aux.IdOrganizador = (int)datos.Lector["IdOrganizador"];
                    aux.IdReglamento = (int)datos.Lector["IdReglamento"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    aux.Formato = (string)datos.Lector["FormatoLiga"];
                    aux.Fases = (bool)datos.Lector["TieneFaseDeGrupos"];
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
        public Competicion buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdOrganizador, IdReglamento, Nombre, Estado, FormatoLiga, TieneFaseDeGrupos FROM Competiciones WHERE IdCompeticion = " + id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Competicion aux = new Competicion();
                    aux.IdOrganizador = (int)datos.Lector["IdOrganizador"];
                    aux.IdReglamento = (int)datos.Lector["IdReglamento"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    aux.Formato = datos.Lector["FormatoLiga"] != DBNull.Value
                        ? datos.Lector["FormatoLiga"].ToString()
                        : null;

                    aux.Fases = datos.Lector["TieneFaseDeGrupos"] != DBNull.Value
                        ? Convert.ToBoolean(datos.Lector["TieneFaseDeGrupos"])
                        : false;
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

        public Competicion modificar(Competicion aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }*/


    }
}
