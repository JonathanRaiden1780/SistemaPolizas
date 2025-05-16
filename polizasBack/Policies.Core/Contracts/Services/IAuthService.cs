 using Policies.Core.Dtos.Auth.Request;
 using Policies.Core.Dtos.Auth.Response;

namespace Policies.Core.Contracts.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto credentials);
        Task ChangePasswordAsync(ChangePasswordRequestDto passwordData);
        Task<bool> ValidatepoliciesAccessAsync(int userId, int policiesId);
    }

}
