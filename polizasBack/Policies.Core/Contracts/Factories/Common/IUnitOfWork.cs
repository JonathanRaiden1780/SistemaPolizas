using Policies.Core.Contracts.Repositories;
using Policies.Core.Contracts.Repositories.Common;

namespace Policies.Core.Contracts.Factories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryPolicies RepositoryPolicies { get; }
        IRepositoryAuth RepositoryAuth { get; }
        IRepositoryClient RepositoryClient { get; }
        void BeginTransaction();
        void CommitChanges();
        void RollbackChanges();

    }
}
