using Dapper;
using EstadoDeCuenta.API.Data;
using EstadoDeCuenta.API.DTOs;
using EstadoDeCuenta.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;



namespace EstadoDeCuenta.API.Repositories
{
    public class TarjetaCreditoRepository : ITarjetaCreditoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public TarjetaCreditoRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
        }


        //public TarjetaCreditoRepository(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public void Actualizar(TarjetaCredito tarjetaCredito)
        {
            throw new NotImplementedException();
        }

        public Task CrearAsync(TarjetaCredito tarjetaCredito)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(TarjetaCredito tarjetaCredito)
        {
            throw new NotImplementedException();
        }

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
        public async Task<EstadoCuentaDto> ObtenerEstadoDeCuentaAsync(int id, decimal porcentajeInteres, decimal porcentajeCuotaMinima)
        {
            // Obtener la cadena de conexión desde Configuration
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Usar Dapper para llamar al Stored Procedure
            using (var connection = new SqlConnection(connectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@TarjetaCreditoId", id);
                parametros.Add("@PorcentajeInteres", porcentajeInteres);
                parametros.Add("@PorcentajeCuotaMinima", porcentajeCuotaMinima);

                var query = "sp_ObtenerEstadoDeCuenta";

                var resultado = await connection.QueryFirstOrDefaultAsync<EstadoCuentaDto>(
                    query,
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return resultado;
            }
        }

        public async Task<IEnumerable<TarjetaCredito>> ObtenerTodasAsync()
        {
            return await _context.TarjetasCredito
                                 .AsNoTracking()
                                 .ToListAsync();
        }

    }
}
