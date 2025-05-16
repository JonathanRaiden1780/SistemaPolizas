
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;

namespace Policies.Core.Contracts.Repositories.Common
{
    public interface IRepositoryPolicies
    {
        Task<IEnumerable<PolicyResponseDto>> GetPoliciesAsync(int? userId);
        Task<PolicyResponseDto> GetPolicyByIdAsync(string id);
        Task<PolicyResponseDto> CreatePolicyAsync(CreatePolicyRequestDto policies);
        Task<PolicyResponseDto> UpdatePolicyAsync(string id, UpdatePolicyRequestDto policies);
        Task<PolicyResponseDto> ChangeStatusPoliciesAsync(string id, string status);
        Task DeletePolicyAsync(string id);
    }
}

