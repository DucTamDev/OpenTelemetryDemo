using Contact.Interfaces.AppServices;
using Contact.Interfaces.HttpServices.Product.Dtos;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace OtelInsightsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtelTraceController : ControllerBase
    {
        internal IOtelTraceService _otelTraceService;
        public OtelTraceController(IOtelTraceService otelTraceService)
        {
            _otelTraceService = otelTraceService;
        }

        [Route("TraceGetAllProduct")]
        [HttpGet]
        public async Task<IList<ProductDto>> GetProductAll(int id)
        {
            return await _otelTraceService.TraceCallService();
        }
    }
}
