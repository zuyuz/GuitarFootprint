using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarFootprint.Domain.Commands;
using GuitarFootprint.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.WebAPI.Controllers
{
    [Route("auth")]
    public class AuthController : ApiControllerBase
    {  
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            return await TryAsync(CommandAsync(command))
                .Map(list => (IActionResult) Ok(list))
                .IfFail(exception => (IActionResult) BadRequest(exception.Message));
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            return await TryAsync(CommandAsync(command))
                .Map(list => (IActionResult)Ok(list))
                .IfFail(exception => (IActionResult) BadRequest(exception.Message));
        }
    }
}
