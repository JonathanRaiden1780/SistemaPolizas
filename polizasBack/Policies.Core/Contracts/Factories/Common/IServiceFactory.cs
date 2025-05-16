using Policies.Core.Contracts.Services;

namespace Policies.Core.Contracts.Factories.Common
{
    public interface IServiceFactory
    {
        public IPolicieService ServicePolicies { get; }
        public IAuthService ServiceAuth { get; }
        public IClientService ServiceClient { get; }

    }
}
