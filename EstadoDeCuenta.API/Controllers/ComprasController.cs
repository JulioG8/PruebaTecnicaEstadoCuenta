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
    public class ComprasController : ControllerBase
    {
        private readonly ICompraRepository _compraRepository;

        public ComprasController(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        /// <summary>
        /// Obtener compras realizadas por mes.
        /// </summary>
        /// <param name="tarjetaId">ID de la Tarjeta de Crédito</param>
        /// <param name="mes">Mes (1-12)</param>
        /// <param name="anio">Año</param>
        /// <returns>Listado de Compras del Mes</returns>
        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerComprasPorMes(int tarjetaId, int mes, int anio)
        {
            var compras = await _compraRepository.ObtenerComprasPorMesAsync(tarjetaId, mes, anio);

            if (compras == null)
            {
                return NotFound("No se encontraron compras para este mes.");
            }

            return Ok(compras);
        }

        /// <summary>
        /// Registrar una nueva compra.
        /// </summary>
        /// <param name="compraDto">Datos de la nueva compra</param>
        /// <returns>Compra registrada</returns>
        [HttpPost]
        public async Task<IActionResult> RegistrarCompra([FromBody] RegistrarCompraDto compraDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validación adicional opcional
            if (compraDto.TarjetaCreditoId <= 0)
            {
                return BadRequest("El ID de la Tarjeta de Crédito no es válido.");
            }

            // Ejecutar el Stored Procedure para registrar la compra
            await _compraRepository.RegistrarCompraAsync(
                compraDto.TarjetaCreditoId,
                DateTime.Now,
                compraDto.Descripcion,
                compraDto.Monto
            );

            return Ok("Compra registrada exitosamente.");
        }
    }
}
