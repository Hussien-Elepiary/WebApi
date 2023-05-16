﻿using ECommerce_Demo_Core.Repositories;
using ECommerce_Repository;
using Microsoft.AspNetCore.Mvc;
using Web_API_ECommerce_Demo.Errors;
using Web_API_ECommerce_Demo.Helpers;

namespace Web_API_ECommerce_Demo.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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