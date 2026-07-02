# API — Verificación de Controladores (Capa API)

Complementa a `Claude.md` (arquitectura hexagonal). Aplica al crear o
normalizar Controllers en el proyecto API. No repite reglas de capas
inferiores — ver `Claude.md` para Repositorios/Services/UseCases.

## Antes de generar código
1. Analiza `Program.cs` (o `Startup.cs` si aplica) y extrae:
   - Configuración de `AddAuthentication` / `AddJwtBearer` (emisor, audiencia,
     firma, expiración, esquema).
   - Políticas declaradas en `AddAuthorization` (`AddPolicy`, roles, claims).
2. Las reglas de JWT viven **solo** en `Program.cs`. El controlador nunca las
   duplica ni las reinterpreta, solo referencia la política/rol ya existente.
3. Si el endpoint necesita una política que no existe, señálalo en el plan
   antes de escribir código — no la inventes dentro del controlador.

## Autenticación y autorización
- Controlador protegido → `[Authorize]` a nivel de clase.
- Restricción más específica que la del controlador → `[Authorize(Policy =
  "...")]` o `[Authorize(Roles = "...")]` a nivel de acción.
- Endpoint público → `[AllowAnonymous]` explícito (nunca dejarlo implícito).
- El controlador no valida ni decodifica el token manualmente; eso es
  responsabilidad del middleware/política configurada en `Program.cs`.

## Etiquetas obligatorias
- `[ApiController]` a nivel de clase.
- `[Route("api/[controller]")]` (o el patrón ya usado en la solución).
- `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]` explícitos por
  acción, con ruta relativa si aplica.
- `[ProducesResponseType(...)]` por cada código de retorno posible del
  método (200/201/400/401/403/404/500).

## Respuestas — `IActionResult`
- Nunca devolver un objeto de dominio directo; siempre un DTO de
  `Domain/DTO's`.
- Éxito: `Ok()` (GET), `Created()`/`CreatedAtAction()` (POST), `NoContent()`
  (PUT/DELETE) — según la operación, no por costumbre.
- Error esperado de negocio: `BadRequest()`, `NotFound()`, `Unauthorized()`,
  `Forbid()`. Nunca `StatusCode(500, ...)` para errores de negocio.
- Excepciones no controladas se delegan al middleware global; sin
  try/catch genérico en el controlador.

## Responsabilidad del Controller
- Solo orquesta: recibe el request, mapea a DTO, delega al
  UseCase/Service correspondiente y traduce el resultado a `IActionResult`.
- Sin lógica de negocio ni acceso directo a Repositorios/UnitOfWork.
- Un método = una acción HTTP = una responsabilidad (SRP).

## Convenciones
- Nombre de clase: `{Entidad}Controller`.
- Nombre de acción: verbo + intención clara (`ObtenerPorId`, `Crear`,
  `Actualizar`, `Eliminar`) — evitar métodos "todo en uno".
- Inyección de dependencias por constructor, siempre contra la interfaz del
  UseCase/Service.

## Al generar o modificar un Controller
1. Analiza `Program.cs` y confirma políticas/roles ya definidos.
2. Verifica el UseCase/Service que el endpoint debe invocar.
3. Aplica las etiquetas obligatorias y el `IActionResult` correcto por caso
   de retorno.
4. Presenta primero el plan de archivos a crear/modificar (Plan Mode) antes
   de aplicar cambios — consistente con `Claude.md`.