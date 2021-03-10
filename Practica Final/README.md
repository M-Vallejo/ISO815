# Proyecto final integración de aplicaciones con tecnología Propietaria-OpenSource

## Base de datos

```sql
    CREATE DATABASE CONTABILIDAD
    GO
    USE CONTABILIDAD
```

- Usuarios
  ```sql
  CREATE TABLE USUARIO (
      USUARIO_ID INT PRIMARY KEY IDENTITY,
      NOMBRE_USUARIO VARCHAR (30) NOT NULL,
      CLAVE VARCHAR(500) NOT NULL,
      NOMBRE VARCHAR (100),
      APELLIDOS VARCHAR (100),
      CORREO VARCHAR(100),
      ROL INT NOT NULL DEFAULT(0),
      FECHA_CREACION DATETIME NOT NULL DEFAULT(GETDATE()),
      ESTADO INT NOT NULL DEFAULT(1)
  )
  ```

- Conceptos de pago
  
  ```sql
    CREATE TABLE CONCEPTO_PAGO (
        CONCEPTO_PAGO_ID INT PRIMARY KEY IDENTITY,
        DESCRIPCION VARCHAR(100) NOT NULL,
        ESTADO INT NOT NULL DEFAULT(1)
    )
  ```

- Proveedores
  
  ```sql
    CREATE TABLE PROVEEDOR (
        PROVEEDOR_ID INT PRIMARY KEY IDENTITY,
        NOMBRE VARCHAR(100) NOT NULL,
        TIPO_PERSONA INT NOT NULL,
        TIPO_DOCUMENTO INT NOT NULL,
        NUMERO_DOCUMENTO VARCHAR(15),
        BALANCE DECIMAL (18,2),
        ESTADO INT NOT NULL DEFAULT(1)
    )
  ```

- Entrada de documentos

  ```sql
    CREATE TABLE ENTRADA_DOCUMENTO (
        NUMERO_DOCUMENTO INT PRIMARY KEY IDENTITY,
        NUMERO_FACTURA INT NOT NULL,
        FECHA_DOCUMENTO DATE NOT NULL,
        MONTO DECIMAL(18,2) NOT NULL,
        FECHA_REGISTRO DATETIME NOT NULL DEFAULT(GETDATE()),
        PROVEEDOR INT NOT NULL FOREIGN KEY REFERENCES PROVEEDOR(PROVEEDOR_ID),
        ESTADO INT NOT NULL DEFAULT(1)
    )
  ```

## Backend

### Requerimientos
  - NET 5 (preferible, 3.1 en su defecto).
  - Web API.
  - Uso de JWT para autenticación (no debe permitir acceder a un recurso que no posea un token de autorización).
  - Uso de EntityFrameworkCore.
  - Integración con base de datos SQL, pero que sea facilmente escalable a MySQL/MariaDB.
  - Las peticiones deben ser retornadas en JSON.
  - La contraseña debe insertarse encriptada (HASH) en base de datos. (Importancia: baja)

### Enums

- Estado
  ```cs
    0 - Inactivo
    1 - Activo
    2 - Eliminado
  ```
- Tipo Documento
  ```cs
    0 - Cedula
    1 - RNC
    2 - Pasaporte
  ```
- Rol
  ```cs
    0 - Usuario
    1 - Adminstrador
  ```
- Tipo persona
  ```cs
    0 - Fisica
    1 - Juridica
  ```

## Frontend

### Requerimientos
- Angular 9 o superior
- Angular Material
- Uso de Guard de autorización (no debe permitir acceso a ningún recurso si no se está autenticado)
- Se debe enviar el token al servidor en las peticiones que lo requieran.
- Se debe pedir confirmación antes de realizar una acción que modifique datos (eliminar, editar, etc...).


### Módulos
- Mantenimiento
  - Usuarios
    - Poder crear, editar, consultar y activar/desactivar usuario.
    - En la pantalla principal se mostrará el listado de usuarios y también un botón para crear nuevo usuario.
    - En la tabla o listado de usuario se debe poder buscar por nombre de usuario, nombre, apellido y por correo.
  - Conceptos de pago
    - Mismas condiciones que usuario a excepción de la búsqueda que se realizará solo por descripción.
  - Proveedores
    - Misma condiciones que conceptos de pago.
- Contabilidad
  - Entrada de documentos
  - Consulta

#### Módulos Usuario
- Mantenimiento
  - Conceptos de pago
  - Proveedores
- Contabilidad
  - Entada de documentos
  - Consulta

#### Módulos Administrador
- Todos

## Token
- Administrador
   ```json
    {
      "nombre": "Michael Vallejo",
      "nombre_usuario": "MVALLEJO",
      "rol": 1
    }
   ```
    > eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJub21icmUiOiJNaWNoYWVsIFZhbGxlam8iLCJub21icmVfdXN1YXJpbyI6Ik1WQUxMRUpPIiwicm9sIjoxfQ.SGzlHreu_WIXZGloxaKQtTzI_7sTYGH_Fo1nftob7Gs

 - Usuario
   ```json
    {
      "nombre": "Javier Mercedes",
      "nombre_usuario": "JMERCEDES",
      "rol": 0
    }
   ```
   > eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJub21icmUiOiJKYXZpZXIgTWVyY2VkZXMiLCJub21icmVfdXN1YXJpbyI6IkpNRVJDRURFUyIsInJvbCI6Mn0.zTKYxEma8-i0MyRV_N-faKno_9dGiiKSyMolPtm0Dbo
