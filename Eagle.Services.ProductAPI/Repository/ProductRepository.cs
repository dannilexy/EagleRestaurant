using AutoMapper;
using Eagle.Services.ProductAPI.Data;
using Eagle.Services.ProductAPI.Models;
using Eagle.Services.ProductAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Eagle.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext _db, IMapper _mapper)
        {
            this._db = _db;
            this._mapper = _mapper;
        }
        public async Task<ProductDTO> CreateOrUpdateProduct(ProductDTO product)
        {
            var productToSave = _mapper.Map<Product>(product);
            if (product.ProductId > 0)
            {
                _db.Products.Update(productToSave);
            }
            else
            {
                await _db.Products.AddAsync(productToSave);
            }
            await _db.SaveChangesAsync();
            var productToReturn = _mapper.Map<ProductDTO>(productToSave);
            return productToReturn;
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == ProductId);
                if (product != null)
                {
                    _db.Products.Remove(product);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ProductDTO> GetProductById(int ProductId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == ProductId);
            var productToReturn = _mapper.Map<ProductDTO>(product);

            return productToReturn;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _db.Products.ToListAsync();
            var productsToReturn = _mapper.Map<List<ProductDTO>>(products);
            return productsToReturn;
        }

    }
}
