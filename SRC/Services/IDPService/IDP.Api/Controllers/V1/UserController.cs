using Asp.Versioning;
using IDP.Api.Controllers.BaseController;
using IDP.Application.Command.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{

    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{v:apiVersion}/[controller]")]

    public class UserController : IBaseController
    {
        public readonly IMediator mediator;
        public UserController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost("Insert")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> Insert(UserCommand userCommand)
        {
            var res = await mediator.Send(userCommand);
            return Ok(res);
        }
        [HttpPost("Insert")]
        [MapToApiVersion(2)]
        public async Task<IActionResult> Insert2(UserCommand userCommand)
        {
            var res = await mediator.Send(userCommand);
            return Ok(res);
        }
    }

        
}
