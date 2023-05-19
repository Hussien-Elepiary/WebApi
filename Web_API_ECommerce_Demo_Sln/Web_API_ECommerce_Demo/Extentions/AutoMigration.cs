using ECommerce_Demo_Core.Entities.Identity;
using ECommerce_Repository.Data;
using ECommerce_Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web_API_ECommerce_Demo.Extentions
{
    public static class AutoMigration
    {
        public static async Task<WebApplication> AutoMigrateAsync(this WebApplication app)
        {
            #region Apply Migration section
            //StoreContext dbContext =  new StoreContext();
            //await dbContext.Database.MigrateAsync();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); //a nice way to log errors on Console screens
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>(); // ask Exiplictly for the Context to auto migrate DataBase Updates
                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await dbContext.Database.MigrateAsync();
                await identityDbContext.Database.MigrateAsync();

                #region Add Data With Json File (DataSeeding)
                await storeContextSeed.SeedAsync(dbContext);

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                #endregion
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There is an error during migration");
            }
            #endregion
            return app;
        }
    }
}
