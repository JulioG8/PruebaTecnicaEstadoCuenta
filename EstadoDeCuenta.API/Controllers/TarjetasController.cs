using EstadoDeCuenta.API.Models;
using EstadoDeCuenta.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly ITarjetaCreditoRepository _tarjetaCreditoRepository;

        public TarjetasController(ITarjetaCreditoRepository tarjetaCreditoRepository)
        {
            _tarjetaCreditoRepository = tarjetaCreditoRepository;
        }

        /// <summary>
        /// Obtener todas las tarjetas de crédito.
        /// </summary>
        /// <returns>Listado de Tarjetas de Crédito</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarjetaCredito>>> ObtenerTodas()
        {
            var tarjetas = await _tarjetaCreditoRepository.ObtenerTodasAsync();

            if (tarjetas == null)
            {
                return NotFound("No se encontraron tarjetas de crédito.");
            }

            return Ok(tarjetas);
        }
    }
}
