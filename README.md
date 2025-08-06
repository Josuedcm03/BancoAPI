# BancoAPI

Este proyecto es una API REST para la gestión de clientes, cuentas y transacciones bancarias, desarrollada con ASP.NET Core y Entity Framework Core.


## Tecnologías utilizadas

- .NET 8 SDK
- Visual Studio Code o Visual Studio 2022
- SQLite (para la base de datos local)
- ASP.NET
- EF Core

## Configuración del entorno

1. **Clona el repositorio**
   ```pwsh
   git clone https://github.com/Josuedcm03/BancoAPI.git
   cd dotnet
   ```

2. **Restaura los paquetes NuGet**
   ```pwsh
   dotnet restore
   ```

3. **Configura la base de datos**
   - Por defecto, el proyecto usa SQLite y los archivos de configuración están en `BancoAPI/appsettings.json` y `BancoAPI/appsettings.Development.json`.
   - Si deseas aplicar las migraciones manualmente:
     ```pwsh
     dotnet ef database update --project BancoAPI/BancoAPI.csproj
     ```


## Ejecución del proyecto

1. **Compila y ejecuta la API**
   ```pwsh
   dotnet run --project BancoAPI/BancoAPI.csproj
   ```
   - La API estará disponible en `https://localhost:5001` o `http://localhost:5000`.
   - Puedes acceder a la documentación interactiva y probar los endpoints desde Swagger en `https://localhost:5001/swagger` o `http://localhost:5000/swagger`.

2. **Prueba los endpoints**
   - UPuedes usar Swagger, Postman o el archivo `BancoAPI/BancoAPI.http` para probar los endpoints.


### Ejemplos de JSON según las clases

**Cliente**
```json
{
  "nombre": "Juan Perez",
  "fechaNacimiento": "1990-01-01T00:00:00",
  "sexo": "M",
  "ingresos": 25000
}
```

**Cuenta**
```json
{
  "numeroCuenta": "123456789",
  "saldo": 1000,
  "clienteId": 1
}
```

**Transaccion**
```json
{
  "tipo": 0,
  "monto": 200,
  "cuentaId": 1
}
```

## Ejecución de pruebas

1. **Ejecuta las pruebas unitarias**
   ```pwsh
   dotnet test BancoAPI.Tests/BancoAPI.Tests.csproj
   ```
   - Se ejecutarán los tests de los servicios y controladores definidos en la carpeta `BancoAPI.Tests`.

## Estructura del proyecto

- `BancoAPI/` - Código fuente principal de la API
- `BancoAPI.Tests/` - Pruebas unitarias
- `banco.db` - Base de datos SQLite
- `dotnet.sln` - Solución principal

## Notas adicionales

- Las migraciones de Entity Framework se encuentran en la carpeta `BancoAPI/Migrations`.

---