using IDP.Api.Controllers.BaseController;
using IDP.Application.Command.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : IBaseController
    {
        public readonly IMediator mediator;
        public UserController(IMediator _mediator)
        {
            mediator=_mediator;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(UserCommand  userCommand)
        {
            var res= await mediator.Send(userCommand);
            return Ok(res);
        }

    }
}
