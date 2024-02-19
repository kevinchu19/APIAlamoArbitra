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

namespace APIAlamoArbitra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<ComprobanteResponse>>> PostFacturacion([FromBody] FacturacionDTO payload)
        {
            List<ComprobanteResponse> response = new List<ComprobanteResponse>();
            bool dioError = false;

            var ControllerName = HttpContext.GetEndpoint().DisplayName;
            string JsonBody = JsonConvert.SerializeObject(payload);
            Logger.Information("{ControllerName} - Body recibido: {JsonBody}", ControllerName, JsonBody);

            //foreach (var item in payload.Items)
            //{
            //    item.Identificador = payload.Identificador;
            //}

            //foreach (var item in payload.MediosDePago)
            //{
            //    item.Identificador = payload.Identificador;
            //}

            FieldMapper mapping = new FieldMapper();
            if (!mapping.LoadMappingFile(AppDomain.CurrentDomain.BaseDirectory + @"\Services\FieldMapFiles\Facturacion.json"))
            {
                response.Add(new ComprobanteResponse(new ComprobanteDTO((string?)payload
                    .GetType()
                    .GetProperty("Identificador")
                    .GetValue(payload), "400", "Error de configuracion", "No se encontro el archivo de configuracion del endpoint", null)));

                return response;
            };


            string errorMessage = await Repository.ExecuteSqlInsertToTablaSAR(mapping.fieldMap,
                                                                            payload,
                                                                            payload.Identificador,
                                                                            Configuration["Facturacion:JobName"]);
            if (errorMessage != "")
            {
                dioError = true;
                response.Add(new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.Identificador, CultureInfo.CreateSpecificCulture("en-GB")), "400", "Bad Request", errorMessage, null)));

            }
            else
            {
                response.Add(new ComprobanteResponse(new ComprobanteDTO(Convert.ToString(payload.Identificador, CultureInfo.CreateSpecificCulture("en-GB")), "200", "OK", errorMessage, null)));
            };


            JsonBody = JsonConvert.SerializeObject(response);
            Logger.Information("{ControllerName} - Respuesta: {JsonBody}", ControllerName, JsonBody);

            if (dioError)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}