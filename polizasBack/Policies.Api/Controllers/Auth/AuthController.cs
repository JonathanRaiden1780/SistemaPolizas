using Microsoft.AspNetCore.Mvc;
using Policies.Core.Contracts.Factories.Common;
using Policies.Core.Dtos;
using Policies.Core.Dtos.Auth.Request;
using Policies.Core.Dtos.Auth.Response;
using Policies.Core.Exceptions;
using Policies.Core.Helpers;

namespace Policies.Api.Controllers.Auth
{
    [ApiController]
    [Produces(Constants.ContentType)]
    [Route(Constants.RouteAuth, Name = Constants.AuthTitle)]
    public class AuthController : BaseController
    {
        public AuthController(Func<string, IServiceFactory> serviceFactory) : base(serviceFactory)
        {
        }

        /// <summary>
        /// Inicia sesión en el sistema
        /// </summary>
        [HttpPost(Constants.Login)]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await serviceFactory("Test").ServiceAuth.LoginAsync(request);
                return Ok(new LoginResponseDto 
                { 
                    Token = response.Token,
                    User = response.User
                });
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
        /// Cambia la contraseña del usuario
        /// </summary>
        [HttpPost(Constants.PassChange)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            try
            {
                await serviceFactory("Test").ServiceAuth.ChangePasswordAsync(request);
                return Ok();
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