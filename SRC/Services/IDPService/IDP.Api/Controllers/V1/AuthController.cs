using Asp.Versioning;
using IDP.Application.Command.Auth;
using IDP.Application.Command.User;
using IDP.Application.Query.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{

    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        public readonly     IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public  async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var res = await _mediator.Send(authQuery);

            return Ok(res);
        }
        /// <summary>
        /// ثبت نام و ارسال کد
        /// </summary>
        /// <param name="authCommand"></param>
        /// <returns></returns>
        [HttpPost("RegisterAndSendOtp")]
        public async Task<IActionResult> RegisterAndSendOtp([FromBody] AuthCommand authCommand)
        {
            var res= await _mediator.Send(authCommand);
            return Ok(res);
        }
    }
}
