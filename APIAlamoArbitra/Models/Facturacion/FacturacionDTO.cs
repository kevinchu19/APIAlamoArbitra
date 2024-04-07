namespace APIAlamoArbitra.Models.Pedidos
{
    public class FacturacionDTO
    {
        public string identificador { get; set; }
        public string empresa { get; set; }
        public string circuitoOrigen { get; set; }
        public string circuitoAplicacion { get; set; }
        public string puntoDeVenta { get; set; }
        public string comprobanteVentas { get; set; }
        public string fechaContable { get; set; }
        public string cliente { get; set; }
        public string listaPrecios { get; set; }
        public string texto { get; set; }
        public string jurisdiccion { get; set; }
        public string tipoExportacion { get; set; }
        public string nombreCliente { get; set; }
        public string tipoDeDocumentoFacturacion { get; set; }
        public string numeroDeDocumentoFacturacion { get; set; }
        public string condicionDeIvaFacturacion { get; set; }
        public string direccionFacturacion { get; set; }
        public string jurisdiccionFacturacion { get; set; }
        public string paisFacturacion { get; set; }
        public string codigoPostalFacturacion { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }

        public ICollection<MedioDePagoDTO> mediosDePago { get; set; }
        public ICollection<ItemDTO> items { get; set; }
    }
}
