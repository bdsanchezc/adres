using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdquisicionesAPI.Data;
using AdquisicionesAPI.Models;

namespace AdquisicionesAPI.Services
{
    public class ParametricsService
    {
        private readonly AppDbContext _context;

        public ParametricsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> GetEstados() => await _context.Estados.ToListAsync();

        public async Task<List<Unidad>> GetUnidades() => await _context.Unidades.ToListAsync();

        public async Task<List<Proveedor>> GetProveedores() => await _context.Proveedores.ToListAsync();
    }
}
