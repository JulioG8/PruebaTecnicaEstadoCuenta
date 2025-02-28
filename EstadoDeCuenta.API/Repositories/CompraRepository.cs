using EstadoDeCuenta.API.Data;
using EstadoDeCuenta.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        private readonly AppDbContext _context;

        public CompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Compra>> ObtenerPorTarjetaIdAsync(int tarjetaId)
        {
            return await _context.Compras
                                 .Where(c => c.TarjetaCreditoId == tarjetaId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Compra>> ObtenerComprasPorMesAsync(int tarjetaId, int mes, int anio)
        {
            var query = "EXEC sp_ObtenerComprasPorMes @TarjetaCreditoId, @Mes, @Anio";

            var parametros = new[]
            {
        new SqlParameter("@TarjetaCreditoId", tarjetaId),
        new SqlParameter("@Mes", mes),
        new SqlParameter("@Anio", anio)
    };

            return await _context.Compras
                                 .FromSqlRaw(query, parametros)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task RegistrarCompraAsync(int tarjetaCreditoId, DateTime fecha, string descripcion, decimal monto)
        {
            var query = "EXEC sp_RegistrarCompra @TarjetaCreditoId, @Fecha, @Descripcion, @Monto";

            var parametros = new[]
            {
        new SqlParameter("@TarjetaCreditoId", tarjetaCreditoId),
        new SqlParameter("@Fecha", fecha),
        new SqlParameter("@Descripcion", descripcion),
        new SqlParameter("@Monto", monto)
    };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }



        public async Task CrearAsync(Compra compra)
        {
            await _context.Compras.AddAsync(compra);
        }
    }
}
