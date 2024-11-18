using Contact.Interfaces.HttpServices.Product.Dtos;

namespace Contact.Interfaces.HttpServices.Product
{
    public interface IProductApi
    {
        public Task<ProductDto> GetProductById(int id);
        public Task<IList<ProductDto>> GetAllProduct();
    }
}
