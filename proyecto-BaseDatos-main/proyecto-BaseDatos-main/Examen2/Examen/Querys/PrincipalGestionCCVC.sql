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





select * from Compras;




	CREATE OR ALTER PROCEDURE [Sp_Upd_Cosmeticos]
@IDCosmetico INT,
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
    UPDATE Cosmeticos
    SET 
        Nombre = @Nombre,
        Marca = @Marca,
        PrecioUnitario = @PrecioUnitario,
        StockDisponible = @StockDisponible,
        FechaVencimiento = @FechaVencimiento,
        Categoria = @Categoria,
        EstadoProducto = @EstadoProducto,
        Imagen = @Imagen
    WHERE IDCosmetico = @IDCosmetico;

    PRINT 'Cosmético actualizado correctamente';
END;
GO





	CREATE OR ALTER PROCEDURE [Sp_Del_Cosmetico]
@IDCosmetico INT
AS
BEGIN
    DELETE FROM Cosmeticos
    WHERE IDCosmetico = @IDCosmetico;

    PRINT 'Cosmético eliminado correctamente';
END;
GO









	CREATE OR ALTER PROCEDURE [Sp_Most_Cosmetico]
@Nombre VARCHAR(250)
AS
BEGIN
    SELECT * FROM Cosmeticos
    WHERE Nombre LIKE '%' + RTRIM(LTRIM(@Nombre)) + '%'
    ORDER BY Nombre;

    PRINT 'Búsqueda por nombre completada';
END;
GO






	CREATE OR ALTER PROCEDURE [Sp_Most_IDCosmetico]
@IDCosmetico INT
AS
BEGIN
    SELECT * FROM Cosmeticos
    WHERE IDCosmetico = @IDCosmetico;

    PRINT 'Búsqueda por ID completada';
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



CREATE OR ALTER PROCEDURE [Sp_Upd_Compra]
@IDCompra INT,
@FechaCompra DATETIME,
@TotalCompra DECIMAL(10,2),
@MetodoPago VARCHAR(250),
@Proveedor VARCHAR(250),
@CantidadProductos INT,
@EstadoCompra VARCHAR(250),
@IDCosmeticos INT
AS
BEGIN
    UPDATE Compras
    SET 
        FechaCompra = @FechaCompra,
        TotalCompra = @TotalCompra,
        MetodoPago = @MetodoPago,
        Proveedor = @Proveedor,
        CantidadProductos = @CantidadProductos,
        EstadoCompra = @EstadoCompra,
        IDCosmeticos = @IDCosmeticos
    WHERE IDCompra = @IDCompra;

    PRINT 'Compra actualizada correctamente';
END;
GO



CREATE OR ALTER PROCEDURE [Sp_Del_Compra]
@IDCompra INT
AS
BEGIN
    DELETE FROM Compras
    WHERE IDCompra = @IDCompra;

    PRINT 'Compra eliminada correctamente';
END;
GO


CREATE OR ALTER PROCEDURE [Sp_Most_IDCompra]
@IDCompra INT
AS
BEGIN
    SELECT * FROM Compras
    WHERE IDCompra = @IDCompra;

    PRINT 'Búsqueda por ID completada';
END;
GO



CREATE OR ALTER PROCEDURE [Sp_Most_EstadoCompra]
@EstadoCompra VARCHAR(250)
AS
BEGIN
    SELECT * FROM Compras
    WHERE EstadoCompra LIKE '%' + RTRIM(LTRIM(@EstadoCompra)) + '%'
    ORDER BY EstadoCompra;

    PRINT 'Búsqueda por estado completada';
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









	create or alter procedure [Sp_Upd_Venta]
	@IDVenta int,
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
	UPDATE Ventas
	set 
	FechaVenta = @FechaVenta,
	TotalVenta = @TotalVenta,
	MetodoPago = @MetodoPago,
	PuntosUsados = @PuntosUsados,
	EstadoVenta = @EstadoVenta,
	CantidadVendido = @CantidadVendido, 
	IDCosmeticos = @IDCosmeticos,
	IDConsumidor = @IDConsumidor

	where IDVenta = @IDVenta;

	Print 'Venta actualizado';
	end;
	go



	create or alter procedure [Sp_Del_Venta]
	@IdVenta int

	as 
	begin 
	Delete from Ventas
	where IDVenta = @IdVenta;
	PRINT 'Venta ELIMINADO';
	END;
	GO








	create or alter procedure [Sp_Most_EstadoVenta]
	@EstadoVenta VARCHAR(250)
	as 
	begin 
	select * from Ventas
	where EstadoVenta LIKE '%' + RTRIM(LTRIM(@EstadoVenta)) + '%'
	order by EstadoVenta;
	Print 'Busqueda completada';
	end;
	go





	create or alter procedure [Sp_Most_IDVenta]
	@IdVenta int
	as 
	begin
	select *
	from Ventas
	where IDVenta = @IdVenta;
	print 'Busqueda completada'

	end;
	go

	use GestionCCVC
	--busquedas para compras 
	
	CREATE OR ALTER PROCEDURE [Sp_Obtener_IDConsumidores]
AS
BEGIN
    SELECT IdConsumidor FROM Consumidores ORDER BY IdConsumidor;
END;
GO

create or alter procedure [Sp_Most_VentaConsumidor]
	@IdConsumidor int
	as 
	begin
	select *
	from Ventas
	where IDConsumidor = @IdConsumidor;
	print 'Busqueda completada'

	end;
	go

CREATE OR ALTER PROCEDURE [Sp_Obtener_IDCosmeticos]
AS
BEGIN

    SELECT IDCosmetico FROM Cosmeticos;

    PRINT 'Búsqueda de IDs de cosméticos completada';
END;
GO
	--Parte de Consumidoresssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
	
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



	create or alter procedure [Sp_Del_Consumidor]
	@IDConsumidor int

	as 
	begin 
	Delete from Consumidores
	where IDConsumidor = @IDConsumidor;
	PRINT 'uUSUARIO ELIMINADO';
	END;
	GO








	create or alter procedure [Sp_Most_ConsumidorNombre]
	@NombreCompleto VARCHAR(250)
	as 
	begin 
	select * from Consumidores
	where NombreCompleto LIKE '%' + RTRIM(LTRIM(@NombreCompleto)) + '%'
	order by NombreCompleto;
	Print 'Busqueda completada';
	end;
	go





	create or alter procedure [Sp_Most_IDConsumidor]
	@IDConsumidor int
	as 
	begin
	select *
	from Consumidores
	where IDConsumidor = @IDConsumidor;
	print 'Busqueda completada'

	end;
	go



	--Vistas de Crystal reportsssssssssssssssssssssssssssssssssssssssssssssssssss
	-- Crear o alterar la vista [Vp_Cns_Cosmeticos] en el esquema adecuado
create or alter view [Vp_Cns_Cosmeticos]
as
select 
    c.IDCosmetico,
    c.Nombre,
    c.Marca,
    c.PrecioUnitario,
    c.StockDisponible,
    c.FechaVencimiento,
    c.Categoria,
    c.EstadoProducto,
    c.Imagen
from Cosmeticos c
go



-- Crear o alterar la vista [Vp_Cns_Consumidores] en el esquema adecuado
create or alter view [Vp_Cns_Consumidores]
as
select 
    c.IdConsumidor,
    c.NombreCompleto,
    c.Telefono,
    c.CorreoElectronico,
    c.FechaRegistro,
    c.FrecuenciaCompra,
    c.PuntosFidelidad,
    c.Direccion
from Consumidores c
go




-- Crear o alterar la vista [Vp_Cns_Ventas] en el esquema adecuado
create or alter view [Vp_Cns_Ventas]
as
select 
    v.IdVenta,
    v.FechaVenta,
    v.TotalVenta,
    v.MetodoPago,
    v.PuntosUsados,
    v.CantidadVendido,
    v.IDCosmeticos,
    v.IDConsumidor,
    v.EstadoVenta
from Ventas v
go




-- Crear o alterar la vista [Vp_Cns_Compras] en el esquema adecuado
create or alter view [Vp_Cns_Compras]
as
select 
    c.IDCompra,
    c.FechaCompra,
    c.TotalCompra,
    c.MetodoPago,
    c.Proveedor,
    c.CantidadProductos,
    c.EstadoCompra,
    c.IDCosmeticos
from Compras c
go


