using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SchoolAdministration.Services.Identity.Infrastructure.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public ApplicationDbContextFactory()
        {

        }
        public ApplicationDbContextFactory(ICurrentUserService currentUserService,
            IDateTime dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();


            optionsBuilder.UseSqlServer(@"Server=SOMAYA-IBRAHIM\SQLEXPRESS;Database=SchoolAdministration.Dev.DB.Identity;User Id=sa;Password=1234;MultipleActiveResultSets=true",
                sqlServerOptionsAction: o => o.MigrationsAssembly("SchoolAdministration.Services.Identity.Infrastructure"));


            return new ApplicationDbContext(optionsBuilder.Options, _currentUserService, _dateTime);
        }
    }

}
