using Policies.Core.Helpers;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.SwaggerUI;
using Constants = Policies.Core.Helpers.Constants;

namespace Policies.Api.Extensions.Swagger
{
    public static class ConfigureSwaggerUIOptionsExt
    {
        public static SwaggerUIOptions AddSwaggerEndpointsPath(this SwaggerUIOptions c, IConfiguration configuration)
        {
            c.SwaggerEndpoint(Constants.SwaggerPathPolicies, Constants.Policies);
            c.SwaggerEndpoint(Constants.SwaggerPathAuth, Constants.Auth);
            c.SwaggerEndpoint(Constants.SwaggerPathClients, Constants.Clients);
            return c;
        }
    }
}
