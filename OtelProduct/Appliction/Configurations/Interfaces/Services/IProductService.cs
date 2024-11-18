using Appliction.Dtos;

namespace Appliction.Configurations.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductById(int id);

        IList<ProductDto> GetAllProducts();

        Task<bool> CreateProduct(ProductDto productDto);
    }
}
