USE EstadoDeCuentaDB;
GO

CREATE PROCEDURE sp_ObtenerEstadoDeCuenta
    @TarjetaCreditoId INT,
    @PorcentajeInteres DECIMAL(5,2),
    @PorcentajeCuotaMinima DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        TC.NombreTitular,
        TC.NumeroTarjeta,
        TC.SaldoActual,
        TC.LimiteCredito,
        TC.SaldoDisponible,
        (TC.SaldoActual * (@PorcentajeInteres / 100)) AS InteresBonificable,
        (TC.SaldoActual * (@PorcentajeCuotaMinima / 100)) AS CuotaMinimaAPagar,
        (TC.SaldoActual + (TC.SaldoActual * (@PorcentajeInteres / 100))) AS MontoTotalAPagar
    FROM TarjetaCredito TC
    WHERE TC.Id = @TarjetaCreditoId;
END;
GO



CREATE OR ALTER PROCEDURE sp_ObtenerComprasPorMes
    @TarjetaCreditoId INT,
    @Mes INT,
    @Anio INT
AS
BEGIN
    SELECT 
        Id,                    -- Asegúrate de incluir el Id aquí
        Fecha,
        Descripcion,
        Monto,
        TarjetaCreditoId
    FROM Compra
    WHERE TarjetaCreditoId = @TarjetaCreditoId
      AND MONTH(Fecha) = @Mes
      AND YEAR(Fecha) = @Anio;
END
go


CREATE OR ALTER PROCEDURE sp_ObtenerPagosPorMes
    @TarjetaCreditoId INT,
    @Mes INT,
    @Anio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
		Id,
        Fecha,
        Monto,
		TarjetaCreditoId
    FROM Pago
    WHERE TarjetaCreditoId = @TarjetaCreditoId 
    AND MONTH(Fecha) = @Mes
    AND YEAR(Fecha) = @Anio;
END;
GO



CREATE PROCEDURE sp_RegistrarCompra
    @TarjetaCreditoId INT,
    @Fecha DATE,
    @Descripcion NVARCHAR(200),
    @Monto DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Compra (Fecha, Descripcion, Monto, TarjetaCreditoId)
    VALUES (@Fecha, @Descripcion, @Monto, @TarjetaCreditoId);

    UPDATE TarjetaCredito
    SET SaldoActual = SaldoActual + @Monto
    WHERE Id = @TarjetaCreditoId;
END;
GO


CREATE PROCEDURE sp_RegistrarPago
    @TarjetaCreditoId INT,
    @Fecha DATE,
    @Monto DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Pago (Fecha, Monto, TarjetaCreditoId)
    VALUES (@Fecha, @Monto, @TarjetaCreditoId);

    UPDATE TarjetaCredito
    SET SaldoActual = SaldoActual - @Monto
    WHERE Id = @TarjetaCreditoId;
END;
GO


