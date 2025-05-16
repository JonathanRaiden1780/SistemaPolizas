using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Dtos;
using Policies.Core.Exceptions;
using Policies.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;

namespace Policies.Api.Controllers.Clients
{
    [ApiController]
    [Produces(Constants.ContentType)]
    [Route(Constants.RouteClients, Name = Constants.ClientsTitle)]
    public class ClientsController : BaseController
    {
        public ClientsController(Func<string, IServiceFactory> serviceFactory) : base(serviceFactory)
        {
        }

         /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientResponseDto>>> GetClients()
        {
            try
            {
                var response = await serviceFactory("Test").ServiceClient.GetClients();
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return BadRequest(new BadRequestDto
                {
                    
                    Title = Constants.OriginService,
                    Errors = new[] { ex.Message },
                    TraceId = trackingCode,
                    
                });
            }
            catch (Exception ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Origin = Constants.OriginService,
                    Message = new[] { ex.Message },
                    TrackingCode = trackingCode
                });
            }
        }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        [HttpGet(Constants.GetClientsById)]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientResponseDto>> GetClientById(int id)
        {
            try
            {
                var response = await serviceFactory("Test").ServiceClient.GetClientById(id);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return NotFound(new BadRequestDto
                {
                    Title = Constants.OriginService,
                    Errors = new[] { ex.Message },
                    TraceId = trackingCode
                });
            }
            catch (Exception ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Origin = Constants.OriginService,
                    Message = new[] { ex.Message },
                    TrackingCode = trackingCode
                });
            }
        }

        /// <summary>
        /// Obtiene un cliente por su username
        /// </summary>
        [HttpGet(Constants.GetClientByUser)]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientResponseDto>> GetClientById(string id)
        {
            try
            {
                var response = await serviceFactory("Test").ServiceClient.GetClientByUser(id);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return NotFound(new BadRequestDto
                {
                    Title = Constants.OriginService,
                    Errors = new[] { ex.Message },
                    TraceId = trackingCode
                });
            }
            catch (Exception ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Origin = Constants.OriginService,
                    Message = new[] { ex.Message },
                    TrackingCode = trackingCode
                });
            }
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        [HttpPost(Constants.CreateClient)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreateClient([FromBody] CreateClientRequestDto request)
        {
            try
            {
                var response = await serviceFactory("Test").ServiceClient.CreateClient(request);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return BadRequest(new BadRequestDto
                {
                    Title = Constants.OriginService,
                    Errors = new[] { ex.Message },
                    TraceId = trackingCode
                });
            }
            catch (Exception ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Origin = Constants.OriginService,
                    Message = new[] { ex.Message },
                    TrackingCode = trackingCode
                });
            }
        }
    }
}