USE EstadoDeCuentaDB;
GO

-- Datos Dummy para TarjetaCredito
INSERT INTO TarjetaCredito (NombreTitular, NumeroTarjeta, SaldoActual, LimiteCredito, SaldoDisponible)
VALUES 
    ('Juan Perez', '1234-5678-9012-3456', 1500.00, 5000.00, 3500.00),
    ('Maria Gomez', '2345-6789-0123-4567', 3000.00, 8000.00, 5000.00),
    ('Carlos Martinez', '3456-7890-1234-5678', 800.00, 3000.00, 2200.00);
GO

-- Datos Dummy para Compras
INSERT INTO Compra (Fecha, Descripcion, Monto, TarjetaCreditoId)
VALUES 
    ('2025-01-10', 'Compra en Amazon', 250.00, 1),
    ('2025-01-15', 'Cena en Restaurante', 80.00, 1),
    ('2025-01-20', 'Suscripción a Netflix', 15.00, 1),
    ('2025-01-05', 'Compra en Walmart', 120.00, 2),
    ('2025-01-18', 'Gasolina', 50.00, 2),
    ('2025-01-25', 'Spotify', 10.00, 2),
    ('2025-01-08', 'Ropa en Zara', 300.00, 3),
    ('2025-01-12', 'Cafetería', 20.00, 3),
    ('2025-01-22', 'Libros en Amazon', 90.00, 3);
GO

-- Datos Dummy para Pagos
INSERT INTO Pago (Fecha, Monto, TarjetaCreditoId)
VALUES 
    ('2025-01-15', 300.00, 1),
    ('2025-01-25', 150.00, 1),
    ('2025-01-10', 500.00, 2),
    ('2025-01-20', 200.00, 2),
    ('2025-01-05', 100.00, 3),
    ('2025-01-18', 50.00, 3);
GO