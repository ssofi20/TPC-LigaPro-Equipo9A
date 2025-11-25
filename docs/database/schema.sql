USE master
GO

-- --- CREACIÓN DE LA BASE DE DATOS ---
CREATE DATABASE LIGAPRO_DB
GO

USE LIGAPRO_DB
GO

-- TABLAS DE ACTORES --

CREATE TABLE Usuarios(
	IdUsuario INT IDENTITY(1,1),
	Email VARCHAR(255) NOT NULL UNIQUE,
	PasswordHash VARCHAR(MAX) NOT NULL,
	NombreUsuario VARCHAR(100) NOT NULL,
	Rol VARCHAR(50) NOT NULL,
	FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
	CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario)
)
GO

CREATE TABLE Organizadores (
    IdOrganizador INT IDENTITY(1,1),
    IdUsuario INT NOT NULL UNIQUE,
    NombrePublico VARCHAR(150) NOT NULL,
    Logo NVARCHAR(MAX),
    EmailContacto VARCHAR(255),
    NumeroTelefono VARCHAR(50),
	CONSTRAINT PK_Organizador PRIMARY KEY (IdOrganizador),
    CONSTRAINT FK_Organizador_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
)
GO

CREATE TABLE Jugadores (
    IdJugador INT IDENTITY(1,1),
    IdUsuarioJugador INT NOT NULL UNIQUE,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    CONSTRAINT PK_Jugadores PRIMARY KEY (IdJugador),
    CONSTRAINT FK_Jugadores_Usuarios FOREIGN KEY (IdUsuarioJugador) REFERENCES Usuarios(IdUsuario)
)
GO

CREATE TABLE Equipos(
    IdEquipo INT IDENTITY(1,1),
    IdUsuarioCreador INT NOT NULL,
    Nombre NVARCHAR(150) NOT NULL,
    Imagen NVARCHAR(MAX),
    CONSTRAINT PF_Equipos PRIMARY KEY (IdEquipo),
    CONSTRAINT FK_Equipos_UsuarioCreador FOREIGN KEY (IdUsuarioCreador) REFERENCES Usuarios(IdUsuario)
)
GO

CREATE TABLE Solicitudes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,  -- El jugador que quiere entrar
    IdEquipo INT NOT NULL,   -- El equipo al que quiere entrar
    FechaSolicitud DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdEquipo) REFERENCES Equipos(IdEquipo)
)
GO

-- TABLA EQUIPO JUGADOR (Representa la instancia de un jugador dentro de un Equipo)

CREATE TABLE EquipoJugador(
    Id INT IDENTITY(1,1),
    IdEquipo INT NOT NULL,
    IdJugador INT NOT NULL, 
    NumeroCamiseta INT,
    Posicion VARCHAR(50),
    EsCapitan BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_EquipoJugador PRIMARY KEY (Id),
    CONSTRAINT FK_EquipoJugador_Equipo FOREIGN KEY (IdEquipo) REFERENCES Equipos(IdEquipo),
    CONSTRAINT FK_EquipoJugador_Jugador FOREIGN KEY (IdJugador) REFERENCES Jugadores(IdJugador)
)
GO

-- TABLAS DE COMPETICIÓN -- 

CREATE TABLE Reglamentos (
    IdReglamento INT IDENTITY(1,1),
    PuntosPorVictoria INT NOT NULL DEFAULT 3,
    PuntosPorEmpate INT NOT NULL DEFAULT 1,
    TarjetasAmarillasParaSuspension INT NOT NULL DEFAULT 5,
    PartidosSuspensionPorRojaDirecta INT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Reglamento PRIMARY KEY (IdReglamento)
)
GO

CREATE TABLE Competiciones (
    IdCompeticion INT PRIMARY KEY IDENTITY(1,1),
    IdOrganizador INT NOT NULL,
    IdReglamento INT NOT NULL,
    Nombre VARCHAR(200) NOT NULL,
    Estado VARCHAR(50) NOT NULL,

    -- Columna Discriminadora
    TipoCompeticion VARCHAR(50) NOT NULL, -- "Liga" o "Torneo"

    -- Propiedades de Liga (Nulables)
    FormatoLiga VARCHAR(50), -- (Enum TipoLiga: "Ida", "IdaYVuelta")

    -- Propiedades de Torneo (Nulables)
    TieneFaseDeGrupos BIT,

    CONSTRAINT FK_Competicion_Organizador FOREIGN KEY (IdOrganizador) REFERENCES Organizadores(IdOrganizador),
    CONSTRAINT FK_Competicion_Reglamento FOREIGN KEY (IdReglamento) REFERENCES Reglamentos(IdReglamento)
)
GO

-- TABLAS DE FASES Y GRUPOS (Esto es para torneos) --

CREATE TABLE Fases (
    IdFase INT PRIMARY KEY IDENTITY(1,1),
    IdTorneo INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
    Orden INT NOT NULL, -- 1 = Fase de Grupos, 2 = Octavos, etc.
    CONSTRAINT FK_Fases_Torneos FOREIGN KEY (IdTorneo) REFERENCES Competiciones(IdCompeticion)
);

CREATE TABLE Grupos (
    IdGrupo INT PRIMARY KEY IDENTITY(1,1),
    IdFase INT NOT NULL,
    Nombre VARCHAR(50) NOT NULL, -- Ej: "Grupo A", "Grupo B"
    CONSTRAINT FK_Grupos_Fases FOREIGN KEY (IdFase) REFERENCES Fases(IdFase)
);

CREATE TABLE Cruces (
    IdCruce INT PRIMARY KEY IDENTITY(1,1),
    IdFase INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL, -- Ej: "Cuartos 1", "Semifinal A"
    CONSTRAINT FK_Cruces_Fases FOREIGN KEY (IdFase) REFERENCES Fases(IdFase)
);

-- TABLA Inscripciones (Representa la inscripción de un Equipo a una Competición)
CREATE TABLE Inscripciones (
    IdInscripcion INT PRIMARY KEY IDENTITY(1,1),
    IdCompeticion INT NOT NULL,
    IdEquipo INT NOT NULL,
    IdGrupo INT, -- Nullable es solo para Fases de Grupos de Torneos
    FechaInscripcion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Inscripciones_Competiciones FOREIGN KEY (IdCompeticion) REFERENCES Competiciones(IdCompeticion),
    CONSTRAINT FK_Inscripciones_Equipos FOREIGN KEY (IdEquipo) REFERENCES Equipos(IdEquipo),
    CONSTRAINT FK_Inscripciones_Grupos FOREIGN KEY (IdGrupo) REFERENCES Grupos(IdGrupo),
    -- Un equipo solo puede inscribirse una vez por competición
    CONSTRAINT UQ_Competicion_Equipo UNIQUE (IdCompeticion, IdEquipo)
);

-- TABLA Partidos 
CREATE TABLE Partidos (
    IdPartido INT PRIMARY KEY IDENTITY(1,1),
    IdCompeticion INT NOT NULL,
    IdInscripcionA INT NOT NULL, -- Se refiere al Id de la tabla Inscripcion
    IdInscripcionB INT NOT NULL, -- Se refiere al Id de la tabla Inscripcion
    FechaHora DATETIME,
    Estado VARCHAR(50) NOT NULL,
    ResultadoEquipoA INT NOT NULL DEFAULT 0,
    ResultadoEquipoB INT NOT NULL DEFAULT 0,

    -- Columna Discriminadora
    TipoPartido NVARCHAR(50) NOT NULL, -- "Liga" o "Eliminatoria"

    -- Propiedades de PartidoLiga (Nulables)
    NumeroJornada INT,
    IdGrupo INT, -- Para partidos de fase de grupos

    -- Propiedades de PartidoEliminatoria (Nulables)
    GolesA_Extra INT,
    GolesB_Extra INT,
    PenalesA INT,
    PenalesB INT,
    IdCruce INT, -- El cruce al que pertenece este partido eliminatorio

    CONSTRAINT FK_Partido_Competicion FOREIGN KEY (IdCompeticion) REFERENCES Competiciones(IdCompeticion),
    CONSTRAINT FK_Partido_InscripcionA FOREIGN KEY (IdInscripcionA) REFERENCES Inscripciones(IdInscripcion),
    CONSTRAINT FK_Partido_InscripcionB FOREIGN KEY (IdInscripcionB) REFERENCES Inscripciones(IdInscripcion),
    CONSTRAINT FK_Partido_Grupo FOREIGN KEY (IdGrupo) REFERENCES Grupos(IdGrupo),
    CONSTRAINT FK_Partido_Cruce FOREIGN KEY (IdCruce) REFERENCES Cruces(IdCruce),
    CONSTRAINT UQ_Partido_Cruce UNIQUE (IdCruce) -- Un cruce solo tiene un partido
);

-- TABLAS DE ESTADÍSTICAS Y SANCIONES -- 
CREATE TABLE EventoPartido (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdPartido INT NOT NULL,
    IdEquipoJugador INT NOT NULL, -- Quién hizo el evento
    Tipo NVARCHAR(50) NOT NULL, 
    MinutoJuego INT NOT NULL,
    IdJugadorAsistencia INT, -- Nullable, FK a EquipoJugador (quién asistió)
    CONSTRAINT FK_EventoPartido_Partido FOREIGN KEY (IdPartido) REFERENCES Partidos(IdPartido),
    CONSTRAINT FK_EventoPartido_Jugador FOREIGN KEY (IdEquipoJugador) REFERENCES EquipoJugador(Id),
    CONSTRAINT FK_EventoPartido_Asistencia FOREIGN KEY (IdJugadorAsistencia) REFERENCES EquipoJugador(Id)
);

CREATE TABLE Sancion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdEquipoJugador INT NOT NULL,
    IdPartidoOrigen INT NOT NULL,
    Motivo NVARCHAR(200) NOT NULL, -- Ej: "Acumulación de 5 amarillas", "Roja directa"
    FechasDeSuspension INT NOT NULL,
    FechasCumplidas INT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Sanciones_Jugadores FOREIGN KEY (IdEquipoJugador) REFERENCES EquipoJugador(Id),
    CONSTRAINT FK_Sanciones_Partidos FOREIGN KEY (IdPartidoOrigen) REFERENCES Partidos(IdPartido)
);
select * from Usuarios where IdUsuario = 5
select * from Jugadores where IdUsuarioJugador = 5


select O.IdOrganizador,O.IdUsuario, O.NombrePublico , O.Logo , O.EmailContacto, O.NumeroTelefono 
FROM Organizadores O, Usuarios U Where O.IdUsuario = U.IdUsuario

UPDATE Organizadores 
SET NombrePublico = 'Sofia', EmailContacto = 'user1@user.com', NumeroTelefono = '1231234'
WHERE IdUsuario = 4



Select J.IdJugador, J.IdJugador, J.Nombres, J.Apellidos, J.FechaNacimiento, U.Email, U.NombreUsuario
FROM Jugadores J, Usuarios U
WHERE J.IdUsuarioJugador = U.IdUsuario



UPDATE Jugadores J, Usuarios U
SET J.Nombres = 'Sofia' , J.Apellidos = 'Iriarte', J.FechaNacimiento = '2004-08-19', U.Email = 'user3@user.com', U.NombreUsuario = 'Sofi'
FROM Jugadores J, Usuarios U WHERE J.IdUsuarioJugador = 5