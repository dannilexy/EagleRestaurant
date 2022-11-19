using AutoMapper;
using Eagle.Services.ShoppingCartAPI.Models;
using Eagle.Services.ShoppingCartAPI.Models.Dto;

namespace Eagle.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
                config.CreateMap<CartDto, Cart>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
