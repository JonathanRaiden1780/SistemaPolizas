using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;

namespace Policies.Core.Contracts.Services
{
    public interface IClientService
    {
        Task<int> CreateClient(CreateClientRequestDto client);
        Task<IEnumerable<List<ClientResponseDto>>> GetClients();
        Task<ClientResponseDto> GetClientById(int clientId);
        Task<ClientResponseDto> GetClientByUser(string email);
        Task<int> UpdateClient(UpdateClientRequestDto client);
    }
}
