using System.Data;
using System.Data.SqlClient;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Contracts.Repositories.Common;
using AutoMapper;
using Dapper;
using Policies.Core.Dtos.Policy.Response;
using Policies.Core.Exceptions;
using Policies.Core.Helpers;

namespace Policies.Repositories.Context
{
    public class RepositoryPolicies : BaseRepository, IRepositoryPolicies
    {
        public RepositoryPolicies(IDbConnection connection, Func<IDbTransaction> transaction, IMapper mapper) 
            : base(connection, transaction, mapper)
        {
        }

        public async Task<PolicyResponseDto> GetPolicyByIdAsync(string id)
        {
            try
            {
                var result = await Connection.QueryAsync<PolicyResponseDto, ClientResponseDto, PolicyResponseDto>(
                    SqlQueries.Policies.GetById,
                    (policy, client) =>
                    {
                        policy.Client = client;
                        return policy;
                    },
                    param: new { Id = id },
                    splitOn: "ClientId",
                    transaction: Transaction);

                var policy = result.FirstOrDefault();   
                if (policy == null)
                    throw new BusinessException("La póliza no existe");

                return policy;
            }
            catch (SqlException ex)
            {
                throw new BusinessException($"Error al consultar la póliza: {ex.Message}");
            }
        }
        public async Task<IEnumerable<PolicyResponseDto>> GetPoliciesAsync(int? userId = null)
        {
            try
            {
                var policies = await Connection.QueryAsync<PolicyResponseDto, ClientResponseDto, PolicyResponseDto>(
                    SqlQueries.StoreProcedures.GetAll,
                    (policy, client) =>
                    {
                        policy.Client = client;
                        return policy;
                    },
                    param: new { UserId = userId }, 
                    splitOn: "ClientId",
                    commandType: CommandType.StoredProcedure);

                return policies;
            }
            catch (SqlException ex)
            {
                throw new BusinessException($"Error al consultar la póliza: {ex.Message}");
            }
        }

      

        public async Task<PolicyResponseDto> CreatePolicyAsync(CreatePolicyRequestDto policy)
        {
            try
            {
                var id = await Connection.ExecuteScalarAsync<string>(
                    SqlQueries.StoreProcedures.CreatePolicy,
                    new { 
                        policy.PolicyNumber,
                        policy.Type,
                        policy.ClientId,
                        policy.StartDate,
                        policy.EndDate,
                        policy.Amount,
                    },
                    commandType: CommandType.StoredProcedure);

                return await GetPolicyByIdAsync(policy.PolicyNumber);
            }
            catch (SqlException ex)
            {
                throw new BusinessException($"Error al crear la póliza: {ex.Message}");
            }
        }

        public async Task<PolicyResponseDto> UpdatePolicyAsync(string id, UpdatePolicyRequestDto policy)
        {
            try
            {
                var rowsAffected = await Connection.ExecuteAsync(
                    SqlQueries.Policies.Update,
                    new { 
                        Id = id, 
                        policy.Type,
                        policy.StartDate,
                        policy.ClientId,
                        policy.EndDate,
                        policy.Amount
                    },
                    Transaction);

                if (rowsAffected == 0)
                    throw new BusinessException("La póliza no existe");

                return await GetPolicyByIdAsync(id);
            }
            catch (SqlException ex)
            {
                throw new BusinessException($"Error al actualizar la póliza: {ex.Message}");
            }
        }

        public async Task<PolicyResponseDto> ChangeStatusPoliciesAsync(string id, string status)
        {
            try
            {
                var rowsAffected = await Connection.ExecuteAsync(
                    status == "Autorizada" ? SqlQueries.Policies.Authorize : SqlQueries.Policies.Reject,
                    new { Id = id },
                    Transaction);

                if (rowsAffected == 0)
                    throw new BusinessException("La póliza no existe");

                return await GetPolicyByIdAsync(id);
            }
            catch (SqlException ex)
            {
                throw new BusinessException($"Error al autorizar la póliza: {ex.Message}");
            }
        }

        public async Task DeletePolicyAsync(string id)
        {
            try
            {
                var rowsAffected = await Connection.ExecuteAsync(
                    SqlQueries.Policies.Delete,
                    new { Id = id },
                    Transaction);

                if (rowsAffected == 0)
                    throw new BusinessException("La póliza no existe");
            }
            catch (SqlException ex)
            {
                throw new BusinessException($"Error al eliminar la póliza: {ex.Message}");
            }
        }
    }
}