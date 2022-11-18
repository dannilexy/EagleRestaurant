using Eagle.Web.Models.DTO;

namespace Eagle.Web.Service.IService
{
    public interface IProductServices
    {
        Task<T> GetAllProductAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int Id, string token);
        Task<T> CreateProductAsync<T> (ProductDTO product, string token);
        Task<T> UpdateProductAsync<T> (ProductDTO product, string token);
        Task<T> DeleteProductAsync<T> (int id, string token);
    }
}
