namespace APIAlamoArbitra.Models.Pedidos
{
    public class ItemDTO
    {
        public string? Identificador { get; set; }
        public string TipoProducto { get; set; }
        public string Producto { get; set; }
        public string TipoConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public string Observaciones { get; set; }
        public string NumeroReserva { get; set; }
    }
}