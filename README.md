# Proyecto Biblioteca

Este es un proyecto de gestión de usuarios para una biblioteca, desarrollado con ASP.NET Core 8.0. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Borrar) sobre los usuarios de la biblioteca.

## Características

- **Autenticación y Autorización**: Utiliza autenticación basada en cookies y roles para controlar el acceso a diferentes partes de la aplicación.
- **Operaciones CRUD**: Permite crear, leer, actualizar y borrar usuarios.
- **Hashing de Contraseñas**: Utiliza BCrypt para el hashing de contraseñas.
- **Interfaz de Usuario**: Utiliza Bootstrap para una interfaz de usuario responsiva y moderna.

## Requisitos

- .NET 8.0 SDK
- Visual Studio 2022 o superior
- SQL Server (o cualquier base de datos compatible con Entity Framework Core)

## Configuración del Proyecto

1. **Clonar el repositorio**:
~~~
git clone https://github.com/tu-usuario/proyecto-biblioteca.git
cd proyecto-biblioteca
~~~
2. **Configurar la base de datos**:
    - Actualiza la cadena de conexión en `appsettings.json` con los detalles de tu base de datos.
~~~
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu-servidor;Database=tu-base-de-datos;User Id=tu-usuario;Password=tu-contraseña;"
  }
}
~~~
3. **Aplicar las migraciones**:
~~~
dotnet ef database update
~~~
4. **Ejecutar el proyecto**:
~~~
dotnet run
~~~
## Estructura del Proyecto

- **Controllers**: Contiene los controladores de la aplicación.
- **Models**: Contiene las clases de modelo de datos.
- **Views**: Contiene las vistas Razor.
- **wwwroot**: Contiene archivos estáticos como CSS, JavaScript e imágenes.

## Uso

### Autenticación

- **Login**: Navega a `/User/Login` para iniciar sesión.
- **Logout**: Navega a `/User/Logout` para cerrar sesión.

### Gestión de Usuarios

- **Crear Usuario**: Navega a `/User/Create` para crear un nuevo usuario.
- **Editar Usuario**: Navega a `/User/Edit_User/{id}` para editar un usuario existente.
- **Borrar Usuario**: Navega a `/User/Delete/{id}` para borrar un usuario existente.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o un pull request para discutir cualquier cambio que te gustaría realizar.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.

## Contacto

Para cualquier consulta o sugerencia, por favor contacta a [tu-email@dominio.com](mailto:tu-email@dominio.com).

