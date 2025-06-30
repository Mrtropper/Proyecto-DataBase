

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


--Procedimientoss





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
/


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

/
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
/

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


/
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
/
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
/
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
/
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


/

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
/

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
/
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
/
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

/
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


/


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

/
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
/
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

/
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
/
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
/

CREATE OR REPLACE PROCEDURE Sp_Obtener_IDCosmetico (
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT idCosmetico FROM Cosmetico;
END;
/
CREATE OR REPLACE PROCEDURE Sp_Obtener_IDConsumidores (
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT idConsumidor FROM Consumidor
        ORDER BY idConsumidor;
END;



/






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


/
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
/

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
/
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

/
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
/
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
/
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

/




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
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en producto: ' || :new.nombreProducto || ' marca: ' || :new.marca;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo producto: ' || :old.nombreProducto || ' marca: ' || :old.marca
        || '/ a producto: ' || :new.nombreProducto || ' marca: ' || :new.marca;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino producto: ' || :old.nombreProducto || ' marca: ' || :old.marca;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'MarcaProducto',
        vDescripcion
    );
end;


/
--Trigger cosmetico
create or replace trigger trg_bitacora_cosmetico
after insert or update or delete on Cosmetico
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en Cosmetico: '|| :new.idCosmetico || '/ nombreProducto: ' || :new.nombreProducto
                    || '/ precioUnitario: '|| :new.precioUnitario || '/ StockDisponible: ' || :new.StockDisponible
                    || '/ FechaVencimiento: '|| :new.FechaVencimiento || '/ Categoria: ' || :new.Categoria
                    || '/ EstadoProducto: '|| :new.estadoProducto || '/ imagen: ' || :new.imagen;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo un registro en Cosmetico: ' || :old.idCosmetico || '/ nombreProducto: ' || :old.nombreProducto
                    || '/ precioUnitario: '|| :old.precioUnitario || '/ StockDisponible: ' || :old.StockDisponible
                    || '/ FechaVencimiento: '|| :old.FechaVencimiento || '/ Categoria: ' || :old.Categoria
                    || '/ EstadoProducto: '|| :old.estadoProducto || '/ imagen: ' || :old.imagen
                    || ' // a Cosmetico: ' || :new.idCosmetico || '/ nombreProducto: ' || :new.nombreProducto
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
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Cosmetico',
        vDescripcion
    );
end;
/


--Trigger for buy
create or replace trigger trg_bitacora_compra
after insert or update or delete on Compra
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en compra: '|| :new.idCompra || '/ FechaCompra: ' || :new.FechaCompra
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ Proveedor: ' || :new.Proveedor
                    || '/ Estado: '|| :new.Estado || '/ TotalCompra: ' || :new.TotalCompra;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo un registro en Compra: ' || :old.idCompra || '/ FechaCompra: ' || :old.FechaCompra
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ Proveedor: ' || :old.Proveedor
                    || '/ Estado: '|| :old.Estado || '/ TotalCompra: ' || :old.TotalCompra
                    || ' // a compra: '|| :new.idCompra || '/ FechaCompra: ' || :new.FechaCompra
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ Proveedor: ' || :new.Proveedor
                    || '/ Estado: '|| :new.Estado || '/ TotalCompra: ' || :new.TotalCompra;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un registro en Compra: ' || :old.idCompra || '/ FechaCompra: ' || :old.FechaCompra
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ Proveedor: ' || :old.Proveedor
                    || '/ Estado: '|| :old.Estado || '/ TotalCompra: ' || :old.TotalCompra;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Compra',
        vDescripcion
    );
end;


/

--trigger for cosmeticoCompra
create or replace trigger trg_bitacora_cosmeticocompra
after insert or update or delete on CosmeticoCompra
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en CosmeticoCompra: / idCompra:' || :new.idCompra || '/ idCosmetico: ' || :new.IDCOSMETICO
                    || '/ cantidadProducto: '|| :new.cantidadProducto;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo un registro en CosmeticoCompra: / idCompra:' || :old.idCompra || '/ idCosmetico: ' || :old.idCosmetico
                    || '/ cantidadProducto: '|| :old.cantidadProducto || 
                    ' a un CosmeticoCompra: / idCompra:' || :new.idCompra || '/ idCosmetico: ' || :new.IDCOSMETICO
                    || '/ cantidadProducto: '|| :new.cantidadProducto;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un registro en CosmeticoCompra: / idCompra:' || :old.idCompra || '/ idCosmetico: ' || :old.IDCOSMETICO
                    || '/ cantidadProducto: '|| :old.cantidadProducto;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'CosmeticoCompra',
        vDescripcion
    );
end;


/

--Trigger for sale
create or replace trigger trg_bitacora_venta
after insert or update or delete on Venta
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en Venta: '  || :new.idVenta || '/ fechaVenta: ' || :new.FechaVenta
                    || '/ MetodoPago: '|| :new.MetodoPago || '/ PuntosUsados: '|| :new.PuntosUsados
                    || '/ IdConsumior: '|| :new.idConsumidor || '/ EstadoVentas: '|| :new.EstadoVentas
                    || '/ TotalVentas: '|| :new.TotalVentas;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo un registro en Venta: '  || :old.idVenta || '/ fechaVenta: ' || :old.FechaVenta
                    || '/ MetodoPago: '|| :old.MetodoPago || '/ PuntosUsados: '|| :old.PuntosUsados
                    || '/ IdConsumior: '|| :old.idConsumidor || '/ EstadoVentas: '|| :old.EstadoVentas
                    || '/ TotalVentas: '|| :old.TotalVentas
                    || ' a en Venta: '  || :new.idVenta || '/ fechaVenta: ' || :new.FechaVenta
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
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Venta',
        vDescripcion
    );
end;

/

--trigger for cosmeticoVenta
create or replace trigger trg_bitacora_cosmeticoventa
after insert or update or delete on CosmeticoVenta
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en CosmeticoVenta: /  idVenta' || :new.idVenta || '/ idCosmetico: ' || :new.idCosmetico
                    || '/ CantidadVendido: '|| :new.CantidadVendido;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo un registro en CosmeticoVenta: /  idVenta' || :old.idVenta || '/ idCosmetico: ' || :old.idCosmetico
                    || '/ CantidadVendido: '|| :old.CantidadVendido ||
                    ' a CosmeticoVenta: /  idVenta' || :new.idVenta || '/ idCosmetico: ' || :new.idCosmetico
                    || '/ CantidadVendido: '|| :new.CantidadVendido ;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un registro en CosmeticoVenta: /  idVenta' || :old.idVenta || '/ idCosmetico: ' || :old.idCosmetico
                    || '/ CantidadVendido: '|| :old.CantidadVendido;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'CosmeticoVenta',
        vDescripcion
    );
end;



/
--trigger consumidor
create or replace trigger trg_bitacora_consumidor
after insert or update or delete on Consumidor
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto el consumidor: / idConsumidor:' || :new.idConsumidor 
                    || '/ nombre: ' || :new.nombreConsumidor || '/ CorreoElectronico: ' || :new.CorreoElectronico
                    || '/ FechaRegistro: ' || :new.FechaRegistro || '/ PuntosFidelidad: ' || :new.PuntosFidelidad
                    || '/ Direccion: ' || :new.Direccion || '/ FrecuenciaCompra: ' || :new.FrecuenciaCompra;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo el consumidor: / idConsumidor:' || :old.idConsumidor 
                    || '/ nombre: ' || :old.nombreConsumidor || '/ CorreoElectronico: ' || :old.CorreoElectronico
                    || '/ FechaRegistro: ' || :old.FechaRegistro || '/ PuntosFidelidad: ' || :old.PuntosFidelidad
                    || '/ Direccion: ' || :old.Direccion || '/ FrecuenciaCompra: ' || :old.FrecuenciaCompra 
                    || ' a idConsumidor:' || :new.idConsumidor 
                    || '/ nombre: ' || :new.nombreConsumidor || '/ CorreoElectronico: ' || :new.CorreoElectronico
                    || '/ FechaRegistro: ' || :new.FechaRegistro || '/ PuntosFidelidad: ' || :new.PuntosFidelidad
                    || '/ Direccion: ' || :new.Direccion || '/ FrecuenciaCompra: ' || :new.FrecuenciaCompra;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino el consumidor: / idConsumidor:' || :old.idConsumidor 
                    || '/ nombre: ' || :old.nombreConsumidor || '/ CorreoElectronico: ' || :old.CorreoElectronico
                    || '/ FechaRegistro: ' || :old.FechaRegistro || '/ PuntosFidelidad: ' || :old.PuntosFidelidad
                    || '/ Direccion: ' || :old.Direccion || '/ FrecuenciaCompra: ' || :old.FrecuenciaCompra;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Consumidor',
        vDescripcion
    );
end;


/



--trigger telefonoConsumidor
create or replace trigger trg_bitacora_telefonoconsumidor
after insert or update or delete on TelefonoConsumidor
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := sys_context('USERENV', 'SESSION_USER');

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto un telefono: / idConsumidor:' || :new.idConsumidor || '/ telefono: ' || :new.telefono;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo un telefono: / idConsumidor:' || :old.idConsumidor || '/ telefono viejo: ' || :old.telefono
                     || ' a / telefono nuevo: ' || :new.telefono;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino un telefono: / idConsumidor:' || :old.idConsumidor || '/ telefono: ' || :old.telefono;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from adan.BitacoraSeguridad;

    insert into adan.BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'TelefonoConsumidor',
        vDescripcion
    );
end;

/
