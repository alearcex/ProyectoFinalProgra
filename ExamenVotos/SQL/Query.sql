USE [Examen2]
GO
Create Table Candidatos(
IdCandidato int identity(1,1) Primary Key,
Cedula varchar(9) not null,
IdPartido int
)

ALTER TABLE Candidatos ADD CONSTRAINT FK_Candidatos_Cedula FOREIGN KEY (Cedula) REFERENCES Padron(Cedula)
ALTER TABLE Candidatos ADD CONSTRAINT FK_Candidatos_IdPartido FOREIGN KEY (IdPartido) REFERENCES Partidos(IdPartido)

Create Table Partidos(
IdPartido int identity(1,1) Primary Key,
Descripcion VARCHAR(30)
)

Create Table Padron(
Cedula varchar(9)Primary Key not null,
Nombre VARCHAR(15) not null,
PrimerApellido VARCHAR(15) not null,
SegundoApellido VARCHAR(15),
Edad INT CHECK (Edad >= 18)
)

Create Table Votos(
IdVoto int Primary Key not null,
Cedula varchar(9) not null,
IdCandidato int not null,
Fecha datetime
)

ALTER TABLE Votos ADD CONSTRAINT FK_Votos_Cedula FOREIGN KEY (Cedula) REFERENCES Padron(Cedula)

Create Table Usuarios(
Cedula varchar(9) Primary Key not null,
Contraseña Varchar(8) not null
)


-- ===========================================================
-- Author:       Alessandro Arce Chaves
-- Create date: 02/08/2024
-- Description: SP que valida las credenciales del usuario
-- ===========================================================
ALTER PROCEDURE ValidarUsuarioSP
    @cedula VARCHAR(9),
    @contra VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 
               FROM Usuarios 
               WHERE Cedula = @cedula 
               AND Contraseña = @contra)
    BEGIN
        SELECT 1 AS Resultado;
    END
    ELSE
    BEGIN
        SELECT 0 AS Resultado;
    END
END
GO

-- =============================================
-- Author:       Alessandro Arce Chaves
-- Create date: 02/08/2024
-- Description: Sp que maneja las consultas 
--              necesarias para la tabla Candidatos
-- =============================================
ALTER PROCEDURE [dbo].[CandidatosSP]
    @accion varchar(10),
    @idCandidato int = NULL,
    @cedula varchar(9) = NULL,
    @idPartido int = NULL
AS
BEGIN
    IF @accion = 'GUARDAR'
    BEGIN
        IF EXISTS (SELECT 1 FROM Candidatos WHERE Cedula = @cedula)
        BEGIN
            UPDATE Candidatos
            SET IdPartido = @idPartido
            WHERE Cedula = @cedula
        END
        ELSE
        BEGIN
            INSERT INTO Candidatos (Cedula, IdPartido)
            VALUES (@cedula, @idPartido)
        END
    END
    
    IF @accion = 'EDITAR'
    BEGIN
        UPDATE Candidatos
        SET Cedula = @cedula,
            IdPartido = @idPartido
        WHERE IdCandidato = @idCandidato
    END
    
    IF @accion = 'ELIMINAR'
    BEGIN
        DELETE FROM Candidatos
        WHERE IdCandidato = @idCandidato
    END

    IF @accion = 'SELECT'
    BEGIN
        SELECT c.IdCandidato, c.Cedula, c.IdPartido, p.Descripcion
        FROM Candidatos c
		INNER JOIN Partidos p ON p.IdPartido = c.IdPartido
    END
END

USE [Examen2]
GO
/****** Object:  StoredProcedure [dbo].[PadronSP]    Script Date: 17/08/2024 01:44:45 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alessandro Arce Chaves
-- Create date: 02/08/2024
-- Description: Sp que maneja las consultas 
--				necesarias para el examen 2
-- =============================================
ALTER PROCEDURE [dbo].[PadronSP]
	@accion varchar(10),
	@cedula varchar(10),
	@nombre varchar(15) = NULL,
	@apellido1 varchar(15) = NULL,
	@apellido2 varchar(15) = NULL,
	@edad int = NULL
AS
BEGIN
	IF @accion = 'GUARDAR'
    BEGIN
        IF EXISTS (SELECT 1 FROM Examen2.dbo.Padron WHERE Cedula = @cedula)
        BEGIN
            UPDATE Examen2.dbo.Padron
            SET Nombre = @nombre,
                PrimerApellido = @apellido1,
                SegundoApellido = @apellido2,
                Edad = @edad
            WHERE Cedula = @cedula
        END
        ELSE
        BEGIN
            INSERT INTO Examen2.dbo.Padron (Cedula, Nombre, PrimerApellido, SegundoApellido, Edad)
            VALUES (@cedula, @nombre, @apellido1, @apellido2, @edad)
			
			INSERT INTO Examen2.dbo.Usuarios(Cedula, Contraseña)
            VALUES (@cedula, 'tse' + RIGHT(REPLICATE('0', 4) + @cedula, 4))

        END
    END
    
	IF @accion = 'ELIMINAR'
    BEGIN
		DELETE FROM Examen2.dbo.Usuarios
        WHERE Cedula = @cedula

		DELETE FROM Examen2.dbo.Candidatos
        WHERE Cedula = @cedula	

		DELETE FROM Examen2.dbo.Votos
        WHERE Cedula = @cedula

		DELETE FROM Examen2.dbo.Padron
        WHERE Cedula = @cedula

    END

    IF @accion = 'SELECT'
    BEGIN
        SELECT Cedula, Nombre, PrimerApellido, SegundoApellido, Edad
        FROM Examen2.dbo.Padron
    END
END

-- =============================================
-- Author:		Alessandro Arce Chaves
-- Create date: 02/08/2024
-- Description: SP que maneja las consultas 
--				necesarias para la tabla Partidos
-- =============================================
ALTER PROCEDURE [dbo].[PartidosSP]
    @accion VARCHAR(10),
    @idPartido INT = NULL,
    @descripcion VARCHAR(30) = NULL
AS
BEGIN
    IF @accion = 'GUARDAR'
    BEGIN
        IF EXISTS (SELECT 1 FROM Partidos WHERE IdPartido = @idPartido)
        BEGIN
            UPDATE Partidos
            SET Descripcion = @descripcion
            WHERE IdPartido = @idPartido
        END
        ELSE
        BEGIN
            INSERT INTO Partidos (Descripcion)
            VALUES (@descripcion)
        END
    END

    IF @accion = 'ELIMINAR'
    BEGIN
	    DELETE FROM Candidatos
        WHERE IdPartido = @idPartido

        DELETE FROM Partidos
        WHERE IdPartido = @idPartido
    END

    IF @accion = 'SELECT'
    BEGIN
        SELECT IdPartido, Descripcion
        FROM Partidos
    END
END
