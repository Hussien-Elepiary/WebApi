using ECommerce_Demo_Core.Entities.Identity;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Repository;
using ECommerce_Repository.Data;
using ECommerce_Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Web_API_ECommerce_Demo.Errors;
using Web_API_ECommerce_Demo.Extentions;
using Web_API_ECommerce_Demo.Helpers;
using Web_API_ECommerce_Demo.MiddleWares;

namespace Web_API_ECommerce_Demo
{
	public class Program
	{
		//Entry Point
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Config Services 
			// Add services to the container.

			builder.Services.AddControllers(); // Allow Dpindance Injection For API services
			builder.Services.AddSwaggerServices();

			builder.Services.AddDbContext<StoreContext>(options =>
				{ 
					options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
				}
			);

			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});

			builder.Services.AddApplicationServices();

			builder.AppAuthServeice();

			#endregion
			var app = builder.Build();

			await app.AutoMigrateAsync();

            #region Confg Kistrel Middelwares

            #region Exption handler middleware user made
            app.UseMiddleware<ExceptionMiddleWare>();
			#endregion


			// Configure the HTTP request pipeline.
			#region Use Swagger as Api Doc
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}
			#endregion

			

			app.UseStatusCodePagesWithReExecute("/Error/{0}");

			app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}