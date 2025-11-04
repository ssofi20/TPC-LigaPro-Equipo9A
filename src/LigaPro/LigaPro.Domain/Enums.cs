using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaPro.Domain
{
    ///Define los roles posibles para un usuario en el sistema.
    public enum RolUsuario
    {
        Organizador,
        Jugador
    }

    ///Define los posibles estados de una competición.
    public enum EstadoCompetencia
    {
        InscripcionAbierta,
        EnCurso,
        Finalizado
    }

    ///Define el tipo de liga (ej. ida o ida y vuelta).
    public enum TipoLiga
    {
        Ida,
        IdaYVuelta
    }

    ///Define los tipos de etapas que puede tener un torneo.
    public enum TipoEtapa
    {
        Grupo,
        EliminatoriaDirecta,
        Final
    }

    ///Define los posibles estados de un partido.
    public enum EstadoPartido
    {
        Pendiente,
        EnCurso,
        Finalizado,
        Suspendido,
        Walkover //Cuando un equipo no se presenta
    }

    ///Define los tipos de eventos que pueden ocurrir en un partido.
    public enum TipoEvento
    {
        Gol,
        Autogol,
        TarjetaAmarilla,
        TarjetaRoja,
        Sustitucion
    }

    public enum TipoFase 
    { 
        Grupos, EliminatoriaDirecta 
    }
}
