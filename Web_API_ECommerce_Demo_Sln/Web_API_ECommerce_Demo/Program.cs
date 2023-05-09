using ECommerce_Demo_Core.Repositories;
using ECommerce_Repository;
using ECommerce_Repository.Data;
using Microsoft.EntityFrameworkCore;

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
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<StoreContext>(options =>
				{ 
					options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
				}
			);

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			#endregion

			var app = builder.Build();

			#region Apply Migration section
			//StoreContext dbContext =  new StoreContext();
			//await dbContext.Database.MigrateAsync();

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var loggerFactory = services.GetRequiredService<ILoggerFactory>(); //a nice way to log errors on Console screens
			try
			{
				var dbContext = services.GetRequiredService<StoreContext>(); // ask Exiplictly for the Context to auto migrate DataBase Updates
				await dbContext.Database.MigrateAsync();
				
				#region Add Data With Json File (DataSeeding)
				await storeContextSeed.SeedAsync(dbContext);
				#endregion
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "There is an error during migration");
			}
			#endregion

			

			#region Confg Kistrel Middelwares
			// Configure the HTTP request pipeline.
			#region Use Swagger as Api Doc
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			} 
			#endregion

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}