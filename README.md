# 👤 API RESTful de usuarios · ASP.NET Core

API REST desarrollada para la materia de **Arquitectura Orientada a Servicios (SOA)**. Expone un CRUD de usuarios sobre una base de datos **MySQL** y se documenta con **Swagger**.

![C#](https://img.shields.io/badge/C#-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=flat-square&logo=mysql&logoColor=white) ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=white)

## 🔌 Endpoints

| Método | Ruta | Descripción |
| --- | --- | --- |
| `GET` | `/api/Usuario` | Lista los usuarios |
| `POST` | `/api/Usuario` | Crea un usuario (valida datos y que no exista) |
| `PUT` | `/api/Usuario/{username}` | Actualiza un usuario |
| `DELETE` | `/api/Usuario/{username}` | Elimina un usuario |

## 🗂️ Estructura

```
Controllers/UsuarioController.cs   # endpoints del CRUD
Data/DB.cs                         # conexión a MySQL
Models/Usuario.cs                  # modelo de datos
Program.cs                         # configuración de la API y Swagger
```

## 🚀 Cómo ejecutarlo

1. Ten una base de datos MySQL con la tabla `usuarios (Username, Password)`.
2. Ajusta la cadena de conexión en `Data/DB.cs`.
3. Ejecuta:

```bash
dotnet run
```

Abre Swagger en `/swagger` para probar los endpoints.

## 🤝 Equipo

[xxxMichael](https://github.com/xxxMichael) · [PAUL-23VE](https://github.com/PAUL-23VE) · [DavidGR21](https://github.com/DavidGR21) · [ACHEDARK](https://github.com/ACHEDARK)

---

<sub>Parte del portafolio de **Paul Velastegui** · [paul-23ve.github.io](https://paul-23ve.github.io) · [GitHub](https://github.com/PAUL-23VE)</sub>
