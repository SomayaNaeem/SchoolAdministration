using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Configuration;
using SchoolAdministration.Services.Identity.Application.Common.Exceptions;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Domain.Entities;
using SchoolAdministration.Services.Identity.Infrastructure.Configuration;
using SchoolAdministration.Services.Identity.Infrastructure.Identity;
using SchoolAdministration.Services.Identity.Infrastructure.Persistence;
using SchoolAdministration.Services.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolAdministration.Services.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment, string migrationsAssembly)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.User.RequireUniqueEmail = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<ApplicationUser>)));
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.AllowedForNewUsers = true;
            })
          .AddEntityFrameworkStores<ApplicationDbContext>().AddErrorDescriber<CustomIdentityErrorDescriber>().AddDefaultTokenProviders();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddIdentityServer()
           .AddDeveloperSigningCredential()
             .AddConfigurationStore(options =>
             {
                 options.ConfigureDbContext = builder =>
                   builder.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                               sql => sql.MigrationsAssembly(migrationsAssembly));
             })
             .AddInMemoryIdentityResources(Config.GetResources())
         .AddInMemoryApiResources(Config.GetApis())
         .AddInMemoryClients(Config.GetClients(configuration))
         .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                sql => sql.MigrationsAssembly(migrationsAssembly));

            // this enables automatic token cleanup. this is optional.
            options.EnableTokenCleanup = true;
    })
    .AddAspNetIdentity<ApplicationUser>();
            services.AddHttpContextAccessor();
            services.AddMvcCore().AddAuthorization()
            .AddNewtonsoftJson();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                 .AddOAuth2Introspection(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                 {
                     options.Authority = configuration.GetSection("Identity:IdentityAuthUrl").Value;
                     // this maps to the API resource name and secret
                     options.ClientId = configuration.GetSection("Identity:APIName").Value;
                     options.ClientSecret = configuration.GetSection("Identity:API_secret").Value;
                 });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            return services;


        }

    }

}
