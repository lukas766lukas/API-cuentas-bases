# Sentinel API

API REST para gestión de usuarios, roles, sesiones y eventos, con autenticación mediante JWT.

## Tecnologías

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core (Code First, migraciones)
- SQL Server
- JWT para autenticación
- Hash de contraseñas
- DTOs

## Funcionalidades

- Registro e inicio de sesión de usuarios
- Autenticación con tokens JWT
- Gestión de sesiones (login / logout)
- Asignación de roles a usuarios
- CRUD de eventos
- Uso de DTOs y mapeos

## Cómo ejecutarlo

1. Clona el repositorio: git clone https://github.com/lukas766lukas/API-cuentas-bases.git
2. Ajusta la cadena de conexión en `Sentinel.Server/appsettings.json` si es necesario.
3. Aplica las migraciones: dotnet ef database update --project Sentinel.Server
4. Ejecuta el proyecto: dotnet run --project Sentinel.Server
5. Abre Swagger en el navegador (la URL aparece en la consola al iniciar).
## Nota
La clave JWT incluida en `appsettings.json` es solo de ejemplo para desarrollo. En producción debe cambiarse y guardarse en variables de entorno o en un gestor de secretos.

## Autor

Lukas - [GitHub](https://github.com/lukas766lukas)
EOF
