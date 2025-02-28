# EstadoCuenta
Proyecto encargado de refelejar los moviemientos en un estado de cuenta

# EstadoDeCuenta

Proyecto encargado de reflejar los movimientos en un estado de cuenta de tarjetas de crédito.

## Descripción

Este proyecto es una aplicación web desarrollada en .NET que permite gestionar estados de cuenta de tarjetas de crédito. Incluye funcionalidades como:

- Visualización de información de tarjetas de crédito.
- Cálculos financieros (interés bonificable, cuota mínima a pagar, monto total a pagar).
- Registro de compras y pagos.
- Filtrado por mes y año.
- Exportación de datos en PDF y Excel.

## Tecnologías Utilizadas

- .NET Core
- ASP.NET Razor Pages
- Bootstrap para el diseño responsivo
- SweetAlert2 para notificaciones interactivas
- iTextSharp para la generación de PDF

## Requisitos Previos

- .NET Core SDK instalado en tu máquina.
- SQL Server o cualquier otro gestor de base de datos compatible con .NET.
- Visual Studio o Visual Studio Code.

## Configuración Inicial

1. Clona el repositorio:
    ```sh
    git clone https://github.com/JulioG8/EstadoDeCuenta.git
    cd EstadoDeCuenta
    ```

2. Restaura los paquetes NuGet:
    ```sh
    dotnet restore
    ```

3. Configura la cadena de conexión en el archivo `appsettings.json` ubicado en `EstadoDeCuenta.Web` y `EstadoDeCuenta.API`.

### Ejemplo de appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR_SQL;Database=EstadoDeCuentaDB;User Id=TU_USUARIO;Password=TU_PASSWORD;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
