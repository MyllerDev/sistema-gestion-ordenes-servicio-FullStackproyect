# ServiceOrders - Sistema de Gestión de Órdenes de Servicio

## Descripción del proyecto

**ServiceOrders** es una aplicación full stack diseñada para la gestión de órdenes de servicio en una empresa de mantenimiento. Permite administrar clientes, técnicos y órdenes de servicio, incluyendo autenticación segura mediante JWT y consultas con filtros avanzados.

El sistema está dividido en dos capas principales:

- **Backend:** API REST desarrollada en .NET 6+ con Dapper
- **Frontend:** Aplicación SPA desarrollada en Angular

---

# Tecnologías utilizadas

## Backend
- .NET 6 / ASP.NET Core Web API
- Dapper (acceso a datos)
- JWT Authentication
- BCrypt (hash de contraseñas)
- PostgreSQL

## Frontend
- Angular 14+
- TypeScript
- HttpClient
- Interceptors HTTP
- Guards de autenticación
- Standalone Components

## Base de datos
- PostgreSQL

---

# Arquitectura del sistema

## Backend (arquitectura en capas)

- Controllers: Exposición de endpoints REST
- Services: Lógica de negocio
- Repositories: Acceso a datos con Dapper
- DTOs: Transferencia de datos
- Models: Entidades del dominio

Estructura:

ServiceOrders.Api/
├── Controllers/
├── Services/
├── Repositories/
├── DTOs/
├── Models/
├── Data/
└── Program.cs

---

## Frontend (arquitectura modular)

- core/: interceptores, guards, modelos
- features/: módulos funcionales
  - auth/
  - clients/
  - technicians/
  - orders/
- layouts/
- app.routes.ts
- app.config.ts

Estructura:

src/app/
├── core/
├── features/
│   ├── auth/
│   ├── clients/
│   ├── technicians/
│   └── orders/
├── layouts/
└── app.routes.ts

---

# Base de datos

## Tablas principales

### Users
- Id (PK)
- Username (UNIQUE)
- PasswordHash
- Role

### Clients
- Id (PK)
- FullName
- DocumentNumber (UNIQUE)
- Address
- Phone

### Technicians
- Id (PK)
- FullName
- Phone
- Specialty

### ServiceOrders
- Id (PK)
- CreatedDate
- Status
- Description
- TechnicianId (FK)
- ClientId (FK)

---

# Autenticación

El sistema utiliza **JWT (JSON Web Tokens)**.

## Flujo de autenticación

1. El usuario envía credenciales al endpoint:
   - POST /api/auth/login

2. El backend valida:
   - Usuario existe
   - Password coincide usando BCrypt

3. Si es válido:
   - Se genera un token JWT
   - Se retorna al frontend

---

# Pruebas en Swagger

## Login

Endpoint:
POST /api/auth/login

Body:

{
  "username": "admin",
  "password": "Admin123*"
}

## Uso del token

1. Copiar el token retornado
2. Ir a Swagger → botón Authorize
3. Pegar:

Bearer {token}

---

## Endpoints protegidos

- /api/clients
- /api/technicians
- /api/serviceorders

Requieren token JWT válido.

---

# Frontend - Ejecución

## Instalación

npm install

## Ejecución

ng serve

Acceso:
http://localhost:4200

---

# Backend - Ejecución

## Requisitos

- .NET 6 SDK
- PostgreSQL

## Ejecutar

dotnet restore
dotnet build
dotnet run

---

# Funcionalidades principales

## Autenticación
- Login con JWT
- Protección de rutas
- Roles de usuario

## Módulo Clients
- CRUD completo
- Validación de documento único

## Módulo Technicians
- CRUD completo
- Validación de datos

## Módulo Service Orders
- CRUD completo
- Filtros por:
  - Estado
  - Cliente
  - Técnico
  - Rango de fechas

---

# Consideraciones técnicas

- Uso de Dapper (sin LINQ en consultas de filtros)
- Validación de datos en backend
- Seguridad con JWT
- Separación de responsabilidades
- Arquitectura limpia en frontend y backend

---

# Autor
Proyecto desarrollado como prueba técnica full stack.
