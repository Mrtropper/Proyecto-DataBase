--
create tablespace proyecto
datafile 'C:\oracle\product\21c\oradata\XE\proyecto01' size 100m;

alter session set "_ORACLE_SCRIPT"=true;

create user adan
identified by ucr2025
default tablespace proyecto
temporary tablespace temp;

grant connect, resource to adan;

ALTER USER adan QUOTA UNLIMITED ON proyecto;


--Negocio1
create user negocio1
identified by ucr2025
default tablespace proyecto
temporary tablespace temp;

grant connect, resource to negocio1;

ALTER USER negocio1 QUOTA UNLIMITED ON proyecto;


--negocio2
create user negocio2
identified by ucr2025
default tablespace proyecto
temporary tablespace temp;

grant connect, resource to negocio2;

ALTER USER negocio2 QUOTA UNLIMITED ON proyecto;


INSERT INTO Consumidor (idConsumidor, nombreConsumidor, correoElectronico, fechaRegistro, puntosFidelidad, direccion, frecuenciaCompra) VALUES
(1, 'Laura González', 'laura.gonzalez@email.com', TO_DATE('2023-01-15', 'YYYY-MM-DD'), 120, 'San José, Costa Rica', 'Ocasional');
commit;
--borrado de las tablas del modulo de seguridad
drop table bitacoraAcceso;
drop table ventanausuario;
drop table usuarioRol;
drop table bitacoraSeguridad;
drop table usuario;
drop table ventanaRol;
drop table rol;
drop table ventana;
drop table Permisos;

-- Tabla MarcaProducto
CREATE TABLE MarcaProducto (
    nombreProducto VARCHAR2(250) PRIMARY KEY,
    marca VARCHAR2(250)
);

-- Tabla Cosmetico
CREATE TABLE Cosmetico (
    idCosmetico INT PRIMARY KEY,
    nombreProducto VARCHAR2(250),
    precioUnitario NUMBER,
    stockDisponible INT,
    fechaVencimiento DATE,
    categoria VARCHAR2(250),            
    estadoProducto VARCHAR2(250),       
    imagen VARCHAR2(250),               
    FOREIGN KEY (nombreProducto) REFERENCES MarcaProducto(nombreProducto)
);


-- Tabla Compra
CREATE TABLE Compra (
    idCompra INT PRIMARY KEY,
    fechaCompra DATE,
    metodoPago VARCHAR2(250),
    proveedor VARCHAR2(250),
    estado VARCHAR2(50),
    totalCompra NUMBER
);

-- Tabla CosmeticoCompra
CREATE TABLE CosmeticoCompra (
    idCompra INT,
    idCosmetico INT,
    cantidadProducto INT,
    PRIMARY KEY (idCompra, idCosmetico),
    FOREIGN KEY (idCompra) REFERENCES Compra(idCompra),
    FOREIGN KEY (idCosmetico) REFERENCES Cosmetico(idCosmetico)
);

-- Tabla Venta
CREATE TABLE Venta (
    idVenta INT PRIMARY KEY,
    fechaVenta DATE,
    metodoPago VARCHAR2(250),
    puntosUsados NUMBER,
    idConsumidor INT,
    estadoVentas VARCHAR2(50),
    totalVentas NUMBER,
    FOREIGN KEY (idConsumidor) REFERENCES Consumidor(idConsumidor)
);

-- Tabla CosmeticoVenta
CREATE TABLE CosmeticoVenta (
    idVenta INT,
    idCosmetico INT,
    cantidadVendido INT,
    PRIMARY KEY (idVenta, idCosmetico),
    FOREIGN KEY (idVenta) REFERENCES Venta(idVenta),
    FOREIGN KEY (idCosmetico) REFERENCES Cosmetico(idCosmetico)
);

-- Tabla Consumidor
CREATE TABLE Consumidor (
    idConsumidor INT PRIMARY KEY,
    nombreConsumidor VARCHAR2(250),
    correoElectronico VARCHAR2(250),
    fechaRegistro DATE,
    puntosFidelidad NUMBER,
    direccion VARCHAR2(500),
    frecuenciaCompra VARCHAR2(250)      
);

-- Tabla TelefonoConsumidor
CREATE TABLE TelefonoConsumidor (
    idConsumidor INT,
    telefono NUMBER,
    PRIMARY KEY (idConsumidor, telefono),
    FOREIGN KEY (idConsumidor) REFERENCES Consumidor(idConsumidor)
);

----------------------------------------------------------------------------modulo de seguridad
-- Tabla de Usuarios del Sistema
CREATE TABLE Usuario (
    NombreUsuario varchar(250) PRIMARY KEY,
    clave VARCHAR2(250),
    status VARCHAR2(50)
);

-- Tabla de Roles
CREATE TABLE Rol (
    idRol INT PRIMARY KEY,
    nombreRol VARCHAR2(250),
    status VARCHAR2(50)
);

-- Tabla intermedia: Usuario-Rol
CREATE TABLE UsuarioRol (
    NombreUsuario varchar(250),
    idRol INT,
    PRIMARY KEY (NombreUsuario, idRol),
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
    FOREIGN KEY (idRol) REFERENCES Rol(idRol)
);

-- Tabla de Permisos (Insertar, Modificar, Eliminar, Ver)
CREATE TABLE Permisos (
    idPermisos INT PRIMARY KEY,
    nombrePermiso VARCHAR2(50)
);

-- Tabla de Ventanas o M�dulos
CREATE TABLE Ventana (
    idVentana INT PRIMARY KEY,
    nombreVentana VARCHAR2(250)
);

alter table ventana add status varchar(50);

-- Tabla intermedia: Ventanas asignadas a Roles
CREATE TABLE VentanaRol (
    idVentana INT,
    idRol INT,
    idPermisos INT,
    PRIMARY KEY (idVentana, idRol, idPermisos),
    FOREIGN KEY (idVentana) REFERENCES Ventana(idVentana),
    FOREIGN KEY (idRol) REFERENCES Rol(idRol),
    FOREIGN KEY (idPermisos) REFERENCES Permisos(idPermisos)
);

-- Tabla intermedia: Ventanas asignadas a Usuarios
CREATE TABLE VentanaUsuario (
    idVentana INT,
    NombreUsuario varchar(250),
    idPermisos INT,
    PRIMARY KEY (idVentana, NombreUsuario, idPermisos),
    FOREIGN KEY (idVentana) REFERENCES Ventana(idVentana),
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
    FOREIGN KEY (idPermisos) REFERENCES Permisos(idPermisos)
);

-- Bit�cora de Seguridad (CRUD sobre entidades cr�ticas)
CREATE TABLE BitacoraSeguridad (
    idBitacoraSeguridad INT PRIMARY KEY,
    NombreUsuario varchar(250),
    fechaHora DATE,
    accion VARCHAR2(50),
    tablaAfectada VARCHAR2(50),
    descripcion VARCHAR2(500),
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario)
);

-- Bit�cora de Accesos (inicio/cierre de sesi�n)
CREATE TABLE BitacoraAcceso (
    idBitacoraAcceso INT PRIMARY KEY,
    NombreUsuario varchar(250),
    fechaHora DATE,
    FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario)
);

--procedimiento de insercion de datos de cosmeticos
create or replace procedure Sp_Ins_Cosmeticos (
    pNombre in varchar2,
    pMarca in varchar2,
    pPrecioUnitario in number,
    pStockDisponible in number,
    pFechaVencimiento in date,
    pCategoria in varchar2,
    pEstadoProducto in varchar2,
    pImagen in varchar2
)
is
    pIdCosmetico int;
    pExisteNombre int;
begin
    select NVL(max(idCosmetico), 0) + 1 into pIdCosmetico
    from cosmetico;

    
    select count(*) into pExisteNombre 
    from marcaProducto
    where nombreproducto = pNombre;
    
    if pExisteNombre = 0 then
        insert into MarcaProducto values (pNombre, pMarca);
    end if;
    insert into Cosmetico values (pIdCosmetico, pNombre, pPrecioUnitario, pStockDisponible, pFechaVencimiento, pCategoria, pEstadoProducto, pImagen);
    DBMS_OUTPUT.PUT_LINE('Cosmetico insertado correctamente');

    commit;
    
exception
    when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
        rollback;
end;

select *
from cosmetico;



--Procedimiento de modificacion de cosmeticos
create or replace procedure Sp_Upd_Cosmeticos (
    pIdCosmetico in number,
    pNombre in varchar2,
    pMarca in varchar2,
    pPrecioUnitario in number,
    pStockDisponible in number,
    pFechaVencimiento in date,
    pCategoria in varchar2,
    pEstadoProducto in varchar2,
    pImagen in varchar2
)
is
begin
    
    update Cosmetico set NombreProducto = pNombre, 
                    PrecioUnitario = pPrecioUnitario, 
                    StockDisponible = pStockDisponible, 
                    FechaVencimiento = pFechaVencimiento, 
                    Categoria = pCategoria, 
                    EstadoProducto = pEstadoProducto, 
                    Imagen =pImagen
    where idCosmetico = pIdCosmetico;
    
    update MarcaProducto set Marca = pMarca
    where nombreProducto = pNombre;
     DBMS_OUTPUT.PUT_LINE('Cosmetico modificado correctamente');
    commit;
    
exception
    when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
        rollback;
end;


--Procedimiento de eliminacion de cosmeticos
create or replace procedure Sp_Del_Cosmeticos (
    pIdCosmetico in number
)
is
begin
    
   delete from Cosmetico
   where idCosmetico = pIdCosmetico;
     DBMS_OUTPUT.PUT_LINE('Cosmetico modificado correctamente');
    commit;
    
exception
    when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
        rollback;
end;


--Procedimiento que muestra los cosmeticos por nombre
CREATE OR REPLACE PROCEDURE Sp_Most_Cosmetico (
    pNombre IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
    SELECT 
        c.idCosmetico,
        c.nombreProducto,
        m.marca,
        c.precioUnitario,
        c.stockDisponible,
        c.fechaVencimiento,
        c.categoria,
        c.estadoProducto,
        c.imagen
    FROM Cosmetico c
    JOIN MarcaProducto m ON c.nombreProducto = m.nombreProducto
    WHERE LOWER(c.nombreProducto) LIKE '%' || LOWER(TRIM(pNombre)) || '%'
    ORDER BY c.nombreProducto;
END;



-- Procedimiento que muestra los cosmeticos por id
create or replace procedure Sp_Most_IDCosmetico (
    pIdCosmetico in number,
    pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select *
    from Cosmetico c
    JOIN marcaproducto mp on c.nombreProducto = c.nombreProducto 
    where idCosmetico = pIdCosmetico;
end;

--procedimiento que sirve para mostrar si un cosmetico ya ha sido vendido
CREATE OR REPLACE PROCEDURE Sp_Verificar_CosmeticoVendido(
    pIDCosmetico IN NUMBER,
    pCantidad OUT NUMBER
)
IS
BEGIN
    SELECT COUNT(*) INTO pCantidad
    FROM cosmeticoVenta
    WHERE idCosmetico = pIDCosmetico;
END;

--procedimiento que sirve para mostrar si un cosmetico ya esta pendiente
CREATE OR REPLACE PROCEDURE Sp_Verificar_VentasPendientes(
    pIDCosmetico in number,
    pCantidad OUT NUMBER)
AS
BEGIN
    SELECT COUNT(*) INTO pCantidad
    FROM cosmeticoVenta cv
    JOIN venta v on v.idVenta = cv.idVenta
    WHERE cv.IDCosmetico = pIDCosmetico AND EstadoVentas = 'Pendiente';
END;

CREATE OR REPLACE PROCEDURE Sp_Validar_Uso_Puntos (
    pIDConsumidor IN NUMBER,
    pResultado OUT NUMBER
)
IS
BEGIN
    SELECT CASE 
             WHEN COUNT(*) = 3 THEN 1 
             ELSE 0 
           END
    INTO pResultado
    FROM (
        SELECT metodoPago
        FROM (
            SELECT metodoPago
            FROM Venta
            WHERE idConsumidor = pIDConsumidor 
              AND estadoVentas = 'Completada'
            ORDER BY fechaVenta DESC
        )
        WHERE ROWNUM <= 3
          AND LOWER(metodoPago) NOT LIKE '%puntos%'
    );

EXCEPTION
    WHEN OTHERS THEN
        pResultado := 0;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;



select *
from venta;





--Parte de compras

--procedimiento de insercion de datos de Compras
create or replace procedure Sp_Ins_Compra (
    pFechaCompra in date,
    pTotalCompra in number,
    pMetodoPago in varchar2,
    pProveedor in varchar2,
    pCantidadProductos in number,
    pEstadoCompra in varchar2,
    pIdCosmetico in number
    
)
is
    vExisteCosmetico int;
    vIdCompra int;
begin
    select nvl(max(idCompra), 0) +1 into vIdCompra
    from compra;
    
    select count(*) into vExisteCosmetico
    from Cosmetico 
    where IdCosmetico = pIdCosmetico;

    if vExisteCosmetico != 0 then
    insert into Compra (
            idCompra, fechaCompra, metodoPago, proveedor, estado, totalCompra
        ) values (vIdCompra, pFechaCompra, pMetodoPago, pProveedor, pEstadoCompra, pTotalCompra);
    
    insert INTO CosmeticoCompra (
            idCompra, idCosmetico, cantidadProducto
        ) values (vIdCompra, pIdCosmetico, pCantidadProductos);
    DBMS_OUTPUT.PUT_LINE('compra insertado correctamente');
    
    else 
        DBMS_OUTPUT.PUT_LINE('Error: El cosm�tico con ID ' || pIdCosmetico || ' no existe');
    end if;

    commit;
    
exception
    when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
        rollback;
end;


--procedimiento de modificacion de datos de Compras
create or replace procedure Sp_Upd_Compra (
    pIdCompra in number,
    pFechaCompra in date,
    pTotalCompra in number,
    pMetodoPago in varchar2,
    pProveedor in varchar2,
    pCantidadProductos in number,
    pEstadoCompra in varchar2,
    pIdCosmeticos in number
    
)
is
begin
   
    update Compra set fechaCompra = pFechaCompra, 
                    metodoPago = pMetodoPago,
                    proveedor = pProveedor,
                    estado = pEstadoCompra,
                    totalCompra = pTotalCompra
    where IdCompra = pIdCompra;
    
    update CosmeticoCompra set cantidadProducto = pCantidadProductos
    where idCompra = pIdCompra and IdCosmetico = pIdCosmeticos;
    
        DBMS_OUTPUT.PUT_LINE('Se actualizo correctamente compras');

    commit;
    
exception
    when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
        rollback;
end;

--Procedure for delete buy
create or replace procedure Sp_Del_Compra (
    pIdCompra in number
)
is 
begin 
    delete from CosmeticoCompra 
    where idCompra = pIdCompra;
    
    delete from Compra 
    where idCompra = pIdCompra;
     commit;
    
exception
    when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
        rollback;
end;

--procedure for show buy by id
CREATE OR REPLACE PROCEDURE Sp_Most_IDCompra (
    pIdCompra IN NUMBER,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT 
            c.idCompra,
            c.fechaCompra,
            c.totalCompra,
            c.metodoPago,
            c.proveedor,
            cc.cantidadProducto,
            c.estado,
            cc.idCosmetico
        FROM Compra c
        JOIN CosmeticoCompra cc ON c.idCompra = cc.idCompra
        WHERE c.idCompra = pIdCompra;
END;
commit;

-- procedure for show buy by condition
CREATE OR REPLACE PROCEDURE Sp_Most_EstadoCompra (
    pEstadoCompra IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT * FROM Compra
        WHERE LOWER(estado) LIKE '%' || LOWER(TRIM(pEstadoCompra)) || '%'
        ORDER BY estado;
END;





--PROCEDIMIENTOS DE VENTAS

-- procedure for insert sale
CREATE OR REPLACE PROCEDURE Sp_Ins_Venta (
    pFechaVenta in date,
    pTotalVenta in number,
    pMetodoPago in varchar2,
    pPuntosUsados in number,
    pEstadoVenta in varchar2,
    pCantidadVendido in number,
    pIdCosmetico in number,
    pIdConsumidor in number
)
IS
    vIdVenta number;
    vCosmeticoExiste number;
    vConsumidorExiste number;
BEGIN
    SELECT NVL(MAX(idVenta), 0) + 1 INTO vIdVenta FROM Venta;

    SELECT COUNT(*) INTO vCosmeticoExiste FROM Cosmetico WHERE idCosmetico = pIdCosmetico;
    SELECT COUNT(*) INTO vConsumidorExiste FROM Consumidor WHERE idConsumidor = pIdConsumidor;

    IF vCosmeticoExiste = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Error: cosm�tico no existe.');
        RETURN;
    elsif vConsumidorExiste = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Error: consumidor no existe.');
        RETURN;
    end if;

    insert into Venta (
        idVenta, fechaVenta, metodoPago, puntosUsados,
        idConsumidor, estadoVentas, totalVentas
    ) values (
        vIdVenta, pFechaVenta, pMetodoPago, pPuntosUsados,
        pIdConsumidor, pEstadoVenta, pTotalVenta
    );

    insert into CosmeticoVenta (
        idVenta, idCosmetico, cantidadVendido
    ) values (
        vIdVenta, pIdCosmetico, pCantidadVendido
    );

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Venta insertada correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;


--procedure for update sale
CREATE OR REPLACE PROCEDURE Sp_Upd_Venta (
    pIdVenta in number,
    pFechaVenta in date,
    pTotalVenta in number,
    pMetodoPago in varchar2,
    pPuntosUsados in number,
    pEstadoVenta in varchar2,
    pCantidadVendido in number,
    pIdCosmetico in number,
    pIdConsumidor in number
)
IS
BEGIN
    update Venta set
        fechaVenta = pFechaVenta,
        metodoPago = pMetodoPago,
        puntosUsados = pPuntosUsados,
        estadoVentas = pEstadoVenta,
        totalVentas = pTotalVenta,
        idConsumidor = pIdConsumidor
    where idVenta = pIdVenta;

    update CosmeticoVenta set
        cantidadVendido = pCantidadVendido
    where idVenta = pIdVenta and idCosmetico = pIdCosmetico;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Venta actualizada correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

-- procedure for delete sale
CREATE OR REPLACE PROCEDURE Sp_Del_Venta (
    pIdVenta in number
)
IS
BEGIN
    delete from CosmeticoVenta WHERE idVenta = pIdVenta;
    delete from Venta WHERE idVenta = pIdVenta;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Venta eliminada correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


-- procedure for show sale by condition
create or replace procedure Sp_Most_EstadoVenta (
    pEstadoVenta in varchar2,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
        select v.Idventa,fechaVenta,metodoPago,puntosUsados,IdConsumidor,EstadoVentas,totalVentas,idCosmetico,cantidadVendido 
        from Venta v
        JOIN cosmeticoVenta cv on cv.idVenta=v.idVenta
        where lower(estadoVentas) like '%' || lower(trim(pEstadoVenta)) || '%'
        order by estadoVentas;
end;

-- procedure for show sale by id
create or replace procedure Sp_Most_IDVenta (
    pIdVenta in number,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
        select v.Idventa,fechaVenta,metodoPago,puntosUsados,IdConsumidor,EstadoVentas,totalVentas,idCosmetico,cantidadVendido from Venta v
        JOIN cosmeticoVenta cv on cv.idVenta=v.idVenta
        where v.idVenta = pIdVenta;
end;


CREATE OR REPLACE PROCEDURE Sp_Obtener_IDCosmetico (
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT idCosmetico FROM Cosmetico;
END;

CREATE OR REPLACE PROCEDURE Sp_Obtener_IDConsumidores (
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT idConsumidor FROM Consumidor
        ORDER BY idConsumidor;
END;










--PROCEDIMIENTOS DE LOS CONSUMIDORES 
-- procedure for insert consumer
create or replace procedure Sp_Ins_Consumidor (
    pNombreCompleto in varchar2,
    pTelefono in number,
    pCorreoElectronico in varchar2,
    pFechaRegistro in date,
    pFrecuenciaCompra in varchar2,
    pPuntosFidelidad in number,
    pDireccion in varchar2
)
is
    vIdConsumidor number;
begin
    select nvl(max(idConsumidor),0 ) +1 into vIdConsumidor
    from Consumidor;
    
    insert into Consumidor (
        idConsumidor, nombreConsumidor, correoElectronico,
        fechaRegistro, puntosFidelidad, direccion, frecuenciaCompra
    ) VALUES (
        vIdConsumidor, pNombreCompleto, pCorreoElectronico,
        pFechaRegistro, pPuntosFidelidad, pDireccion, pFrecuenciaCompra
    );

    INSERT INTO TelefonoConsumidor (
        idConsumidor, telefono
    ) VALUES (
        vIdConsumidor, pTelefono
    );

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Consumidor insertado correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;



-- procedure for update consumer
create or replace procedure Sp_Upd_Consumidor (
    pIdConsumidor in number,
    pNombreCompleto in varchar2,
    pTelefono in number,
    pCorreoElectronico in varchar2,
    pFechaRegistro in date,
    pFrecuenciaCompra in varchar2,
    pPuntosFidelidad in number,
    pDireccion in varchar2
)
is
begin
    update Consumidor set
        nombreConsumidor = pNombreCompleto,
        correoElectronico = pCorreoElectronico,
        fechaRegistro = pFechaRegistro,
        frecuenciaCompra = pFrecuenciaCompra,
        puntosFidelidad = pPuntosFidelidad,
        direccion = pDireccion
    where idConsumidor = pIdConsumidor;

    update TelefonoConsumidor set
        telefono = pTelefono
    where idConsumidor = pIdConsumidor;

    commit;
    DBMS_OUTPUT.PUT_LINE('Consumidor actualizado correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


-- procedure for delete consumer
create or replace procedure Sp_Del_Consumidor (
    pIdConsumidor in number
)
is
begin
    delete from TelefonoConsumidor where idConsumidor = pIdConsumidor;
    delete from Consumidor where idConsumidor = pIdConsumidor;

    commit;
    DBMS_OUTPUT.PUT_LINE('Consumidor eliminado correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--este procedimiento es para mostrar los datos de la tabla
create or replace procedure Sp_Most_ConsumidorNombre (
    pNombre in varchar2,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
        select * from Consumidor
        where lower(nombreConsumidor) like '%' || lower(trim(pNombre)) || '%'
        order by nombreConsumidor;
end;


create or replace procedure Sp_Most_IDConsumidor (
    pIdConsumidor in number,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
        select * from Consumidor
        where idConsumidor = pIdConsumidor;
end;
--este procedimiento es para recolectar todos los datos y poder cargarlos al momento de querer modificar a un dato.
CREATE OR REPLACE PROCEDURE Sp_Most_NombreConsumidor(
    pNombreCompleto IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT c.idconsumidor,nombreConsumidor,telefono,correoElectronico,fecharegistro,frecuenciaCompra,puntosFidelidad,Direccion 
        FROM consumidor c
        JOIN telefonoConsumidor tc On tc.idConsumidor = c.idConsumidor
        WHERE nombreconsumidor = pNombreCompleto;
END;

CREATE OR REPLACE PROCEDURE Sp_Most_VentaConsumidor (
    pIDConsumidor IN NUMBER,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT *
        FROM Venta
        WHERE idConsumidor = pIDConsumidor
        ORDER BY fechaVenta; -- o el campo que prefieras
END;



-------------------------------------------------------------------parte hecha por sebas



--PART OF SECURITY

--procedure for insert user
create or replace procedure Sp_Ins_Usuario (
    pNombreUsuario in varchar2,
    pClave in varchar2,
    pstatus in varchar2)
is
    nombreUsuarioExiste number;
begin 
    select count(nombreUsuario) into nombreUsuarioExiste
    from usuario
    where nombreUsuario = pNombreUsuario;
    
    if nombreUsuarioExiste < 1 then
        insert into usuario (nombreUsuario,clave,status)
        values (pNombreUsuario,pClave,pstatus);
        

    end if;

    commit;
    
exception when others then
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
    rollback;
end;


--procedure for delete user
CREATE OR REPLACE PROCEDURE Sp_Del_Usuario(
    pNombreUsuario in varchar2,
    pUsuarioActual in varchar2
)
IS
BEGIN

    update Usuario 
    set status = 'inactivo'
    WHERE NombreUsuario = pNombreUsuario;
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Usuario eliminada correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--procedure for update user

CREATE OR REPLACE PROCEDURE Sp_Upd_Usuario(
    pClave IN VARCHAR2,
    pStatus IN VARCHAR2,
    pnombreUsuarioModificado IN VARCHAR2,
    mensaje OUT VARCHAR2
)
IS
    pExisteUsuario INT;
    claveVieja     VARCHAR2(250);
    statusViejo    VARCHAR2(50);
BEGIN 
    SELECT COUNT(*) INTO pExisteUsuario
    FROM usuario
    WHERE nombreUsuario = pnombreUsuarioModificado;

    IF pExisteUsuario = 1 THEN
        SELECT clave, status INTO claveVieja, statusViejo
        FROM usuario
        WHERE nombreUsuario = pnombreUsuarioModificado;

        UPDATE Usuario SET
            clave  = pClave,
            status = pStatus
        WHERE nombreUsuario = pnombreUsuarioModificado;

        COMMIT;
        mensaje := 'Guardado correctamente';
    ELSE
        mensaje := 'El usuario modificado no existe';
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        mensaje := 'Error: ' || SQLERRM;
END;



--Procedure for show user
create or replace procedure Sp_Most_Usuario (
    pNombre in varchar2,
        pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select NombreUsuario, status
    from Usuario
    where Lower(NombreUsuario) like '%' || lower(NVL(trim(pNombre), '')) || '%'
    order by NombreUsuario;
EXCEPTION
WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--Procedure for show user by status
create or replace procedure Sp_Most_UsuarioStatus (
    pStatus in varchar2,
        pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select NombreUsuario, status
    from Usuario
    where Lower(Status) like '%' || lower(NVL(trim(pStatus), '')) || '%'
    order by NombreUsuario;
EXCEPTION
WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--Procedure for show UserRol
create or replace procedure Sp_Most_UsuarioRol (
    pNombre in varchar2,
        pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select ur.idRol, r.nombreRol
    from UsuarioRol ur
    join Rol r on r.idRol = ur.idRol 
    where Lower(ur.NombreUsuario) like '%' || lower(NVL(trim(pNombre), '')) || '%';
EXCEPTION
WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--Procedure for show ScreenUser
create or replace procedure Sp_Most_VentanaUsuario (
    pNombre in varchar2,
        pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select v.NombreVentana, p.NombrePermiso
    from VentanaUsuario vu
    join Ventana v on v.idVentana = vu.idVentana
    join Permisos p on p.idPermisos = vu.idPermisos
    where Lower(vu.NombreUsuario) like '%' || lower(NVL(trim(pNombre), '')) || '%';
EXCEPTION
WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


--Procedure for insert screenUser
create or replace procedure Sp_Ins_UsuarioRol (
    pNombreUsuario in varchar2,
    pIdRol in number
)
is
begin
    insert into UsuarioRol (nombreUsuario, idRol)
    values (pNombreUsuario, pIdRol);

    commit;
    DBMS_OUTPUT.PUT_LINE('Rol asignado al usuario correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


--procedure for insert screenUser
Create or replace procedure Sp_Ins_VentanaUsuario (
    pNombreUsuario in varchar2,
    pIdVentana in number,
    pIdPermiso in number
)
is
begin
    insert into VentanaUsuario (nombreUsuario, idVentana, idPermisos)
    values (pNombreUsuario, pIdVentana, pIdPermiso);

    commit;
    DBMS_OUTPUT.PUT_LINE('Permiso asignado al usuario en la ventana');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--Procedure for delete sceenUser
create or replace procedure  Sp_Del_VentanaUsuario (
    pNombreUsuario in varchar2,
    pIdVentana in number,
    pIdPermiso in number
)
is
begin
    delete from VentanaUsuario
    where nombreUsuario = pNombreUsuario
      and idVentana = pIdVentana
      and idPermisos = pIdPermiso;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Permiso de usuario en ventana eliminado');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;









--PART OF ROLE

--procedure for insert role
create or replace procedure Sp_Ins_Rol (
    pNombreRol in varchar2,
    pStatus in varchar2,
    pUsuarioActual in varchar2
)
is
    vExiste number;
    vIdRol number;
begin
    -- Verificar si ya existe el nombre del rol
    select count(*) into vExiste
    from Rol
    where lower(nombreRol) = lower(pNombreRol);

    IF vExiste = 0 THEN
        -- Obtener nuevo ID
        select nvl(max(idRol), 0) + 1 into vIdRol from Rol;

        insert into Rol (idRol, nombreRol, status)
        values (vIdRol, pNombreRol, pStatus);



        COMMIT;
        DBMS_OUTPUT.PUT_LINE('Rol insertado correctamente');
    else
        DBMS_OUTPUT.PUT_LINE('El rol ya existe');
    end if;

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


--procedure for update rol
create or replace procedure Sp_Upd_Rol (
    pIdRol in number,
    pNuevoNombreRol in varchar2,
    pNuevoStatus in varchar2,
    pUsuarioActual in varchar2
)
is
    vNombreAntiguo varchar2(250);
    vStatusAntiguo varchar2(50);
    vIdBitacora number;
begin
    -- Guardar valores anteriores
    select nombreRol, status into vNombreAntiguo, vStatusAntiguo
    from Rol
    where idRol = pIdRol;

    -- Actualizar valores
    update Rol set
        nombreRol = pNuevoNombreRol,
        status = pNuevoStatus
    where idRol = pIdRol;

    -- Bit�cora
   select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;


    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Rol actualizado correctamente');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


--Procedure for show rol
create or replace procedure Sp_Most_Rol (
    pNombre in varchar2,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
    select idRol, nombreRol, status
        from Rol
        where lower(nombreRol) LIKE '%' || lower(NVL(TRIM(pNombre), '')) || '%'
        ORDER BY nombreRol;

exception
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


--Procedure for delete rol
CREATE OR REPLACE PROCEDURE Sp_Del_Rol (
    pIdRol IN NUMBER,
    pUsuarioActual IN VARCHAR2
)
IS
    vIdBitacora NUMBER;
BEGIN

    -- actualiza su estado para inactivar el rol y dejarlo con un soft delete
    UPDATE Rol
    SET status = 'Inactive'
    WHERE idRol = pIdRol;

    -- Registrar en bitácora
    SELECT NVL(MAX(idBitacoraSeguridad), 0) + 1
    INTO vIdBitacora
    FROM BitacoraSeguridad;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Rol marcado como inactivo correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;



create or replace procedure Sp_Ins_VentanaRol (
    pIdRol in number,
    pIdVentana in number,
    pIdPermiso in number
)
is
begin
    insert into VentanaRol (idRol, idVentana, idPermisos)
    values (pIdRol, pIdVentana, pIdPermiso);

    commit;
    DBMS_OUTPUT.PUT_LINE('Permiso asignado al rol en la ventana');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


--Procedure for show screenRol
create or replace procedure Sp_Most_VentanaRol (
    pIdRol in number,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
        select v.nombreVentana, p.nombrePermiso
        from VentanaRol vr
        JOIN Ventana v ON vr.idVentana = v.idVentana
        JOIN Permisos p ON vr.idPermisos = p.idPermisos
        where vr.idRol = pIdRol;

exception
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--Procedure for delete screenRol
create or replace procedure Sp_Del_UsuarioRol (
    pNombreUsuario in varchar2,
    pIdRol in number
)
is
begin
    delete from UsuarioRol
    where nombreUsuario = pNombreUsuario and idRol = pIdRol;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Rol removido del usuario');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

--Procedure for delete screenRol
create or replace procedure Sp_Del_VentanaRol (
    pIdRol in number,
    pIdVentana in number,
    pIdPermiso in number
)
is
begin
    DELETE FROM VentanaRol
    WHERE idRol = pIdRol
      AND idVentana = pIdVentana
      AND idPermisos = pIdPermiso;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Permiso del rol en ventana eliminado');

exception
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;


create or replace procedure Sp_Most_RolesNoAsignados (
    pNombre in varchar2,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
    select r.idRol, r.nombreRol
    from Rol r
    where r.status = 'activo'
      and not exists (
          select 1 from UsuarioRol ur
          where ur.idRol = r.idRol
            and lower(ur.NombreUsuario) = lower(trim(pNombre))
      );
exception
    when others then
        dbms_output.put_line('Error: ' || sqlerrm);
end;

--Procedimiento para las ventanas
CREATE OR REPLACE PROCEDURE Sp_Ins_Ventana (
    pNombreVentana IN VARCHAR2,
    pStatus IN VARCHAR2,
    pUsuarioActual IN VARCHAR2
)
IS
    vExiste NUMBER;
    vIdVentana NUMBER;
BEGIN
    -- Verificar si ya existe el nombre de la ventana
    SELECT COUNT(*) INTO vExiste
    FROM Ventana
    WHERE LOWER(nombreVentana) = LOWER(pNombreVentana);

    IF vExiste = 0 THEN
        -- Obtener nuevo ID
        SELECT NVL(MAX(idVentana), 0) + 1 INTO vIdVentana FROM Ventana;

        INSERT INTO Ventana (idVentana, nombreVentana, status)
        VALUES (vIdVentana, pNombreVentana, pStatus);

        COMMIT;
        DBMS_OUTPUT.PUT_LINE('Ventana insertada correctamente');
    ELSE
        DBMS_OUTPUT.PUT_LINE('La ventana ya existe');
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

CREATE OR REPLACE PROCEDURE Sp_Upd_Ventana (
    pIdVentana IN NUMBER,
    pNuevoNombreVentana IN VARCHAR2,
    pNuevoStatus IN VARCHAR2,
    pUsuarioActual IN VARCHAR2
)
IS
    vNombreAntiguo VARCHAR2(250);
    vStatusAntiguo VARCHAR2(50);
BEGIN
    -- Guardar valores anteriores
    SELECT nombreVentana, status INTO vNombreAntiguo, vStatusAntiguo
    FROM Ventana
    WHERE idVentana = pIdVentana;

    -- Actualizar valores
    UPDATE Ventana
    SET nombreVentana = pNuevoNombreVentana,
        status = pNuevoStatus
    WHERE idVentana = pIdVentana;


    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Ventana actualizada correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

CREATE OR REPLACE PROCEDURE Sp_Most_Ventana (
    pNombre IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT idVentana, nombreVentana, status
        FROM Ventana
        WHERE LOWER(nombreVentana) LIKE '%' || LOWER(NVL(TRIM(pNombre), '')) || '%'
        ORDER BY nombreVentana;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

CREATE OR REPLACE PROCEDURE Sp_Del_Ventana (
    pIdVentana IN NUMBER,
    pUsuarioActual IN VARCHAR2
)
IS
BEGIN
    -- Soft delete: cambiar estado
    UPDATE Ventana
    SET status = 'Inactive'
    WHERE idVentana = pIdVentana;


    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Ventana marcada como inactiva correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;




--procedure for login control

CREATE OR REPLACE PROCEDURE Sp_Login ( --hecho -- me gustaria que diferencie entre si lo que fallo fue solo la clave o o el nombre y la clave 
    pNombreusuario IN VARCHAR2,
    pClave         IN VARCHAR2,
    mensaje        OUT VARCHAR2
)
IS
    idBitacora NUMBER;
    vnombre    VARCHAR2(250);
    vclave     VARCHAR2(250);
    vstatus    VARCHAR2(50);
BEGIN
    BEGIN
        SELECT nombreusuario, clave, status
        INTO vnombre, vclave, vstatus
        FROM usuario
        WHERE nombreUsuario = pNombreUsuario;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            mensaje := 'El usuario no existe';
            RETURN;
    END;

    IF vclave = pClave THEN
        IF LOWER(vstatus) = 'activo' THEN
            SELECT NVL(MAX(idbitacoraAcceso), 0) + 1
            INTO idBitacora
            FROM BitacoraAcceso;

            INSERT INTO BitacoraAcceso (idBitacoraAcceso, nombreUsuario, fechaHora)
            VALUES (idBitacora, pNombreUsuario, SYSDATE);
            COMMIT;
            mensaje := 'Acceso permitido';
        ELSE
            mensaje := 'El status del perfil es inactivo';
        END IF;
    ELSE
        mensaje := 'Contraseña inválida';
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        mensaje := 'Error inesperado: ' || SQLERRM;
END;










--TRIGGERS 



--TRIGGER DE APLICACION

--Trigger marcaProducto
create or replace trigger trg_bitacora_marcaproducto
after insert or update or delete on MarcaProducto
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� en producto: ' || :new.nombreProducto || ' marca: ' || :new.marca;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� producto: ' || :old.nombreProducto || ' marca: ' || :old.marca
        || '/ a producto: ' || :new.nombreProducto || ' marca: ' || :new.marca;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino producto: ' || :old.nombreProducto || ' marca: ' || :old.marca;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'MarcaProducto',
        vDescripcion
    );
end;


--Trigger cosmetico
create or replace trigger trg_bitacora_cosmetico
after insert or update or delete on Cosmetico
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� en Cosmetico: '|| :new.idCosmetico || '/ nombreProducto: ' || :new.nombreProducto
                    || '/ precioUnitario: '|| :new.precioUnitario || '/ StockDisponible: ' || :new.StockDisponible
                    || '/ FechaVencimiento: '|| :new.FechaVencimiento || '/ Categoria: ' || :new.Categoria
                    || '/ EstadoProducto: '|| :new.estadoProducto || '/ imagen: ' || :new.imagen;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� un registro en Cosmetico: ' || :old.idCosmetico || '/ nombreProducto: ' || :old.nombreProducto
                    || '/ precioUnitario: '|| :old.precioUnitario || '/ StockDisponible: ' || :old.StockDisponible
                    || '/ FechaVencimiento: '|| :old.FechaVencimiento || '/ Categoria: ' || :old.Categoria
                    || '/ EstadoProducto: '|| :old.estadoProducto || '/ imagen: ' || :old.imagen
                    || ' / / a Cosmetico: ' || :new.idCosmetico || '/ nombreProducto: ' || :new.nombreProducto
                    || '/ precioUnitario: '|| :new.precioUnitario || '/ StockDisponible: ' || :new.StockDisponible
                    || '/ FechaVencimiento: '|| :new.FechaVencimiento || '/ Categoria: ' || :new.Categoria
                    || '/ EstadoProducto: '|| :new.estadoProducto || '/ imagen: ' || :new.imagen;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un registro en Cosmetico: ' || :old.idCosmetico || '/ nombreProducto: ' || :old.nombreProducto
                    || '/ precioUnitario: '|| :old.precioUnitario || '/ StockDisponible: ' || :old.StockDisponible
                    || '/ FechaVencimiento: '|| :old.FechaVencimiento || '/ Categoria: ' || :old.Categoria
                    || '/ EstadoProducto: '|| :old.estadoProducto || '/ imagen: ' || :old.imagen;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Cosmetico',
        vDescripcion
    );
end;



--Trigger for buy
create or replace trigger trg_bitacora_compra
after insert or update or delete on Compra
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� en compra: '|| :new.idCompra || '/ FechaCompra: ' || :new.FechaCompra
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ Proveedor: ' || :new.Proveedor
                    || '/ Estado: '|| :new.Estado || '/ TotalCompra: ' || :new.TotalCompra;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� un registro en Compra' || :old.idCompra || '/ FechaCompra: ' || :old.FechaCompra
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ Proveedor: ' || :old.Proveedor
                    || '/ Estado: '|| :old.Estado || '/no TotalCompra: ' || :old.TotalCompra
                    || 'a compra: '|| :new.idCompra || '/ FechaCompra: ' || :new.FechaCompra
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ Proveedor: ' || :new.Proveedor
                    || '/ Estado: '|| :new.Estado || '/ TotalCompra: ' || :new.TotalCompra;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un registro en Compra' || :old.idCompra || '/ FechaCompra: ' || :old.FechaCompra
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ Proveedor: ' || :old.Proveedor
                    || '/ Estado: '|| :old.Estado || '/ TotalCompra: ' || :old.TotalCompra;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Compra',
        vDescripcion
    );
end;



--trigger for cosmeticoCompra
create or replace trigger trg_bitacora_cosmeticocompra
after insert or update or delete on CosmeticoCompra
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� en CosmeticoCompra: / idCompra:' || :new.idCompra || '/ idCosmetico: ' || :new.IDCOSMETICO
                    || '/ cantidadProducto: '|| :new.cantidadProducto;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� un registro en CosmeticoCompra: / idCompra:' || :old.idCompra || '/ idCosmetico: ' || :old.idCosmetico
                    || '/ cantidadProducto: '|| :old.cantidadProducto || 
                    'a un CosmeticoCompra: / idCompra:' || :new.idCompra || '/ idCosmetico: ' || :new.IDCOSMETICO
                    || '/ cantidadProducto: '|| :new.cantidadProducto;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� un registro en CosmeticoCompra: / idCompra:' || :old.idCompra || '/ idCosmetico: ' || :old.IDCOSMETICO
                    || '/ cantidadProducto: '|| :old.cantidadProducto;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'CosmeticoCompra',
        vDescripcion
    );
end;



--Trigger for sale
create or replace trigger trg_bitacora_venta
after insert or update or delete on Venta
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� en Venta: '  || :new.idVenta || '/ fechaVenta: ' || :new.FechaVenta
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ PuntosUsados: '|| :new.PuntosUsados
                    || '/ IdConsumior: '|| :new.idConsumidor || '/ EstadoVentas: '|| :new.EstadoVentas
                    || '/ TotalVentas: '|| :new.TotalVentas;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� un registro en Venta: '  || :old.idVenta || '/ fechaVenta: ' || :old.FechaVenta
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ PuntosUsados: '|| :old.PuntosUsados
                    || '/ IdConsumior: '|| :old.idConsumidor || '/ EstadoVentas: '|| :old.EstadoVentas
                    || '/ TotalVentas: '|| :old.TotalVentas
                    || 'a en Venta: '  || :new.idVenta || '/ fechaVenta: ' || :new.FechaVenta
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ PuntosUsados: '|| :new.PuntosUsados
                    || '/ IdConsumior: '|| :new.idConsumidor || '/ EstadoVentas: '|| :new.EstadoVentas
                    || '/ TotalVentas: '|| :new.TotalVentas;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un registro en Venta: '  || :old.idVenta || '/ fechaVenta: ' || :old.FechaVenta
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ PuntosUsados: '|| :old.PuntosUsados
                    || '/ IdConsumior: '|| :old.idConsumidor || '/ EstadoVentas: '|| :old.EstadoVentas
                    || '/ TotalVentas: '|| :old.TotalVentas;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Venta',
        vDescripcion
    );
end;



--trigger for cosmeticoVenta
create or replace trigger trg_bitacora_cosmeticoventa
after insert or update or delete on CosmeticoVenta
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� en CosmeticoVenta: /  idVenta' || :new.idVenta || '/ idCosmetico: ' || :new.idCosmetico
                    || '/ CantidadVendido: '|| :new.CantidadVendido;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� un registro en CosmeticoVenta: /  idVenta' || :old.idVenta || '/ idCosmetico: ' || :old.idCosmetico
                    || '/ CantidadVendido: '|| :old.CantidadVendido ||
                    'a CosmeticoVenta: /  idVenta' || :new.idVenta || '/ idCosmetico: ' || :new.idCosmetico
                    || '/ CantidadVendido: '|| :new.CantidadVendido ;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� un registro en CosmeticoVenta: /  idVenta' || :old.idVenta || '/ idCosmetico: ' || :old.idCosmetico
                    || '/ CantidadVendido: '|| :old.CantidadVendido;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'CosmeticoVenta',
        vDescripcion
    );
end;



--trigger consumidor
create or replace trigger trg_bitacora_consumidor
after insert or update or delete on Consumidor
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� el consumidor: / idConsumidor:' || :new.idConsumidor 
                    || '/ nombre: ' || :new.nombreConsumidor || '/ CorreoElectronico: ' || :new.CorreoElectronico
                    || '/ FechaRegistro: ' || :new.FechaRegistro || '/ PuntosFidelidad: ' || :new.PuntosFidelidad
                    || '/ Direccion: ' || :new.Direccion || '/ FrecuenciaCompra: ' || :new.FrecuenciaCompra;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo el consumidor: / idConsumidor:' || :old.idConsumidor 
                    || '/ nombre: ' || :old.nombreConsumidor || '/ CorreoElectronico: ' || :old.CorreoElectronico
                    || '/ FechaRegistro: ' || :old.FechaRegistro || '/ PuntosFidelidad: ' || :old.PuntosFidelidad
                    || '/ Direccion: ' || :old.Direccion || '/ FrecuenciaCompra: ' || :old.FrecuenciaCompra 
                    || 'a idConsumidor:' || :new.idConsumidor 
                    || '/ nombre: ' || :new.nombreConsumidor || '/ CorreoElectronico: ' || :new.CorreoElectronico
                    || '/ FechaRegistro: ' || :new.FechaRegistro || '/ PuntosFidelidad: ' || :new.PuntosFidelidad
                    || '/ Direccion: ' || :new.Direccion || '/ FrecuenciaCompra: ' || :new.FrecuenciaCompra;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� el consumidor: / idConsumidor:' || :old.idConsumidor 
                    || '/ nombre: ' || :old.nombreConsumidor || '/ CorreoElectronico: ' || :old.CorreoElectronico
                    || '/ FechaRegistro: ' || :old.FechaRegistro || '/ PuntosFidelidad: ' || :old.PuntosFidelidad
                    || '/ Direccion: ' || :old.Direccion || '/ FrecuenciaCompra: ' || :old.FrecuenciaCompra;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Consumidor',
        vDescripcion
    );
end;





--trigger telefonoConsumidor
create or replace trigger trg_bitacora_telefonoconsumidor
after insert or update or delete on TelefonoConsumidor
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� un tel�fono: / idConsumidor:' || :new.idConsumidor || '/ tel�fono: ' || :new.telefono;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� un tel�fono: / idConsumidor:' || :old.idConsumidor || '/ tel�fono viejo: ' || :old.telefono
                     || ' a / tel�fono nuevo: ' || :new.telefono;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� un tel�fono: / idConsumidor:' || :old.idConsumidor || '/ tel�fono: ' || :old.telefono;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'TelefonoConsumidor',
        vDescripcion
    );
end;


--TRIGGER MODULO SEGURIDAD


--trigger users
create or replace trigger trg_bitacora_usuario
after insert or update or delete on Usuario
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    -- Obtener nombre del usuario que ejecut� la acci�n
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� el usuario: ' || :new.nombreUsuario || ' / Status: ' || :new.status;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� el usuario: ' || :old.nombreUsuario || ' / Status: ' || :old.status
                   || 'a usuario: ' || :new.nombreUsuario || ' / Status: ' || :new.status;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� el usuario: ' || :old.nombreUsuario || ' / Status: ' || :old.status;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Usuario',
        vDescripcion
    );
end;


--Trigger for role
create or replace trigger trg_bitacora_rol
after insert or update or delete on Rol
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� el rol: / idRol:' || :new.idRol || '/ nombre: ' || :new.nombreRol
        || '/ status: ' || :new.status;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� el rol: / idRol:' || :old.idRol || '/ nombre anterior: ' || :old.nombreRol
                     || '/ status: ' || :old.status || ' a / idRol:' || :new.idRol || '/ nombre: ' 
                     || :new.nombreRol || '/ status: ' || :new.status;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino el rol: / idRol:' || :old.idRol || '/ nombre anterior: ' || :old.nombreRol
                     || '/ status: ' || :old.status;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Rol',
        vDescripcion
    );
end;



--trigger usuarioRol
create or replace trigger trg_bitacora_usuariorol
after insert or update or delete on UsuarioRol
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto usarioRol: rol: / idRol:' || :new.idRol || ' / usuario: ' || :new.NombreUsuario;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se modific� la asignaci�n de rol para el usuario: ' || :old.NombreUsuario 
                     || ' / idRol: ' || :old.idRol || 'a  / NombreUsuario: ' || :new.NombreUsuario ||'idRol: ' 
                     || :new.idRol;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� el rol: / idRol:' || :old.idRol || ' / usuario: ' || :old.NombreUsuario;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'UsuarioRol',
        vDescripcion
    );
end;



--trigger for permits
create or replace trigger trg_bitacora_permisos
after insert or update or delete on Permisos
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� el permiso: / idPermiso:' || :new.idPermisos || '/ nombre: ' || :new.nombrePermiso;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� el permiso: / idPermiso:' || :old.idPermisos || '/ nombre: ' || :old.nombrePermiso
                     || 'a  / idPermiso:' || :new.idPermisos || '/ nombre: ' || :new.nombrePermiso;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� el permiso: / idPermiso:' || :old.idPermisos || '/ nombre: ' || :old.nombrePermiso;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Permisos',
        vDescripcion
    );
end;


--trigger for ventana
create or replace trigger trg_bitacora_ventana
after insert or update or delete on Ventana
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se insert� la ventana: / idVentana:' || :new.idVentana || '/ nombre: ' || :new.nombreVentana;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� la ventana: / idVentana:' || :old.idVentana || '/ nombre: ' || :old.nombreVentana
                     || 'a / idVentana:' || :new.idVentana || ' / nombre nuevo: ' || :new.nombreVentana;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� la ventana: / idVentana:' || :old.idVentana || '/ nombre: ' || :old.nombreVentana;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Ventana',
        vDescripcion
    );
end;


--trigger for ventanaRol
create or replace trigger trg_bitacora_ventanarol
after insert or update or delete on VentanaRol
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en ventnRol: / idPermisos:' || :new.idPermisos || ' // rol: ' || :new.idRol || ' / ventana: ' || :new.idVentana;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� la asignaci�n en VentanaRol: / idVentana: ' || :old.idVentana || '/ idRol: ' || :old.idRol
        ||' a / idPermisos:' || :new.idPermisos || ' // rol: ' || :new.idRol || ' / ventana: ' || :new.idVentana;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� asignaci�n en VentanaRol: / idVentana:' || :old.idVentana || '/ idRol:' || :old.idRol
                     || '/ idPermisos:' || :old.idPermisos;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'VentanaRol',
        vDescripcion
    );
end;

--trigger for ventanaUsuario
create or replace trigger trg_bitacora_ventanausuario
after insert or update or delete on VentanaUsuario
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se asign� permiso: / idPermisos:' || :new.idPermisos || ' / usuario: ' || :new.NombreUsuario || ' / ventana: ' || :new.idVentana;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz� asignaci�n en VentanaUsuario: / idVentana: ' || :old.idVentana || '/ usuario: ' || :old.NombreUsuario
        ||' / idPermisos:' || :old.idPermisos 
        ||' a / idPermisos:' || :new.idPermisos || ' / usuario: ' || :new.NombreUsuario || ' / ventana: ' || :new.idVentana;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin� asignaci�n en VentanaUsuario: / idVentana:' || :old.idVentana || '/ usuario:' || :old.NombreUsuario 
        || '/ idPermisos:' || :new.idPermisos;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vUsuarioActual,
        sysdate,
        vAccion,
        'VentanaUsuario',
        vDescripcion
    );
end;

