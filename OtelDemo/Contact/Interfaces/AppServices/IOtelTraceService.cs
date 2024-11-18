using Contact.Interfaces.HttpServices.Product.Dtos;

namespace Contact.Interfaces.AppServices
{
    public interface IOtelTraceService
    {
        void TestTrace();

        Task<IList<ProductDto>> TraceCallService();
    }
}
