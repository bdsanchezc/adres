using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdquisicionesAPI.Data;
using AdquisicionesAPI.Models;

namespace AdquisicionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdquisicionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdquisicionesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<Adquisicion>>>> GetAdquisiciones(
            [FromQuery] int? proveedorId,
            [FromQuery] int? unidadId,
            [FromQuery] int? estadoId,
            [FromQuery] string? fechaAdquisicion)
        {
            var query = _context.Adquisiciones
                .Include(a => a.Unidad)
                .Include(a => a.Proveedor)
                .Include(a => a.Estado)
                .AsQueryable();  

            // 🔹 Aplicar filtros opcionales
            if (proveedorId.HasValue)
                query = query.Where(a => a.ProveedorId == proveedorId.Value);

            if (unidadId.HasValue)
                query = query.Where(a => a.UnidadId == unidadId.Value);

            if (estadoId.HasValue)
                query = query.Where(a => a.EstadoId == estadoId.Value);

            if (!string.IsNullOrEmpty(fechaAdquisicion))
                query = query.Where(a => a.FechaAdquisicion == fechaAdquisicion);

            
            var adquisiciones = await query.ToListAsync();

            return Ok(new Response<List<Adquisicion>>("success", adquisiciones));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Adquisicion>>> GetAdquisicionPorId(int id)
        {
            var adquisicion = await _context.Adquisiciones
                .Include(a => a.Unidad)
                .Include(a => a.Proveedor)
                .Include(a => a.Estado)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (adquisicion == null)
            {
                return NotFound(new Response<string>("error", "La adquisición no existe."));
            }

            return Ok(new Response<Adquisicion>("success", adquisicion));
        }

        [HttpPost]
        public async Task<ActionResult<Adquisicion>> PostAdquisicion(Adquisicion adquisicion, [FromHeader] string? usuario)
        {
            adquisicion.EstadoId = 1;
            _context.Adquisiciones.Add(adquisicion);
            await _context.SaveChangesAsync();

            usuario = string.IsNullOrEmpty(usuario) ? "System" : usuario;

            var historio = new HistorialAdquisicion {
                AdquisicionId = adquisicion.Id,
                CampoModificado = "Registro creado",
                ValorAnterior = "",
                ValorNuevo = "",
                FechaModificacion = DateTime.UtcNow,
                Usuario = usuario,
                AccionId = 1
            };

            _context.HistorialAdquisiciones.Add(historio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAdquisiciones), new { id = adquisicion.Id }, 
                new Response<Adquisicion>("success", adquisicion)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdquisicion(int id, Adquisicion adquisicionModificada, [FromHeader] string? usuario)
        {
            var adquisicion = await _context.Adquisiciones.FindAsync(id);

            if (adquisicion == null)
            {
                return NotFound(new { status = "error", message = "La adquisición no existe." });
            }

            usuario = string.IsNullOrEmpty(usuario) ? "Sistema" : usuario;
            var historicoCambios = new List<HistorialAdquisicion>();

            var propiedades = typeof(Adquisicion).GetProperties();

            foreach (var propiedad in propiedades)
            {
                if (propiedad.Name == "Id") continue;

                var valorAnterior = propiedad.GetValue(adquisicion);
                var valorNuevo = propiedad.GetValue(adquisicionModificada);

                if (valorAnterior?.ToString() != valorNuevo?.ToString())
                {
                    historicoCambios.Add(new HistorialAdquisicion
                    {
                        AdquisicionId = id,
                        CampoModificado = propiedad.Name,
                        ValorAnterior = valorAnterior?.ToString() ?? "N/A",
                        ValorNuevo = valorNuevo?.ToString() ?? "N/A",
                        FechaModificacion = DateTime.UtcNow,
                        Usuario = usuario,
                        AccionId = 2 // Acción: Modificación
                    });

                    propiedad.SetValue(adquisicion, valorNuevo);
                }
            }

            await _context.SaveChangesAsync();

            if (historicoCambios.Any())
            {
                _context.HistorialAdquisiciones.AddRange(historicoCambios);
                await _context.SaveChangesAsync();
            }

            return Ok(new Response<Adquisicion>("success", adquisicion));
        }
    }
}
