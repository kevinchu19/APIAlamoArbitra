namespace APIAlamoArbitra.Models.Pedidos
{
    public class MedioDePagoDTO
    {
        public string? identificador { get; set; }
        public string tipoConcepto { get; set; }
        public string codigoConcepto { get; set; }
        public decimal importe { get; set; }
        public string observaciones { get; set; }
        public int cuotas { get; set; }
        public string codigoAutorizacion { get; set; }
    }
}
