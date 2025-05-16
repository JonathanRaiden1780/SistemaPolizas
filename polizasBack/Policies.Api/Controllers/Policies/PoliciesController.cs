using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Dtos;
using Policies.Core.Exceptions;
using Policies.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;
using AutoMapper;

namespace Policies.Api.Controllers.Policies
{
    [ApiController]
    [Produces(Constants.ContentType)]
    [Route(Constants.RoutePolicies, Name = Constants.PoliciesTitle)]
    public class PoliciesController : BaseController
    {
        public PoliciesController(Func<string, IServiceFactory> serviceFactory) : base(serviceFactory)
        {
        }

     /// <summary>
        /// Obtiene todas las pólizas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PolicyResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PolicyResponseDto>>> GetPolicies(int? userId = null)
        {
            try
            {
                var response = await serviceFactory("Test").ServicePolicies.GetPoliciesAsync(userId);
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
        /// Obtiene una póliza por su ID
        /// </summary>
        [HttpGet(Constants.GetPoliciesById)]
        [ProducesResponseType(typeof(PolicyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PolicyResponseDto>> GetPolicyById(string id)
        {
            try
            {
                var response = await serviceFactory("Test").ServicePolicies.GetPolicyByIdAsync(id);
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
        /// Crea una nueva póliza
        /// </summary>
        [HttpPost(Constants.CreatePolicy)]
        [ProducesResponseType(typeof(PolicyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PolicyResponseDto>> CreatePolicy([FromBody] CreatePolicyRequestDto request)
        {
            try
            {
                var response = await serviceFactory("Test").ServicePolicies.CreatepoliciesAsync(request);
                return CreatedAtAction(nameof(GetPolicyById), new { id = response.Id }, response);
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

        /// <summary>
        /// Edita una póliza
        /// </summary>
        [HttpPut(Constants.UpdatePolicy)]
        [ProducesResponseType(typeof(PolicyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PolicyResponseDto>> UpdatePolicy([FromQuery] string id ,[FromBody] UpdatePolicyRequestDto request)
        {
            try
            {
                var response = await serviceFactory("Test").ServicePolicies.UpdatepoliciesAsync(id,request);
                return CreatedAtAction(nameof(GetPolicyById), new { id = response.Id }, response);
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

        /// <summary>
        /// Autoriza/Rechaza una póliza
        /// </summary>
        [HttpGet(Constants.ChangeStatus)]
        [ProducesResponseType(typeof(PolicyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PolicyResponseDto>> ChangeStatusPoliciesAsync(string id, string status)
        {
            try
            {
                var response = await serviceFactory("Test").ServicePolicies.ChangeStatusPoliciesAsync(id, status);
                return CreatedAtAction(nameof(GetPolicyById), new { id = response.Id }, response);
            }
            catch (BusinessException ex)
            {
                var trackingCode = Guid.NewGuid().ToString();
                if (ex.Message == "Solo se pueden autorizar pólizas en estado 'Cotizada'") 
                    return Conflict(new BadRequestDto
                    {
                        Title = Constants.OriginService,
                        Errors = new[] { ex.Message },
                        TraceId = trackingCode
                    });
                else
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