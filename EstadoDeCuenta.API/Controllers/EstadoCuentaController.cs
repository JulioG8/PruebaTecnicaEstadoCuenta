using EstadoDeCuenta.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCuentaController : ControllerBase
    {
        private readonly ITarjetaCreditoRepository _tarjetaCreditoRepository;

        public EstadoCuentaController(ITarjetaCreditoRepository tarjetaCreditoRepository)
        {
            _tarjetaCreditoRepository = tarjetaCreditoRepository;
        }

        /// <summary>
        /// Obtener el estado de cuenta completo de una tarjeta de crédito.
        /// Incluye cálculos de:
        /// - Interés Bonificable
        /// - Cuota Mínima a Pagar
        /// - Monto Total a Pagar
        /// </summary>
        /// <param name="id">ID de la Tarjeta de Crédito</param>
        /// <returns>Estado de Cuenta Completo</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadoDeCuenta(int id)
        {
            // Configuración de los porcentajes
            decimal porcentajeInteres = 25m;
            decimal porcentajeCuotaMinima = 5m;

            // Llamada al método en el repositorio
            var estadoDeCuenta = await _tarjetaCreditoRepository
                                          .ObtenerEstadoDeCuentaAsync(id, porcentajeInteres, porcentajeCuotaMinima);

            if (estadoDeCuenta == null)
            {
                return NotFound("Tarjeta de crédito no encontrada.");
            }

            return Ok(estadoDeCuenta);
        }
    }
}
