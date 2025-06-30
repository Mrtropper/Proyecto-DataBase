USE MASTER;
GO
-- Verificar si la base de datos 'GestionCCVC' existe y eliminarla si es necesario
IF EXISTS(SELECT NAME FROM DBO.SYSDATABASES WHERE NAME = 'GestionCCVC')
BEGIN
 DROP DATABASE GestionCCVC; -- Eliminar base de datos existente
END
GO


CREATE DATABASE GestionCCVC; -- Crear base de datos
GO

-- Se utiliza la base de datos "GestionCCVC"
USE GestionCCVC;
GO

-- Creación de la tabla "Cosmeticos"
CREATE TABLE Cosmeticos (
    IDCosmetico INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Identificador único del cosmético (Clave primaria con autoincremento)
    Nombre VARCHAR(250) NOT NULL, -- Nombre del cosmético
    Marca VARCHAR(250) NOT NULL, -- Marca del cosmético
    PrecioUnitario DECIMAL(10,000) NOT NULL, -- Precio unitario del cosmético
    StockDisponible INT NOT NULL, -- Cantidad de unidades disponibles en inventario
    FechaVencimiento DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de vencimiento del cosmético, por defecto la fecha actual
    Categoria VARCHAR(250) NOT NULL, -- Categoría del cosmético
    EstadoProducto VARCHAR(250) NOT NULL, -- Estado del producto (ejemplo: disponible, agotado, etc.)
    Imagen VARCHAR(250) -- Ruta o URL de la imagen del cosmético
);
GO

-- Creación de la tabla "Consumidores"
CREATE TABLE Consumidores (
    IdConsumidor INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Identificador único del consumidor (Clave primaria con autoincremento)
    NombreCompleto VARCHAR(250) NOT NULL, -- Nombre completo del consumidor
    telefono VARCHAR(250) NOT NULL, -- Número de teléfono del consumidor
    CorreoElectronico VARCHAR(250) NOT NULL, -- **Error de tipo de dato**, debería ser VARCHAR en lugar de DECIMAL
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de registro del consumidor, por defecto la fecha actual
    FrecuenciaCompra VARCHAR(250) NOT NULL, -- Frecuencia de compra del consumidor (ejemplo: diaria, semanal, mensual)
    PuntosFidelidad INT, -- Puntos de fidelidad acumulados por el consumidor
    Direccion VARCHAR(250) -- Dirección del consumidor
);
GO

-- Creación de la tabla "Compras"
CREATE TABLE Compras (
    IDCompra INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Identificador único de la compra (Clave primaria con autoincremento)
    FechaCompra DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de la compra, por defecto la fecha actual
    TotalCompra DECIMAL(10,000) NOT NULL, -- Monto total de la compra
    MetodoPago VARCHAR(250) NOT NULL, -- Método de pago utilizado (ejemplo: efectivo, tarjeta)
    Proveedor VARCHAR(250) NOT NULL, -- Nombre del proveedor
    CantidadProductos INT, -- Cantidad total de productos en la compra
    EstadoCompra VARCHAR(250), -- Estado de la compra (ejemplo: completada, pendiente)
    IDCosmeticos INT NOT NULL, -- Identificador del cosmético comprado
    CONSTRAINT FK_Compras_Cosmeticos FOREIGN KEY (IDCosmeticos) REFERENCES Cosmeticos(IDCosmetico) -- Clave foránea que relaciona la compra con la tabla "Cosmeticos"
);
GO
-- Creación de la tabla "Ventas"
CREATE TABLE Ventas (
    IdVenta INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Identificador único de la venta (Clave primaria con autoincremento)
    FechaVenta DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de la venta, por defecto la fecha actual
    TotalVenta DECIMAL(10,000) NOT NULL, -- Monto total de la venta
    MetodoPago VARCHAR(250) NOT NULL, -- Método de pago utilizado
    PuntosUsados INT, -- Puntos de fidelidad utilizados en la compra
    CantidadVendido INT NOT NULL, -- Cantidad de productos vendidos
    IDCosmeticos INT NOT NULL, -- Identificador del cosmético vendido
    CONSTRAINT FK_Ventas_Cosmeticos FOREIGN KEY (IDCosmeticos) REFERENCES Cosmeticos(IDCosmetico), -- Clave foránea que relaciona la venta con la tabla "Cosmeticos"
    IDConsumidor INT NOT NULL, -- Identificador del consumidor que realizó la compra
    CONSTRAINT FK_Ventas_Consumidores FOREIGN KEY (IDConsumidor) REFERENCES Consumidores(IdConsumidor), -- Clave foránea que relaciona la venta con la tabla "Consumidores"
    EstadoVenta VARCHAR(250) NOT NULL -- Estado de la venta (ejemplo: pagada, cancelada)
);
GO





--PARTE DE COSMETICOSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
CREATE OR ALTER PROCEDURE [Sp_Ins_Cosmeticos]
@Nombre VARCHAR(250),
@Marca VARCHAR(250),
@PrecioUnitario DECIMAL(10,2),
@StockDisponible INT,
@FechaVencimiento DATETIME,
@Categoria VARCHAR(250),
@EstadoProducto VARCHAR(250),
@Imagen VARCHAR(250)
AS
BEGIN
    INSERT INTO Cosmeticos (Nombre, Marca, PrecioUnitario, StockDisponible, FechaVencimiento, Categoria, EstadoProducto, Imagen)
    VALUES (@Nombre, @Marca, @PrecioUnitario, @StockDisponible, @FechaVencimiento, @Categoria, @EstadoProducto, @Imagen);

    PRINT 'Cosmético insertado correctamente';
END;
GO


--Parte de Comprasssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss

CREATE OR ALTER PROCEDURE [Sp_Ins_Compra]
@FechaCompra DATETIME,
@TotalCompra DECIMAL(10,2),
@MetodoPago VARCHAR(250),
@Proveedor VARCHAR(250),
@CantidadProductos INT,
@EstadoCompra VARCHAR(250),
@IDCosmeticos INT
AS
BEGIN
    INSERT INTO Compras (FechaCompra, TotalCompra, MetodoPago, Proveedor, CantidadProductos, EstadoCompra, IDCosmeticos)
    VALUES (@FechaCompra, @TotalCompra, @MetodoPago, @Proveedor, @CantidadProductos, @EstadoCompra, @IDCosmeticos);

    PRINT 'Compra insertada correctamente';
END;
GO

--Parte de Ventasssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
create or alter procedure [Sp_Ins_Ventas]
@FechaVenta DATETIME,
@TotalVenta Decimal(10,000),
@MetodoPago VARCHAR(250),
@PuntosUsados int,
@EstadoVenta VARCHAR(250),
@CantidadVendido int,
@IDCosmeticos int,
@IDConsumidor int
as 
begin 
	INSERT INTO Ventas(FechaVenta, TotalVenta, MetodoPago, PuntosUsados, EstadoVenta, CantidadVendido, IDCosmeticos, IDConsumidor)
	Values ( @FechaVenta,@TotalVenta, @MetodoPago, @PuntosUsados, @EstadoVenta,@CantidadVendido, @IDCosmeticos, @IDConsumidor);

	print 'Consumidor insertado correctamente';
	end;
	go

	
	CREATE OR ALTER PROCEDURE [Sp_Obtener_IDConsumidores]
AS
BEGIN
    SELECT IdConsumidor FROM Consumidores ORDER BY IdConsumidor;
END;
GO

create or alter procedure [Sp_Ins_Consumidor]
	@NombreCompleto VARCHAR(250),
	@Telefono VARCHAR(250),
	@CorreoElectronico VARCHAR(250),
	@FechaRegistro DATETIME,
	@FrecuenciaCompra VARCHAR(250), 
	@PuntosFidelidad int,
	@Direccion VARCHAR(500)
as 
begin 
	INSERT INTO Consumidores(NombreCompleto, Telefono, CorreoElectronico, FechaRegistro, FrecuenciaCompra, PuntosFidelidad, Direccion)
	Values ( @NombreCompleto, @Telefono, @CorreoElectronico, @FechaRegistro,@FrecuenciaCompra, @PuntosFidelidad, @Direccion);

	print 'Consumidor insertado correctamente';
	end;
	go

	create or alter procedure [Sp_Upd_Consumidor]
	@IDConsumidor int, 
	@NombreCompleto VARCHAR(250),
	@Telefono VARCHAR(250),
	@CorreoElectronico VARCHAR(250),
	@FechaRegistro DATETIME,
	@FrecuenciaCompra VARCHAR(250), 
	@PuntosFidelidad int,
	@Direccion VARCHAR(500)
as 
begin 
	UPDATE Consumidores
	set 
	NombreCompleto = @NombreCompleto,
	Telefono = @Telefono,
	CorreoElectronico = @CorreoElectronico,
	FechaRegistro = @FechaRegistro,
	FrecuenciaCompra = @FrecuenciaCompra, 
	PuntosFidelidad = @PuntosFidelidad
	where IDConsumidor = @IDConsumidor;

	Print 'consumidor actualizado';
	end;
	go
