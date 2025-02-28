CREATE DATABASE EstadoDeCuentaDB;
GO
USE EstadoDeCuentaDB;
GO

CREATE TABLE TarjetaCredito (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreTitular NVARCHAR(100) NOT NULL,
    NumeroTarjeta NVARCHAR(16) NOT NULL,
    SaldoActual DECIMAL(10, 2) NOT NULL,
    LimiteCredito DECIMAL(10, 2) NOT NULL,
    SaldoDisponible AS (LimiteCredito - SaldoActual)
);

CREATE TABLE Compra (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Descripcion NVARCHAR(200) NOT NULL,
    Monto DECIMAL(10, 2) NOT NULL,
    TarjetaCreditoId INT,
    FOREIGN KEY (TarjetaCreditoId) REFERENCES TarjetaCredito(Id)
);

CREATE TABLE Pago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Monto DECIMAL(10, 2) NOT NULL,
    TarjetaCreditoId INT,
    FOREIGN KEY (TarjetaCreditoId) REFERENCES TarjetaCredito(Id)
);

SELECT * 
FROM INFORMATION_SCHEMA.TABLES;
