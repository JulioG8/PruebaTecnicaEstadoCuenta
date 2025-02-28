namespace EstadoDeCuenta.Web.Models
{
    public class PagoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int TarjetaCreditoId { get; set; }
    }
}
