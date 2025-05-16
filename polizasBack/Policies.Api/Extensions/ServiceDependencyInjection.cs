using System.Text;
using Microsoft.IdentityModel.Tokens;
using Policies.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Policies.Core.Contracts.Factories.Common;
using Policies.Repositories;
using Policies.Services;
using enums = Policies.Core.Enums;

namespace Policies.Api.Extensions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddInyeccionDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCorsSpecificOriginspolicies(configuration);
            services.AddServiceAuthApi();
            services.DependencyInjection();
            services.AddServiceFactories();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        private static IServiceCollection AddServiceAuthApi(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(Constants.SecretKey))
                    ),
                    ClockSkew = TimeSpan.Zero
                });


            return services;
        }
        private static IServiceCollection DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ServiceFactory>();
            return services;
        }
        private static IServiceCollection AddServiceFactories(this IServiceCollection services)
        {
            services.AddScoped<Func<string, IServiceFactory>>(serviceFactory => key =>
            {
                return key switch
                {
                    nameof(enums.Catalogs.Test) => serviceFactory.GetService<ServiceFactory>()
                };
            });

            return services;
        }
    }
}
