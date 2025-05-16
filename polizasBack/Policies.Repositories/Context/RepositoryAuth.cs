using Policies.Core.Contracts.Repositories;
using Policies.Core.Dtos.Auth.Request;
using Policies.Core.Dtos.Auth.Response;
using Policies.Core.Exceptions;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Dapper;
using Policies.Core.Helpers;
using static Policies.Core.Helpers.SqlQueries;

namespace Policies.Repositories.Context
{
    public class RepositoryAuth : BaseRepository, IRepositoryAuth
    {
        public RepositoryAuth(IDbConnection connection, Func<IDbTransaction> transaction, IMapper mapper) : base(
            connection, transaction, mapper)
        {
        }
        public async Task<UserDto> ValidateUserAsync(LoginRequestDto credentials)
        {
            if(!PasswordHasher.VerifyPassword(credentials.Password, CheckPass(credentials)))
                throw new BusinessException("Usuario o contraseña incorrectos");

            var user = await Connection.QueryFirstOrDefaultAsync<UserDto>(
                SqlQueries.StoreProcedures.ValidateUser,
                 new { Username = credentials.Username } ,
                commandType: CommandType.StoredProcedure);

            if (user == null || !PasswordHasher.VerifyPassword(credentials.Password, CheckPass(credentials)))
                throw new BusinessException("Usuario o contraseña incorrectos");

            return user;
        }

        public string CheckPass(LoginRequestDto credentials)
        {
            return Connection.QueryFirstOrDefault<string>(SqlQueries.Auth.CheckPass, new { Username = credentials.Username });
        }

        public string CheckUser(int userId)
        {
            return Connection.QueryFirstOrDefault<string>(SqlQueries.Auth.CheckUser, new { Id = userId });
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDto request)
        {
            var hashedPassword = PasswordHasher.HashPassword(request.NewPassword);
            
            var rowsAffected = await Connection.ExecuteAsync(
                SqlQueries.StoreProcedures.ChangePassword,
                new 
                { 
                    UserId = userId,
                    NewPassword = hashedPassword,
                },
                commandType: CommandType.StoredProcedure);

            if (rowsAffected == 0)
                throw new BusinessException("No se pudo actualizar la contraseña");
        }

        public async Task<bool> ValidateUserAccessAsync(int userId, int policiesId)
        {
            var hasAccess = await Connection.QueryFirstOrDefaultAsync<int?>(
                SqlQueries.Auth.ValidateAccess,
                new { UserId = userId, PoliciesId = policiesId },
                Transaction);

            return hasAccess.HasValue;
        }

        public async Task<bool> CreateUser(CreateUser credentials)
        {
            var hasAccess = await Connection.QueryFirstOrDefaultAsync<int?>(
            SqlQueries.StoreProcedures.CreateUser,
                credentials,
                commandType: CommandType.StoredProcedure);

            return hasAccess.HasValue;
        }
    }
}