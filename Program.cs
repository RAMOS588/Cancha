using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────
// 1. CORS: obligatorio para que el frontend consuma la API
// ─────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 2. JSON en camelCase (por defecto ya lo hace, pero lo aseguramos)
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

app.UseCors();   // ← NO OLVIDAR

// ─────────────────────────────────────────────
// 3. DATOS EN MEMORIA 
// ─────────────────────────────────────────────

var empresa = new
{
    id = 1,
    nombre = "Cancha El Golazo",
    eslogan = "Juega como en casa",
    descripcion = "Cancha de grass sintético con iluminación LED.",
    logo = "https://cdn.ejemplo.com/logo.png",
    imagenPrincipal = "https://cdn.ejemplo.com/cancha.jpg",
    direccion = "Av. Los Deportes 123",
    distrito = "Lima",
    telefono = "+51 999 888 777",
    whatsapp = "https://wa.me/51999888777",
    email = "contacto@elgolazo.com",
    horarioAtencion = "Lun a Dom 7:00 - 23:00",
    moneda = "PEN"
};

var canchas = new[]
{
    new { id = 1, nombre = "Cancha 1", tipo = "Fútbol 7", superficie = "Grass sintético",
          capacidad = 7, techada = false, iluminacion = true, precioBase = 90,
          moneda = "PEN", imagen = "https://cdn.ejemplo.com/c1.jpg",
          estado = "Disponible", descripcion = "Grass sintético premium." },

    new { id = 2, nombre = "Cancha 2", tipo = "Fútbol 5", superficie = "Grass sintético",
          capacidad = 5, techada = true, iluminacion = true, precioBase = 70,
          moneda = "PEN", imagen = "https://cdn.ejemplo.com/c2.jpg",
          estado = "Disponible", descripcion = "Cancha techada." },

    new { id = 3, nombre = "Cancha 3", tipo = "Fútbol 11", superficie = "Grass natural",
          capacidad = 11, techada = false, iluminacion = true, precioBase = 180,
          moneda = "PEN", imagen = "https://cdn.ejemplo.com/c3.jpg",
          estado = "Mantenimiento", descripcion = "Cancha profesional." }
};

var servicios = new[]
{
    new { id = 1, nombre = "Alquiler de cancha", descripcion = "Alquiler por hora.",
          icono = "soccer", categoria = "Deportivo", precio = 90m, moneda = "PEN",
          incluido = false, disponible = true },

    new { id = 2, nombre = "Campeonatos", descripcion = "Organización de torneos.",
          icono = "trophy", categoria = "Eventos", precio = 500m, moneda = "PEN",
          incluido = false, disponible = true },

    new { id = 3, nombre = "Entrenamientos", descripcion = "Sesiones dirigidas.",
          icono = "whistle", categoria = "Deportivo", precio = 60m, moneda = "PEN",
          incluido = false, disponible = true },

    new { id = 4, nombre = "Vestuarios", descripcion = "Vestuarios con duchas.",
          icono = "shower", categoria = "Comodidad", precio = 0m, moneda = "PEN",
          incluido = true, disponible = true },

    new { id = 5, nombre = "Iluminación", descripcion = "Luces LED para juego nocturno.",
          icono = "lightbulb", categoria = "Comodidad", precio = 0m, moneda = "PEN",
          incluido = true, disponible = true },

    new { id = 6, nombre = "Estacionamiento", descripcion = "Zona de estacionamiento.",
          icono = "car", categoria = "Comodidad", precio = 0m, moneda = "PEN",
          incluido = true, disponible = true }
};

var promociones = new[]
{
    new { id = 1, titulo = "2x1 los martes", descripcion = "Reserva 1 hora y llévate otra gratis.",
          descuento = 50, precioPromocional = 90m, moneda = "PEN",
          fechaInicio = "2026-10-01", fechaFin = "2026-10-31",
          condiciones = "Solo martes de 14:00 a 18:00.",
          imagen = "https://cdn.ejemplo.com/p1.jpg", canchaId = 1, activa = true },

    new { id = 2, titulo = "Noche deportiva", descripcion = "20% en horario nocturno.",
          descuento = 20, precioPromocional = 96m, moneda = "PEN",
          fechaInicio = "2026-10-01", fechaFin = "2026-12-31",
          condiciones = "De lunes a jueves después de las 20:00.",
          imagen = "https://cdn.ejemplo.com/p2.jpg", canchaId = 2, activa = true }
};

var tarifas = new[]
{
    new { id = 1, canchaId = 1, tipoCancha = "Fútbol 7", franja = "Diurno",
          horaInicio = "07:00", horaFin = "18:00", precio = 90m, moneda = "PEN",
          duracionMin = 60 },

    new { id = 2, canchaId = 1, tipoCancha = "Fútbol 7", franja = "Nocturno",
          horaInicio = "18:00", horaFin = "23:00", precio = 120m, moneda = "PEN",
          duracionMin = 60 },

    new { id = 3, canchaId = 2, tipoCancha = "Fútbol 5", franja = "Diurno",
          horaInicio = "07:00", horaFin = "18:00", precio = 70m, moneda = "PEN",
          duracionMin = 60 },

    new { id = 4, canchaId = 3, tipoCancha = "Fútbol 11", franja = "Nocturno",
          horaInicio = "18:00", horaFin = "23:00", precio = 180m, moneda = "PEN",
          duracionMin = 90 }
};

var horarios = new[]
{
    new { id = 101, canchaId = 1, tipoCancha = "Fútbol 7", fecha = "2026-10-15",
          horaInicio = "18:00", horaFin = "19:00", duracionMin = 60,
          estado = "Disponible", precio = 120m, moneda = "PEN", promocionId = (int?)null,
          servicios = new[] { "Iluminación", "Vestuarios", "Estacionamiento" },
          observacion = "Incluye balón.", actualizadoEn = "2026-10-01T10:00:00" },

    new { id = 102, canchaId = 1, tipoCancha = "Fútbol 7", fecha = "2026-10-15",
          horaInicio = "19:00", horaFin = "20:00", duracionMin = 60,
          estado = "Ocupado", precio = 120m, moneda = "PEN", promocionId = (int?)null,
          servicios = new[] { "Iluminación", "Vestuarios" },
          observacion = "", actualizadoEn = "2026-10-01T10:00:00" },

    new { id = 103, canchaId = 2, tipoCancha = "Fútbol 5", fecha = "2026-10-15",
          horaInicio = "20:00", horaFin = "21:00", duracionMin = 60,
          estado = "Disponible", precio = 70m, moneda = "PEN", promocionId = 2,
          servicios = new[] { "Vestuarios", "Estacionamiento" },
          observacion = "Aplica promoción nocturna.", actualizadoEn = "2026-10-01T10:00:00" },

    new { id = 104, canchaId = 3, tipoCancha = "Fútbol 11", fecha = "2026-10-16",
          horaInicio = "09:00", horaFin = "10:30", duracionMin = 90,
          estado = "Disponible", precio = 180m, moneda = "PEN", promocionId = (int?)null,
          servicios = new[] { "Vestuarios", "Estacionamiento" },
          observacion = "", actualizadoEn = "2026-10-01T10:00:00" }
};

var destacados = new[]
{
    new { id = 1, titulo = "Iluminación LED", descripcion = "Juega de noche con visibilidad total.",
          icono = "lightbulb", imagen = "https://cdn.ejemplo.com/led.jpg", orden = 1 },
    new { id = 2, titulo = "Grass sintético premium", descripcion = "Superficie de última generación.",
          icono = "grass", imagen = "https://cdn.ejemplo.com/grass.jpg", orden = 2 },
    new { id = 3, titulo = "Estacionamiento amplio", descripcion = "Espacio para 20 vehículos.",
          icono = "car", imagen = "https://cdn.ejemplo.com/parking.jpg", orden = 3 }
};

// ─────────────────────────────────────────────
// 4. ENDPOINTS 
// ─────────────────────────────────────────────

app.MapGet("/", () => "API Cancha funcionando");

app.MapGet("/api/empresa", () => Results.Ok(new { data = empresa }));

app.MapGet("/api/canchas", (string? tipo, string? estado) =>
{
    var q = canchas.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(tipo))
        q = q.Where(c => c.tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(estado))
        q = q.Where(c => c.estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/canchas/{id:int}", (int id) =>
{
    var cancha = canchas.FirstOrDefault(c => c.id == id);
    return cancha is null
        ? Results.NotFound(new { error = true, mensaje = "Cancha no encontrada" })
        : Results.Ok(new { data = cancha });
});

app.MapGet("/api/servicios", (string? categoria, bool? disponible) =>
{
    var q = servicios.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(categoria))
        q = q.Where(s => s.categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
    if (disponible.HasValue)
        q = q.Where(s => s.disponible == disponible.Value);
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/servicios/{id:int}", (int id) =>
{
    var s = servicios.FirstOrDefault(x => x.id == id);
    return s is null
        ? Results.NotFound(new { error = true, mensaje = "Servicio no encontrado" })
        : Results.Ok(new { data = s });
});

app.MapGet("/api/promociones", (bool? activa, int? canchaId) =>
{
    var q = promociones.AsEnumerable();
    if (activa.HasValue) q = q.Where(p => p.activa == activa.Value);
    if (canchaId.HasValue) q = q.Where(p => p.canchaId == canchaId.Value);
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/tarifas", (string? tipoCancha, string? franja) =>
{
    var q = tarifas.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(tipoCancha))
        q = q.Where(t => t.tipoCancha.Equals(tipoCancha, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(franja))
        q = q.Where(t => t.franja.Equals(franja, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(new { data = q.ToArray() });
});

// Recurso principal: horarios con filtros
app.MapGet("/api/horarios", (
    string? fecha,
    string? horaInicio,
    string? estado,
    string? tipoCancha,
    decimal? precioMax,
    int? canchaId,
    string? sort) =>
{
    var q = horarios.AsEnumerable();

    if (!string.IsNullOrWhiteSpace(fecha))
        q = q.Where(h => h.fecha == fecha);

    if (!string.IsNullOrWhiteSpace(horaInicio))
        q = q.Where(h => string.Compare(h.horaInicio, horaInicio) >= 0);

    if (!string.IsNullOrWhiteSpace(estado))
        q = q.Where(h => h.estado.Equals(estado, StringComparison.OrdinalIgnoreCase));

    if (!string.IsNullOrWhiteSpace(tipoCancha))
        q = q.Where(h => h.tipoCancha.Equals(tipoCancha, StringComparison.OrdinalIgnoreCase));

    if (precioMax.HasValue)
        q = q.Where(h => h.precio <= precioMax.Value);

    if (canchaId.HasValue)
        q = q.Where(h => h.canchaId == canchaId.Value);

    q = sort switch
    {
        "precio"  => q.OrderBy(h => h.precio),
        "-precio" => q.OrderByDescending(h => h.precio),
        _         => q.OrderBy(h => h.fecha).ThenBy(h => h.horaInicio)
    };

    var lista = q.ToArray();
    return Results.Ok(new { data = lista, meta = new { total = lista.Length } });
});

app.MapGet("/api/horarios/{id:int}", (int id) =>
{
    var h = horarios.FirstOrDefault(x => x.id == id);
    return h is null
        ? Results.NotFound(new { error = true, mensaje = "Horario no encontrado" })
        : Results.Ok(new { data = h });
});

app.MapGet("/api/destacados", () =>
    Results.Ok(new { data = destacados.OrderBy(d => d.orden).ToArray() }));


var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");

