/**
Tabla de Candidatos (CandidatoID PK, Nombre, Partido, Plataforma) - Tabla Fuerte

Tabla de Votantes (VotanteID PK, Nombre, Cedula) - Tabla Fuerte

Tabla de Votos (VotoID PK, VotanteID FK, CandidatoID FK, Fecha, Hora) - Tabla Debil

Tabla de Resultados (ResultadoID PK, CandidatoID FK, Votos_Totales, Porcentaje_Total) - Tabla Debil

**/

-- Crear la base de datos
CREATE DATABASE VOTACIONES_DB
GO

-- Usar la base de datos
USE VOTACIONES_DB
GO

-- Tabla de Candidatos
CREATE TABLE CANDIDATOS
(
    CANDIDATO_ID INT IDENTITY(1,1) PRIMARY KEY,
    NOMBRE VARCHAR(100) NOT NULL, 
    PARTIDO VARCHAR(100) NOT NULL,
    PROPUESTA TEXT, -- Propuestas del candidato
    UNIQUE (NOMBRE, PARTIDO) -- Evita que dos candidatos con el mismo nombre y partido existan
)
GO

-- Tabla de Votantes
CREATE TABLE VOTANTES
(
    VOTANTE_ID INT IDENTITY(1,1) PRIMARY KEY,
    NOMBRE VARCHAR(100) NOT NULL, 
    CEDULA VARCHAR(50) UNIQUE NOT NULL, -- Evita duplicación de cédulas
    CHECK (LEN(CEDULA) = 9) -- Asegura que la cédula tenga 9 caracteres
)
GO

-- Tabla de Votos
CREATE TABLE VOTOS
(
    VOTO_ID INT IDENTITY(1,1) PRIMARY KEY,
    VOTANTE_ID INT,
    CANDIDATO_ID INT,
    FECHA DATE NOT NULL,
    HORA TIME NOT NULL,
    FOREIGN KEY (VOTANTE_ID) REFERENCES VOTANTES(VOTANTE_ID) ON DELETE CASCADE, -- Asegura que si se elimina un votante, se eliminen todos sus votos
    FOREIGN KEY (CANDIDATO_ID) REFERENCES CANDIDATOS(CANDIDATO_ID) ON DELETE CASCADE, -- Asegura que si se elimina un candidato, se eliminen todos los votos asociados a ese candidato
    CHECK (FECHA >= '2024-01-01'), -- Asegura que la fecha de votación sea en el año 2024 o posterior
    UNIQUE (VOTANTE_ID, CANDIDATO_ID) -- Asegura que un votante no vote más de una vez por el mismo candidato
)
GO

-- Tabla de Resultados
CREATE TABLE RESULTADOS
(
    RESULTADO_ID INT IDENTITY(1,1) PRIMARY KEY,
    CANDIDATO_ID INT,
    VOTOS_TOTALES INT NOT NULL DEFAULT 0, -- Total de votos obtenidos por el candidato, con valor por defecto 0
    PORCENTAJE_TOTAL DECIMAL(5, 2) NOT NULL, -- Porcentaje total de votos obtenidos por el candidato, con dos decimales
    FOREIGN KEY (CANDIDATO_ID) REFERENCES CANDIDATOS(CANDIDATO_ID) ON DELETE CASCADE, -- Asegura que si se elimina un candidato, se eliminen todos los resultados asociados a ese candidato
    CHECK (PORCENTAJE_TOTAL >= 0 AND PORCENTAJE_TOTAL <= 100) -- Asegura que el porcentaje esté entre 0 y 100
)
GO

-- Insertar el primer candidato con una propuesta
INSERT INTO CANDIDATOS (NOMBRE, PARTIDO, PROPUESTA) VALUES ('Jose Figueres Ferrer', 'Liberacion Nacional', 'Reformas educativas y agrarias')

-- Insertar el segundo candidato con su propuesta
INSERT INTO CANDIDATOS (NOMBRE, PARTIDO, PROPUESTA) VALUES ('Rafael Angel Calderon Guardia', 'Partido Republicano', 'Seguridad social y bienestar')

-- Insertar el tercer candidato con su propuesta
INSERT INTO CANDIDATOS (NOMBRE, PARTIDO, PROPUESTA) VALUES ('Ricardo Jimenez Oreamuno', 'Partido Reformista', 'Transparencia y anticorrupción')

SELECT * FROM CANDIDATOS

-- Insertar el primer votante
INSERT INTO VOTANTES (NOMBRE, CEDULA) VALUES ('Maria Gonzalez', '123456789');

-- Insertar el segundo votante
INSERT INTO VOTANTES (NOMBRE, CEDULA) VALUES ('Carlos Ramirez', '987654321');

-- Insertar el tercer votante
INSERT INTO VOTANTES (NOMBRE, CEDULA) VALUES ('Ana Rodriguez', '456789123');

-- Insertar el cuarto votante
INSERT INTO VOTANTES (NOMBRE, CEDULA) VALUES ('Jorge Perez', '112233445');

-- Insertar el quinto votante
INSERT INTO VOTANTES (NOMBRE, CEDULA) VALUES ('Lucia Fernandez', '998877665');

-- Insertar el sexto votante
INSERT INTO VOTANTES (NOMBRE, CEDULA) VALUES ('Brenda Chavarria', '207960801');

SELECT * FROM VOTANTES

-- Como funciona un login. Es un select donde se valida lo que el usuario digito y la clave
SELECT NOMBRE, CEDULA FROM VOTANTES WHERE NOMBRE='Maria Gonzalez' AND CEDULA='123456789'



