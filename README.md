# API de Gestión de Inventario

Este proyecto es una API RESTful para gestionar un sistema de inventario de productos. Está desarrollado usando **.NET 6**, **Entity Framework** para operaciones de lectura, y **Dapper** para operaciones de escritura. El proyecto sigue el **patrón CQRS** (Command Query Responsibility Segregation) y aplica buenas prácticas como los **principios SOLID**, **Clean Code**, y **Desarrollo Guiado por Pruebas (TDD)**. La aplicación está dockerizada para facilitar su despliegue e incluye tanto la API como la base de datos SQL Server.

## Tabla de Contenidos
1. [Características del Proyecto](#características-del-proyecto)
2. [Tecnologías Usadas](#tecnologías-usadas)
3. [Requisitos](#requisitos)
4. [Instrucciones de Configuración](#instrucciones-de-configuración)
    - [1. Clonar el Repositorio](#1-clonar-el-repositorio)
    - [2. Configuración de Docker](#2-configuración-de-docker)
    - [3. Construir y Ejecutar la Aplicación](#3-construir-y-ejecutar-la-aplicación)
    - [4. Acceder a la API](#4-acceder-a-la-api)
    - [5. Ejecutar las Pruebas Unitarias](#5-ejecutar-las-pruebas-unitarias)
5. [Estructura del Proyecto](#estructura-del-proyecto)
6. [Endpoints de la API](#endpoints-de-la-api)
7. [Esquema de Base de Datos](#esquema-de-base-de-datos)
8. [Autenticación](#autenticación)
9. [Contribuciones](#contribuciones)

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
- **Backend**: .NET 6.0, ASP.NET Core Web API
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

### 1. Clonar el Repositorio
Clona el repositorio en tu máquina local:
```bash
git clone https://github.com/tuusuario/inventory-management-api.git
cd inventory-management-api
```

### 2. Configuración de Docker
Este proyecto utiliza Docker y Docker Compose para ejecutar la API y la base de datos SQL Server. Asegúrate de tener Docker instalado en tu máquina.

1. Dockerfile para la API: La API se construye y dockeriza usando el siguiente Dockerfile:
```bash
# Usar la imagen oficial de .NET para construir la API
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryAPI/InventoryAPI.csproj", "InventoryAPI/"]
RUN dotnet restore "InventoryAPI/InventoryAPI.csproj"
COPY . .
WORKDIR "/src/InventoryAPI"
RUN dotnet build "InventoryAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryAPI.dll"]

```

2. Docker Compose: El archivo docker-compose.yml se usa para ejecutar tanto la API como la base de datos en contenedores.
```bash
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:80"
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=InventoryDB;User=sa;Password=Your_password123;

  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      - SA_PASSWORD=Your_password123
      - ACCEPT_EULA=Y
    ports:
      - "1433:1433"

```

3. Script de Inicialización de Base de Datos: Se incluye un script de inicialización de la base de datos (init.sql) que crea automáticamente el esquema necesario cuando la base de datos se inicia.
```bash
CREATE DATABASE InventoryDB;

USE InventoryDB;

CREATE TABLE Categories (
  CategoryId INT PRIMARY KEY IDENTITY(1,1),
  CategoryName NVARCHAR(100) NOT NULL
);

CREATE TABLE Products (
  ProductId INT PRIMARY KEY IDENTITY(1,1),
  Name NVARCHAR(100) NOT NULL,
  Price DECIMAL(18,2),
  Stock INT,
  CategoryId INT
);

CREATE TABLE InventoryMovements (
  MovementId INT PRIMARY KEY IDENTITY(1,1),
  ProductId INT,
  Quantity INT
);

```

### 3. Construir y Ejecutar la Aplicación

Para ejecutar la aplicación con Docker, simplemente ejecuta el siguiente comando en el directorio raíz:
```bash
docker-compose up --build
```

### 4. Acceder a la API

Una vez que los contenedores estén en funcionamiento, puedes acceder a la API en:
```bash
http://localhost:5000/swagger
```

### 5. Ejecutar las Pruebas Unitarias

Para ejecutar las pruebas unitarias localmente:
```bash
dotnet test
```

## Estructura del Proyecto

```bash
InventoryAPI/
├── Controllers/
│   └── ProductsController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── ProductRepository.cs
├── Models/
│   └── Product.cs
├── Services/
│   └── ProductService.cs
├── DTOs/
│   └── ProductDto.cs
├── Program.cs
├── Startup.cs
└── Dockerfile
```

## Endpoints de la API

### Productos:
- `POST /api/products`: Crear un nuevo producto.
- `GET /api/products`: Obtener todos los productos.
- `GET /api/products/{id}`: Obtener un producto por ID.
- `PUT /api/products/{id}`: Actualizar un producto.
- `DELETE /api/products/{id}`: Eliminar un producto.

### Categorías:
- `POST /api/categories`: Crear una nueva categoría.
- `GET /api/categories`: Obtener todas las categorías.
- `DELETE /api/categories/{id}`: Eliminar una categoría.

### Movimientos de Inventario:
- `POST /api/inventory`: Registrar entrada/salida de productos.

---

## Esquema de Base de Datos

Las siguientes tablas se utilizan en el sistema:

- **Productos**: Gestiona los productos en el inventario.
- **Categorías**: Clasifica los productos en categorías.
- **Movimientos de Inventario**: Rastrea los movimientos de productos (entradas/salidas).

---

## Autenticación

Esta API está protegida mediante OAuth2 y tokens JWT. Para acceder a los endpoints protegidos, deberás autenticarte y obtener un token.


