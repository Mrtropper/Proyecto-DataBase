# Enterprise Database Management System

A comprehensive enterprise solution demonstrating secure, role-based access control integrated with business management applications. This project showcases a complete implementation of database security architecture coupled with a real-world business system for cosmetics inventory and sales management.

## Table of Contents

- [Overview](#overview)
- [System Architecture](#system-architecture)
- [Projects](#projects)
- [Technology Stack](#technology-stack)
- [Key Features](#key-features)
- [Database Design](#database-design)
- [Security Implementation](#security-implementation)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)
- [Development](#development)

## Overview

This enterprise-grade database management system consists of two integrated applications that work together to provide secure, permission-controlled access to business operations:

1. **Security Management Module** - A centralized security system implementing role-based access control (RBAC) with comprehensive user, role, and permission management capabilities
2. **Cosmetics Business Management System** - A full-featured inventory and sales management application that leverages the security module for granular access control

The system demonstrates industry best practices including multi-tier architecture, stored procedure-based data access, password encryption, audit logging, and dynamic permission management.

## System Architecture

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│  ┌──────────────────────┐  ┌──────────────────────────────┐ │
│  │  Security Admin      │  │  Business Application        │ │
│  │  Interface           │  │  (Cosmetics Management)      │ │
│  │  - User Management   │  │  - Sales Management          │ │
│  │  - Role Management   │  │  - Inventory Control         │ │
│  │  - Permission Setup  │  │  - Purchase Orders           │ │
│  └──────────────────────┘  └──────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    Business Logic Layer                      │
│  - Authentication & Authorization                            │
│  - Business Rules Validation                                 │
│  - Data Transformation                                       │
│  - Transaction Management                                    │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    Data Access Layer                         │
│  - Connection Management                                     │
│  - Stored Procedure Execution                                │
│  - Parameter Mapping                                         │
│  - Result Set Processing                                     │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    Oracle Database 21c                       │
│  ┌────────────┐  ┌────────────┐  ┌──────────────────────┐  │
│  │ Security   │  │ Business   │  │ Audit & Logging      │  │
│  │ Schema     │  │ Schema     │  │ - BitacoraSeguridad  │  │
│  │ (adan)     │  │ (negocio1) │  │ - BitacoraAcceso     │  │
│  └────────────┘  └────────────┘  └──────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

### Three-Tier Architecture Pattern

Both applications follow a strict three-tier architecture:

1. **Presentation Layer (UI)**: Windows Forms interfaces providing intuitive user interaction
2. **Business Logic Layer (BLL)**: Domain models, business rules, and validation logic
3. **Data Access Layer (DAL)**: Database connection management and stored procedure execution

## Projects

### 1. Security Management Module (`modulo-seguridadBD-main`)

A comprehensive security administration system providing centralized access control for multiple business applications.

#### Core Capabilities
- User account management with encrypted password storage (BCrypt)
- Role definition and assignment per business system
- Granular permission control at the window/form level
- Multi-system support for enterprise-wide security
- Complete audit trail of security events
- User session tracking and access logging

#### Key Components
- User Management: Create, modify, activate/deactivate user accounts
- Role Management: Define roles with system-specific permissions
- Permission Management: Assign Read/Write/Delete permissions at the form level
- System Administration: Register and manage multiple business systems
- Window Registration: Define and track application windows/forms
- Audit Logging: Track all security-related activities

### 2. Cosmetics Business Management System (`proyecto-BaseDatos-main`)

A complete inventory and sales management system for cosmetics retail operations, fully integrated with the security module.

#### Core Capabilities
- Product catalog management with image support
- Inventory tracking with stock levels and expiration dates
- Sales transaction processing with multiple payment methods
- Purchase order management from suppliers
- Customer relationship management
- Permission-based feature access control

#### Key Modules

**Product Management (Cosmeticos)**
- Add, edit, and manage cosmetic products
- Track product details: name, brand, price, stock, expiration, category
- Product image management
- Active/inactive status management
- Soft delete for products with sales history

**Sales Management (Ventas)**
- Process customer sales transactions
- Support multiple payment methods
- Apply customer loyalty points
- Track sales history
- Generate sales reports
- Real-time inventory updates

**Purchase Management (Compras)**
- Create purchase orders to suppliers
- Track purchase history
- Manage supplier relationships
- Automatic inventory replenishment
- Purchase status tracking

**Customer Management (Consumidores)**
- Customer registration and profiles
- Contact information management
- Purchase history tracking
- Loyalty program integration

## Technology Stack

### Development Platform
- **C# .NET Framework 4.7.2** - Core development platform
- **Windows Forms** - Desktop UI framework
- **Visual Studio 2019+** - Integrated development environment
- **ADO.NET** - Data access framework

### Database
- **Oracle Database 21c** - Enterprise relational database
- **PL/SQL** - Stored procedures and database logic
- **Oracle Managed Data Access (ODP.NET) 23.8.0** - Oracle connectivity

### Security & Libraries
- **BCrypt.Net-Next 4.0.3** - Industry-standard password hashing
- **System.Text.Json 8.0.5** - JSON serialization
- **System.Configuration** - Application configuration management

### Design Patterns
- **Three-Tier Architecture** - Separation of concerns
- **Repository Pattern** - Data access abstraction
- **Factory Pattern** - Object creation
- **Singleton Pattern** - Database connection management

## Key Features

### Security Features

#### Authentication & Authorization
- Secure login with BCrypt password hashing
- Session management and tracking
- Role-based access control (RBAC)
- Permission inheritance with user-level overrides
- Multi-system user assignment

#### Permission Granularity
- **Window-Level Permissions**: Control access to specific forms/modules
- **Operation-Level Permissions**: Read, Write (Create/Update), Delete
- **Role-Based Permissions**: Assign permissions to roles
- **Direct User Permissions**: Override role permissions for specific users
- **Dynamic Permission Checking**: Real-time validation during application use

#### Audit & Compliance
- **Security Audit Trail (BitacoraSeguridad)**: 
  - Track all administrative actions
  - Record user/role/permission changes
  - Log system configuration modifications
  - Timestamp all operations
  
- **Access Logging (BitacoraAcceso)**:
  - Record all login attempts
  - Track user session information
  - Monitor access patterns
  - Support compliance requirements

### Business Features

#### Inventory Management
- Real-time stock tracking
- Low stock alerts
- Expiration date monitoring
- Product categorization
- Multi-brand support
- Product status management (active/inactive)

#### Sales Processing
- Point-of-sale transactions
- Multiple payment methods (cash, credit, debit, transfer)
- Customer loyalty point integration
- Automatic inventory deduction
- Sales history and reporting
- Transaction status tracking

#### Purchase Management
- Supplier management
- Purchase order creation
- Automatic stock replenishment
- Purchase history tracking
- Payment method recording
- Order status management

#### Customer Management
- Customer profiles with contact information
- Purchase history tracking
- Loyalty program management
- Customer search and filtering

#### Permission-Controlled Access
- Form-level access control
- Feature visibility based on permissions
- Dynamic button enabling/disabling
- DataGrid operation control
- Role-based UI customization

## Database Design

### Security Schema (adan)

#### Core Tables

**Usuario (User)**
```sql
- nombreUsuario VARCHAR2(250) PK
- clave VARCHAR2(250)          -- BCrypt hashed
- status VARCHAR2(50)           -- Active/Inactive
```

**Sistemas (Systems)**
```sql
- nombreSistema VARCHAR2(50) PK
- descripcion VARCHAR2(50)
```

**Rol (Role)**
```sql
- idRol INT PK
- nombreRol VARCHAR2(250)
- status VARCHAR2(50)
- nombreSistema VARCHAR2(50) FK
```

**Ventana (Window/Form)**
```sql
- idVentana INT PK
- nombreVentana VARCHAR2(250)
- nombreSistema VARCHAR2(50) FK
- status VARCHAR2(50)
```

**Permisos (Permissions)**
```sql
- idPermisos INT PK
- nombrePermiso VARCHAR2(50)    -- Lectura/Escritura/Eliminacion
```

#### Relationship Tables

**SistemaUsuario**: User-System assignments  
**UsuarioRol**: User-Role assignments  
**VentanaRol**: Role-Window-Permission assignments  
**VentanaUsuario**: Direct user-Window-Permission assignments  

#### Audit Tables

**BitacoraSeguridad**: Security event logging  
**BitacoraAcceso**: User access logging  

### Business Schema (negocio1/negocio2)

#### Core Tables

**Cosmetico (Product)**
```sql
- IDCosmetico INT PK (Sequence-generated)
- Nombre VARCHAR2(100)
- Marca VARCHAR2(100)
- PrecioUnitario NUMBER(10,2)
- StockDisponible INT
- FechaVencimiento DATE
- Categoria VARCHAR2(50)
- EstadoProducto VARCHAR2(20)   -- Activo/Inactivo
- Imagen VARCHAR2(500)
```

**Consumidor (Customer)**
```sql
- IDConsumidor INT PK
- Nombre VARCHAR2(100)
- Apellido VARCHAR2(100)
- Email VARCHAR2(150)
- FechaNacimiento DATE
- EstadoConsumidor VARCHAR2(20)
- PuntosAcumulados INT
```

**Venta (Sale)**
```sql
- IDVenta INT PK
- FechaVenta DATE
- TotalVenta NUMBER(10,2)
- MetodoPago VARCHAR2(50)
- PuntosUsados INT
- IDConsumidor INT FK
- EstadoVenta VARCHAR2(20)
```

**Compra (Purchase)**
```sql
- IDCompra INT PK
- FechaCompra DATE
- TotalCompra NUMBER(10,2)
- MetodoPago VARCHAR2(50)
- Proveedor VARCHAR2(100)
- CantidadProductos INT
- EstadoCompra VARCHAR2(20)
- IDCosmetico INT FK
```

**CosmeticoVenta**: Sale-Product relationship with quantities  
**CosmeticoCompra**: Purchase-Product relationship with quantities  

### Stored Procedures

#### Security Procedures
- `SP_LOGIN` - User authentication
- `SP_INS_USUARIO` / `SP_UPD_USUARIO` / `SP_DEL_USUARIO` - User CRUD
- `SP_INS_ROL` / `SP_UPD_ROL` / `SP_DEL_ROL` - Role CRUD
- `SP_INS_VENTANA` / `SP_UPD_VENTANA` / `SP_DEL_VENTANA` - Window CRUD
- `SP_INS_USUARIOROL` / `SP_DEL_USUARIOROL` - Role assignments
- `SP_INS_VENTANAROL` / `SP_DEL_VENTANAROL` - Role permissions
- `SP_INS_VENTANAUSUARIO` / `SP_DEL_VENTANAUSUARIO` - Direct user permissions
- `SP_MOST_PERMISOS_COMPLETOS_USUARIO` - Get complete user permissions
- `SP_MOST_USUARIO` / `SP_MOST_ROL` / `SP_MOST_VENTANA` - Retrieve operations

#### Business Procedures
- `Sp_Ins_Cosmeticos` / `Sp_Upd_Cosmeticos` / `Sp_Del_Cosmeticos` - Product CRUD
- `Sp_Ins_Venta` / `Sp_Upd_Venta` / `Sp_Del_Venta` - Sales CRUD
- `Sp_Ins_Compra` / `Sp_Upd_Compra` / `Sp_Del_Compra` - Purchase CRUD
- `Sp_Ins_Consumidor` / `Sp_Upd_Consumidor` / `Sp_Del_Consumidor` - Customer CRUD
- Product search and filtering procedures
- Sales and purchase reporting procedures

## Security Implementation

### Password Security
1. **BCrypt Hashing**: Passwords are hashed using BCrypt with automatic salt generation
2. **One-Way Encryption**: Original passwords are never stored or retrievable
3. **Secure Comparison**: BCrypt.Verify() provides timing-attack resistant comparison

### Access Control Flow

```
User Login
    ↓
Credential Validation (BCrypt)
    ↓
Load User Systems
    ↓
System Selection
    ↓
Load Complete Permissions (SP_MOST_PERMISOS_COMPLETOS_USUARIO)
    ↓
    ├─ Role-Based Permissions
    ├─ Direct User Permissions (Override)
    └─ Window Permissions (Read/Write/Delete)
    ↓
Apply UI Permissions
    ├─ Hide/Show Forms
    ├─ Enable/Disable Buttons
    ├─ Control DataGrid Operations
    └─ Dynamic Feature Access
```

### Permission Precedence
1. **Direct User Permissions**: Highest priority, overrides role permissions
2. **Role Permissions**: Applied when no direct user permission exists
3. **Default**: No access if neither is defined

### Real-Time Permission Checking
```csharp
// Example: Permission-controlled UI
public VentanaCosmetico(PermisosVentana permisos)
{
    btn_agregar.Visible = permisos.PuedeCrear;
    btn_editar.Enabled = permisos.PuedeActualizar;
    btn_eliminar.Visible = permisos.PuedeEliminar;
    dtgDatos.Enabled = permisos.PuedeLeer;
}
```

## Prerequisites

### Required Software
- **Operating System**: Windows 10/11 or Windows Server 2016+
- **.NET Framework**: Version 4.7.2 or higher
- **Oracle Database**: 21c or higher (19c compatible)
- **Oracle Client**: Oracle Data Provider for .NET (ODP.NET)

### Development Requirements
- **Visual Studio**: 2019 or later
- **.NET Framework 4.7.2 SDK**
- **Oracle Developer Tools for Visual Studio** (optional, for SQL editing)

## Installation

### 1. Database Setup

#### Step 1: Create Tablespace
```sql
CREATE TABLESPACE proyecto
DATAFILE 'C:\oracle\product\21c\oradata\XE\proyecto01' SIZE 100M;
```

#### Step 2: Create Users
```sql
ALTER SESSION SET "_ORACLE_SCRIPT"=true;

-- Security schema owner
CREATE USER adan
IDENTIFIED BY ucr2025
DEFAULT TABLESPACE proyecto
TEMPORARY TABLESPACE temp;

GRANT CONNECT, RESOURCE TO adan;
ALTER USER adan QUOTA UNLIMITED ON proyecto;

-- Business system users
CREATE USER negocio1 IDENTIFIED BY ucr2025;
CREATE USER negocio2 IDENTIFIED BY ucr2025;
GRANT CONNECT, RESOURCE TO negocio1;
GRANT CONNECT, RESOURCE TO negocio2;
ALTER USER negocio1 QUOTA UNLIMITED ON proyecto;
ALTER USER negocio2 QUOTA UNLIMITED ON proyecto;

-- Grant audit logging access
GRANT SELECT, INSERT ON adan.BitacoraSeguridad TO negocio1;
GRANT SELECT, INSERT ON adan.BitacoraAcceso TO negocio1;
```

#### Step 3: Execute Database Scripts

Connect as `adan` user and run scripts in order:

1. **SeguridadScript.sql** (in modulo-seguridadBD-main/)
   - Creates security tables
   - Creates stored procedures
   - Inserts base data (permissions, default system)

2. **negocio1Scrip.sql** (in modulo-seguridadBD-main/)
   - Creates business tables (Cosmetico, Venta, Compra, Consumidor)
   - Creates business stored procedures
   - Sets up sequences

3. **ProyectoBaseDatos.sql** (in modulo-seguridadBD-main/)
   - Additional configurations
   - Sample data (optional)

### 2. Application Setup

#### Clone/Download Project
```bash
cd C:\Projects
# Extract or clone the repository
```

#### Security Module Setup

1. Open solution:
   ```
   modulo-seguridadBD-main\modulo-seguridadBD\modulo-seguridadBD.sln
   ```

2. Restore NuGet packages (automatically restored on build):
   - Oracle.ManagedDataAccess (23.8.0)
   - BCrypt.Net-Next (4.0.3)
   - System.Text.Json (8.0.5)

3. Configure connection in `App.config`:
   ```xml
   <connectionStrings>
     <add name="OracleConexion" 
          connectionString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=adan;Password=ucr2025;" 
          providerName="Oracle.ManagedDataAccess.Client"/>
   </connectionStrings>
   ```

4. Build solution (Ctrl+Shift+B)

#### Business Module Setup

1. Open solution:
   ```
   proyecto-BaseDatos-main\Examen\Examen\ExamenGrupo5\ExamenGrupo5.sln
   ```

2. Restore NuGet packages

3. Configure connections in `App.config`:
   ```xml
   <connectionStrings>
     <!-- Business system connection -->
     <add name="OracleSistem" 
          connectionString="Data Source=localhost:1521/XE;User Id=negocio1;Password=ucr2025;" 
          providerName="Oracle.ManagedDataAccess.Client"/>
     
     <!-- Security system connection -->
     <add name="OracleConnection" 
          connectionString="Data Source=localhost:1521/XE;User Id=adan;Password=ucr2025;" 
          providerName="Oracle.ManagedDataAccess.Client"/>
   </connectionStrings>
   ```

4. Build solution

### 3. Initial Configuration

#### Create Administrator User
Connect to Oracle as `adan` and execute:
```sql
-- Insert admin user
EXEC Sp_Ins_Usuario('admin', 'admin123', 'activo');

-- Register business system if not exists
INSERT INTO Sistemas VALUES ('negocio1', 'Sistema de Gestión de Cosméticos');

-- Assign user to system
EXEC SP_INS_SISTEMAUSUARIO('negocio1', 'admin');

-- Create admin role
EXEC SP_INS_ROL(1, 'Administrador', 'activo', 'negocio1');

-- Assign role to user
EXEC SP_INS_USUARIOROL('admin', 1);

-- Register business windows
EXEC SP_INS_VENTANA(1, 'Cosmetico', 'negocio1', 'activo');
EXEC SP_INS_VENTANA(2, 'Venta', 'negocio1', 'activo');
EXEC SP_INS_VENTANA(3, 'Compra', 'negocio1', 'activo');
EXEC SP_INS_VENTANA(4, 'Consumidor', 'negocio1', 'activo');

-- Assign full permissions to admin role
EXEC SP_INS_VENTANAROL(1, 1, 1); -- Read
EXEC SP_INS_VENTANAROL(1, 1, 2); -- Write
EXEC SP_INS_VENTANAROL(1, 1, 3); -- Delete
-- Repeat for other windows...
```

## Configuration

### Security Module Configuration

**App.config** (modulo-seguridadBD\modulo-seguridadBD\App.config)
```xml
<configuration>
  <connectionStrings>
    <add name="OracleConexion" 
         connectionString="Data Source=YOUR_TNS_NAME;User Id=adan;Password=YOUR_PASSWORD;" 
         providerName="Oracle.ManagedDataAccess.Client"/>
  </connectionStrings>
</configuration>
```

### Business Module Configuration

**App.config** (ExamenGrupo5\App.config)
```xml
<configuration>
  <connectionStrings>
    <!-- Business data connection -->
    <add name="OracleSistem" 
         connectionString="Data Source=YOUR_TNS_NAME;User Id=negocio1;Password=YOUR_PASSWORD;"/>
    
    <!-- Security validation connection -->
    <add name="OracleConnection" 
         connectionString="Data Source=YOUR_TNS_NAME;User Id=adan;Password=YOUR_PASSWORD;"/>
  </connectionStrings>
</configuration>
```

### Connection String Formats

**Easy Connect**
```
Data Source=hostname:1521/servicename;User Id=username;Password=password;
```

**TNS Names**
```
Data Source=TNSNAME;User Id=username;Password=password;
```

**Full TNS Descriptor**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=hostname)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=servicename)));User Id=username;Password=password;
```

## Usage

### Security Administration Workflow

#### 1. Launch Security Module
```
Run: modulo-seguridadBD.exe
Login: admin / admin123 (or your created admin user)
```

#### 2. Create Business System
- Navigate to **System Administration**
- Click **Create System**
- Enter system name and description
- Save

#### 3. Register Application Windows
- Select system
- Navigate to **Window Management**
- Click **Create Window**
- Enter window name (must match code: "Cosmetico", "Venta", etc.)
- Save

#### 4. Create Roles
- Navigate to **Role Management**
- Select target system
- Click **Create Role**
- Enter role name (e.g., "Sales Manager", "Inventory Clerk")
- Assign window permissions:
  - Lectura (Read): View data
  - Escritura (Write): Create/modify data
  - Eliminacion (Delete): Delete data
- Save role

#### 5. Create Users
- Navigate to **User Management**
- Click **Create User**
- Enter username and password
- Select user status (active/inactive)
- Assign to systems
- Assign roles or direct permissions
- Save

#### 6. Manage User Permissions
- Select user
- Click **User Permissions**
- View inherited role permissions
- Add direct permissions (override role permissions)
- Save changes

### Business Application Workflow

#### 1. Launch Business Application
```
Run: ExamenGrupo5.exe
Login with user created in security module
Select system: negocio1
```

#### 2. Product Management
- Navigate to **Cosmetics** (if permission granted)
- Add new products with image
- Edit existing products
- Search and filter products
- Manage product status
- Track expiration dates

#### 3. Sales Processing
- Navigate to **Sales**
- Select customer
- Add products to sale
- Apply loyalty points
- Choose payment method
- Process transaction
- Print receipt (if implemented)

#### 4. Purchase Orders
- Navigate to **Purchases**
- Create purchase order
- Select supplier
- Add products and quantities
- Record payment method
- Save order
- Track order status

#### 5. Customer Management
- Navigate to **Customers**
- Add new customers
- Update customer information
- View purchase history
- Track loyalty points

### Permission Testing

To verify permission system:

1. Create two users with different roles
2. Assign different permissions to each role
3. Login as each user
4. Observe UI differences:
   - Available menu items
   - Visible buttons
   - Enabled operations
   - Accessible forms

## Project Structure

```
Proyecto-DataBase/
│
├── modulo-seguridadBD-main/              # Security Management System
│   ├── modulo-seguridadBD/
│   │   ├── modulo-seguridadBD.sln
│   │   ├── BLL/                          # Business Logic Layer
│   │   │   └── BLL.csproj
│   │   ├── DAL/                          # Data Access Layer
│   │   │   ├── ConexionOracle.cs        # Database operations
│   │   │   ├── App.config
│   │   │   └── DAL.csproj
│   │   └── modulo-seguridadBD/          # Presentation Layer
│   │       ├── Program.cs
│   │       ├── VentanaLogin.cs          # Authentication
│   │       ├── VentanaAdministrador.cs  # Main dashboard
│   │       ├── CrearUsuario.cs          # User management
│   │       ├── VentanaAgregarRol.cs     # Role management
│   │       ├── VentanaPermisosUsuario.cs
│   │       ├── CrearVentana.cs          # Window registration
│   │       └── modulo-seguridadBD.csproj
│   ├── SeguridadScript.sql              # Security schema DDL/DML
│   ├── negocio1Scrip.sql               # Business schema DDL/DML
│   ├── ProyectoBaseDatos.sql           # Additional scripts
│   └── README.md
│
├── proyecto-BaseDatos-main/              # Business Management System
│   ├── Examen/                           # Version 1
│   │   └── Examen/
│   │       ├── BLL/                      # Business entities
│   │       │   ├── Cosmetico.cs
│   │       │   ├── Venta.cs
│   │       │   ├── Compra.cs
│   │       │   └── Consumidor.cs
│   │       ├── DAL/                      # Data access
│   │       │   ├── OracleConexion.cs    # Business operations
│   │       │   └── OracleConexionSeguridad.cs  # Permission queries
│   │       └── ExamenGrupo5/            # UI application
│   │           ├── ExamenGrupo5.sln
│   │           ├── Program.cs
│   │           ├── VentanaLogin.cs      # Integrated authentication
│   │           ├── Principal.cs         # Main menu with permissions
│   │           ├── VentanaCosmetico.cs  # Product management
│   │           ├── VentanaVentas.cs     # Sales processing
│   │           ├── VentanaCompras.cs    # Purchase orders
│   │           ├── PermisosVentana.cs   # Permission model
│   │           └── imagenes/            # Product images
│   ├── Examen2/                          # Version 2 (backup/alternative)
│   └── README.md
│
└── README.md                             # This file
```

## Screenshots

### Security Module

**Login Screen**
- User authentication with credentials
- System validates against encrypted passwords
- Grants access based on user status

**Administration Dashboard**
- User listing with search/filter
- System selector for multi-system environment
- Quick access to management functions

**User Management**
- Create/edit user accounts
- Assign systems and roles
- Direct permission assignment
- User status control

**Role Management**
- Define roles per system
- Assign window permissions
- Role status management
- Permission matrix view

**Permission Management**
- Visual permission assignment
- Role vs. direct user permissions
- Read/Write/Delete permission levels
- Window-level granularity

### Business Application

**Product Catalog**
- Grid view with product details
- Add/edit/delete operations
- Image upload support
- Permission-controlled buttons

**Sales Interface**
- Customer selection
- Product selection with images
- Shopping cart functionality
- Payment processing

**Purchase Orders**
- Supplier management
- Product ordering
- Stock replenishment
- Order tracking

**Reports & Analytics**
- Sales reports
- Inventory status
- Customer analytics
- Purchase history

## Development

### Building from Source

#### Security Module
```powershell
cd modulo-seguridadBD-main\modulo-seguridadBD
dotnet restore  # or use Visual Studio NuGet restore
msbuild modulo-seguridadBD.sln /p:Configuration=Release
```

#### Business Module
```powershell
cd proyecto-BaseDatos-main\Examen\Examen\ExamenGrupo5
dotnet restore
msbuild ExamenGrupo5.sln /p:Configuration=Release
```

### Development Guidelines

#### Code Style
- Follow C# naming conventions (PascalCase for classes, camelCase for locals)
- Use meaningful variable names
- Comment complex business logic
- Document public methods

#### Database Guidelines
- All data operations through stored procedures
- Use parameterized queries (OracleParameter)
- Implement proper transaction management
- Handle exceptions with try-catch-finally
- Always close connections in finally blocks

#### Security Best Practices
- Never log or display passwords
- Validate all user inputs
- Use BCrypt for password operations
- Check permissions before operations
- Log security events to BitacoraSeguridad

#### Testing Checklist
- [ ] User creation with valid/invalid data
- [ ] Role assignment and permission inheritance
- [ ] Direct permission override
- [ ] Multi-system user access
- [ ] Password encryption verification
- [ ] Audit log completeness
- [ ] UI permission enforcement
- [ ] Database constraint validation
- [ ] Concurrent user sessions
- [ ] Error handling and recovery

### Debugging

#### Connection Issues
```csharp
// Test connection in DAL
try
{
    _connection.Open();
    Console.WriteLine("Connection successful");
}
catch (OracleException ex)
{
    Console.WriteLine($"Oracle Error: {ex.Message}");
    Console.WriteLine($"Error Code: {ex.Number}");
}
```

#### Permission Issues
- Check SP_MOST_PERMISOS_COMPLETOS_USUARIO output
- Verify window names match exactly
- Confirm user has system access
- Check role assignments

#### Common Issues
1. **Connection Timeout**: Check TNS configuration, Oracle listener status
2. **Permission Denied**: Verify role assignments, check direct permissions
3. **Stored Procedure Not Found**: Ensure procedures created under correct schema
4. **Password Not Matching**: Verify BCrypt.Verify() implementation

### Extending the System

#### Adding New Business System
1. Create database user (e.g., negocio3)
2. Grant permissions to audit tables
3. Execute business schema script
4. Register system in Sistemas table
5. Register windows in Ventana table
6. Create roles and assign permissions

#### Adding New Window/Form
1. Create Windows Form class
2. Register in database: `SP_INS_VENTANA`
3. Add to permission check in Principal.cs
4. Implement PermisosVentana constructor
5. Apply permissions to UI elements
6. Test with different roles

#### Adding New Permission Type
1. Insert into Permisos table
2. Update PermisosVentana class
3. Modify permission query procedures
4. Update UI to check new permission
5. Update documentation

## Performance Considerations

- **Connection Pooling**: Enabled by default in ODP.NET
- **Stored Procedures**: Reduced network traffic, improved security
- **Lazy Loading**: Permissions loaded once per session
- **Caching**: Consider caching permission data
- **Indexed Queries**: Database indexes on foreign keys
- **DataGrid Pagination**: Implement for large datasets

## Security Considerations

### Production Deployment
- Change all default passwords
- Use environment-specific connection strings
- Enable Oracle auditing
- Implement session timeout
- Add login attempt limiting
- Use SSL/TLS for database connections
- Regular security audits
- Backup encryption keys
- Implement password complexity rules
- Regular password rotation policy

### Data Protection
- Sensitive data encryption at rest
- Secure connection strings (encrypted config)
- Principle of least privilege
- Regular audit log review
- Backup and disaster recovery plan

## Future Enhancements

- [ ] Web-based administration portal
- [ ] RESTful API for mobile applications
- [ ] Advanced reporting and analytics
- [ ] Email notifications for security events
- [ ] Two-factor authentication
- [ ] LDAP/Active Directory integration
- [ ] Password reset functionality
- [ ] Session management dashboard
- [ ] Real-time permission updates
- [ ] Multi-language support
- [ ] Export/import configuration
- [ ] Role templates
- [ ] Permission approval workflow

## Contributing

When contributing to this project:
1. Maintain three-tier architecture separation
2. Follow established naming conventions
3. Document all new stored procedures
4. Update audit logging for new operations
5. Write unit tests for business logic
6. Update this README with new features

## License

This project was developed as an academic demonstration of enterprise database management and security architecture. Please ensure compliance with your institution's policies and Oracle licensing requirements when deploying.

## Acknowledgments

This system demonstrates:
- Enterprise application architecture
- Database security best practices
- Role-based access control implementation
- Multi-tier application design
- Oracle PL/SQL programming
- Windows Forms development
- Password security (BCrypt)
- Audit trail implementation
