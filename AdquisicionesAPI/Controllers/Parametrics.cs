using Microsoft.AspNetCore.Mvc;
using AdquisicionesAPI.Services;
using AdquisicionesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdquisicionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametricsController : ControllerBase
    {
        private readonly ParametricsService _service;

        public ParametricsController(ParametricsService service)
        {
            _service = service;
        }

        [HttpGet("Estados")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await _service.GetEstados();
        }

        [HttpGet("Unidades")]
        public async Task<ActionResult<IEnumerable<Unidad>>> GetUnidades()
        {
            return await _service.GetUnidades();
        }

        [HttpGet("Proveedores")]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _service.GetProveedores();
        }
    }
}
