namespace EstadoDeCuenta.API.DTOs
{
    public class EstadoCuentaDto
    {
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinimaAPagar { get; set; }
        public decimal MontoTotalAPagar { get; set; }
    }
}
