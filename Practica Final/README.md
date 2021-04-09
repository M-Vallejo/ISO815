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

- Tipo de documento

  ```sql
  CREATE TABLE TIPO_DOCUMENTO (
      TIPO_DOCUMENTO_ID INT PRIMARY KEY IDENTITY,
      DESCRIPCION VARCHAR(100),
      ESTADO INT NOT NULL DEFAULT(1)
  )
  ```

- Entrada de documentos

  ```sql
  CREATE TABLE ENTRADA_DOCUMENTO (
      NUMERO_DOCUMENTO INT PRIMARY KEY IDENTITY,
      NUMERO_FACTURA INT NOT NULL,
      NUMERO_CHEQUE INT NULL,
      FECHA_DOCUMENTO DATE NOT NULL,
      MONTO DECIMAL(18,2) NOT NULL,
      TIPO_DOCUMENTO INT NOT NULL FOREIGN KEY REFERENCES TIPO_DOCUMENTO(TIPO_DOCUMENTO_ID),
      PROVEEDOR INT NOT NULL FOREIGN KEY REFERENCES PROVEEDOR(PROVEEDOR_ID),
      ID_ASIENTO INT NULL,
      CONDICIONES VARCHAR(255) NULL,
      FECHA_REGISTRO DATETIME NOT NULL DEFAULT(GETDATE()),
      ESTADO INT NOT NULL DEFAULT(1)
  )
  ```

## Backend

### Requerimientos
  - NetCore 3.1.
  - Web API.
  - Uso de JWT para autenticación (no debe permitir acceder a un recurso que no posea un token de autorización).
  - Uso de EntityFrameworkCore.
  - Integración con base de datos SQL Server, pero que sea facilmente escalable a MySQL/MariaDB.
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
### Integración contabilidad

  Clase `AccountingService/ServicioContabilidad`  que contendrá la lógiga necesaria para la integración con el web service de contabilidad.
  - Método de Procesar Asientos que recibirá como parámetros la suma de los montos de los asientos contables (entrada de documentos) enviados por el frontend; los datos de configuración como lo son el Id del Auxiliar, Cuenta débido y cuenta crédito y el texto:
    - > Asiento contable de inventario correspondiente al periodo yyyy-MM
    - Debe devolver un string con el número de asiento contable que devuelva contabilidad.
  - Se deben actualizar los asientos (entrada de documentos) utilizados para enviar a contabilidad, con el id de asiento que envia contabilidad luego de procesar los asientos enviados. El campo a actualizar será el `ID_ASIENTO`.

## Frontend

### Requerimientos
- VueJs
- Uso de autorización (no debe permitir acceso a ningún recurso si no se está autenticado).
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
    - Formulario que permita registrar las entradas de documentos, debe mostrar los siguientes campos:
      - Número de Factura: `number`
      - Número de cheque: `number`
      - Fecha: `Date`
      - Tipo de documento: `Dropdown<Tipo_Documento>`
      - Proveedor: `Dropdown<Proveedor>`
      - Condiciones: `textarea`
  - Contabilidad
    - Muestra una lista de los documentos registrados, filtrados por rango de fecha y un botón de buscar.
    - Los documentos desplegados en esta sección serán enviado a contabilidad cuando se presione el botón de contabilizar.
    - Luego que los documentos se hayan contabilizado, debe hacer una buscqueda nuevamente con los filtros seleccioados arriba.

#### Módulos Usuario
- Mantenimiento
  - Conceptos de pago
  - Proveedores
- Contabilidad
  - Entada de documentos
  - Consulta

#### Módulos Administrador
- Todos

## Configuración
 ```json
 {
   "AppSettings": {
    ...
  },
  "AccountingSettings": {
    "AsientoId": 6,
    "CuentaDebito": 82,
    "CuentaCredito": 4
  },
  ...
 }
 ```

## Token
- Administrador
   ```json
    {
      "id": 1,
      "nombre": "Michael Vallejo",
      "nombre_usuario": "MVALLEJO",
      "rol": 1
    }
   ```
    > eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJub21icmUiOiJNaWNoYWVsIFZhbGxlam8iLCJub21icmVfdXN1YXJpbyI6Ik1WQUxMRUpPIiwicm9sIjoxfQ.SGzlHreu_WIXZGloxaKQtTzI_7sTYGH_Fo1nftob7Gs

 - Usuario
   ```json
    {
      "id": 2,
      "nombre": "Javier Mercedes",
      "nombre_usuario": "JMERCEDES",
      "rol": 0
    }
   ```
   > eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJub21icmUiOiJKYXZpZXIgTWVyY2VkZXMiLCJub21icmVfdXN1YXJpbyI6IkpNRVJDRURFUyIsInJvbCI6Mn0.zTKYxEma8-i0MyRV_N-faKno_9dGiiKSyMolPtm0Dbo

## Endpoint

- ##### Login
    > HTTP POST
    > https://localhost:44348/api/account/Login

    Parámetros (Body)
    ```c#
        username: string
        password: string
    ```
    
    Respuesta
    - 401 (unauthorized) + mensaje si los datos están incorrectos.
    - Token si los datos están correctos.

- ##### Validar y renovar token
    > HTTP GET
    > https://localhost:44348/api/account/IsAuthorized
    
    Parámetros (Header)
    ```c#
        Authorization: string
    ```
    
    Respuesta
    - 401 (unauthorized) si el token es inválido
    - Token renovado si el token anterior es válido y aún no ha expirado
 
 - ###### Obtener Todos los proveedores
       > HTTP GET
       > https://localhost:44348/api/proveedores/get
        
         Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Una lista de Proveedores. 

 - ###### Obtener proveedores por estado
        >HTTP GET
        >https://localhost:44348/api/proveedores/GetProveedoresByEstatus/{Estado}
        

        Parámetros (Params)
        ```c#
            Estado: int
        ```
        Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Lista de proveedores por estado.

- ###### Obtener proveedores por id
        >Http GET
        >https://localhost:44348/api/proveedores/GetProveedorById/id

        Parámetros ()
        ```c#
            id: int
        ```
        
        Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 un proveedor.
    - 404 No encontro el registro.
    
 - ###### Crear Proveedor
        >Http Post
        >https://localhost:44348/api/proveedores/CreateProveedor
        
        Parámetros (Body)
        ```c#
            "nombre": string,
            "tipo_persona": int,
            "tipo_documento": int,
            "numero_documento": string,
            "balance":decimal
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Crea un nuevo proveedor.
    - 500 Error al crear el registro.
    
   - ###### Editar Proveedor
        >Http Put
        >https://localhost:44348/api/proveedores/EditProveedor
              
        Parámetros (Body)
        ```c#
            proveedor_id: int
            nombre: string
            tipo_persona: int
            tipo_documento: int
            numero_documento: string
            balance: decimal: decimal
            estado: int
            
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Edita el registro.
    - 404 Registro no encontrado
    - 500 Error al editar el registro.

  - ###### Editar Proveedor
        >Http Put
        >https://localhost:44348/api/proveedores/ChangeEstadoProveedores?id=5&estado=2
              
        Parámetros (Query)
        ```c#
            id: int,
            estado: int
            
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Edita el registro.
    - 404 Registor no encontrado
    - 500 Error al editar el registro.



 - ###### Obtener Todos los Conceptos de Pago
       > HTTP GET
       > https://localhost:44348/api/ConceptoPago/get
        
         Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Una lista de Concepto de Pagos. 
 - ###### Obtener Conceptos de pagos
        >HTTP GET
        >https://localhost:44348/api/ConceptoPago/GetConceptoPagoByEstatus/{Estado}
        

        Parámetros (Params)
        ```c#
            Estado: int
        ```
        Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Lista de concepto de pagos por estado.

- ###### Obtener Concepto de pagos por id
        >Http GET
        >https://localhost:44348/api/ConceptoPago/GetConceptoPagoById/id

        Parámetros ()
        ```c#
            id: int
        ```
        
        Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Un concepto de pago.
    - 404 No encontro el registro.
    
 - ###### Crear Concepto de pagos
        >Http Post
        >https://localhost:44348/api/ConceptoPago/CreateConceptoPago
        
        Parámetros (Body)
        ```c#
            descripcion: string
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Crea un nuevo concepto de pago.
    - 500 Error al crear el registro.
    
   - ###### Editar Concepto de pagos
        >Http Put
        >https://localhost:44348/api/ConceptoPago/EditConceptoPago
              
        Parámetros (Body)
        ```c#
            Concepto_pago_id: int,
            descripcion: string,
            estado: int
            
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Edita el registro.
    - 404 Registro no encontrado
    - 500 Error al editar el registro.

  - ###### Editar estado de Concepto de pago
        >Http Put
        >https://localhost:44348/api/ConceptoPago/ChangeEstadoConceptoPago?id=5&estado=2
              
        Parámetros (Query)
        ```c#
            id: int,
            estado: int
            
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Edita el registro.
    - 404 Registor no encontrado
    - 500 Error al editar el registro.

- ###### Obtener Todos los Usuarios
       > HTTP GET
       > https://localhost:44348/api/usuario/get
        
         Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Una lista de Usuarios. 

- ###### Obtener usuario por id
        >Http GET
        >https://localhost:44348/api/usuario/GetUsuarioById/1

        Parámetros ()
        ```c#
            id: int
        ```
        
        Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Un Usuario
    - 404 No encontro el registro.


 - ###### Crear usuario
        >Http Post
        >https://localhost:44348/api/usuario/CreateUsuario
        
        Parámetros (Body)
        ```c#
            NOMBRE_USUARIO: string,
            CLAVE": string,
            NOMBRE: string,
            APELLIDOS:string,
            CORREO: string,
            ROl:int
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Crea un nuevo usuario.
    - 500 Error al crear el registro.

 - ###### Editar Usuarios
        >Http Put
        >https://localhost:44348/api/ConceptoPago/EditConceptoPago
              
        Parámetros (Body)
        ```c#
            USUARIO_ID : int,
            NOMBRE_USUARIO: string,
            CLAVE": string,
            NOMBRE: string,
            APELLIDOS:string,
            CORREO: string,
            ROl:int
            ESTADO = int
            
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Edita el registro.
    - 404 Registro no encontrado
    - 500 Error al editar el registro.
    - 
 - ###### Editar estado de usuario
        >Http Put
        >https://localhost:44348/api/ConceptoPago/ChangeEstadoConceptoPago?id=5&estado=2
              
        Parámetros (Query)
        ```c#
            id: int,
            estado: int
            
        ```
            Respuesta
    - 401 (unauthorized) si el token es inválido
    - 200 Edita el registro.
    - 404 Registro no encontrado
    - 500 Error al editar el registro.
    
