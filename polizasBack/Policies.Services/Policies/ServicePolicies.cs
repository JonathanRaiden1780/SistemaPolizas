using AutoMapper;
using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Contracts.Services;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;
using Policies.Core.Exceptions;
using Policies.Services;

public class ServicePolicies : BaseService, IPolicieService
{
    public ServicePolicies(IUnitOfWork UnitOfWork, Func<string, IServiceFactory> serviceFactory, IMapper mapper) :
        base(UnitOfWork, serviceFactory, mapper)
    {
    }

    public async Task<IEnumerable<PolicyResponseDto>> GetPoliciesAsync(int? userId)
    {
        try
        {
            return await UnitOfWork.RepositoryPolicies.GetPoliciesAsync(userId);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al obtener las pólizas: {ex.Message}");
        }
    }

    public async Task<PolicyResponseDto> GetPolicyByIdAsync(string id)
    {
        try
        {
            var policy = await UnitOfWork.RepositoryPolicies.GetPolicyByIdAsync(id);
            if (policy == null)
            {
                throw new BusinessException("La póliza no existe");
            }
            return policy;
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al obtener la póliza: {ex.Message}");
        }
    }

    public async Task<PolicyResponseDto> CreatepoliciesAsync(CreatePolicyRequestDto policy)
    {
        try
        {
            await ValidatePolicyData(policy);
            return await UnitOfWork.RepositoryPolicies.CreatePolicyAsync(policy);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al crear la póliza: {ex.Message}");
        }
    }

    public async Task<PolicyResponseDto> UpdatepoliciesAsync(string id, UpdatePolicyRequestDto policy)
    {
        try
        {
            var existingPolicy = await GetPolicyByIdAsync(id);
            
            if (existingPolicy.Status.ToString() != "Cotizada")
            {
                throw new BusinessException("Solo se pueden modificar pólizas en estado 'Cotizada'");
            }

            await ValidatePolicyData(policy);
            await ValidateClientAge(policy.ClientId);

            return await UnitOfWork.RepositoryPolicies.UpdatePolicyAsync(id, policy);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al actualizar la póliza: {ex.Message}");
        }
    }

    public async Task<PolicyResponseDto> ChangeStatusPoliciesAsync(string id, string status)
    {
        try
        {
            var policy = await GetPolicyByIdAsync(id);
            
            if (policy.Status.ToString() != "Cotizada")
            {
                throw new BusinessException("Solo se pueden autorizar pólizas en estado 'Cotizada'");
            }

            return await UnitOfWork.RepositoryPolicies.ChangeStatusPoliciesAsync(id, status);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al autorizar la póliza: {ex.Message}");
        }
    }

    private async Task ValidatePolicyData(IPolicyBaseData policy)
    {
        if (policy.EndDate <= policy.StartDate)
        {
            throw new BusinessException("La fecha de fin debe ser posterior a la fecha de inicio");
        }

        if (policy.EndDate != policy.StartDate.AddMonths(1))
        {
            throw new BusinessException("La fecha de fin debe ser exactamente un mes después de la fecha de inicio");
        }
    }

    private async Task ValidateClientAge(int clientId)
    {
        var client = await UnitOfWork.RepositoryClient.GetClientById(clientId);
        if (client != null)
        {
            if (client.Age < 18)
                throw new BusinessException("El cliente debe ser mayor de edad");
        }
    }

    public Task DeletepoliciesAsync(string id)
    {
        try
        {
            return UnitOfWork.RepositoryPolicies.DeletePolicyAsync(id);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al eli<minar la póliza: {ex.Message}");
        }
    }
}