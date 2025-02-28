namespace EstadoDeCuenta.API.DTOs
{
    public class RegistrarCompraDto
    {
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int TarjetaCreditoId { get; set; }
    }
}
