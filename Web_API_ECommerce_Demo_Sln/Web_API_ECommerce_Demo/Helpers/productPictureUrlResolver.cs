using AutoMapper;
using AutoMapper.Execution;
using ECommerce_Demo_Core.Entities.Products;
using Web_API_ECommerce_Demo.Dtos;

namespace Web_API_ECommerce_Demo.Helpers
{
    public class productPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public productPictureUrlResolver(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PicUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.PicUrl}";
            return string.Empty ;
        }
    }
}
