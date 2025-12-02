using LigaPro.Domain.Actores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Datos
{
    public class PartidoDatos
    {
        public void CrearPartidoManual(Partido partido, int idEquipoLocal, int idEquipoVisita)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            INSERT INTO Partidos (
                IdCompeticion, 
                IdInscripcionA, 
                IdInscripcionB, 
                FechaHora, 
                Estado, 
                ResultadoEquipoA, 
                ResultadoEquipoB, 
                TipoPartido,
                NumeroJornada,
                IdGrupo,
                IdCruce
            )
            VALUES (
                @idTorneo,
                (SELECT IdInscripcion FROM Inscripciones WHERE IdEquipo = @idLocal AND IdTorneo = @idTorneo),
                (SELECT IdInscripcion FROM Inscripciones WHERE IdEquipo = @idVisita AND IdTorneo = @idTorneo),
                @fecha,
                'Pendiente',
                0, 
                0,
                @tipo,
                @jornada,
                @idGrupo,
                @idCruce
            )";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idTorneo", partido.IdTorneo);
                datos.setearParametro("@idLocal", idEquipoLocal);
                datos.setearParametro("@idVisita", idEquipoVisita);
                datos.setearParametro("@fecha", partido.FechaHora);
                datos.setearParametro("@tipo", partido.TipoPartido);

                // Parámetros específicos (Si es NULL, enviamos DBNull)
                if (partido is PartidoGrupo pg)
                {
                    datos.setearParametro("@jornada", pg.NumeroJornada);
                    datos.setearParametro("@idGrupo", (object)pg.IdGrupo ?? DBNull.Value);
                    datos.setearParametro("@idCruce", DBNull.Value);
                }
                else if (partido is PartidoEliminatoria pe)
                {
                    datos.setearParametro("@jornada", DBNull.Value);
                    datos.setearParametro("@idGrupo", DBNull.Value);
                    datos.setearParametro("@idCruce", (object)pe.IdCruce ?? DBNull.Value);
                }
                else
                {
                    // Caso genérico
                    datos.setearParametro("@jornada", DBNull.Value);
                    datos.setearParametro("@idGrupo", DBNull.Value);
                    datos.setearParametro("@idCruce", DBNull.Value);
                }

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }
    }
}
