using EstadoDeCuenta.API.DTOs;
using EstadoDeCuenta.API.Models;
using EstadoDeCuenta.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoRepository _pagoRepository;

        public PagosController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        /// <summary>
        /// Obtener pagos realizados por mes.
        /// </summary>
        /// <param name="tarjetaId">ID de la Tarjeta de Crédito</param>
        /// <param name="mes">Mes (1-12)</param>
        /// <param name="anio">Año</param>
        /// <returns>Listado de Pagos del Mes</returns>
        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerPagosPorMes(int tarjetaId, int mes, int anio)
        {
            var pagos = await _pagoRepository.ObtenerPagosPorMesAsync(tarjetaId, mes, anio);

            if (pagos == null)
            {
                return NotFound("No se encontraron pagos para este mes.");
            }

            return Ok(pagos);
        }

        /// <summary>
        /// Registrar un nuevo pago.
        /// </summary>
        /// <param name="pagoDto">Datos del nuevo pago</param>
        /// <returns>Pago registrado</returns>
        [HttpPost]
        public async Task<IActionResult> RegistrarPago([FromBody] RegistrarPagoDto pagoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validación adicional opcional
            if (pagoDto.TarjetaCreditoId <= 0)
            {
                return BadRequest("El ID de la Tarjeta de Crédito no es válido.");
            }

            // Ejecutar el Stored Procedure para registrar el pago
            await _pagoRepository.RegistrarPagoAsync(
                pagoDto.TarjetaCreditoId,
                DateTime.Now,
                pagoDto.Monto
            );

            return Ok("Pago registrado exitosamente.");
        }

    }
}
