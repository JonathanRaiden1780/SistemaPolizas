using Microsoft.AspNetCore.Mvc;
using Policies.Core.Contracts.Factories.Common;

namespace Policies.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly Func<string, IServiceFactory> serviceFactory;

        protected BaseController(Func<string, IServiceFactory> serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }
    }
}
