using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SchoolAdministration.Services.Identity.Infrastructure.Factories
{
    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            var operationOptions = new OperationalStoreOptions();

            optionsBuilder.UseSqlServer(@"Server=SOMAYA-IBRAHIM\SQLEXPRESS;Database=SchoolAdministration.Dev.DB.Identity;User Id=sa;Password=1234;MultipleActiveResultSets=true",
                sqlServerOptionsAction: o => o.MigrationsAssembly("SchoolAdministration.Services.Identity.Infrastructure"));

            return new PersistedGrantDbContext(optionsBuilder.Options, operationOptions);
        }
    }

}
