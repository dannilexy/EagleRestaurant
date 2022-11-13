using Eagle.Services.ProductAPI.Models.DTO;

namespace Eagle.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int ProductId);
        Task<ProductDTO> CreateOrUpdateProduct(ProductDTO product);
        Task<bool> DeleteProduct(int ProductId);
    }
}
