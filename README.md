# FarmaCorp-DDD

Sistema académico para la gestión de una farmacia, desarrollado con arquitectura basada en Domain-Driven Design (DDD) utilizando ASP.NET Core y PostgreSQL.

---

## Descripción

FarmaCorp-DDD es un proyecto orientado a la administración de procesos relacionados con productos, usuarios y pedidos dentro de una farmacia. La solución está organizada por capas para mantener una estructura clara, escalable y mantenible.

---

## Tecnologías Utilizadas

* ASP.NET Core
* Entity Framework Core
* PostgreSQL
* Supabase
* Swagger
* Git y GitHub
* JetBrains Rider

---

## Estructura del Proyecto

```text id="12m1k2"
FarmaCorp-DDD/
│
├── API/
├── Application/
├── Core/
└── Infrastructure/
```

### Descripción de Carpetas

| Carpeta        | Descripción                                        |
| -------------- | -------------------------------------------------- |
| API            | Configuración principal, controladores y endpoints |
| Application    | Servicios y lógica de aplicación                   |
| Core           | Entidades y reglas del dominio                     |
| Infrastructure | Persistencia y configuraciones externas            |

---

## Ejecución del Proyecto

### Clonar el repositorio

```bash id="v9q3x1"
git clone https://github.com/usuario/FarmaCorp-DDD.git
```

### Restaurar dependencias

```bash id="h7m2p8"
dotnet restore
```

### Ejecutar migraciones

```bash id="n5k4w6"
dotnet ef database update
```

### Ejecutar la aplicación

```bash id="c3z8r2"
dotnet run
```

---

## Integrantes

* Quiroz Cruz, Borix
* Salas Arenas, Marcelo Aldahir
* Delgado Quispe, Paul Omar
* Sardón Lozano, Máximo
* Trigoso Estefanero, Ricardo Miguel
* Calderón Velarde, Mauricio Alexander
