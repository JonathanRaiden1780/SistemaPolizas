using System.Reflection;
using Microsoft.OpenApi.Models;
using Policies.Core.Helpers;

namespace Policies.Api.Extensions.Swagger
{

    public static class ServiceSwagger
    {
        public static IServiceCollection AddServiceSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {


                c.SwaggerDoc(Constants.Policies, new OpenApiInfo
                {
                    Version = Constants.ServiceVersion,
                    Title = Constants.PoliciesTitle
                });

                c.SwaggerDoc(Constants.Auth, new OpenApiInfo
                {
                    Version = Constants.ServiceVersion,
                    Title = Constants.AuthTitle
                });

                c.SwaggerDoc(Constants.Clients, new OpenApiInfo
                {
                    Version = Constants.ServiceVersion,
                    Title = Constants.ClientsTitle
                });

                c.AddSecurityDefinition(Constants.Bearer, new OpenApiSecurityScheme
                {
                    Name = Constants.DefinitionName,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Constants.Bearer,
                    BearerFormat = Constants.BearerFormat,
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Constants.Bearer
                            }
                        },
                        new string[]{}
                    }
                });

                c.OperationFilter<CustomHeaderSwaggerAttribute>();
                c.OperationFilter<CustomQuerySwaggerAttribute>();
                c.CustomSchemaIds(x => x.Name.Replace(Constants.Dto, string.Empty));

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}{Constants.SwaggerExtensionXml}";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            return services;
        }
    }
}
