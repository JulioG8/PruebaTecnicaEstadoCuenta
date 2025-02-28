namespace EstadoDeCuenta.API.Models
{
    public class TarjetaCredito
    {
        public int Id { get; set; }
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }

        public ICollection<Compra> Compras { get; set; }
        public ICollection<Pago> Pagos { get; set; }
    }
}
