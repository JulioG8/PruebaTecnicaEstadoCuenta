using EstadoDeCuenta.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public interface ICompraRepository
    {
        /// <summary>
        /// Obtener compras realizadas por mes.
        /// </summary>
        /// <param name="tarjetaId">ID de la Tarjeta de Crédito</param>
        /// <param name="mes">Mes (1-12)</param>
        /// <param name="anio">Año</param>
        /// <returns>Listado de Compras del Mes</returns>
        Task<IEnumerable<Compra>> ObtenerComprasPorMesAsync(int tarjetaId, int mes, int anio);

        /// <summary>
        /// Registrar una nueva compra.
        /// </summary>
        /// <param name="tarjetaId">ID de la Tarjeta de Crédito</param>
        /// <param name="fecha">Fecha de la Compra</param>
        /// <param name="descripcion">Descripción de la Compra</param>
        /// <param name="monto">Monto de la Compra</param>
        Task RegistrarCompraAsync(int tarjetaId, DateTime fecha, string descripcion, decimal monto);
    }
}



//using EstadoDeCuenta.API.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace EstadoDeCuenta.API.Repositories
//{
//    public interface ICompraRepository
//    {
//        Task<IEnumerable<Compra>> ObtenerPorTarjetaIdAsync(int tarjetaId);
//        Task CrearAsync(Compra compra);
//    }
//}
