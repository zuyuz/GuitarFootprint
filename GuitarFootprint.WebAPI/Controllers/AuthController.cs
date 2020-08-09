using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarFootprint.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GuitarFootprint.WebAPI.Controllers
{
    public class AuthController : ApiControllerBase
    {  
        private IConfiguration _config;

        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken("fake@email.com");
            return token;
        }
    }
}
