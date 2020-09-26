using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using SchoolAdministration.Services.Identity.Domain.Entities;
using SchoolAdministration.Services.Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SchoolAdministration.Services.Identity.WebUI
{
	public class Program
	{
        public async static Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var pcontext = services.GetRequiredService<PersistedGrantDbContext>();
                    var configContext = services.GetRequiredService<ConfigurationDbContext>();
                    var usermanagement = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var rolemanagement = services.GetRequiredService<RoleManager<IdentityRole>>();
                    if (pcontext.Database.IsSqlServer())
                    {
                        pcontext.Database.Migrate();
                    }
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    await ApplicationDbContextSeed.SeedAsync(context, usermanagement,rolemanagement);

                    if (configContext.Database.IsSqlServer())
                    {
                        configContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)            
               .UseStartup<Startup>();       
    }
}
