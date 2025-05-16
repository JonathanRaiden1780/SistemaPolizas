using System.Data;
using System.Data.SqlClient;
using Policies.Repositories.Context;
using Microsoft.Extensions.Configuration;
using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Contracts.Repositories.Common;
using AutoMapper;
using Policies.Core.Contracts.Repositories;

namespace Policies.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDbConnection connection = null;
        private bool _disposed;
        private IDbTransaction transaction = null;
        private readonly IMapper mapper = null;

        private IRepositoryPolicies repositoryPolicies = null;
        private IRepositoryAuth repositoryAuth = null;
        private IRepositoryClient repositoryClient = null;

        public UnitOfWork(IConfiguration configuration, IMapper mapper)
        {
            this.mapper = mapper;
            connection = new SqlConnection(configuration.GetConnectionString("ConexionComunes"));
            connection.Open();
        }

        public IRepositoryPolicies RepositoryPolicies => repositoryPolicies ??= new RepositoryPolicies(connection, () => transaction, mapper);
        public IRepositoryAuth RepositoryAuth => repositoryAuth ??= new RepositoryAuth(connection, () => transaction, mapper);
        public IRepositoryClient RepositoryClient => repositoryClient ??= new RepositoryClient(connection, () => transaction, mapper);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CommitChanges()
        {
            transaction.Commit();
            transaction = null;
        }

        public void RollbackChanges()
        {
            transaction.Rollback();
            transaction = null;
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
