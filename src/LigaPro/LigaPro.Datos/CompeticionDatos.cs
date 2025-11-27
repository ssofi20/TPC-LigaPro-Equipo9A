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
                datos.setearConsulta("INSERT INTO Competiciones (IdOrganizador, IdReglamento, Nombre, Estado, FormatoLiga, TieneFaseDeGrupos, Activo) values (@IdOrganizador, @IdReglamento, @Nombre, @Estado, @FormatoLiga, @TieneFaseDeGrupos, @Activo)");
                datos.setearParametro("@IdOrganizador", nuevo.OrganizadorCompetencia.Id);
                datos.setearParametro("@IdReglamento", nuevo.Reglas.Id);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Estado", nuevo.Estado);
                datos.setearParametro("@FormatoLiga", nuevo.Formato != null ? (object)nuevo.Formato : DBNull.Value);
                datos.setearParametro("@TieneFaseDeGrupos", nuevo.Fases);
                datos.setearParametro("@Activo", true);

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

        public List<Competicion> listarCompeticion()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Competicion> lista = new List<Competicion>();
            try
            {
                datos.setearConsulta("SELECT C.IdCompeticion, C.Nombre, C.Estado, C.FormatoLiga, C.TieneFaseDeGrupos, C.Activo, O.IdOrganizador, O.NombrePublico, R.IdReglamento, R.PuntosPorVictoria, R.PuntosPorEmpate, R.TarjetasAmarillasParaSuspension, R.PartidosSuspensionPorRojaDirecta FROM Competiciones C INNER JOIN Organizadores O ON O.IdOrganizador = C.IdOrganizador INNER JOIN Reglamentos R ON R.IdReglamento = C.IdReglamento");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Competicion aux = new Competicion();
                    aux.OrganizadorCompetencia = new Organizador();
                    aux.Reglas = new Reglamento();

                    aux.OrganizadorCompetencia.Id = (int)datos.Lector["IdOrganizador"];
                    aux.OrganizadorCompetencia.NombrePublico = (string)datos.Lector["NombrePublico"];

                    aux.Reglas.Id = (int)datos.Lector["IdReglamento"];
                    aux.Reglas.PuntosPorVictoria = (int)datos.Lector["PuntosPorVictoria"];
                    aux.Reglas.PuntosPorEmpate = (int)datos.Lector["PuntosPorEmpate"];
                    aux.Reglas.TarjetasAmarillasParaSuspension = (int)datos.Lector["TarjetasAmarillasParaSuspension"];
                    aux.Reglas.PartidosSuspensionPorRojaDirecta = (int)datos.Lector["PartidosSuspensionPorRojaDirecta"];


                    aux.Id = (int)datos.Lector["IdCompeticion"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (EstadoCompetencia)Enum.Parse(typeof(EstadoCompetencia), datos.Lector["Estado"].ToString());
                    aux.Formato = datos.Lector["FormatoLiga"] != DBNull.Value
                        ? datos.Lector["FormatoLiga"].ToString()
                        : null;
                    aux.Fases = datos.Lector["TieneFaseDeGrupos"] != DBNull.Value
                        ? Convert.ToBoolean(datos.Lector["TieneFaseDeGrupos"])
                        : false;
                    aux.Activo = (bool)datos.Lector["Activo"];
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
                datos.setearConsulta("SELECT C.IdCompeticion, C.Nombre, C.Estado, C.FormatoLiga, C.TieneFaseDeGrupos, O.IdOrganizador, O.NombrePublico, R.IdReglamento, R.PuntosPorVictoria, R.PuntosPorEmpate, R.TarjetasAmarillasParaSuspension, R.PartidosSuspensionPorRojaDirecta FROM Competiciones C INNER JOIN Organizadores O ON O.IdOrganizador = C.IdOrganizador INNER JOIN Reglamentos R ON R.IdReglamento = C.IdReglamento WHERE C.IdCompeticion = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Competicion aux = new Competicion();
                    aux.OrganizadorCompetencia = new Organizador();
                    aux.Reglas = new Reglamento();

                    aux.OrganizadorCompetencia.Id = (int)datos.Lector["IdOrganizador"];
                    aux.OrganizadorCompetencia.NombrePublico = (string)datos.Lector["NombrePublico"];

                    aux.Reglas.Id = (int)datos.Lector["IdReglamento"];
                    aux.Reglas.PuntosPorVictoria = (int)datos.Lector["PuntosPorVictoria"];
                    aux.Reglas.PuntosPorEmpate = (int)datos.Lector["PuntosPorEmpate"];
                    aux.Reglas.TarjetasAmarillasParaSuspension = (int)datos.Lector["TarjetasAmarillasParaSuspension"];
                    aux.Reglas.PartidosSuspensionPorRojaDirecta = (int)datos.Lector["PartidosSuspensionPorRojaDirecta"];

                    aux.Id = (int)datos.Lector["IdCompeticion"];
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

        public void modificarCompetencia(Competicion aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Reglamentos SET PuntosPorVictoria = @PV, PuntosPorEmpate = @PE, TarjetasAmarillasParaSuspension = @TAS, PartidosSuspensionPorRojaDirecta = @PSRD WHERE IdReglamento = @IdReglamento");
                datos.setearParametro("@PV", aux.Reglas.PuntosPorVictoria);
                datos.setearParametro("@PE", aux.Reglas.PuntosPorEmpate);
                datos.setearParametro("@TAS", aux.Reglas.TarjetasAmarillasParaSuspension);
                datos.setearParametro("@PSRD", aux.Reglas.PartidosSuspensionPorRojaDirecta);
                datos.setearParametro("@IdReglamento", aux.Reglas.Id);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                AccesoDatos datosComp = new AccesoDatos();


                datosComp.setearConsulta("UPDATE Competiciones SET IdOrganizador = @IdOrganizador, IdReglamento = @IdReglamento, Nombre = @Nombre, Estado = @Estado, FormatoLiga = @FormatoLiga, TieneFaseDeGrupos = @TieneFaseDeGrupos WHERE IdCompeticion = @IdCompeticion");
                datosComp.setearParametro("@IdOrganizador", aux.OrganizadorCompetencia.Id);
                datosComp.setearParametro("@IdReglamento", aux.Reglas.Id);
                datosComp.setearParametro("@Nombre", aux.Nombre);
                datosComp.setearParametro("@Estado", aux.Estado);
                datosComp.setearParametro("@FormatoLiga", aux.Formato != null ? (object)aux.Formato : DBNull.Value);
                datosComp.setearParametro("@TieneFaseDeGrupos", aux.Fases);
                datosComp.setearParametro("@IdCompeticion", aux.Id);
                datosComp.ejecutarAccion();
                datos.cerrarConexion();
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

        public void DesactivarCompeticion(Competicion aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Competiciones SET Activo = 0 WHERE IdCompeticion = @Id");
                datos.setearParametro("@Id", aux.Id);

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
