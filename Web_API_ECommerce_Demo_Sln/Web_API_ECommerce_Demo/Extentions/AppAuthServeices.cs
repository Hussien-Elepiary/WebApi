using ECommerce_Demo_Core.Entities.Identity;
using ECommerce_Demo_Core.IServices;
using ECommerce_Repository.Identity;
using ECommerce_Service.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web_API_ECommerce_Demo.Extentions
{
    public static class AppAuthServeices
    {
        public static WebApplicationBuilder AppAuthServeice(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<ITokenService,TokenService>();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequiredLength = 10;

            })
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssure"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

            return builder;
        }
    }
}
