# 📖 Documentación de API — SistemaGestionOrquesta Backend

**Base URL:** `https://<host>/`  
**Autenticación:** JWT Bearer Token (se obtiene con `/usuario/login`)  
**Formato:** Todos los body son `application/json`

---

## 🔐 1. Autenticación (UsuarioController)

| Verbo  | Endpoint         | Auth   | Descripción                       |
|--------|------------------|--------|-----------------------------------|
| `POST` | `/usuario/login` | ❌ No  | Inicia sesión y devuelve un JWT   |

### POST /usuario/login

Autentica al usuario y devuelve un token JWT válido por 60 minutos.

**Body:**

    {
      "email": "administrador@gmail.com",
      "password": "123"
    }

**Respuesta exitosa (200):**

    {
      "succes": true,
      "message": "exito",
      "result": "eyJhbGciOiJIUzI1NiIs..."
    }

**Respuesta error (200 con success false):**

    {
      "success": false,
      "message": "Credenciales incorrectas",
      "result": ""
    }

**Claims incluidos en el token:**

- `ClaimTypes.NameIdentifier` → ID del usuario
- `ClaimTypes.Name` → Nombre de usuario
- `ClaimTypes.Role` → Rol (ej: "Admin")

---

## 👨‍🎓 2. Estudiantes (EstudianteController)

> 🔒 Todos los endpoints requieren header `Authorization: Bearer <token>`

| Verbo    | Endpoint                                                 | Roles      | Descripción                              |
|----------|----------------------------------------------------------|------------|------------------------------------------|
| `GET`    | `/estudiantes`                                           | **Admin**  | Lista todos los estudiantes              |
| `GET`    | `/estudiante/{id}`                                       | Cualquiera | Obtiene un estudiante por su ID          |
| `GET`    | `/estudianteNombreYDni/{nombre}/{documento}`             | Cualquiera | Busca estudiante por nombre y documento  |
| `GET`    | `/estudianteNombreApellido/{nombre}/{apellido}`          | Cualquiera | Busca estudiante por nombre y apellido   |
| `GET`    | `/estudiantesByCurso/{idCurso}`                          | Cualquiera | Lista estudiantes activos de un curso    |
| `GET`    | `/cursosByEstudiante/{idEstudiante}`                     | Cualquiera | Lista cursos de un estudiante            |
| `POST`   | `/estudiante/save`                                       | Cualquiera | Crea un nuevo estudiante                 |
| `POST`   | `/estudianteDarDeAltaEnCurso/{id}/{Idcurso}`             | Cualquiera | Inscribe estudiante en un curso          |
| `PUT`    | `/estudiante/update/{id}`                                | Cualquiera | Modifica un estudiante                   |
| `PUT`    | `/estudianteCambiarCurso/{id}/{cursoNuevo}/{cursoViejo}` | Cualquiera | Cambia un estudiante de curso            |
| `DELETE` | `/estudiante/delete/{id}`                                | Cualquiera | Elimina un estudiante                    |
| `DELETE` | `/estudiante/baja/{id}`                                  | Cualquiera | Da de baja un estudiante (baja lógica)   |
| `DELETE` | `/estudianteEliminarCurso/{id}/{Idcurso}`                | Cualquiera | Desvincula estudiante de un curso        |

### Parámetros

- **{id}**: `Guid`
- **{idCurso} / {Idcurso}**: `int`
- **{idEstudiante}**: `Guid`
- **{nombre}, {apellido}, {documento}**: `string` (en URL)
- **{cursoNuevo}, {cursoViejo}**: `int`

### POST /estudiante/save — Body:

    {
      "nombre": "Juan",
      "apellido": "Pérez",
      "documento": "12345678",
      "fechaNacimiento": "2010-05-15",
      "direccion": "Calle 123",
      "telefono": "1155554444",
      "email": "juan@mail.com",
      "nacionalidad": "Argentina",
      "nombreTutor": "María Pérez",
      "telefonoTutor": "1155551111",
      "autoretiro": false,
      "particularidad": "sin observaciones",
      "tmtMédico": "sin observaciones",
      "epPsicoMotriz": "sin observaciones",
      "instrumentoId": 1
    }

### PUT /estudiante/update/{id} — Mismo body que save.

---

## 👨‍🏫 3. Profesores (ProfesorController)

> 🔓 Sin autenticación requerida

| Verbo    | Endpoint                                      | Descripción                  |
|----------|-----------------------------------------------|------------------------------|
| `GET`    | `/profesores`                                 | Lista todos los profesores   |
| `GET`    | `/profesor/{id}`                              | Obtiene un profesor por ID   |
| `GET`    | `/profesorNombreYDni/{nombre}/{documento}`    | Busca por nombre y documento |
| `GET`    | `/profesorNombreApellido/{nombre}/{apellido}` | Busca por nombre y apellido  |
| `POST`   | `/profesor/save`                              | Crea un profesor             |
| `PUT`    | `/profesor/update/{id}`                       | Modifica un profesor         |
| `DELETE` | `/profesor/delete/{id}`                       | Elimina un profesor          |
| `DELETE` | `/profesor/baja/{id}`                         | Baja lógica de un profesor   |

### Parámetros

- **{id}**: `Guid`
- **{nombre}, {apellido}, {documento}**: `string`

### POST /profesor/save — Body:

    {
      "nombre": "Carlos",
      "apellido": "López",
      "documento": "30123456",
      "fechaNacimiento": "1985-03-20",
      "direccion": "Av. Siempreviva 742",
      "telefono": "1166667777",
      "email": "carlos@mail.com"
    }

### PUT /profesor/update/{id} — Mismo body que save.

---

## 🎵 4. Instrumentos (InstrumentoController)

> 🔓 Sin autenticación requerida

| Verbo    | Endpoint                                                 | Descripción                                            |
|----------|----------------------------------------------------------|--------------------------------------------------------|
| `GET`    | `/instrumentos`                                          | Lista todos los instrumentos (catálogo)                |
| `GET`    | `/instrumento/{id}`                                      | Obtiene un instrumento por ID                          |
| `GET`    | `/instrumentoNombre/{nombre}`                            | Busca instrumento por nombre                           |
| `POST`   | `/instrumento/save`                                      | Crea un instrumento in el catálogo                     |
| `POST`   | `/instrumento/prestamo/{idEstudiante}/{idInstrumento}`   | ⚠️ Legacy — usar `api/prestamos/asignar` en su lugar  |
| `POST`   | `/instrumento/devolucion/{idEstudiante}/{idInstrumento}` | ⚠️ Legacy — usar `api/prestamos/devolver` en su lugar |
| `PUT`    | `/instrumento/update/{id}`                               | Modifica un instrumento                                |
| `DELETE` | `/instrumento/delete/{id}`                               | Elimina un instrumento                                 |

### Parámetros

- **{id} / {idInstrumento}**: `int`
- **{idEstudiante}**: `Guid`
- **{nombre}**: `string`

### POST /instrumento/save — Body:

    {
      "nombre": "Violín",
      "detalles": "Violín 4/4 marca Yamaha",
      "disponible": true
    }

### PUT /instrumento/update/{id} — Mismo body que save.

---

## 📦 5. Stock de Instrumentos (StockController)

> 🔓 Sin autenticación requerida

Gestiona los **ejemplares físicos** de cada instrumento del catálogo. Un instrumento (ej: "Violín") puede tener múltiples ejemplares en stock (ej: VL-001, VL-002).

| Verbo    | Endpoint                                | Descripción                                              |
|----------|-----------------------------------------|----------------------------------------------------------|
| `GET`    | `api/stock`                             | Lista todo el stock con su instrumento asociado          |
| `GET`    | `api/stock/{id}`                        | Obtiene un ejemplar por ID                               |
| `GET`    | `api/stock/codigo/{codigoInventario}`   | Busca un ejemplar por código de inventario               |
| `GET`    | `api/stock/disponibles/{instrumentoId}` | Lista ejemplares disponibles de un instrumento           |
| `POST`   | `api/stock`                             | Crea un nuevo ejemplar físico                            |
| `PUT`    | `api/stock/{id}`                        | Modifica un ejemplar (código, serie, estado)             |
| `DELETE` | `api/stock/{id}`                        | Elimina un ejemplar (falla si tiene préstamos abiertos)  |

### Parámetros

- **{id}**: `int`
- **{instrumentoId}**: `int`
- **{codigoInventario}**: `string`

### POST api/stock — Body:

    {
      "instrumentoId": 1,
      "codigoInventario": "VL-001",
      "numeroSerie": "YMH-2024-0001"
    }

### PUT api/stock/{id} — Body (todos los campos son opcionales):

    {
      "codigoInventario": "VL-002",
      "numeroSerie": "YMH-2024-0002",
      "estado": "En reparación"
    }

### Estados posibles de un ejemplar:

- `"Disponible"` — Listo para prestar
- `"Prestado"` — Actualmente prestado a un estudiante
- `"En reparación"` — En mantenimiento (se puede usar vía Update)

---

## 🤝 6. Préstamos (PrestamosController)

> 🔓 Sin autenticación requerida

Gestiona el préstamo y devolución de ejemplares físicos (stock) a estudiantes.

| Verbo  | Endpoint                                  | Descripción                                 |
|--------|-------------------------------------------|---------------------------------------------|
| `GET`  | `api/prestamos`                           | Lista todos los préstamos (historial)       |
| `GET`  | `api/prestamos/activos`                   | Solo préstamos sin devolver                 |
| `GET`  | `api/prestamos/estudiante/{estudianteId}` | Historial de préstamos de un estudiante     |
| `GET`  | `api/prestamos/{id}`                      | Detalle de un préstamo por ID               |
| `POST` | `api/prestamos/asignar`                   | Asigna un ejemplar a un estudiante          |
| `POST` | `api/prestamos/devolver`                  | Registra la devolución de un ejemplar       |

### Parámetros

- **{id}**: `int`
- **{estudianteId}**: `Guid`

### POST api/prestamos/asignar — Body:

    {
      "estudianteId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "stockInstrumentoId": 1
    }

**Lógica interna:**

1. Valida que el ejemplar exista y esté en estado "Disponible"
2. Valida que el estudiante exista
3. Crea el registro de préstamo con FechaPrestamo = ahora
4. Cambia el estado del stock a "Prestado"
5. Actualiza Instrumento.Disponible según queden ejemplares disponibles

### POST api/prestamos/devolver — Body:

    {
      "stockInstrumentoId": 1
    }

**Lógica interna:**

1. Busca el préstamo abierto (sin fecha de devolución) más reciente para ese ejemplar
2. Registra FechaDevolucion = ahora
3. Cambia el estado del stock a "Disponible"
4. Actualiza Instrumento.Disponible

---

## 🎓 7. Cursos (CursoController)

> 🔓 Sin autenticación requerida

| Verbo    | Endpoint                | Descripción             |
|----------|-------------------------|-------------------------|
| `GET`    | `/cursos`               | Lista todos los cursos  |
| `GET`    | `/curso/{id}`           | Obtiene un curso por ID |
| `GET`    | `/cursoNombre/{nombre}` | Busca curso por nombre  |
| `POST`   | `/curso/save`           | Crea un curso           |
| `PUT`    | `/curso/update/{id}`    | Modifica un curso       |
| `DELETE` | `/curso/delete/{id}`    | Elimina un curso        |

### Parámetros

- **{id}**: `int`
- **{nombre}**: `string`

### POST /curso/save — Body:

    {
      "nombre": "Violín Inicial",
      "profesorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }

### PUT /curso/update/{id} — Mismo body que save.

---

## 📊 Códigos de respuesta HTTP

| Código | Significado                                                |
|--------|------------------------------------------------------------|
| `200`  | OK — Operación exitosa                                     |
| `201`  | Created — Recurso creado exitosamente                      |
| `204`  | No Content — Eliminado correctamente                       |
| `400`  | Bad Request — Parámetros inválidos                         |
| `401`  | Unauthorized — Token faltante o inválido                   |
| `403`  | Forbidden — Token válido pero sin permisos (no es Admin)   |
| `404`  | Not Found — Recurso no encontrado                          |
| `409`  | Conflict — Conflicto de datos (duplicado, estado inválido) |
| `500`  | Internal Server Error — Error inesperado del servidor      |
| `504`  | Gateway Timeout — Timeout en la operación                  |

---

## 📋 8. Asistencia (AsistenciaController)

> 🔓 Sin autenticación requerida

Gestiona la toma de asistencia de estudiantes por curso y fecha.

| Verbo  | Endpoint                                  | Descripción                                      |
|--------|-------------------------------------------|--------------------------------------------------|
| `POST` | `api/asistencia`                          | Guarda la asistencia completa de un curso        |
| `GET`  | `api/asistencia?cursoId=X&fecha=Y`        | Consulta asistencia de un curso en una fecha     |
| `GET`  | `api/asistencia/estudiante/{estudianteId}` | Historial de asistencia de un estudiante         |

### Parámetros

- **{estudianteId}**: `Guid`
- **cursoId**: `int` (query string)
- **fecha**: `string (date)` (query string, formato `YYYY-MM-DD`)

### POST api/asistencia — Body:

    {
      "cursoId": 10,
      "fecha": "2026-04-05",
      "asistencias": [
        { "estudianteId": "a1b2c3d4-...", "presente": true },
        { "estudianteId": "e5f6a7b8-...", "presente": false },
        { "estudianteId": "c9d0e1f2-...", "presente": true }
      ]
    }

**Lógica interna:**

1. Valida que el `cursoId` exista
2. Si ya existe asistencia para ese curso y fecha → **la reemplaza** (borra las anteriores e inserta las nuevas dentro de una transacción)
3. Inserta un registro por cada estudiante en la tabla `Asistencia`
4. Devuelve la lista guardada con datos del estudiante y curso

**Respuesta exitosa (201):**

    [
      {
        "asistenciaId": 1,
        "estudianteId": "a1b2c3d4-...",
        "nombreEstudiante": "Juan",
        "apellidoEstudiante": "Pérez",
        "cursoId": 10,
        "nombreCurso": "Violín Inicial",
        "fecha": "2026-04-05",
        "presente": true
      },
      ...
    ]

**Respuesta error — Curso no encontrado (404):**

    "Curso 99 no existe."

### GET api/asistencia?cursoId=10&fecha=2026-04-05

Devuelve la asistencia registrada para ese curso y fecha, ordenada por apellido del estudiante.

**Respuesta (200):** Mismo formato de array que la respuesta del POST.

### GET api/asistencia/estudiante/{estudianteId}

Devuelve el historial completo de asistencia de un estudiante, ordenado por fecha descendente.

**Ejemplo:**

    GET api/asistencia/estudiante/a1b2c3d4-e5f6-7890-abcd-1234567890ab

**Respuesta (200):**

    [
      {
        "asistenciaId": 5,
        "estudianteId": "a1b2c3d4-...",
        "nombreEstudiante": "Juan",
        "apellidoEstudiante": "Pérez",
        "cursoId": 10,
        "nombreCurso": "Violín Inicial",
        "fecha": "2026-04-05",
        "presente": true
      },
      {
        "asistenciaId": 2,
        "estudianteId": "a1b2c3d4-...",
        "nombreEstudiante": "Juan",
        "apellidoEstudiante": "Pérez",
        "cursoId": 10,
        "nombreCurso": "Violín Inicial",
        "fecha": "2026-04-04",
        "presente": false
      }
    ]

### Tabla en base de datos: `Asistencia`

| Columna        | Tipo             | Descripción                                  |
|----------------|------------------|----------------------------------------------|
| AsistenciaID   | `int` (PK, auto) | Identificador único                          |
| EstudianteID   | `uniqueidentifier` | FK → Estudiante                            |
| CursoID        | `int`            | FK → Curso                                   |
| Fecha          | `date`           | Fecha de la clase                            |
| Presente       | `bit`            | `true` = presente, `false` = ausente         |

**Índice único:** `(EstudianteID, CursoID, Fecha)` — Evita duplicados. Un estudiante solo puede tener un registro de asistencia por curso por día.

---

## 📖 9. Documentación detallada: Save de Estudiante + Alta en Curso

### Paso 1: Crear un estudiante — `POST /estudiante/save`

**Requiere autenticación:** Sí — Header `Authorization: Bearer <token>`

**Content-Type:** `application/json`

**Body completo:**

    {
      "nombre": "Juan",
      "apellido": "Pérez",
      "documento": "12345678",
      "fechaNacimiento": "2010-05-15",
      "direccion": "Calle 123",
      "telefono": "1155554444",
      "email": "juan@mail.com",
      "nacionalidad": "Argentina",
      "instrumentoId": 1,
      "activo": true,
      "asegurado": false,
      "autoretiro": false,
      "nombreTutor": "María Pérez",
      "telefonoTutor": "1155551111",
      "documentoTutor": "30111222",
      "nombreTutor2": "Carlos Pérez",
      "telefonoTutor2": "1155552222",
      "documentoTutor2": "30333444",
      "particularidad": "sin observaciones",
      "tmtMédico": "sin observaciones",
      "epPsicoMotriz": "sin observaciones",
      "rutaFoto": null
    }

**Campos obligatorios vs opcionales:**

| Campo            | Tipo      | Requerido       | Notas                                                     |
|------------------|-----------|-----------------|------------------------------------------------------------|
| `nombre`         | `string`  | ✅               | Usado para validar duplicados (junto con `documento`)      |
| `apellido`       | `string`  | ⚠️ Recomendado  | Puede ser null                                             |
| `documento`      | `string`  | ✅               | Usado para validar duplicados (junto con `nombre`)         |
| `fechaNacimiento`| `string`  | ⚠️ Recomendado  | Formato `"YYYY-MM-DD"`                                     |
| `direccion`      | `string`  | Opcional         |                                                            |
| `telefono`       | `string`  | Opcional         | Máx 20 chars                                               |
| `email`          | `string`  | Opcional         | Máx 100 chars                                              |
| `nacionalidad`   | `string`  | Opcional         | Máx 20 chars                                               |
| `instrumentoId`  | `int`     | Opcional         | FK al catálogo de instrumentos. `null` si no tiene         |
| `activo`         | `bool`    | Opcional         | Recomendado enviar `true`                                  |
| `asegurado`      | `bool`    | Opcional         |                                                            |
| `autoretiro`     | `bool`    | Opcional         | Default en BD: `false`                                     |
| `nombreTutor`    | `string`  | Opcional         | Máx 100 chars                                              |
| `telefonoTutor`  | `string`  | Opcional         | Máx 20 chars                                               |
| `documentoTutor` | `string`  | Opcional         |                                                            |
| `nombreTutor2`   | `string`  | Opcional         | Segundo tutor                                              |
| `telefonoTutor2` | `string`  | Opcional         |                                                            |
| `documentoTutor2`| `string`  | Opcional         |                                                            |
| `particularidad` | `string`  | Opcional         | Default en BD: `"sin observaciones"`                       |
| `tmtMédico`      | `string`  | Opcional         | Default en BD: `"sin observaciones"`                       |
| `epPsicoMotriz`  | `string`  | Opcional         | Default en BD: `"sin observaciones"`                       |
| `rutaFoto`       | `string`  | Opcional         | URL o path de la foto                                      |

> ⚠️ **NO enviar `estudianteId`**. La BD lo genera automáticamente como `GUID` con `newid()`.

**Validación de duplicados:** El backend busca por **nombre + documento** simultáneamente. Si ya existe un estudiante con el mismo nombre **y** el mismo documento, rechaza la creación.

**Respuesta exitosa (201):**

    {
      "Estudiante": {
        "estudianteId": "a1b2c3d4-...",
        "nombre": "Juan",
        "apellido": "Pérez",
        ...
      },
      "messages": [
        {
          "status": "respuesta exitosa",
          "text": "",
          "help": ""
        }
      ]
    }

**Respuesta error — Duplicado:**

    {
      "Estudiante": { ... },
      "messages": [
        {
          "status": "Error",
          "text": "",
          "help": "El estudiante ya existe en la base de datos."
        }
      ]
    }

### Paso 2: Inscribir estudiante en un curso — `POST /estudianteDarDeAltaEnCurso/{id}/{Idcurso}`

**Requiere autenticación:** Sí — Header `Authorization: Bearer <token>`

**Parámetros en URL (no hay body):**

| Parámetro  | Tipo   | Ejemplo                | Descripción         |
|------------|--------|------------------------|---------------------|
| `{id}`     | `Guid` | `a1b2c3d4-e5f6-...`   | ID del estudiante   |
| `{Idcurso}`| `int`  | `9`                    | ID del curso        |

**Ejemplo de llamada:**

    POST https://sistemagestionorquesta.azurewebsites.net/estudianteDarDeAltaEnCurso/a1b2c3d4-e5f6-7890-abcd-1234567890ab/9

**No lleva body.** Todos los datos van en la URL.

**Respuesta exitosa (200):** `true`

**Respuesta `false` (200) — Posibles causas:**
- El estudiante no existe
- El curso no existe
- El estudiante **ya está inscrito** en ese curso

**Lógica interna:**

1. Busca el estudiante con `Include(Cursos)`
2. Busca el curso por ID
3. Verifica que el estudiante **no esté ya inscrito**
4. Si no lo está → agrega el curso a `estudiante.Cursos` (tabla intermedia `AlumnosCurso`)
5. `SaveChanges()` → devuelve `true`

### Paso 3: Desvincular estudiante de un curso — `DELETE /estudianteEliminarCurso/{id}/{Idcurso}`

**Mismos parámetros en URL:**

    DELETE https://sistemagestionorquesta.azurewebsites.net/estudianteEliminarCurso/a1b2c3d4-.../13

**Respuesta exitosa:** `true`  
**Respuesta si no estaba inscrito:** `false`

### Flujo completo desde el frontend

    1. POST /estudiante/save              → Crear estudiante (obtener estudianteId de la respuesta)
    2. POST /estudianteDarDeAltaEnCurso/{estudianteId}/{cursoId}  → Inscribir en curso
    3. POST api/asistencia                → Guardar asistencia (cuando el profesor toma lista)

### Ejemplo de código JavaScript/TypeScript

    const BASE_URL = "https://sistemagestionorquesta.azurewebsites.net";
    const token = "eyJhbGciOi..."; // obtenido del login

    // --- PASO 1: Crear estudiante ---
    const nuevoEstudiante = {
      nombre: "Juan",
      apellido: "Pérez",
      documento: "12345678",
      fechaNacimiento: "2010-05-15",
      direccion: "Calle 123",
      telefono: "1155554444",
      email: "juan@mail.com",
      nacionalidad: "Argentina",
      instrumentoId: 1,
      activo: true,
      autoretiro: false,
      nombreTutor: "María Pérez",
      telefonoTutor: "1155551111"
    };

    const saveResponse = await fetch(`${BASE_URL}/estudiante/save`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      },
      body: JSON.stringify(nuevoEstudiante)
    });

    const saveData = await saveResponse.json();
    const parsed = JSON.parse(saveData);
    const estudianteId = parsed.Estudiante.estudianteId;

    // --- PASO 2: Inscribir en curso (cursoId = 9) ---
    const cursoId = 9;
    const inscribirResponse = await fetch(
      `${BASE_URL}/estudianteDarDeAltaEnCurso/${estudianteId}/${cursoId}`,
      {
        method: "POST",
        headers: {
          "Authorization": `Bearer ${token}`
        }
        // NO lleva body
      }
    );

    const inscripcionOk = await inscribirResponse.json(); // true o false

> **Nota sobre la respuesta del save:** El backend devuelve un `JsonResult` (string) dentro del `CustomResponse`. Dependiendo de cómo esté configurado el cliente HTTP en Blazor/JS, puede que se necesite un doble parse o puede que ya venga como objeto. Probar en Swagger primero para ver la estructura exacta.

---

## 🔗 Relación completa entre entidades (actualizada)

    Instrumento (catálogo)
        ├── StockInstrumento (ejemplares físicos: VL-001, VL-002...)
        │       └── PrestamosInstrumentos (historial de préstamos por ejemplar)
        └── Estudiante (relación directa, legacy)

    Estudiante ←→ Curso (muchos a muchos, tabla intermedia AlumnosCurso)
    Estudiante ←  Asistencia (uno a muchos)

    Curso → Profesor (muchos a uno)
    Curso ←  Asistencia (uno a muchos)