using EstadoDeCuenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public interface IPagoRepository
    {
        /// <summary>
        /// Obtener pagos realizados por mes.
        /// </summary>
        /// <param name="tarjetaId">ID de la Tarjeta de Crédito</param>
        /// <param name="mes">Mes (1-12)</param>
        /// <param name="anio">Año</param>
        /// <returns>Listado de Pagos del Mes</returns>
        Task<IEnumerable<Pago>> ObtenerPagosPorMesAsync(int tarjetaId, int mes, int anio);

        /// <summary>
        /// Registrar un nuevo pago.
        /// </summary>
        /// <param name="tarjetaId">ID de la Tarjeta de Crédito</param>
        /// <param name="fecha">Fecha del Pago</param>
        /// <param name="monto">Monto del Pago</param>
        Task RegistrarPagoAsync(int tarjetaId, DateTime fecha, decimal monto);
    }
}


//using EstadoDeCuenta.API.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace EstadoDeCuenta.API.Repositories
//{
//    public interface IPagoRepository
//    {
//        Task<IEnumerable<Pago>> ObtenerPorTarjetaIdAsync(int tarjetaId);
//        Task CrearAsync(Pago pago);
//    }
//}
