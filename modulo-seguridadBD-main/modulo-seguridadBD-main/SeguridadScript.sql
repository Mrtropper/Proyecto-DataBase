--Creacion de usuarios
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
grant select, insert on adan.BitacoraSeguridad to negocio1;


ALTER USER negocio1 QUOTA UNLIMITED ON proyecto;


--negocio2
create user negocio2
identified by ucr2025
default tablespace proyecto
temporary tablespace temp;

grant connect, resource to negocio2;
grant select, insert on adan.BitacoraSeguridad to negocio2;

ALTER USER negocio2 QUOTA UNLIMITED ON proyecto;

--PART OF SECURITY


drop table compra;
drop table consumidor;
drop table cosmetico;
drop table cosmeticoCompra;
drop table cosmeticoVenta;
drop table marcaProducto;
drop table telefonoConsumidor;
drop table venta;
-- Eliminar tablas intermedias y dependientes
DROP TABLE VentanaUsuario CASCADE CONSTRAINTS;
DROP TABLE VentanaRol CASCADE CONSTRAINTS;
DROP TABLE UsuarioRol CASCADE CONSTRAINTS;
DROP TABLE BitacoraSeguridad CASCADE CONSTRAINTS;
DROP TABLE BitacoraAcceso CASCADE CONSTRAINTS;
DROP TABLE SistemaUsuario CASCADE CONSTRAINTS;

-- Eliminar tablas principales
DROP TABLE Ventana CASCADE CONSTRAINTS;
DROP TABLE Rol CASCADE CONSTRAINTS;
DROP TABLE Permisos CASCADE CONSTRAINTS;
DROP TABLE Usuario CASCADE CONSTRAINTS;
DROP TABLE sistemas CASCADE CONSTRAINTS;

DROP PROCEDURE SP_CREARUSUARIODEFAULT;
DROP PROCEDURE SP_DEL_ROL;
DROP PROCEDURE SP_DEL_SISTEMAUSUARIO;
DROP PROCEDURE SP_DEL_USUARIO;
DROP PROCEDURE SP_DEL_USUARIOROL;
DROP PROCEDURE SP_DEL_VENTANA;
DROP PROCEDURE SP_DEL_VENTANAROL;
DROP PROCEDURE SP_DEL_VENTANAUSUARIO;
DROP PROCEDURE SP_INS_ROL;
DROP PROCEDURE SP_INS_SISTEMAUSUARIO;
DROP PROCEDURE SP_INS_USUARIO;
DROP PROCEDURE SP_INS_USUARIOROL;
DROP PROCEDURE SP_INS_VENTANA;
DROP PROCEDURE SP_INS_VENTANAROL;
DROP PROCEDURE SP_INS_VENTANAUSUARIO;
DROP PROCEDURE SP_LOGIN;
DROP PROCEDURE SP_MOST_PERMISOS_COMPLETOS_USUARIO;
DROP PROCEDURE SP_MOST_ROL;
DROP PROCEDURE SP_MOST_ROLESNOASIGNADOS;
DROP PROCEDURE SP_MOST_SISTEMAS;
DROP PROCEDURE SP_MOST_SISTEMASUSUARIO;
DROP PROCEDURE SP_MOST_USUARIO;
DROP PROCEDURE SP_MOST_USUARIOIDSISTEMA;
DROP PROCEDURE SP_MOST_USUARIOSTATUS;
DROP PROCEDURE SP_MOST_VENTANA;
DROP PROCEDURE SP_MOST_VENTANANEGOCIO;
DROP PROCEDURE Sp_Most_VentanaPermisosRol;
DROP PROCEDURE SP_MOST_VENTANAROL;
DROP PROCEDURE SP_MOST_VENTANAUSUARIO;
DROP PROCEDURE SP_OBTENERSISTEMAS;
DROP PROCEDURE SP_OBTENERVENTANASNEGOCIO;
DROP PROCEDURE SP_UPD_ROL;
DROP PROCEDURE SP_UPD_USUARIO;
DROP PROCEDURE SP_UPD_VENTANA;
DROP PROCEDURE sp_most_usuarioRol;
DROP PROCEDURE sp_most_usuarioSistema;


--tablas nuevas

CREATE TABLE Sistemas (
    nombreSistema VARCHAR2(50) PRIMARY KEY,
    descripcion VARCHAR2(50)
);

CREATE TABLE Usuario (
    nombreUsuario VARCHAR2(250) primary key,
    clave VARCHAR2(250),
    status VARCHAR2(50)
);

--tabla intermedia
CREATE TABLE SistemaUsuario (
    nombreSistema VARCHAR2(50),
    nombreUsuario VARCHAR2(250),
    PRIMARY KEY (nombreSistema, nombreUsuario),
    FOREIGN KEY (nombreSistema) REFERENCES Sistemas(nombreSistema),
    FOREIGN KEY (nombreUsuario) REFERENCES Usuario(nombreUsuario)
);

CREATE TABLE Rol (
    idRol INT PRIMARY KEY,
    nombreRol VARCHAR2(250),
    status VARCHAR2(50),
    nombreSistema VARCHAR2(50),
    FOREIGN KEY (nombreSistema) REFERENCES Sistemas(nombreSistema)
);

-- Tabla intermedia UsuarioRol
CREATE TABLE UsuarioRol (
    NombreUsuario varchar(250),
    idRol INT,
    PRIMARY KEY (NombreUsuario, idRol),
    FOREIGN KEY (NombreUsuario ) REFERENCES Usuario(NombreUsuario),
    FOREIGN KEY (idRol) REFERENCES Rol(idRol)
);

CREATE TABLE Permisos (
    idPermisos INT PRIMARY KEY,
    nombrePermiso VARCHAR2(50)
);

CREATE TABLE Ventana (
    idVentana INT PRIMARY KEY,
    nombreVentana VARCHAR2(250), 
    nombreSistema VARCHAR2(50),
    status VARCHAR2(50),
    FOREIGN KEY (nombreSistema) REFERENCES Sistemas(nombreSistema)
);

CREATE TABLE VentanaRol (
    idVentana INT,
    idRol INT,
    idPermisos INT,
    PRIMARY KEY (idVentana, idRol, idPermisos),
    FOREIGN KEY (idVentana) REFERENCES Ventana(idVentana),
    FOREIGN KEY (idRol) REFERENCES Rol(idRol),
    FOREIGN KEY (idPermisos) REFERENCES Permisos(idPermisos)
);

CREATE TABLE VentanaUsuario (
    idVentana INT,
    nombreUsuario VARCHAR2(250),
    idPermisos INT,
    PRIMARY KEY (idVentana, nombreUsuario, idPermisos),
    FOREIGN KEY (idVentana) REFERENCES Ventana(idVentana),
    FOREIGN KEY (nombreUsuario) REFERENCES Usuario(nombreUsuario),
    FOREIGN KEY (idPermisos) REFERENCES Permisos(idPermisos)
);

CREATE TABLE BitacoraSeguridad (
    idBitacoraSeguridad INT PRIMARY KEY,
    nombreSistema VARCHAR2(50),
    nombreUsuario VARCHAR2(250),
    fechaHora DATE,
    accion VARCHAR2(50),
    tablaAfectada VARCHAR2(50),
    descripcion VARCHAR2(500),
    FOREIGN KEY (nombreUsuario) REFERENCES Usuario(nombreUsuario)
);

CREATE TABLE BitacoraAcceso (
    idBitacoraAcceso INT PRIMARY KEY,
    nombreSistema VARCHAR2(50),
    nombreUsuario VARCHAR2(250),
    fechaHora DATE,
    FOREIGN KEY (nombreUsuario) REFERENCES Usuario(nombreUsuario)
);
\

--PROCEDIMIENTOS
--procedure for insert user
CREATE OR REPLACE PROCEDURE Sp_Ins_Usuario (
    pNombreUsuario  IN VARCHAR2,
    pClave          IN VARCHAR2,
    pStatus         IN VARCHAR2,
    pNombreSistema  IN VARCHAR2,
    pMensaje        OUT VARCHAR2
)
IS
    vUsuarioExiste NUMBER;
BEGIN 
    SELECT COUNT(*) INTO vUsuarioExiste
    FROM Usuario
    WHERE nombreUsuario = pNombreUsuario;

    IF vUsuarioExiste = 0 THEN
        INSERT INTO Usuario (nombreUsuario, clave, status)
        VALUES (pNombreUsuario, pClave, pStatus);

        INSERT INTO SistemaUsuario (nombreSistema, nombreUsuario)
        VALUES (pNombreSistema, pNombreUsuario);

        pMensaje := 'Usuario guardado correctamente y asignado al sistema.';
    ELSE
        pMensaje := 'El usuario ya existe.';
    END IF;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        pMensaje := 'Error: ' || SQLERRM;
END;

/
CREATE OR REPLACE PROCEDURE Sp_Del_Usuario(
    pNombreUsuario IN VARCHAR2
)
IS
BEGIN
    UPDATE Usuario 
    SET status = 'inactivo'
    WHERE nombreUsuario = pNombreUsuario;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Usuario marcado como inactivo correctamente.');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/

--procedure for update user
create or replace procedure Sp_Upd_Usuario(
    pClave in varchar2,
    pStatus in varchar2,
    pnombreUsuarioModificado in varchar2
)
is
    pExisteUsuario int;
    claveVieja varchar(250);
    statusViejo varchar(50);
begin 
    select count(*) into pExisteUsuario
    from usuario
    where nombreUsuario = pnombreUsuarioModificado;

    if pExisteUsuario = 1 then
        update Usuario set
            clave = pclave,
            status = pstatus
        where nombreUsuario = pnombreUsuarioModificado;

        COMMIT;
    end if;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

/
--Procedure for show user
create or replace procedure Sp_Most_Usuario (
    pNombre in varchar2,
    pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select *
    from Usuario
    where Lower(NombreUsuario) like '%' || lower(NVL(trim(pNombre), ''))
    order by NombreUsuario;
EXCEPTION
WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;
/
--Procedure for show user by status
create or replace procedure Sp_Most_UsuarioStatus (
    pStatus in varchar2,
    pResultado out sys_refcursor)
is 
begin
    open pResultado for
    select *
    from Usuario
    where Lower(Status) like '%' || lower(NVL(trim(pStatus), '')) 
    order by NombreUsuario;
EXCEPTION
WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;
/

CREATE OR REPLACE PROCEDURE Sp_Most_UsuarioRol (
    pNombre     IN  VARCHAR2,
    pSistema    IN  VARCHAR2,
    pResultado  OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT ur.idRol,
               r.nombreRol
        FROM   UsuarioRol ur
        JOIN   Rol r ON r.idRol = ur.idRol
        JOIN   SistemaUsuario su ON su.nombreUsuario = ur.nombreUsuario
        WHERE  LOWER(ur.nombreUsuario) LIKE '%' || LOWER(NVL(TRIM(pNombre), '')) || '%'
          AND  su.nombreSistema = pSistema
          AND  r.nombreSistema = su.nombreSistema;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Most_UsuarioRol: ' || SQLERRM);
END;

/

-- parte ya hecha
--Procedure for show ScreenUser
CREATE OR REPLACE PROCEDURE Sp_Most_VentanaUsuario (
    pNombre IN VARCHAR2,
    pSistema IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT V.IDVENTANA,
               V.NOMBREVENTANA,
               MAX(CASE WHEN VU.IDPERMISOS = 1 THEN 1 ELSE 0 END) AS "CREATE",
               MAX(CASE WHEN VU.IDPERMISOS = 2 THEN 1 ELSE 0 END) AS "READ",
               MAX(CASE WHEN VU.IDPERMISOS = 3 THEN 1 ELSE 0 END) AS "UPDATE",
               MAX(CASE WHEN VU.IDPERMISOS = 4 THEN 1 ELSE 0 END) AS "DELETE"
        FROM VENTANA V
        LEFT JOIN VENTANAUSUARIO VU
            ON V.IDVENTANA = VU.IDVENTANA
           AND LOWER(VU.NOMBREUSUARIO) = LOWER(TRIM(pNombre))
        WHERE V.STATUS = 'activo'
          AND V.NOMBRESISTEMA = pSistema
        GROUP BY V.IDVENTANA, V.NOMBREVENTANA
        ORDER BY V.NOMBREVENTANA;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;


/

create or replace NONEDITIONABLE PROCEDURE Sp_Most_VentanaNegocio (
    pNegocio IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT idVentana, nombreVentana,nombresistema,status
        FROM Ventana
        WHERE LOWER(nombresistema) LIKE '%' || LOWER(NVL(TRIM(pNegocio), '')) || '%'
          AND status = 'activo'
        ORDER BY nombresistema;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

/

--Procedure for insert screenUser
CREATE OR REPLACE PROCEDURE Sp_Ins_UsuarioRol (
    pNombreUsuario IN VARCHAR2,
    pIdRol IN NUMBER
)
IS
BEGIN
    INSERT INTO UsuarioRol (nombreUsuario, idRol)
    VALUES (pNombreUsuario, pIdRol);

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Rol asignado al usuario correctamente');
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

/
--procedure for insert screenUser
CREATE OR REPLACE PROCEDURE Sp_Ins_VentanaUsuario (
    pNombreUsuario IN VARCHAR2,
    pIdVentana IN NUMBER,
    pIdPermiso IN NUMBER
)
IS
BEGIN
    INSERT INTO VentanaUsuario (nombreUsuario, idVentana, idPermisos)
    VALUES (pNombreUsuario, pIdVentana, pIdPermiso);

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Permiso asignado al usuario en la ventana');
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;




/
CREATE OR REPLACE PROCEDURE Sp_Del_VentanaUsuario (
    pNombreUsuario IN VARCHAR2,
    pIdVentana IN NUMBER,
    pIdPermiso IN NUMBER
)
IS
BEGIN
    DELETE FROM VentanaUsuario
    WHERE nombreUsuario = pNombreUsuario
      AND idVentana = pIdVentana
      AND idPermisos = pIdPermiso;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Permiso de usuario en ventana eliminado');
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

/



--PART OF ROLE

--procedure for insert role
create or replace procedure Sp_Ins_Rol (
    pNombreRol in varchar2,
    pStatus in varchar2,
    pUsuarioActual in varchar2,
    pSistema in varchar2
)
is
    vExiste number;
    vIdRol number;
begin
    select count(*) into vExiste
    from Rol
    where lower(nombreRol) = lower(pNombreRol)
      and nombreSistema = pSistema;

    IF vExiste = 0 THEN
        select nvl(max(idRol), 0) + 1 into vIdRol from Rol;

        insert into Rol (idRol, nombreRol, status, nombreSistema)
        values (vIdRol, pNombreRol, pStatus, pSistema);

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

/
--procedure for update rol
CREATE OR REPLACE PROCEDURE Sp_Upd_Rol (
    pIdRol IN NUMBER,
    pNuevoNombreRol IN VARCHAR2,
    pNuevoStatus IN VARCHAR2,
    pUsuarioActual IN VARCHAR2,
    pNuevoSistema IN VARCHAR2
)
IS
    vNombreAntiguo VARCHAR2(250);
    vStatusAntiguo VARCHAR2(50);
    vIdBitacora NUMBER;
BEGIN
    -- Obtener los datos anteriores (sin filtrar por sistema)
    SELECT nombreRol, status
    INTO vNombreAntiguo, vStatusAntiguo
    FROM Rol
    WHERE idRol = pIdRol;

    -- Actualizar todos los campos, incluido el sistema
    UPDATE Rol
    SET nombreRol = pNuevoNombreRol,
        status = pNuevoStatus,
        nombreSistema = pNuevoSistema
    WHERE idRol = pIdRol;

    -- Preparar id para la bitácora (reservado para futuro uso)
    SELECT NVL(MAX(idBitacoraSeguridad), 0) + 1
    INTO vIdBitacora
    FROM BitacoraSeguridad;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Rol actualizado correctamente');
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/


--Procedure for show rol
create or replace procedure Sp_Most_Rol (
     pNombre in varchar2,
    pSistema in varchar2,
    pResultado out sys_refcursor
)
is
begin
    open pResultado for
    select idRol, nombreRol, status
        from Rol
        where lower(nombreRol) LIKE '%' || lower(NVL(TRIM(pNombre), '')) || '%'
          and nombreSistema = pSistema
        ORDER BY nombreRol;

exception
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
end;

/
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

/

create or replace procedure Sp_Ins_VentanaRol (
    pIdVentana in number,
    pIdRol in number,
    pIdPermisos in number
)
is
    vCount number;
begin
    -- verificar si ya existe
    select count(*) into vCount
    from VentanaRol
    where idVentana = pIdVentana
      and idRol = pIdRol
      and idPermisos = pIdPermisos;

    if vCount = 0 then
        insert into VentanaRol(idVentana, idRol, idPermisos)
        values(pIdVentana, pIdRol, pIdPermisos);
        commit;
    end if;
end;

/

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
        join Ventana v on vr.idVentana = v.idVentana
        join Permisos p on vr.idPermisos = p.idPermisos
        where vr.idRol = pIdRol;
exception
    when others then
        dbms_output.put_line('Error: ' || sqlerrm);
end;
/
--Procedure for delete screenRol
CREATE OR REPLACE PROCEDURE Sp_Del_UsuarioRol (
    pNombreUsuario IN VARCHAR2,
    pIdRol IN NUMBER
)
IS
BEGIN
    DELETE FROM UsuarioRol
    WHERE nombreUsuario = pNombreUsuario
      AND idRol = pIdRol;

    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

/
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

/
CREATE OR REPLACE PROCEDURE Sp_Most_RolesNoAsignados (
    pNombre IN VARCHAR2,
    pNombreSistema IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT r.idRol, r.nombreRol
        FROM Rol r
        INNER JOIN SistemaUsuario su
            ON su.nombreSistema = r.nombreSistema
           AND lower(su.nombreUsuario) = lower(trim(pNombre))
        WHERE r.status = 'activo'
          AND r.nombreSistema = pNombreSistema
          AND NOT EXISTS (
              SELECT 1 FROM UsuarioRol ur
              WHERE ur.idRol = r.idRol
                AND lower(ur.nombreUsuario) = lower(trim(pNombre))
          );
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

/
CREATE OR REPLACE PROCEDURE Sp_Most_UsuarioSistema (
    pNombre         IN  VARCHAR2,
    pNombreSistema  IN  VARCHAR2,
    pResultado      OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT u.nombreUsuario,
               u.status
        FROM   Usuario u
        JOIN   SistemaUsuario su ON su.nombreUsuario = u.nombreUsuario
        WHERE  su.nombreSistema = pNombreSistema AND  LOWER(u.nombreUsuario) LIKE '%' || LOWER(NVL(TRIM(pNombre), '')) || '%'
        ORDER BY u.nombreUsuario;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Most_UsuarioSistema: ' || SQLERRM);
END;

/
create or replace PROCEDURE Sp_Upd_Ventana (
    pIdVentana IN NUMBER,
    pNuevoNombreVentana IN VARCHAR2,
    pNuevoStatus IN VARCHAR2,
    pNuevoNombreSistema IN VARCHAR2,
    pUsuarioActual IN VARCHAR2
)
IS
    vNombreAntiguo VARCHAR2(250);
    vStatusAntiguo VARCHAR2(50);
    vSistemaAntiguo VARCHAR2(50);
BEGIN
    -- Guardar valores anteriores
    SELECT nombreVentana, status, nombreSistema
    INTO vNombreAntiguo, vStatusAntiguo, vSistemaAntiguo
    FROM Ventana
    WHERE idVentana = pIdVentana;

    -- Actualizar la ventana con el nuevo sistema también
    UPDATE Ventana
    SET nombreVentana = pNuevoNombreVentana,
        status = pNuevoStatus,
        nombreSistema = pNuevoNombreSistema
    WHERE idVentana = pIdVentana;

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Ventana actualizada correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE Sp_Most_Ventana (
    pNombre IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT *
        FROM Ventana
        WHERE LOWER(nombreVentana) LIKE '%' || LOWER(NVL(TRIM(pNombre), '')) || '%'
        ORDER BY nombreVentana;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE Sp_Del_Ventana (
    pIdVentana IN NUMBER,
    pUsuarioActual IN VARCHAR2
)
IS
BEGIN
    -- Soft delete: cambiar estado
    UPDATE Ventana
    SET status = 'inactivo'
    WHERE idVentana = pIdVentana;


    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Ventana marcada como inactiva correctamente');

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE Sp_Most_VentanaPermisosRol (
    pIdRol IN NUMBER,
    pSistema IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT V.IDVENTANA,
               V.NOMBREVENTANA,
               MAX(CASE WHEN VR.IDPERMISOS = 1 THEN 1 ELSE 0 END) AS "CREATE",
               MAX(CASE WHEN VR.IDPERMISOS = 2 THEN 1 ELSE 0 END) AS "READ",
               MAX(CASE WHEN VR.IDPERMISOS = 3 THEN 1 ELSE 0 END) AS "UPDATE",
               MAX(CASE WHEN VR.IDPERMISOS = 4 THEN 1 ELSE 0 END) AS "DELETE"
        FROM VENTANA V
        LEFT JOIN VENTANAROL VR
            ON V.IDVENTANA = VR.IDVENTANA
           AND VR.IDROL = pIdRol
        WHERE V.STATUS = 'activo'
          AND V.NOMBRESISTEMA = pSistema
        GROUP BY V.IDVENTANA, V.NOMBREVENTANA
        ORDER BY V.NOMBREVENTANA;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/




--Procedimiento para las ventanas
create or replace NONEDITIONABLE PROCEDURE Sp_Ins_Ventana (
    pNombreVentana IN VARCHAR2,
    pStatus IN VARCHAR2,
    pUsuarioActual IN VARCHAR2,
    pNombreSistema IN VARCHAR2,
    pMensaje OUT VARCHAR2
)
IS
    vExiste NUMBER;
    vIdVentana NUMBER;
BEGIN
    SELECT COUNT(*) INTO vExiste
    FROM Ventana
    WHERE LOWER(nombreVentana) = LOWER(pNombreVentana)
      AND nombreSistema = pNombreSistema;

    IF vExiste = 0 THEN
        SELECT NVL(MAX(idVentana), 0) + 1 INTO vIdVentana FROM Ventana;

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (vIdVentana, pNombreVentana, pNombreSistema, pStatus);

        COMMIT;
        pMensaje := 'Ventana insertada correctamente';
    ELSE
        pMensaje := 'La ventana ya existe';
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        pMensaje := 'Error: ' || SQLERRM;
END;

/
--procedure for login control
CREATE OR REPLACE NONEDITIONABLE PROCEDURE Sp_Login (
    pNombreUsuario  IN VARCHAR2,
    pNombreSistema  IN VARCHAR2,
    pClaveHash OUT VARCHAR2,
    pStatus OUT VARCHAR2,
    pMensaje OUT VARCHAR2
)
IS
    vAsignado        NUMBER;
    vExisteUsuario   NUMBER;
BEGIN
    -- Validar existencia del usuario (insensible a mayúsculas)
    SELECT COUNT(*) INTO vExisteUsuario
    FROM Usuario
    WHERE LOWER(nombreUsuario) = LOWER(pNombreUsuario);

    IF vExisteUsuario = 0 THEN
        pMensaje := 'El usuario no existe';
        RETURN;
    END IF;

    -- Verificar si está asignado al sistema
    SELECT COUNT(*) INTO vAsignado
    FROM SistemaUsuario
    WHERE LOWER(nombreUsuario) = LOWER(pNombreUsuario)
      AND LOWER(nombreSistema) = LOWER(pNombreSistema);

    IF vAsignado = 0 THEN
        pMensaje := 'El usuario no existe';
        RETURN;
    END IF;

    -- Obtener clave y estado
    SELECT clave, status INTO pClaveHash, pStatus
    FROM Usuario
    WHERE LOWER(nombreUsuario) = LOWER(pNombreUsuario);

    pMensaje := 'OK';

EXCEPTION
    WHEN OTHERS THEN
        pMensaje := 'Error inesperado: ' || SQLERRM;
END;
/
CREATE OR REPLACE NONEDITIONABLE PROCEDURE Sp_Most_Permisos_Completos_Usuario (
    pNombreUsuario IN VARCHAR2,
    pNombreSistema IN VARCHAR2,
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT
            V.IDVENTANA,
            V.NOMBREVENTANA,
            MAX(CASE WHEN PERMISO = 1 THEN 1 ELSE 0 END) AS "CREATE",
            MAX(CASE WHEN PERMISO = 2 THEN 1 ELSE 0 END) AS "READ",
            MAX(CASE WHEN PERMISO = 3 THEN 1 ELSE 0 END) AS "UPDATE",
            MAX(CASE WHEN PERMISO = 4 THEN 1 ELSE 0 END) AS "DELETE"
        FROM (
            -- Permisos directos
            SELECT
                VU.IDVENTANA,
                VU.IDPERMISOS AS PERMISO
            FROM VENTANAUSUARIO VU
            JOIN VENTANA V ON V.IDVENTANA = VU.IDVENTANA
            WHERE LOWER(VU.NOMBREUSUARIO) = LOWER(TRIM(pNombreUsuario))
              AND V.NOMBRESISTEMA = pNombreSistema

            UNION ALL

            -- Permisos indirectos (por rol)
            SELECT
                VR.IDVENTANA,
                VR.IDPERMISOS AS PERMISO
            FROM USUARIOROL UR
            JOIN VENTANAROL VR ON UR.IDROL = VR.IDROL
            JOIN VENTANA V ON V.IDVENTANA = VR.IDVENTANA
            WHERE LOWER(UR.NOMBREUSUARIO) = LOWER(TRIM(pNombreUsuario))
              AND V.NOMBRESISTEMA = pNombreSistema
        ) PERMISOS
        JOIN VENTANA V ON V.IDVENTANA = PERMISOS.IDVENTANA
        WHERE V.STATUS = 'activo'
          AND V.NOMBRESISTEMA = pNombreSistema
        GROUP BY V.IDVENTANA, V.NOMBREVENTANA
        ORDER BY V.NOMBREVENTANA;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/







--TRIGGERS 





--TRIGGER MODULO SEGURIDAD


--trigger users
CREATE OR REPLACE TRIGGER trg_bitacora_usuario 
AFTER INSERT OR UPDATE OR DELETE ON Usuario
FOR EACH ROW
DECLARE
    vAccion         VARCHAR2(20);
    vDescripcion    VARCHAR2(1000);
    vIdBitacora     NUMBER;
    vUsuarioActual  VARCHAR2(250);  -- CLIENT_IDENTIFIER desde la aplicación
    vSistema        VARCHAR2(50);
    vNombreUsuario  VARCHAR2(250);
BEGIN
    -- Obtener el identificador del usuario desde la sesión (configurado por la app con CLIENT_IDENTIFIER)
    vUsuarioActual := SYS_CONTEXT('USERENV', 'CLIENT_IDENTIFIER');

    -- Determinar el nombre del usuario afectado según la operación
    IF INSERTING OR UPDATING THEN
        vNombreUsuario := :NEW.nombreUsuario;
    ELSIF DELETING THEN
        vNombreUsuario := :OLD.nombreUsuario;
    END IF;

    -- Intentar obtener el sistema relacionado al usuario (puede no tener uno aún)
    BEGIN
        IF vNombreUsuario IS NOT NULL THEN
            SELECT nombreSistema
            INTO vSistema
            FROM SistemaUsuario
            WHERE nombreUsuario = vNombreUsuario
            FETCH FIRST 1 ROWS ONLY;
        ELSE
            vSistema := 'desconocido';
        END IF;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            vSistema := 'desconocido';
    END;

    -- Determinar la acción y descripción según el tipo de operación
    IF INSERTING THEN
        vAccion := 'CREATE';
        vDescripcion := 'Se insertó el usuario: ' || :NEW.nombreUsuario || ' / Status: ' || :NEW.status;
    ELSIF UPDATING THEN
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizó el usuario: ' || :OLD.nombreUsuario || ' / Status: ' || :OLD.status ||
                        ' a usuario: ' || :NEW.nombreUsuario || ' / Status: ' || :NEW.status;
    ELSIF DELETING THEN
        vAccion := 'DELETE';
        vDescripcion := 'Se eliminó el usuario: ' || :OLD.nombreUsuario || ' / Status: ' || :OLD.status;
    END IF;

    -- Generar nuevo ID para la bitácora
    SELECT NVL(MAX(idBitacoraSeguridad), 0) + 1 INTO vIdBitacora FROM BitacoraSeguridad;

    -- Insertar el registro en la bitácora de seguridad
    INSERT INTO BitacoraSeguridad (
        idBitacoraSeguridad,
        nombreSistema,
        nombreUsuario,
        fechaHora,
        accion,
        tablaAfectada,
        descripcion
    ) VALUES (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        SYSDATE,
        vAccion,
        'Usuario',
        vDescripcion
    );
END;



/
--Trigger for role
create or replace trigger trg_bitacora_rol
after insert or update or delete on Rol
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := case when inserting then :new.nombreSistema
                     when updating then :new.nombreSistema
                     else :old.nombreSistema end;

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto el rol: / idRol:' || :new.idRol || '/ nombre: ' || :new.nombreRol || '/ status: ' || :new.status;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo el rol: / idRol:' || :old.idRol || '/ nombre anterior: ' || :old.nombreRol
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
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Rol',
        vDescripcion
    );
end;


/


--trigger usuarioRol
CREATE OR REPLACE TRIGGER trg_bitacora_usuariorol
AFTER INSERT OR UPDATE OR DELETE ON UsuarioRol
FOR EACH ROW
DECLARE
    vAccion         VARCHAR2(20);
    vDescripcion    VARCHAR2(1000);
    vIdBitacora     NUMBER;
    vUsuarioActual  VARCHAR2(250);
    vSistema        VARCHAR2(50);
    vRolId          NUMBER;
BEGIN
    vUsuarioActual := SYS_CONTEXT('USERENV', 'CLIENT_IDENTIFIER');
    
    -- Determinar id del rol seg n la operaci n
    vRolId := CASE
        WHEN INSERTING THEN :NEW.idRol
        WHEN UPDATING THEN :NEW.idRol
        ELSE :OLD.idRol
    END;

    -- Buscar nombre del sistema desde la tabla ROL
    SELECT nombreSistema
    INTO vSistema
    FROM Rol
    WHERE idRol = vRolId;

    -- Armar acci n y descripci n
    IF INSERTING THEN
        vAccion := 'CREATE';
        vDescripcion := 'Se insert  UsuarioRol: / idRol: ' || :NEW.idRol || ' / usuario: ' || :NEW.NombreUsuario;
    ELSIF UPDATING THEN
        vAccion := 'UPDATE';
        vDescripcion := 'Se modific  la asignaci n de rol para el usuario: ' || :OLD.NombreUsuario || 
                        ' / idRol: ' || :OLD.idRol || 
                        ' a / usuario: ' || :NEW.NombreUsuario || ' / idRol: ' || :NEW.idRol;
    ELSIF DELETING THEN
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin  el rol: / idRol: ' || :OLD.idRol || ' / usuario: ' || :OLD.NombreUsuario;
    END IF;

    -- Insert en bit cora
    SELECT NVL(MAX(idBitacoraSeguridad), 0) + 1 INTO vIdBitacora FROM BitacoraSeguridad;

    INSERT INTO BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) VALUES (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        SYSDATE,
        vAccion,
        'UsuarioRol',
        vDescripcion
    );
END;





/

--trigger for permits
create or replace trigger trg_bitacora_permisos
after insert or update or delete on Permisos
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := 'general'; 

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto el permiso: / idPermiso:' || :new.idPermisos || '/ nombre: ' || :new.nombrePermiso;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo el permiso: / idPermiso:' || :old.idPermisos || '/ nombre: ' || :old.nombrePermiso
                     || ' a / idPermiso:' || :new.idPermisos || '/ nombre: ' || :new.nombrePermiso;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino el permiso: / idPermiso:' || :old.idPermisos || '/ nombre: ' || :old.nombrePermiso;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Permisos',
        vDescripcion
    );
end;


/

--trigger for ventana
create or replace trigger trg_bitacora_ventana
after insert or update or delete on Ventana
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := case when inserting then :new.nombreSistema
                     when updating then :new.nombreSistema
                     else :old.nombreSistema end;

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto la ventana: / idVentana:' || :new.idVentana || '/ nombre: ' || :new.nombreVentana;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo la ventana: / idVentana:' || :old.idVentana || '/ nombre: ' || :old.nombreVentana
                     || ' a / idVentana:' || :new.idVentana || ' / nombre nuevo: ' || :new.nombreVentana;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino la ventana: / idVentana:' || :old.idVentana || '/ nombre: ' || :old.nombreVentana;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'Ventana',
        vDescripcion
    );
end;


/
--trigger for ventanaRol
create or replace trigger trg_bitacora_ventanarol
after insert or update or delete on VentanaRol
for each row
declare
    vAccion varchar2(20);
    vDescripcion varchar2(1000);
    vIdBitacora number;
    vUsuarioActual varchar2(250);
    vSistema varchar2(50);
begin
    vUsuarioActual := sys_context('USERENV', 'CLIENT_IDENTIFIER');
    vSistema := 'general'; -- si no hay columna nombreSistema en VentanaRol

    if inserting then
        vAccion := 'CREATE';
        vDescripcion := 'Se inserto en ventanaRol: / idPermisos:' || :new.idPermisos || ' // rol: ' || :new.idRol || ' / ventana: ' || :new.idVentana;
    elsif updating then
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualizo la asignacion en VentanaRol: / idVentana: ' || :old.idVentana || '/ idRol: ' || :old.idRol
        ||' a / idPermisos:' || :new.idPermisos || ' // rol: ' || :new.idRol || ' / ventana: ' || :new.idVentana;
    elsif deleting then
        vAccion := 'DELETE';
        vDescripcion := 'Se elimino asignacion en VentanaRol: / idVentana:' || :old.idVentana || '/ idRol:' || :old.idRol
                     || '/ idPermisos:' || :old.idPermisos;
    end if;

    select nvl(max(idBitacoraSeguridad), 0) + 1 into vIdBitacora
    from BitacoraSeguridad;

    insert into BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) values (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        sysdate,
        vAccion,
        'VentanaRol',
        vDescripcion
    );
end;

/

--trigger for ventanaUsuario
CREATE OR REPLACE TRIGGER trg_bitacora_ventanausuario
AFTER INSERT OR UPDATE OR DELETE ON VentanaUsuario
FOR EACH ROW
DECLARE
    vAccion         VARCHAR2(20);
    vDescripcion    VARCHAR2(1000);
    vIdBitacora     NUMBER;
    vUsuarioActual  VARCHAR2(250);
    vSistema        VARCHAR2(50);
    vVentana        INT;
BEGIN
    vUsuarioActual := SYS_CONTEXT('USERENV', 'CLIENT_IDENTIFIER');

    -- Determina el ID de la ventana afectada seg n el tipo de operaci n
    IF INSERTING OR UPDATING THEN
        vVentana := :NEW.idVentana;
    ELSE
        vVentana := :OLD.idVentana;
    END IF;

    -- Consulta el nombre del sistema usando el ID de ventana
    SELECT nombreSistema INTO vSistema
    FROM Ventana
    WHERE idVentana = vVentana;

    -- Determina el tipo de acci n y descripci n
    IF INSERTING THEN
        vAccion := 'CREATE';
        vDescripcion := 'Se asign  permiso: / idPermisos: ' || :NEW.idPermisos || 
                        ' / usuario: ' || :NEW.NombreUsuario || 
                        ' / ventana: ' || :NEW.idVentana;
    ELSIF UPDATING THEN
        vAccion := 'UPDATE';
        vDescripcion := 'Se actualiz  asignaci n en VentanaUsuario: / idVentana: ' || :OLD.idVentana ||
                        ' / usuario: ' || :OLD.NombreUsuario ||
                        ' / idPermisos: ' || :OLD.idPermisos ||
                        ' a / idPermisos: ' || :NEW.idPermisos || 
                        ' / usuario: ' || :NEW.NombreUsuario || 
                        ' / ventana: ' || :NEW.idVentana;
    ELSIF DELETING THEN
        vAccion := 'DELETE';
        vDescripcion := 'Se elimin  asignaci n en VentanaUsuario: / idVentana: ' || :OLD.idVentana ||
                        ' / usuario: ' || :OLD.NombreUsuario ||
                        ' / idPermisos: ' || :OLD.idPermisos;
    END IF;

    -- Obtener el siguiente ID para la bit cora
    SELECT NVL(MAX(idBitacoraSeguridad), 0) + 1 INTO vIdBitacora
    FROM BitacoraSeguridad;

    -- Insertar registro en la bit cora
    INSERT INTO BitacoraSeguridad (
        idBitacoraSeguridad, nombreSistema, nombreUsuario, fechaHora, accion, tablaAfectada, descripcion
    ) VALUES (
        vIdBitacora,
        vSistema,
        vUsuarioActual,
        SYSDATE,
        vAccion,
        'VentanaUsuario',
        vDescripcion
    );
END;
/



/
CREATE OR REPLACE PROCEDURE Sp_Most_Sistemas (
    pResultado OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT *
        FROM   Sistemas
        ORDER BY nombreSistema;
        
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Most_Sistemas: ' || SQLERRM);
END;

/
CREATE OR REPLACE PROCEDURE Sp_Most_SistemasUsuario (
    pNombreUsuario IN VARCHAR2,
    pResultado     OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pResultado FOR
        SELECT s.nombreSistema,s.descripcion,
               CASE 
                   WHEN su.nombreUsuario IS NOT NULL THEN 1 
                   ELSE 0 
               END AS asignado
        FROM Sistemas s
        LEFT JOIN SistemaUsuario su ON su.nombreSistema = s.nombreSistema AND su.nombreUsuario = pNombreUsuario
        ORDER BY s.nombreSistema;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Most_SistemasUsuario: ' || SQLERRM);
END;
/
--sistemas

CREATE OR REPLACE PROCEDURE Sp_Ins_Sistema (
    pNombreSistema IN VARCHAR2,
    pDescripcion IN VARCHAR2
)
IS
BEGIN
    INSERT INTO Sistemas(nombreSistema,Descripcion)
    VALUES (pNombreSistema,pDescripcion);

    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Ins_Sistema: ' || SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE Sp_Upd_Sistema (
    pDescripcion   IN Varchar,
    pNuevoNombre   IN VARCHAR2,
    pMensaje       OUT VARCHAR2
)
IS
    vExiste NUMBER;
BEGIN
    SELECT COUNT(*) INTO vExiste
    FROM Sistemas 
    WHERE NOMBRESISTEMA = pNuevoNombre;
    
    IF vExiste > 0 THEN
        UPDATE Sistemas
        SET Descripcion = pDescripcion
        WHERE NOMBRESISTEMA = pNuevoNombre;

        pMensaje := 'El sistema se modificó correctamente';
    ELSE
        pMensaje := 'El sistema no existe';
    END IF;

    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        pMensaje := 'Error: ' || SQLERRM;
END;

/



--procedimientos sistemasUsuario
CREATE OR REPLACE PROCEDURE Sp_Ins_SistemaUsuario (
    pNombreSistema  IN VARCHAR2,
    pNombreUsuario  IN VARCHAR2
)
IS
    vExiste NUMBER;
BEGIN
    SELECT COUNT(*) INTO vExiste
    FROM SistemaUsuario
    WHERE nombreSistema = pNombreSistema
      AND nombreUsuario = pNombreUsuario;

    IF vExiste = 0 THEN
        INSERT INTO SistemaUsuario (nombreSistema, nombreUsuario)
        VALUES (pNombreSistema, pNombreUsuario);
    END IF;
    
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Ins_SistemaUsuario: ' || SQLERRM);
END;
/
CREATE OR REPLACE PROCEDURE Sp_Del_SistemaUsuario (
    pNombreSistema  IN VARCHAR2,
    pNombreUsuario  IN VARCHAR2
)
IS
BEGIN
    DELETE FROM SistemaUsuario
    WHERE nombreSistema = pNombreSistema
      AND nombreUsuario = pNombreUsuario;

    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error en Sp_Del_SistemaUsuario: ' || SQLERRM);
END;
/

CREATE OR REPLACE PROCEDURE Sp_ObtenerSistemas(pCursor OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN pCursor FOR
        SELECT nombreSistema
        FROM sistemas
        ORDER BY nombreSistema;
END;
/
-- procedimientos Sistemaventanas
CREATE OR REPLACE PROCEDURE Sp_ObtenerVentanasNegocio (
    pNombreSistema IN VARCHAR2,
    pCursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN pCursor FOR
        SELECT *
        FROM ventana
        WHERE nombreSistema = pNombreSistema;  -- Sin filtro por 'activo'
END;

/

CREATE OR REPLACE TRIGGER trg_crear_datos_default --revisar la logica
AFTER INSERT ON SistemaUsuario
FOR EACH ROW
DECLARE
    vTotalUsuarios   NUMBER;
    vTotalSistemas   NUMBER;
BEGIN
    -- Verificar si no hay usuarios ni sistemas
    SELECT COUNT(*) INTO vTotalUsuarios FROM Usuario;
    SELECT COUNT(*) INTO vTotalSistemas FROM Sistemas;

    IF vTotalUsuarios = 0 AND vTotalSistemas = 0 THEN

        -- Insertar sistemas por defecto
        INSERT INTO Sistemas (nombreSistema, descripcion)
        VALUES ('negocio1', 'Sistema de operaciones de negocio');

        INSERT INTO Sistemas (nombreSistema, descripcion)
        VALUES ('seguridad', 'Sistema de gestión de seguridad');

        -- Insertar usuario por defecto
       INSERT INTO Usuario (nombreUsuario, clave, status)
       VALUES ('admin','$2a$11$iHfQ3xNf3Pqb1dX7J1Y3fOTvJSZDd0bGM3dGniITB5BPbLAW65yLG','activo');

        -- Asociar usuario con ambos sistemas
        INSERT INTO SistemaUsuario (nombreSistema, nombreUsuario)
        VALUES ('negocio1', 'admin');

        INSERT INTO SistemaUsuario (nombreSistema, nombreUsuario)
        VALUES ('seguridad', 'admin');

        -- Insertar permisos sin validación
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (1, 'Crear');
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (2, 'Leer');
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (3, 'Actualizar');
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (4, 'Eliminar');

        -- Ventanas del sistema negocio1
        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (100, 'Cosmetico', 'negocio1', 'activo');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (101, 'Compra', 'negocio1', 'activo');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (102, 'Venta', 'negocio1', 'activo');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (103, 'Consumidor', 'negocio1', 'activo');

        -- Ventana del sistema seguridad
        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (104, 'Usuarios', 'seguridad', 'activo');

        -- Asignar todos los permisos a 'admin' para negocio1 (idVentana 100–103)
        FOR i IN 100 .. 103 LOOP
            FOR permiso IN 1 .. 4 LOOP
                INSERT INTO VentanaUsuario (nombreUsuario, idVentana, idPermisos)
                VALUES ('admin', i, permiso);
            END LOOP;
        END LOOP;

        -- Asignar todos los permisos a 'admin' para seguridad (idVentana 104)
        FOR permiso IN 1 .. 4 LOOP
            INSERT INTO VentanaUsuario (nombreUsuario, idVentana, idPermisos)
            VALUES ('admin', 104, permiso);
        END LOOP;

        DBMS_OUTPUT.PUT_LINE('Datos por defecto creados correctamente.');
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error en trigger de datos por defecto: ' || SQLERRM);
END;


/


CREATE OR REPLACE PROCEDURE Sp_CrearUsuarioDefault
IS
    vUsuariosActivos NUMBER;
BEGIN
    -- Verificar si hay usuarios activos
    SELECT COUNT(*) INTO vUsuariosActivos 
    FROM Usuario 
    WHERE LOWER(status) = 'activo';

    -- Si no hay ninguno activo, activar al usuario admin
    IF vUsuariosActivos = 0 THEN
        UPDATE Usuario 
        SET status = 'activo' 
        WHERE LOWER(nombreUsuario) = 'admin';

        COMMIT;
        DBMS_OUTPUT.PUT_LINE('El usuario admin ha sido reactivado.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Ya existen usuarios activos. No se hizo ningún cambio.');
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error en Sp_CrearUsuarioDefault: ' || SQLERRM);
END;
/


--datos para poder correr correctamente el programa:
   
        INSERT INTO Usuario (nombreUsuario, clave, status)
        VALUES ('admin','$2a$11$iHfQ3xNf3Pqb1dX7J1Y3fOTvJSZDd0bGM3dGniITB5BPbLAW65yLG','activo');
        
        insert into sistemas
        values ('seguridad', 'seguridad');
        
        insert into sistemas
        values ('negocio1', 'seguridad');
        
        insert into sistemaUsuario
        values ('seguridad','admin');
        
        insert into sistemaUsuario
        values ('negocio1','admin');
        
        -- Insertar permisos sin validación
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (1, 'Crear');
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (2, 'Leer');
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (3, 'Actualizar');
        INSERT INTO Permisos (idPermisos, nombrePermiso) VALUES (4, 'Eliminar');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (100, 'Cosmetico', 'negocio1', 'activo');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (101, 'Compra', 'negocio1', 'activo');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (102, 'Venta', 'negocio1', 'activo');

        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (103, 'Consumidor', 'negocio1', 'activo');

        -- Ventana del sistema seguridad
        INSERT INTO Ventana (idVentana, nombreVentana, nombreSistema, status)
        VALUES (104, 'Usuarios', 'seguridad', 'activo');
        
        insert into rol 
        values (1,'rol1', 'activo', 'seguridad')
        
        commit;
        
        select *
        from SistemaUsuario;
 
        --AUDITORIA
-- 1. Crear el usuario auditor
create user auditor
identified by ucr2025
default tablespace proyecto
temporary tablespace temp;


alter user auditor QUOTA UNLIMITED ON proyecto;

-- 2. Permitirle conectarse
GRANT CONNECT TO auditor;

-- 3. Otorgar permisos de solo lectura sobre todas las tablas de ADAN y NEGOCIO1
declare 
    cursor cAuditor is
    select distinct owner, table_name
    from dba_tables 
    where tablespace_name = 'PROYECTO';
    
    tablaUsuario varchar(500);
begin
    for i in cAuditor loop
        
        tablaUsuario := 'grant select on ' || i.owner || '.' || i.table_name || ' to auditor';
        
        execute immediate tablaUsuario;
    
    end loop;
end;
/
--FIN AUDITORIA
  