using AutoMapper;
using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Policies.Services.Auth;

namespace Policies.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IUnitOfWork UnitOfWork = null;
        private readonly IMapper mapper = null;
        private readonly Func<string, IServiceFactory> serviceFactory = null;
        protected readonly IConfiguration configuration;

        private IPolicieService servicePolicies = null;
        private IAuthService serviceAuth = null;
        private IClientService serviceClient = null;


        public ServiceFactory(IUnitOfWork unitOfWork, Func<string, IServiceFactory> serviceFactory, IMapper mapper, IConfiguration configuration)
        {
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
            this.serviceFactory = serviceFactory;
            this.configuration = configuration;
        }

        public IPolicieService ServicePolicies => servicePolicies ??= new ServicePolicies(UnitOfWork, serviceFactory, mapper);
        public IAuthService ServiceAuth => serviceAuth ??= new ServiceAuth(UnitOfWork, serviceFactory, mapper, configuration);
        public IClientService ServiceClient => serviceClient ??= new ServiceClient(UnitOfWork, serviceFactory, mapper);
    }
}
