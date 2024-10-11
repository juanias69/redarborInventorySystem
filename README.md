# API de Gestión de Inventario

Este proyecto es una API RESTful para gestionar un sistema de inventario de productos. Está desarrollado usando **.NET 8**, **Entity Framework** para operaciones de lectura, y **Dapper** para operaciones de escritura. El proyecto sigue el **patrón CQRS** (Command Query Responsibility Segregation) y aplica buenas prácticas como los **principios SOLID**, **Clean Code**, y **Desarrollo Guiado por Pruebas (TDD)**. La aplicación está dockerizada para facilitar su despliegue e incluye tanto la API como la base de datos SQL Server.

## Tabla de Contenidos
1. [Características del Proyecto](#características-del-proyecto)
2. [Tecnologías Usadas](#tecnologías-usadas)
3. [Requisitos](#requisitos)
4. [Instrucciones de Configuración](#instrucciones-de-configuración)
    - [1. Clonar el Repositorio](#1-clonar-este-repositorio)
    - [2. Navegar a la Carpeta](#2-navegar-a-la-carpeta-donde-se-encuentra-el-archivo-docker-composeyml)
    - [3. Crear los Contenedores](#3-crear-los-contenedores-ejecutando-el-siguiente-comando)
    - [4. Verificar que los Contenedores Estén Corriendo](#4-verificar-que-los-contenedores-estén-corriendo)
    - [5. Acceder a la API](#5-acceder-a-la-api)
    - [6. Ejecutar las Pruebas Unitarias](#6-ejecutar-las-pruebas-unitarias)
5. [Endpoints de la API](#endpoints-de-la-api)
6. [Esquema de Base de Datos](#esquema-de-base-de-datos)
7. [Autenticación](#autenticación)

---

## Características del Proyecto
- Operaciones CRUD para **Productos** y **Categorías**.
- Registro de movimientos de inventario (entrada/salida).
- Implementa **CQRS** para separar operaciones de lectura y escritura.
- Autenticación basada en **OAuth2**.
- Entorno completamente dockerizado para la API y la base de datos SQL Server.
- Documentación de endpoints con Swagger.
- Incluye pruebas unitarias para servicios clave.

## Tecnologías Usadas
- **Backend**: .NET 8.0, ASP.NET Core Web API
- **ORM para Lecturas**: Entity Framework Core
- **Micro ORM para Escrituras**: Dapper
- **Base de Datos**: SQL Server (Dockerizada)
- **Autenticación**: OAuth2 con JWT
- **Pruebas**: xUnit

---

## Requisitos
- [Docker](https://www.docker.com/get-started) (para la containerización)
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (para desarrollo y pruebas locales)

---

## Instrucciones de Configuración

### 1. Clonar este repositorio

    ```bash
    git clone https://github.com/tu-usuario/redarborInventorySystem.git
    cd redarborInventorySystem
    ```
    
### 2. Navegar a la carpeta donde se encuentra el archivo `docker-compose.yml` para la base de datos:

    ```bash
    cd docker/DataBase
    ```

### 3. Crear los contenedores ejecutando el siguiente comando:

    ```bash
    docker-compose up --build
    ```

### 3. Verificar que los contenedores estén corriendo:

    ```bash
    docker ps
    ```

### 5. Acceder a la API

Una vez que los contenedores estén en funcionamiento, puedes acceder a la API en:
```bash
http://localhost:32770/swagger
```

### 6. Ejecutar las Pruebas Unitarias

Para ejecutar las pruebas unitarias localmente:
```bash
dotnet test
```

## Endpoints de la API

### Productos:
- `GET /products`: Obtener todos los productos.
- `GET /products/{id}`: Obtener un producto por ID.
- `POST /products`: Crear un nuevo producto.
- `PUT /products/{id}`: Actualizar un producto.
- `DELETE /products/{id}`: Eliminar un producto.

### Categorías:
- `GET /api/categories`: Obtener todas las categorías.
- `POST /api/categories`: Crear una nueva categoría.
- `DELETE /api/categories/{id}`: Eliminar una categoría.

### Inventario:
- `POST /inventory`: Registrar un producto en el inventario.
- `PUT /inventory`: Actualizar el registro de un inventario.
- `GET /inventory`: Obtener todo el inventario.
- `GET /inventory/{id}`: Obtener el inventario de un producto.
---

### Inventario:
- `POST /generate-token`: Genera un token para poder acceder a los demas endpoints.

## Esquema de Base de Datos

Las siguientes tablas se utilizan en el sistema:

- **Productos**: Gestiona los productos en el inventario.
- **Categorías**: Clasifica los productos en categorías.
- **Movimientos de Inventario**: Rastrea los movimientos de productos (entradas/salidas).

Script Base de Datos

```bash
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InventoryDb')
BEGIN
    CREATE DATABASE InventoryDb;
END
GO

USE InventoryDb;
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categories')
BEGIN
    CREATE TABLE Categories (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(100) NOT NULL,
        CategoryId INT NOT NULL,
        Price DECIMAL(18,2) NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Inventory')
BEGIN
    CREATE TABLE Inventory (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ProductId INT NOT NULL,
        Quantity INT NOT NULL CHECK (Quantity >= 0), 
        EntryDate SMALLDATETIME NOT NULL DEFAULT GETDATE()
    );
END
```

---

## Autenticación

Esta API está protegida mediante OAuth2 y tokens JWT. Para acceder a los endpoints protegidos, deberás autenticarte y obtener un token.
