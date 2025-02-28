using System.Threading.Tasks;

namespace EstadoDeCuenta.API.Repositories
{
    public interface IUnitOfWork
    {
        ITarjetaCreditoRepository TarjetaCreditoRepository { get; }
        ICompraRepository CompraRepository { get; }
        IPagoRepository PagoRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
