using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        //自動映射 startup會用到
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();  //也可以.ReverseMap
                config.CreateMap<Product, ProductDto>();
            });

            return mappingConfig;
        }
         
    }
}
