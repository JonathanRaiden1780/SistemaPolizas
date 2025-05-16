using Policies.Core.Dtos.Auth.Request;
using Policies.Core.Dtos.Auth.Response;

namespace Policies.Core.Contracts.Repositories
{
    public interface IRepositoryAuth
    {
        Task<UserDto> ValidateUserAsync(LoginRequestDto credentials);
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDto request);
        Task<bool> ValidateUserAccessAsync(int userId, int policiesId);
        string? CheckPass(LoginRequestDto credentials);
        string CheckUser(int userId);
        Task<bool> CreateUser(CreateUser credentials);
    }
}