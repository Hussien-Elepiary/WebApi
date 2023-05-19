using ECommerce_Demo_Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Hussien Assem",
                    Email = "Hussien.assem.98@gmail.com",
                    UserName = "Hussien.Assem",
                    PhoneNumber = "01096669113",
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
