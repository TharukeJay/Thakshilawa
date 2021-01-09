using XYZLaundry.Data;
using XYZLaundry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace XYZLaundry.Helpers
{
    public static class DataSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using IServiceScope scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // User Info                
            string firstName = "Super";
            string lastName = "Admin";
            string email = "admin@xyz.com";
            string password = "123456";
            string role = "Super Admin";
            string role2 = "Manager";
            string role3 = "User";

            if (await _userManager.FindByNameAsync(email) == null)
            {
                // Create SuperAdmins role if it doesn't exist
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                if (await roleManager.FindByNameAsync(role2) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role2));
                }
                if (await roleManager.FindByNameAsync(role3) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role3));
                }

                // Create user account if it doesn't exist
                ApplicationUser user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DateCreated = DateTime.Now.ToString()
                };

                IdentityResult result = await _userManager.CreateAsync(user, password);

                // Assign role to the user
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.FirstName));
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
