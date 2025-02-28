using EstadoDeCuenta.API.Data;
using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ITarjetaCreditoRepository TarjetaCreditoRepository { get; }
        public ICompraRepository CompraRepository { get; }
        public IPagoRepository PagoRepository { get; }

        public UnitOfWork(AppDbContext context,
            ITarjetaCreditoRepository tarjetaCreditoRepository,
            ICompraRepository compraRepository,
            IPagoRepository pagoRepository)
        {
            _context = context;
            TarjetaCreditoRepository = tarjetaCreditoRepository;
            CompraRepository = compraRepository;
            PagoRepository = pagoRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
