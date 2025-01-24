using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            // Gets the RoleManager from the serviceProvider
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Checks if the Admin Role exists, if not it creates the role
            if (! await roleManager.RoleExistsAsync(Roles.Admin))
            {
               await  roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            };
            //JobSeeker Role
            if (!await roleManager.RoleExistsAsync(Roles.JobSeeker))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.JobSeeker));
            };
            //Employer Role
            if (!await roleManager.RoleExistsAsync(Roles.Employer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Employer));
            };
        }
    }
    
}
