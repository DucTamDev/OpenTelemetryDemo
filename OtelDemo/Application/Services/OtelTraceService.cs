using Contact.Interfaces.AppServices;
using Contact.Interfaces.HttpServices.Product;
using Contact.Interfaces.HttpServices.Product.Dtos;
using System.Diagnostics;

namespace Application.Services
{
    public class OtelTraceService : IOtelTraceService
    {
        private readonly IProductApi _productApi;
        public OtelTraceService(IProductApi productApi)
        {
            _productApi = productApi;
        }
        public void TestTrace()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ProductDto>> TraceCallService()
        {
            Activity.Current?.AddBaggage("userId", "0012");

            return await _productApi.GetAllProduct();
        }
    }
}
