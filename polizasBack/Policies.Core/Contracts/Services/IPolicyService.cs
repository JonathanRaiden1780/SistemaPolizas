using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;

namespace Policies.Core.Contracts.Services
{
    public interface IPolicieService
    {
        Task<IEnumerable<PolicyResponseDto>> GetPoliciesAsync(int? userId);
        Task<PolicyResponseDto> GetPolicyByIdAsync(string id);
        Task<PolicyResponseDto> CreatepoliciesAsync(CreatePolicyRequestDto policies);
        Task<PolicyResponseDto> UpdatepoliciesAsync(string id, UpdatePolicyRequestDto policies);
        Task<PolicyResponseDto> ChangeStatusPoliciesAsync(string id, string status);
        Task DeletepoliciesAsync(string id);
    }
}
