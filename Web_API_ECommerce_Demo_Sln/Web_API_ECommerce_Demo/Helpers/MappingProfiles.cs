using AutoMapper;
using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Entities.Cart;
using ECommerce_Demo_Core.Entities.Identity;
using ECommerce_Demo_Core.Entities.Order_Aggregate;
using ECommerce_Demo_Core.Entities.Products;
using Web_API_ECommerce_Demo.Dtos;
using Web_API_ECommerce_Demo.Dtos.AuthDtos;
using Web_API_ECommerce_Demo.Dtos.BasketDtos;
using Web_API_ECommerce_Demo.Dtos.Orders;

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
                .ForMember(d => d.PictureUrl, o => o.MapFrom<productPictureUrlResolver>());
            #endregion
            #region Address To AddressDto
            CreateMap<ECommerce_Demo_Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            #endregion
            #region basket to basketDto
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            #endregion
            #region Order
            CreateMap<AddressDto, ECommerce_Demo_Core.Entities.Order_Aggregate.Address>();

            #region OrderToReturnDto
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.ShippingAddress, o=>o.MapFrom(s=>s.ShippingAddress)).ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductID,O=>O.MapFrom(S=>S.Product.ProductID))
                .ForMember(d => d.ProductName,O=>O.MapFrom(S=>S.Product.ProductName))
                .ForMember(d => d.PictureUrl,O=>O.MapFrom(S=>S.Product.PictureUrl));
            #endregion
            #endregion
        }
    }
}
