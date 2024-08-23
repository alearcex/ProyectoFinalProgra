CREATE DATABASE Examen2
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
IdVoto int Identity Primary Key not null,
Cedula varchar(9) not null,
IdCandidato int not null,
Fecha datetime
)

ALTER TABLE Votos ADD CONSTRAINT FK_Votos_Cedula FOREIGN KEY (Cedula) REFERENCES Padron(Cedula)

Create Table Usuarios(
Cedula varchar(9) Primary Key not null,
Contraseña Varchar(8) not null
)

INSERT INTO Padron (Cedula, Nombre, PrimerApellido, SegundoApellido, Edad) VALUES
('123456789', 'Juan', 'Gómez', 'Mora', 35),
('234567890', 'Ana', 'Ramírez', 'Castro', 28),
('345678901', 'Carlos', 'Pérez', 'Vargas', 42),
('456789012', 'María', 'Hernández', 'Soto', 30),
('567890123', 'Luis', 'Morales', 'Jiménez', 55),
('678901234', 'Laura', 'Gutiérrez', 'Montero', 25),
('789012345', 'Miguel', 'Cordero', 'Quirós', 40),
('890123456', 'Isabel', 'Cruz', 'Martínez', 33),
('901234567', 'Roberto', 'Alvarado', 'Rojas', 47),
('012345678', 'Patricia', 'Segura', 'Salazar', 29)

INSERT INTO Partidos (Descripcion) VALUES
('Liberación Nacional'),
('Acción Ciudadana'),
('Partido Random')

INSERT INTO Candidatos (Cedula, IdPartido) VALUES
('123456789', 1), 
('234567890', 2),  
('345678901', 3) 

INSERT INTO Votos (IdVoto, Cedula, IdCandidato, Fecha) VALUES
(1, '123456789', 1, '2024-08-01'),
(2, '234567890', 2, '2024-08-02'),
(3, '345678901', 3, '2024-08-03'),
(4, '456789012', 1, '2024-08-04'),
(5, '567890123', 2, '2024-08-05'),
(6, '678901234', 3, '2024-08-06');

INSERT INTO Usuarios (Cedula, Contraseña) VALUES
('123456789', 'tse6789'),
('234567890', 'tse7890'),
('345678901', 'tse8901'),
('456789012', 'tse9012'),
('567890123', 'tse0123'),
('678901234', 'tse1234'),
('789012345', 'tse2345'),
('890123456', 'tse3456'),
('901234567', 'tse4567'),
('012345678', 'tse5678');
