namespace EstadoDeCuenta.Web.Models
{
    public class CompraDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int TarjetaCreditoId { get; set; }
    }
}
