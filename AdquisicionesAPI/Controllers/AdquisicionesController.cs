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
        public async Task<ActionResult<IEnumerable<Adquisicion>>> GetAdquisiciones()
        {
            return await _context.Adquisiciones.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Adquisicion>> PostAdquisicion(Adquisicion adquisicion)
        {
            adquisicion.ValorTotal = adquisicion.Cantidad * adquisicion.ValorUnitario;
            _context.Adquisiciones.Add(adquisicion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdquisiciones), new { id = adquisicion.Id }, adquisicion);
        }
    }
}
