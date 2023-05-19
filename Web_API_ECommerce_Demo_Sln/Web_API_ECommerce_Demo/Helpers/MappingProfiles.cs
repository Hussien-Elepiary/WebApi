using AutoMapper;
using ECommerce_Demo_Core.Entities.Cart;
using ECommerce_Demo_Core.Entities.Identity;
using ECommerce_Demo_Core.Entities.Products;
using Web_API_ECommerce_Demo.Dtos;
using Web_API_ECommerce_Demo.Dtos.AuthDtos;
using Web_API_ECommerce_Demo.Dtos.BasketDtos;

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
            #region Address To AddressDto
            CreateMap<Address, AddressDto>().ReverseMap();
            #endregion
            #region basket to basketDto
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            #endregion
        }
    }
}
