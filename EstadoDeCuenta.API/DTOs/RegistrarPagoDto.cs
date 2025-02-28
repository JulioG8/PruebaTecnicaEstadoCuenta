namespace EstadoDeCuenta.API.DTOs
{
    public class RegistrarPagoDto
    {
        public decimal Monto { get; set; }
        public int TarjetaCreditoId { get; set; }
    }
}
