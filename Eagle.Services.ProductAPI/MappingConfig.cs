using AutoMapper;
using Eagle.Services.ProductAPI.Models;
using Eagle.Services.ProductAPI.Models.DTO;

namespace Eagle.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
