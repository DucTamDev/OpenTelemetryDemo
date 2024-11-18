using Appliction.Configurations.Interfaces.Services;
using Appliction.Dtos;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using System.Diagnostics;

namespace OtelProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("all")]
        [HttpGet]
        public IList<ProductDto> GetAllProducts()
        {
            var dataHeader = HttpContext.Request.Headers;

            return _productService.GetAllProducts();
        }

        [Route("getById")]
        [HttpGet]
        public async Task<ProductDto>? GetProductById(int id)
        {
            return await _productService.GetProductById(id);
        }

        [Route("create")]
        [HttpPost]
        public async Task<bool> CreateProduct([FromBody] ProductDto productDto)
        {
            return await _productService.CreateProduct(productDto);
        }
    }
}
