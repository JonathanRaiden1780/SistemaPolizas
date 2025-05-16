using Policies.Core.Contracts.Repositories;
using Policies.Core.Exceptions;
using System.Data;
using AutoMapper;
using Dapper;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;
using Policies.Core.Helpers;

namespace Policies.Repositories.Context
{
    public class RepositoryClient : BaseRepository, IRepositoryClient
    {
        public RepositoryClient(IDbConnection connection, Func<IDbTransaction> transaction, IMapper mapper) : base(
            connection, transaction, mapper)
        {
        }

        public async Task<int> CreateClient(CreateClientRequestDto client)
        {
            return await Connection.QueryFirstOrDefaultAsync<int>(SqlQueries.Client.CreateClient, client, Transaction);
        }

        public async Task<int> UpdateClient(UpdateClientRequestDto client)
        {
            var rowsAffected = await Connection.ExecuteAsync(
               SqlQueries.Client.UpdateClient,
                client, Transaction);
            return rowsAffected == 0 ?  throw new BusinessException("No se pudo actualizar la información del cliente") : client.ClientId;
        }

        public async Task<IEnumerable<List<ClientResponseDto>>> GetClients()
        {
            return await Connection.QueryAsync<List<ClientResponseDto>>(SqlQueries.Client.GetClients,"",Transaction);
        }

        public async Task<ClientResponseDto> GetClientById(int clientId)
        {
            return await Connection.QueryFirstOrDefaultAsync<ClientResponseDto>(SqlQueries.Client.GetClientById,new{ clientId = clientId}, Transaction);
        }

        public async Task<ClientResponseDto> GetClientByUser(string email)
        {
            return await Connection.QueryFirstOrDefaultAsync<ClientResponseDto>(SqlQueries.Client.GetClientByUser, new { email = email }, Transaction);

        }
    }
}