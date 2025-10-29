USE LIGAPRO_DB
GO

--UPDATE
ALTER TABLE DEPORTES
ADD PermiteEmpate BIT NOT NULL DEFAULT 0

ALTER TABLE DEPORTES
ALTER COLUMN Nombre NVARCHAR(50) NOT NULL;

--INSERT
INSERT INTO DEPORTES (Nombre, CantidadJugadores, PermiteEmpate) VALUES 
('Fútbol', 11, 1),
('Básquet', 5, 0),
('Voley', 6, 0),
('Paddle', 2, 0)

SELECT * FROM DEPORTES

--Consulta para listar los deportes
SELECT IdDeporte, Nombre, CantidadJugadores, PermiteEmpate
FROM DEPORTES