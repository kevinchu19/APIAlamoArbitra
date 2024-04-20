using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using System.Globalization;
using APIAlamoArbitra.Models.Response.Comprobante;
using APIAlamoArbitra.Models.Pedidos;
using APIAlamoArbitra.Services;
using APIAlamoArbitra.Repositories;
using APIAlamoArbitra.Services.ApiKey;

namespace APIAlamoArbitra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey]
    public class FacturacionController : ControllerBase
    {

        private Serilog.ILogger Logger { get; set; }

        public FacturacionController(Serilog.ILogger logger, FacturacionRepository repository, IConfiguration configuration)
        {
            Logger = logger;
            Repository = repository;
            Configuration = configuration;
        }

        public FacturacionRepository Repository { get; }
        public IConfiguration Configuration { get; }

        
        [HttpPost]
        
        public async Task<ActionResult<ComprobanteResponse>> PostFacturacion([FromBody] FacturacionDTO payload)
        {
            

            var ControllerName = HttpContext.GetEndpoint().DisplayName;
            string JsonBody = JsonConvert.SerializeObject(payload);
            Logger.Information("{ControllerName} - Body recibido: {JsonBody}", ControllerName, JsonBody);

            
            FieldMapper mapping = new FieldMapper();
            if (!mapping.LoadMappingFile(AppDomain.CurrentDomain.BaseDirectory + @"\Services\FieldMapFiles\Facturacion.json"))
            {

                return BadRequest(new ComprobanteResponse(new ComprobanteDTO((string?)payload
                    .GetType()
                    .GetProperty("identificador")
                    .GetValue(payload), "400", "Error de configuracion", "No se encontro el archivo de configuracion del endpoint", null)));
            };


            string errorMessage = await Repository.ExecuteSqlInsertToTablaSAR(mapping.fieldMap,
                                                                            payload,
                                                                            payload.identificador,
                                                                            Configuration["Facturacion:JobName"]);
            if (errorMessage != "")
            {
                return BadRequest(new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.identificador, CultureInfo.CreateSpecificCulture("en-GB")), "400", "Bad Request", errorMessage, null)));

            }
            else
            {
                return Ok(new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.identificador, CultureInfo.CreateSpecificCulture("en-GB")), "200", "OK", errorMessage, null)));
            };


            //JsonBody = JsonConvert.SerializeObject(response);
            //Logger.Information("{ControllerName} - Respuesta: {JsonBody}", ControllerName, JsonBody);

        }

        
        [HttpGet]
        [Route("{identificador}")]
        public async Task<ActionResult<ComprobanteResponse>> GetFacturacion(string identificador)
        {
            ComprobanteResponse respuesta = await Repository.GetTransaccion(identificador, "SAR_FCRMVH");

            switch (respuesta.response.status)
            {
                case "404":
                    return NotFound(respuesta);
                    break;
                default:
                    return Ok(respuesta);
                    break;
            }

        }

    }
}