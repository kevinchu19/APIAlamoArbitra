namespace APIAlamoArbitra.Models.Pedidos
{
    public class FacturacionDTO
    {
        public string Identificador { get; set; }
        public string Empresa { get; set; }
        public string CircuitoOrigen { get; set; }
        public string CircuitoAplicacion { get; set; }
        public string PuntoDeVenta { get; set; }
        public string ComprobanteVentas { get; set; }
        public string FechaContable { get; set; }
        public string Cliente { get; set; }
        public string ListaPrecios { get; set; }
        public string Texto { get; set; }
        public string Jurisdiccion { get; set; }
        public string TipoExportacion { get; set; }
        public string NombreCliente { get; set; }
        public string TipoDeDocumentoFacturacion { get; set; }
        public string NumeroDeDocumentoFacturacion { get; set; }
        public string CondicionDeIvaFacturacion { get; set; }
        public string DireccionFacturacion { get; set; }
        public string JurisdiccionFacturacion { get; set; }
        public string PaisFacturacion { get; set; }
        public string CodigoPostalFacturacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public ICollection<MedioDePagoDTO> MediosDePago { get; set; }
        public ICollection<ItemDTO> Items { get; set; }
    }
}
