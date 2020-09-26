using SchoolAdministration.Services.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            // Seed, if necessary
            var student =await roleManager.FindByNameAsync("Student");
            if (student==null)
            {
                student = new IdentityRole()
                {
                    Name = "Student"
                };
                _ = roleManager.CreateAsync(student);
            }
            var HR = await roleManager.FindByNameAsync("HR");
            if (HR == null)
            {
                HR = new IdentityRole()
                {
                    Name = "HR"
                };
                _ = roleManager.CreateAsync(HR);
            }

            // Create default administrator
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
            if (!userManager.Users.All(s=>s.Id== defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
            if (!userManager.IsInRoleAsync(defaultUser,HR.Name).Result)
            {
                _ = userManager.AddToRoleAsync(defaultUser, HR.Name).Result;
            }
        }
    }
}
