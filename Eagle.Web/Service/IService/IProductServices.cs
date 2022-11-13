using Eagle.Web.Models.DTO;

namespace Eagle.Web.Service.IService
{
    public interface IProductServices
    {
        Task<T> GetAllProductAsync<T>();
        Task<T> GetProductByIdAsync<T>(int Id);
        Task<T> CreateProductAsync<T> (ProductDTO product);
        Task<T> UpdateProductAsync<T> (ProductDTO product);
        Task<T> DeleteProductAsync<T> (int id);
    }
}
