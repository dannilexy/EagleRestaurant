using AutoMapper;


namespace Eagle.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
