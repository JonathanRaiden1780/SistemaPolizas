using AutoMapper;
using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Contracts.Services;
using Policies.Core.Dtos.Auth.Request;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;
using Policies.Core.Exceptions;
using Policies.Services;

public class ServiceClient : BaseService, IClientService
{
    public ServiceClient(IUnitOfWork UnitOfWork, Func<string, IServiceFactory> serviceFactory, IMapper mapper) :
        base(UnitOfWork, serviceFactory, mapper)
    {
    }

    public async Task<int> CreateClient(CreateClientRequestDto client)
    {
        try
        { 
            var user = GetClientByUser(client.Email);
            if (user.Result == null)
            {
                if (client.Age < 18)
                    throw new BusinessException("El cliente debe ser mayor de edad");

                var password = client.Email.Split("@");
                var passHash = PasswordHasher.HashPassword(password[0]);
                var userNew = new CreateUser()
                {
                    Password = passHash,
                    Role = "Client",
                    Username = client.Email
                };
                var userCreated = UnitOfWork.RepositoryAuth.CreateUser(userNew).Result;
                if (!userCreated)
                    throw new BusinessException("Error al generar el usuario del cliente");

                var id = await UnitOfWork.RepositoryClient.CreateClient(client);

                return id;
            }
            else
            {
                var updateData = mapper.Map<UpdateClientRequestDto>(client);
                updateData.ClientId = user.Result.Id;

                var existingClientData = mapper.Map<UpdateClientRequestDto>(user.Result);
                if (!existingClientData.Equals(updateData))
                {
                    return await UnitOfWork.RepositoryClient.UpdateClient(updateData);
                }
                else
                {
                    return user.Result.Id; 
                }
            }
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al crear nuevo cliente: {ex.Message}");
        }
    }

    public async Task<int> UpdateClient(UpdateClientRequestDto client)
    {
        try
        {
            return await UnitOfWork.RepositoryClient.UpdateClient(client);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al actualizar cliente: {ex.Message}");
        }
    }

    public async Task<IEnumerable<List<ClientResponseDto>>> GetClients()
    {
        try
        {
            return await UnitOfWork.RepositoryClient.GetClients();
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al obtener los clientes: {ex.Message}");
        }
    }
    
    public async Task<ClientResponseDto> GetClientById(int clientId)
    {
        try
        {
            return await UnitOfWork.RepositoryClient.GetClientById(clientId);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al obtener el cliente solicitado: {ex.Message}");
        }
    }

    public async Task<ClientResponseDto> GetClientByUser(string email)
    {
        try
        {
            return await UnitOfWork.RepositoryClient.GetClientByUser(email);
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al obtener el cliente solicitado: {ex.Message}");
        }
    }
}