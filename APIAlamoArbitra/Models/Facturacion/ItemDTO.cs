namespace APIAlamoArbitra.Models.Pedidos
{
    public class ItemDTO
    {
        public string? identificador { get; set; }
        public string tipoProducto { get; set; }
        public string producto { get; set; }
        public string tipoConcepto { get; set; }
        public string concepto { get; set; }
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
        public string observaciones { get; set; }
        public string numeroReserva { get; set; }
    }
}