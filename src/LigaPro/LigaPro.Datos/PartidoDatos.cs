using LigaPro.Domain.Actores;
using LigaPro.Domain.Partidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class PartidoDatos
    {
        // PARTIDO MANUAL - GRUPOS
        public void CrearPartidoManual(PartidoGrupo partido, int idEquipoLocal, int idEquipoVisita)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                AsignarEquipoAGrupo(partido.IdTorneo, idEquipoLocal, partido.NombreGrupo);
                AsignarEquipoAGrupo(partido.IdTorneo, idEquipoVisita, partido.NombreGrupo);

                datos.setearConsulta("INSERT INTO Partidos (IdInscripcionA, IdInscripcionB, IdTorneo, IdGrupo,FechaHora, ResultadoEquipoA, ResultadoEquipoB, TipoPartido, Estado)\r\nSELECT \r\n\t(SELECT IdInscripcion FROM Inscripciones WHERE IdEquipo = @idEquipoLocal AND IdTorneo = @idTorneo),    \r\n\t(SELECT IdInscripcion FROM Inscripciones WHERE IdEquipo = @idEquipoVisita AND IdTorneo = @idTorneo),\r\n\t@idTorneo,\r\n\t(SELECT IdGrupo FROM Grupos WHERE IdTorneo = @idTorneo AND Nombre = @nombreGrupo),\r\n\t@fechaHora,\r\n\t@resultadoA,\r\n\t@resultadoB, \r\n\t@tipoPartido,\r\n\t@estado;");
                datos.setearParametro("@idTorneo", partido.IdTorneo);
                datos.setearParametro("@idEquipoLocal", idEquipoLocal);
                datos.setearParametro("@idEquipoVisita", idEquipoVisita);
                datos.setearParametro("@nombreGrupo", partido.NombreGrupo);
                datos.setearParametro("@fechaHora", partido.FechaHora);
                datos.setearParametro("@resultadoA", partido.ResultadoEquipoA);
                datos.setearParametro("@resultadoB", partido.ResultadoEquipoB);
                datos.setearParametro("@tipoPartido", partido.TipoPartido);
                datos.setearParametro("@estado", partido.Estado);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public void AsignarEquipoAGrupo(int idTorneo, int idEquipo, string nombreGrupo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DECLARE @IdInscripcion INT;\r\nDECLARE @IdGrupo INT;\r\n\r\nSELECT @IdInscripcion = IdInscripcion \r\nFROM Inscripciones \r\nWHERE IdEquipo = @idEquipo AND IdTorneo = @idTorneo;\r\n\r\nSELECT @IdGrupo = IdGrupo \r\nFROM Grupos \r\nWHERE IdTorneo = @idTorneo AND Nombre = @nombreGrupo;\r\n\r\nIF NOT EXISTS (SELECT 1 FROM InscripcionGrupo WHERE IdInscripcion = @IdInscripcion AND IdGrupo = @IdGrupo)\r\nBEGIN\r\n\tINSERT INTO InscripcionGrupo (IdGrupo, IdInscripcion) \r\n\tVALUES (@IdGrupo, @IdInscripcion);\r\nEND");
                datos.setearParametro("@idTorneo", idTorneo);
                datos.setearParametro("@idEquipo", idEquipo);
                datos.setearParametro("@nombreGrupo", nombreGrupo);
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

        // PARTIDO MANUAL - ELIMINATORIAS
        public void CrearPartidoManual(PartidoEliminatoria partido, int idEquipoLocal, int idEquipoVisita)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Partidos (\r\n    IdInscripcionA, \r\n    IdInscripcionB, \r\n    IdTorneo,       \r\n    FechaHora, \r\n    ResultadoEquipoA, \r\n    ResultadoEquipoB, \r\n    TipoPartido, \r\n    Estado\r\n)\r\nSELECT \r\n    (SELECT IdInscripcion \r\n     FROM Inscripciones \r\n     WHERE IdEquipo = @idEquipoLocal AND IdTorneo = @idTorneo),\r\n     \r\n    (SELECT IdInscripcion \r\n     FROM Inscripciones \r\n     WHERE IdEquipo = @idEquipoVisita AND IdTorneo = @idTorneo),\r\n     \r\n    @idTorneo,\r\n    @fechaHora,\r\n    @resultadoA,\r\n    @resultadoB,\r\n    @tipoPartido,\r\n    @estado");
                datos.setearParametro("@idTorneo", partido.IdTorneo);
                datos.setearParametro("@idEquipoLocal", idEquipoLocal);
                datos.setearParametro("@idEquipoVisita", idEquipoVisita);
                datos.setearParametro("@fechaHora", partido.FechaHora);
                datos.setearParametro("@resultadoA", partido.ResultadoEquipoA);
                datos.setearParametro("@resultadoB", partido.ResultadoEquipoB);
                datos.setearParametro("@tipoPartido", partido.TipoPartido);
                datos.setearParametro("@estado", partido.Estado);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // LISTADO DE PARTIDOS PARA PANEL DE GESTION
        public List<PartidoDto> ListarPartidosResumen(int idTorneo)
        {
            List<PartidoDto> lista = new List<PartidoDto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
                    SELECT 
                        P.IdPartido,
                        P.FechaHora,
                        P.Estado,
                        P.ResultadoEquipoA,
                        P.ResultadoEquipoB,
                        P.TipoPartido,
                        G.Nombre as NombreGrupo,
                        E1.Nombre as Local,
                        E2.Nombre as Visita
                    FROM Partidos P
                    INNER JOIN Inscripciones I1 ON P.IdInscripcionA = I1.IdInscripcion
                    INNER JOIN Equipos E1 ON I1.IdEquipo = E1.IdEquipo
                    INNER JOIN Inscripciones I2 ON P.IdInscripcionB = I2.IdInscripcion
                    INNER JOIN Equipos E2 ON I2.IdEquipo = E2.IdEquipo
                    LEFT JOIN Grupos G ON P.IdGrupo = G.IdGrupo
                    WHERE P.IdTorneo = @idTorneo
                    ORDER BY P.FechaHora DESC";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idTorneo", idTorneo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    PartidoDto aux = new PartidoDto();
                    aux.Id = (int)datos.Lector["IdPartido"];
                    aux.FechaProgramada = (DateTime)datos.Lector["FechaHora"];
                    aux.NombreLocal = (string)datos.Lector["Local"];
                    aux.NombreVisita = (string)datos.Lector["Visita"];
                    aux.GolesLocal = (int)datos.Lector["ResultadoEquipoA"];
                    aux.GolesVisita = (int)datos.Lector["ResultadoEquipoB"];
                    aux.Estado = (string)datos.Lector["Estado"];
                    aux.Jugado = aux.Estado == "Finalizado";

                    string tipo = (string)datos.Lector["TipoPartido"];
                    if (tipo == "Grupo")
                    {
                        string grupo = datos.Lector["NombreGrupo"] != DBNull.Value ? datos.Lector["NombreGrupo"].ToString() : "?";
                        aux.Nombre = "Grupo " + grupo;
                    }
                    else
                    {
                        aux.Nombre = tipo; // Ej: "Octavos", "Final"
                    }

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

        // OBTERNER PARTIDO POR ID
        public PartidoDto ObtenerPorId(int idPartido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Traemos los datos básicos de la tabla Partidos
                datos.setearConsulta("SELECT IdPartido, FechaHora, Estado, ResultadoEquipoA, ResultadoEquipoB, TipoPartido FROM Partidos WHERE IdPartido = @id");
                datos.setearParametro("@id", idPartido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    PartidoDto partido = new PartidoDto();
                    partido.Id = (int)datos.Lector["IdPartido"];
                    partido.FechaProgramada = (DateTime)datos.Lector["FechaHora"];
                    partido.Estado = datos.Lector["Estado"].ToString();

                    // Mapeamos goles por si los necesitas, aunque para modificar fecha/estado no son críticos
                    partido.GolesLocal = datos.Lector["ResultadoEquipoA"] != DBNull.Value ? (int)datos.Lector["ResultadoEquipoA"] : 0;
                    partido.GolesVisita = datos.Lector["ResultadoEquipoB"] != DBNull.Value ? (int)datos.Lector["ResultadoEquipoB"] : 0;

                    return partido;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // MODIFICAR FECHA, HORA Y ESTADO DE UN PARTIDO
        public void ModificarPartido(int idPartido, DateTime nuevaFecha, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Partidos SET FechaHora = @fechaHora, Estado = @estado WHERE IdPartido = @idPartido");
                datos.setearParametro("@fechaHora", nuevaFecha);
                datos.setearParametro("@estado", nuevoEstado);
                datos.setearParametro("@idPartido", idPartido);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // ELIMINAR PARTIDO
        public void EliminarPartido(int idPartido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Partidos SET Activo = 0 WHERE IdPartido = @id");
                datos.setearParametro("@id", idPartido);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // CARGAR RESULTADO DE PARTIDO
        public void CargarResultado(int idPartido, int golesL, int golesV, bool finalizado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string estado = finalizado ? "Finalizado" : "Pendiente";

                datos.setearConsulta(@"
                    UPDATE Partidos 
                    SET ResultadoEquipoA = @golesL, 
                        ResultadoEquipoB = @golesV, 
                        Estado = @estado 
                    WHERE IdPartido = @id");

                datos.setearParametro("@golesL", golesL);
                datos.setearParametro("@golesV", golesV);
                datos.setearParametro("@estado", estado);
                datos.setearParametro("@id", idPartido);

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
