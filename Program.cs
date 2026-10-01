var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseCors();

// ─────────────────────────────────────────────
// ENDPOINTS
// ─────────────────────────────────────────────

app.MapGet("/", () =>
{
    return "API Cancha funcionando";
});

// Empresa
app.MapGet("/api/empresa", () =>
{
    return Results.Ok(new
    {
        data = new
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
        }
    });
});

// Canchas
app.MapGet("/api/canchas", () =>
{
    return Results.Ok(new
    {
        data = new[]
        {
            new
            {
                id = 1,
                nombre = "Cancha 1",
                tipo = "Fútbol 7",
                superficie = "Grass sintético",
                capacidad = 7,
                techada = false,
                iluminacion = true,
                precioBase = 90,
                moneda = "PEN",
                imagen = "https://cdn.ejemplo.com/c1.jpg",
                estado = "Disponible",
                descripcion = "Grass sintético premium."
            },
            new
            {
                id = 2,
                nombre = "Cancha 2",
                tipo = "Fútbol 5",
                superficie = "Grass sintético",
                capacidad = 5,
                techada = true,
                iluminacion = true,
                precioBase = 70,
                moneda = "PEN",
                imagen = "https://cdn.ejemplo.com/c2.jpg",
                estado = "Disponible",
                descripcion = "Cancha techada."
            },
            new
            {
                id = 3,
                nombre = "Cancha 3",
                tipo = "Fútbol 11",
                superficie = "Grass natural",
                capacidad = 11,
                techada = false,
                iluminacion = true,
                precioBase = 180,
                moneda = "PEN",
                imagen = "https://cdn.ejemplo.com/c3.jpg",
                estado = "Mantenimiento",
                descripcion = "Cancha profesional."
            }
        }
    });
});

// Cancha por ID
app.MapGet("/api/canchas/{id:int}", (int id) =>
{
    var canchas = new[]
    {
        new { id = 1, nombre = "Cancha 1", tipo = "Fútbol 7",  precioBase = 90,  estado = "Disponible" },
        new { id = 2, nombre = "Cancha 2", tipo = "Fútbol 5",  precioBase = 70,  estado = "Disponible" },
        new { id = 3, nombre = "Cancha 3", tipo = "Fútbol 11", precioBase = 180, estado = "Mantenimiento" }
    };

    var cancha = canchas.FirstOrDefault(c => c.id == id);

    if (cancha is null)
        return Results.NotFound(new { error = true, mensaje = "Cancha no encontrada" });

    return Results.Ok(new { data = cancha });
});

// Servicios
app.MapGet("/api/servicios", () =>
{
    return Results.Ok(new
    {
        data = new[]
        {
            new { id = 1, nombre = "Alquiler de cancha", descripcion = "Alquiler por hora.",          icono = "soccer",    categoria = "Deportivo", precio = 90,  moneda = "PEN", incluido = false, disponible = true },
            new { id = 2, nombre = "Campeonatos",        descripcion = "Organización de torneos.",    icono = "trophy",    categoria = "Eventos",   precio = 500, moneda = "PEN", incluido = false, disponible = true },
            new { id = 3, nombre = "Entrenamientos",     descripcion = "Sesiones dirigidas.",         icono = "whistle",   categoria = "Deportivo", precio = 60,  moneda = "PEN", incluido = false, disponible = true },
            new { id = 4, nombre = "Vestuarios",         descripcion = "Vestuarios con duchas.",      icono = "shower",    categoria = "Comodidad", precio = 0,   moneda = "PEN", incluido = true,  disponible = true },
            new { id = 5, nombre = "Iluminación",        descripcion = "Luces LED nocturnas.",        icono = "lightbulb", categoria = "Comodidad", precio = 0,   moneda = "PEN", incluido = true,  disponible = true },
            new { id = 6, nombre = "Estacionamiento",    descripcion = "Zona de estacionamiento.",    icono = "car",       categoria = "Comodidad", precio = 0,   moneda = "PEN", incluido = true,  disponible = true }
        }
    });
});

// Promociones
app.MapGet("/api/promociones", () =>
{
    return Results.Ok(new
    {
        data = new[]
        {
            new
            {
                id = 1,
                titulo = "2x1 los martes",
                descripcion = "Reserva 1 hora y llévate otra gratis.",
                descuento = 50,
                precioPromocional = 90,
                moneda = "PEN",
                fechaInicio = "2026-10-01",
                fechaFin = "2026-10-31",
                condiciones = "Solo martes de 14:00 a 18:00.",
                imagen = "https://cdn.ejemplo.com/p1.jpg",
                canchaId = 1,
                activa = true
            },
            new
            {
                id = 2,
                titulo = "Noche deportiva",
                descripcion = "20% en horario nocturno.",
                descuento = 20,
                precioPromocional = 96,
                moneda = "PEN",
                fechaInicio = "2026-10-01",
                fechaFin = "2026-12-31",
                condiciones = "De lunes a jueves después de las 20:00.",
                imagen = "https://cdn.ejemplo.com/p2.jpg",
                canchaId = 2,
                activa = true
            }
        }
    });
});

// Tarifas
app.MapGet("/api/tarifas", () =>
{
    return Results.Ok(new
    {
        data = new[]
        {
            new { id = 1, canchaId = 1, tipoCancha = "Fútbol 7",  franja = "Diurno",   horaInicio = "07:00", horaFin = "18:00", precio = 90,  moneda = "PEN", duracionMin = 60 },
            new { id = 2, canchaId = 1, tipoCancha = "Fútbol 7",  franja = "Nocturno", horaInicio = "18:00", horaFin = "23:00", precio = 120, moneda = "PEN", duracionMin = 60 },
            new { id = 3, canchaId = 2, tipoCancha = "Fútbol 5",  franja = "Diurno",   horaInicio = "07:00", horaFin = "18:00", precio = 70,  moneda = "PEN", duracionMin = 60 },
            new { id = 4, canchaId = 3, tipoCancha = "Fútbol 11", franja = "Nocturno", horaInicio = "18:00", horaFin = "23:00", precio = 180, moneda = "PEN", duracionMin = 90 }
        }
    });
});

// Horarios (recurso principal)
app.MapGet("/api/horarios", () =>
{
    return Results.Ok(new
    {
        data = new[]
        {
            new
            {
                id = 101,
                canchaId = 1,
                tipoCancha = "Fútbol 7",
                fecha = "2026-10-15",
                horaInicio = "18:00",
                horaFin = "19:00",
                duracionMin = 60,
                estado = "Disponible",
                precio = 120,
                moneda = "PEN",
                promocionId = (int?)null,
                servicios = new[] { "Iluminación", "Vestuarios", "Estacionamiento" },
                observacion = "Incluye balón.",
                actualizadoEn = "2026-10-01T10:00:00"
            },
            new
            {
                id = 102,
                canchaId = 1,
                tipoCancha = "Fútbol 7",
                fecha = "2026-10-15",
                horaInicio = "19:00",
                horaFin = "20:00",
                duracionMin = 60,
                estado = "Ocupado",
                precio = 120,
                moneda = "PEN",
                promocionId = (int?)null,
                servicios = new[] { "Iluminación", "Vestuarios" },
                observacion = "",
                actualizadoEn = "2026-10-01T10:00:00"
            },
            new
            {
                id = 103,
                canchaId = 2,
                tipoCancha = "Fútbol 5",
                fecha = "2026-10-15",
                horaInicio = "20:00",
                horaFin = "21:00",
                duracionMin = 60,
                estado = "Disponible",
                precio = 70,
                moneda = "PEN",
                promocionId = (int?)2,
                servicios = new[] { "Vestuarios", "Estacionamiento" },
                observacion = "Aplica promoción nocturna.",
                actualizadoEn = "2026-10-01T10:00:00"
            },
            new
            {
                id = 104,
                canchaId = 3,
                tipoCancha = "Fútbol 11",
                fecha = "2026-10-16",
                horaInicio = "09:00",
                horaFin = "10:30",
                duracionMin = 90,
                estado = "Disponible",
                precio = 180,
                moneda = "PEN",
                promocionId = (int?)null,
                servicios = new[] { "Vestuarios", "Estacionamiento" },
                observacion = "",
                actualizadoEn = "2026-10-01T10:00:00"
            }
        }
    });
});

// Horario por ID
app.MapGet("/api/horarios/{id:int}", (int id) =>
{
    var horarios = new[]
    {
        new { id = 101, canchaId = 1, tipoCancha = "Fútbol 7",  fecha = "2026-10-15", horaInicio = "18:00", horaFin = "19:00", duracionMin = 60, estado = "Disponible", precio = 120 },
        new { id = 102, canchaId = 1, tipoCancha = "Fútbol 7",  fecha = "2026-10-15", horaInicio = "19:00", horaFin = "20:00", duracionMin = 60, estado = "Ocupado",    precio = 120 },
        new { id = 103, canchaId = 2, tipoCancha = "Fútbol 5",  fecha = "2026-10-15", horaInicio = "20:00", horaFin = "21:00", duracionMin = 60, estado = "Disponible", precio = 70  },
        new { id = 104, canchaId = 3, tipoCancha = "Fútbol 11", fecha = "2026-10-16", horaInicio = "09:00", horaFin = "10:30", duracionMin = 90, estado = "Disponible", precio = 180 }
    };

    var horario = horarios.FirstOrDefault(h => h.id == id);

    if (horario is null)
        return Results.NotFound(new { error = true, mensaje = "Horario no encontrado" });

    return Results.Ok(new { data = horario });
});

// Destacados
app.MapGet("/api/destacados", () =>
{
    return Results.Ok(new
    {
        data = new[]
        {
            new { id = 1, titulo = "Iluminación LED",           descripcion = "Juega de noche con visibilidad total.", icono = "lightbulb", imagen = "https://cdn.ejemplo.com/led.jpg",     orden = 1 },
            new { id = 2, titulo = "Grass sintético premium",   descripcion = "Superficie de última generación.",     icono = "grass",     imagen = "https://cdn.ejemplo.com/grass.jpg",   orden = 2 },
            new { id = 3, titulo = "Estacionamiento amplio",    descripcion = "Espacio para 20 vehículos.",           icono = "car",       imagen = "https://cdn.ejemplo.com/parking.jpg", orden = 3 }
        }
    });
});

// ─────────────────────────────────────────────
// PUERTO DINÁMICO PARA RENDER
// ─────────────────────────────────────────────
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";

app.Run($"http://0.0.0.0:{port}");
