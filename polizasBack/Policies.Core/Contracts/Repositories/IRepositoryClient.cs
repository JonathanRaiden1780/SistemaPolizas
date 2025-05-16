using Policies.Core.Dtos.Auth.Request;
using Policies.Core.Dtos.Auth.Response;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;

namespace Policies.Core.Contracts.Repositories
{
    public interface IRepositoryClient
    {
        Task<int> CreateClient(CreateClientRequestDto client);
        Task<IEnumerable<List<ClientResponseDto>>> GetClients();
        Task<int> UpdateClient(UpdateClientRequestDto client);
        Task<ClientResponseDto> GetClientById(int clientId);
        Task<ClientResponseDto> GetClientByUser(string email);


    }
}