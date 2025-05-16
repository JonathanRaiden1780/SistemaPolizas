using AutoMapper;
using System.Data;

namespace Policies.Repositories
{
    public class BaseRepository
    {
        protected readonly IMapper mapper;

        protected readonly Func<IDbTransaction> transaction = null;
        protected IDbConnection Connection { get; private set; }
        protected IDbTransaction Transaction => transaction();
        protected BaseRepository(IDbConnection connection, Func<IDbTransaction> transaction, IMapper mapper)
        {
            Connection = connection;
            this.transaction = transaction;
            this.mapper = mapper;
        }
    }
}
