using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();
app.UseCors();

// ─────────────────────────────────────────────
// MODELOS (records)
// ─────────────────────────────────────────────
record Empresa(
    int Id, string Nombre, string Eslogan, string Descripcion,
    string Logo, string ImagenPrincipal, string Direccion, string Distrito,
    string Telefono, string Whatsapp, string Email, string HorarioAtencion, string Moneda);

record Cancha(
    int Id, string Nombre, string Tipo, string Superficie, int Capacidad,
    bool Techada, bool Iluminacion, decimal PrecioBase, string Moneda,
    string Imagen, string Estado, string Descripcion);

record Servicio(
    int Id, string Nombre, string Descripcion, string Icono, string Categoria,
    decimal Precio, string Moneda, bool Incluido, bool Disponible);

record Promocion(
    int Id, string Titulo, string Descripcion, int Descuento,
    decimal PrecioPromocional, string Moneda, string FechaInicio, string FechaFin,
    string Condiciones, string Imagen, int CanchaId, bool Activa);

record Tarifa(
    int Id, int CanchaId, string TipoCancha, string Franja,
    string HoraInicio, string HoraFin, decimal Precio, string Moneda, int DuracionMin);

record Horario(
    int Id, int CanchaId, string TipoCancha, string Fecha,
    string HoraInicio, string HoraFin, int DuracionMin, string Estado,
    decimal Precio, string Moneda, int? PromocionId, string[] Servicios,
    string Observacion, string ActualizadoEn);

record Destacado(
    int Id, string Titulo, string Descripcion, string Icono, string Imagen, int Orden);

// ─────────────────────────────────────────────
// DATOS EN MEMORIA
// ─────────────────────────────────────────────

var empresa = new Empresa(
    1, "Cancha El Golazo", "Juega como en casa",
    "Cancha de grass sintético con iluminación LED.",
    "https://cdn.ejemplo.com/logo.png",
    "https://cdn.ejemplo.com/cancha.jpg",
    "Av. Los Deportes 123", "Lima",
    "+51 999 888 777", "https://wa.me/51999888777",
    "contacto@elgolazo.com", "Lun a Dom 7:00 - 23:00", "PEN");

var canchas = new Cancha[]
{
    new(1, "Cancha 1", "Fútbol 7", "Grass sintético", 7, false, true,
        90m, "PEN", "https://cdn.ejemplo.com/c1.jpg", "Disponible",
        "Grass sintético premium."),
    new(2, "Cancha 2", "Fútbol 5", "Grass sintético", 5, true, true,
        70m, "PEN", "https://cdn.ejemplo.com/c2.jpg", "Disponible",
        "Cancha techada."),
    new(3, "Cancha 3", "Fútbol 11", "Grass natural", 11, false, true,
        180m, "PEN", "https://cdn.ejemplo.com/c3.jpg", "Mantenimiento",
        "Cancha profesional.")
};

var servicios = new Servicio[]
{
    new(1, "Alquiler de cancha", "Alquiler por hora.", "soccer",
        "Deportivo", 90m, "PEN", false, true),
    new(2, "Campeonatos", "Organización de torneos.", "trophy",
        "Eventos", 500m, "PEN", false, true),
    new(3, "Entrenamientos", "Sesiones dirigidas.", "whistle",
        "Deportivo", 60m, "PEN", false, true),
    new(4, "Vestuarios", "Vestuarios con duchas.", "shower",
        "Comodidad", 0m, "PEN", true, true),
    new(5, "Iluminación", "Luces LED para juego nocturno.", "lightbulb",
        "Comodidad", 0m, "PEN", true, true),
    new(6, "Estacionamiento", "Zona de estacionamiento.", "car",
        "Comodidad", 0m, "PEN", true, true)
};

var promociones = new Promocion[]
{
    new(1, "2x1 los martes", "Reserva 1 hora y llévate otra gratis.",
        50, 90m, "PEN", "2026-10-01", "2026-10-31",
        "Solo martes de 14:00 a 18:00.",
        "https://cdn.ejemplo.com/p1.jpg", 1, true),
    new(2, "Noche deportiva", "20% en horario nocturno.",
        20, 96m, "PEN", "2026-10-01", "2026-12-31",
        "De lunes a jueves después de las 20:00.",
        "https://cdn.ejemplo.com/p2.jpg", 2, true)
};

var tarifas = new Tarifa[]
{
    new(1, 1, "Fútbol 7",  "Diurno",   "07:00", "18:00", 90m,  "PEN", 60),
    new(2, 1, "Fútbol 7",  "Nocturno", "18:00", "23:00", 120m, "PEN", 60),
    new(3, 2, "Fútbol 5",  "Diurno",   "07:00", "18:00", 70m,  "PEN", 60),
    new(4, 3, "Fútbol 11", "Nocturno", "18:00", "23:00", 180m, "PEN", 90)
};

var horarios = new Horario[]
{
    new(101, 1, "Fútbol 7",  "2026-10-15", "18:00", "19:00", 60,
        "Disponible", 120m, "PEN", null,
        new[] { "Iluminación", "Vestuarios", "Estacionamiento" },
        "Incluye balón.", "2026-10-01T10:00:00"),
    new(102, 1, "Fútbol 7",  "2026-10-15", "19:00", "20:00", 60,
        "Ocupado", 120m, "PEN", null,
        new[] { "Iluminación", "Vestuarios" },
        "", "2026-10-01T10:00:00"),
    new(103, 2, "Fútbol 5",  "2026-10-15", "20:00", "21:00", 60,
        "Disponible", 70m, "PEN", 2,
        new[] { "Vestuarios", "Estacionamiento" },
        "Aplica promoción nocturna.", "2026-10-01T10:00:00"),
    new(104, 3, "Fútbol 11", "2026-10-16", "09:00", "10:30", 90,
        "Disponible", 180m, "PEN", null,
        new[] { "Vestuarios", "Estacionamiento" },
        "", "2026-10-01T10:00:00")
};

var destacados = new Destacado[]
{
    new(1, "Iluminación LED", "Juega de noche con visibilidad total.",
        "lightbulb", "https://cdn.ejemplo.com/led.jpg", 1),
    new(2, "Grass sintético premium", "Superficie de última generación.",
        "grass", "https://cdn.ejemplo.com/grass.jpg", 2),
    new(3, "Estacionamiento amplio", "Espacio para 20 vehículos.",
        "car", "https://cdn.ejemplo.com/parking.jpg", 3)
};

// ─────────────────────────────────────────────
// ENDPOINTS (solo GET)
// ─────────────────────────────────────────────

app.MapGet("/", () => "API Cancha funcionando");

app.MapGet("/api/empresa", () => Results.Ok(new { data = empresa }));

app.MapGet("/api/canchas", (string? tipo, string? estado) =>
{
    var q = canchas.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(tipo))
        q = q.Where(c => c.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(estado))
        q = q.Where(c => c.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/canchas/{id:int}", (int id) =>
{
    var cancha = canchas.FirstOrDefault(c => c.Id == id);
    return cancha is null
        ? Results.NotFound(new { error = true, mensaje = "Cancha no encontrada" })
        : Results.Ok(new { data = cancha });
});

app.MapGet("/api/servicios", (string? categoria, bool? disponible) =>
{
    var q = servicios.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(categoria))
        q = q.Where(s => s.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
    if (disponible.HasValue)
        q = q.Where(s => s.Disponible == disponible.Value);
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/servicios/{id:int}", (int id) =>
{
    var s = servicios.FirstOrDefault(x => x.Id == id);
    return s is null
        ? Results.NotFound(new { error = true, mensaje = "Servicio no encontrado" })
        : Results.Ok(new { data = s });
});

app.MapGet("/api/promociones", (bool? activa, int? canchaId) =>
{
    var q = promociones.AsEnumerable();
    if (activa.HasValue) q = q.Where(p => p.Activa == activa.Value);
    if (canchaId.HasValue) q = q.Where(p => p.CanchaId == canchaId.Value);
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/tarifas", (string? tipoCancha, string? franja) =>
{
    var q = tarifas.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(tipoCancha))
        q = q.Where(t => t.TipoCancha.Equals(tipoCancha, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(franja))
        q = q.Where(t => t.Franja.Equals(franja, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(new { data = q.ToArray() });
});

app.MapGet("/api/horarios", (
    string? fecha, string? horaInicio, string? estado,
    string? tipoCancha, decimal? precioMax, int? canchaId, string? sort) =>
{
    var q = horarios.AsEnumerable();

    if (!string.IsNullOrWhiteSpace(fecha))
        q = q.Where(h => h.Fecha == fecha);
    if (!string.IsNullOrWhiteSpace(horaInicio))
        q = q.Where(h => string.Compare(h.HoraInicio, horaInicio) >= 0);
    if (!string.IsNullOrWhiteSpace(estado))
        q = q.Where(h => h.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(tipoCancha))
        q = q.Where(h => h.TipoCancha.Equals(tipoCancha, StringComparison.OrdinalIgnoreCase));
    if (precioMax.HasValue)
        q = q.Where(h => h.Precio <= precioMax.Value);
    if (canchaId.HasValue)
        q = q.Where(h => h.CanchaId == canchaId.Value);

    q = sort switch
    {
        "precio"  => q.OrderBy(h => h.Precio),
        "-precio" => q.OrderByDescending(h => h.Precio),
        _         => q.OrderBy(h => h.Fecha).ThenBy(h => h.HoraInicio)
    };

    var lista = q.ToArray();
    return Results.Ok(new { data = lista, meta = new { total = lista.Length } });
});

app.MapGet("/api/horarios/{id:int}", (int id) =>
{
    var h = horarios.FirstOrDefault(x => x.Id == id);
    return h is null
        ? Results.NotFound(new { error = true, mensaje = "Horario no encontrado" })
        : Results.Ok(new { data = h });
});

app.MapGet("/api/destacados", () =>
    Results.Ok(new { data = destacados.OrderBy(d => d.Orden).ToArray() }));

// ─────────────────────────────────────────────
// PUERTO DINÁMICO PARA RENDER
// ─────────────────────────────────────────────
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
