using EstadoDeCuenta.API.DTOs;
using EstadoDeCuenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public interface ITarjetaCreditoRepository
    {
        /// <summary>
        /// Obtener el estado de cuenta completo, incluyendo cálculos como:
        /// - Interés Bonificable
        /// - Cuota Mínima a Pagar
        /// - Monto Total a Pagar
        /// </summary>
        /// <param name="id">ID de la Tarjeta de Crédito</param>
        /// <param name="porcentajeInteres">Porcentaje de Interés Configurable</param>
        /// <param name="porcentajeCuotaMinima">Porcentaje de Cuota Mínima Configurable</param>
        /// <returns>Estado de Cuenta Completo</returns>
        Task<EstadoCuentaDto> ObtenerEstadoDeCuentaAsync(int id, decimal porcentajeInteres, decimal porcentajeCuotaMinima);

        /// <summary>
        /// Obtener todas las tarjetas de crédito.
        /// </summary>
        /// <returns>Listado de Tarjetas de Crédito</returns>
        Task<IEnumerable<TarjetaCredito>> ObtenerTodasAsync();

        /// <summary>
        /// Crear una nueva tarjeta de crédito.
        /// </summary>
        /// <param name="tarjetaCredito">Objeto TarjetaCredito a crear</param>
        Task CrearAsync(TarjetaCredito tarjetaCredito);

        /// <summary>
        /// Actualizar una tarjeta de crédito existente.
        /// </summary>
        /// <param name="tarjetaCredito">Objeto TarjetaCredito a actualizar</param>
        void Actualizar(TarjetaCredito tarjetaCredito);

        /// <summary>
        /// Eliminar una tarjeta de crédito.
        /// </summary>
        /// <param name="tarjetaCredito">Objeto TarjetaCredito a eliminar</param>
        void Eliminar(TarjetaCredito tarjetaCredito);
    }
}
