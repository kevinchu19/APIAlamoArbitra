namespace APIAlamoArbitra.Models.Pedidos
{
    public class MedioDePagoDTO
    {
        public string? Identificador { get; set; }
        public string TipoConcepto { get; set; }
        public string CodigoConcepto { get; set; }
        public decimal Importe { get; set; }
        public string Observaciones { get; set; }
        public int Cuotas { get; set; }
        public string CodigoAutorizacion { get; set; }
    }
}
