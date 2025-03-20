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
        public async Task<ActionResult<Response<List<Estado>>>> GetEstados()
        {
            try
            {
                var estados = await _service.GetEstados();
                return Ok( new Response<List<Estado>>("success", estados) );
            }
            catch (System.Exception ex)
            {
                return BadRequest( new Response<string>("error", ex.Message) );
            }
        }

        [HttpGet("Unidades")]
        public async Task<ActionResult<Response<List<Unidad>>>> GetUnidades()
        {
            try
            {
                var unidades = await _service.GetUnidades();
                return Ok( new Response<List<Unidad>>("success", unidades) );
            }
            catch (System.Exception ex)
            {
                return BadRequest( new Response<string>("error", ex.Message) );
            }
        }

        [HttpGet("Proveedores")]
        public async Task<ActionResult<Response<List<Proveedor>>>> GetProveedores()
        {
            try
            {
                var proveedores = await _service.GetProveedores();
                return Ok( new Response<List<Proveedor>>("success", proveedores) );
            }
            catch (System.Exception ex)
            {
                return BadRequest( new Response<string>("error", ex.Message) );
            }
        }
    }
}
