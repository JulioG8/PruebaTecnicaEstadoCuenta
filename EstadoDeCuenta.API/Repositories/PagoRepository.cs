using EstadoDeCuenta.API.Data;
using EstadoDeCuenta.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly AppDbContext _context;

        public PagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> ObtenerPorTarjetaIdAsync(int tarjetaId)
        {
            return await _context.Pagos
                                 .Where(p => p.TarjetaCreditoId == tarjetaId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Pago>> ObtenerPagosPorMesAsync(int tarjetaId, int mes, int anio)
        {
            var query = "EXEC sp_ObtenerPagosPorMes @TarjetaCreditoId, @Mes, @Anio";

            var parametros = new[]
            {
        new SqlParameter("@TarjetaCreditoId", tarjetaId),
        new SqlParameter("@Mes", mes),
        new SqlParameter("@Anio", anio)
    };

            return await _context.Pagos
                                 .FromSqlRaw(query, parametros)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task RegistrarPagoAsync(int tarjetaCreditoId, DateTime fecha, decimal monto)
        {
            var query = "EXEC sp_RegistrarPago @TarjetaCreditoId, @Fecha, @Monto";

            var parametros = new[]
            {
        new SqlParameter("@TarjetaCreditoId", tarjetaCreditoId),
        new SqlParameter("@Fecha", fecha),
        new SqlParameter("@Monto", monto)
    };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }


        public async Task CrearAsync(Pago pago)
        {
            await _context.Pagos.AddAsync(pago);
        }
    }
}
