using AutoMapper;
using ECommerce_Demo_Core.Entities;
using Web_API_ECommerce_Demo.Dtos;

namespace Web_API_ECommerce_Demo.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            #region Product To ProductDto
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand,o => o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d => d.ProductType,o => o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d => d.PicUrl , o => o.MapFrom<productPictureUrlResolver>());
            #endregion
        }
    }
}
