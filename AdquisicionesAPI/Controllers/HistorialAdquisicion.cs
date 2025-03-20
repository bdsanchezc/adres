using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdquisicionesAPI.Data;
using AdquisicionesAPI.Models;

namespace AdquisicionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialAdquisiciones : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistorialAdquisiciones(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{adquisicionId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetHistorialByAdquisicion(int adquisicionId)
        {
            var historial = await _context.HistorialAdquisiciones
                .Include(h => h.Accion)
                .Where(h => h.AdquisicionId == adquisicionId)
                .OrderByDescending(h => h.FechaModificacion)
                .ToListAsync();

            if (!historial.Any())
            {
                return NotFound(new { status = "error", message = $"No hay historial de cambios para la adquisición con ID {adquisicionId}" });
            }

            // Obtener listas de paramétricas para hacer la conversión de ID a nombre
            var unidades = await _context.Unidades.ToDictionaryAsync(u => u.Id, u => u.Nombre);
            var proveedores = await _context.Proveedores.ToDictionaryAsync(p => p.Id, p => p.Nombre);
            var estados = await _context.Estados.ToDictionaryAsync(e => e.Id, e => e.Nombre);

            // Diccionario para formatear nombres de campos
            var camposFormateados = new Dictionary<string, string>
            {
                { "UnidadId", "Unidad" },
                { "ProveedorId", "Proveedor" },
                { "EstadoId", "Estado" },
                { "Presupuesto", "Presupuesto Asignado" },
                { "TipoBienServicio", "Tipo de Bien o Servicio" },
                { "Cantidad", "Cantidad Solicitada" },
                { "ValorUnitario", "Valor Unitario" },
                { "ValorTotal", "Valor Total" },
                { "FechaAdquisicion", "Fecha de Adquisición" },
                { "Documentacion", "Documentación Adjunta" }
            };

            var historialFormateado = historial.Select(h => new
            {
                h.Id,
                h.AdquisicionId,
                CampoModificado = camposFormateados.ContainsKey(h.CampoModificado) ? camposFormateados[h.CampoModificado] : h.CampoModificado,
                ValorAnterior = ConvertirValor(h.CampoModificado, h.ValorAnterior, unidades, proveedores, estados),
                ValorNuevo = ConvertirValor(h.CampoModificado, h.ValorNuevo, unidades, proveedores, estados),
                FechaModificacion = h.FechaModificacion,
                Usuario = h.Usuario,
                Accion = h.Accion?.Nombre
            });

            return Ok(new { status = "success", data = historialFormateado });
        }
        
        [HttpPost]
        public async Task<ActionResult<HistorialAdquisicion>> PostHistorialAdquisicion(HistorialAdquisicion historial)
        {
            _context.HistorialAdquisiciones.Add(historial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorialByAdquisicion), new { adquisicionId = historial.AdquisicionId }, historial);
        }

        private string ConvertirValor(string campo, string valor, Dictionary<int, string> unidades, Dictionary<int, string> proveedores, Dictionary<int, string> estados)
        {
            if (int.TryParse(valor, out int id)) // Si el valor es un número
            {
                return campo switch
                {
                    "UnidadId" => unidades.ContainsKey(id) ? unidades[id] : valor,
                    "ProveedorId" => proveedores.ContainsKey(id) ? proveedores[id] : valor,
                    "EstadoId" => estados.ContainsKey(id) ? estados[id] : valor,
                    _ => valor
                };
            }
            return valor; // Si el valor no es un ID numérico, devolverlo tal cual
        }
    }
}
