using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Contracts.Services;
using Policies.Core.Dtos.Auth.Request;
using Policies.Core.Dtos.Auth.Response;
using Policies.Core.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Policies.Services.Auth
{
    public class ServiceAuth : BaseService, IAuthService
    {
        private readonly IConfiguration configuration;

        public ServiceAuth(IUnitOfWork unitOfWork, Func<string, IServiceFactory> serviceFactory, IMapper mapper, IConfiguration configuration)
            : base(unitOfWork, serviceFactory, mapper)
        {
            this.configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto credentials)
        {
            if (string.IsNullOrEmpty(credentials.Username) || string.IsNullOrEmpty(credentials.Password))
            {
                throw new BusinessException("Credenciales inválidas");
            }

            var user = await UnitOfWork.RepositoryAuth.ValidateUserAsync(credentials);
            if (user == null)
            {
                throw new BusinessException("Usuario o contraseña incorrectos");
            }

            var response = new LoginResponseDto
            {
                User = user,
                Token = GenerateJwtToken(user)
            };

            return response;
        }

        public async Task ChangePasswordAsync(ChangePasswordRequestDto passwordData)
        {
            if (string.IsNullOrEmpty(passwordData.OldPassword) || string.IsNullOrEmpty(passwordData.NewPassword))
            {
                throw new BusinessException("Las contraseñas no pueden estar vacías");
            }

            if (passwordData.OldPassword == passwordData.NewPassword)
            {
                throw new BusinessException("La nueva contraseña debe ser diferente a la actual");
            }

            var userName = UnitOfWork.RepositoryAuth.CheckUser(passwordData.idUser);

            if (userName == null)
            {
                throw new BusinessException("Usuario no encontrado");
            }

            var credentials = new LoginRequestDto 
            { 
                Username = userName,
                Password = passwordData.OldPassword 
            };
            
            var user = await UnitOfWork.RepositoryAuth.ValidateUserAsync(credentials);
            if (user == null)
            {
                throw new BusinessException("La contraseña actual es incorrecta");
            }

            await UnitOfWork.RepositoryAuth.ChangePasswordAsync(user.Id, passwordData);
        }

        public async Task<bool> ValidatepoliciesAccessAsync(int userId, int policiesId)
        {
            return await UnitOfWork.RepositoryAuth.ValidateUserAccessAsync(userId, policiesId);
        }

        private string GenerateJwtToken(UserDto user)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}