# Proyecto — Arquitectura Hexagonal

Solución dividida en varios proyectos (Ports & Adapters). Las carpetas clave
viven en distintos proyectos dentro de la misma solución — revisa todos antes
de generar código.

## Ubicación por proyecto (ajustar nombres reales)
- `Domain` → `Entities/`, `Ports/ERepositories/` (puertos), `/Ports/Repositories/IGRepositories.cs`, `/Ports/Repositories/IUnitOfWork.cs`, `Ports/Services/` (interfaz)
- `Infrastructure` → implementaciones concretas de repositorios y UnitOfWork
- `Infraestructure/Configuration/DependencyInjection` → inyección de dependencias (`AddScoped<Interfaz, Implementacion>`)
- `Domain/DTO's/*` → carpetas requeridas para la generación de los DTO's y sus respectivos mappers(si corresponde).

## Principios a aplicar al normalizar
- SRP: una clase/método, una responsabilidad.
- DIP: las capas superiores dependen de interfaces, no de implementaciones.
- DRY: extraer a Services la lógica duplicada entre UseCases.
- Nombres explícitos e intención clara; evitar métodos "todo en uno".
- Mantener consistencia con el patrón ya usado en el resto de la solución antes de imponer uno nuevo (normalizar ≠ reescribir sin justificación).

## Responsabilidad de cada capa
- **Repositorios**: solo acceso a datos. Sin lógica de negocio.
- **UnitOfWork**: coordina transacciones y expone repositorios. Sin lógica propia.
- **Services**: lógica de dominio/aplicación reutilizable entre casos de uso.
- **UseCases**: orquestan un flujo concreto; acceden a datos solo vía
  Services/UoW, nunca directo a un repositorio.
- Toda dependencia entre capas se referencia contra **interfaces (puertos)**,
  nunca contra la implementación concreta.

## Convenciones
- Interfaz: `I{Entidad}{Rol}` — ej. `IProductoService`
- Implementación: `{Entidad}{Rol}` — ej. `ProductoService`
- Inyección de dependencias por constructor, siempre contra la interfaz.

## Al generar o modificar código
1. Repensa el principio de Responsabilidad Única (FUNDAMENTAL).
2. Analiza `Entities`, `ERepositories`, `IUnitOfWork` e `IGRepositories` de cada proyecto involucrado antes de escribir nada.
3. Si un Service ya existe, evalúa si respeta la separación de
   responsabilidades antes de decidir mantenerlo, ajustarlo o reescribirlo.
4. No mezclar acceso a datos dentro de Services.
5. Evita estructurar DTO's o Mappers dentro de los servicios.
6. Presenta primero el plan de archivos a crear/modificar (Plan Mode) antes de aplicar cambios.