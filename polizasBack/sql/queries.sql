-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PoliciesDB')
BEGIN
    CREATE DATABASE PoliciesDB;
END
GO

-- Crear login para la aplicación
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'policies_user')
BEGIN
    CREATE LOGIN policies_user WITH PASSWORD = 'P0l1c13s@2025';
END
GO

-- Crear usuario en la base de datos y asignar permisos
USE PoliciesDB;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'policies_user')
BEGIN
    CREATE USER policies_user FOR LOGIN policies_user;
    
    -- Asignar permisos necesarios
    GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO policies_user;
    GRANT EXECUTE ON SCHEMA::dbo TO policies_user;
END
GO

-- Tabla de Usuarios
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        Password NVARCHAR(256) NOT NULL,
        Role NVARCHAR(20) NOT NULL,
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- Tabla de Clientes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Clients')
BEGIN
    CREATE TABLE Clients (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ExId NVARCHAR(100) NOT NULL UNIQUE,
        Name NVARCHAR(100) NOT NULL,
        FirstLastName NVARCHAR(100) NOT NULL,
        SecondLastName NVARCHAR(100),
        Age INT NOT NULL,
        BirthCountry NVARCHAR(100) NOT NULL,
        Gender NVARCHAR(10) NOT NULL,
        Email NVARCHAR(150) NOT NULL UNIQUE,
        Phone NVARCHAR(20) NOT NULL,
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- Tabla de Pólizas
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Policies')
BEGIN
    CREATE TABLE Policies (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        PolicyNumber NVARCHAR(50) NOT NULL UNIQUE,
        Type NVARCHAR(20) NOT NULL,
        StartDate DATETIME NOT NULL,
        EndDate DATETIME NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(20) NOT NULL,
        ClientId INT NOT NULL,
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (ClientId) REFERENCES Clients(Id)
    );
END
GO

--SP
CREATE PROCEDURE sp_ValidateUser
    @Username NVARCHAR(100)
AS
BEGIN
    SELECT Id, Username, Role 
    FROM Users 
    WHERE Username = @Username 
END
GO

CREATE PROCEDURE sp_ChangePassword
    @UserId INT,
    @NewPassword NVARCHAR(256)
AS
BEGIN
    UPDATE Users 
    SET Password = @NewPassword,
        UpdatedAt = GETDATE()
    WHERE Id = @UserId 
END
GO

CREATE PROCEDURE sp_GetPoliciesWithClient
    @UserId INT = NULL
AS
BEGIN
    SELECT 
        p.Id,
        p.PolicyNumber,
        p.Type,
        p.StartDate,
        p.EndDate,
        p.Amount,
        p.Status,
        c.Id AS ClientId,
        c.ExId,
        c.Name,
        c.FirstLastName,
        c.SecondLastName,
        c.Age,
        c.BirthCountry,
        c.Gender,
        c.Email,
        c.Phone
    FROM Policies p
    INNER JOIN Clients c ON p.ClientId = c.Id
    WHERE (@UserId IS NULL OR c.Id = @UserId)
END
GO

CREATE PROCEDURE sp_CreateUser
    @Username NVARCHAR(100),
    @Password NVARCHAR(256),
    @Role NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users (
        Username, Password, Role)
    VALUES (
        @Username, @Password, @Role);
    
    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_CreatePolicy
    @PolicyNumber NVARCHAR(50),
    @Type NVARCHAR(20),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @Amount DECIMAL(18,2),
    @ClientId INT
AS
BEGIN
    INSERT INTO Policies (
        PolicyNumber, Type, StartDate, EndDate, 
        Amount, Status, ClientId, CreatedAt, UpdatedAt)
    VALUES (
        @PolicyNumber, @Type, @StartDate, @EndDate,
        @Amount, 'Cotizada', @ClientId, GETDATE(), GETDATE())
    
    SELECT SCOPE_IDENTITY()
END
GO

-- Insertar usuario de prueba si no existe
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, Password, Role)
    VALUES ('admin@sisinsurance.com', '$2a$10$SboC54ZbmgVWZ0OoiSiRZu9.AgxmCtBzl5jF4W6Z6tGAYgOD0sefi', 'Admin');
END
GO

IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'broker')
BEGIN
    INSERT INTO Users (Username, Password, Role)
    VALUES ('broker@sisinsurance.com', '$2a$10$/iSqpGk2mED1CqgpHdI1cehbZuzXWXyVgr/sWkeiRUGwR5SQ14sk.', 'Broker');
END
GO

CREATE TABLE #TempClients (
    Username NVARCHAR(100),
    ClientId INT IDENTITY(1,1),
    Gender CHAR(1)
);

INSERT INTO PoliciesDB.dbo.Users 
(Username, Password, [Role], CreatedAt, UpdatedAt)
OUTPUT inserted.Username, 'F' INTO #TempClients (Username, Gender)
VALUES
(N'laura@mail.com',    N'$2a$10$OY.i6sjGER.LyRUKBaRN9.lxO/Hp8R5A3PPMQsQaVk733FAqkzyeS', N'Client', GETDATE(), GETDATE()),
(N'valeria@mail.com',  N'$2a$10$OY.i6sjGER.LyRUKBaRN9.lxO/Hp8R5A3PPMQsQaVk733FAqkzyeS', N'Client', GETDATE(), GETDATE());


INSERT INTO PoliciesDB.dbo.Users 
(Username, Password, [Role], CreatedAt, UpdatedAt)
OUTPUT inserted.Username, 'M' INTO #TempClients (Username, Gender)
VALUES
(N'carlos@mail.com',   N'$2a$10$OY.i6sjGER.LyRUKBaRN9.lxO/Hp8R5A3PPMQsQaVk733FAqkzyeS', N'Client', GETDATE(), GETDATE()),
(N'mario@mail.com',    N'$2a$10$OY.i6sjGER.LyRUKBaRN9.lxO/Hp8R5A3PPMQsQaVk733FAqkzyeS', N'Client', GETDATE(), GETDATE());

INSERT INTO PoliciesDB.dbo.Clients
(ExId, Name, FirstLastName, SecondLastName, Age, BirthCountry, Gender, Email, Phone, CreatedAt, UpdatedAt)
SELECT 
    NEWID(),
    CHOOSE(ClientId, 'Jonathan', 'Laura', 'Carlos', 'Valeria', 'Mario'),
    CHOOSE(ClientId, 'Alexis', 'Gomez', 'Fernandez', 'Santos', 'López'),
    CHOOSE(ClientId, 'Huerta', 'Ramirez', 'Torres', 'Martinez', 'Juarez'),
    22 + ClientId,
    N'México',
    Gender,
    Username,
    '55' + RIGHT('00000000' + CAST(66757530 + ClientId AS VARCHAR), 8),
    GETDATE(),
    GETDATE()
FROM #TempClients;

DECLARE @i INT = 0;
DECLARE @startDate DATE;
DECLARE @clientCount INT = (SELECT COUNT(*) FROM #TempClients);

WHILE @i < 10
BEGIN
    SET @startDate = DATEADD(DAY, @i, '2025-05-16');

    DECLARE @clientId INT = ((@i % @clientCount) + 1);
    DECLARE @gender CHAR(1) = (SELECT Gender FROM #TempClients WHERE ClientId = @clientId);
    DECLARE @amount MONEY = CASE WHEN @gender = 'F' THEN 2500 ELSE 2000 END;

    INSERT INTO PoliciesDB.dbo.Policies
    (PolicyNumber, [Type], StartDate, EndDate, Amount, Status, ClientId, CreatedAt, UpdatedAt)
    VALUES (
        NEWID(),
        CHOOSE((@i % 4) + 1, N'VIDA', N'AUTO', N'HOGAR', N'SALUD'),
        @startDate,
        DATEADD(MONTH, 1, @startDate),
        @amount,
        CHOOSE((@i % 3) + 1, N'Autorizada', N'Rechazada', N'Cotizada'),
        @clientId,
        GETDATE(),
        GETDATE()
    );

    SET @i += 1;
END

DROP TABLE #TempClients;