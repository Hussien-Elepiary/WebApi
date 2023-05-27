using ECommerce_Demo_Core.IRepositories;
using ECommerce_Demo_Core.IServices;
using ECommerce_Demo_Core.UnitOfWork;
using ECommerce_Repository;
using ECommerce_Service.Order_Service;
using ECommerce_Service.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web_API_ECommerce_Demo.Errors;
using Web_API_ECommerce_Demo.Helpers;

namespace Web_API_ECommerce_Demo.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IPaymentService, PaymentService>();

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddAutoMapper(typeof(MappingProfiles));

            #region Handling Validation Error
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {


                    var errors = context.ModelState.Where(P => P.Value.Errors.Count() > 0)/* Gets all the Errors */
                                                   .SelectMany(P => P.Value.Errors)
                                                   .Select(E => E.ErrorMessage)
                                                   .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            #endregion

            return services;
        }
    }
}
